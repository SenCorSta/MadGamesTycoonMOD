using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000399 RID: 921
	[ExecuteInEditMode]
	public class SuimonoModuleFX : MonoBehaviour
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x0016258C File Offset: 0x0016078C
		private void Start()
		{
			this.fxParentObject = base.transform.Find("_particle_effects");
			this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			if (Application.isPlaying && this.effectsSystems.Length != 0 && this.fxParentObject != null)
			{
				Vector3 position = new Vector3(base.transform.position.x, -10000f, base.transform.position.z);
				this.fxObjects = new Transform[this.effectsSystems.Length];
				this.fxParticles = new ParticleSystem[this.effectsSystems.Length];
				for (int i = 0; i < this.effectsSystems.Length; i++)
				{
					Transform transform = UnityEngine.Object.Instantiate<Transform>(this.effectsSystems[i], position, base.transform.rotation);
					transform.transform.parent = this.fxParentObject.transform;
					this.fxObjects[i] = transform;
					this.fxParticles[i] = transform.gameObject.GetComponent<ParticleSystem>();
				}
			}
			SuimonoModuleFX.staggerOffset++;
			this.stagger = ((float)SuimonoModuleFX.staggerOffset + 0f) * 0.05f;
			SuimonoModuleFX.staggerOffset %= SuimonoModuleFX.staggerModulus;
			float repeatRate = 0.25f;
			base.InvokeRepeating("ClampSystems", 0.15f + this.stagger, repeatRate);
			base.InvokeRepeating("UpdateSystems", 0.2f + this.stagger, 1f);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00162710 File Offset: 0x00160910
		private void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.sysNames = new List<string>();
				this.sysNames.Add("None");
				this.sN = 0;
				while (this.sN < this.effectsSystems.Length)
				{
					this.setName = "---";
					if (this.effectsSystems[this.sN] != null)
					{
						this.setName = this.effectsSystems[this.sN].transform.name;
					}
					this.s = 0;
					while (this.s < this.sN)
					{
						this.setName += " ";
						this.s++;
					}
					this.sysNames.Add(this.setName);
					this.sN++;
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x001627F8 File Offset: 0x001609F8
		private void UpdateSystems()
		{
			if (Application.isPlaying)
			{
				this.sysNames = new List<string>();
				this.sysNames.Add("None");
				this.sN = 0;
				while (this.sN < this.effectsSystems.Length)
				{
					this.setName = "---";
					if (this.effectsSystems[this.sN] != null)
					{
						this.setName = this.effectsSystems[this.sN].transform.name;
					}
					this.s = 0;
					while (this.s < this.sN)
					{
						this.setName += " ";
						this.s++;
					}
					this.sysNames.Add(this.setName);
					this.sN++;
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x001628E0 File Offset: 0x00160AE0
		private void ClampSystems()
		{
			this.fx = 0;
			while (this.fx < this.fxObjects.Length)
			{
				if (this.fxObjects[this.fx] != null && this.clampIndex[this.fx] != 0)
				{
					this.currPXWaterPos = 0f;
					this.useParticleComponent = this.fxParticles[this.fx];
					if (this.setParticles == null)
					{
						this.setParticles = new ParticleSystem.Particle[this.useParticleComponent.particleCount];
					}
					this.useParticleComponent.GetParticles(this.setParticles);
					if ((float)this.useParticleComponent.particleCount > 0f)
					{
						this.px = 0;
						while (this.px < this.useParticleComponent.particleCount)
						{
							this.currPXWaterPos = this.moduleObject.SuimonoGetHeight(this.setParticles[this.px].position, "surfaceLevel");
							if (this.clampIndex[this.fx] == 1)
							{
								this.setParticles[this.px].position = new Vector3(this.setParticles[this.px].position.x, this.currPXWaterPos + 0.2f, this.setParticles[this.px].position.z);
							}
							if (this.clampIndex[this.fx] == 2 && this.setParticles[this.px].position.y > this.currPXWaterPos - 0.2f)
							{
								this.setParticles[this.px].position = new Vector3(this.setParticles[this.px].position.x, this.currPXWaterPos - 0.2f, this.setParticles[this.px].position.z);
							}
							if (this.clampIndex[this.fx] == 3 && this.setParticles[this.px].position.y < this.currPXWaterPos + 0.2f)
							{
								this.setParticles[this.px].position = new Vector3(this.setParticles[this.px].position.x, this.currPXWaterPos + 0.2f, this.setParticles[this.px].position.z);
							}
							this.px++;
						}
						this.useParticleComponent.SetParticles(this.setParticles, this.setParticles.Length);
						this.useParticleComponent.Play();
					}
				}
				this.fx++;
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00162BC8 File Offset: 0x00160DC8
		public void AddSystem()
		{
			this.tempSystems = this.effectsSystems;
			this.tempClamp = this.clampIndex;
			this.effectsSystems = new Transform[this.tempSystems.Length + 1];
			this.clampIndex = new int[this.tempClamp.Length + 1];
			this.aR = 0;
			while (this.aR < this.tempSystems.Length)
			{
				this.effectsSystems[this.aR] = this.tempSystems[this.aR];
				this.clampIndex[this.aR] = this.tempClamp[this.aR];
				this.aR++;
			}
			this.effectsSystems[this.tempSystems.Length] = null;
			this.clampIndex[this.tempClamp.Length] = 0;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00016D10 File Offset: 0x00014F10
		public void AddParticle(ParticleSystem.Particle particleData)
		{
			this.particleReserve.Add(particleData);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00016D1E File Offset: 0x00014F1E
		private IEnumerator updateFX()
		{
			this.efx = 0;
			while (this.efx < this.effectsSystems.Length)
			{
				this.epx = 0;
				while (this.epx < this.particleReserve.Count)
				{
					if (Mathf.Floor(this.particleReserve[this.epx].angularVelocity) == (float)this.efx)
					{
						this.fxParticles[this.fx].Emit(1);
					}
					this.epx++;
				}
				this.efx++;
			}
			this.fx = 0;
			while (this.fx < this.effectsSystems.Length)
			{
				this.px = 0;
				while (this.px < this.particleReserve.Count)
				{
					if (Mathf.Floor(this.particleReserve[this.px].angularVelocity) == (float)this.fx)
					{
						this.useParticleComponent = this.fxParticles[this.fx];
						if (this.setParticles == null)
						{
							this.setParticles = new ParticleSystem.Particle[this.useParticleComponent.particleCount];
						}
						this.useParticleComponent.GetParticles(this.setParticles);
						this.sx = this.useParticleComponent.particleCount - 1;
						while (this.sx < this.useParticleComponent.particleCount)
						{
							this.setParticles[this.px].position = this.particleReserve[this.px].position;
							this.setParticles[this.px].startSize = this.particleReserve[this.px].startSize;
							this.setParticles[this.px].rotation = this.particleReserve[this.px].rotation;
							this.setParticles[this.px].velocity = this.particleReserve[this.px].velocity;
							this.sx++;
						}
						this.useParticleComponent.SetParticles(this.setParticles, this.setParticles.Length);
					}
					this.px++;
				}
				this.fx++;
			}
			yield return null;
			if (this.particleReserve == null)
			{
				this.particleReserve = new List<ParticleSystem.Particle>();
			}
			yield break;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00162C94 File Offset: 0x00160E94
		public void DeleteSystem(int sysNum)
		{
			this.tempSystems = this.effectsSystems;
			this.tempClamp = this.clampIndex;
			this.endLP = this.tempSystems.Length - 1;
			if (this.endLP <= 0)
			{
				this.endLP = 0;
				if (this.effectsSystems == null)
				{
					this.effectsSystems = new Transform[this.tempSystems.Length + 1];
				}
				if (this.clampIndex == null)
				{
					this.clampIndex = new int[this.tempSystems.Length + 1];
					return;
				}
			}
			else
			{
				this.tempSystems = new Transform[this.endLP];
				this.setInt = 0;
				this.aR = 0;
				while (this.aR < this.endLP + 1)
				{
					if (this.aR != sysNum)
					{
						this.tempSystems[this.setInt] = this.effectsSystems[this.aR];
						this.setInt++;
					}
					this.aR++;
				}
				this.effectsSystems = this.tempSystems;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00016D2D File Offset: 0x00014F2D
		private void OnApplicationQuit()
		{
			this.fx = 0;
			while (this.fx < this.effectsSystems.Length)
			{
				UnityEngine.Object.Destroy(this.fxObjects[this.fx]);
				this.fx++;
			}
		}

		// Token: 0x04002A1E RID: 10782
		public string[] effectsLabels;

		// Token: 0x04002A1F RID: 10783
		public Transform[] effectsSystems;

		// Token: 0x04002A20 RID: 10784
		public Sui_FX_ClampType systemClampType;

		// Token: 0x04002A21 RID: 10785
		public Transform[] fxObjects;

		// Token: 0x04002A22 RID: 10786
		public ParticleSystem[] fxParticles;

		// Token: 0x04002A23 RID: 10787
		public int[] clampIndex;

		// Token: 0x04002A24 RID: 10788
		public List<string> clampOptions = new List<string>
		{
			"No Clamp",
			"Clamp to Surface",
			"Keep Below Surface",
			"Keep Above Surface"
		};

		// Token: 0x04002A25 RID: 10789
		public List<ParticleSystem.Particle> particleReserve = new List<ParticleSystem.Particle>();

		// Token: 0x04002A26 RID: 10790
		private Transform fxParentObject;

		// Token: 0x04002A27 RID: 10791
		private SuimonoModule moduleObject;

		// Token: 0x04002A28 RID: 10792
		private int fx;

		// Token: 0x04002A29 RID: 10793
		private int px;

		// Token: 0x04002A2A RID: 10794
		private float currPXWaterPos;

		// Token: 0x04002A2B RID: 10795
		private ParticleSystem useParticleComponent;

		// Token: 0x04002A2C RID: 10796
		private ParticleSystem.Particle[] setParticles;

		// Token: 0x04002A2D RID: 10797
		private Transform[] tempSystems;

		// Token: 0x04002A2E RID: 10798
		private int[] tempClamp;

		// Token: 0x04002A2F RID: 10799
		private int aR;

		// Token: 0x04002A30 RID: 10800
		private int efx;

		// Token: 0x04002A31 RID: 10801
		private int epx;

		// Token: 0x04002A32 RID: 10802
		private int sx;

		// Token: 0x04002A33 RID: 10803
		private int endLP;

		// Token: 0x04002A34 RID: 10804
		private int setInt;

		// Token: 0x04002A35 RID: 10805
		public List<string> sysNames = new List<string>();

		// Token: 0x04002A36 RID: 10806
		public int sN;

		// Token: 0x04002A37 RID: 10807
		public int s;

		// Token: 0x04002A38 RID: 10808
		public string setName;

		// Token: 0x04002A39 RID: 10809
		private static int staggerOffset = 0;

		// Token: 0x04002A3A RID: 10810
		private static int staggerModulus = 20;

		// Token: 0x04002A3B RID: 10811
		private float stagger;
	}
}
