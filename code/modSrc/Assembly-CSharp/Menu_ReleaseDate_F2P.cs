using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000157 RID: 343
public class Menu_ReleaseDate_F2P : MonoBehaviour
{
	// Token: 0x06000CA6 RID: 3238 RVA: 0x0008980D File Offset: 0x00087A0D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00089818 File Offset: 0x00087A18
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

	// Token: 0x06000CA8 RID: 3240 RVA: 0x000899B6 File Offset: 0x00087BB6
	public void Init(gameScript game_, taskGame t_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.SLIDER_Wochen();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x000899F0 File Offset: 0x00087BF0
	public void SLIDER_Wochen()
	{
		if (this.uiObjects[2].GetComponent<Slider>().value > 1f)
		{
			string text = this.tS_.GetText(1123);
			text = text.Replace("<NUM>", Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value).ToString());
			this.uiObjects[1].GetComponent<Text>().text = text;
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1864);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00089A88 File Offset: 0x00087C88
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00089AA4 File Offset: 0x00087CA4
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		if (this.task_)
		{
			UnityEngine.Object.Destroy(this.task_.gameObject);
		}
		this.gS_.releaseDate = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (this.gS_.handy && this.gS_.gameTyp != 2)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[301]);
			this.guiMain_.uiObjects[301].GetComponent<Menu_HandyPreis>().Init(this.gS_);
		}
		else
		{
			this.gS_.SetPublisher(this.mS_.myID);
			this.gS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		this.guiMain_.uiObjects[69].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001113 RID: 4371
	public GameObject[] uiObjects;

	// Token: 0x04001114 RID: 4372
	private GameObject main_;

	// Token: 0x04001115 RID: 4373
	private mainScript mS_;

	// Token: 0x04001116 RID: 4374
	private textScript tS_;

	// Token: 0x04001117 RID: 4375
	private GUI_Main guiMain_;

	// Token: 0x04001118 RID: 4376
	private sfxScript sfx_;

	// Token: 0x04001119 RID: 4377
	private genres genres_;

	// Token: 0x0400111A RID: 4378
	private themes themes_;

	// Token: 0x0400111B RID: 4379
	private licences licences_;

	// Token: 0x0400111C RID: 4380
	private engineFeatures eF_;

	// Token: 0x0400111D RID: 4381
	private cameraMovementScript cmS_;

	// Token: 0x0400111E RID: 4382
	private unlockScript unlock_;

	// Token: 0x0400111F RID: 4383
	private gameplayFeatures gF_;

	// Token: 0x04001120 RID: 4384
	private games games_;

	// Token: 0x04001121 RID: 4385
	private gameScript gS_;

	// Token: 0x04001122 RID: 4386
	private taskGame task_;
}
