using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018D RID: 397
public class Menu_BuyEngine_Details : MonoBehaviour
{
	// Token: 0x06000F19 RID: 3865 RVA: 0x000A0440 File Offset: 0x0009E640
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000A0448 File Offset: 0x0009E648
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000A052E File Offset: 0x0009E72E
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x000A0544 File Offset: 0x0009E744
	private void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.eS_.GetTechLevel();
		this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(this.eS_.spezialgenre);
		this.uiObjects[4].GetComponent<Text>().text = this.eS_.GetGamesAmount().ToString() + " " + this.tS_.GetText(271);
		this.uiObjects[5].GetComponent<Text>().text = this.eS_.GetFeaturesAmount().ToString() + " " + this.tS_.GetText(272);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.preis, true);
		this.uiObjects[7].GetComponent<Text>().text = this.eS_.gewinnbeteiligung.ToString() + "%";
		this.uiObjects[10].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.guiMain_.DrawStars(this.uiObjects[3], this.genres_.genres_LEVEL[this.eS_.spezialgenre]);
		platformScript spezialPlatformScript = this.eS_.GetSpezialPlatformScript();
		if (spezialPlatformScript)
		{
			this.uiObjects[13].GetComponent<Text>().text = spezialPlatformScript.GetName();
			spezialPlatformScript.SetPic(this.uiObjects[11]);
			this.guiMain_.DrawStars(this.uiObjects[12], spezialPlatformScript.erfahrung);
		}
		if (this.eS_.ownerID != this.mS_.myID && !this.eS_.gekauft)
		{
			this.uiObjects[8].SetActive(true);
			this.uiObjects[9].SetActive(false);
			return;
		}
		this.uiObjects[8].SetActive(false);
		this.uiObjects[9].SetActive(true);
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x000A07B9 File Offset: 0x0009E9B9
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000A07CF File Offset: 0x0009E9CF
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x000A07EC File Offset: 0x0009E9EC
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		if (this.eS_.ownerID != -1)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.myID, this.eS_.ownerID, 1, this.eS_.preis);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.eS_.ownerID, 1, this.eS_.preis);
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000A08DC File Offset: 0x0009EADC
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000A0930 File Offset: 0x0009EB30
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	// Token: 0x0400135C RID: 4956
	public GameObject[] uiObjects;

	// Token: 0x0400135D RID: 4957
	private roomScript rS_;

	// Token: 0x0400135E RID: 4958
	private GameObject main_;

	// Token: 0x0400135F RID: 4959
	private mainScript mS_;

	// Token: 0x04001360 RID: 4960
	private textScript tS_;

	// Token: 0x04001361 RID: 4961
	private GUI_Main guiMain_;

	// Token: 0x04001362 RID: 4962
	private sfxScript sfx_;

	// Token: 0x04001363 RID: 4963
	private genres genres_;

	// Token: 0x04001364 RID: 4964
	private engineFeatures eF_;

	// Token: 0x04001365 RID: 4965
	private engineScript eS_;
}
