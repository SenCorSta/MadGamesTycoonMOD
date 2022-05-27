using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015D RID: 349
public class Menu_Dev_KonsoleComplete : MonoBehaviour
{
	// Token: 0x06000CFB RID: 3323 RVA: 0x00009163 File Offset: 0x00007363
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0009DF64 File Offset: 0x0009C164
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x00009163 File Offset: 0x00007363
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0000916B File Offset: 0x0000736B
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0009E140 File Offset: 0x0009C340
	public void Init(platformScript s1_, taskKonsole s2_)
	{
		this.FindScripts();
		this.pS_ = s1_;
		this.task_ = s2_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.myName;
		this.pS_.SetPic(this.uiObjects[1]);
		this.uiObjects[2].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		this.uiObjects[4].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1612) + ": <b><color=blue>" + this.mS_.GetMoney((long)this.platforms_.GetPerformance(this.pS_), false) + "</color></b>";
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x00009186 File Offset: 0x00007386
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0009E268 File Offset: 0x0009C468
	public void BUTTON_Release()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.startProduktionskosten = this.pS_.CalcStartProductionsCosts();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[328]);
		this.guiMain_.uiObjects[328].GetComponent<Menu_Konsolenpreis>().Init(this.pS_, this.task_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[327]);
		this.guiMain_.uiObjects[327].GetComponent<Menu_W_Dev_KonsoleVerwerfen>().Init(this.pS_, this.task_);
	}

	// Token: 0x04001189 RID: 4489
	public GameObject[] uiObjects;

	// Token: 0x0400118A RID: 4490
	private GameObject main_;

	// Token: 0x0400118B RID: 4491
	private mainScript mS_;

	// Token: 0x0400118C RID: 4492
	private textScript tS_;

	// Token: 0x0400118D RID: 4493
	private GUI_Main guiMain_;

	// Token: 0x0400118E RID: 4494
	private sfxScript sfx_;

	// Token: 0x0400118F RID: 4495
	private genres genres_;

	// Token: 0x04001190 RID: 4496
	private themes themes_;

	// Token: 0x04001191 RID: 4497
	private licences licences_;

	// Token: 0x04001192 RID: 4498
	private engineFeatures eF_;

	// Token: 0x04001193 RID: 4499
	private cameraMovementScript cmS_;

	// Token: 0x04001194 RID: 4500
	private unlockScript unlock_;

	// Token: 0x04001195 RID: 4501
	private gameplayFeatures gF_;

	// Token: 0x04001196 RID: 4502
	private games games_;

	// Token: 0x04001197 RID: 4503
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04001198 RID: 4504
	private platforms platforms_;

	// Token: 0x04001199 RID: 4505
	private platformScript pS_;

	// Token: 0x0400119A RID: 4506
	private taskKonsole task_;
}
