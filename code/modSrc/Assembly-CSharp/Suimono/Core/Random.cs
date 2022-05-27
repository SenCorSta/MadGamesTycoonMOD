using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000398 RID: 920
	public class Random
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x0016012A File Offset: 0x0015E32A
		public Random(int seed = 1)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0016014E File Offset: 0x0015E34E
		public void Reset()
		{
			this.m_stateA = 181353UL * (ulong)this.m_seed;
			this.m_stateB = 7UL * (ulong)this.m_seed;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00160174 File Offset: 0x0015E374
		public void Reset(int seed)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00160194 File Offset: 0x0015E394
		public void Reset(ulong stateA, ulong stateB)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Resetting RNG State ",
				stateA,
				" ",
				stateB
			}));
			this.m_stateA = stateA;
			this.m_stateB = stateB;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x001601E1 File Offset: 0x0015E3E1
		public void GetState(out int seed, out ulong stateA, out ulong stateB)
		{
			seed = this.m_seed;
			stateA = this.m_stateA;
			stateB = this.m_stateB;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x001601FC File Offset: 0x0015E3FC
		public float Next()
		{
			ulong num = this.m_stateA;
			ulong stateB = this.m_stateB;
			this.m_stateA = stateB;
			num ^= num << 23;
			num ^= num >> 17;
			num ^= (stateB ^ stateB >> 26);
			this.m_stateB = num;
			return (num + stateB) / 1.8446744E+19f;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00160247 File Offset: 0x0015E447
		public int NextInt()
		{
			return (int)(this.Next() * 2.1474836E+09f);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00160256 File Offset: 0x0015E456
		public float Next(float min, float max)
		{
			return min + this.Next() * (max - min);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00160264 File Offset: 0x0015E464
		public int Next(int min, int max)
		{
			if (min == max)
			{
				return min;
			}
			return (int)this.Next((float)min, (float)max + 0.999f);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0016027D File Offset: 0x0015E47D
		public Vector3 NextVector()
		{
			return new Vector3(this.Next(), this.Next(), this.Next());
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00160296 File Offset: 0x0015E496
		public Vector3 NextVector(float min, float max)
		{
			return new Vector3(this.Next(min, max), this.Next(min, max), this.Next(min, max));
		}

		// Token: 0x0400297D RID: 10621
		private const ulong m_A_Init = 181353UL;

		// Token: 0x0400297E RID: 10622
		private const ulong m_B_Init = 7UL;

		// Token: 0x0400297F RID: 10623
		public int m_seed;

		// Token: 0x04002980 RID: 10624
		public ulong m_stateA;

		// Token: 0x04002981 RID: 10625
		public ulong m_stateB;
	}
}
