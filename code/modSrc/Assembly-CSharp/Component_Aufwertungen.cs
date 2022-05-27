using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000107 RID: 263
public class Component_Aufwertungen : MonoBehaviour
{
	// Token: 0x06000872 RID: 2162 RVA: 0x00006495 File Offset: 0x00004695
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0006EA44 File Offset: 0x0006CC44
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		for (int i = 0; i < gS_.gameplayStudio.Length; i++)
		{
			if (gS_.gameplayStudio[i])
			{
				this.uiObjects[i].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[i].GetComponent<Image>().color = this.colors[1];
			}
		}
		for (int j = 0; j < gS_.grafikStudio.Length; j++)
		{
			if (gS_.grafikStudio[j])
			{
				this.uiObjects[12 + j].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[12 + j].GetComponent<Image>().color = this.colors[1];
			}
		}
		for (int k = 0; k < gS_.soundStudio.Length; k++)
		{
			if (gS_.soundStudio[k])
			{
				this.uiObjects[6 + k].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[6 + k].GetComponent<Image>().color = this.colors[1];
			}
		}
		if (!gS_.motionCaptureStudio[0])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[18];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[1];
		}
		if (gS_.motionCaptureStudio[0])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[18];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[1])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[19];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[2])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[20];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (!gS_.motionCaptureStudio[3])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[21];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[1];
		}
		if (gS_.motionCaptureStudio[3])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[21];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[4])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[22];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[5])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[23];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
	}

	// Token: 0x04000CF5 RID: 3317
	public GameObject[] uiObjects;

	// Token: 0x04000CF6 RID: 3318
	public Sprite[] uiSprites;

	// Token: 0x04000CF7 RID: 3319
	public Color[] colors;

	// Token: 0x04000CF8 RID: 3320
	private GameObject main_;

	// Token: 0x04000CF9 RID: 3321
	private mainScript mS_;

	// Token: 0x04000CFA RID: 3322
	private textScript tS_;

	// Token: 0x04000CFB RID: 3323
	private GUI_Main guiMain_;

	// Token: 0x04000CFC RID: 3324
	private sfxScript sfx_;

	// Token: 0x04000CFD RID: 3325
	private genres genres_;

	// Token: 0x04000CFE RID: 3326
	private themes themes_;

	// Token: 0x04000CFF RID: 3327
	private licences licences_;

	// Token: 0x04000D00 RID: 3328
	private engineFeatures eF_;

	// Token: 0x04000D01 RID: 3329
	private cameraMovementScript cmS_;

	// Token: 0x04000D02 RID: 3330
	private unlockScript unlock_;

	// Token: 0x04000D03 RID: 3331
	private gameplayFeatures gF_;

	// Token: 0x04000D04 RID: 3332
	private games games_;
}
