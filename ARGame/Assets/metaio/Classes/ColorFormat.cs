using System;

namespace metaio.common
{
	/// <summary>
	/// Image color format enumeration.
	/// Same integer values as in the C++ header ColorFormat.h 
	/// </summary>
	public enum ColorFormat : int
	{
		RGB8 = 2,
		BGR8 = 3,
		RGBA8 = 4,
		BGRA8 = 5,		
		YUY2 = 8,
		YV12 = 10,
		GRAY8 = 11,
		NV21 = 12,
		NV12 = 13,
		D16 = 100,
		UV32 = 101,
		FLIR8 = 200,
		UNKNOWN = 999
	}
}

