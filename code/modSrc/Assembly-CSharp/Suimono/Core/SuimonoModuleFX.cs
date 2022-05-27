using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class SuimonoModuleFX : MonoBehaviour
	{
		
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

		
		public void AddParticle(ParticleSystem.Particle particleData)
		{
			this.particleReserve.Add(particleData);
		}

		
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

		
		private void OnApplicationQuit()
		{
			this.fx = 0;
			while (this.fx < this.effectsSystems.Length)
			{
				UnityEngine.Object.Destroy(this.fxObjects[this.fx]);
				this.fx++;
			}
		}

		
		public string[] effectsLabels;

		
		public Transform[] effectsSystems;

		
		public Sui_FX_ClampType systemClampType;

		
		public Transform[] fxObjects;

		
		public ParticleSystem[] fxParticles;

		
		public int[] clampIndex;

		
		public List<string> clampOptions = new List<string>
		{
			"No Clamp",
			"Clamp to Surface",
			"Keep Below Surface",
			"Keep Above Surface"
		};

		
		public List<ParticleSystem.Particle> particleReserve = new List<ParticleSystem.Particle>();

		
		private Transform fxParentObject;

		
		private SuimonoModule moduleObject;

		
		private int fx;

		
		private int px;

		
		private float currPXWaterPos;

		
		private ParticleSystem useParticleComponent;

		
		private ParticleSystem.Particle[] setParticles;

		
		private Transform[] tempSystems;

		
		private int[] tempClamp;

		
		private int aR;

		
		private int efx;

		
		private int epx;

		
		private int sx;

		
		private int endLP;

		
		private int setInt;

		
		public List<string> sysNames = new List<string>();

		
		public int sN;

		
		public int s;

		
		public string setName;

		
		private static int staggerOffset = 0;

		
		private static int staggerModulus = 20;

		
		private float stagger;
	}
}
