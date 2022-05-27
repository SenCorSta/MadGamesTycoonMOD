using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[ExecuteInEditMode]
public class PostProcessing_Sobel : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x000040E8 File Offset: 0x000022E8
	private void Start()
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		this.sobelMat = new Material(Shader.Find("Nasty-Screen/SobelOutline"));
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000410C File Offset: 0x0000230C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.sobelMat.SetFloat("_ResX", (float)Screen.width * this.SobelResolution);
		this.sobelMat.SetFloat("_ResY", (float)Screen.height * this.SobelResolution);
		this.sobelMat.SetColor("_Outline", this.outlineColor);
		Graphics.Blit(source, destination, this.sobelMat);
	}

	// Token: 0x04000068 RID: 104
	private Material sobelMat;

	// Token: 0x04000069 RID: 105
	[Range(0f, 3f)]
	public float SobelResolution = 1f;

	// Token: 0x0400006A RID: 106
	public Color outlineColor;
}
