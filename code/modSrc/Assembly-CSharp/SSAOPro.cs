using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[ImageEffectAllowedInSceneView]
[HelpURL("http://www.thomashourdel.com/ssaopro/doc/")]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/SSAO Pro")]
[RequireComponent(typeof(Camera))]
public class SSAOPro : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000076 RID: 118 RVA: 0x00002556 File Offset: 0x00000756
	public Material Material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.ShaderSSAO)
				{
					hideFlags = HideFlags.HideAndDontSave
				};
			}
			return this.m_Material;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000077 RID: 119 RVA: 0x00002585 File Offset: 0x00000785
	public Shader ShaderSSAO
	{
		get
		{
			if (this.m_ShaderSSAO == null)
			{
				this.m_ShaderSSAO = Shader.Find("Hidden/SSAO Pro V2");
			}
			return this.m_ShaderSSAO;
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0001AA04 File Offset: 0x00018C04
	private void OnEnable()
	{
		this.m_Camera = base.GetComponent<Camera>();
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarning("Image Effects are not supported on this device.");
			base.enabled = false;
			return;
		}
		if (this.ShaderSSAO == null)
		{
			Debug.LogWarning("Missing shader (SSAO).");
			base.enabled = false;
			return;
		}
		if (!this.ShaderSSAO.isSupported)
		{
			Debug.LogWarning("Unsupported shader (SSAO).");
			base.enabled = false;
			return;
		}
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			Debug.LogWarning("Depth textures aren't supported on this device.");
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000025AB File Offset: 0x000007AB
	private void OnPreRender()
	{
		this.m_Camera.depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.DepthNormals);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000025C0 File Offset: 0x000007C0
	private void OnDisable()
	{
		if (this.m_Material != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
		this.m_Material = null;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0001AA90 File Offset: 0x00018C90
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.ShaderSSAO == null || Mathf.Approximately(this.Intensity, 0f))
		{
			Graphics.Blit(source, destination);
			return;
		}
		this.Material.shaderKeywords = null;
		switch (this.Samples)
		{
		case SSAOPro.SampleCount.Low:
			this.Material.EnableKeyword("SAMPLES_LOW");
			break;
		case SSAOPro.SampleCount.Medium:
			this.Material.EnableKeyword("SAMPLES_MEDIUM");
			break;
		case SSAOPro.SampleCount.High:
			this.Material.EnableKeyword("SAMPLES_HIGH");
			break;
		case SSAOPro.SampleCount.Ultra:
			this.Material.EnableKeyword("SAMPLES_ULTRA");
			break;
		}
		int num = 0;
		if (this.NoiseTexture != null)
		{
			num = 1;
		}
		if (!Mathf.Approximately(this.LumContribution, 0f))
		{
			num += 2;
		}
		num++;
		this.Material.SetMatrix("_InverseViewProject", (this.m_Camera.projectionMatrix * this.m_Camera.worldToCameraMatrix).inverse);
		this.Material.SetMatrix("_CameraModelView", this.m_Camera.cameraToWorldMatrix);
		this.Material.SetTexture("_NoiseTex", this.NoiseTexture);
		this.Material.SetVector("_Params1", new Vector4((this.NoiseTexture == null) ? 0f : ((float)this.NoiseTexture.width), this.Radius, this.Intensity, this.Distance));
		this.Material.SetVector("_Params2", new Vector4(this.Bias, this.LumContribution, this.CutoffDistance, this.CutoffFalloff));
		this.Material.SetColor("_OcclusionColor", this.OcclusionColor);
		if (this.Blur != SSAOPro.BlurMode.None)
		{
			SSAOPro.Pass pass = (this.Blur == SSAOPro.BlurMode.HighQualityBilateral) ? SSAOPro.Pass.HighQualityBilateralBlur : SSAOPro.Pass.GaussianBlur;
			int num2 = this.BlurDownsampling ? this.Downsampling : 1;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.ARGB32);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / this.Downsampling, source.height / this.Downsampling, 0, RenderTextureFormat.ARGB32);
			Graphics.Blit(temporary, temporary, this.Material, 0);
			Graphics.Blit(source, temporary, this.Material, num);
			this.Material.SetFloat("_BilateralThreshold", this.BlurBilateralThreshold * 5f);
			for (int i = 0; i < this.BlurPasses; i++)
			{
				this.Material.SetVector("_Direction", new Vector2(1f / (float)source.width, 0f));
				Graphics.Blit(temporary, temporary2, this.Material, (int)pass);
				temporary.DiscardContents();
				this.Material.SetVector("_Direction", new Vector2(0f, 1f / (float)source.height));
				Graphics.Blit(temporary2, temporary, this.Material, (int)pass);
				temporary2.DiscardContents();
			}
			if (!this.DebugAO)
			{
				this.Material.SetTexture("_SSAOTex", temporary);
				Graphics.Blit(source, destination, this.Material, 7);
			}
			else
			{
				Graphics.Blit(temporary, destination);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			return;
		}
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / this.Downsampling, source.height / this.Downsampling, 0, RenderTextureFormat.ARGB32);
		Graphics.Blit(temporary3, temporary3, this.Material, 0);
		if (this.DebugAO)
		{
			Graphics.Blit(source, temporary3, this.Material, num);
			Graphics.Blit(temporary3, destination);
			RenderTexture.ReleaseTemporary(temporary3);
			return;
		}
		Graphics.Blit(source, temporary3, this.Material, num);
		this.Material.SetTexture("_SSAOTex", temporary3);
		Graphics.Blit(source, destination, this.Material, 7);
		RenderTexture.ReleaseTemporary(temporary3);
	}

	// Token: 0x0400006B RID: 107
	public Texture2D NoiseTexture;

	// Token: 0x0400006C RID: 108
	public bool UseHighPrecisionDepthMap;

	// Token: 0x0400006D RID: 109
	public SSAOPro.SampleCount Samples = SSAOPro.SampleCount.Medium;

	// Token: 0x0400006E RID: 110
	[Range(1f, 4f)]
	public int Downsampling = 1;

	// Token: 0x0400006F RID: 111
	[Range(0.01f, 1.25f)]
	public float Radius = 0.12f;

	// Token: 0x04000070 RID: 112
	[Range(0f, 16f)]
	public float Intensity = 2.5f;

	// Token: 0x04000071 RID: 113
	[Range(0f, 10f)]
	public float Distance = 1f;

	// Token: 0x04000072 RID: 114
	[Range(0f, 1f)]
	public float Bias = 0.1f;

	// Token: 0x04000073 RID: 115
	[Range(0f, 1f)]
	public float LumContribution = 0.5f;

	// Token: 0x04000074 RID: 116
	[ColorUsage(false)]
	public Color OcclusionColor = Color.black;

	// Token: 0x04000075 RID: 117
	public float CutoffDistance = 150f;

	// Token: 0x04000076 RID: 118
	public float CutoffFalloff = 50f;

	// Token: 0x04000077 RID: 119
	public SSAOPro.BlurMode Blur = SSAOPro.BlurMode.HighQualityBilateral;

	// Token: 0x04000078 RID: 120
	public bool BlurDownsampling;

	// Token: 0x04000079 RID: 121
	[Range(1f, 4f)]
	public int BlurPasses = 1;

	// Token: 0x0400007A RID: 122
	[Range(1f, 20f)]
	public float BlurBilateralThreshold = 10f;

	// Token: 0x0400007B RID: 123
	public bool DebugAO;

	// Token: 0x0400007C RID: 124
	protected Shader m_ShaderSSAO;

	// Token: 0x0400007D RID: 125
	protected Material m_Material;

	// Token: 0x0400007E RID: 126
	protected Camera m_Camera;

	// Token: 0x02000019 RID: 25
	public enum BlurMode
	{
		// Token: 0x04000080 RID: 128
		None,
		// Token: 0x04000081 RID: 129
		Gaussian,
		// Token: 0x04000082 RID: 130
		HighQualityBilateral
	}

	// Token: 0x0200001A RID: 26
	public enum SampleCount
	{
		// Token: 0x04000084 RID: 132
		VeryLow,
		// Token: 0x04000085 RID: 133
		Low,
		// Token: 0x04000086 RID: 134
		Medium,
		// Token: 0x04000087 RID: 135
		High,
		// Token: 0x04000088 RID: 136
		Ultra
	}

	// Token: 0x0200001B RID: 27
	protected enum Pass
	{
		// Token: 0x0400008A RID: 138
		Clear,
		// Token: 0x0400008B RID: 139
		GaussianBlur = 5,
		// Token: 0x0400008C RID: 140
		HighQualityBilateralBlur,
		// Token: 0x0400008D RID: 141
		Composite
	}
}
