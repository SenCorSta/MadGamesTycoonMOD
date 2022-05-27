using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E0 RID: 992
	[RequireComponent(typeof(Distort))]
	public class AnimatedDistort : MonoBehaviour
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x00018195 File Offset: 0x00016395
		// (set) Token: 0x06002366 RID: 9062 RVA: 0x0001819F File Offset: 0x0001639F
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

		// Token: 0x06002367 RID: 9063 RVA: 0x000181AA File Offset: 0x000163AA
		private void Start()
		{
			this.Setup();
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000181B2 File Offset: 0x000163B2
		public void Play()
		{
			this.isPlaying = true;
			this.playingAnimationTime = 0f;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000181C6 File Offset: 0x000163C6
		public void Stop()
		{
			this.isPlaying = false;
			this.playingAnimationTime = 0f;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000181DA File Offset: 0x000163DA
		private void LateUpdate()
		{
			if (this.isPlaying && !this.updatingAnimation && this.playAnimationIndex == 0)
			{
				this.Animation(this.playingAnimationTime, Time.deltaTime);
				this.playingAnimationTime += Time.deltaTime;
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00018217 File Offset: 0x00016417
		private void Setup()
		{
			this.distort = base.GetComponent<Distort>();
			this.distort.MakeDynamic();
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00018230 File Offset: 0x00016430
		public void CalculateInRealTime()
		{
			this.playAnimationIndex = 0;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0016E8CC File Offset: 0x0016CACC
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

		// Token: 0x04002D70 RID: 11632
		public float animationFramesPerSec = 30f;

		// Token: 0x04002D71 RID: 11633
		public int animationFrames = 1;

		// Token: 0x04002D72 RID: 11634
		protected Distort distort;

		// Token: 0x04002D73 RID: 11635
		public AnimatedDistort.Type type;

		// Token: 0x04002D74 RID: 11636
		public AnimatedDistort.Animate animate = AnimatedDistort.Animate.displacement;

		// Token: 0x04002D75 RID: 11637
		public AnimationCurve curveType;

		// Token: 0x04002D76 RID: 11638
		public float constantSpeed = 1f;

		// Token: 0x04002D77 RID: 11639
		public float minValue;

		// Token: 0x04002D78 RID: 11640
		public float maxValue = 10f;

		// Token: 0x04002D79 RID: 11641
		public int playAnimationIndex;

		// Token: 0x04002D7A RID: 11642
		public bool updatingAnimation;

		// Token: 0x04002D7B RID: 11643
		public bool isPlaying = true;

		// Token: 0x04002D7C RID: 11644
		private float playingAnimationTime;

		// Token: 0x020003E1 RID: 993
		public enum Animate
		{
			// Token: 0x04002D7E RID: 11646
			force,
			// Token: 0x04002D7F RID: 11647
			displacement
		}

		// Token: 0x020003E2 RID: 994
		public enum Type
		{
			// Token: 0x04002D81 RID: 11649
			constant,
			// Token: 0x04002D82 RID: 11650
			pingpong,
			// Token: 0x04002D83 RID: 11651
			sin,
			// Token: 0x04002D84 RID: 11652
			curve
		}
	}
}
