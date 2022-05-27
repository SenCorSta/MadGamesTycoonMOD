using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000395 RID: 917
	public class Random
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x00016AF2 File Offset: 0x00014CF2
		public Random(int seed = 1)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00016B16 File Offset: 0x00014D16
		public void Reset()
		{
			this.m_stateA = 181353UL * (ulong)this.m_seed;
			this.m_stateB = 7UL * (ulong)this.m_seed;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00016B3C File Offset: 0x00014D3C
		public void Reset(int seed)
		{
			this.m_seed = seed;
			if (this.m_seed == 0)
			{
				this.m_seed = 1;
			}
			this.Reset();
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0015EEF8 File Offset: 0x0015D0F8
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

		// Token: 0x060021E9 RID: 8681 RVA: 0x00016B5A File Offset: 0x00014D5A
		public void GetState(out int seed, out ulong stateA, out ulong stateB)
		{
			seed = this.m_seed;
			stateA = this.m_stateA;
			stateB = this.m_stateB;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0015EF48 File Offset: 0x0015D148
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

		// Token: 0x060021EB RID: 8683 RVA: 0x00016B74 File Offset: 0x00014D74
		public int NextInt()
		{
			return (int)(this.Next() * 2.1474836E+09f);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00016B83 File Offset: 0x00014D83
		public float Next(float min, float max)
		{
			return min + this.Next() * (max - min);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00016B91 File Offset: 0x00014D91
		public int Next(int min, int max)
		{
			if (min == max)
			{
				return min;
			}
			return (int)this.Next((float)min, (float)max + 0.999f);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00016BAA File Offset: 0x00014DAA
		public Vector3 NextVector()
		{
			return new Vector3(this.Next(), this.Next(), this.Next());
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00016BC3 File Offset: 0x00014DC3
		public Vector3 NextVector(float min, float max)
		{
			return new Vector3(this.Next(min, max), this.Next(min, max), this.Next(min, max));
		}

		// Token: 0x04002967 RID: 10599
		private const ulong m_A_Init = 181353UL;

		// Token: 0x04002968 RID: 10600
		private const ulong m_B_Init = 7UL;

		// Token: 0x04002969 RID: 10601
		public int m_seed;

		// Token: 0x0400296A RID: 10602
		public ulong m_stateA;

		// Token: 0x0400296B RID: 10603
		public ulong m_stateB;
	}
}
