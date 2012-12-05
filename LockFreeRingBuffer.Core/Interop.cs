using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LockFreeRingBuffer.Core
{
    public class Interop
    {
        [DllImport("LockFreeRingBuffer.Unmanaged32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MFence();

        [DllImport("LockFreeRingBuffer.Unmanaged32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LFence();

        [DllImport("LockFreeRingBuffer.Unmanaged32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SFence();
    }
}
