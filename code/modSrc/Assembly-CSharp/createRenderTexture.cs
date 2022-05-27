using System;
using UnityEngine;
using UnityEngine.UI;


public class createRenderTexture : MonoBehaviour
{
	
	private void Start()
	{
		this.CreateNewTexture();
		base.gameObject.SetActive(false);
	}

	
	private void Update()
	{
		if (this.screenW != Screen.width)
		{
			this.CreateNewTexture();
			return;
		}
		if (this.screenH != Screen.height)
		{
			this.CreateNewTexture();
			return;
		}
	}

	
	private void OnEnable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(true);
		}
	}

	
	private void OnDisable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(false);
		}
	}

	
	private void CreateNewTexture()
	{
		this.rt = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
		this.rt.Create();
		this.camera_ = base.GetComponent<Camera>();
		this.camera_.targetTexture = this.rt;
		this.cameraOutlineImage.GetComponent<RawImage>().texture = this.rt;
		this.screenW = Screen.width;
		this.screenH = Screen.height;
	}

	
	private RenderTexture rt;

	
	private Camera camera_;

	
	public GameObject cameraOutlineImage;

	
	private int screenW;

	
	private int screenH;
}
