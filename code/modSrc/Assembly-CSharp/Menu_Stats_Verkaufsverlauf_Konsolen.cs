using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025F RID: 607
public class Menu_Stats_Verkaufsverlauf_Konsolen : MonoBehaviour
{
	// Token: 0x0600178F RID: 6031 RVA: 0x00010682 File Offset: 0x0000E882
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000F3FF4 File Offset: 0x000F21F4
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

	// Token: 0x06001791 RID: 6033 RVA: 0x0001068A File Offset: 0x0000E88A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x00010692 File Offset: 0x0000E892
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000F40DC File Offset: 0x000F22DC
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000F4130 File Offset: 0x000F2330
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

	// Token: 0x06001795 RID: 6037 RVA: 0x000106A8 File Offset: 0x0000E8A8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B67 RID: 7015
	public GameObject[] uiBalken;

	// Token: 0x04001B68 RID: 7016
	public GameObject[] uiObjects;

	// Token: 0x04001B69 RID: 7017
	private GameObject main_;

	// Token: 0x04001B6A RID: 7018
	private mainScript mS_;

	// Token: 0x04001B6B RID: 7019
	private textScript tS_;

	// Token: 0x04001B6C RID: 7020
	private GUI_Main guiMain_;

	// Token: 0x04001B6D RID: 7021
	private sfxScript sfx_;

	// Token: 0x04001B6E RID: 7022
	private genres genres_;

	// Token: 0x04001B6F RID: 7023
	private engineFeatures eF_;

	// Token: 0x04001B70 RID: 7024
	private engineScript eS_;
}
