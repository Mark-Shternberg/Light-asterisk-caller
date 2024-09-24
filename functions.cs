using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Light_Asterisk_Caller
{
    internal class functions
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize64Bit(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);

        public static void FlushMem()
        {
            // Сборка мусора
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                IntPtr processHandle = System.Diagnostics.Process.GetCurrentProcess().Handle;

                // Определение разрядности
                if (IntPtr.Size == 4) // 32-битная система
                {
                    SetProcessWorkingSetSize32Bit(processHandle, -1, -1);
                }
                else if (IntPtr.Size == 8) // 64-битная система
                {
                    SetProcessWorkingSetSize64Bit(processHandle, -1, -1);
                }
            }
        }
    }
}
