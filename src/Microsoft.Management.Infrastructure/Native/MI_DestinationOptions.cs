﻿using System;
using System.Runtime.InteropServices;

namespace NativeObject
{
    [StructLayout(LayoutKind.Sequential, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
    public struct MI_DestinationOptionsPtr
    {
        public IntPtr ptr;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
    public struct MI_DestinationOptionsOutPtr
    {
        public IntPtr ptr;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
    public class MI_DestinationOptions
    {
        public MI_Result SetInterval(
            string optionName,
            MI_Interval value,
            MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.SetInterval(this,
                optionName,
                ref value,
                flags);
            return resultLocal;
        }

        public MI_Result GetInterval(
            string optionName,
            out MI_Interval value,
            out UInt32 index,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_Interval valueLocal = new MI_Interval();
            MI_Result resultLocal = this.ft.GetInterval(this,
                optionName,
                ref valueLocal,
                out index,
                out flags);

            value = valueLocal;
            return resultLocal;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
        private struct MI_DestinationOptionsMembers
        {
            public UInt64 reserved1;
            public IntPtr reserved2;
            public IntPtr ft;
        }

        // Marshal implements these with Reflection - pay this hit only once
        private static int MI_DestinationOptionsMembersFTOffset = (int)Marshal.OffsetOf<MI_DestinationOptionsMembers>("ft");

        private static int MI_DestinationOptionsMembersSize = Marshal.SizeOf<MI_DestinationOptionsMembers>();

        private MI_DestinationOptionsPtr ptr;
        private bool isDirect;
        private Lazy<MI_DestinationOptionsFT> mft;

        ~MI_DestinationOptions()
        {
            Marshal.FreeHGlobal(this.ptr.ptr);
        }

        private MI_DestinationOptions(bool isDirect)
        {
            this.isDirect = isDirect;
            this.mft = new Lazy<MI_DestinationOptionsFT>(this.MarshalFT);

            var necessarySize = this.isDirect ? MI_DestinationOptionsMembersSize : NativeMethods.IntPtrSize;
            this.ptr.ptr = Marshal.AllocHGlobal(necessarySize);

            unsafe
            {
                NativeMethods.memset((byte*)this.ptr.ptr, 0, (uint)necessarySize);
            }
        }

        public static MI_DestinationOptions NewDirectPtr()
        {
            return new MI_DestinationOptions(true);
        }

        public static MI_DestinationOptions NewIndirectPtr()
        {
            return new MI_DestinationOptions(false);
        }

        public static MI_DestinationOptions NewFromDirectPtr(IntPtr ptr)
        {
            var res = new MI_DestinationOptions(false);
            Marshal.WriteIntPtr(res.ptr.ptr, ptr);
            return res;
        }

        public static implicit operator MI_DestinationOptionsPtr(MI_DestinationOptions instance)
        {
            // If the indirect pointer is zero then the object has not
            // been initialized and it is not valid to refer to its data
            if (instance != null && instance.Ptr == IntPtr.Zero)
            {
                throw new InvalidCastException();
            }

            return new MI_DestinationOptionsPtr() { ptr = instance == null ? IntPtr.Zero : instance.Ptr };
        }

        public static implicit operator MI_DestinationOptionsOutPtr(MI_DestinationOptions instance)
        {
            // We are not currently supporting the ability to get the address
            // of our direct pointer, though it is technically feasible
            if (instance != null && instance.isDirect)
            {
                throw new InvalidCastException();
            }

            return new MI_DestinationOptionsOutPtr() { ptr = instance == null ? IntPtr.Zero : instance.ptr.ptr };
        }

        public static MI_DestinationOptions Null { get { return null; } }
        public bool IsNull { get { return this.Ptr == IntPtr.Zero; } }

        public IntPtr Ptr
        {
            get
            {
                IntPtr structurePtr = this.ptr.ptr;
                if (!this.isDirect)
                {
                    if (structurePtr == IntPtr.Zero)
                    {
                        throw new InvalidOperationException();
                    }

                    // This can be easily implemented with Marshal.ReadIntPtr
                    // but that has function call overhead
                    unsafe
                    {
                        structurePtr = *(IntPtr*)structurePtr;
                    }
                }

                return structurePtr;
            }
        }

        public void Delete()
        {
            this.ft.Delete(this);
        }

        public MI_Result SetString(
            string optionName,
            string value,
            MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.SetString(this,
                optionName,
                value,
                flags);
            return resultLocal;
        }

        public MI_Result SetNumber(
            string optionName,
            UInt32 value,
            MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.SetNumber(this,
                optionName,
                value,
                flags);
            return resultLocal;
        }

        public MI_Result AddCredentials(
            string optionName,
            MI_UserCredentials credentials,
            MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.AddCredentials(this,
                optionName,
                credentials,
                flags);
            return resultLocal;
        }

        public MI_Result GetString(
            string optionName,
            out string value,
            out UInt32 index,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_String valueLocal = MI_String.NewIndirectPtr();

            MI_Result resultLocal = this.ft.GetString(this,
                optionName,
                valueLocal,
                out index,
                out flags);

            value = valueLocal.Value;
            return resultLocal;
        }

        public MI_Result GetNumber(
            string optionName,
            out UInt32 value,
            out UInt32 index,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.GetNumber(this,
                optionName,
                out value,
                out index,
                out flags);
            return resultLocal;
        }

        public MI_Result GetOptionCount(
            out UInt32 count
            )
        {
            MI_Result resultLocal = this.ft.GetOptionCount(this,
                out count);
            return resultLocal;
        }

        public MI_Result GetOptionAt(
            UInt32 index,
            out string optionName,
            MI_Value value,
            out MI_Type type,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_String optionNameLocal = MI_String.NewIndirectPtr();

            MI_Result resultLocal = this.ft.GetOptionAt(this,
                index,
                optionNameLocal,
                value,
                out type,
                out flags);

            optionName = optionNameLocal.Value;
            return resultLocal;
        }

        public MI_Result GetOption(
            string optionName,
            MI_Value value,
            out MI_Type type,
            out UInt32 index,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_Result resultLocal = this.ft.GetOption(this,
                optionName,
                value,
                out type,
                out index,
                out flags);
            return resultLocal;
        }

        public MI_Result GetCredentialsCount(
            out UInt32 count
            )
        {
            MI_Result resultLocal = this.ft.GetCredentialsCount(this,
                out count);
            return resultLocal;
        }

        public MI_Result GetCredentialsAt(
            UInt32 index,
            out string optionName,
            out MI_UserCredentials credentials,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_String optionNameLocal = MI_String.NewIndirectPtr();
            MI_UserCredentials credentialsLocal = new MI_UserCredentials();

            MI_Result resultLocal = this.ft.GetCredentialsAt(this,
                index,
                optionNameLocal,
                ref credentialsLocal,
                out flags);

            optionName = optionNameLocal.Value;
            credentials = credentialsLocal;
            return resultLocal;
        }

        public MI_Result GetCredentialsPasswordAt(
            UInt32 index,
            out string optionName,
            string password,
            UInt32 bufferLength,
            out UInt32 passwordLength,
            out MI_DestinationOptionsFlags flags
            )
        {
            MI_String optionNameLocal = MI_String.NewIndirectPtr();

            MI_Result resultLocal = this.ft.GetCredentialsPasswordAt(this,
                index,
                optionNameLocal,
                password,
                bufferLength,
                out passwordLength,
                out flags);

            optionName = optionNameLocal.Value;
            return resultLocal;
        }

        public MI_Result Clone(
            out MI_DestinationOptions newDestinationOptions
            )
        {
            MI_DestinationOptions newDestinationOptionsLocal =
                MI_DestinationOptions.NewIndirectPtr();

            MI_Result resultLocal = this.ft.Clone(this,
                newDestinationOptionsLocal);

            newDestinationOptions = newDestinationOptionsLocal;
            return resultLocal;
        }

        private MI_DestinationOptionsFT ft { get { return this.mft.Value; } }

        private MI_DestinationOptionsFT MarshalFT()
        {
            MI_DestinationOptionsFT res = new MI_DestinationOptionsFT();
            IntPtr ftPtr = IntPtr.Zero;
            unsafe
            {
                // Just as easily could be implemented with Marshal
                // but that would copy more than the one pointer we need
                IntPtr structurePtr = this.Ptr;
                if (structurePtr == IntPtr.Zero)
                {
                    throw new InvalidOperationException();
                }

                ftPtr = *((IntPtr*)((byte*)structurePtr + MI_DestinationOptionsMembersFTOffset));
            }

            if (ftPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            // No apparent way to implement this in an unsafe block
            Marshal.PtrToStructure(ftPtr, res);
            return res;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
        public class MI_DestinationOptionsFT
        {
            public MI_DestinationOptions_Delete Delete;
            public MI_DestinationOptions_SetString SetString;
            public MI_DestinationOptions_SetNumber SetNumber;
            public MI_DestinationOptions_AddCredentials AddCredentials;
            public MI_DestinationOptions_GetString GetString;
            public MI_DestinationOptions_GetNumber GetNumber;
            public MI_DestinationOptions_GetOptionCount GetOptionCount;
            public MI_DestinationOptions_GetOptionAt GetOptionAt;
            public MI_DestinationOptions_GetOption GetOption;
            public MI_DestinationOptions_GetCredentialsCount GetCredentialsCount;
            public MI_DestinationOptions_GetCredentialsAt GetCredentialsAt;
            public MI_DestinationOptions_GetCredentialsPasswordAt GetCredentialsPasswordAt;
            public MI_DestinationOptions_Clone Clone;
            public MI_DestinationOptions_SetInterval SetInterval;
            public MI_DestinationOptions_GetInterval GetInterval;

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate void MI_DestinationOptions_Delete(
                MI_DestinationOptionsPtr options
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_SetString(
                MI_DestinationOptionsPtr options,
                string optionName,
                string value,
                MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_SetNumber(
                MI_DestinationOptionsPtr options,
                string optionName,
                UInt32 value,
                MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_AddCredentials(
                MI_DestinationOptionsPtr options,
                string optionName,
                MI_UserCredentials credentials,
                MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetString(
                MI_DestinationOptionsPtr options,
                string optionName,
                [In, Out] MI_String value,
                out UInt32 index,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetNumber(
                MI_DestinationOptionsPtr options,
                string optionName,
                out UInt32 value,
                out UInt32 index,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetOptionCount(
                MI_DestinationOptionsPtr options,
                out UInt32 count
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetOptionAt(
                MI_DestinationOptionsPtr options,
                UInt32 index,
                [In, Out] MI_String optionName,
                [In, Out] MI_Value.MIValueBlock value,
                out MI_Type type,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetOption(
                MI_DestinationOptionsPtr options,
                string optionName,
                [In, Out] MI_Value.MIValueBlock value,
                out MI_Type type,
                out UInt32 index,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetCredentialsCount(
                MI_DestinationOptionsPtr options,
                out UInt32 count
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetCredentialsAt(
                MI_DestinationOptionsPtr options,
                UInt32 index,
                [In, Out] MI_String optionName,
                ref MI_UserCredentials credentials,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetCredentialsPasswordAt(
                MI_DestinationOptionsPtr options,
                UInt32 index,
                [In, Out] MI_String optionName,
                string password,
                UInt32 bufferLength,
                out UInt32 passwordLength,
                out MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_Clone(
                MI_DestinationOptionsPtr self,
                [In, Out] MI_DestinationOptionsPtr newDestinationOptions
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_SetInterval(
                MI_DestinationOptionsPtr options,
                string optionName,
                ref MI_Interval value,
                MI_DestinationOptionsFlags flags
                );

            [UnmanagedFunctionPointer(MI_PlatformSpecific.MiCallConvention, CharSet = MI_PlatformSpecific.AppropriateCharSet)]
            public delegate MI_Result MI_DestinationOptions_GetInterval(
                MI_DestinationOptionsPtr options,
                string optionName,
                ref MI_Interval value,
                out UInt32 index,
                out MI_DestinationOptionsFlags flags
                );
        }
    }
}