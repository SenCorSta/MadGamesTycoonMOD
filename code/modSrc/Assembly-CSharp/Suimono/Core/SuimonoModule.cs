using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class SuimonoModule : MonoBehaviour
	{
		
		private void Awake()
		{
			this.suimonoVersionNumber = "2.1.13";
			base.gameObject.name = "SUIMONO_Module";
			this.sObjects = new List<SuimonoObject>();
			this.sRends = new List<Renderer>();
			this.sRendSCs = new List<Renderer>();
		}

		
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

		
		private void InitLayers()
		{
			if (this.autoSetLayers && !this.layersAreSet)
			{
				this.layersAreSet = true;
			}
		}

		
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

		
		private void OnDisable()
		{
			base.CancelInvoke("StoreSurfaceHeight");
		}

		
		private void OnEnable()
		{
			base.InvokeRepeating("StoreSurfaceHeight", 0.01f, 0.1f);
		}

		
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

		
		public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
		{
			this.dir = point - pivot;
			this.dir = Quaternion.Euler(angles * -1f) * this.dir;
			point = this.dir + pivot;
			return point;
		}

		
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

		
		private void InstallCameraEffect()
		{
			if (this.setCameraComponent != null && this.autoSetCameraFX && !(this.setCameraComponent.gameObject.GetComponent<Suimono_UnderwaterFog>() != null) && this.enableUnderwaterFX)
			{
				this.setCameraComponent.gameObject.AddComponent<Suimono_UnderwaterFog>();
			}
		}

		
		public string suimonoVersionNumber = "";

		
		public float systemTime;

		
		public bool autoSetLayers = true;

		
		public string layerWater;

		
		public int layerWaterNum = -1;

		
		public string layerDepth;

		
		public int layerDepthNum = -1;

		
		public string layerScreenFX;

		
		public int layerScreenFXNum = -1;

		
		public bool layersAreSet;

		
		public bool autoSetCameraFX = true;

		
		public Transform manualCamera;

		
		public Transform mainCamera;

		
		public int cameraTypeIndex;

		
		[NonSerialized]
		public List<string> cameraTypeOptions = new List<string>
		{
			"Auto Select Camera",
			"Manual Select Camera"
		};

		
		public Transform setCamera;

		
		public Transform setTrack;

		
		public Light setLight;

		
		public bool enableUnderwaterFX = true;

		
		public bool enableInteraction = true;

		
		public float objectEnableUnderwaterFX = 1f;

		
		public bool disableMSAA;

		
		public bool enableRefraction = true;

		
		public bool enableReflections = true;

		
		public bool enableDynamicReflections = true;

		
		public bool enableCaustics = true;

		
		public bool enableCausticsBlending;

		
		public bool enableAdvancedEdge = true;

		
		public bool enableAdvancedDistort = true;

		
		public bool enableTenkoku;

		
		public bool enableAutoAdvance = true;

		
		public bool showPerformance;

		
		public bool showGeneral;

		
		public Color underwaterColor = new Color(0.58f, 0.61f, 0.61f, 0f);

		
		public bool enableTransition = true;

		
		public float transition_offset = 0.1f;

		
		public GameObject fxRippleObject;

		
		private float underLightAmt;

		
		private GameObject targetSurface;

		
		private float doTransitionTimer;

		
		public bool isUnderwater;

		
		private static bool doWaterTransition;

		
		public bool enableTransparency = true;

		
		private bool useEnableTransparency = true;

		
		public int transResolution = 3;

		
		public int transLayer;

		
		public LayerMask transLayerMask;

		
		public int causticLayer;

		
		public LayerMask causticLayerMask;

		
		[NonSerialized]
		public List<string> suiLayerMasks;

		
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

		
		public float transRenderDistance = 100f;

		
		public bool playSounds = true;

		
		public bool playSoundBelow = true;

		
		public bool playSoundAbove = true;

		
		public float maxVolume = 1f;

		
		public int maxSounds = 10;

		
		public AudioClip[] defaultSplashSound;

		
		private float setvolume = 0.65f;

		
		private GameObject underSoundObject;

		
		private AudioSource underSoundComponent;

		
		private AudioSource[] sndComponents;

		
		private int currentSound;

		
		public float transitionStrength = 1f;

		
		public float currentObjectIsOver;

		
		public float currentObjectDepth;

		
		public float currentTransitionDepth;

		
		public float currentSurfaceLevel;

		
		public float underwaterThreshold = 0.1f;

		
		public SuimonoObject suimonoObject;

		
		private ParticleSystem.Particle[] effectBubbles;

		
		public SuimonoModuleLib suimonoModuleLibrary;

		
		public Camera setCameraComponent;

		
		private float underTrans;

		
		public float useTenkoku;

		
		public float tenkokuWindDir;

		
		public float tenkokuWindAmt;

		
		public bool tenkokuUseWind = true;

		
		private GameObject tenObject;

		
		public bool showTenkoku = true;

		
		public bool tenkokuUseReflect = true;

		
		private WindZone tenkokuWindModule;

		
		private int lx;

		
		private int fx;

		
		private int px;

		
		private ParticleSystem.Particle[] setParticles;

		
		private AudioClip setstep;

		
		private float setpitch;

		
		private AudioSource useSoundAudioComponent;

		
		private float useRefract;

		
		private float useLight = 1f;

		
		private Color useLightCol;

		
		private Vector2 flow_dir;

		
		private Vector3 tempAngle;

		
		private float getheight;

		
		private float getheightC;

		
		private float getheightT;

		
		private float getheightR;

		
		private bool isOverWater;

		
		private float surfaceLevel;

		
		private int layer;

		
		private int layermask;

		
		private Vector3 testpos;

		
		private int i;

		
		private RaycastHit hit;

		
		private Vector2 pixelUV;

		
		private float returnValue;

		
		private float[] returnValueAll;

		
		private float h1;

		
		private float setDegrees;

		
		private float enabledUFX = 1f;

		
		private float enabledCaustics = 1f;

		
		private float setUnderBright;

		
		private float enCaustic;

		
		private float setEdge = 1f;

		
		private Suimono_UnderwaterFog underwaterObject;

		
		private GameObject currentSurfaceObject;

		
		public float[] heightValues;

		
		public float isForward;

		
		public float isAdvDist;

		
		public float waveScale = 1f;

		
		public float flowSpeed = 0.02f;

		
		public float offset;

		
		public Texture2D heightTex;

		
		public Texture2D heightTexT;

		
		public Texture2D heightTexR;

		
		public Transform heightObject;

		
		public Vector2 relativePos = new Vector2(0f, 0f);

		
		public Vector3 texCoord = new Vector3(0f, 0f, 0f);

		
		public Vector3 texCoord1 = new Vector3(0f, 0f, 0f);

		
		public Vector3 texCoordT = new Vector3(0f, 0f, 0f);

		
		public Vector3 texCoordT1 = new Vector3(0f, 0f, 0f);

		
		public Vector3 texCoordR = new Vector3(0f, 0f, 0f);

		
		public Vector3 texCoordR1 = new Vector3(0f, 0f, 0f);

		
		public Color heightVal0;

		
		public Color heightVal1;

		
		public Color heightValT0;

		
		public Color heightValT1;

		
		public Color heightValR0;

		
		public Color heightValR1;

		
		public float localTime;

		
		private float baseHeight;

		
		private float baseAngle;

		
		private Color[] pixelArray;

		
		private Color[] pixelArrayT;

		
		private Color[] pixelArrayR;

		
		private Texture2D useDecodeTex;

		
		private Color[] useDecodeArray;

		
		public int row;

		
		public int pixIndex;

		
		public Color pixCol;

		
		public int t;

		
		public int y;

		
		public Vector3 dir;

		
		public Vector3 pivotPoint;

		
		public float useLocalTime;

		
		public Vector2 flow_dirC;

		
		public Vector2 flowSpeed0;

		
		public Vector2 flowSpeed1;

		
		public Vector2 flowSpeed2;

		
		public Vector2 flowSpeed3;

		
		public float tScale;

		
		public Vector2 oPos;

		
		private int renderCount;

		
		private int randSeed;

		
		private Suimono.Core.Random modRand;

		
		private List<SuimonoObject> sObjects;

		
		private List<Renderer> sRends;

		
		private List<Renderer> sRendSCs;

		
		private ParticleSystem.EmissionModule debrisEmission;

		
		private ColorSpace _colorspace;

		
		private float _deltaTime;
	}
}
