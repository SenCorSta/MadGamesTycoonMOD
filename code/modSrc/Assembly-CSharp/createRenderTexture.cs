using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DA RID: 730
public class createRenderTexture : MonoBehaviour
{
	// Token: 0x060019F3 RID: 6643 RVA: 0x000117C6 File Offset: 0x0000F9C6
	private void Start()
	{
		this.CreateNewTexture();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x000117DA File Offset: 0x0000F9DA
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

	// Token: 0x060019F5 RID: 6645 RVA: 0x00011804 File Offset: 0x0000FA04
	private void OnEnable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(true);
		}
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x0001181F File Offset: 0x0000FA1F
	private void OnDisable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(false);
		}
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x0010E4D4 File Offset: 0x0010C6D4
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

	// Token: 0x0400212E RID: 8494
	private RenderTexture rt;

	// Token: 0x0400212F RID: 8495
	private Camera camera_;

	// Token: 0x04002130 RID: 8496
	public GameObject cameraOutlineImage;

	// Token: 0x04002131 RID: 8497
	private int screenW;

	// Token: 0x04002132 RID: 8498
	private int screenH;
}
