using System;
using System.IO;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class loadResources : MonoBehaviour
{
	// Token: 0x06001A3B RID: 6715 RVA: 0x00011A74 File Offset: 0x0000FC74
	private void Start()
	{
		this.FindScripts();
		this.LoadLogos();
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x00011A82 File Offset: 0x0000FC82
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

	// Token: 0x06001A3D RID: 6717 RVA: 0x0010F5AC File Offset: 0x0010D7AC
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

	// Token: 0x04002172 RID: 8562
	private mainScript mS_;

	// Token: 0x04002173 RID: 8563
	private GUI_Main guiMain_;
}
