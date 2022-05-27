using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_Publisher_Main : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.flagSprites[this.pS_.country];
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.guiMain_.DrawStarsColor(this.uiObjects[3], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), Color.white);
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$" + this.mS_.Round(this.pS_.share, 1).ToString() + "</b>";
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.GetDateString();
		if (this.pS_.IsTochterfirma() && this.pS_.TochterfirmaGeschlossen())
		{
			Text component = this.uiObjects[5].GetComponent<Text>();
			component.text = component.text + "\n<color=red><b>" + this.tS_.GetText(1969) + "</b></color>";
		}
		if (this.pS_.IsMyTochterfirma())
		{
			if (!this.uiObjects[11].activeSelf)
			{
				this.uiObjects[11].SetActive(true);
			}
		}
		else if (this.uiObjects[11].activeSelf)
		{
			this.uiObjects[11].SetActive(false);
		}
		this.uiObjects[6].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[6].GetComponent<tooltip>().c = this.tS_.GetText(437) + ": <b>" + this.genres_.GetName(this.pS_.fanGenre) + "</b>";
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[9].activeSelf)
			{
				this.uiObjects[9].SetActive(true);
			}
		}
		else if (this.uiObjects[9].activeSelf)
		{
			this.uiObjects[9].SetActive(false);
		}
		if (this.pS_.IsMyTochterfirma() || this.pS_.notForSale)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[8].GetComponent<Button>().interactable = true;
		}
		if (this.mS_.multiplayer)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Awards()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[144]);
		this.guiMain_.uiObjects[144].GetComponent<Menu_Stats_Awards>().Init(this.pS_);
	}

	
	public void BUTTON_Games()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[360]);
		this.guiMain_.uiObjects[360].GetComponent<Menu_Stats_Developer_Games>().Init(this.pS_);
	}

	
	public void BUTTON_IPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[361]);
		this.guiMain_.uiObjects[361].GetComponent<Menu_Stats_Developer_IPs>().Init(this.pS_);
	}

	
	public void BUTTON_Vertrieben()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[374]);
		this.guiMain_.uiObjects[374].GetComponent<Menu_Stats_Publisher_Vertrieben>().Init(this.pS_);
	}

	
	public void BUTTON_FirmaKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[386]);
		this.guiMain_.uiObjects[386].GetComponent<Menu_W_FirmaKaufen>().Init(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private publisherScript pS_;
}
