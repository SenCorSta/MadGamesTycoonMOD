using System;
using UnityEngine;

namespace MeshDistortLite
{
	
	[RequireComponent(typeof(Distort))]
	public class AnimatedDistort : MonoBehaviour
	{
		
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

		
		private void Start()
		{
			this.Setup();
		}

		
		public void Play()
		{
			this.isPlaying = true;
			this.playingAnimationTime = 0f;
		}

		
		public void Stop()
		{
			this.isPlaying = false;
			this.playingAnimationTime = 0f;
		}

		
		private void LateUpdate()
		{
			if (this.isPlaying && !this.updatingAnimation && this.playAnimationIndex == 0)
			{
				this.Animation(this.playingAnimationTime, Time.deltaTime);
				this.playingAnimationTime += Time.deltaTime;
			}
		}

		
		private void Setup()
		{
			this.distort = base.GetComponent<Distort>();
			this.distort.MakeDynamic();
		}

		
		public void CalculateInRealTime()
		{
			this.playAnimationIndex = 0;
		}

		
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

		
		public float animationFramesPerSec = 30f;

		
		public int animationFrames = 1;

		
		protected Distort distort;

		
		public AnimatedDistort.Type type;

		
		public AnimatedDistort.Animate animate = AnimatedDistort.Animate.displacement;

		
		public AnimationCurve curveType;

		
		public float constantSpeed = 1f;

		
		public float minValue;

		
		public float maxValue = 10f;

		
		public int playAnimationIndex;

		
		public bool updatingAnimation;

		
		public bool isPlaying = true;

		
		private float playingAnimationTime;

		
		public enum Animate
		{
			
			force,
			
			displacement
		}

		
		public enum Type
		{
			
			constant,
			
			pingpong,
			
			sin,
			
			curve
		}
	}
}
