﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011D RID: 285
public class Menu_DevGame_EngineFeature : MonoBehaviour
{
	// Token: 0x060009D9 RID: 2521 RVA: 0x0006C2B3 File Offset: 0x0006A4B3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0006C2BC File Offset: 0x0006A4BC
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.devGame_)
		{
			this.devGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0006C3AA File Offset: 0x0006A5AA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0006C3DC File Offset: 0x0006A5DC
	public void Init(int featureArt_)
	{
		this.featureArt = featureArt_;
		this.FindScripts();
		if (this.devGame_.g_GameEngineScript_)
		{
			this.uiObjects[7].GetComponent<Text>().text = this.devGame_.g_GameEngineScript_.GetTechLevel().ToString();
		}
		this.uiObjects[8].GetComponent<Text>().text = this.devGame_.GetLowestPlatformTechLevel().ToString();
		switch (featureArt_)
		{
		case 0:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(9);
			break;
		case 1:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(10);
			break;
		case 2:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(11);
			break;
		case 3:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(12);
			break;
		}
		this.CreateItems(featureArt_);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0006C518 File Offset: 0x0006A718
	private void CreateItems(int typ_)
	{
		if (!this.devGame_.g_GameEngineScript_)
		{
			return;
		}
		for (int i = 0; i < this.devGame_.g_GameEngineScript_.features.Length; i++)
		{
			if (this.devGame_.g_GameEngineScript_.features[i] && this.eF_.engineFeatures_TYP[i] == typ_)
			{
				Item_DevGame_EngineFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_EngineFeature>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.eF_ = this.eF_;
			}
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0006C5FC File Offset: 0x0006A7FC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E27 RID: 3623
	private mainScript mS_;

	// Token: 0x04000E28 RID: 3624
	private GameObject main_;

	// Token: 0x04000E29 RID: 3625
	private GUI_Main guiMain_;

	// Token: 0x04000E2A RID: 3626
	private sfxScript sfx_;

	// Token: 0x04000E2B RID: 3627
	private textScript tS_;

	// Token: 0x04000E2C RID: 3628
	private engineFeatures eF_;

	// Token: 0x04000E2D RID: 3629
	private Menu_DevGame devGame_;

	// Token: 0x04000E2E RID: 3630
	public GameObject[] uiPrefabs;

	// Token: 0x04000E2F RID: 3631
	public GameObject[] uiObjects;

	// Token: 0x04000E30 RID: 3632
	private int featureArt;
}
