using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Suimono.Core
{
	// Token: 0x020003A2 RID: 930
	[ExecuteInEditMode]
	public class cameraTools : MonoBehaviour
	{
		// Token: 0x0600225A RID: 8794 RVA: 0x00169854 File Offset: 0x00167A54
		private void Start()
		{
			this.suimonoModuleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			if (this.cameraType != suiCamToolType.localReflection)
			{
				if (base.transform.parent != null)
				{
					this.surfaceRenderer = base.transform.parent.gameObject.GetComponent<Renderer>();
				}
			}
			else if (base.transform.parent != null)
			{
				this.surfaceRenderer = base.transform.parent.Find("Suimono_Object").gameObject.GetComponent<Renderer>();
				this.scaleRenderer = base.transform.parent.Find("Suimono_ObjectScale").gameObject.GetComponent<Renderer>();
			}
			this.cam = base.gameObject.GetComponent<Camera>();
			this.SetCopyCamera();
			this.UpdateRenderTex();
			this.CameraUpdate();
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00016FE9 File Offset: 0x000151E9
		private void OnPreRender()
		{
			if (Application.isPlaying && this.cameraType == suiCamToolType.localReflection)
			{
				GL.invertCulling = true;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00017001 File Offset: 0x00015201
		private void OnPostRender()
		{
			if (Application.isPlaying)
			{
				GL.invertCulling = false;
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00169934 File Offset: 0x00167B34
		private void Update()
		{
			if (!Application.isPlaying && this.executeInEditMode)
			{
				this.CameraUpdate();
			}
			if (this.cam != null && this.cameraType == suiCamToolType.shorelineCapture)
			{
				this.cam.cullingMask = 1 << this.suimonoModuleObject.layerDepthNum;
			}
			bool flag = false;
			int num = 0;
			if (this.cameraType != suiCamToolType.shorelineObject)
			{
				if (this.cam.transform.rotation.x == 0f)
				{
					num++;
				}
				if (this.cam.transform.rotation.y == 0f)
				{
					num++;
				}
				if (this.cam.transform.rotation.z == 0f)
				{
					num++;
				}
				if (num > 1)
				{
					flag = true;
				}
				if (flag)
				{
					Quaternion rotation = this.cam.transform.rotation;
					if (this.cam.transform.rotation.x == 0f)
					{
						rotation.x = 0.001f;
					}
					if (this.cam.transform.rotation.y == 0f)
					{
						rotation.y = 0.001f;
					}
					if (this.cam.transform.rotation.z == 0f)
					{
						rotation.z = 0.001f;
					}
					this.cam.transform.rotation = rotation;
				}
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00169AA0 File Offset: 0x00167CA0
		private void LateUpdate()
		{
			if (Application.isPlaying)
			{
				if (this.cameraType == suiCamToolType.shorelineObject)
				{
					if (this.hasStarted == 0f && Time.time > 0.2f)
					{
						this.CameraUpdate();
						this.hasStarted = 1f;
						return;
					}
				}
				else
				{
					this.CameraUpdate();
				}
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00169AF0 File Offset: 0x00167CF0
		private void SetCopyCamera()
		{
			if (this.suimonoModuleObject != null && this.suimonoModuleObject.setCamera != null)
			{
				if (this.suimonoModuleObject.setCameraComponent != null)
				{
					this.copyCam = this.suimonoModuleObject.setCameraComponent;
					return;
				}
				this.copyCam = this.suimonoModuleObject.setCamera.GetComponent<Camera>();
			}
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00169B5C File Offset: 0x00167D5C
		private void CameraRender()
		{
			if (this.cameraType == suiCamToolType.localReflection)
			{
				this.ReflectionPreRender();
			}
			this.cam.targetTexture = this.renderTexDiff;
			if (Application.isPlaying && this.cameraType == suiCamToolType.shorelineObject)
			{
				this.cam.enabled = false;
				this.cameraPos.y = 3f;
				this.cam.transform.localPosition = this.cameraPos;
				this.cam.nearClipPlane = 0.01f;
				this.cam.farClipPlane = 50f;
				this.cam.Render();
			}
			else
			{
				this.cam.enabled = true;
			}
			if (this.cameraType == suiCamToolType.localReflection)
			{
				this.ReflectionPostRender();
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00169C14 File Offset: 0x00167E14
		public void CameraUpdate()
		{
			this.SetCopyCamera();
			if (this.copyCam != null && this.cam != null)
			{
				if (this.cameraType != suiCamToolType.shorelineObject)
				{
					this.cam.transform.position = this.copyCam.transform.position;
					this.cam.transform.rotation = this.copyCam.transform.rotation;
					this.cam.projectionMatrix = this.copyCam.projectionMatrix;
					this.cam.fieldOfView = this.copyCam.fieldOfView;
				}
				if (this.cameraOffset != 0f)
				{
					this.cam.transform.Translate(Vector3.forward * this.cameraOffset);
				}
				if (this.renderType == suiCamToolRender.automatic)
				{
					this.usePath = this.copyCam.actualRenderingPath;
					if (this.cameraType == suiCamToolType.transparent)
					{
						if (this.copyCam.renderingPath == RenderingPath.Forward)
						{
							this.usePath = RenderingPath.DeferredLighting;
						}
						else
						{
							this.usePath = this.copyCam.renderingPath;
						}
					}
				}
				else if (this.renderType == suiCamToolRender.deferredShading)
				{
					this.usePath = RenderingPath.DeferredShading;
				}
				else if (this.renderType == suiCamToolRender.deferredLighting)
				{
					this.usePath = RenderingPath.DeferredLighting;
				}
				else if (this.renderType == suiCamToolRender.forward)
				{
					this.usePath = RenderingPath.Forward;
				}
				this.cam.renderingPath = this.usePath;
				if (this.renderTexDiff != null)
				{
					if (this.resolution != this.currResolution)
					{
						if (this.cameraType == suiCamToolType.shorelineObject)
						{
							this.shoreObject = base.transform.parent.gameObject.GetComponent<Suimono_ShorelineObject>();
							if (this.shoreObject != null)
							{
								this.resolution = this.shoreObject.useResolution;
							}
						}
						this.currResolution = this.resolution;
						this.UpdateRenderTex();
					}
					if (this.cameraType == suiCamToolType.normals)
					{
						if (this.suimonoModuleObject.enableAdvancedDistort)
						{
							this.cam.allowHDR = false;
							this.cam.SetReplacementShader(this.renderShader, "RenderType");
							this.CameraRender();
						}
						else
						{
							this.renderTexDiff = null;
						}
					}
					else if (this.cameraType == suiCamToolType.wakeEffects)
					{
						if (this.suimonoModuleObject.enableAdvancedDistort)
						{
							this.cam.SetReplacementShader(this.renderShader, "RenderType");
							this.CameraRender();
						}
						else
						{
							this.renderTexDiff = null;
						}
					}
					else if (this.cameraType == suiCamToolType.transparent)
					{
						if (this.suimonoModuleObject.enableTransparency)
						{
							this.CameraRender();
						}
						else
						{
							this.renderTexDiff = null;
						}
					}
					else if (this.cameraType == suiCamToolType.transparentCaustic)
					{
						if (this.suimonoModuleObject.enableCaustics)
						{
							this.CameraRender();
						}
						else
						{
							this.renderTexDiff = null;
						}
					}
					else
					{
						this.CameraRender();
					}
					if (this.cameraType == suiCamToolType.transparent)
					{
						Shader.SetGlobalTexture("_suimono_TransTex", this.renderTexDiff);
						if (!this.suimonoModuleObject.enableCausticsBlending)
						{
							Shader.SetGlobalTexture("_suimono_CausticTex", this.renderTexDiff);
						}
					}
					if (this.cameraType == suiCamToolType.transparentCaustic && this.suimonoModuleObject.enableCausticsBlending && this.suimonoModuleObject.enableCaustics)
					{
						Shader.SetGlobalTexture("_suimono_CausticTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.wakeEffects)
					{
						Shader.SetGlobalTexture("_suimono_WakeTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.normals)
					{
						Shader.SetGlobalTexture("_suimono_NormalsTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.depthMask)
					{
						Shader.SetGlobalTexture("_suimono_depthMaskTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.underwaterMask)
					{
						Shader.SetGlobalTexture("_suimono_underwaterMaskTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.underwater)
					{
						Shader.SetGlobalTexture("_suimono_underwaterTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.localReflection && this.surfaceRenderer != null)
					{
						this.surfaceRenderer.sharedMaterial.SetTexture("_ReflectionTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.shorelineObject && this.surfaceRenderer != null)
					{
						this.surfaceRenderer.sharedMaterial.SetTexture("_MainTex", this.renderTexDiff);
					}
					if (this.cameraType == suiCamToolType.shorelineCapture)
					{
						Shader.SetGlobalTexture("_suimono_shorelineTex", this.renderTexDiff);
						return;
					}
				}
				else
				{
					this.UpdateRenderTex();
				}
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0016A038 File Offset: 0x00168238
		private void UpdateRenderTex()
		{
			if (this.resolution < 4)
			{
				this.resolution = 4;
			}
			if (this.renderTexDiff != null)
			{
				if (this.cam != null)
				{
					this.cam.targetTexture = null;
				}
				UnityEngine.Object.DestroyImmediate(this.renderTexDiff);
			}
			this.renderTexDiff = new RenderTexture(this.resolution, this.resolution, 24, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
			this.renderTexDiff.dimension = TextureDimension.Tex2D;
			this.renderTexDiff.autoGenerateMips = false;
			this.renderTexDiff.anisoLevel = 1;
			this.renderTexDiff.filterMode = FilterMode.Trilinear;
			this.renderTexDiff.wrapMode = TextureWrapMode.Clamp;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0016A0E0 File Offset: 0x001682E0
		private void ReflectionPreRender()
		{
			this.pos = base.transform.parent.position;
			if (this.isUnderwater)
			{
				this.normal = -base.transform.parent.transform.up;
			}
			else
			{
				this.normal = base.transform.parent.transform.up;
			}
			this.cam.CopyFrom(this.copyCam);
			this.cam.backgroundColor = this.clearFlagColor;
			if (this.hdrMode == suiCamHdrMode.off)
			{
				this.cam.allowHDR = false;
			}
			else if (this.hdrMode == suiCamHdrMode.on)
			{
				this.cam.allowHDR = true;
			}
			if (this.isUnderwater)
			{
				this.cam.farClipPlane = 3f;
				this.cam.clearFlags = CameraClearFlags.Color;
				this.cam.depthTextureMode = DepthTextureMode.Depth;
			}
			else if (this.clearMode != suiCamClearFlags.automatic)
			{
				if (this.clearMode == suiCamClearFlags.skybox)
				{
					this.cam.clearFlags = CameraClearFlags.Skybox;
				}
				if (this.clearMode == suiCamClearFlags.color)
				{
					this.cam.clearFlags = CameraClearFlags.Color;
					this.cam.backgroundColor = this.clearFlagColor;
				}
			}
			if (this.cameraType == suiCamToolType.localReflection && this.renderShader != null)
			{
				this.cam.SetReplacementShader(this.renderShader, null);
			}
			this.cam.cullingMask = this.setLayers;
			this.d = -Vector3.Dot(this.normal, this.pos) - this.clipPlaneOffset;
			this.reflectionPlane = new Vector4(this.normal.x, this.normal.y - this.reflectionOffset, this.normal.z, this.d);
			this.reflection = Matrix4x4.zero;
			this.reflection = this.Set_CalculateReflectionMatrix(this.reflectionPlane);
			this.oldpos = this.copyCam.transform.position;
			this.newpos = this.reflection.MultiplyPoint(this.oldpos);
			this.cam.worldToCameraMatrix = this.copyCam.worldToCameraMatrix * this.reflection;
			this.clipPlane = this.Set_CameraSpacePlane(this.cam, this.pos, this.normal, 1f);
			this.projection = this.copyCam.projectionMatrix;
			this.projection = this.Set_CalculateObliqueMatrix(this.clipPlane);
			this.cam.projectionMatrix = this.projection;
			GL.invertCulling = true;
			this.cam.transform.position = this.newpos;
			this.euler = this.copyCam.transform.eulerAngles;
			this.cam.transform.eulerAngles = new Vector3(0f, this.euler.y, this.euler.z);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0016A3C0 File Offset: 0x001685C0
		private void ReflectionPostRender()
		{
			this.cam.transform.position = this.oldpos;
			GL.invertCulling = false;
			this.scaleOffset = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
			this.scale = base.transform.lossyScale;
			this.mtx = base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1f / this.scale.x, -1f / this.scale.y, 1f / this.scale.z));
			this.mtx = this.scaleOffset * this.copyCam.projectionMatrix * this.copyCam.worldToCameraMatrix * this.mtx;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00017010 File Offset: 0x00015210
		public float Set_sgn(float a)
		{
			if (a > 0f)
			{
				return 1f;
			}
			if (a < 0f)
			{
				return -1f;
			}
			return 0f;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0016A4BC File Offset: 0x001686BC
		public Vector4 Set_CameraSpacePlane(Camera cm, Vector3 pos, Vector3 normal, float sideSign)
		{
			this.offsetPos = pos + normal * this.clipPlaneOffset;
			this.m = cm.worldToCameraMatrix;
			this.cpos = this.m.MultiplyPoint(this.offsetPos);
			this.cnormal = this.m.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(this.cnormal.x, this.cnormal.y, this.cnormal.z, -Vector3.Dot(this.cpos, this.cnormal));
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0016A560 File Offset: 0x00168760
		public Matrix4x4 Set_CalculateObliqueMatrix(Vector4 clipPlane)
		{
			this.proj = this.copyCam.projectionMatrix;
			this.q = this.proj.inverse * new Vector4(this.Set_sgn(clipPlane.x), this.Set_sgn(clipPlane.y), 1f, 1f);
			this.c = clipPlane * (2f / Vector4.Dot(clipPlane, this.q));
			this.proj[2] = this.c.x - this.proj[3];
			this.proj[6] = this.c.y - this.proj[7];
			this.proj[10] = this.c.z - this.proj[11];
			this.proj[14] = this.c.w - this.proj[15];
			return this.proj;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0016A674 File Offset: 0x00168874
		public Matrix4x4 Set_CalculateReflectionMatrix(Vector4 plane)
		{
			Matrix4x4 zero = Matrix4x4.zero;
			zero.m00 = 1f - 2f * plane[0] * plane[0];
			zero.m01 = -2f * plane[0] * plane[1];
			zero.m02 = -2f * plane[0] * plane[2];
			zero.m03 = -2f * plane[3] * plane[0];
			zero.m10 = -2f * plane[1] * plane[0];
			zero.m11 = 1f - 2f * plane[1] * plane[1];
			zero.m12 = -2f * plane[1] * plane[2];
			zero.m13 = -2f * plane[3] * plane[1];
			zero.m20 = -2f * plane[2] * plane[0];
			zero.m21 = -2f * plane[2] * plane[1];
			zero.m22 = 1f - 2f * plane[2] * plane[2];
			zero.m23 = -2f * plane[3] * plane[2];
			zero.m30 = 0f;
			zero.m31 = 0f;
			zero.m32 = 0f;
			zero.m33 = 1f;
			return zero;
		}

		// Token: 0x04002BFC RID: 11260
		public suiCamToolType cameraType;

		// Token: 0x04002BFD RID: 11261
		public suiCamToolRender renderType;

		// Token: 0x04002BFE RID: 11262
		public suiCamHdrMode hdrMode;

		// Token: 0x04002BFF RID: 11263
		public suiCamClearFlags clearMode;

		// Token: 0x04002C00 RID: 11264
		public Color clearFlagColor = Color.black;

		// Token: 0x04002C01 RID: 11265
		public int resolution = 256;

		// Token: 0x04002C02 RID: 11266
		public float cameraOffset;

		// Token: 0x04002C03 RID: 11267
		public float reflectionOffset;

		// Token: 0x04002C04 RID: 11268
		public RenderTexture renderTexDiff;

		// Token: 0x04002C05 RID: 11269
		public Shader renderShader;

		// Token: 0x04002C06 RID: 11270
		public bool executeInEditMode;

		// Token: 0x04002C07 RID: 11271
		public bool isUnderwater;

		// Token: 0x04002C08 RID: 11272
		[HideInInspector]
		public Renderer surfaceRenderer;

		// Token: 0x04002C09 RID: 11273
		[HideInInspector]
		public Renderer scaleRenderer;

		// Token: 0x04002C0A RID: 11274
		[HideInInspector]
		public float reflectionDistance = 200f;

		// Token: 0x04002C0B RID: 11275
		[HideInInspector]
		public int setLayers;

		// Token: 0x04002C0C RID: 11276
		private RenderingPath usePath;

		// Token: 0x04002C0D RID: 11277
		private SuimonoModule suimonoModuleObject;

		// Token: 0x04002C0E RID: 11278
		private Camera cam;

		// Token: 0x04002C0F RID: 11279
		private Camera copyCam;

		// Token: 0x04002C10 RID: 11280
		private int currResolution = 256;

		// Token: 0x04002C11 RID: 11281
		private float clipPlaneOffset = 0.07f;

		// Token: 0x04002C12 RID: 11282
		private Vector3 pos;

		// Token: 0x04002C13 RID: 11283
		private Vector3 normal;

		// Token: 0x04002C14 RID: 11284
		private float d;

		// Token: 0x04002C15 RID: 11285
		private Vector4 reflectionPlane;

		// Token: 0x04002C16 RID: 11286
		private Matrix4x4 reflection;

		// Token: 0x04002C17 RID: 11287
		private Vector3 oldpos;

		// Token: 0x04002C18 RID: 11288
		private Vector3 newpos;

		// Token: 0x04002C19 RID: 11289
		private Vector4 clipPlane;

		// Token: 0x04002C1A RID: 11290
		private Matrix4x4 projection;

		// Token: 0x04002C1B RID: 11291
		private Vector3 euler;

		// Token: 0x04002C1C RID: 11292
		private Matrix4x4 scaleOffset;

		// Token: 0x04002C1D RID: 11293
		private Vector3 scale;

		// Token: 0x04002C1E RID: 11294
		private Matrix4x4 mtx;

		// Token: 0x04002C1F RID: 11295
		private Vector3 offsetPos;

		// Token: 0x04002C20 RID: 11296
		private Matrix4x4 m;

		// Token: 0x04002C21 RID: 11297
		private Vector3 cpos;

		// Token: 0x04002C22 RID: 11298
		private Vector3 cnormal;

		// Token: 0x04002C23 RID: 11299
		private Matrix4x4 proj;

		// Token: 0x04002C24 RID: 11300
		private Vector4 q;

		// Token: 0x04002C25 RID: 11301
		private Vector4 c;

		// Token: 0x04002C26 RID: 11302
		private float hasStarted;

		// Token: 0x04002C27 RID: 11303
		private Vector3 cameraPos = Vector3.zero;

		// Token: 0x04002C28 RID: 11304
		private Suimono_ShorelineObject shoreObject;
	}
}
