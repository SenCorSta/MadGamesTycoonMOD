using System;
using UnityEngine;

namespace Suimono.Core
{
	
	public class Suimono_DistanceBlur : MonoBehaviour
	{
		
		private void Start()
		{
			this.CreateMaterial();
		}

		
		private void CreateMaterial()
		{
			if (this.material == null)
			{
				this.material = new Material(this.blurShader);
				this.material.hideFlags = HideFlags.DontSave;
			}
		}

		
		public void OnDisable()
		{
			if (this.material)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		
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

		
		public float blurAmt;

		
		public int iterations = 3;

		
		public float blurSpread = 0.6f;

		
		public Shader blurShader;

		
		public Material material;

		
		private float offc;

		
		private float off;

		
		private int rtW;

		
		private int rtH;

		
		private int i;

		
		private RenderTexture buffer;

		
		private RenderTexture buffer2;

		
		[Range(0f, 2f)]
		public int downsample = 1;

		
		[Range(0f, 10f)]
		public float blurSize = 3f;
	}
}
