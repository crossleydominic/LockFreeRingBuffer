//#define USE_MANAGED_FENCE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockFreeRingBuffer.Core
{
#if USE_MANAGED_FENCE    
    public static class Fences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitFullFence()
        {
            Thread.MemoryBarrier();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitAcquireFence()
        {
            Thread.MemoryBarrier();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitReleaseFence()
        {
            Thread.MemoryBarrier();
        }
    }
#else
    public static class Fences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitFullFence()
        {
            Interop.MFence();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitAcquireFence()
        {
            Interop.LFence();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EmitReleaseFence()
        {
            Interop.SFence();
        }
    }
#endif
}
