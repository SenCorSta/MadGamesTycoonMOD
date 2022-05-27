using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000160 RID: 352
public class Menu_Dev_KonsoleEntwicklungsbericht : MonoBehaviour
{
	// Token: 0x06000D25 RID: 3365 RVA: 0x0008FE4F File Offset: 0x0008E04F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0008FE58 File Offset: 0x0008E058
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

	// Token: 0x06000D27 RID: 3367 RVA: 0x00090022 File Offset: 0x0008E222
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x00090036 File Offset: 0x0008E236
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0009004C File Offset: 0x0008E24C
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

	// Token: 0x06000D2A RID: 3370 RVA: 0x00090230 File Offset: 0x0008E430
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x00090256 File Offset: 0x0008E456
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length > 0)
		{
			this.pS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
		this.BUTTON_Close();
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x00090298 File Offset: 0x0008E498
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

	// Token: 0x06000D2D RID: 3373 RVA: 0x00090400 File Offset: 0x0008E600
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

	// Token: 0x06000D2E RID: 3374 RVA: 0x0009044C File Offset: 0x0008E64C
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[324]);
		this.guiMain_.uiObjects[324].GetComponent<Menu_LeitenderTechniker>().Init(this.rS_);
	}

	// Token: 0x040011AF RID: 4527
	public GameObject[] uiObjects;

	// Token: 0x040011B0 RID: 4528
	private GameObject main_;

	// Token: 0x040011B1 RID: 4529
	private mainScript mS_;

	// Token: 0x040011B2 RID: 4530
	private textScript tS_;

	// Token: 0x040011B3 RID: 4531
	private GUI_Main guiMain_;

	// Token: 0x040011B4 RID: 4532
	private sfxScript sfx_;

	// Token: 0x040011B5 RID: 4533
	private genres genres_;

	// Token: 0x040011B6 RID: 4534
	private themes themes_;

	// Token: 0x040011B7 RID: 4535
	private licences licences_;

	// Token: 0x040011B8 RID: 4536
	private engineFeatures eF_;

	// Token: 0x040011B9 RID: 4537
	private cameraMovementScript cmS_;

	// Token: 0x040011BA RID: 4538
	private unlockScript unlock_;

	// Token: 0x040011BB RID: 4539
	private gameplayFeatures gF_;

	// Token: 0x040011BC RID: 4540
	private games games_;

	// Token: 0x040011BD RID: 4541
	private platforms platforms_;

	// Token: 0x040011BE RID: 4542
	private platformScript pS_;

	// Token: 0x040011BF RID: 4543
	private roomScript rS_;
}
