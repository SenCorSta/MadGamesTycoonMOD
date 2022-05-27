using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039C RID: 924
	[ExecuteInEditMode]
	public class SuimonoObject : MonoBehaviour
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x001630B8 File Offset: 0x001612B8
		private void Start()
		{
			if (GameObject.Find("SUIMONO_Module") != null)
			{
				this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				if (this.moduleObject != null)
				{
					this.suimonoModuleLibrary = this.moduleObject.GetComponent<SuimonoModuleLib>();
				}
			}
			this.baseDir = "Resources/";
			this.dir = Path.Combine(Application.dataPath, this.baseDir);
			this.suimonoObject = base.transform.Find("Suimono_Object").gameObject;
			this.surfaceRenderer = base.transform.Find("Suimono_Object").gameObject.GetComponent<Renderer>();
			this.surfaceMesh = base.transform.Find("Suimono_Object").GetComponent<MeshFilter>();
			this.surfaceCollider = base.transform.Find("Suimono_Object").GetComponent<MeshCollider>();
			this.surfaceReflections = base.transform.Find("cam_LocalReflections").gameObject.GetComponent<cameraTools>();
			this.surfaceReflBlur = base.transform.Find("cam_LocalReflections").gameObject.GetComponent<Suimono_DistanceBlur>();
			this.scaleObject = base.transform.Find("Suimono_ObjectScale").gameObject;
			this.scaleRenderer = base.transform.Find("Suimono_ObjectScale").gameObject.GetComponent<Renderer>();
			this.scaleCollider = base.transform.Find("Suimono_ObjectScale").gameObject.GetComponent<MeshCollider>();
			if (this.scaleCollider == null)
			{
				this.scaleCollider = base.transform.Find("Suimono_ObjectScale").gameObject.AddComponent<MeshCollider>();
			}
			this.scaleMesh = base.transform.Find("Suimono_ObjectScale").GetComponent<MeshFilter>();
			this.shader_Surface = Shader.Find("Suimono2/surface");
			this.shader_Scale = Shader.Find("Suimono2/surface_scale");
			this.shader_Under = Shader.Find("Suimono2/surface_under");
			this.tempMaterial = new Material(this.suimonoModuleLibrary.materialSurface);
			if (this.suimonoObject != null)
			{
				this.tempMaterial.shader = this.shader_Surface;
				this.suimonoObject.GetComponent<Renderer>().sharedMaterial = this.tempMaterial;
				this.surfaceRenderer = base.transform.Find("Suimono_Object").gameObject.GetComponent<Renderer>();
			}
			this.ReloadData();
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00163328 File Offset: 0x00161528
		private void OnEnable()
		{
			if (Application.isPlaying)
			{
				if (this.moduleObject == null)
				{
					this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				}
				if (this.moduleObject != null)
				{
					this.moduleObject.RegisterSurface(this);
				}
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00163380 File Offset: 0x00161580
		private void OnDisable()
		{
			if (Application.isPlaying)
			{
				if (this.moduleObject == null)
				{
					this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				}
				if (this.moduleObject != null)
				{
					this.moduleObject.DeregisterSurface(this);
				}
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00016D8E File Offset: 0x00014F8E
		private void ReloadData()
		{
			SuimonoObject.reloadData = false;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x001633D8 File Offset: 0x001615D8
		private void LateUpdate()
		{
			if (this.moduleObject != null)
			{
				this.suimonoVersionNumber = this.moduleObject.suimonoVersionNumber;
				if (SuimonoObject.reloadData)
				{
					this.ReloadData();
				}
				this.systemLocalTime = this.moduleObject.systemTime;
				this.localTime = this.systemLocalTime * this.flowSpeed * (1f / this.waveScale);
				this.flow_dir = this.SuimonoConvertAngleToVector(this.flowDirection);
				if (this.surfaceRenderer != null)
				{
					this.surfaceRenderer.sharedMaterial.SetVector("_suimono_Dir", new Vector4(this.flow_dir.x, 1f, this.flow_dir.y, this.localTime));
				}
				if (this.moduleObject.autoSetLayers)
				{
					base.gameObject.layer = this.moduleObject.layerWaterNum;
					if (this.suimonoObject != null)
					{
						this.suimonoObject.layer = this.moduleObject.layerWaterNum;
					}
					if (this.scaleObject != null)
					{
						this.scaleObject.layer = this.moduleObject.layerWaterNum;
					}
					if (this.surfaceReflections != null)
					{
						this.surfaceReflections.gameObject.layer = this.moduleObject.layerWaterNum;
					}
				}
				if (this.underwaterDepth < 0.1f)
				{
					this.underwaterDepth = 0.1f;
				}
				if (!this.enableCustomMesh)
				{
					base.transform.localScale = new Vector3(base.transform.localScale.x, 1f, base.transform.localScale.z);
				}
				if (this.typeIndex == 0)
				{
					this.suimonoObject.transform.localScale = new Vector3(this.suimonoObject.transform.localScale.x, 1f, this.suimonoObject.transform.localScale.z);
					this.scaleObject.transform.localScale = new Vector3(this.scaleObject.transform.localScale.x, 1f, this.scaleObject.transform.localScale.z);
					this.surfaceReflections.transform.localScale = new Vector3(this.surfaceReflections.transform.localScale.x, 1f, this.surfaceReflections.transform.localScale.z);
				}
				this.useBeaufortScale = !this.customWaves;
				if (this.useBeaufortScale)
				{
					this.beaufortVal = this.beaufortScale / 12f;
					this.turbulenceFactor = Mathf.Clamp(Mathf.Lerp(-0.1f, 2.1f, this.beaufortVal) * 0.9f, 0f, 0.75f);
					this.waveHeight = Mathf.Clamp(Mathf.Lerp(0f, 5f, this.beaufortVal), 0f, 0.65f);
					this.waveHeight -= Mathf.Clamp(Mathf.Lerp(-1.5f, 1f, this.beaufortVal), 0f, 0.5f);
					this.lgWaveHeight = Mathf.Clamp(Mathf.Lerp(-0.2f, 1.1f, this.beaufortVal) * 2.8f, 0f, 3f);
					if (this.typeIndex == 0)
					{
						this.waveScale = 0.5f;
						this.lgWaveScale = 0.03125f;
					}
				}
				if (this.presetStartTransition)
				{
					if (this.presetTimer >= 1f)
					{
						this.presetStartTransition = false;
					}
					else
					{
						this.presetTimer += Time.deltaTime / this.presetTransitionTime;
						this.PresetLoadBuild(this.currentPresetFolder, this.currentPresetName);
					}
				}
				if (this.typeIndex == 0)
				{
					this.useLodIndex = 0;
				}
				if (this.typeIndex == 1)
				{
					this.useLodIndex = this.lodIndex;
				}
				if (this.typeIndex == 2)
				{
					this.useLodIndex = 3;
				}
				if (this.typeIndex == 0)
				{
					this.enableCustomMesh = false;
				}
				if (!this.enableCustomMesh)
				{
					if (this.suimonoModuleLibrary && !this.meshWasSet)
					{
						if (this.suimonoModuleLibrary.texNormalC && this.surfaceMesh != null)
						{
							this.surfaceMesh.mesh = this.suimonoModuleLibrary.meshLevel[this.useLodIndex];
						}
						if (this.suimonoModuleLibrary.texNormalC && this.surfaceCollider != null)
						{
							this.surfaceCollider.sharedMesh = this.suimonoModuleLibrary.meshLevel[3];
						}
						this.meshWasSet = true;
					}
					else
					{
						this.meshWasSet = false;
					}
				}
				else if (this.customMesh != null)
				{
					if (this.surfaceMesh != null)
					{
						this.surfaceMesh.mesh = this.customMesh;
					}
					if (this.surfaceCollider != null)
					{
						this.surfaceCollider.sharedMesh = this.customMesh;
					}
				}
				else
				{
					if (this.suimonoModuleLibrary.texNormalC && this.surfaceMesh != null)
					{
						this.surfaceMesh.mesh = this.suimonoModuleLibrary.meshLevel[this.useLodIndex];
					}
					if (this.suimonoModuleLibrary.texNormalC && this.surfaceCollider != null)
					{
						this.surfaceCollider.sharedMesh = this.suimonoModuleLibrary.meshLevel[3];
					}
					this.meshWasSet = false;
				}
				if (this.useLodIndex == 3)
				{
					this.useHeightProjection = 0f;
					this.useEnableTess = false;
				}
				else
				{
					this.useHeightProjection = this.heightProjection;
					this.useEnableTess = this.enableTess;
				}
				Shader.SetGlobalFloat("cmScaleX", this.enableCustomMesh ? this.cmScaleX : 1f);
				Shader.SetGlobalFloat("cmScaleY", this.enableCustomMesh ? this.cmScaleY : 1f);
				if (this.suimonoModuleLibrary.texNormalC && this.scaleMesh != null)
				{
					this.scaleMesh.mesh = this.suimonoModuleLibrary.meshLevel[1];
				}
				if (this.enableCustomTextures)
				{
					if (this.customTexNormal1 != null)
					{
						this.useTexNormal1 = this.customTexNormal1;
					}
					else
					{
						this.useTexNormal1 = this.suimonoModuleLibrary.texNormalC;
					}
					if (this.customTexNormal2 != null)
					{
						this.useTexNormal2 = this.customTexNormal2;
					}
					else
					{
						this.useTexNormal2 = this.suimonoModuleLibrary.texNormalT;
					}
					if (this.customTexNormal3 != null)
					{
						this.useTexNormal3 = this.customTexNormal3;
					}
					else
					{
						this.useTexNormal3 = this.suimonoModuleLibrary.texNormalR;
					}
				}
				else if (this.suimonoModuleLibrary != null)
				{
					this.useTexNormal1 = this.suimonoModuleLibrary.texNormalC;
					this.useTexNormal2 = this.suimonoModuleLibrary.texNormalT;
					this.useTexNormal3 = this.suimonoModuleLibrary.texNormalR;
				}
				if (this.suimonoModuleLibrary != null && this.surfaceRenderer != null)
				{
					this.surfaceRenderer.sharedMaterial.SetTexture("_MaskTex", this.suimonoModuleLibrary.texMask);
				}
				if (this.surfaceReflections != null && this.moduleObject != null)
				{
					this.useEnableReflections = this.enableReflections;
					this.useEnableDynamicReflections = this.enableDynamicReflections;
					if (!this.moduleObject.enableReflections)
					{
						this.useEnableReflections = false;
					}
					if (!this.moduleObject.enableDynamicReflections)
					{
						this.useEnableDynamicReflections = false;
					}
					if (!this.useEnableReflections || !this.moduleObject.enableReflections)
					{
						this.useReflections = false;
						this.surfaceReflections.gameObject.SetActive(false);
					}
					else if (!this.useEnableDynamicReflections || !this.moduleObject.enableDynamicReflections)
					{
						this.surfaceReflections.gameObject.SetActive(false);
					}
					else
					{
						this.surfaceReflections.gameObject.SetActive(true);
						this.useReflections = true;
						this.reflectLayer &= ~(1 << this.moduleObject.layerWaterNum);
						this.reflectLayer &= ~(1 << this.moduleObject.layerDepthNum);
						this.reflectLayer &= ~(1 << this.moduleObject.layerScreenFXNum);
						this.surfaceReflections.setLayers = this.reflectLayer;
						this.surfaceReflections.resolution = Convert.ToInt32(this.resolutions[this.reflectResolution]);
						if (this.moduleObject.setCameraComponent != null)
						{
							this.reflectionDistance = this.moduleObject.setCameraComponent.farClipPlane + 200f;
						}
						this.surfaceReflections.reflectionDistance = this.reflectionDistance;
						this.surfaceReflBlur.blurAmt = this.reflectBlur;
						if (this.useShader == this.shader_Under)
						{
							this.surfaceReflections.isUnderwater = true;
						}
						else
						{
							this.surfaceReflections.isUnderwater = false;
						}
					}
				}
				if (this.surfaceRenderer != null)
				{
					if (Application.isPlaying && this.useShader != null && this.currUseShader != this.useShader)
					{
						this.currUseShader = this.useShader;
						this.surfaceRenderer.sharedMaterial.shader = this.currUseShader;
					}
					if (!Application.isPlaying)
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_isPlaying", 0f);
					}
					else
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_isPlaying", 1f);
					}
					this.surfaceRenderer.sharedMaterial.SetTexture("_NormalTexS", this.useTexNormal1);
					this.surfaceRenderer.sharedMaterial.SetTexture("_NormalTexD", this.useTexNormal2);
					this.surfaceRenderer.sharedMaterial.SetTexture("_NormalTexR", this.useTexNormal3);
					this.surfaceRenderer.sharedMaterial.SetFloat("_beaufortFlag", this.useBeaufortScale ? 1f : 0f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_beaufortScale", this.beaufortVal);
					this.surfaceRenderer.sharedMaterial.SetFloat("_turbulenceFactor", this.turbulenceFactor);
					float num = this.enableCustomMesh ? this.cmScaleX : 1f;
					float num2 = this.enableCustomMesh ? this.cmScaleY : 1f;
					this.surfaceRenderer.sharedMaterial.SetTextureScale("_NormalTexS", new Vector2(this.suimonoObject.transform.localScale.x / this.waveScale * base.transform.localScale.x * num, this.suimonoObject.transform.localScale.z / this.waveScale * base.transform.localScale.z * num2));
					this.surfaceRenderer.sharedMaterial.SetVector("_scaleUVs", new Vector4(this.suimonoObject.transform.localScale.x / this.waveScale * num, this.suimonoObject.transform.localScale.z / this.waveScale * num2, 0f, 0f));
					this.surfaceRenderer.sharedMaterial.SetFloat("_lgWaveScale", this.lgWaveScale);
					this.surfaceRenderer.sharedMaterial.SetFloat("_lgWaveHeight", this.lgWaveHeight);
					if (this.typeIndex == 0)
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_tessScale", this.suimonoObject.transform.localScale.x);
					}
					else
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_tessScale", base.transform.localScale.x);
					}
					this.surfaceRenderer.sharedMaterial.SetFloat("_Tess", Mathf.Lerp(0.001f, this.waveTessAmt, this.useEnableTess ? 1f : 0f));
					this.surfaceRenderer.sharedMaterial.SetFloat("_minDist", Mathf.Lerp(-180f, 0f, this.waveTessMin));
					this.surfaceRenderer.sharedMaterial.SetFloat("_maxDist", Mathf.Lerp(20f, 500f, this.waveTessSpread));
					this.surfaceRenderer.sharedMaterial.SetFloat("_unity_fogstart", RenderSettings.fogStartDistance);
					this.surfaceRenderer.sharedMaterial.SetFloat("_unity_fogend", RenderSettings.fogEndDistance);
					this.surfaceRenderer.sharedMaterial.SetFloat("_causticsFlag", this.enableCausticFX ? 1f : 0f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_CausticsFade", Mathf.Lerp(1f, 500f, this.causticsFade));
					this.surfaceRenderer.sharedMaterial.SetColor("_CausticsColor", this.causticsColor);
					this.surfaceRenderer.sharedMaterial.SetFloat("_aberrationScale", this.aberrationScale);
					this.surfaceRenderer.sharedMaterial.SetFloat("_enableFoam", this.enableFoam ? 1f : 0f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_EdgeFoamFade", Mathf.Lerp(1500f, 5f, this.edgeFoamAmt));
					this.surfaceRenderer.sharedMaterial.SetFloat("_HeightFoamAmt", this.heightFoamAmt);
					this.surfaceRenderer.sharedMaterial.SetFloat("_HeightFoamHeight", this.hFoamHeight);
					this.surfaceRenderer.sharedMaterial.SetFloat("_HeightFoamSpread", this.hFoamSpread);
					this.surfaceRenderer.sharedMaterial.SetFloat("_foamSpeed", this.foamSpeed);
					this.surfaceRenderer.sharedMaterial.SetTextureScale("_FoamTex", this.foamScale * new Vector2(this.suimonoObject.transform.localScale.x / this.foamScale * base.transform.localScale.x * num, this.suimonoObject.transform.localScale.z / this.foamScale * base.transform.localScale.z * num2));
					this.surfaceRenderer.sharedMaterial.SetFloat("_foamScale", Mathf.Lerp(160f, 1f, this.foamScale));
					this.surfaceRenderer.sharedMaterial.SetColor("_FoamColor", this.foamColor);
					this.surfaceRenderer.sharedMaterial.SetFloat("_ShallowFoamAmt", this.shallowFoamAmt);
					this.surfaceRenderer.sharedMaterial.SetFloat("_heightScaleFac", 1f / base.transform.localScale.y);
					this.surfaceRenderer.sharedMaterial.SetFloat("_heightProjection", this.useHeightProjection);
					this.surfaceRenderer.sharedMaterial.SetFloat("_heightScale", this.waveHeight);
					this.surfaceRenderer.sharedMaterial.SetFloat("_RefractStrength", this.refractStrength);
					this.surfaceRenderer.sharedMaterial.SetFloat("_ReflectStrength", this.reflectProjection);
					this.surfaceRenderer.sharedMaterial.SetFloat("_shorelineHeight", this.shorelineHeight);
					this.surfaceRenderer.sharedMaterial.SetFloat("_shorelineFrequency", this.shorelineFreq);
					this.surfaceRenderer.sharedMaterial.SetFloat("_shorelineScale", 0.1f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_shorelineSpeed", this.shorelineSpeed);
					this.surfaceRenderer.sharedMaterial.SetFloat("_shorelineNorm", this.shorelineNorm);
					this.surfaceRenderer.sharedMaterial.SetFloat("_specularPower", this.specularPower);
					this.surfaceRenderer.sharedMaterial.SetFloat("_roughness", this.roughness);
					this.surfaceRenderer.sharedMaterial.SetFloat("_roughness2", this.roughness2);
					this.surfaceRenderer.sharedMaterial.SetFloat("_reflecTerm", this.reflectTerm);
					this.surfaceRenderer.sharedMaterial.SetFloat("_reflecSharp", Mathf.Lerp(0f, -1.5f, this.reflectSharpen));
					this.surfaceRenderer.sharedMaterial.SetFloat("_overallBrightness", this.overallBright);
					this.surfaceRenderer.sharedMaterial.SetFloat("_overallTransparency", this.overallTransparency);
					this.surfaceRenderer.sharedMaterial.SetFloat("_DepthFade", Mathf.Lerp(0.1f, 200f, this.depthAmt));
					this.surfaceRenderer.sharedMaterial.SetFloat("_ShallowFade", Mathf.Lerp(0.1f, 800f, this.shallowAmt));
					this.surfaceRenderer.sharedMaterial.SetColor("_depthColor", this.depthColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_shallowColor", this.shallowColor);
					this.surfaceRenderer.sharedMaterial.SetFloat("_EdgeFade", Mathf.Lerp(10f, 1000f, this.edgeAmt));
					this.surfaceRenderer.sharedMaterial.SetColor("_SpecularColor", this.specularColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_SSSColor", this.sssColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_BlendColor", this.blendColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_OverlayColor", this.overlayColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_UnderwaterColor", this.underwaterColor);
					this.surfaceRenderer.sharedMaterial.SetFloat("_reflectFlag", this.useEnableReflections ? 1f : 0f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_reflectDynamicFlag", this.useEnableDynamicReflections ? 1f : 0f);
					this.surfaceRenderer.sharedMaterial.SetFloat("_reflectFallback", (float)this.reflectFallback);
					this.surfaceRenderer.sharedMaterial.SetColor("_reflectFallbackColor", this.customRefColor);
					this.surfaceRenderer.sharedMaterial.SetColor("_ReflectionColor", this.reflectionColor);
					this.skybox = RenderSettings.skybox;
					if (this.skybox != null && this.skybox.HasProperty("_Tex") && this.skybox.HasProperty("_Tint") && this.skybox.HasProperty("_Exposure") && this.skybox.HasProperty("_Rotation"))
					{
						this.surfaceRenderer.sharedMaterial.SetTexture("_SkyCubemap", this.skybox.GetTexture("_Tex"));
						this.surfaceRenderer.sharedMaterial.SetColor("_SkyTint", this.skybox.GetColor("_Tint"));
						this.surfaceRenderer.sharedMaterial.SetFloat("_SkyExposure", this.skybox.GetFloat("_Exposure"));
						this.surfaceRenderer.sharedMaterial.SetFloat("_SkyRotation", this.skybox.GetFloat("_Rotation"));
					}
					if (this.customRefCubemap != null)
					{
						this.surfaceRenderer.sharedMaterial.SetTexture("_CubeTex", this.customRefCubemap);
					}
					this.surfaceRenderer.sharedMaterial.SetFloat("_cameraDistance", this.cameraDistance);
					Vector2 value = new Vector2(1f, 10f);
					this.surfaceRenderer.sharedMaterial.SetTextureScale("_WaveTex", value);
					this.scaleRenderer.sharedMaterial.SetTextureScale("_WaveTex", value);
					this.suimono_refl_off = 0f;
					this.suimono_refl_sky = 0f;
					this.suimono_refl_cube = 0f;
					this.suimono_refl_color = 0f;
					if (this.reflectFallback == 0)
					{
						this.suimono_refl_off = 1f;
					}
					if (this.reflectFallback == 1)
					{
						this.suimono_refl_sky = 1f;
					}
					if (this.reflectFallback == 2)
					{
						this.suimono_refl_cube = 1f;
					}
					if (this.reflectFallback == 3)
					{
						this.suimono_refl_color = 1f;
					}
					if (this.surfaceRenderer != null)
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_tess_on", this.enableTess ? 1f : 0f);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_trans_on", this.moduleObject.enableTransparency ? 1f : 0f);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_caust_on", this.moduleObject.enableCaustics ? 1f : 0f);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_dynrefl_on", this.useDynReflections ? 1f : 0f);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_refl_off", this.suimono_refl_off);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_refl_sky", this.suimono_refl_sky);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_refl_cube", this.suimono_refl_cube);
						this.surfaceRenderer.sharedMaterial.SetFloat("suimono_refl_color", this.suimono_refl_color);
					}
					if (this.scaleRenderer != null && this.typeIndex == 0)
					{
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_tess_on", this.enableTess ? 1f : 0f);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_trans_on", this.moduleObject.enableTransparency ? 1f : 0f);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_caust_on", this.moduleObject.enableCaustics ? 1f : 0f);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_dynrefl_on", this.useDynReflections ? 1f : 0f);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_refl_off", this.suimono_refl_off);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_refl_sky", this.suimono_refl_sky);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_refl_cube", this.suimono_refl_cube);
						this.scaleRenderer.sharedMaterial.SetFloat("suimono_refl_color", this.suimono_refl_color);
					}
				}
				if (this.typeIndex == 0 && Application.isPlaying)
				{
					if (this.moduleObject.isUnderwater)
					{
						if (this.scaleRenderer != null)
						{
							this.scaleRenderer.enabled = false;
						}
						if (this.scaleCollider != null)
						{
							this.scaleCollider.enabled = false;
						}
					}
					else
					{
						if (this.scaleRenderer != null)
						{
							this.scaleRenderer.enabled = true;
						}
						if (this.scaleCollider != null)
						{
							this.scaleCollider.enabled = true;
						}
					}
				}
				else
				{
					if (this.scaleRenderer != null)
					{
						this.scaleRenderer.enabled = false;
					}
					if (this.scaleCollider != null)
					{
						this.scaleCollider.enabled = false;
					}
				}
				if (Application.isPlaying)
				{
					if (this.typeIndex == 0)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						if (this.oceanScale < 1f)
						{
							this.oceanScale = 1f;
						}
						this.offamt = 0.4027f * this.oceanScale / this.waveScale;
						this.spacer = this.suimonoObject.transform.localScale.x * 4f;
						this.newPos = new Vector3(this.moduleObject.setCamera.position.x, this.suimonoObject.transform.position.y, this.moduleObject.setCamera.position.z);
						if (Mathf.Abs(this.suimonoObject.transform.position.x - this.newPos.x) > this.spacer)
						{
							if (this.suimonoObject.transform.position.x > this.newPos.x)
							{
								this.setScaleX -= this.offamt;
							}
							if (this.suimonoObject.transform.position.x < this.newPos.x)
							{
								this.setScaleX += this.offamt;
							}
							this.suimonoObject.transform.position = new Vector3(this.newPos.x, this.suimonoObject.transform.position.y, this.suimonoObject.transform.position.z);
							this.scaleObject.transform.position = new Vector3(this.newPos.x, this.scaleObject.transform.position.y, this.scaleObject.transform.position.z);
						}
						if (Mathf.Abs(this.suimonoObject.transform.position.z - this.newPos.z) > this.spacer)
						{
							if (this.suimonoObject.transform.position.z > this.newPos.z)
							{
								this.setScaleZ -= this.offamt;
							}
							if (this.suimonoObject.transform.position.z < this.newPos.z)
							{
								this.setScaleZ += this.offamt;
							}
							this.suimonoObject.transform.position = new Vector3(this.suimonoObject.transform.position.x, this.suimonoObject.transform.position.y, this.newPos.z);
							this.scaleObject.transform.position = new Vector3(this.scaleObject.transform.position.x, this.scaleObject.transform.position.y, this.newPos.z);
						}
						if (this.currentPosition != this.suimonoObject.transform.position)
						{
							this.currentPosition = this.suimonoObject.transform.position;
							this.savePos = new Vector2(this.setScaleX, this.setScaleZ);
						}
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_uvx", 0f - this.savePos.x);
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_uvy", 0f - this.savePos.y);
						this.scaleObject.transform.localPosition = new Vector3(this.scaleObject.transform.localPosition.x, -0.1f, this.scaleObject.transform.localPosition.z);
						if (this.scaleRenderer != null)
						{
							this.setScale = Mathf.Ceil(this.moduleObject.setCameraComponent.farClipPlane / 20f) * this.suimonoObject.transform.localScale.x;
							this.scaleObject.transform.localScale = new Vector3(this.setScale * 0.5f, 1f, this.setScale * 0.5f);
							this.oceanUseScale = 4f;
							base.transform.localScale = new Vector3(1f, 1f, 1f);
							this.suimonoObject.transform.localScale = new Vector3(this.oceanUseScale * this.oceanScale, 1f, this.oceanUseScale * this.oceanScale);
							if (this.scaleRenderer != null)
							{
								this.scaleRenderer.material.CopyPropertiesFromMaterial(this.tempMaterial);
								this.scaleRenderer.sharedMaterial.SetFloat("_suimono_uvx", 0f - this.savePos.x);
								this.scaleRenderer.sharedMaterial.SetFloat("_suimono_uvy", 0f - this.savePos.y);
								this.setSc = this.scaleRenderer.sharedMaterial.GetTextureScale("_NormalTexS");
								this.useSc = this.scaleObject.transform.localScale.x / this.suimonoObject.transform.localScale.x;
								this.scaleRenderer.sharedMaterial.SetTextureScale("_NormalTexS", this.setSc * this.useSc);
								this.scaleRenderer.sharedMaterial.SetTextureScale("_FoamTex", this.setSc * this.useSc);
							}
						}
					}
					else
					{
						this.savePos = new Vector3(0f, 0f, 0f);
						this.suimonoObject.transform.localScale = new Vector3(1f, 1f, 1f);
						this.scaleObject.transform.localScale = new Vector3(1f, 1f, 1f);
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_uvx", 0f);
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_uvy", 0f);
					}
				}
				if (this.surfaceRenderer != null)
				{
					if (this.showDepthMask)
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_DebugDepthMask", 1f);
					}
					else
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_DebugDepthMask", 0f);
					}
					if (this.showWorldMask)
					{
						this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_DebugWorldNormalMask", 1f);
						return;
					}
					this.surfaceRenderer.sharedMaterial.SetFloat("_suimono_DebugWorldNormalMask", 0f);
				}
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x001652EC File Offset: 0x001634EC
		public Vector2 SuimonoConvertAngleToVector(float convertAngle)
		{
			this.flow_dir = new Vector2(0f, 0f);
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

		// Token: 0x0600222B RID: 8747 RVA: 0x00016D96 File Offset: 0x00014F96
		public void SuimonoSetPreset(string fName, string pName)
		{
			this.presetTimer = 1f;
			this.SetTemporaryPresetData();
			this.PresetLoadBuild(fName, pName);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x001653CC File Offset: 0x001635CC
		public void SuimonoSavePreset(string fName, string pName)
		{
			int num = this.PresetGetNum("folder", fName);
			int num2 = this.PresetGetNum("preset", pName);
			if (num >= 0 && num2 >= 0)
			{
				this.PresetSave(num, num2);
				return;
			}
			Debug.Log(string.Concat(new string[]
			{
				"The Preset ",
				pName,
				" in folder ",
				fName,
				" cannot be found!"
			}));
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00165438 File Offset: 0x00163638
		private void PresetInit()
		{
			this.presetTimer = 1f;
			this.presetDirsArr = new List<string>();
			this.dirInfo = new DirectoryInfo(this.dir + "/").GetFiles("SUIMONO_PRESETS_*");
			if (new DirectoryInfo(this.dir + "/") != null)
			{
				this.d = 0;
				while (this.d < this.dirInfo.Length)
				{
					this.presetDirsArr.Add(this.dirInfo[this.d].ToString());
					this.d++;
				}
			}
			this.presetDirs = new List<string>(new string[this.presetDirsArr.Count]);
			this.dn = 0;
			while (this.dn < this.presetDirsArr.Count)
			{
				this.presetDirs[this.dn] = this.presetDirsArr[this.dn].ToString();
				this.presetDirs[this.dn] = this.presetDirs[this.dn].Remove(0, this.dir.Length);
				this.presetDirs[this.dn] = this.presetDirs[this.dn].Replace("SUIMONO_PRESETS_", "");
				this.presetDirs[this.dn] = this.presetDirs[this.dn].Replace(".meta", "");
				this.dn++;
			}
			this.presetFilesArr = new List<string>();
			this.pdir = this.dir + "/SUIMONO_PRESETS_" + this.presetDirs[this.presetFileIndex];
			this.fileInfo = new DirectoryInfo(this.pdir).GetFiles("SUIMONO_PRESET_*");
			if (new DirectoryInfo(this.pdir) != null)
			{
				this.f = 0;
				while (this.f < this.fileInfo.Length)
				{
					this.presetFilesArr.Add(this.fileInfo[this.f].ToString());
					this.f++;
				}
			}
			this.px = 0;
			this.nx = 0;
			while (this.nx < this.presetFilesArr.Count)
			{
				if (!this.presetFilesArr[this.nx].ToString().Contains(".meta"))
				{
					this.px++;
				}
				this.nx++;
			}
			this.presetFiles = new string[this.px];
			this.ax = 0;
			this.n = 0;
			while (this.n < this.presetFilesArr.Count)
			{
				if (!this.presetFilesArr[this.n].ToString().Contains(".meta"))
				{
					this.presetFiles[this.ax] = this.presetFilesArr[this.n].ToString();
					this.presetFiles[this.ax] = this.presetFiles[this.ax].Remove(0, this.pdir.Length);
					this.presetFiles[this.ax] = this.presetFiles[this.ax].Replace("SUIMONO_PRESET_", "");
					this.presetFiles[this.ax] = this.presetFiles[this.ax].Replace(".txt", "");
					this.ax++;
				}
				this.n++;
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x001657FC File Offset: 0x001639FC
		public int PresetGetNum(string mode, string pName)
		{
			int result = -1;
			int num = -1;
			int num2 = -1;
			if (mode == "folder")
			{
				this.tempPresetDirsArr = new List<string>();
				this.dirInfo = new DirectoryInfo(this.dir + "/").GetFiles("SUIMONO_PRESETS_*");
				if (new DirectoryInfo(this.dir + "/") != null)
				{
					this.d = 0;
					while (this.d < this.dirInfo.Length)
					{
						this.tempPresetDirsArr.Add(this.dirInfo[this.d].ToString());
						this.d++;
					}
				}
				this.tempPresetDirs = new string[this.tempPresetDirsArr.Count];
				this.dn = 0;
				while (this.dn < this.tempPresetDirsArr.Count)
				{
					this.tempPresetDirs[this.dn] = this.tempPresetDirsArr[this.dn].ToString();
					this.tempPresetDirs[this.dn] = this.tempPresetDirs[this.dn].Remove(0, this.dir.Length);
					this.tempPresetDirs[this.dn] = this.tempPresetDirs[this.dn].Replace("SUIMONO_PRESETS_", "");
					this.tempPresetDirs[this.dn] = this.tempPresetDirs[this.dn].Replace(".meta", "");
					if (this.tempPresetDirs[this.dn] == pName)
					{
						num = this.dn;
					}
					this.dn++;
				}
				result = num;
			}
			if (mode == "preset")
			{
				this.tempPresetFilesArr = new List<string>();
				this.pdir = this.dir + "/SUIMONO_PRESETS_" + this.presetDirs[this.presetFileIndex];
				this.fileInfo = new DirectoryInfo(this.pdir).GetFiles("SUIMONO_PRESET_*");
				if (new DirectoryInfo(this.pdir) != null)
				{
					this.f = 0;
					while (this.f < this.fileInfo.Length)
					{
						this.tempPresetFilesArr.Add(this.fileInfo[this.f].ToString());
						this.f++;
					}
				}
				this.px = 0;
				this.nx = 0;
				while (this.nx < this.tempPresetFilesArr.Count)
				{
					if (!this.tempPresetFilesArr[this.nx].ToString().Contains(".meta"))
					{
						this.px++;
					}
					this.nx++;
				}
				this.tempPresetFiles = new string[this.px];
				this.ax = 0;
				this.n = 0;
				while (this.n < this.tempPresetFilesArr.Count)
				{
					if (!this.tempPresetFilesArr[this.n].ToString().Contains(".meta"))
					{
						this.tempPresetFiles[this.ax] = this.tempPresetFilesArr[this.n].ToString();
						this.tempPresetFiles[this.ax] = this.tempPresetFiles[this.ax].Remove(0, this.pdir.Length);
						this.tempPresetFiles[this.ax] = this.tempPresetFiles[this.ax].Replace("SUIMONO_PRESET_", "");
						this.tempPresetFiles[this.ax] = this.tempPresetFiles[this.ax].Replace(".txt", "");
						if (this.tempPresetFiles[this.ax] == pName)
						{
							num2 = this.ax;
						}
						this.ax++;
					}
					this.n++;
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00002098 File Offset: 0x00000298
		public void PresetRename(int ppos, string newName)
		{
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00002098 File Offset: 0x00000298
		public void PresetAdd()
		{
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00002098 File Offset: 0x00000298
		public void PresetDelete(int fpos, int ppos)
		{
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00002098 File Offset: 0x00000298
		public void PresetSave(int fpos, int ppos)
		{
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00165BF8 File Offset: 0x00163DF8
		public void PresetLoad(int ppos)
		{
			if (this.presetIndex >= 0)
			{
				this.pdir = this.dir + "/SUIMONO_PRESETS_" + this.presetDirs[this.presetFileIndex];
				this.sr = new StreamReader(this.pdir + "/SUIMONO_PRESET_" + this.presetFiles[ppos] + ".txt");
				this.presetDataString = this.sr.ReadToEnd();
				this.sr.Close();
				this.presetDataArray = this.presetDataString.Split(new char[]
				{
					"\n"[0]
				});
				this.dx = 0;
				while (this.dx < this.presetDataArray.Length)
				{
					if (this.presetDataArray[this.dx] != "" && this.presetDataArray[this.dx] != "\n")
					{
						this.pFrom = this.presetDataArray[this.dx].IndexOf("<") + "<".Length;
						this.pTo = this.presetDataArray[this.dx].LastIndexOf(">");
						this.key = this.presetDataArray[this.dx].Substring(this.pFrom, this.pTo - this.pFrom);
						this.pFrom = this.presetDataArray[this.dx].IndexOf("(") + "(".Length;
						this.pTo = this.presetDataArray[this.dx].LastIndexOf(")");
						this.dat = this.presetDataArray[this.dx].Substring(this.pFrom, this.pTo - this.pFrom);
						this.SetTemporaryPresetData();
						this.PresetDecode(this.key, this.dat);
					}
					this.dx++;
				}
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00165E00 File Offset: 0x00164000
		private void PresetLoadBuild(string fName, string pName)
		{
			this.datFile = (Resources.Load("SUIMONO_PRESETS_" + fName + "/SUIMONO_PRESET_" + pName) as TextAsset);
			this.presetDataString = this.datFile.text;
			this.presetDataArray = this.presetDataString.Split(new char[]
			{
				"\n"[0]
			});
			this.dx = 0;
			while (this.dx < this.presetDataArray.Length)
			{
				if (this.presetDataArray[this.dx] != "" && this.presetDataArray[this.dx] != "\n")
				{
					this.pFrom = this.presetDataArray[this.dx].IndexOf("<") + "<".Length;
					this.pTo = this.presetDataArray[this.dx].LastIndexOf(">");
					this.key = this.presetDataArray[this.dx].Substring(this.pFrom, this.pTo - this.pFrom);
					this.pFrom = this.presetDataArray[this.dx].IndexOf("(") + "(".Length;
					this.pTo = this.presetDataArray[this.dx].LastIndexOf(")");
					this.dat = this.presetDataArray[this.dx].Substring(this.pFrom, this.pTo - this.pFrom);
					this.PresetDecode(this.key, this.dat);
				}
				this.dx++;
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00165FBC File Offset: 0x001641BC
		private void PresetDecode(string key, string dat)
		{
			this.dataS = dat.Split(new char[]
			{
				","[0]
			});
			if (this.presetTimer > 1f)
			{
				this.presetTimer = 1f;
			}
			if (key == "color_depth")
			{
				this.depthColor = Color.Lerp(this.temp_depthColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_shallow")
			{
				this.shallowColor = Color.Lerp(this.temp_shallowColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_blend")
			{
				this.blendColor = Color.Lerp(this.temp_blendColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_overlay")
			{
				this.overlayColor = Color.Lerp(this.temp_overlayColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_caustics")
			{
				this.causticsColor = Color.Lerp(this.temp_causticsColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_reflection")
			{
				this.reflectionColor = Color.Lerp(this.temp_reflectionColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_specular")
			{
				this.specularColor = Color.Lerp(this.temp_specularColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_sss")
			{
				this.sssColor = Color.Lerp(this.temp_sssColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_foam")
			{
				this.foamColor = Color.Lerp(this.temp_foamColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "color_underwater")
			{
				this.underwaterColor = Color.Lerp(this.temp_underwaterColor, this.DecodeColor(this.dataS), this.presetTimer);
			}
			if (key == "data_beaufort")
			{
				this.beaufortScale = Mathf.Lerp(this.temp_beaufortScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_flowdir")
			{
				this.flowDirection = Mathf.Lerp(this.temp_flowDirection, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_flowspeed")
			{
				this.flowSpeed = Mathf.Lerp(this.temp_flowSpeed, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_wavescale")
			{
				this.waveScale = Mathf.Lerp(this.temp_waveScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_waveheight")
			{
				this.waveHeight = Mathf.Lerp(this.temp_waveHeight, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_heightprojection")
			{
				this.heightProjection = Mathf.Lerp(this.temp_heightProjection, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_turbulence")
			{
				this.turbulenceFactor = Mathf.Lerp(this.temp_turbulenceFactor, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_lgwaveheight")
			{
				this.lgWaveHeight = Mathf.Lerp(this.temp_lgWaveHeight, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_lgwavescale")
			{
				this.lgWaveScale = Mathf.Lerp(this.temp_lgWaveScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shorelineheight")
			{
				this.shorelineHeight = Mathf.Lerp(this.temp_shorelineHeight, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shorelinefreq")
			{
				this.shorelineFreq = Mathf.Lerp(this.temp_shorelineFreq, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shorelinescale")
			{
				this.shorelineScale = Mathf.Lerp(this.temp_shorelineScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shorelinespeed")
			{
				this.shorelineSpeed = Mathf.Lerp(this.temp_shorelineSpeed, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shorelinenorm")
			{
				this.shorelineNorm = Mathf.Lerp(this.temp_shorelineNorm, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_overallbright")
			{
				this.overallBright = Mathf.Lerp(this.temp_overallBright, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_overalltransparency")
			{
				this.overallTransparency = Mathf.Lerp(this.temp_overallTransparency, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_edgeamt")
			{
				this.edgeAmt = Mathf.Lerp(this.temp_edgeAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_depthamt")
			{
				this.depthAmt = Mathf.Lerp(this.temp_depthAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shallowamt")
			{
				this.shallowAmt = Mathf.Lerp(this.temp_shallowAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_refractstrength")
			{
				this.refractStrength = Mathf.Lerp(this.temp_refractStrength, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_aberrationscale")
			{
				this.aberrationScale = Mathf.Lerp(this.temp_aberrationScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_causticsfade")
			{
				this.causticsFade = Mathf.Lerp(this.temp_causticsFade, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_reflectprojection")
			{
				this.reflectProjection = Mathf.Lerp(this.temp_reflectProjection, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_reflectblur")
			{
				this.reflectBlur = Mathf.Lerp(this.temp_reflectBlur, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_reflectterm")
			{
				this.reflectTerm = Mathf.Lerp(this.temp_reflectTerm, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_reflectsharpen")
			{
				this.reflectSharpen = Mathf.Lerp(this.temp_reflectSharpen, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_roughness")
			{
				this.roughness = Mathf.Lerp(this.temp_roughness, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_roughness2")
			{
				this.roughness2 = Mathf.Lerp(this.temp_roughness2, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_foamscale")
			{
				this.foamScale = Mathf.Lerp(this.temp_foamScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_foamspeed")
			{
				this.foamSpeed = Mathf.Lerp(this.temp_foamSpeed, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_edgefoamamt")
			{
				this.edgeFoamAmt = Mathf.Lerp(this.temp_edgeFoamAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_shallowfoamamt")
			{
				this.shallowFoamAmt = Mathf.Lerp(this.temp_shallowFoamAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_heightfoamamt")
			{
				this.heightFoamAmt = Mathf.Lerp(this.temp_heightFoamAmt, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_hfoamheight")
			{
				this.hFoamHeight = Mathf.Lerp(this.temp_hFoamHeight, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_hfoamspread")
			{
				this.hFoamSpread = Mathf.Lerp(this.temp_hFoamSpread, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underlightfactor")
			{
				this.underLightFactor = Mathf.Lerp(this.temp_underLightFactor, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underrefractionamount")
			{
				this.underRefractionAmount = Mathf.Lerp(this.temp_underRefractionAmount, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underrefractionscale")
			{
				this.underRefractionScale = Mathf.Lerp(this.temp_underRefractionScale, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underrefractionspeed")
			{
				this.underRefractionSpeed = Mathf.Lerp(this.temp_underRefractionSpeed, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underbluramount")
			{
				this.underBlurAmount = Mathf.Lerp(this.temp_underBlurAmount, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underwaterfogdist")
			{
				this.underwaterFogDist = Mathf.Lerp(this.temp_underwaterFogDist, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underwaterfogspread")
			{
				this.underwaterFogSpread = Mathf.Lerp(this.temp_underwaterFogSpread, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_underDarkRange")
			{
				this.underDarkRange = Mathf.Lerp(this.temp_underDarkRange, this.DecodeFloat(this.dataS), this.presetTimer);
			}
			if (key == "data_customwaves")
			{
				this.customWaves = this.DecodeBool(this.dataS);
			}
			if (key == "data_enablefoam")
			{
				this.enableFoam = this.DecodeBool(this.dataS);
			}
			if (key == "data_enableunderdebris")
			{
				this.enableUnderDebris = this.DecodeBool(this.dataS);
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00016DB1 File Offset: 0x00014FB1
		public Color DecodeColor(string[] data)
		{
			return new Color(this.DecodeSingleFloat(data[0]), this.DecodeSingleFloat(data[1]), this.DecodeSingleFloat(data[2]), this.DecodeSingleFloat(data[3]));
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00016DDC File Offset: 0x00014FDC
		public float DecodeFloat(string[] data)
		{
			return this.DecodeSingleFloat(data[0]);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00016DE7 File Offset: 0x00014FE7
		public int DecodeInt(string[] data)
		{
			return int.Parse(data[0]);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00016DF1 File Offset: 0x00014FF1
		public bool DecodeBool(string[] data)
		{
			this.retVal = false;
			if (data[0] == "True")
			{
				this.retVal = true;
			}
			return this.retVal;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00016E16 File Offset: 0x00015016
		public float DecodeSingleFloat(string data)
		{
			return float.Parse(data, NumberStyles.Float, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00166A50 File Offset: 0x00164C50
		public string PresetEncode(string key)
		{
			this.retData = "";
			if (key == "color_depth")
			{
				this.retData = this.EncodeColor(this.depthColor);
			}
			if (key == "color_shallow")
			{
				this.retData = this.EncodeColor(this.shallowColor);
			}
			if (key == "color_blend")
			{
				this.retData = this.EncodeColor(this.blendColor);
			}
			if (key == "color_overlay")
			{
				this.retData = this.EncodeColor(this.overlayColor);
			}
			if (key == "color_caustics")
			{
				this.retData = this.EncodeColor(this.causticsColor);
			}
			if (key == "color_reflection")
			{
				this.retData = this.EncodeColor(this.reflectionColor);
			}
			if (key == "color_specular")
			{
				this.retData = this.EncodeColor(this.specularColor);
			}
			if (key == "color_sss")
			{
				this.retData = this.EncodeColor(this.sssColor);
			}
			if (key == "color_foam")
			{
				this.retData = this.EncodeColor(this.foamColor);
			}
			if (key == "color_underwater")
			{
				this.retData = this.EncodeColor(this.underwaterColor);
			}
			if (key == "data_customwaves")
			{
				this.retData = "(" + this.customWaves.ToString().Replace(" ", "") + ")";
			}
			if (key == "data_enableunderdebris")
			{
				this.retData = "(" + this.enableUnderDebris.ToString().Replace(" ", "") + ")";
			}
			if (key == "data_enablefoam")
			{
				this.retData = "(" + this.enableFoam.ToString().Replace(" ", "") + ")";
			}
			if (key == "data_beaufort")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.beaufortScale) + ")";
			}
			if (key == "data_flowdir")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.flowDirection) + ")";
			}
			if (key == "data_flowspeed")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.flowSpeed) + ")";
			}
			if (key == "data_wavescale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.waveScale) + ")";
			}
			if (key == "data_waveheight")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.waveHeight) + ")";
			}
			if (key == "data_heightprojection")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.heightProjection) + ")";
			}
			if (key == "data_turbulence")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.turbulenceFactor) + ")";
			}
			if (key == "data_lgwaveheight")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.lgWaveHeight) + ")";
			}
			if (key == "data_lgwavescale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.lgWaveScale) + ")";
			}
			if (key == "data_shorelineheight")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shorelineHeight) + ")";
			}
			if (key == "data_shorelinefreq")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shorelineFreq) + ")";
			}
			if (key == "data_shorelinescale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shorelineScale) + ")";
			}
			if (key == "data_shorelinespeed")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shorelineSpeed) + ")";
			}
			if (key == "data_shorelinenorm")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shorelineNorm) + ")";
			}
			if (key == "data_overallbright")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.overallBright) + ")";
			}
			if (key == "data_overalltransparency")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.overallTransparency) + ")";
			}
			if (key == "data_edgeamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.edgeAmt) + ")";
			}
			if (key == "data_depthamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.depthAmt) + ")";
			}
			if (key == "data_shallowamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shallowAmt) + ")";
			}
			if (key == "data_refractstrength")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.refractStrength) + ")";
			}
			if (key == "data_aberrationscale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.aberrationScale) + ")";
			}
			if (key == "data_causticsfade")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.causticsFade) + ")";
			}
			if (key == "data_reflectprojection")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.reflectProjection) + ")";
			}
			if (key == "data_reflectblur")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.reflectBlur) + ")";
			}
			if (key == "data_reflectterm")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.reflectTerm) + ")";
			}
			if (key == "data_reflectsharpen")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.reflectSharpen) + ")";
			}
			if (key == "data_roughness")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.roughness) + ")";
			}
			if (key == "data_roughness2")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.roughness2) + ")";
			}
			if (key == "data_foamscale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.foamScale) + ")";
			}
			if (key == "data_foamspeed")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.foamSpeed) + ")";
			}
			if (key == "data_edgefoamamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.edgeFoamAmt) + ")";
			}
			if (key == "data_shallowfoamamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.shallowFoamAmt) + ")";
			}
			if (key == "data_heightfoamamt")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.heightFoamAmt) + ")";
			}
			if (key == "data_hfoamheight")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.hFoamHeight) + ")";
			}
			if (key == "data_hfoamspread")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.hFoamSpread) + ")";
			}
			if (key == "data_underlightfactor")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underLightFactor) + ")";
			}
			if (key == "data_underrefractionamount")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underRefractionAmount) + ")";
			}
			if (key == "data_underrefractionscale")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underRefractionScale) + ")";
			}
			if (key == "data_underrefractionspeed")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underRefractionSpeed) + ")";
			}
			if (key == "data_underbluramount")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underBlurAmount) + ")";
			}
			if (key == "data_underwaterfogdist")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underwaterFogDist) + ")";
			}
			if (key == "data_underwaterfogspread")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underwaterFogSpread) + ")";
			}
			if (key == "data_underDarkRange")
			{
				this.retData = "(" + this.EncodeSingleFloat(this.underDarkRange) + ")";
			}
			this.retData = "<" + key + ">" + this.retData;
			return this.retData;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00016E28 File Offset: 0x00015028
		public string EncodeSingleFloat(float data)
		{
			return data.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00167430 File Offset: 0x00165630
		public string EncodeColor(Color data)
		{
			return string.Format("({0},{1},{2},{3})", new object[]
			{
				this.EncodeSingleFloat(data.r),
				this.EncodeSingleFloat(data.g),
				this.EncodeSingleFloat(data.b),
				this.EncodeSingleFloat(data.a)
			});
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00016E36 File Offset: 0x00015036
		public void SuimonoTransitionPreset(string pName, float transitionTime)
		{
			this.presetTimer = 0f;
			this.presetTransitionTime = transitionTime;
			this.presetStartTransition = true;
			this.currentPresetFolder = "Built-In Presets";
			this.currentPresetName = pName;
			this.SetTemporaryPresetData();
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00016E69 File Offset: 0x00015069
		public void SuimonoTransitionPreset(string fName, string pName, float transitionTime)
		{
			this.presetTimer = 0f;
			this.presetTransitionTime = transitionTime;
			this.presetStartTransition = true;
			this.currentPresetFolder = fName;
			this.currentPresetName = pName;
			this.SetTemporaryPresetData();
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00016E98 File Offset: 0x00015098
		public void SuimonoTransitionPreset(string fName, string pName0, string pName1, float transitionTime)
		{
			this.SuimonoSetPreset(fName, pName0);
			this.presetTimer = 0f;
			this.presetTransitionTime = transitionTime;
			this.presetStartTransition = true;
			this.currentPresetFolder = fName;
			this.currentPresetName = pName1;
			this.SetTemporaryPresetData();
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00016ED0 File Offset: 0x000150D0
		public void SuimonoTransitionPreset(string fName0, string pName0, string fName1, string pName1, float transitionTime)
		{
			this.SuimonoSetPreset(fName0, pName0);
			this.presetTimer = 0f;
			this.presetTransitionTime = transitionTime;
			this.presetStartTransition = true;
			this.currentPresetFolder = fName1;
			this.currentPresetName = pName1;
			this.SetTemporaryPresetData();
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0016748C File Offset: 0x0016568C
		private void SetTemporaryPresetData()
		{
			this.temp_depthColor = this.depthColor;
			this.temp_shallowColor = this.shallowColor;
			this.temp_blendColor = this.blendColor;
			this.temp_overlayColor = this.overlayColor;
			this.temp_causticsColor = this.causticsColor;
			this.temp_reflectionColor = this.reflectionColor;
			this.temp_specularColor = this.specularColor;
			this.temp_sssColor = this.sssColor;
			this.temp_foamColor = this.foamColor;
			this.temp_underwaterColor = this.underwaterColor;
			this.temp_beaufortScale = this.beaufortScale;
			this.temp_flowDirection = this.flowDirection;
			this.temp_flowSpeed = this.flowSpeed;
			this.temp_waveScale = this.waveScale;
			this.temp_waveHeight = this.waveHeight;
			this.temp_heightProjection = this.heightProjection;
			this.temp_turbulenceFactor = this.turbulenceFactor;
			this.temp_lgWaveHeight = this.lgWaveHeight;
			this.temp_lgWaveScale = this.lgWaveScale;
			this.temp_shorelineHeight = this.shorelineHeight;
			this.temp_shorelineFreq = this.shorelineFreq;
			this.temp_shorelineScale = this.shorelineScale;
			this.temp_shorelineSpeed = this.shorelineSpeed;
			this.temp_shorelineNorm = this.shorelineNorm;
			this.temp_overallBright = this.overallBright;
			this.temp_overallTransparency = this.overallTransparency;
			this.temp_edgeAmt = this.edgeAmt;
			this.temp_depthAmt = this.depthAmt;
			this.temp_shallowAmt = this.shallowAmt;
			this.temp_refractStrength = this.refractStrength;
			this.temp_aberrationScale = this.aberrationScale;
			this.temp_causticsFade = this.causticsFade;
			this.temp_reflectProjection = this.reflectProjection;
			this.temp_reflectBlur = this.reflectBlur;
			this.temp_reflectTerm = this.reflectTerm;
			this.temp_reflectSharpen = this.reflectSharpen;
			this.temp_roughness = this.roughness;
			this.temp_roughness2 = this.roughness2;
			this.temp_foamScale = this.foamScale;
			this.temp_foamSpeed = this.foamSpeed;
			this.temp_edgeFoamAmt = this.edgeFoamAmt;
			this.temp_shallowFoamAmt = this.shallowFoamAmt;
			this.temp_heightFoamAmt = this.heightFoamAmt;
			this.temp_hFoamHeight = this.hFoamHeight;
			this.temp_hFoamSpread = this.hFoamSpread;
			this.temp_underLightFactor = this.underLightFactor;
			this.temp_underRefractionAmount = this.underRefractionAmount;
			this.temp_underRefractionScale = this.underRefractionScale;
			this.temp_underRefractionSpeed = this.underRefractionSpeed;
			this.temp_underBlurAmount = this.underBlurAmount;
			this.temp_underwaterFogDist = this.underwaterFogDist;
			this.temp_underwaterFogSpread = this.underwaterFogSpread;
			this.temp_underDarkRange = this.underDarkRange;
		}

		// Token: 0x04002A65 RID: 10853
		public float systemTime;

		// Token: 0x04002A66 RID: 10854
		public float systemLocalTime;

		// Token: 0x04002A67 RID: 10855
		public float flowSpeed = 0.1f;

		// Token: 0x04002A68 RID: 10856
		public float flowDirection = 180f;

		// Token: 0x04002A69 RID: 10857
		public bool useBeaufortScale;

		// Token: 0x04002A6A RID: 10858
		public float beaufortScale = 1f;

		// Token: 0x04002A6B RID: 10859
		public float turbulenceFactor = 1f;

		// Token: 0x04002A6C RID: 10860
		public float waveScale = 0.5f;

		// Token: 0x04002A6D RID: 10861
		public float lgWaveHeight;

		// Token: 0x04002A6E RID: 10862
		public float lgWaveScale = 1f;

		// Token: 0x04002A6F RID: 10863
		public float waveHeight = 1f;

		// Token: 0x04002A70 RID: 10864
		public float heightProjection = 1f;

		// Token: 0x04002A71 RID: 10865
		public float useHeightProjection = 1f;

		// Token: 0x04002A72 RID: 10866
		public float refractStrength = 1f;

		// Token: 0x04002A73 RID: 10867
		public float reflectProjection = 1f;

		// Token: 0x04002A74 RID: 10868
		public float reflectBlur;

		// Token: 0x04002A75 RID: 10869
		public float aberrationScale = 0.1f;

		// Token: 0x04002A76 RID: 10870
		public float specularPower = 1f;

		// Token: 0x04002A77 RID: 10871
		public float roughness = 0.1f;

		// Token: 0x04002A78 RID: 10872
		public float roughness2 = 0.35f;

		// Token: 0x04002A79 RID: 10873
		public float reflectTerm = 0.0255f;

		// Token: 0x04002A7A RID: 10874
		public float reflectSharpen;

		// Token: 0x04002A7B RID: 10875
		public bool showDepthMask;

		// Token: 0x04002A7C RID: 10876
		public bool showWorldMask;

		// Token: 0x04002A7D RID: 10877
		public float cameraDistance = 1000f;

		// Token: 0x04002A7E RID: 10878
		public float underwaterDepth = 5f;

		// Token: 0x04002A7F RID: 10879
		public bool useDX9Settings;

		// Token: 0x04002A80 RID: 10880
		public SuimonoModule moduleObject;

		// Token: 0x04002A81 RID: 10881
		private SuimonoModuleLib suimonoModuleLibrary;

		// Token: 0x04002A82 RID: 10882
		private GameObject suimonoObject;

		// Token: 0x04002A83 RID: 10883
		private Renderer surfaceRenderer;

		// Token: 0x04002A84 RID: 10884
		private MeshFilter surfaceMesh;

		// Token: 0x04002A85 RID: 10885
		private MeshCollider surfaceCollider;

		// Token: 0x04002A86 RID: 10886
		private cameraTools surfaceReflections;

		// Token: 0x04002A87 RID: 10887
		private Suimono_DistanceBlur surfaceReflBlur;

		// Token: 0x04002A88 RID: 10888
		private GameObject scaleObject;

		// Token: 0x04002A89 RID: 10889
		private Renderer scaleRenderer;

		// Token: 0x04002A8A RID: 10890
		private MeshCollider scaleCollider;

		// Token: 0x04002A8B RID: 10891
		private MeshFilter scaleMesh;

		// Token: 0x04002A8C RID: 10892
		private Renderer surfaceVolume;

		// Token: 0x04002A8D RID: 10893
		private Material tempMaterial;

		// Token: 0x04002A8E RID: 10894
		public string suimonoVersionNumber;

		// Token: 0x04002A8F RID: 10895
		public bool showGeneral;

		// Token: 0x04002A90 RID: 10896
		public int typeIndex = 1;

		// Token: 0x04002A91 RID: 10897
		[NonSerialized]
		public List<string> typeOptions = new List<string>
		{
			"Infinite 3D Ocean",
			"3D Waves",
			"Flat Plane"
		};

		// Token: 0x04002A92 RID: 10898
		public int editorIndex = 1;

		// Token: 0x04002A93 RID: 10899
		public int editorUseIndex = 1;

		// Token: 0x04002A94 RID: 10900
		[NonSerialized]
		public List<string> editorOptions = new List<string>
		{
			"Simple",
			"Advanced"
		};

		// Token: 0x04002A95 RID: 10901
		public bool enableCustomMesh;

		// Token: 0x04002A96 RID: 10902
		public float cmScaleX = 1f;

		// Token: 0x04002A97 RID: 10903
		public float cmScaleY = 1f;

		// Token: 0x04002A98 RID: 10904
		public int lodIndex;

		// Token: 0x04002A99 RID: 10905
		public int useLodIndex;

		// Token: 0x04002A9A RID: 10906
		[NonSerialized]
		public List<string> lodOptions = new List<string>
		{
			"High Detail",
			"Medium Detail",
			"Low Detail",
			"Single Quad"
		};

		// Token: 0x04002A9B RID: 10907
		public Mesh customMesh;

		// Token: 0x04002A9C RID: 10908
		public float oceanScale = 2f;

		// Token: 0x04002A9D RID: 10909
		private bool meshWasSet;

		// Token: 0x04002A9E RID: 10910
		public bool enableCausticFX = true;

		// Token: 0x04002A9F RID: 10911
		public float causticsFade = 0.55f;

		// Token: 0x04002AA0 RID: 10912
		public Color causticsColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002AA1 RID: 10913
		public bool enableTess = true;

		// Token: 0x04002AA2 RID: 10914
		public bool useEnableTess = true;

		// Token: 0x04002AA3 RID: 10915
		public float waveTessAmt = 8f;

		// Token: 0x04002AA4 RID: 10916
		public float waveTessMin;

		// Token: 0x04002AA5 RID: 10917
		public float waveTessSpread = 0.08f;

		// Token: 0x04002AA6 RID: 10918
		public bool enableInteraction = true;

		// Token: 0x04002AA7 RID: 10919
		public float dynamicReflectFlag = 1f;

		// Token: 0x04002AA8 RID: 10920
		public bool enableReflections = true;

		// Token: 0x04002AA9 RID: 10921
		public bool enableDynamicReflections = true;

		// Token: 0x04002AAA RID: 10922
		public bool useEnableReflections = true;

		// Token: 0x04002AAB RID: 10923
		public bool useEnableDynamicReflections = true;

		// Token: 0x04002AAC RID: 10924
		public bool useReflections = true;

		// Token: 0x04002AAD RID: 10925
		public bool useDynReflections = true;

		// Token: 0x04002AAE RID: 10926
		public int reflectLayer;

		// Token: 0x04002AAF RID: 10927
		public int reflectResolution = 4;

		// Token: 0x04002AB0 RID: 10928
		public LayerMask reflectLayerMask;

		// Token: 0x04002AB1 RID: 10929
		public float reflectionDistance = 1000f;

		// Token: 0x04002AB2 RID: 10930
		[NonSerialized]
		public List<string> suiLayerMasks = new List<string>();

		// Token: 0x04002AB3 RID: 10931
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

		// Token: 0x04002AB4 RID: 10932
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

		// Token: 0x04002AB5 RID: 10933
		public int reflectFallback = 1;

		// Token: 0x04002AB6 RID: 10934
		[NonSerialized]
		public List<string> resFallbackOptions = new List<string>
		{
			"None",
			"skybox",
			"Custom Cubemap",
			"Color"
		};

		// Token: 0x04002AB7 RID: 10935
		public Texture customRefCubemap;

		// Token: 0x04002AB8 RID: 10936
		public Color customRefColor = new Color(0.9f, 0.9f, 0.9f, 1f);

		// Token: 0x04002AB9 RID: 10937
		public Color reflectionColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002ABA RID: 10938
		public bool enableCustomTextures;

		// Token: 0x04002ABB RID: 10939
		public Texture2D customTexNormal1;

		// Token: 0x04002ABC RID: 10940
		public Texture2D customTexNormal2;

		// Token: 0x04002ABD RID: 10941
		public Texture2D customTexNormal3;

		// Token: 0x04002ABE RID: 10942
		public Texture2D useTexNormal1;

		// Token: 0x04002ABF RID: 10943
		public Texture2D useTexNormal2;

		// Token: 0x04002AC0 RID: 10944
		public Texture2D useTexNormal3;

		// Token: 0x04002AC1 RID: 10945
		public bool showWaves;

		// Token: 0x04002AC2 RID: 10946
		public bool customWaves;

		// Token: 0x04002AC3 RID: 10947
		public float localTime;

		// Token: 0x04002AC4 RID: 10948
		private Vector2 flow_dir = new Vector2(0f, 0f);

		// Token: 0x04002AC5 RID: 10949
		private Vector3 tempAngle;

		// Token: 0x04002AC6 RID: 10950
		public float beaufortVal = 1f;

		// Token: 0x04002AC7 RID: 10951
		public bool showShore;

		// Token: 0x04002AC8 RID: 10952
		public float shorelineHeight = 0.75f;

		// Token: 0x04002AC9 RID: 10953
		public float shorelineFreq = 0.5f;

		// Token: 0x04002ACA RID: 10954
		public float shorelineScale = 0.15f;

		// Token: 0x04002ACB RID: 10955
		public float shorelineSpeed = 2.5f;

		// Token: 0x04002ACC RID: 10956
		public float shorelineNorm = 0.5f;

		// Token: 0x04002ACD RID: 10957
		public bool showSurface;

		// Token: 0x04002ACE RID: 10958
		public float overallBright = 1f;

		// Token: 0x04002ACF RID: 10959
		public float overallTransparency = 1f;

		// Token: 0x04002AD0 RID: 10960
		public float depthAmt = 0.1f;

		// Token: 0x04002AD1 RID: 10961
		public float shallowAmt = 0.1f;

		// Token: 0x04002AD2 RID: 10962
		public Color depthColor;

		// Token: 0x04002AD3 RID: 10963
		public Color shallowColor;

		// Token: 0x04002AD4 RID: 10964
		public float edgeAmt = 0.1f;

		// Token: 0x04002AD5 RID: 10965
		public Color specularColor;

		// Token: 0x04002AD6 RID: 10966
		public Color sssColor;

		// Token: 0x04002AD7 RID: 10967
		public Color blendColor;

		// Token: 0x04002AD8 RID: 10968
		public Color overlayColor;

		// Token: 0x04002AD9 RID: 10969
		public bool showFoam;

		// Token: 0x04002ADA RID: 10970
		public bool enableFoam = true;

		// Token: 0x04002ADB RID: 10971
		public Color foamColor = new Color(0.9f, 0.9f, 0.9f, 1f);

		// Token: 0x04002ADC RID: 10972
		public float foamScale = 40f;

		// Token: 0x04002ADD RID: 10973
		public float foamSpeed = 0.1f;

		// Token: 0x04002ADE RID: 10974
		public float edgeFoamAmt = 0.5f;

		// Token: 0x04002ADF RID: 10975
		public float shallowFoamAmt = 1f;

		// Token: 0x04002AE0 RID: 10976
		public float hFoamHeight = 1f;

		// Token: 0x04002AE1 RID: 10977
		public float hFoamSpread = 1f;

		// Token: 0x04002AE2 RID: 10978
		public float heightFoamAmt = 0.5f;

		// Token: 0x04002AE3 RID: 10979
		public bool showUnderwater;

		// Token: 0x04002AE4 RID: 10980
		public Color underwaterColor = new Color(1f, 0f, 0f, 1f);

		// Token: 0x04002AE5 RID: 10981
		public float underLightFactor = 1f;

		// Token: 0x04002AE6 RID: 10982
		public float underRefractionAmount = 0.005f;

		// Token: 0x04002AE7 RID: 10983
		public float underRefractionScale = 1.5f;

		// Token: 0x04002AE8 RID: 10984
		public float underRefractionSpeed = 0.5f;

		// Token: 0x04002AE9 RID: 10985
		public float underwaterFogDist = 20f;

		// Token: 0x04002AEA RID: 10986
		public float underwaterFogSpread;

		// Token: 0x04002AEB RID: 10987
		public bool enableUnderwater = true;

		// Token: 0x04002AEC RID: 10988
		public bool enableUnderDebris;

		// Token: 0x04002AED RID: 10989
		public float underBlurAmount = 1f;

		// Token: 0x04002AEE RID: 10990
		public float underDarkRange = 40f;

		// Token: 0x04002AEF RID: 10991
		public float setScale = 1f;

		// Token: 0x04002AF0 RID: 10992
		public Vector3 currentAngles = new Vector3(0f, 0f, 0f);

		// Token: 0x04002AF1 RID: 10993
		public Vector3 currentPosition = new Vector3(0f, 0f, 0f);

		// Token: 0x04002AF2 RID: 10994
		public Vector3 newPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04002AF3 RID: 10995
		public float spacer;

		// Token: 0x04002AF4 RID: 10996
		public float setScaleX;

		// Token: 0x04002AF5 RID: 10997
		public float setScaleZ;

		// Token: 0x04002AF6 RID: 10998
		public float offamt;

		// Token: 0x04002AF7 RID: 10999
		public Vector2 savePos = new Vector2(0f, 0f);

		// Token: 0x04002AF8 RID: 11000
		public Vector2 recPos = new Vector2(0f, 0f);

		// Token: 0x04002AF9 RID: 11001
		public Vector2 _suimono_uv = new Vector2(0f, 0f);

		// Token: 0x04002AFA RID: 11002
		public bool showSimpleEditor;

		// Token: 0x04002AFB RID: 11003
		public Shader useShader;

		// Token: 0x04002AFC RID: 11004
		public Shader currUseShader;

		// Token: 0x04002AFD RID: 11005
		public Shader shader_Surface;

		// Token: 0x04002AFE RID: 11006
		public Shader shader_Scale;

		// Token: 0x04002AFF RID: 11007
		public Shader shader_Under;

		// Token: 0x04002B00 RID: 11008
		[NonSerialized]
		public List<string> presetDirs;

		// Token: 0x04002B01 RID: 11009
		public string[] presetFiles;

		// Token: 0x04002B02 RID: 11010
		public int presetIndex = -1;

		// Token: 0x04002B03 RID: 11011
		public int presetUseIndex = -1;

		// Token: 0x04002B04 RID: 11012
		public int presetFileIndex;

		// Token: 0x04002B05 RID: 11013
		public int presetFileUseIndex;

		// Token: 0x04002B06 RID: 11014
		public string[] presetOptions;

		// Token: 0x04002B07 RID: 11015
		public bool showPresets;

		// Token: 0x04002B08 RID: 11016
		public bool presetStartTransition;

		// Token: 0x04002B09 RID: 11017
		public float presetTimer = 1f;

		// Token: 0x04002B0A RID: 11018
		public string currentPresetFolder = "Built-In Presets";

		// Token: 0x04002B0B RID: 11019
		public string currentPresetName = "";

		// Token: 0x04002B0C RID: 11020
		public int presetTransitionCurrent;

		// Token: 0x04002B0D RID: 11021
		public float presetTransitionTime = 1f;

		// Token: 0x04002B0E RID: 11022
		public int presetTransIndexFrm;

		// Token: 0x04002B0F RID: 11023
		public int presetTransIndexTo;

		// Token: 0x04002B10 RID: 11024
		public bool presetToggleSave;

		// Token: 0x04002B11 RID: 11025
		public bool presetsLoaded;

		// Token: 0x04002B12 RID: 11026
		public string[] presetDataArray;

		// Token: 0x04002B13 RID: 11027
		public string presetDataString;

		// Token: 0x04002B14 RID: 11028
		public string dir = "";

		// Token: 0x04002B15 RID: 11029
		public string baseDir = "SUIMONO - WATER SYSTEM 2/RESOURCES/";

		// Token: 0x04002B16 RID: 11030
		public string presetSaveName = "my custom preset";

		// Token: 0x04002B17 RID: 11031
		public string presetFile = "";

		// Token: 0x04002B18 RID: 11032
		public string workData;

		// Token: 0x04002B19 RID: 11033
		public string workData2;

		// Token: 0x04002B1A RID: 11034
		private Color temp_depthColor;

		// Token: 0x04002B1B RID: 11035
		private Color temp_shallowColor;

		// Token: 0x04002B1C RID: 11036
		private Color temp_blendColor;

		// Token: 0x04002B1D RID: 11037
		private Color temp_overlayColor;

		// Token: 0x04002B1E RID: 11038
		private Color temp_causticsColor;

		// Token: 0x04002B1F RID: 11039
		private Color temp_reflectionColor;

		// Token: 0x04002B20 RID: 11040
		private Color temp_specularColor;

		// Token: 0x04002B21 RID: 11041
		private Color temp_sssColor;

		// Token: 0x04002B22 RID: 11042
		private Color temp_foamColor;

		// Token: 0x04002B23 RID: 11043
		private Color temp_underwaterColor;

		// Token: 0x04002B24 RID: 11044
		private float temp_beaufortScale;

		// Token: 0x04002B25 RID: 11045
		private float temp_flowDirection;

		// Token: 0x04002B26 RID: 11046
		private float temp_flowSpeed;

		// Token: 0x04002B27 RID: 11047
		private float temp_waveScale;

		// Token: 0x04002B28 RID: 11048
		private float temp_waveHeight;

		// Token: 0x04002B29 RID: 11049
		private float temp_heightProjection;

		// Token: 0x04002B2A RID: 11050
		private float temp_turbulenceFactor;

		// Token: 0x04002B2B RID: 11051
		private float temp_lgWaveHeight;

		// Token: 0x04002B2C RID: 11052
		private float temp_lgWaveScale;

		// Token: 0x04002B2D RID: 11053
		private float temp_shorelineHeight;

		// Token: 0x04002B2E RID: 11054
		private float temp_shorelineFreq;

		// Token: 0x04002B2F RID: 11055
		private float temp_shorelineScale;

		// Token: 0x04002B30 RID: 11056
		private float temp_shorelineSpeed;

		// Token: 0x04002B31 RID: 11057
		private float temp_shorelineNorm;

		// Token: 0x04002B32 RID: 11058
		private float temp_overallBright;

		// Token: 0x04002B33 RID: 11059
		private float temp_overallTransparency;

		// Token: 0x04002B34 RID: 11060
		private float temp_edgeAmt;

		// Token: 0x04002B35 RID: 11061
		private float temp_depthAmt;

		// Token: 0x04002B36 RID: 11062
		private float temp_shallowAmt;

		// Token: 0x04002B37 RID: 11063
		private float temp_refractStrength;

		// Token: 0x04002B38 RID: 11064
		private float temp_aberrationScale;

		// Token: 0x04002B39 RID: 11065
		private float temp_causticsFade;

		// Token: 0x04002B3A RID: 11066
		private float temp_reflectProjection;

		// Token: 0x04002B3B RID: 11067
		private float temp_reflectBlur;

		// Token: 0x04002B3C RID: 11068
		private float temp_reflectTerm;

		// Token: 0x04002B3D RID: 11069
		private float temp_reflectSharpen;

		// Token: 0x04002B3E RID: 11070
		private float temp_roughness;

		// Token: 0x04002B3F RID: 11071
		private float temp_roughness2;

		// Token: 0x04002B40 RID: 11072
		private float temp_foamScale;

		// Token: 0x04002B41 RID: 11073
		private float temp_foamSpeed;

		// Token: 0x04002B42 RID: 11074
		private float temp_edgeFoamAmt;

		// Token: 0x04002B43 RID: 11075
		private float temp_shallowFoamAmt;

		// Token: 0x04002B44 RID: 11076
		private float temp_heightFoamAmt;

		// Token: 0x04002B45 RID: 11077
		private float temp_hFoamHeight;

		// Token: 0x04002B46 RID: 11078
		private float temp_hFoamSpread;

		// Token: 0x04002B47 RID: 11079
		private float temp_underLightFactor;

		// Token: 0x04002B48 RID: 11080
		private float temp_underRefractionAmount;

		// Token: 0x04002B49 RID: 11081
		private float temp_underRefractionScale;

		// Token: 0x04002B4A RID: 11082
		private float temp_underRefractionSpeed;

		// Token: 0x04002B4B RID: 11083
		private float temp_underBlurAmount;

		// Token: 0x04002B4C RID: 11084
		private float temp_underwaterFogDist;

		// Token: 0x04002B4D RID: 11085
		private float temp_underwaterFogSpread;

		// Token: 0x04002B4E RID: 11086
		private float temp_underDarkRange;

		// Token: 0x04002B4F RID: 11087
		public string materialPath;

		// Token: 0x04002B50 RID: 11088
		public float oceanUseScale;

		// Token: 0x04002B51 RID: 11089
		public float useSc;

		// Token: 0x04002B52 RID: 11090
		public Vector2 setSc;

		// Token: 0x04002B53 RID: 11091
		public Vector2 scaleOff;

		// Token: 0x04002B54 RID: 11092
		public int i;

		// Token: 0x04002B55 RID: 11093
		public string layerName;

		// Token: 0x04002B56 RID: 11094
		public Material skybox;

		// Token: 0x04002B57 RID: 11095
		[NonSerialized]
		public List<string> presetDirsArr = new List<string>();

		// Token: 0x04002B58 RID: 11096
		public int d;

		// Token: 0x04002B59 RID: 11097
		public int dn;

		// Token: 0x04002B5A RID: 11098
		[NonSerialized]
		public List<string> presetFilesArr = new List<string>();

		// Token: 0x04002B5B RID: 11099
		public string pdir;

		// Token: 0x04002B5C RID: 11100
		public FileInfo[] fileInfo;

		// Token: 0x04002B5D RID: 11101
		public int f;

		// Token: 0x04002B5E RID: 11102
		public int px;

		// Token: 0x04002B5F RID: 11103
		public int nx;

		// Token: 0x04002B60 RID: 11104
		public int ax;

		// Token: 0x04002B61 RID: 11105
		public int n;

		// Token: 0x04002B62 RID: 11106
		[NonSerialized]
		public List<string> tempPresetDirsArr = new List<string>();

		// Token: 0x04002B63 RID: 11107
		public FileInfo[] dirInfo;

		// Token: 0x04002B64 RID: 11108
		public string[] tempPresetDirs;

		// Token: 0x04002B65 RID: 11109
		[NonSerialized]
		public List<string> tempPresetFilesArr = new List<string>();

		// Token: 0x04002B66 RID: 11110
		public string[] tempPresetFiles;

		// Token: 0x04002B67 RID: 11111
		public string oldName;

		// Token: 0x04002B68 RID: 11112
		public string moveName;

		// Token: 0x04002B69 RID: 11113
		public int setNum;

		// Token: 0x04002B6A RID: 11114
		public StreamWriter sw;

		// Token: 0x04002B6B RID: 11115
		public StreamReader sr;

		// Token: 0x04002B6C RID: 11116
		public string key;

		// Token: 0x04002B6D RID: 11117
		public string dat;

		// Token: 0x04002B6E RID: 11118
		public int pFrom;

		// Token: 0x04002B6F RID: 11119
		public int pTo;

		// Token: 0x04002B70 RID: 11120
		public int dx;

		// Token: 0x04002B71 RID: 11121
		public TextAsset datFile;

		// Token: 0x04002B72 RID: 11122
		public string[] dataS;

		// Token: 0x04002B73 RID: 11123
		public string retData;

		// Token: 0x04002B74 RID: 11124
		public bool retVal;

		// Token: 0x04002B75 RID: 11125
		private float suimono_refl_off;

		// Token: 0x04002B76 RID: 11126
		private float suimono_refl_sky;

		// Token: 0x04002B77 RID: 11127
		private float suimono_refl_cube;

		// Token: 0x04002B78 RID: 11128
		private float suimono_refl_color;

		// Token: 0x04002B79 RID: 11129
		private static bool reloadData;
	}
}
