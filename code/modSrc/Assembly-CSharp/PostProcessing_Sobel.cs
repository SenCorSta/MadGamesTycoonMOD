using System;
using UnityEngine;


[ExecuteInEditMode]
public class PostProcessing_Sobel : MonoBehaviour
{
	
	private void Start()
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		this.sobelMat = new Material(Shader.Find("Nasty-Screen/SobelOutline"));
	}

	
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.sobelMat.SetFloat("_ResX", (float)Screen.width * this.SobelResolution);
		this.sobelMat.SetFloat("_ResY", (float)Screen.height * this.SobelResolution);
		this.sobelMat.SetColor("_Outline", this.outlineColor);
		Graphics.Blit(source, destination, this.sobelMat);
	}

	
	private Material sobelMat;

	
	[Range(0f, 3f)]
	public float SobelResolution = 1f;

	
	public Color outlineColor;
}
