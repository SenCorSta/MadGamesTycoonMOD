using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_Aboverlauf : MonoBehaviour
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.InitBalken();
		long num = this.mS_.aboverlauf[0] - this.mS_.aboverlauf[23];
		if (num >= 0L)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(700) + ": <color=black>+" + this.mS_.GetMoney(num, false) + "</color>";
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(700) + ": <color=red>" + this.mS_.GetMoney(num, false) + "</color>";
	}

	
	private void InitBalken()
	{
		float num = 400f;
		long num2 = 0L;
		for (int i = 0; i < this.mS_.aboverlauf.Length; i++)
		{
			if (num2 < this.mS_.aboverlauf[i])
			{
				num2 = this.mS_.aboverlauf[i];
			}
		}
		float num3 = num / (float)num2;
		for (int j = 0; j < this.mS_.aboverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.aboverlauf[j] * num3 / num;
			if (this.mS_.aboverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.aboverlauf[j], false);
				if (j < this.mS_.aboverlauf.Length - 1)
				{
					long num4 = this.mS_.aboverlauf[j] - this.mS_.aboverlauf[j + 1];
					if (num4 > 0L)
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "+" + this.mS_.GetMoney(num4, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[4];
					}
					else
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = this.mS_.GetMoney(num4, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[8];
					}
				}
			}
			else
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = "";
				this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "";
			}
		}
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

	
	private genres genres_;

	
	private engineFeatures eF_;

	
	private engineScript eS_;
}
