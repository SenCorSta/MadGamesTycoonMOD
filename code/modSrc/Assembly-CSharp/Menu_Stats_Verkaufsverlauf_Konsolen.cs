using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000263 RID: 611
public class Menu_Stats_Verkaufsverlauf_Konsolen : MonoBehaviour
{
	// Token: 0x060017D2 RID: 6098 RVA: 0x000EE73A File Offset: 0x000EC93A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x000EE744 File Offset: 0x000EC944
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

	// Token: 0x060017D4 RID: 6100 RVA: 0x000EE82A File Offset: 0x000ECA2A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x000EE832 File Offset: 0x000ECA32
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000EE848 File Offset: 0x000ECA48
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000EE89C File Offset: 0x000ECA9C
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.verkaufsverlaufKonsolen.Length; i++)
		{
			num += this.mS_.verkaufsverlaufKonsolen[i];
			if (num3 < this.mS_.verkaufsverlaufKonsolen[i])
			{
				num3 = this.mS_.verkaufsverlaufKonsolen[i];
			}
		}
		float num4 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.verkaufsverlaufKonsolen.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.verkaufsverlaufKonsolen[j] * num4 / num2;
			if (this.mS_.verkaufsverlaufKonsolen[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.verkaufsverlaufKonsolen[j], false);
				if (j < this.mS_.verkaufsverlaufKonsolen.Length - 1)
				{
					long num5 = this.mS_.verkaufsverlaufKonsolen[j] - this.mS_.verkaufsverlaufKonsolen[j + 1];
					if (num5 > 0L)
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "+" + this.mS_.GetMoney(num5, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[4];
					}
					else
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = this.mS_.GetMoney(num5, false);
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
		return num;
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000EEAE7 File Offset: 0x000ECCE7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B81 RID: 7041
	public GameObject[] uiBalken;

	// Token: 0x04001B82 RID: 7042
	public GameObject[] uiObjects;

	// Token: 0x04001B83 RID: 7043
	private GameObject main_;

	// Token: 0x04001B84 RID: 7044
	private mainScript mS_;

	// Token: 0x04001B85 RID: 7045
	private textScript tS_;

	// Token: 0x04001B86 RID: 7046
	private GUI_Main guiMain_;

	// Token: 0x04001B87 RID: 7047
	private sfxScript sfx_;

	// Token: 0x04001B88 RID: 7048
	private genres genres_;

	// Token: 0x04001B89 RID: 7049
	private engineFeatures eF_;

	// Token: 0x04001B8A RID: 7050
	private engineScript eS_;
}
