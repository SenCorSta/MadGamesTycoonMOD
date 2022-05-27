using System;
using System.IO;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class loadResources : MonoBehaviour
{
	// Token: 0x06001A85 RID: 6789 RVA: 0x0010B6AA File Offset: 0x001098AA
	private void Start()
	{
		this.FindScripts();
		this.LoadLogos();
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x0010B6B8 File Offset: 0x001098B8
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x0010B6F8 File Offset: 0x001098F8
	private void LoadLogos()
	{
		string text = Application.dataPath + "/Extern/CompanyLogos/";
		FileInfo[] files = new DirectoryInfo(text).GetFiles("*.png");
		this.guiMain_.logoSprites = new Sprite[files.Length];
		for (int i = 0; i < files.Length; i++)
		{
			string filePath = text + i.ToString() + ".png";
			this.guiMain_.logoSprites[i] = this.mS_.LoadPNG(filePath);
		}
	}

	// Token: 0x0400218C RID: 8588
	private mainScript mS_;

	// Token: 0x0400218D RID: 8589
	private GUI_Main guiMain_;
}
