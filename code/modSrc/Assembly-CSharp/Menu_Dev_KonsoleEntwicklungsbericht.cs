using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_KonsoleEntwicklungsbericht : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length > 0)
		{
			this.pS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
		this.BUTTON_Close();
	}

	
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

	
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[324]);
		this.guiMain_.uiObjects[324].GetComponent<Menu_LeitenderTechniker>().Init(this.rS_);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private platforms platforms_;

	
	private platformScript pS_;

	
	private roomScript rS_;
}
