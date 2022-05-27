using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039C RID: 924
	[ExecuteInEditMode]
	public class SuimonoModuleFX : MonoBehaviour
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x00163AC4 File Offset: 0x00161CC4
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

		// Token: 0x06002267 RID: 8807 RVA: 0x00163C48 File Offset: 0x00161E48
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

		// Token: 0x06002268 RID: 8808 RVA: 0x00163D30 File Offset: 0x00161F30
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

		// Token: 0x06002269 RID: 8809 RVA: 0x00163E18 File Offset: 0x00162018
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

		// Token: 0x0600226A RID: 8810 RVA: 0x00164100 File Offset: 0x00162300
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

		// Token: 0x0600226B RID: 8811 RVA: 0x001641CA File Offset: 0x001623CA
		public void AddParticle(ParticleSystem.Particle particleData)
		{
			this.particleReserve.Add(particleData);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x001641D8 File Offset: 0x001623D8
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

		// Token: 0x0600226D RID: 8813 RVA: 0x001641E8 File Offset: 0x001623E8
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

		// Token: 0x0600226E RID: 8814 RVA: 0x001642E7 File Offset: 0x001624E7
		private void OnApplicationQuit()
		{
			this.fx = 0;
			while (this.fx < this.effectsSystems.Length)
			{
				UnityEngine.Object.Destroy(this.fxObjects[this.fx]);
				this.fx++;
			}
		}

		// Token: 0x04002A34 RID: 10804
		public string[] effectsLabels;

		// Token: 0x04002A35 RID: 10805
		public Transform[] effectsSystems;

		// Token: 0x04002A36 RID: 10806
		public Sui_FX_ClampType systemClampType;

		// Token: 0x04002A37 RID: 10807
		public Transform[] fxObjects;

		// Token: 0x04002A38 RID: 10808
		public ParticleSystem[] fxParticles;

		// Token: 0x04002A39 RID: 10809
		public int[] clampIndex;

		// Token: 0x04002A3A RID: 10810
		public List<string> clampOptions = new List<string>
		{
			"No Clamp",
			"Clamp to Surface",
			"Keep Below Surface",
			"Keep Above Surface"
		};

		// Token: 0x04002A3B RID: 10811
		public List<ParticleSystem.Particle> particleReserve = new List<ParticleSystem.Particle>();

		// Token: 0x04002A3C RID: 10812
		private Transform fxParentObject;

		// Token: 0x04002A3D RID: 10813
		private SuimonoModule moduleObject;

		// Token: 0x04002A3E RID: 10814
		private int fx;

		// Token: 0x04002A3F RID: 10815
		private int px;

		// Token: 0x04002A40 RID: 10816
		private float currPXWaterPos;

		// Token: 0x04002A41 RID: 10817
		private ParticleSystem useParticleComponent;

		// Token: 0x04002A42 RID: 10818
		private ParticleSystem.Particle[] setParticles;

		// Token: 0x04002A43 RID: 10819
		private Transform[] tempSystems;

		// Token: 0x04002A44 RID: 10820
		private int[] tempClamp;

		// Token: 0x04002A45 RID: 10821
		private int aR;

		// Token: 0x04002A46 RID: 10822
		private int efx;

		// Token: 0x04002A47 RID: 10823
		private int epx;

		// Token: 0x04002A48 RID: 10824
		private int sx;

		// Token: 0x04002A49 RID: 10825
		private int endLP;

		// Token: 0x04002A4A RID: 10826
		private int setInt;

		// Token: 0x04002A4B RID: 10827
		public List<string> sysNames = new List<string>();

		// Token: 0x04002A4C RID: 10828
		public int sN;

		// Token: 0x04002A4D RID: 10829
		public int s;

		// Token: 0x04002A4E RID: 10830
		public string setName;

		// Token: 0x04002A4F RID: 10831
		private static int staggerOffset = 0;

		// Token: 0x04002A50 RID: 10832
		private static int staggerModulus = 20;

		// Token: 0x04002A51 RID: 10833
		private float stagger;
	}
}
