using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E3 RID: 995
	[RequireComponent(typeof(Distort))]
	public class AnimatedDistort : MonoBehaviour
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0017134A File Offset: 0x0016F54A
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x00171354 File Offset: 0x0016F554
		public int currentAnimation
		{
			get
			{
				return this.playAnimationIndex - 1;
			}
			set
			{
				this.playAnimationIndex = value + 1;
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0017135F File Offset: 0x0016F55F
		private void Start()
		{
			this.Setup();
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00171367 File Offset: 0x0016F567
		public void Play()
		{
			this.isPlaying = true;
			this.playingAnimationTime = 0f;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0017137B File Offset: 0x0016F57B
		public void Stop()
		{
			this.isPlaying = false;
			this.playingAnimationTime = 0f;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0017138F File Offset: 0x0016F58F
		private void LateUpdate()
		{
			if (this.isPlaying && !this.updatingAnimation && this.playAnimationIndex == 0)
			{
				this.Animation(this.playingAnimationTime, Time.deltaTime);
				this.playingAnimationTime += Time.deltaTime;
			}
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x001713CC File Offset: 0x0016F5CC
		private void Setup()
		{
			this.distort = base.GetComponent<Distort>();
			this.distort.MakeDynamic();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x001713E5 File Offset: 0x0016F5E5
		public void CalculateInRealTime()
		{
			this.playAnimationIndex = 0;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x001713F0 File Offset: 0x0016F5F0
		private void Animation(float displaceOffset, float delta)
		{
			if (this.distort == null)
			{
				this.Setup();
			}
			for (int i = 0; i < this.distort.distort.Count; i++)
			{
				if (this.type == AnimatedDistort.Type.constant)
				{
					if (this.animate == AnimatedDistort.Animate.displacement)
					{
						float num = this.constantSpeed * delta * this.distort.distort[i].animationSpeed;
						this.distort.distort[i].movementDisplacement += num;
					}
					else
					{
						this.distort.distort[i].force += this.constantSpeed * delta * this.distort.distort[i].animationSpeed;
					}
				}
				else if (this.type == AnimatedDistort.Type.pingpong)
				{
					if (this.animate == AnimatedDistort.Animate.displacement)
					{
						float movementDisplacement = Mathf.Lerp(this.minValue, this.maxValue, Mathf.PingPong(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed, 1f));
						this.distort.distort[i].movementDisplacement = movementDisplacement;
					}
					else
					{
						this.distort.distort[i].force = Mathf.Lerp(this.minValue, this.maxValue, Mathf.PingPong(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed, 1f));
					}
				}
				else if (this.type == AnimatedDistort.Type.sin)
				{
					if (this.animate == AnimatedDistort.Animate.displacement)
					{
						float movementDisplacement2 = Mathf.Lerp(this.minValue, this.maxValue, (Mathf.Sin(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed) + 1f) * 0.5f);
						this.distort.distort[i].movementDisplacement = movementDisplacement2;
					}
					else
					{
						this.distort.distort[i].force = Mathf.Lerp(this.minValue, this.maxValue, (Mathf.Sin(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed) + 1f) * 0.5f);
					}
				}
				else if (this.type == AnimatedDistort.Type.curve)
				{
					if (this.animate == AnimatedDistort.Animate.displacement)
					{
						float movementDisplacement3 = this.curveType.Evaluate(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed);
						this.distort.distort[i].movementDisplacement = movementDisplacement3;
					}
					else
					{
						this.distort.distort[i].force = this.curveType.Evaluate(displaceOffset * this.constantSpeed * this.distort.distort[i].animationSpeed);
					}
				}
			}
			this.distort.UpdateDistort();
		}

		// Token: 0x04002D86 RID: 11654
		public float animationFramesPerSec = 30f;

		// Token: 0x04002D87 RID: 11655
		public int animationFrames = 1;

		// Token: 0x04002D88 RID: 11656
		protected Distort distort;

		// Token: 0x04002D89 RID: 11657
		public AnimatedDistort.Type type;

		// Token: 0x04002D8A RID: 11658
		public AnimatedDistort.Animate animate = AnimatedDistort.Animate.displacement;

		// Token: 0x04002D8B RID: 11659
		public AnimationCurve curveType;

		// Token: 0x04002D8C RID: 11660
		public float constantSpeed = 1f;

		// Token: 0x04002D8D RID: 11661
		public float minValue;

		// Token: 0x04002D8E RID: 11662
		public float maxValue = 10f;

		// Token: 0x04002D8F RID: 11663
		public int playAnimationIndex;

		// Token: 0x04002D90 RID: 11664
		public bool updatingAnimation;

		// Token: 0x04002D91 RID: 11665
		public bool isPlaying = true;

		// Token: 0x04002D92 RID: 11666
		private float playingAnimationTime;

		// Token: 0x020003E4 RID: 996
		public enum Animate
		{
			// Token: 0x04002D94 RID: 11668
			force,
			// Token: 0x04002D95 RID: 11669
			displacement
		}

		// Token: 0x020003E5 RID: 997
		public enum Type
		{
			// Token: 0x04002D97 RID: 11671
			constant,
			// Token: 0x04002D98 RID: 11672
			pingpong,
			// Token: 0x04002D99 RID: 11673
			sin,
			// Token: 0x04002D9A RID: 11674
			curve
		}
	}
}
