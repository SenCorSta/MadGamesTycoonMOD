using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_TochterfirmaUmsatz : MonoBehaviour
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
		if (this.pS_)
		{
			long num = this.InitBalken();
			if (num >= 0L)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=green>" + this.mS_.GetMoney(num, true) + "</color>";
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=red>" + this.mS_.GetMoney(num, true) + "</color>";
		}
	}

	
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.pS_.tf_umsatz.Length; i++)
		{
			num += this.pS_.tf_umsatz[i];
			long num4 = this.pS_.tf_umsatz[i];
			if (num4 < 0L)
			{
				num4 *= -1L;
			}
			if (num3 < num4)
			{
				num3 = num4;
			}
		}
		float num5 = num2 / (float)num3;
		for (int j = 0; j < this.pS_.tf_umsatz.Length; j++)
		{
			long num6 = this.pS_.tf_umsatz[j];
			if (num6 < 0L)
			{
				num6 *= -1L;
			}
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)num6 * num5 / num2;
			if (this.pS_.tf_umsatz[j] < 0L)
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[5];
			}
			else
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.pS_.tf_umsatz[j], true);
			this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "";
		}
		return num;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiBalken;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private publisherScript pS_;
}
