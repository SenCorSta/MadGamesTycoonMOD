using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000262 RID: 610
public class Menu_Stats_Verkaufsverlauf : MonoBehaviour
{
	// Token: 0x060017CA RID: 6090 RVA: 0x000EE348 File Offset: 0x000EC548
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000EE350 File Offset: 0x000EC550
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

	// Token: 0x060017CC RID: 6092 RVA: 0x000EE436 File Offset: 0x000EC636
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000EE43E File Offset: 0x000EC63E
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000EE454 File Offset: 0x000EC654
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x000EE4A8 File Offset: 0x000EC6A8
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.verkaufsverlauf.Length; i++)
		{
			num += this.mS_.verkaufsverlauf[i];
			if (num3 < this.mS_.verkaufsverlauf[i])
			{
				num3 = this.mS_.verkaufsverlauf[i];
			}
		}
		float num4 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.verkaufsverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.verkaufsverlauf[j] * num4 / num2;
			if (this.mS_.verkaufsverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.verkaufsverlauf[j], false);
				if (j < this.mS_.verkaufsverlauf.Length - 1)
				{
					long num5 = this.mS_.verkaufsverlauf[j] - this.mS_.verkaufsverlauf[j + 1];
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

	// Token: 0x060017D0 RID: 6096 RVA: 0x000EE6F3 File Offset: 0x000EC8F3
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B77 RID: 7031
	public GameObject[] uiBalken;

	// Token: 0x04001B78 RID: 7032
	public GameObject[] uiObjects;

	// Token: 0x04001B79 RID: 7033
	private GameObject main_;

	// Token: 0x04001B7A RID: 7034
	private mainScript mS_;

	// Token: 0x04001B7B RID: 7035
	private textScript tS_;

	// Token: 0x04001B7C RID: 7036
	private GUI_Main guiMain_;

	// Token: 0x04001B7D RID: 7037
	private sfxScript sfx_;

	// Token: 0x04001B7E RID: 7038
	private genres genres_;

	// Token: 0x04001B7F RID: 7039
	private engineFeatures eF_;

	// Token: 0x04001B80 RID: 7040
	private engineScript eS_;
}
