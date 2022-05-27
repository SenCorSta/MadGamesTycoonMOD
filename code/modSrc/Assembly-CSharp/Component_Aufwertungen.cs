using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000108 RID: 264
public class Component_Aufwertungen : MonoBehaviour
{
	// Token: 0x06000881 RID: 2177 RVA: 0x0005CFAF File Offset: 0x0005B1AF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0005CFB8 File Offset: 0x0005B1B8
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

	// Token: 0x06000883 RID: 2179 RVA: 0x0005D158 File Offset: 0x0005B358
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

	// Token: 0x04000CFD RID: 3325
	public GameObject[] uiObjects;

	// Token: 0x04000CFE RID: 3326
	public Sprite[] uiSprites;

	// Token: 0x04000CFF RID: 3327
	public Color[] colors;

	// Token: 0x04000D00 RID: 3328
	private GameObject main_;

	// Token: 0x04000D01 RID: 3329
	private mainScript mS_;

	// Token: 0x04000D02 RID: 3330
	private textScript tS_;

	// Token: 0x04000D03 RID: 3331
	private GUI_Main guiMain_;

	// Token: 0x04000D04 RID: 3332
	private sfxScript sfx_;

	// Token: 0x04000D05 RID: 3333
	private genres genres_;

	// Token: 0x04000D06 RID: 3334
	private themes themes_;

	// Token: 0x04000D07 RID: 3335
	private licences licences_;

	// Token: 0x04000D08 RID: 3336
	private engineFeatures eF_;

	// Token: 0x04000D09 RID: 3337
	private cameraMovementScript cmS_;

	// Token: 0x04000D0A RID: 3338
	private unlockScript unlock_;

	// Token: 0x04000D0B RID: 3339
	private gameplayFeatures gF_;

	// Token: 0x04000D0C RID: 3340
	private games games_;
}
