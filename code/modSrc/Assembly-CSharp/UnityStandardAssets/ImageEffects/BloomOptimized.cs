using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200037A RID: 890
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
	public class BloomOptimized : PostEffectsBase
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x0014FAEB File Offset: 0x0014DCEB
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

		// Token: 0x06002054 RID: 8276 RVA: 0x0014FB21 File Offset: 0x0014DD21
		private void OnDisable()
		{
			if (this.fastBloomMaterial)
			{
				UnityEngine.Object.DestroyImmediate(this.fastBloomMaterial);
			}
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0014FB3C File Offset: 0x0014DD3C
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

		// Token: 0x040028A6 RID: 10406
		[Range(0f, 1.5f)]
		public float threshold = 0.25f;

		// Token: 0x040028A7 RID: 10407
		[Range(0f, 2.5f)]
		public float intensity = 0.75f;

		// Token: 0x040028A8 RID: 10408
		[Range(0.25f, 5.5f)]
		public float blurSize = 1f;

		// Token: 0x040028A9 RID: 10409
		private BloomOptimized.Resolution resolution;

		// Token: 0x040028AA RID: 10410
		[Range(1f, 4f)]
		public int blurIterations = 1;

		// Token: 0x040028AB RID: 10411
		public BloomOptimized.BlurType blurType;

		// Token: 0x040028AC RID: 10412
		public Shader fastBloomShader;

		// Token: 0x040028AD RID: 10413
		private Material fastBloomMaterial;

		// Token: 0x0200037B RID: 891
		public enum Resolution
		{
			// Token: 0x040028AF RID: 10415
			Low,
			// Token: 0x040028B0 RID: 10416
			High
		}

		// Token: 0x0200037C RID: 892
		public enum BlurType
		{
			// Token: 0x040028B2 RID: 10418
			Standard,
			// Token: 0x040028B3 RID: 10419
			Sgx
		}
	}
}
