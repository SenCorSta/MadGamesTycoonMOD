using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DD RID: 733
public class createRenderTexture : MonoBehaviour
{
	// Token: 0x06001A3D RID: 6717 RVA: 0x0010A2F7 File Offset: 0x001084F7
	private void Start()
	{
		this.CreateNewTexture();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x0010A30B File Offset: 0x0010850B
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

	// Token: 0x06001A3F RID: 6719 RVA: 0x0010A335 File Offset: 0x00108535
	private void OnEnable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(true);
		}
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x0010A350 File Offset: 0x00108550
	private void OnDisable()
	{
		if (this.cameraOutlineImage)
		{
			this.cameraOutlineImage.SetActive(false);
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x0010A36C File Offset: 0x0010856C
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

	// Token: 0x04002148 RID: 8520
	private RenderTexture rt;

	// Token: 0x04002149 RID: 8521
	private Camera camera_;

	// Token: 0x0400214A RID: 8522
	public GameObject cameraOutlineImage;

	// Token: 0x0400214B RID: 8523
	private int screenW;

	// Token: 0x0400214C RID: 8524
	private int screenH;
}
