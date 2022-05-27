using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015F RID: 351
public class Menu_Dev_KonsoleEntwicklungsbericht : MonoBehaviour
{
	// Token: 0x06000D0D RID: 3341 RVA: 0x00009201 File Offset: 0x00007401
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0009E91C File Offset: 0x0009CB1C
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
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
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00009209 File Offset: 0x00007409
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000921D File Offset: 0x0000741D
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0009EAE8 File Offset: 0x0009CCE8
	public void Init(platformScript plat_, roomScript room_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.rS_ = room_;
		this.SetLeitenderTechniker(this.GetLeitenderTechniker(), false);
		this.uiObjects[0].GetComponent<InputField>().text = this.pS_.myName;
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(1612) + ": <b><color=blue>" + this.mS_.GetMoney((long)this.platforms_.GetPerformance(this.pS_), false) + "</color></b>";
		this.uiObjects[6].GetComponent<Image>().fillAmount = this.pS_.GetProzent() * 0.01f;
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(450) + " " + this.mS_.Round(this.pS_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(6) + " <color=red>" + this.mS_.GetMoney(this.pS_.GetGesamtAusgaben(), true) + "</color>";
		string text = this.tS_.GetText(1775);
		text = text.Replace("<NUM>", this.pS_.weeksInDevelopment.ToString());
		this.uiObjects[9].GetComponent<Text>().text = text;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x00009231 File Offset: 0x00007431
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00009257 File Offset: 0x00007457
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length > 0)
		{
			this.pS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
		this.BUTTON_Close();
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0009ECCC File Offset: 0x0009CECC
	public void SetLeitenderTechniker(characterScript charS_, bool manuellSelectet)
	{
		taskKonsole taskKonsole = null;
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			taskKonsole = gameObject.GetComponent<taskKonsole>();
		}
		if (!charS_)
		{
			float num = 0f;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					characterScript component = array[i].GetComponent<characterScript>();
					if (component && component.roomID == this.rS_.myID)
					{
						if (component.s_technik > num)
						{
							num = component.s_technik;
							charS_ = component;
						}
						if (this.rS_.leitenderTechniker == component.myID)
						{
							charS_ = component;
							break;
						}
					}
				}
			}
		}
		if (!charS_)
		{
			this.uiObjects[1].GetComponent<Text>().text = "---";
			taskKonsole.leitenderTechnikerID = -1;
			taskKonsole.techniker_ = null;
			this.rS_.leitenderTechniker = -1;
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = charS_.myName;
		taskKonsole.leitenderTechnikerID = charS_.myID;
		taskKonsole.techniker_ = charS_;
		if (this.rS_.leitenderTechniker != charS_.myID)
		{
			this.rS_.leitenderTechniker = -1;
		}
		if (manuellSelectet)
		{
			this.rS_.leitenderTechniker = charS_.myID;
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0009EE34 File Offset: 0x0009D034
	public characterScript GetLeitenderTechniker()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			taskKonsole component = gameObject.GetComponent<taskKonsole>();
			if (component)
			{
				return component.techniker_;
			}
		}
		return null;
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0009EE80 File Offset: 0x0009D080
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[324]);
		this.guiMain_.uiObjects[324].GetComponent<Menu_LeitenderTechniker>().Init(this.rS_);
	}

	// Token: 0x040011A7 RID: 4519
	public GameObject[] uiObjects;

	// Token: 0x040011A8 RID: 4520
	private GameObject main_;

	// Token: 0x040011A9 RID: 4521
	private mainScript mS_;

	// Token: 0x040011AA RID: 4522
	private textScript tS_;

	// Token: 0x040011AB RID: 4523
	private GUI_Main guiMain_;

	// Token: 0x040011AC RID: 4524
	private sfxScript sfx_;

	// Token: 0x040011AD RID: 4525
	private genres genres_;

	// Token: 0x040011AE RID: 4526
	private themes themes_;

	// Token: 0x040011AF RID: 4527
	private licences licences_;

	// Token: 0x040011B0 RID: 4528
	private engineFeatures eF_;

	// Token: 0x040011B1 RID: 4529
	private cameraMovementScript cmS_;

	// Token: 0x040011B2 RID: 4530
	private unlockScript unlock_;

	// Token: 0x040011B3 RID: 4531
	private gameplayFeatures gF_;

	// Token: 0x040011B4 RID: 4532
	private games games_;

	// Token: 0x040011B5 RID: 4533
	private platforms platforms_;

	// Token: 0x040011B6 RID: 4534
	private platformScript pS_;

	// Token: 0x040011B7 RID: 4535
	private roomScript rS_;
}
