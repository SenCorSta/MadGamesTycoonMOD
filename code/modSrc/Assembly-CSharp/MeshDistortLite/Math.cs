using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003EA RID: 1002
	public class Math : ScriptableObject
	{
		// Token: 0x060023D7 RID: 9175 RVA: 0x00172A6E File Offset: 0x00170C6E
		public static float Repeat(float num, float min, float max)
		{
			if (num < min)
			{
				return max - (min - num) % (max - min);
			}
			return min + (num - min) % (max - min);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00172A87 File Offset: 0x00170C87
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
