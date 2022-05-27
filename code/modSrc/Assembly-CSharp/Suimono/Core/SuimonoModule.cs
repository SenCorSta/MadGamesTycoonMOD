using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x02000398 RID: 920
	[ExecuteInEditMode]
	public class SuimonoModule : MonoBehaviour
	{
		// Token: 0x060021F7 RID: 8695 RVA: 0x00016C56 File Offset: 0x00014E56
		private void Awake()
		{
			this.suimonoVersionNumber = "2.1.13";
			base.gameObject.name = "SUIMONO_Module";
			this.sObjects = new List<SuimonoObject>();
			this.sRends = new List<Renderer>();
			this.sRendSCs = new List<Renderer>();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0015F004 File Offset: 0x0015D204
		private void Start()
		{
			this.randSeed = Environment.TickCount;
			this.modRand = new Suimono.Core.Random(this.randSeed);
			this.InitLayers();
			this.Suimono_CheckCamera();
			if (this.autoSetLayers)
			{
				this.lx = 0;
				while (this.lx < 32)
				{
					Physics.IgnoreLayerCollision(this.lx, this.layerWaterNum);
					this.lx++;
				}
			}
			this.suimonoModuleLibrary = base.gameObject.GetComponent<SuimonoModuleLib>();
			if (this.suimonoModuleLibrary.sndparentobj != null)
			{
				this.maxSounds = this.suimonoModuleLibrary.sndparentobj.maxSounds;
				this.sndComponents = new AudioSource[this.maxSounds];
				GameObject[] array = new GameObject[this.maxSounds];
				for (int i = 0; i < this.maxSounds; i++)
				{
					array[i] = new GameObject("SuimonoAudioObject");
					array[i].transform.parent = this.suimonoModuleLibrary.sndparentobj.transform;
					array[i].AddComponent<AudioSource>();
					this.sndComponents[i] = array[i].GetComponent<AudioSource>();
				}
				if (this.suimonoModuleLibrary.sndparentobj.underwaterSound != null)
				{
					this.underSoundObject = new GameObject("Underwater Sound");
					this.underSoundObject.AddComponent<AudioSource>();
					this.underSoundObject.transform.parent = this.suimonoModuleLibrary.sndparentobj.transform;
					this.underSoundComponent = this.underSoundObject.GetComponent<AudioSource>();
				}
			}
			if (this.disableMSAA)
			{
				QualitySettings.antiAliasing = 0;
			}
			this._colorspace = QualitySettings.activeColorSpace;
			if (this._colorspace == ColorSpace.Linear)
			{
				Shader.SetGlobalFloat("_Suimono_isLinear", 1f);
			}
			else
			{
				Shader.SetGlobalFloat("_Suimono_isLinear", 0f);
			}
			if (this.suimonoModuleLibrary != null)
			{
				if (this.suimonoModuleLibrary.texNormalC != null)
				{
					this.heightTex = this.suimonoModuleLibrary.texNormalC;
					this.pixelArray = this.suimonoModuleLibrary.texNormalC.GetPixels(0);
				}
				if (this.suimonoModuleLibrary.texNormalT != null)
				{
					this.heightTexT = this.suimonoModuleLibrary.texNormalT;
					this.pixelArrayT = this.suimonoModuleLibrary.texNormalT.GetPixels(0);
				}
				if (this.suimonoModuleLibrary.texNormalR != null)
				{
					this.heightTexR = this.suimonoModuleLibrary.texNormalR;
					this.pixelArrayR = this.suimonoModuleLibrary.texNormalR.GetPixels(0);
				}
			}
			this.tenObject = GameObject.Find("Tenkoku DynamicSky");
			Shader.SetGlobalFloat("_useTenkoku", 0f);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00016C94 File Offset: 0x00014E94
		private void InitLayers()
		{
			if (this.autoSetLayers && !this.layersAreSet)
			{
				this.layersAreSet = true;
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0015F2AC File Offset: 0x0015D4AC
		private void LateUpdate()
		{
			this._deltaTime = Time.deltaTime;
			if (this.modRand == null)
			{
				this.modRand = new Suimono.Core.Random(this.randSeed);
			}
			if (this.systemTime < 0f)
			{
				this.systemTime = 0f;
			}
			if (this.enableAutoAdvance)
			{
				this.systemTime += this._deltaTime;
			}
			this.SetCullFunction();
			this.useTenkoku = 0f;
			if (this.tenObject != null)
			{
				if (this.tenObject.activeInHierarchy)
				{
					this.useTenkoku = 1f;
				}
				if (this.useTenkoku == 1f)
				{
					if (this.setLight == null)
					{
						this.setLight = GameObject.Find("LIGHT_World").GetComponent<Light>();
					}
					if (this.tenkokuWindModule == null)
					{
						this.tenkokuWindModule = GameObject.Find("Tenkoku_WindZone").GetComponent<WindZone>();
					}
					else
					{
						this.tenkokuWindDir = this.tenkokuWindModule.transform.eulerAngles.y;
						this.tenkokuWindAmt = this.tenkokuWindModule.windMain;
					}
				}
			}
			Shader.SetGlobalFloat("_useTenkoku", this.useTenkoku);
			if (Application.isPlaying && this.fxRippleObject == null)
			{
				this.fxRippleObject = GameObject.Find("fx_rippleNormals(Clone)");
			}
			if (this.fxRippleObject != null)
			{
				this.fxRippleObject.layer = this.layerScreenFXNum;
			}
			if (this.suimonoModuleLibrary.normalsCamObject != null)
			{
				this.suimonoModuleLibrary.normalsCamObject.cullingMask = 1 << this.layerScreenFXNum;
			}
			if (this.suimonoModuleLibrary.wakeCamObject != null)
			{
				this.suimonoModuleLibrary.wakeCamObject.cullingMask = 1 << this.layerScreenFXNum;
			}
			if (this.suimonoModuleLibrary.transCamObject != null)
			{
				this.transLayer &= ~(1 << this.layerWaterNum);
				this.transLayer &= ~(1 << this.layerDepthNum);
				this.transLayer &= ~(1 << this.layerScreenFXNum);
				this.suimonoModuleLibrary.transCamObject.cullingMask = this.transLayer;
				this.suimonoModuleLibrary.transCamObject.farClipPlane = this.transRenderDistance * 1.2f;
				Shader.SetGlobalFloat("_suimonoTransDist", this.transRenderDistance);
			}
			else
			{
				this.suimonoModuleLibrary.transCamObject = base.transform.Find("cam_SuimonoTrans").gameObject.GetComponent<Camera>();
			}
			if (this.suimonoModuleLibrary.transToolsObject != null)
			{
				this.suimonoModuleLibrary.transToolsObject.resolution = Convert.ToInt32(this.resolutions[this.transResolution]);
				this.suimonoModuleLibrary.transToolsObject.gameObject.SetActive(this.enableTransparency);
			}
			else
			{
				this.suimonoModuleLibrary.transToolsObject = base.transform.Find("cam_SuimonoTrans").gameObject.GetComponent<cameraTools>();
			}
			if (this.suimonoModuleLibrary.causticCamObject != null)
			{
				if (this.enableCaustics)
				{
					this.suimonoModuleLibrary.causticCamObject.gameObject.SetActive(this.enableCausticsBlending);
					this.transLayer &= ~(1 << this.layerDepthNum);
					this.transLayer &= ~(1 << this.layerScreenFXNum);
					this.suimonoModuleLibrary.causticCamObject.cullingMask = this.transLayer;
					this.suimonoModuleLibrary.causticCamObject.farClipPlane = this.transRenderDistance * 1.2f;
				}
				if (this.suimonoModuleLibrary.causticHandlerObjectTrans != null)
				{
					this.suimonoModuleLibrary.causticHandlerObjectTrans.enabled = !this.enableCausticsBlending;
				}
			}
			else
			{
				this.suimonoModuleLibrary.causticCamObject = base.transform.Find("cam_SuimonoCaustic").gameObject.GetComponent<Camera>();
			}
			if (this.suimonoModuleLibrary.causticToolsObject != null)
			{
				this.suimonoModuleLibrary.causticToolsObject.resolution = Convert.ToInt32(this.resolutions[this.transResolution]);
			}
			else
			{
				this.suimonoModuleLibrary.causticToolsObject = base.transform.Find("cam_SuimonoCaustic").gameObject.GetComponent<cameraTools>();
			}
			Shader.SetGlobalFloat("_enableTransparency", this.useEnableTransparency ? 1f : 0f);
			this.enCaustic = 0f;
			if (this.enableCaustics)
			{
				this.enCaustic = 1f;
			}
			Shader.SetGlobalFloat("_suimono_enableCaustic", this.enCaustic);
			this.causticLayer = (this.causticLayer &= ~(1 << this.layerWaterNum << this.layerDepthNum << this.layerScreenFXNum));
			this.setEdge = 1f;
			if (!this.enableAdvancedEdge)
			{
				this.setEdge = 0f;
			}
			Shader.SetGlobalFloat("_suimono_advancedEdge", this.setEdge);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0015F7C0 File Offset: 0x0015D9C0
		private void FixedUpdate()
		{
			if (this.autoSetLayers)
			{
				this.lx = 0;
				while (this.lx < 20)
				{
					Physics.IgnoreLayerCollision(this.lx, this.layerWaterNum);
					this.lx++;
				}
			}
			this.Suimono_CheckCamera();
			if (this.suimonoModuleLibrary.causticObject != null)
			{
				if (Application.isPlaying)
				{
					this.suimonoModuleLibrary.causticObject.enableCaustics = this.enableCaustics;
				}
				else
				{
					this.suimonoModuleLibrary.causticObject.enableCaustics = false;
				}
				if (this.setLight != null)
				{
					this.suimonoModuleLibrary.causticObject.sceneLightObject = this.setLight;
				}
			}
			this.PlayUnderwaterSound();
			if (this.setCamera != null)
			{
				this.isForward = 0f;
				if (this.setCameraComponent.actualRenderingPath == RenderingPath.Forward)
				{
					this.isForward = 1f;
				}
				Shader.SetGlobalFloat("_isForward", this.isForward);
			}
			if (this.enableAdvancedDistort)
			{
				this.isAdvDist = 1f;
				this.suimonoModuleLibrary.wakeObject.SetActive(true);
				this.suimonoModuleLibrary.normalsObject.SetActive(true);
			}
			else
			{
				this.isAdvDist = 0f;
				this.suimonoModuleLibrary.wakeObject.SetActive(false);
				this.suimonoModuleLibrary.normalsObject.SetActive(false);
			}
			Shader.SetGlobalFloat("_suimono_advancedDistort", this.isAdvDist);
			if (this.setCameraComponent != null)
			{
				if (this.suimonoObject != null)
				{
					this.setCameraComponent.backgroundColor = this.suimonoObject.underwaterColor;
				}
				Shader.SetGlobalColor("_cameraBGColor", this.setCameraComponent.backgroundColor);
			}
			if (this.setCameraComponent != null)
			{
				if (this.setCameraComponent.actualRenderingPath == RenderingPath.DeferredShading)
				{
					this.setCameraComponent.depthTextureMode = DepthTextureMode.Depth;
				}
				else if (this.setCameraComponent.actualRenderingPath == RenderingPath.DeferredLighting)
				{
					this.setCameraComponent.depthTextureMode = DepthTextureMode.Depth;
				}
				else
				{
					this.setCameraComponent.depthTextureMode = DepthTextureMode.DepthNormals;
				}
				this.setCameraComponent.cullingMask = (this.setCameraComponent.cullingMask | 1 << this.layerWaterNum);
				this.setCameraComponent.cullingMask = (this.setCameraComponent.cullingMask & ~(1 << this.layerDepthNum) & ~(1 << this.layerScreenFXNum));
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00016CAD File Offset: 0x00014EAD
		private void OnDisable()
		{
			base.CancelInvoke("StoreSurfaceHeight");
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00016CBA File Offset: 0x00014EBA
		private void OnEnable()
		{
			base.InvokeRepeating("StoreSurfaceHeight", 0.01f, 0.1f);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0015FA20 File Offset: 0x0015DC20
		private void StoreSurfaceHeight()
		{
			if (base.enabled && this.setCamera != null)
			{
				this.heightValues = this.SuimonoGetHeightAll(this.setCamera.transform.position);
				this.currentSurfaceLevel = this.heightValues[1];
				this.currentObjectDepth = this.heightValues[3];
				this.currentObjectIsOver = this.heightValues[4];
				this.currentTransitionDepth = this.heightValues[9];
				this.objectEnableUnderwaterFX = this.heightValues[10];
				this.checkUnderwaterEffects();
				this.checkWaterTransition();
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0015FAB4 File Offset: 0x0015DCB4
		private void PlayUnderwaterSound()
		{
			if (Application.isPlaying && this.underSoundObject != null && this.setTrack != null && this.underSoundComponent != null)
			{
				this.underSoundObject.transform.position = this.setTrack.position;
				if (this.currentTransitionDepth > 0f)
				{
					if (!this.playSoundBelow || !this.playSounds)
					{
						this.underSoundComponent.Stop();
						return;
					}
					this.underSoundComponent.clip = this.suimonoModuleLibrary.sndparentobj.underwaterSound;
					this.underSoundComponent.volume = this.maxVolume;
					this.underSoundComponent.loop = true;
					if (!this.underSoundComponent.isPlaying)
					{
						this.underSoundComponent.Play();
						return;
					}
				}
				else if (this.suimonoModuleLibrary.sndparentobj.underwaterSound != null)
				{
					if (this.playSoundAbove && this.playSounds)
					{
						this.underSoundComponent.clip = this.suimonoModuleLibrary.sndparentobj.abovewaterSound;
						this.underSoundComponent.volume = 0.45f * this.maxVolume;
						this.underSoundComponent.loop = true;
						if (!this.underSoundComponent.isPlaying)
						{
							this.underSoundComponent.Play();
							return;
						}
					}
					else
					{
						this.underSoundComponent.Stop();
					}
				}
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0015FC20 File Offset: 0x0015DE20
		public void AddFX(int fxSystem, Vector3 effectPos, int addRate, float addSize, float addRot, float addARot, Vector3 addVeloc, Color addCol)
		{
			if (this.suimonoModuleLibrary.fxObject != null)
			{
				this.fx = fxSystem;
				if (this.suimonoModuleLibrary.fxObject.fxParticles[this.fx] != null)
				{
					this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].Emit(addRate);
					if (this.setParticles != null)
					{
						this.setParticles = null;
					}
					this.setParticles = new ParticleSystem.Particle[this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].particleCount];
					this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].GetParticles(this.setParticles);
					if ((float)this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].particleCount > 0f)
					{
						this.px = this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].particleCount - addRate;
						while (this.px < this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].particleCount)
						{
							this.setParticles[this.px].position = new Vector3(effectPos.x, effectPos.y, effectPos.z);
							this.setParticles[this.px].startSize = addSize;
							this.setParticles[this.px].rotation = addRot;
							this.setParticles[this.px].angularVelocity = addARot;
							this.setParticles[this.px].velocity = new Vector3(addVeloc.x, addVeloc.y, addVeloc.z);
							ParticleSystem.Particle[] array = this.setParticles;
							int num = this.px;
							array[num].startColor = array[num].startColor * addCol;
							this.px++;
						}
						this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].SetParticles(this.setParticles, this.setParticles.Length);
						this.suimonoModuleLibrary.fxObject.fxParticles[this.fx].Play();
					}
				}
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0015FE80 File Offset: 0x0015E080
		public void AddSoundFX(AudioClip sndClip, Vector3 soundPos, Vector3 sndVelocity)
		{
			if (this.enableInteraction)
			{
				this.setpitch = 1f;
				this.setvolume = 1f;
				if (this.playSounds && this.suimonoModuleLibrary.sndparentobj.defaultSplashSound.Length >= 1)
				{
					this.setstep = this.suimonoModuleLibrary.sndparentobj.defaultSplashSound[this.modRand.Next(0, this.defaultSplashSound.Length - 1)];
					this.setpitch = sndVelocity.y;
					this.setvolume = sndVelocity.z;
					this.setvolume = Mathf.Lerp(0f, 1f, this.setvolume) * this.maxVolume;
					if (this.currentObjectDepth > 0f)
					{
						this.setpitch *= 0.25f;
						this.setvolume *= 0.5f;
					}
					this.useSoundAudioComponent = this.sndComponents[this.currentSound];
					this.useSoundAudioComponent.clip = sndClip;
					if (!this.useSoundAudioComponent.isPlaying)
					{
						this.useSoundAudioComponent.transform.localPosition = soundPos;
						this.useSoundAudioComponent.volume = this.setvolume;
						this.useSoundAudioComponent.pitch = this.setpitch;
						this.useSoundAudioComponent.minDistance = 4f;
						this.useSoundAudioComponent.maxDistance = 20f;
						this.useSoundAudioComponent.clip = this.setstep;
						this.useSoundAudioComponent.loop = false;
						this.useSoundAudioComponent.Play();
					}
					this.currentSound++;
					if (this.currentSound >= this.maxSounds - 1)
					{
						this.currentSound = 0;
					}
				}
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00160038 File Offset: 0x0015E238
		public void AddSound(string sndMode, Vector3 soundPos, Vector3 sndVelocity)
		{
			if (this.enableInteraction)
			{
				this.setpitch = 1f;
				this.setvolume = 1f;
				if (this.playSounds && this.suimonoModuleLibrary.sndparentobj.defaultSplashSound.Length >= 1)
				{
					this.setstep = this.suimonoModuleLibrary.sndparentobj.defaultSplashSound[this.modRand.Next(0, this.suimonoModuleLibrary.sndparentobj.defaultSplashSound.Length - 1)];
					this.setpitch = sndVelocity.y;
					this.setvolume = sndVelocity.z;
					this.setvolume = Mathf.Lerp(0f, 10f, this.setvolume);
					if (this.currentObjectDepth > 0f)
					{
						this.setpitch *= 0.25f;
						this.setvolume *= 0.5f;
					}
					this.useSoundAudioComponent = this.sndComponents[this.currentSound];
					if (!this.useSoundAudioComponent.isPlaying)
					{
						this.useSoundAudioComponent.transform.localPosition = soundPos;
						this.useSoundAudioComponent.volume = this.setvolume;
						this.useSoundAudioComponent.pitch = this.setpitch;
						this.useSoundAudioComponent.minDistance = 4f;
						this.useSoundAudioComponent.maxDistance = 20f;
						this.useSoundAudioComponent.clip = this.setstep;
						this.useSoundAudioComponent.loop = false;
						this.useSoundAudioComponent.Play();
					}
					this.currentSound++;
					if (this.currentSound >= this.maxSounds - 1)
					{
						this.currentSound = 0;
					}
				}
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x001601E4 File Offset: 0x0015E3E4
		private void checkUnderwaterEffects()
		{
			if (Application.isPlaying)
			{
				if (this.currentTransitionDepth > this.underwaterThreshold)
				{
					if (this.suimonoObject != null && this.enableUnderwaterFX && this.suimonoObject.enableUnderwater && this.currentObjectIsOver == 1f)
					{
						this.isUnderwater = true;
						Shader.SetGlobalFloat("_Suimono_IsUnderwater", 1f);
						if (this.suimonoObject != null)
						{
							this.suimonoObject.useShader = this.suimonoObject.shader_Under;
						}
						if (this.suimonoModuleLibrary.causticHandlerObject != null)
						{
							this.suimonoModuleLibrary.causticHandlerObjectTrans.isUnderwater = true;
							this.suimonoModuleLibrary.causticHandlerObject.isUnderwater = true;
							return;
						}
					}
				}
				else
				{
					this.isUnderwater = false;
					Shader.SetGlobalFloat("_Suimono_IsUnderwater", 0f);
					if (this.suimonoObject != null)
					{
						this.suimonoObject.useShader = this.suimonoObject.shader_Surface;
					}
					if (this.suimonoModuleLibrary.causticHandlerObject != null)
					{
						this.suimonoModuleLibrary.causticHandlerObjectTrans.isUnderwater = false;
						this.suimonoModuleLibrary.causticHandlerObject.isUnderwater = false;
					}
				}
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0016032C File Offset: 0x0015E52C
		private void checkWaterTransition()
		{
			if (Application.isPlaying)
			{
				this.doTransitionTimer += this._deltaTime;
				if (this.currentTransitionDepth > this.underwaterThreshold && this.currentObjectIsOver == 1f)
				{
					SuimonoModule.doWaterTransition = true;
					if (this.suimonoObject != null && this.setCamera != null)
					{
						if (this.enableUnderwaterFX && this.suimonoObject.enableUnderwater && this.objectEnableUnderwaterFX == 1f)
						{
							if (this.suimonoObject.enableUnderDebris)
							{
								this.suimonoModuleLibrary.underwaterDebrisTransform.position = this.setCamera.transform.position;
								this.suimonoModuleLibrary.underwaterDebrisTransform.rotation = this.setCamera.transform.rotation;
								this.suimonoModuleLibrary.underwaterDebrisTransform.Translate(Vector3.forward * 5f);
								this.suimonoModuleLibrary.underwaterDebrisRendererComponent.enabled = true;
								this.debrisEmission = this.suimonoModuleLibrary.underwaterDebris.emission;
								this.debrisEmission.enabled = this.isUnderwater;
							}
							else if (this.suimonoModuleLibrary.underwaterDebris != null)
							{
								this.suimonoModuleLibrary.underwaterDebrisRendererComponent.enabled = false;
							}
							this.setUnderBright = this.underLightAmt;
							this.setUnderBright *= 0.5f;
							this.useLight = 1f;
							this.useLightCol = new Color(1f, 1f, 1f, 1f);
							this.useRefract = 1f;
							if (this.setLight != null)
							{
								this.useLight = this.setLight.intensity;
								this.useLightCol = this.setLight.color;
							}
							if (!this.enableRefraction)
							{
								this.useRefract = 0f;
							}
							if (this.underwaterObject == null && this.setCamera.gameObject.GetComponent<Suimono_UnderwaterFog>() != null)
							{
								this.underwaterObject = this.setCamera.gameObject.GetComponent<Suimono_UnderwaterFog>();
							}
							if (this.underwaterObject != null)
							{
								this.underwaterObject.lightFactor = this.suimonoObject.underLightFactor * this.useLight;
								this.underwaterObject.refractAmt = this.suimonoObject.underRefractionAmount * this.useRefract;
								this.underwaterObject.refractScale = this.suimonoObject.underRefractionScale;
								this.underwaterObject.refractSpd = this.suimonoObject.underRefractionSpeed * this.useRefract;
								this.underwaterObject.fogEnd = this.suimonoObject.underwaterFogDist;
								this.underwaterObject.fogStart = this.suimonoObject.underwaterFogSpread;
								this.underwaterObject.blurSpread = this.suimonoObject.underBlurAmount;
								this.underwaterObject.underwaterColor = this.suimonoObject.underwaterColor;
								this.underwaterObject.darkRange = this.suimonoObject.underDarkRange;
								this.underwaterObject.transitionStrength = this.transitionStrength;
								Shader.SetGlobalColor("_suimono_lightColor", this.useLightCol);
								this.underwaterObject.doTransition = false;
								if (this.suimonoModuleLibrary.causticObject != null && Application.isPlaying && this.suimonoModuleLibrary.causticObject != null)
								{
									this.suimonoModuleLibrary.causticObject.heightFac = this.underwaterObject.hFac * 2f;
								}
							}
						}
						else if (this.suimonoModuleLibrary.underwaterDebris != null)
						{
							this.suimonoModuleLibrary.underwaterDebrisRendererComponent.enabled = false;
						}
					}
					if (this.underwaterObject != null)
					{
						this.underwaterObject.cancelTransition = true;
					}
				}
				else
				{
					if (this.suimonoModuleLibrary.underwaterDebris != null)
					{
						this.suimonoModuleLibrary.underwaterDebrisRendererComponent.enabled = false;
					}
					if (this.enableTransition)
					{
						if (SuimonoModule.doWaterTransition && this.setCamera != null)
						{
							this.doTransitionTimer = 0f;
							if (this.underwaterObject != null)
							{
								this.underwaterObject.doTransition = true;
							}
							SuimonoModule.doWaterTransition = false;
						}
						else
						{
							this.underTrans = 1f;
						}
					}
				}
				if (!this.enableUnderwaterFX && this.suimonoModuleLibrary.underwaterDebrisRendererComponent != null)
				{
					this.suimonoModuleLibrary.underwaterDebrisRendererComponent.enabled = false;
				}
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x001607BC File Offset: 0x0015E9BC
		private void Suimono_CheckCamera()
		{
			if (this.cameraTypeIndex == 0)
			{
				if (Camera.main != null)
				{
					this.mainCamera = Camera.main.transform;
				}
				this.manualCamera = null;
			}
			if (this.cameraTypeIndex == 1)
			{
				if (this.manualCamera != null)
				{
					this.mainCamera = this.manualCamera;
				}
				else if (Camera.main != null)
				{
					this.mainCamera = Camera.main.transform;
				}
			}
			if (this.setCamera != this.mainCamera)
			{
				this.setCamera = this.mainCamera;
				this.setCameraComponent = this.setCamera.gameObject.GetComponent<Camera>();
				this.underwaterObject = this.setCamera.gameObject.GetComponent<Suimono_UnderwaterFog>();
			}
			if (this.setCameraComponent == null && this.setCamera != null)
			{
				this.setCameraComponent = this.setCamera.gameObject.GetComponent<Camera>();
			}
			if (this.setCamera != null && this.setCameraComponent != null && this.setCameraComponent.transform != this.setCamera)
			{
				this.setCameraComponent = this.setCamera.gameObject.GetComponent<Camera>();
				this.underwaterObject = this.setCamera.gameObject.GetComponent<Suimono_UnderwaterFog>();
			}
			if (this.setTrack == null && this.setCamera != null)
			{
				this.setTrack = this.setCamera.transform;
			}
			this.InstallCameraEffect();
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00160948 File Offset: 0x0015EB48
		public Vector2 SuimonoConvertAngleToDegrees(float convertAngle)
		{
			this.flow_dir = new Vector3(0f, 0f, 0f);
			this.tempAngle = new Vector3(0f, 0f, 0f);
			if (convertAngle <= 180f)
			{
				this.tempAngle = Vector3.Slerp(Vector3.forward, -Vector3.forward, convertAngle / 180f);
				this.flow_dir = new Vector2(this.tempAngle.x, this.tempAngle.z);
			}
			if (convertAngle > 180f)
			{
				this.tempAngle = Vector3.Slerp(-Vector3.forward, Vector3.forward, (convertAngle - 180f) / 180f);
				this.flow_dir = new Vector2(-this.tempAngle.x, this.tempAngle.z);
			}
			return this.flow_dir;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00160948 File Offset: 0x0015EB48
		public Vector2 SuimonoConvertAngleToVector(float convertAngle)
		{
			this.flow_dir = new Vector3(0f, 0f, 0f);
			this.tempAngle = new Vector3(0f, 0f, 0f);
			if (convertAngle <= 180f)
			{
				this.tempAngle = Vector3.Slerp(Vector3.forward, -Vector3.forward, convertAngle / 180f);
				this.flow_dir = new Vector2(this.tempAngle.x, this.tempAngle.z);
			}
			if (convertAngle > 180f)
			{
				this.tempAngle = Vector3.Slerp(-Vector3.forward, Vector3.forward, (convertAngle - 180f) / 180f);
				this.flow_dir = new Vector2(-this.tempAngle.x, this.tempAngle.z);
			}
			return this.flow_dir;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00160A30 File Offset: 0x0015EC30
		public float SuimonoGetHeight(Vector3 testObject, string returnMode)
		{
			this.CalculateHeights(testObject);
			this.returnValue = 0f;
			if (returnMode == "height")
			{
				this.returnValue = this.getheight;
			}
			if (returnMode == "surfaceLevel")
			{
				this.returnValue = this.surfaceLevel + this.getheight;
			}
			if (returnMode == "baseLevel")
			{
				this.returnValue = this.surfaceLevel;
			}
			if (returnMode == "object depth")
			{
				this.returnValue = this.getheight - testObject.y;
			}
			if (returnMode == "isOverWater" && this.isOverWater)
			{
				this.returnValue = 1f;
			}
			if (returnMode == "isOverWater" && !this.isOverWater)
			{
				this.returnValue = 0f;
			}
			if (returnMode == "isAtSurface" && this.surfaceLevel + this.getheight - testObject.y < 0.25f && this.surfaceLevel + this.getheight - testObject.y > -0.25f)
			{
				this.returnValue = 1f;
			}
			if (this.suimonoObject != null)
			{
				if (returnMode == "direction")
				{
					this.returnValue = this.suimonoObject.flowDirection;
				}
				if (returnMode == "speed")
				{
					this.returnValue = this.suimonoObject.flowSpeed;
				}
				if (returnMode == "wave height")
				{
					this.h1 = 0f;
					this.returnValue = this.getheight / this.h1;
				}
			}
			if (returnMode == "transitionDepth")
			{
				this.returnValue = this.surfaceLevel + this.getheight - (testObject.y - this.transition_offset * this.underTrans);
			}
			if (returnMode == "underwaterEnabled")
			{
				this.enabledUFX = 1f;
				if (!this.suimonoObject.enableUnderwater)
				{
					this.enabledUFX = 0f;
				}
				this.returnValue = this.enabledUFX;
			}
			if (returnMode == "causticsEnabled")
			{
				this.enabledCaustics = 1f;
				if (!this.suimonoObject.enableCausticFX)
				{
					this.enabledCaustics = 0f;
				}
				this.returnValue = this.enabledCaustics;
			}
			return this.returnValue;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00160C78 File Offset: 0x0015EE78
		public float[] SuimonoGetHeightAll(Vector3 testObject)
		{
			this.CalculateHeights(testObject);
			this.returnValueAll = new float[12];
			this.returnValueAll[0] = this.getheight;
			this.returnValueAll[1] = this.getheight;
			this.returnValueAll[2] = this.surfaceLevel;
			this.returnValueAll[3] = this.getheight - testObject.y;
			this.returnValue = 1f;
			if (!this.isOverWater)
			{
				this.returnValue = 0f;
			}
			this.returnValueAll[4] = this.returnValue;
			this.returnValue = 0f;
			if (this.getheight - testObject.y < 0.25f && this.getheight - testObject.y > -0.25f)
			{
				this.returnValue = 1f;
			}
			this.returnValueAll[5] = this.returnValue;
			if (this.suimonoObject != null)
			{
				this.setDegrees = this.suimonoObject.flowDirection + this.suimonoObject.transform.eulerAngles.y;
				if (this.setDegrees < 0f)
				{
					this.setDegrees = 365f + this.setDegrees;
				}
				if (this.setDegrees > 365f)
				{
					this.setDegrees -= 365f;
				}
				if (this.suimonoObject != null)
				{
					this.returnValueAll[6] = this.setDegrees;
				}
				if (this.suimonoObject == null)
				{
					this.returnValueAll[6] = 0f;
				}
				if (this.suimonoObject != null)
				{
					this.returnValueAll[7] = this.suimonoObject.flowSpeed;
				}
				if (this.suimonoObject == null)
				{
					this.returnValueAll[7] = 0f;
				}
				if (this.suimonoObject != null)
				{
					this.h1 = this.suimonoObject.lgWaveHeight;
				}
				if (this.suimonoObject == null)
				{
					this.h1 = 0f;
				}
				this.returnValueAll[8] = this.getheight / this.h1;
			}
			this.returnValueAll[9] = this.getheight - (testObject.y - this.transition_offset * this.underTrans);
			this.enabledUFX = 1f;
			if (this.suimonoObject != null)
			{
				if (!this.suimonoObject.enableUnderwater)
				{
					this.enabledUFX = 0f;
				}
				this.returnValueAll[10] = this.enabledUFX;
			}
			this.enabledCaustics = 1f;
			if (this.suimonoObject != null)
			{
				if (!this.suimonoObject.enableCausticFX)
				{
					this.enabledCaustics = 0f;
				}
				this.returnValueAll[11] = this.enabledCaustics;
			}
			return this.returnValueAll;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00016CD1 File Offset: 0x00014ED1
		public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
		{
			this.dir = point - pivot;
			this.dir = Quaternion.Euler(angles * -1f) * this.dir;
			point = this.dir + pivot;
			return point;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00160F30 File Offset: 0x0015F130
		public Color DecodeHeightPixels(float texPosx, float texPosy, int texNum)
		{
			if (texNum == 0)
			{
				this.useDecodeTex = this.heightTex;
				this.useDecodeArray = this.pixelArray;
			}
			if (texNum == 1)
			{
				this.useDecodeTex = this.heightTexT;
				this.useDecodeArray = this.pixelArrayT;
			}
			if (texNum == 2)
			{
				this.useDecodeTex = this.heightTexR;
				this.useDecodeArray = this.pixelArrayR;
			}
			texPosx %= (float)this.useDecodeTex.width;
			texPosy %= (float)this.useDecodeTex.height;
			if (texPosx < 0f)
			{
				texPosx = (float)this.useDecodeTex.width - Mathf.Abs(texPosx);
			}
			if (texPosy < 0f)
			{
				texPosy = (float)this.useDecodeTex.height - Mathf.Abs(texPosy);
			}
			if (texPosx > (float)this.useDecodeTex.width)
			{
				texPosx -= (float)this.useDecodeTex.width;
			}
			if (texPosy > (float)this.useDecodeTex.height)
			{
				texPosy -= (float)this.useDecodeTex.height;
			}
			this.row = this.useDecodeArray.Length / this.useDecodeTex.height - Mathf.FloorToInt(texPosy);
			this.pixIndex = Mathf.FloorToInt(texPosx) + 1 + (this.useDecodeArray.Length - this.useDecodeTex.width * this.row) - 1;
			if (this.pixIndex > this.useDecodeArray.Length)
			{
				this.pixIndex -= this.useDecodeArray.Length;
			}
			if (this.pixIndex < 0)
			{
				this.pixIndex = this.useDecodeArray.Length - this.pixIndex;
			}
			this.pixCol = this.useDecodeArray[this.pixIndex];
			if (this._colorspace == ColorSpace.Linear)
			{
				this.pixCol.a = Mathf.Clamp(Mathf.Lerp(-0.035f, 0.5f, this.pixCol.a), 0f, 1f);
			}
			if (this._colorspace == ColorSpace.Gamma)
			{
				this.pixCol.a = Mathf.Clamp(Mathf.Lerp(-0.035f, 0.5f, this.pixCol.a), 0f, 1f);
			}
			return this.pixCol;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00161150 File Offset: 0x0015F350
		private void CalculateHeights(Vector3 testObject)
		{
			this.getheight = -1f;
			this.getheightC = -1f;
			this.getheightT = -1f;
			this.getheightR = 0f;
			this.isOverWater = false;
			this.surfaceLevel = -1f;
			this.layermask = 1 << this.layerWaterNum;
			this.testpos = new Vector3(testObject.x, testObject.y + 30000f, testObject.z);
			if (Physics.Raycast(this.testpos, -Vector3.up, out this.hit, 60000f, this.layermask))
			{
				this.targetSurface = this.hit.transform.gameObject;
				if (this.currentSurfaceObject != this.targetSurface || this.suimonoObject == null)
				{
					this.currentSurfaceObject = this.targetSurface;
					if (this.hit.transform.parent != null && this.hit.transform.parent.gameObject.GetComponent<SuimonoObject>() != null)
					{
						this.suimonoObject = this.hit.transform.parent.gameObject.GetComponent<SuimonoObject>();
					}
				}
				if (this.suimonoObject != null && this.enableInteraction && this.suimonoObject.enableInteraction)
				{
					if (this.suimonoObject.typeIndex == 0)
					{
						this.heightObject = this.hit.transform;
					}
					else
					{
						this.heightObject = this.hit.transform.parent;
					}
					if (this.suimonoObject != null && this.hit.collider != null)
					{
						this.isOverWater = true;
						this.surfaceLevel = this.heightObject.position.y;
						if (this.heightObject != null)
						{
							this.baseHeight = this.heightObject.position.y;
							this.baseAngle = this.heightObject.rotation.y;
							this.relativePos.x = ((this.heightObject.position.x - testObject.x) / (20f * this.heightObject.localScale.x) + 1f) * 0.5f * this.heightObject.localScale.x;
							this.relativePos.y = ((this.heightObject.position.z - testObject.z) / (20f * this.heightObject.localScale.z) + 1f) * 0.5f * this.heightObject.localScale.z;
						}
						float num = this.suimonoObject.enableCustomMesh ? this.suimonoObject.cmScaleX : 1f;
						float num2 = this.suimonoObject.enableCustomMesh ? this.suimonoObject.cmScaleY : 1f;
						this.relativePos.x = this.relativePos.x * num;
						this.relativePos.y = this.relativePos.y * num2;
						this.useLocalTime = this.suimonoObject.localTime;
						this.flow_dirC = this.SuimonoConvertAngleToVector(this.suimonoObject.flowDirection);
						this.flowSpeed0 = new Vector2(this.flow_dirC.x * this.useLocalTime, this.flow_dirC.y * this.useLocalTime);
						this.flowSpeed1 = new Vector2(this.flow_dirC.x * this.useLocalTime * 0.25f, this.flow_dirC.y * this.useLocalTime * 0.25f);
						this.flowSpeed2 = new Vector2(this.flow_dirC.x * this.useLocalTime * 0.0625f, this.flow_dirC.y * this.useLocalTime * 0.0625f);
						this.flowSpeed3 = new Vector2(this.flow_dirC.x * this.useLocalTime * 0.125f, this.flow_dirC.y * this.useLocalTime * 0.125f);
						this.tScale = 1f / this.suimonoObject.waveScale;
						this.oPos = new Vector2(0f - this.suimonoObject.savePos.x, 0f - this.suimonoObject.savePos.y);
						if (this.heightTex != null)
						{
							this.texCoord.x = (this.relativePos.x * this.tScale + this.flowSpeed0.x + this.oPos.x) * (float)this.heightTex.width;
							this.texCoord.z = (this.relativePos.y * this.tScale + this.flowSpeed0.y + this.oPos.y) * (float)this.heightTex.height;
							this.texCoord1.x = (this.relativePos.x * this.tScale * 0.75f - this.flowSpeed1.x + this.oPos.x * 0.75f) * (float)this.heightTex.width;
							this.texCoord1.z = (this.relativePos.y * this.tScale * 0.75f - this.flowSpeed1.y + this.oPos.y * 0.75f) * (float)this.heightTex.height;
							this.texCoordT.x = (this.relativePos.x * this.tScale + this.flowSpeed0.x + this.oPos.x) * (float)this.heightTexT.width;
							this.texCoordT.z = (this.relativePos.y * this.tScale + this.flowSpeed0.y + this.oPos.y) * (float)this.heightTexT.height;
							this.texCoordT1.x = (this.relativePos.x * this.tScale * 0.5f - this.flowSpeed1.x + this.oPos.x * 0.5f) * (float)this.heightTexT.width;
							this.texCoordT1.z = (this.relativePos.y * this.tScale * 0.5f - this.flowSpeed1.y + this.oPos.y * 0.5f) * (float)this.heightTexT.height;
							this.texCoordR.x = (this.relativePos.x * this.suimonoObject.lgWaveScale * this.tScale + this.flowSpeed2.x + this.oPos.x * this.suimonoObject.lgWaveScale) * (float)this.heightTexR.width;
							this.texCoordR.z = (this.relativePos.y * this.suimonoObject.lgWaveScale * this.tScale + this.flowSpeed2.y + this.oPos.y * this.suimonoObject.lgWaveScale) * (float)this.heightTexR.height;
							this.texCoordR1.x = (this.relativePos.x * this.suimonoObject.lgWaveScale * this.tScale + this.flowSpeed3.x + this.oPos.x * this.suimonoObject.lgWaveScale) * (float)this.heightTexR.width;
							this.texCoordR1.z = (this.relativePos.y * this.suimonoObject.lgWaveScale * this.tScale + this.flowSpeed3.y + this.oPos.y * this.suimonoObject.lgWaveScale) * (float)this.heightTexR.height;
							if (this.baseAngle != 0f)
							{
								this.pivotPoint = new Vector3((float)this.heightTex.width * this.heightObject.localScale.x * this.tScale * 0.5f + this.flowSpeed0.x * (float)this.heightTex.width, 0f, (float)this.heightTex.height * this.heightObject.localScale.z * this.tScale * 0.5f + this.flowSpeed0.y * (float)this.heightTex.height);
								this.texCoord = this.RotatePointAroundPivot(this.texCoord, this.pivotPoint, this.heightObject.eulerAngles);
								this.pivotPoint = new Vector3((float)this.heightTex.width * this.heightObject.localScale.x * this.tScale * 0.5f * 0.75f - this.flowSpeed1.x * (float)this.heightTex.width, 0f, (float)this.heightTex.height * this.heightObject.localScale.z * this.tScale * 0.5f * 0.75f - this.flowSpeed1.y * (float)this.heightTex.height);
								this.texCoord1 = this.RotatePointAroundPivot(this.texCoord1, this.pivotPoint, this.heightObject.eulerAngles);
								this.pivotPoint = new Vector3((float)this.heightTexT.width * this.heightObject.localScale.x * this.tScale * 0.5f + this.flowSpeed0.x * (float)this.heightTexT.width, 0f, (float)this.heightTexT.height * this.heightObject.localScale.z * this.tScale * 0.5f + this.flowSpeed0.y * (float)this.heightTexT.height);
								this.texCoordT = this.RotatePointAroundPivot(this.texCoordT, this.pivotPoint, this.heightObject.eulerAngles);
								this.pivotPoint = new Vector3((float)this.heightTexT.width * this.heightObject.localScale.x * this.tScale * 0.5f * 0.5f - this.flowSpeed1.x * (float)this.heightTexT.width, 0f, (float)this.heightTexT.height * this.heightObject.localScale.z * this.tScale * 0.5f * 0.5f - this.flowSpeed1.y * (float)this.heightTexT.height);
								this.texCoordT1 = this.RotatePointAroundPivot(this.texCoordT1, this.pivotPoint, this.heightObject.eulerAngles);
								this.pivotPoint = new Vector3((float)this.heightTexR.width * this.heightObject.localScale.x * this.suimonoObject.lgWaveScale * this.tScale * 0.5f + this.flowSpeed2.x * (float)this.heightTexR.width, 0f, (float)this.heightTexR.height * this.heightObject.localScale.z * this.suimonoObject.lgWaveScale * this.tScale * 0.5f + this.flowSpeed2.y * (float)this.heightTexR.height);
								this.texCoordR = this.RotatePointAroundPivot(this.texCoordR, this.pivotPoint, this.heightObject.eulerAngles);
								this.pivotPoint = new Vector3((float)this.heightTexR.width * this.heightObject.localScale.x * this.suimonoObject.lgWaveScale * this.tScale * 0.5f + this.flowSpeed3.x * (float)this.heightTexR.width, 0f, (float)this.heightTexR.height * this.heightObject.localScale.z * this.suimonoObject.lgWaveScale * this.tScale * 0.5f + this.flowSpeed3.y * (float)this.heightTexR.height);
								this.texCoordR1 = this.RotatePointAroundPivot(this.texCoordR1, this.pivotPoint, this.heightObject.eulerAngles);
							}
							this.heightVal0 = this.DecodeHeightPixels(this.texCoord.x, this.texCoord.z, 0);
							this.heightVal1 = this.DecodeHeightPixels(this.texCoord1.x, this.texCoord1.z, 0);
							this.heightValT0 = this.DecodeHeightPixels(this.texCoordT.x, this.texCoordT.z, 1);
							this.heightValT1 = this.DecodeHeightPixels(this.texCoordT1.x, this.texCoordT1.z, 1);
							this.heightValR0 = this.DecodeHeightPixels(this.texCoordR.x, this.texCoordR.z, 2);
							this.heightValR1 = this.DecodeHeightPixels(this.texCoordR1.x, this.texCoordR1.z, 2);
							this.getheightC = (this.heightVal0.a + this.heightVal1.a) * 0.8f;
							this.getheightT = (this.heightValT0.a * 0.2f + this.heightValT0.a * this.heightValT1.a * 0.8f) * this.suimonoObject.turbulenceFactor * 0.5f;
							this.getheightR = this.heightValR0.a * 4f + this.heightValR1.a * 3f;
							this.getheight = this.baseHeight + this.getheightC * this.suimonoObject.waveHeight;
							this.getheight += this.getheightT * this.suimonoObject.waveHeight;
							this.getheight += this.getheightR * this.suimonoObject.lgWaveHeight;
							this.getheight = Mathf.Lerp(this.baseHeight, this.getheight, this.suimonoObject.useHeightProjection);
						}
					}
				}
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00162038 File Offset: 0x00160238
		public void RegisterSurface(SuimonoObject surface)
		{
			if (Application.isPlaying && surface != null && this.sObjects != null)
			{
				if (this.sObjects.IndexOf(surface) > -1)
				{
					return;
				}
				this.sObjects.Add(surface);
				this.sRends.Add(surface.transform.Find("Suimono_Object").gameObject.GetComponent<Renderer>());
				this.sRendSCs.Add(surface.transform.Find("Suimono_ObjectScale").gameObject.GetComponent<Renderer>());
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x001620C4 File Offset: 0x001602C4
		public void DeregisterSurface(SuimonoObject surface)
		{
			if (Application.isPlaying && surface != null && this.sObjects != null)
			{
				int num = this.sObjects.IndexOf(surface);
				if (num < 0)
				{
					return;
				}
				this.sObjects.RemoveAt(num);
				this.sRends.RemoveAt(num);
				this.sRendSCs.RemoveAt(num);
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00162120 File Offset: 0x00160320
		private void SetCullFunction()
		{
			this.renderCount = 0;
			for (int i = 0; i < this.sObjects.Count; i++)
			{
				if (!(this.sRends[i] == null) && this.sRends[i].isVisible)
				{
					if (this.sObjects[i].typeIndex == 0)
					{
						if (this.sRendSCs[i] != null && this.sRendSCs[i].isVisible)
						{
							this.renderCount++;
						}
					}
					else
					{
						this.renderCount++;
					}
				}
			}
			if (this.renderCount > 0 || this.isUnderwater)
			{
				this.useEnableTransparency = this.enableTransparency;
			}
			if (this.renderCount <= 0 && !this.isUnderwater)
			{
				this.useEnableTransparency = false;
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00162204 File Offset: 0x00160404
		private void InstallCameraEffect()
		{
			if (this.setCameraComponent != null && this.autoSetCameraFX && !(this.setCameraComponent.gameObject.GetComponent<Suimono_UnderwaterFog>() != null) && this.enableUnderwaterFX)
			{
				this.setCameraComponent.gameObject.AddComponent<Suimono_UnderwaterFog>();
			}
		}

		// Token: 0x04002971 RID: 10609
		public string suimonoVersionNumber = "";

		// Token: 0x04002972 RID: 10610
		public float systemTime;

		// Token: 0x04002973 RID: 10611
		public bool autoSetLayers = true;

		// Token: 0x04002974 RID: 10612
		public string layerWater;

		// Token: 0x04002975 RID: 10613
		public int layerWaterNum = -1;

		// Token: 0x04002976 RID: 10614
		public string layerDepth;

		// Token: 0x04002977 RID: 10615
		public int layerDepthNum = -1;

		// Token: 0x04002978 RID: 10616
		public string layerScreenFX;

		// Token: 0x04002979 RID: 10617
		public int layerScreenFXNum = -1;

		// Token: 0x0400297A RID: 10618
		public bool layersAreSet;

		// Token: 0x0400297B RID: 10619
		public bool autoSetCameraFX = true;

		// Token: 0x0400297C RID: 10620
		public Transform manualCamera;

		// Token: 0x0400297D RID: 10621
		public Transform mainCamera;

		// Token: 0x0400297E RID: 10622
		public int cameraTypeIndex;

		// Token: 0x0400297F RID: 10623
		[NonSerialized]
		public List<string> cameraTypeOptions = new List<string>
		{
			"Auto Select Camera",
			"Manual Select Camera"
		};

		// Token: 0x04002980 RID: 10624
		public Transform setCamera;

		// Token: 0x04002981 RID: 10625
		public Transform setTrack;

		// Token: 0x04002982 RID: 10626
		public Light setLight;

		// Token: 0x04002983 RID: 10627
		public bool enableUnderwaterFX = true;

		// Token: 0x04002984 RID: 10628
		public bool enableInteraction = true;

		// Token: 0x04002985 RID: 10629
		public float objectEnableUnderwaterFX = 1f;

		// Token: 0x04002986 RID: 10630
		public bool disableMSAA;

		// Token: 0x04002987 RID: 10631
		public bool enableRefraction = true;

		// Token: 0x04002988 RID: 10632
		public bool enableReflections = true;

		// Token: 0x04002989 RID: 10633
		public bool enableDynamicReflections = true;

		// Token: 0x0400298A RID: 10634
		public bool enableCaustics = true;

		// Token: 0x0400298B RID: 10635
		public bool enableCausticsBlending;

		// Token: 0x0400298C RID: 10636
		public bool enableAdvancedEdge = true;

		// Token: 0x0400298D RID: 10637
		public bool enableAdvancedDistort = true;

		// Token: 0x0400298E RID: 10638
		public bool enableTenkoku;

		// Token: 0x0400298F RID: 10639
		public bool enableAutoAdvance = true;

		// Token: 0x04002990 RID: 10640
		public bool showPerformance;

		// Token: 0x04002991 RID: 10641
		public bool showGeneral;

		// Token: 0x04002992 RID: 10642
		public Color underwaterColor = new Color(0.58f, 0.61f, 0.61f, 0f);

		// Token: 0x04002993 RID: 10643
		public bool enableTransition = true;

		// Token: 0x04002994 RID: 10644
		public float transition_offset = 0.1f;

		// Token: 0x04002995 RID: 10645
		public GameObject fxRippleObject;

		// Token: 0x04002996 RID: 10646
		private float underLightAmt;

		// Token: 0x04002997 RID: 10647
		private GameObject targetSurface;

		// Token: 0x04002998 RID: 10648
		private float doTransitionTimer;

		// Token: 0x04002999 RID: 10649
		public bool isUnderwater;

		// Token: 0x0400299A RID: 10650
		private static bool doWaterTransition;

		// Token: 0x0400299B RID: 10651
		public bool enableTransparency = true;

		// Token: 0x0400299C RID: 10652
		private bool useEnableTransparency = true;

		// Token: 0x0400299D RID: 10653
		public int transResolution = 3;

		// Token: 0x0400299E RID: 10654
		public int transLayer;

		// Token: 0x0400299F RID: 10655
		public LayerMask transLayerMask;

		// Token: 0x040029A0 RID: 10656
		public int causticLayer;

		// Token: 0x040029A1 RID: 10657
		public LayerMask causticLayerMask;

		// Token: 0x040029A2 RID: 10658
		[NonSerialized]
		public List<string> suiLayerMasks;

		// Token: 0x040029A3 RID: 10659
		[NonSerialized]
		public List<string> resOptions = new List<string>
		{
			"4096",
			"2048",
			"1024",
			"512",
			"256",
			"128",
			"64",
			"32",
			"16",
			"8"
		};

		// Token: 0x040029A4 RID: 10660
		[NonSerialized]
		public List<int> resolutions = new List<int>
		{
			4096,
			2048,
			1024,
			512,
			256,
			128,
			64,
			32,
			16,
			8
		};

		// Token: 0x040029A5 RID: 10661
		public float transRenderDistance = 100f;

		// Token: 0x040029A6 RID: 10662
		public bool playSounds = true;

		// Token: 0x040029A7 RID: 10663
		public bool playSoundBelow = true;

		// Token: 0x040029A8 RID: 10664
		public bool playSoundAbove = true;

		// Token: 0x040029A9 RID: 10665
		public float maxVolume = 1f;

		// Token: 0x040029AA RID: 10666
		public int maxSounds = 10;

		// Token: 0x040029AB RID: 10667
		public AudioClip[] defaultSplashSound;

		// Token: 0x040029AC RID: 10668
		private float setvolume = 0.65f;

		// Token: 0x040029AD RID: 10669
		private GameObject underSoundObject;

		// Token: 0x040029AE RID: 10670
		private AudioSource underSoundComponent;

		// Token: 0x040029AF RID: 10671
		private AudioSource[] sndComponents;

		// Token: 0x040029B0 RID: 10672
		private int currentSound;

		// Token: 0x040029B1 RID: 10673
		public float transitionStrength = 1f;

		// Token: 0x040029B2 RID: 10674
		public float currentObjectIsOver;

		// Token: 0x040029B3 RID: 10675
		public float currentObjectDepth;

		// Token: 0x040029B4 RID: 10676
		public float currentTransitionDepth;

		// Token: 0x040029B5 RID: 10677
		public float currentSurfaceLevel;

		// Token: 0x040029B6 RID: 10678
		public float underwaterThreshold = 0.1f;

		// Token: 0x040029B7 RID: 10679
		public SuimonoObject suimonoObject;

		// Token: 0x040029B8 RID: 10680
		private ParticleSystem.Particle[] effectBubbles;

		// Token: 0x040029B9 RID: 10681
		public SuimonoModuleLib suimonoModuleLibrary;

		// Token: 0x040029BA RID: 10682
		public Camera setCameraComponent;

		// Token: 0x040029BB RID: 10683
		private float underTrans;

		// Token: 0x040029BC RID: 10684
		public float useTenkoku;

		// Token: 0x040029BD RID: 10685
		public float tenkokuWindDir;

		// Token: 0x040029BE RID: 10686
		public float tenkokuWindAmt;

		// Token: 0x040029BF RID: 10687
		public bool tenkokuUseWind = true;

		// Token: 0x040029C0 RID: 10688
		private GameObject tenObject;

		// Token: 0x040029C1 RID: 10689
		public bool showTenkoku = true;

		// Token: 0x040029C2 RID: 10690
		public bool tenkokuUseReflect = true;

		// Token: 0x040029C3 RID: 10691
		private WindZone tenkokuWindModule;

		// Token: 0x040029C4 RID: 10692
		private int lx;

		// Token: 0x040029C5 RID: 10693
		private int fx;

		// Token: 0x040029C6 RID: 10694
		private int px;

		// Token: 0x040029C7 RID: 10695
		private ParticleSystem.Particle[] setParticles;

		// Token: 0x040029C8 RID: 10696
		private AudioClip setstep;

		// Token: 0x040029C9 RID: 10697
		private float setpitch;

		// Token: 0x040029CA RID: 10698
		private AudioSource useSoundAudioComponent;

		// Token: 0x040029CB RID: 10699
		private float useRefract;

		// Token: 0x040029CC RID: 10700
		private float useLight = 1f;

		// Token: 0x040029CD RID: 10701
		private Color useLightCol;

		// Token: 0x040029CE RID: 10702
		private Vector2 flow_dir;

		// Token: 0x040029CF RID: 10703
		private Vector3 tempAngle;

		// Token: 0x040029D0 RID: 10704
		private float getheight;

		// Token: 0x040029D1 RID: 10705
		private float getheightC;

		// Token: 0x040029D2 RID: 10706
		private float getheightT;

		// Token: 0x040029D3 RID: 10707
		private float getheightR;

		// Token: 0x040029D4 RID: 10708
		private bool isOverWater;

		// Token: 0x040029D5 RID: 10709
		private float surfaceLevel;

		// Token: 0x040029D6 RID: 10710
		private int layer;

		// Token: 0x040029D7 RID: 10711
		private int layermask;

		// Token: 0x040029D8 RID: 10712
		private Vector3 testpos;

		// Token: 0x040029D9 RID: 10713
		private int i;

		// Token: 0x040029DA RID: 10714
		private RaycastHit hit;

		// Token: 0x040029DB RID: 10715
		private Vector2 pixelUV;

		// Token: 0x040029DC RID: 10716
		private float returnValue;

		// Token: 0x040029DD RID: 10717
		private float[] returnValueAll;

		// Token: 0x040029DE RID: 10718
		private float h1;

		// Token: 0x040029DF RID: 10719
		private float setDegrees;

		// Token: 0x040029E0 RID: 10720
		private float enabledUFX = 1f;

		// Token: 0x040029E1 RID: 10721
		private float enabledCaustics = 1f;

		// Token: 0x040029E2 RID: 10722
		private float setUnderBright;

		// Token: 0x040029E3 RID: 10723
		private float enCaustic;

		// Token: 0x040029E4 RID: 10724
		private float setEdge = 1f;

		// Token: 0x040029E5 RID: 10725
		private Suimono_UnderwaterFog underwaterObject;

		// Token: 0x040029E6 RID: 10726
		private GameObject currentSurfaceObject;

		// Token: 0x040029E7 RID: 10727
		public float[] heightValues;

		// Token: 0x040029E8 RID: 10728
		public float isForward;

		// Token: 0x040029E9 RID: 10729
		public float isAdvDist;

		// Token: 0x040029EA RID: 10730
		public float waveScale = 1f;

		// Token: 0x040029EB RID: 10731
		public float flowSpeed = 0.02f;

		// Token: 0x040029EC RID: 10732
		public float offset;

		// Token: 0x040029ED RID: 10733
		public Texture2D heightTex;

		// Token: 0x040029EE RID: 10734
		public Texture2D heightTexT;

		// Token: 0x040029EF RID: 10735
		public Texture2D heightTexR;

		// Token: 0x040029F0 RID: 10736
		public Transform heightObject;

		// Token: 0x040029F1 RID: 10737
		public Vector2 relativePos = new Vector2(0f, 0f);

		// Token: 0x040029F2 RID: 10738
		public Vector3 texCoord = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F3 RID: 10739
		public Vector3 texCoord1 = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F4 RID: 10740
		public Vector3 texCoordT = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F5 RID: 10741
		public Vector3 texCoordT1 = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F6 RID: 10742
		public Vector3 texCoordR = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F7 RID: 10743
		public Vector3 texCoordR1 = new Vector3(0f, 0f, 0f);

		// Token: 0x040029F8 RID: 10744
		public Color heightVal0;

		// Token: 0x040029F9 RID: 10745
		public Color heightVal1;

		// Token: 0x040029FA RID: 10746
		public Color heightValT0;

		// Token: 0x040029FB RID: 10747
		public Color heightValT1;

		// Token: 0x040029FC RID: 10748
		public Color heightValR0;

		// Token: 0x040029FD RID: 10749
		public Color heightValR1;

		// Token: 0x040029FE RID: 10750
		public float localTime;

		// Token: 0x040029FF RID: 10751
		private float baseHeight;

		// Token: 0x04002A00 RID: 10752
		private float baseAngle;

		// Token: 0x04002A01 RID: 10753
		private Color[] pixelArray;

		// Token: 0x04002A02 RID: 10754
		private Color[] pixelArrayT;

		// Token: 0x04002A03 RID: 10755
		private Color[] pixelArrayR;

		// Token: 0x04002A04 RID: 10756
		private Texture2D useDecodeTex;

		// Token: 0x04002A05 RID: 10757
		private Color[] useDecodeArray;

		// Token: 0x04002A06 RID: 10758
		public int row;

		// Token: 0x04002A07 RID: 10759
		public int pixIndex;

		// Token: 0x04002A08 RID: 10760
		public Color pixCol;

		// Token: 0x04002A09 RID: 10761
		public int t;

		// Token: 0x04002A0A RID: 10762
		public int y;

		// Token: 0x04002A0B RID: 10763
		public Vector3 dir;

		// Token: 0x04002A0C RID: 10764
		public Vector3 pivotPoint;

		// Token: 0x04002A0D RID: 10765
		public float useLocalTime;

		// Token: 0x04002A0E RID: 10766
		public Vector2 flow_dirC;

		// Token: 0x04002A0F RID: 10767
		public Vector2 flowSpeed0;

		// Token: 0x04002A10 RID: 10768
		public Vector2 flowSpeed1;

		// Token: 0x04002A11 RID: 10769
		public Vector2 flowSpeed2;

		// Token: 0x04002A12 RID: 10770
		public Vector2 flowSpeed3;

		// Token: 0x04002A13 RID: 10771
		public float tScale;

		// Token: 0x04002A14 RID: 10772
		public Vector2 oPos;

		// Token: 0x04002A15 RID: 10773
		private int renderCount;

		// Token: 0x04002A16 RID: 10774
		private int randSeed;

		// Token: 0x04002A17 RID: 10775
		private Suimono.Core.Random modRand;

		// Token: 0x04002A18 RID: 10776
		private List<SuimonoObject> sObjects;

		// Token: 0x04002A19 RID: 10777
		private List<Renderer> sRends;

		// Token: 0x04002A1A RID: 10778
		private List<Renderer> sRendSCs;

		// Token: 0x04002A1B RID: 10779
		private ParticleSystem.EmissionModule debrisEmission;

		// Token: 0x04002A1C RID: 10780
		private ColorSpace _colorspace;

		// Token: 0x04002A1D RID: 10781
		private float _deltaTime;
	}
}
