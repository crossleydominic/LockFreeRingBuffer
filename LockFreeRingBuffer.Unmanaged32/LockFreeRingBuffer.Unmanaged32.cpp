// LockFreeRingBuffer.Unmanaged32.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

extern "C" __declspec(dllexport) void MFence()
{
	__asm{
		mfence;
	};
}

extern "C" __declspec(dllexport) void LFence()
{
	__asm{
		lfence;
	};
}

extern "C" __declspec(dllexport) void SFence()
{
	__asm{
		sfence;
	};
}
