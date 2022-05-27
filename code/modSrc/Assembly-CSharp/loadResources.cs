using System;
using System.IO;
using UnityEngine;


public class loadResources : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.LoadLogos();
	}

	
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

	
	private mainScript mS_;

	
	private GUI_Main guiMain_;
}
