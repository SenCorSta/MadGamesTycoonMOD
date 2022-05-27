using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_Tochterfirma_Main : MonoBehaviour
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
		this.UpdateData();
	}

	
	public void UpdateData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStarsColor(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f), Color.white);
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[6].GetComponent<tooltip>().c = this.tS_.GetText(437) + ": <b>" + this.genres_.GetName(this.pS_.fanGenre) + "</b>";
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(685) + ": <b>" + this.pS_.GetFirmenwertString() + "</b>";
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1934) + ": <b>" + this.mS_.GetMoney(this.pS_.GetVerwaltungskosten(), true) + "</b>";
		if (this.pS_.developer)
		{
			int num = this.pS_.newGameInWeeks;
			if (num < 2)
			{
				num = 2;
			}
			string text = this.tS_.GetText(1948);
			text = text.Replace("<NUM>", "<color=blue><b>" + num.ToString() + "</b></color>");
			this.uiObjects[21].GetComponent<Text>().text = text;
			float num2 = (float)this.pS_.newGameInWeeksORG;
			if (num2 <= (float)this.pS_.newGameInWeeks)
			{
				num2 = (float)this.pS_.newGameInWeeks;
			}
			num2 = 100f / num2;
			num2 = 100f - num2 * (float)this.pS_.newGameInWeeks;
			this.uiObjects[19].GetComponent<Image>().fillAmount = num2 * 0.01f;
			if (num <= 2)
			{
				this.uiObjects[19].GetComponent<Image>().fillAmount = 1f;
			}
			if (this.pS_.newGameInWeeks > 2)
			{
				this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1944) + ": " + Mathf.RoundToInt(num2).ToString() + "%";
			}
			else
			{
				this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1947);
			}
		}
		else
		{
			this.uiObjects[21].GetComponent<Text>().text = "";
			this.uiObjects[20].GetComponent<Text>().text = this.tS_.GetText(1949);
			this.uiObjects[19].GetComponent<Image>().fillAmount = 0f;
		}
		if (this.pS_.tf_geschlossen)
		{
			this.uiObjects[12].SetActive(true);
			this.uiObjects[18].SetActive(true);
			this.uiObjects[0].GetComponent<Text>().text = "<color=red>" + this.pS_.GetName() + "</color>";
			this.uiObjects[10].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		}
		else
		{
			this.uiObjects[12].SetActive(false);
			this.uiObjects[18].SetActive(false);
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
			this.uiObjects[10].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		}
		if (!this.pS_.publisher)
		{
			this.uiObjects[13].SetActive(true);
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$ -</b>";
		}
		else
		{
			this.uiObjects[13].SetActive(false);
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(436) + ": <b>$" + this.mS_.Round(this.pS_.share, 1).ToString() + "</b>";
		}
		if (!this.pS_.tf_publisher)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
			this.uiObjects[16].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[8].GetComponent<Button>().interactable = true;
			this.uiObjects[16].GetComponent<Button>().interactable = false;
		}
		if (!this.pS_.tf_developer)
		{
			this.uiObjects[9].GetComponent<Button>().interactable = false;
			this.uiObjects[17].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[9].GetComponent<Button>().interactable = true;
			this.uiObjects[17].GetComponent<Button>().interactable = false;
		}
		if (this.pS_.stars >= 100f || this.pS_.GetStarsAmount() >= 5)
		{
			this.uiObjects[15].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[15].GetComponent<Button>().interactable = true;
		}
		this.UpdateGewinnanteilTooltip();
	}

	
	private void UpdateGewinnanteilTooltip()
	{
		string text = this.tS_.GetText(1991);
		text = text.Replace("<NUM>", "<color=blue><b>" + this.mS_.GetMoney((long)Mathf.RoundToInt(this.pS_.share), true) + "</b></color>");
		this.uiObjects[22].GetComponent<tooltip>().c = text;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[385].GetComponent<Menu_Statistics_Tochterfirmen>().BUTTON_Search();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Rename()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[391]);
		this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().Init(this.pS_);
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

	
	public void BUTTON_Settings()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[393]);
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().Init(this.pS_);
	}

	
	public void BUTTON_Vertrieben()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[374]);
		this.guiMain_.uiObjects[374].GetComponent<Menu_Stats_Publisher_Vertrieben>().Init(this.pS_);
	}

	
	public void BUTTON_Umsatz()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[396]);
		this.guiMain_.uiObjects[396].GetComponent<Menu_Stats_TochterfirmaUmsatz>().Init(this.pS_);
	}

	
	public void BUTTON_FirmaVerkaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[395]);
		this.guiMain_.uiObjects[395].GetComponent<Menu_W_FirmaVerkaufen>().Init(this.pS_);
	}

	
	public void BUTTON_FirmaSchiessen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.pS_)
		{
			this.pS_.tf_geschlossen = !this.pS_.tf_geschlossen;
		}
		this.UpdateData();
	}

	
	public void BUTTON_FirmaAufwerten()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[388]);
			this.guiMain_.uiObjects[388].GetComponent<Menu_W_FirmaAufwerten>().Init(this.pS_);
		}
	}

	
	public void BUTTON_FirmaAufwertenPublisher()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[389]);
			this.guiMain_.uiObjects[389].GetComponent<Menu_W_FirmaAufwertenPublisher>().Init(this.pS_);
		}
	}

	
	public void BUTTON_FirmaAufwertenDeveloper()
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[390]);
			this.guiMain_.uiObjects[390].GetComponent<Menu_W_FirmaAufwertenDeveloper>().Init(this.pS_);
		}
	}

	
	private IEnumerator iMinusGewinnbeteiligung(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusGewinnbeteiligung(i);
		}
		yield break;
	}

	
	public void BUTTON_MinusGewinnbeteiligung(int i)
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.pS_.share -= (float)i;
			if (this.pS_.share < 3f)
			{
				this.pS_.share = 3f;
			}
			base.StartCoroutine(this.iMinusGewinnbeteiligung(i));
			this.UpdateData();
		}
	}

	
	private IEnumerator iPlusGewinnbeteiligung(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusGewinnbeteiligung(i);
		}
		yield break;
	}

	
	public void BUTTON_PlusGewinnbeteiligung(int i)
	{
		if (this.pS_)
		{
			this.sfx_.PlaySound(3, true);
			this.pS_.share += (float)i;
			if (this.pS_.share > 9f)
			{
				this.pS_.share = 9f;
			}
			base.StartCoroutine(this.iPlusGewinnbeteiligung(i));
			this.UpdateData();
		}
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
