﻿using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039F RID: 927
	[AddComponentMenu("Image Effects/Suimono/UnderwaterFX")]
	public class Suimono_UnderwaterFog : MonoBehaviour
	{
		// Token: 0x0600224E RID: 8782 RVA: 0x001689E0 File Offset: 0x00166BE0
		private void Start()
		{
			this.cam = base.gameObject.GetComponent<Camera>();
			this.camtr = this.cam.transform;
			if (GameObject.Find("SUIMONO_Module") != null)
			{
				this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				this.moduleLibrary = (SuimonoModuleLib)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModuleLib));
			}
			if (this.moduleLibrary != null)
			{
				this.distortTex = this.moduleLibrary.texNormalC;
				this.mask2Tex = this.moduleLibrary.texDrops;
			}
			this.randSeed = Environment.TickCount;
			this.dropRand = new Suimono.Core.Random(this.randSeed);
			this.fogShader = Shader.Find("Hidden/SuimonoUnderwaterFog");
			this.fogMaterial = new Material(this.fogShader);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00168AC4 File Offset: 0x00166CC4
		private void LateUpdate()
		{
			if (this.dropRand == null)
			{
				this.dropRand = new Suimono.Core.Random(this.randSeed);
			}
			this._deltaTime = Time.deltaTime;
			if (this.cancelTransition)
			{
				this.doTransition = false;
				this.cancelTransition = false;
				this.trans1Time = 1.1f;
				this.trans2Time = 1.1f;
			}
			if (this.doTransition)
			{
				this.doTransition = false;
				this.trans1Time = 0f;
				this.trans2Time = 0f;
				this.dropOff = new Vector2(this.dropRand.Next(0f, 1f), this.dropRand.Next(0f, 1f));
			}
			this.trans1Time += this._deltaTime * 0.7f * this.wipeTime;
			this.trans2Time += this._deltaTime * 0.1f * this.dropsTime;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00168BBC File Offset: 0x00166DBC
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination);
			this.frustumCorners = Matrix4x4.identity;
			this.fovWHalf = this.cam.fieldOfView * 0.5f;
			this.toRight = this.camtr.right * this.cam.nearClipPlane * Mathf.Tan(this.fovWHalf * 0.017453292f) * this.cam.aspect;
			this.toTop = this.camtr.up * this.cam.nearClipPlane * Mathf.Tan(this.fovWHalf * 0.017453292f);
			this.topLeft = this.camtr.forward * this.cam.nearClipPlane - this.toRight + this.toTop;
			this.camScale = this.topLeft.magnitude * this.cam.farClipPlane / this.cam.nearClipPlane;
			this.topLeft.Normalize();
			this.topLeft *= this.camScale;
			this.topRight = this.camtr.forward * this.cam.nearClipPlane + this.toRight + this.toTop;
			this.topRight.Normalize();
			this.topRight *= this.camScale;
			this.bottomRight = this.camtr.forward * this.cam.nearClipPlane + this.toRight - this.toTop;
			this.bottomRight.Normalize();
			this.bottomRight *= this.camScale;
			this.bottomLeft = this.camtr.forward * this.cam.nearClipPlane - this.toRight - this.toTop;
			this.bottomLeft.Normalize();
			this.bottomLeft *= this.camScale;
			this.frustumCorners.SetRow(0, this.topLeft);
			this.frustumCorners.SetRow(1, this.topRight);
			this.frustumCorners.SetRow(2, this.bottomRight);
			this.frustumCorners.SetRow(3, this.bottomLeft);
			if (this.heightFog && base.transform.parent != null)
			{
				this.height = base.transform.parent.transform.position.y + 1f;
				this.heightDensity = 2f;
			}
			this.camPos = this.camtr.position;
			this.FdotC = this.camPos.y - this.height;
			this.paramK = ((this.FdotC <= 0f) ? 1f : 0f);
			this.sceneStart = this.fogStart;
			this.sceneEnd = this.fogEnd;
			this.diff = this.sceneEnd - this.sceneStart;
			this.invDiff = ((Mathf.Abs(this.diff) > 0.0001f) ? (1f / this.diff) : 0f);
			this.sceneParams.x = 0f;
			this.sceneParams.y = 0f;
			this.sceneParams.z = -this.invDiff;
			this.sceneParams.w = this.sceneEnd * this.invDiff;
			if (this.fogMaterial != null)
			{
				this.fogMaterial.SetMatrix("_FrustumCornersWS", this.frustumCorners);
				this.fogMaterial.SetVector("_CameraWS", this.camPos);
				this.fogMaterial.SetVector("_HeightParams", new Vector4(this.height, this.FdotC, this.paramK, this.heightDensity * 0.5f));
				this.fogMaterial.SetVector("_DistanceParams", new Vector4(-Mathf.Max(this.startDistance, 0f), 0f, 0f, 0f));
				this.fogMaterial.SetVector("_SceneFogParams", this.sceneParams);
				this.fogMaterial.SetVector("_SceneFogMode", new Vector4(1f, this.useRadialDistance ? 1f : 0f, 0f, 0f));
				this.fogMaterial.SetColor("_underwaterColor", this.underwaterColor);
				if (this.distortTex != null)
				{
					this.fogMaterial.SetTexture("_underwaterDistort", this.distortTex);
					this.fogMaterial.SetFloat("_distortAmt", this.refractAmt);
					this.fogMaterial.SetFloat("_distortSpeed", this.refractSpd);
					this.fogMaterial.SetFloat("_distortScale", this.refractScale);
					this.fogMaterial.SetFloat("_lightFactor", this.lightFactor);
				}
				if (this.distortTex != null)
				{
					this.fogMaterial.SetTexture("_distort1Mask", this.distortTex);
				}
				if (this.mask2Tex != null)
				{
					this.fogMaterial.SetTexture("_distort2Mask", this.mask2Tex);
				}
				this.fogMaterial.SetFloat("_trans1", this.trans1Time);
				this.fogMaterial.SetFloat("_trans2", this.trans2Time);
				this.fogMaterial.SetFloat("_transStrength", this.transitionStrength);
				this.fogMaterial.SetFloat("_dropOffx", this.dropOff.x);
				this.fogMaterial.SetFloat("_dropOffy", this.dropOff.y);
				this.fogMaterial.SetFloat("_showScreenMask", this.showScreenMask ? 1f : 0f);
				this.blurSpread = Mathf.Clamp01(this.blurSpread);
				this.fogMaterial.SetFloat("_blur", this.blurSpread);
				if (this.moduleObject != null)
				{
					if (this.moduleObject.setTrack != null)
					{
						this.trackobject = this.moduleObject.setTrack.transform;
					}
					else if (this.moduleObject.setCamera != null)
					{
						this.trackobject = this.moduleObject.setCamera.transform;
					}
					if (this.trackobject != null)
					{
						this.hFac = Mathf.Clamp(11.5f - this.trackobject.localPosition.y, 0f, 500f);
					}
					this.heightDepth = this.hFac;
					this.hFac = Mathf.Clamp01(Mathf.Lerp(-0.2f, 1f, Mathf.Clamp01(this.hFac / this.darkRange)));
					this.fogMaterial.SetFloat("_hDepth", this.hFac);
					this.fogMaterial.SetFloat("_enableUnderwater", this.moduleObject.enableUnderwaterFX ? 1f : 0f);
				}
				this.rtW = source.width / 4;
				this.rtH = source.height / 4;
				this.buffer = RenderTexture.GetTemporary(this.rtW, this.rtH, 0);
				this.DownSample4x(source, this.buffer);
				this.i = 0;
				while (this.i < this.iterations)
				{
					this.buffer2 = RenderTexture.GetTemporary(this.rtW, this.rtH, 0);
					this.FourTapCone(this.buffer, this.buffer2, this.i);
					RenderTexture.ReleaseTemporary(this.buffer);
					this.buffer = this.buffer2;
					this.i++;
				}
				Graphics.Blit(this.buffer, destination);
				RenderTexture.ReleaseTemporary(this.buffer);
				this.pass = 0;
				if (this.distanceFog && this.heightFog)
				{
					this.pass = 0;
				}
				else if (this.distanceFog)
				{
					this.pass = 1;
				}
				else
				{
					this.pass = 2;
				}
				this.CustomGraphicsBlit(source, destination, this.fogMaterial, this.pass);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00169448 File Offset: 0x00167648
		private void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
		{
			RenderTexture.active = dest;
			fxMaterial.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			fxMaterial.SetPass(passNr);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0016951C File Offset: 0x0016771C
		private void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
		{
			this.offc = 0.5f + (float)iteration * this.blurSpread * 2f;
			Graphics.BlitMultiTap(source, dest, this.fogMaterial, new Vector2[]
			{
				new Vector2(-this.offc, -this.offc),
				new Vector2(-this.offc, this.offc),
				new Vector2(this.offc, this.offc),
				new Vector2(this.offc, -this.offc)
			});
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x001695BC File Offset: 0x001677BC
		private void DownSample4x(RenderTexture source, RenderTexture dest)
		{
			this.off = 1f;
			Graphics.BlitMultiTap(source, dest, this.fogMaterial, new Vector2[]
			{
				new Vector2(-this.off, -this.off),
				new Vector2(-this.off, this.off),
				new Vector2(this.off, this.off),
				new Vector2(this.off, -this.off)
			});
		}

		// Token: 0x04002BB3 RID: 11187
		public bool showScreenMask;

		// Token: 0x04002BB4 RID: 11188
		public bool doTransition;

		// Token: 0x04002BB5 RID: 11189
		public bool cancelTransition;

		// Token: 0x04002BB6 RID: 11190
		public bool useUnderSurfaceView;

		// Token: 0x04002BB7 RID: 11191
		public bool distanceFog = true;

		// Token: 0x04002BB8 RID: 11192
		public bool useRadialDistance = true;

		// Token: 0x04002BB9 RID: 11193
		public bool heightFog;

		// Token: 0x04002BBA RID: 11194
		public float height = 1f;

		// Token: 0x04002BBB RID: 11195
		public float heightDensity = 2f;

		// Token: 0x04002BBC RID: 11196
		public float startDistance;

		// Token: 0x04002BBD RID: 11197
		public float fogStart;

		// Token: 0x04002BBE RID: 11198
		public float fogEnd = 20f;

		// Token: 0x04002BBF RID: 11199
		public float refractAmt = 0.005f;

		// Token: 0x04002BC0 RID: 11200
		public float refractSpd = 1.5f;

		// Token: 0x04002BC1 RID: 11201
		public float refractScale = 0.5f;

		// Token: 0x04002BC2 RID: 11202
		public float lightFactor = 1f;

		// Token: 0x04002BC3 RID: 11203
		public Color underwaterColor;

		// Token: 0x04002BC4 RID: 11204
		public float dropsTime = 2f;

		// Token: 0x04002BC5 RID: 11205
		public float wipeTime = 1f;

		// Token: 0x04002BC6 RID: 11206
		public float transitionStrength = 1f;

		// Token: 0x04002BC7 RID: 11207
		public int iterations = 2;

		// Token: 0x04002BC8 RID: 11208
		public float blurSpread = 1f;

		// Token: 0x04002BC9 RID: 11209
		public float darkRange = 40f;

		// Token: 0x04002BCA RID: 11210
		public float heightDepth = 1f;

		// Token: 0x04002BCB RID: 11211
		public float hFac;

		// Token: 0x04002BCC RID: 11212
		public Texture distortTex;

		// Token: 0x04002BCD RID: 11213
		public Texture mask2Tex;

		// Token: 0x04002BCE RID: 11214
		public Shader fogShader;

		// Token: 0x04002BCF RID: 11215
		public Material fogMaterial;

		// Token: 0x04002BD0 RID: 11216
		private SuimonoModule moduleObject;

		// Token: 0x04002BD1 RID: 11217
		private SuimonoModuleLib moduleLibrary;

		// Token: 0x04002BD2 RID: 11218
		private float trans1Time = 1.1f;

		// Token: 0x04002BD3 RID: 11219
		private float trans2Time = 1.1f;

		// Token: 0x04002BD4 RID: 11220
		private int randSeed;

		// Token: 0x04002BD5 RID: 11221
		private Suimono.Core.Random dropRand;

		// Token: 0x04002BD6 RID: 11222
		private Vector2 dropOff;

		// Token: 0x04002BD7 RID: 11223
		private Camera cam;

		// Token: 0x04002BD8 RID: 11224
		private Transform camtr;

		// Token: 0x04002BD9 RID: 11225
		private int pass;

		// Token: 0x04002BDA RID: 11226
		private int rtW;

		// Token: 0x04002BDB RID: 11227
		private int rtH;

		// Token: 0x04002BDC RID: 11228
		private RenderTexture buffer;

		// Token: 0x04002BDD RID: 11229
		private int i;

		// Token: 0x04002BDE RID: 11230
		private RenderTexture buffer2;

		// Token: 0x04002BDF RID: 11231
		private Vector3 camPos;

		// Token: 0x04002BE0 RID: 11232
		private float FdotC;

		// Token: 0x04002BE1 RID: 11233
		private float paramK;

		// Token: 0x04002BE2 RID: 11234
		private float sceneStart;

		// Token: 0x04002BE3 RID: 11235
		private float sceneEnd;

		// Token: 0x04002BE4 RID: 11236
		private Vector4 sceneParams;

		// Token: 0x04002BE5 RID: 11237
		private float diff;

		// Token: 0x04002BE6 RID: 11238
		private float invDiff;

		// Token: 0x04002BE7 RID: 11239
		private Matrix4x4 frustumCorners;

		// Token: 0x04002BE8 RID: 11240
		private float fovWHalf;

		// Token: 0x04002BE9 RID: 11241
		private Vector3 toRight;

		// Token: 0x04002BEA RID: 11242
		private Vector3 toTop;

		// Token: 0x04002BEB RID: 11243
		private Vector3 topLeft;

		// Token: 0x04002BEC RID: 11244
		private float camScale;

		// Token: 0x04002BED RID: 11245
		private Vector3 topRight;

		// Token: 0x04002BEE RID: 11246
		private Vector3 bottomRight;

		// Token: 0x04002BEF RID: 11247
		private Vector3 bottomLeft;

		// Token: 0x04002BF0 RID: 11248
		private float offc;

		// Token: 0x04002BF1 RID: 11249
		private float off;

		// Token: 0x04002BF2 RID: 11250
		private Transform trackobject;

		// Token: 0x04002BF3 RID: 11251
		private float _deltaTime;
	}
}
