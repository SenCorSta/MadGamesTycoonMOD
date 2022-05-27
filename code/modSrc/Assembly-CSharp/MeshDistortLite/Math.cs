using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E7 RID: 999
	public class Math : ScriptableObject
	{
		// Token: 0x06002384 RID: 9092 RVA: 0x000182D7 File Offset: 0x000164D7
		public static float Repeat(float num, float min, float max)
		{
			if (num < min)
			{
				return max - (min - num) % (max - min);
			}
			return min + (num - min) % (max - min);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000182F0 File Offset: 0x000164F0
		public static float PingPong(float num, float min, float max)
		{
			min = Math.Repeat(num, min, 2f * max);
			if (min < max)
			{
				return min;
			}
			return 2f * max - min;
		}
	}
}
