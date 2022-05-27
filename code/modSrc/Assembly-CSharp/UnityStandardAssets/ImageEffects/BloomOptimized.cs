using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000377 RID: 887
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
	public class BloomOptimized : PostEffectsBase
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x000152D9 File Offset: 0x000134D9
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.fastBloomMaterial = base.CheckShaderAndCreateMaterial(this.fastBloomShader, this.fastBloomMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0001530F File Offset: 0x0001350F
		private void OnDisable()
		{
			if (this.fastBloomMaterial)
			{
				UnityEngine.Object.DestroyImmediate(this.fastBloomMaterial);
			}
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x001500E0 File Offset: 0x0014E2E0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int num = (this.resolution == BloomOptimized.Resolution.Low) ? 4 : 2;
			float num2 = (this.resolution == BloomOptimized.Resolution.Low) ? 0.5f : 1f;
			this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2, 0f, this.threshold, this.intensity));
			source.filterMode = FilterMode.Bilinear;
			int width = source.width / num;
			int height = source.height / num;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.fastBloomMaterial, 1);
			int num3 = (this.blurType == BloomOptimized.BlurType.Standard) ? 0 : 2;
			for (int i = 0; i < this.blurIterations; i++)
			{
				this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2 + (float)i * 1f, 0f, this.threshold, this.intensity));
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.fastBloomMaterial, 2 + num3);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.fastBloomMaterial, 3 + num3);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			this.fastBloomMaterial.SetTexture("_Bloom", renderTexture);
			Graphics.Blit(source, destination, this.fastBloomMaterial, 0);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x04002890 RID: 10384
		[Range(0f, 1.5f)]
		public float threshold = 0.25f;

		// Token: 0x04002891 RID: 10385
		[Range(0f, 2.5f)]
		public float intensity = 0.75f;

		// Token: 0x04002892 RID: 10386
		[Range(0.25f, 5.5f)]
		public float blurSize = 1f;

		// Token: 0x04002893 RID: 10387
		private BloomOptimized.Resolution resolution;

		// Token: 0x04002894 RID: 10388
		[Range(1f, 4f)]
		public int blurIterations = 1;

		// Token: 0x04002895 RID: 10389
		public BloomOptimized.BlurType blurType;

		// Token: 0x04002896 RID: 10390
		public Shader fastBloomShader;

		// Token: 0x04002897 RID: 10391
		private Material fastBloomMaterial;

		// Token: 0x02000378 RID: 888
		public enum Resolution
		{
			// Token: 0x04002899 RID: 10393
			Low,
			// Token: 0x0400289A RID: 10394
			High
		}

		// Token: 0x02000379 RID: 889
		public enum BlurType
		{
			// Token: 0x0400289C RID: 10396
			Standard,
			// Token: 0x0400289D RID: 10397
			Sgx
		}
	}
}
