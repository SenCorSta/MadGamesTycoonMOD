using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015E RID: 350
public class Menu_Dev_KonsoleComplete : MonoBehaviour
{
	// Token: 0x06000D13 RID: 3347 RVA: 0x0008F3A3 File Offset: 0x0008D5A3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0008F3AC File Offset: 0x0008D5AC
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0008F3A3 File Offset: 0x0008D5A3
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0008F586 File Offset: 0x0008D786
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
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

	// Token: 0x06000D18 RID: 3352 RVA: 0x0008F6CA File Offset: 0x0008D8CA
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0008F6F0 File Offset: 0x0008D8F0
	public void BUTTON_Release()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.startProduktionskosten = this.pS_.CalcStartProductionsCosts();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[328]);
		this.guiMain_.uiObjects[328].GetComponent<Menu_Konsolenpreis>().Init(this.pS_, this.task_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0008F770 File Offset: 0x0008D970
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[327]);
		this.guiMain_.uiObjects[327].GetComponent<Menu_W_Dev_KonsoleVerwerfen>().Init(this.pS_, this.task_);
	}

	// Token: 0x04001191 RID: 4497
	public GameObject[] uiObjects;

	// Token: 0x04001192 RID: 4498
	private GameObject main_;

	// Token: 0x04001193 RID: 4499
	private mainScript mS_;

	// Token: 0x04001194 RID: 4500
	private textScript tS_;

	// Token: 0x04001195 RID: 4501
	private GUI_Main guiMain_;

	// Token: 0x04001196 RID: 4502
	private sfxScript sfx_;

	// Token: 0x04001197 RID: 4503
	private genres genres_;

	// Token: 0x04001198 RID: 4504
	private themes themes_;

	// Token: 0x04001199 RID: 4505
	private licences licences_;

	// Token: 0x0400119A RID: 4506
	private engineFeatures eF_;

	// Token: 0x0400119B RID: 4507
	private cameraMovementScript cmS_;

	// Token: 0x0400119C RID: 4508
	private unlockScript unlock_;

	// Token: 0x0400119D RID: 4509
	private gameplayFeatures gF_;

	// Token: 0x0400119E RID: 4510
	private games games_;

	// Token: 0x0400119F RID: 4511
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x040011A0 RID: 4512
	private platforms platforms_;

	// Token: 0x040011A1 RID: 4513
	private platformScript pS_;

	// Token: 0x040011A2 RID: 4514
	private taskKonsole task_;
}
