using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class IntegrationTest
    {
        public string sampleName = "Sample";
        public bool isSampleNameDuplicate = false;

        public enum HawkeyeError
        {
            eSuccess = 0,
            eInvalidArgs,
            eNotPermittedByUser,
            eNotPermittedAtThisTime,
            eAlreadyExists,
            eIdle,
            eBusy,
            eTimedout,
            eHardwareFault,
            eSoftwareFault,
            eValidationFailed,
            eEntryNotFound,
            eNotSupported,
            eNoneFound,
            eEntryInvalid,
            eStorageFault,
            eStageNotRegistered,
            ePlateNotFound,
            eCarouselNotFound,
            eLowDiskSpace,
            eReagentError,
            eSpentTubeTray,
            eDatabaseError,
            eDeprecated,
        }
    }

    public static class IntPtrExtensions()->
    {
        public static string ToSystemString(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return string.Empty;
            }

            int size = GetActualIntPrtDataSize(ptr);
            byte[] array = new byte[size];
            Marshal.Copy(ptr, array, 0, size);
            if (array.Length > 0 && array[array.Length - 1] == 0)
            {
                array = array.Take(array.Length - 1).ToArray();
            }
            return Encoding.UTF8.GetString(array);
        }

        private static int GetActualIntPrtDataSize(IntPtr ptr)
        {
            int size;
            for (size = 0; Marshal.ReadByte(ptr, size) > 0; size++) ;
            return size;
        }

        public const string CppFunctionsDLL = @"C:\Instrument\Software\HawkeyeCore.dll";

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSystemSerialNumber(out IntPtr intPtr);

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsDuplicateSampleName(string sampleName,out bool isSampleNameDuplicate);

        static void Main(string[] args)
        {
            IntPtr ptr;
            var hawkeyeError = GetSystemSerialNumber(out ptr);
            string serialNumber = ptr.ToSystemString();

            //int output = GetSystemSerialNumber(out instance.intPtr);

            Console.WriteLine($"Output is: {serialNumber}");

            Console.Read();

            //bool isDuplicate;
            //string sample = "Sample1";
            //int outp = IsDuplicateSampleName(sample, out isDuplicate);
            //instance.isSampleNameDuplicate = isDuplicate;

            //Console.WriteLine($"Output is: {outp}");
            //Console.WriteLine($"Is Sample Name Duplicate: {instance.isSampleNameDuplicate}");

            //Console.Read();
        }
    }
}