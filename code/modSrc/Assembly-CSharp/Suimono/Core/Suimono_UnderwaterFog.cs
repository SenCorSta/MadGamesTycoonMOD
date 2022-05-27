using System;
using UnityEngine;

namespace Suimono.Core
{
	
	[AddComponentMenu("Image Effects/Suimono/UnderwaterFX")]
	public class Suimono_UnderwaterFog : MonoBehaviour
	{
		
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

		
		public bool showScreenMask;

		
		public bool doTransition;

		
		public bool cancelTransition;

		
		public bool useUnderSurfaceView;

		
		public bool distanceFog = true;

		
		public bool useRadialDistance = true;

		
		public bool heightFog;

		
		public float height = 1f;

		
		public float heightDensity = 2f;

		
		public float startDistance;

		
		public float fogStart;

		
		public float fogEnd = 20f;

		
		public float refractAmt = 0.005f;

		
		public float refractSpd = 1.5f;

		
		public float refractScale = 0.5f;

		
		public float lightFactor = 1f;

		
		public Color underwaterColor;

		
		public float dropsTime = 2f;

		
		public float wipeTime = 1f;

		
		public float transitionStrength = 1f;

		
		public int iterations = 2;

		
		public float blurSpread = 1f;

		
		public float darkRange = 40f;

		
		public float heightDepth = 1f;

		
		public float hFac;

		
		public Texture distortTex;

		
		public Texture mask2Tex;

		
		public Shader fogShader;

		
		public Material fogMaterial;

		
		private SuimonoModule moduleObject;

		
		private SuimonoModuleLib moduleLibrary;

		
		private float trans1Time = 1.1f;

		
		private float trans2Time = 1.1f;

		
		private int randSeed;

		
		private Suimono.Core.Random dropRand;

		
		private Vector2 dropOff;

		
		private Camera cam;

		
		private Transform camtr;

		
		private int pass;

		
		private int rtW;

		
		private int rtH;

		
		private RenderTexture buffer;

		
		private int i;

		
		private RenderTexture buffer2;

		
		private Vector3 camPos;

		
		private float FdotC;

		
		private float paramK;

		
		private float sceneStart;

		
		private float sceneEnd;

		
		private Vector4 sceneParams;

		
		private float diff;

		
		private float invDiff;

		
		private Matrix4x4 frustumCorners;

		
		private float fovWHalf;

		
		private Vector3 toRight;

		
		private Vector3 toTop;

		
		private Vector3 topLeft;

		
		private float camScale;

		
		private Vector3 topRight;

		
		private Vector3 bottomRight;

		
		private Vector3 bottomLeft;

		
		private float offc;

		
		private float off;

		
		private Transform trackobject;

		
		private float _deltaTime;
	}
}
