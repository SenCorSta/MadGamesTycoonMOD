using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class Menu_BuyEngine_Details : MonoBehaviour
{
	// Token: 0x06000F01 RID: 3841 RVA: 0x0000AA1F File Offset: 0x00008C1F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x000AD528 File Offset: 0x000AB728
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

	// Token: 0x06000F03 RID: 3843 RVA: 0x0000AA27 File Offset: 0x00008C27
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x000AD610 File Offset: 0x000AB810
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
		if (!this.eS_.playerEngine && !this.eS_.gekauft)
		{
			this.uiObjects[8].SetActive(true);
			this.uiObjects[9].SetActive(false);
			return;
		}
		this.uiObjects[8].SetActive(false);
		this.uiObjects[9].SetActive(true);
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0000AA3C File Offset: 0x00008C3C
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0000AA52 File Offset: 0x00008C52
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x000AD87C File Offset: 0x000ABA7C
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		if (this.eS_.multiplayerSlot != -1)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.mpCalls_.myID, this.eS_.multiplayerSlot, 1, this.eS_.preis);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.eS_.multiplayerSlot, 1, this.eS_.preis);
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x000AD974 File Offset: 0x000ABB74
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	// Token: 0x04001353 RID: 4947
	public GameObject[] uiObjects;

	// Token: 0x04001354 RID: 4948
	private roomScript rS_;

	// Token: 0x04001355 RID: 4949
	private GameObject main_;

	// Token: 0x04001356 RID: 4950
	private mainScript mS_;

	// Token: 0x04001357 RID: 4951
	private textScript tS_;

	// Token: 0x04001358 RID: 4952
	private GUI_Main guiMain_;

	// Token: 0x04001359 RID: 4953
	private sfxScript sfx_;

	// Token: 0x0400135A RID: 4954
	private genres genres_;

	// Token: 0x0400135B RID: 4955
	private engineFeatures eF_;

	// Token: 0x0400135C RID: 4956
	private engineScript eS_;
}
