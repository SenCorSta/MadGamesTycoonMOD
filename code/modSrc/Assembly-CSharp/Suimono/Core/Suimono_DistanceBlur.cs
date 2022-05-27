using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039D RID: 925
	public class Suimono_DistanceBlur : MonoBehaviour
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x00016F09 File Offset: 0x00015109
		private void Start()
		{
			this.CreateMaterial();
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00016F11 File Offset: 0x00015111
		private void CreateMaterial()
		{
			if (this.material == null)
			{
				this.material = new Material(this.blurShader);
				this.material.hideFlags = HideFlags.DontSave;
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00016F3F File Offset: 0x0001513F
		public void OnDisable()
		{
			if (this.material)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00167D58 File Offset: 0x00165F58
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.material == null)
			{
				this.CreateMaterial();
			}
			this.iterations = Mathf.FloorToInt(Mathf.Lerp(0f, 2f, this.blurAmt));
			this.downsample = Mathf.FloorToInt(Mathf.Lerp(0f, 2f, this.blurAmt));
			this.blurSpread = Mathf.Lerp(0f, 2f, this.blurAmt);
			float num = 1f / (1f * (float)(1 << this.downsample));
			this.material.SetVector("_Parameter", new Vector4(this.blurSpread * num, -this.blurSpread * num, 0f, 0f));
			source.filterMode = FilterMode.Bilinear;
			int width = source.width >> this.downsample;
			int height = source.height >> this.downsample;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.material, 0);
			int num2 = 0;
			for (int i = 0; i < this.iterations; i++)
			{
				float num3 = (float)i * 1f;
				this.material.SetVector("_Parameter", new Vector4(this.blurAmt * num + num3, -this.blurAmt * num - num3, 0f, 0f));
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.material, 1 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.material, 2 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destination);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x04002B7A RID: 11130
		public float blurAmt;

		// Token: 0x04002B7B RID: 11131
		public int iterations = 3;

		// Token: 0x04002B7C RID: 11132
		public float blurSpread = 0.6f;

		// Token: 0x04002B7D RID: 11133
		public Shader blurShader;

		// Token: 0x04002B7E RID: 11134
		public Material material;

		// Token: 0x04002B7F RID: 11135
		private float offc;

		// Token: 0x04002B80 RID: 11136
		private float off;

		// Token: 0x04002B81 RID: 11137
		private int rtW;

		// Token: 0x04002B82 RID: 11138
		private int rtH;

		// Token: 0x04002B83 RID: 11139
		private int i;

		// Token: 0x04002B84 RID: 11140
		private RenderTexture buffer;

		// Token: 0x04002B85 RID: 11141
		private RenderTexture buffer2;

		// Token: 0x04002B86 RID: 11142
		[Range(0f, 2f)]
		public int downsample = 1;

		// Token: 0x04002B87 RID: 11143
		[Range(0f, 10f)]
		public float blurSize = 3f;
	}
}
