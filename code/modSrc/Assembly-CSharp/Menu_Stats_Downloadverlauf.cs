using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000234 RID: 564
public class Menu_Stats_Downloadverlauf : MonoBehaviour
{
	// Token: 0x060015C1 RID: 5569 RVA: 0x000DD79F File Offset: 0x000DB99F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000DD7A8 File Offset: 0x000DB9A8
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

	// Token: 0x060015C3 RID: 5571 RVA: 0x000DD88E File Offset: 0x000DBA8E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000DD896 File Offset: 0x000DBA96
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000DD8AC File Offset: 0x000DBAAC
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x000DD900 File Offset: 0x000DBB00
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.downloadverlauf.Length; i++)
		{
			num += this.mS_.downloadverlauf[i];
			if (num3 < this.mS_.downloadverlauf[i])
			{
				num3 = this.mS_.downloadverlauf[i];
			}
		}
		float num4 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.downloadverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.downloadverlauf[j] * num4 / num2;
			if (this.mS_.downloadverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.downloadverlauf[j], false);
				if (j < this.mS_.downloadverlauf.Length - 1)
				{
					long num5 = this.mS_.downloadverlauf[j] - this.mS_.downloadverlauf[j + 1];
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

	// Token: 0x060015C7 RID: 5575 RVA: 0x000DDB4B File Offset: 0x000DBD4B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019AF RID: 6575
	public GameObject[] uiBalken;

	// Token: 0x040019B0 RID: 6576
	public GameObject[] uiObjects;

	// Token: 0x040019B1 RID: 6577
	private GameObject main_;

	// Token: 0x040019B2 RID: 6578
	private mainScript mS_;

	// Token: 0x040019B3 RID: 6579
	private textScript tS_;

	// Token: 0x040019B4 RID: 6580
	private GUI_Main guiMain_;

	// Token: 0x040019B5 RID: 6581
	private sfxScript sfx_;

	// Token: 0x040019B6 RID: 6582
	private genres genres_;

	// Token: 0x040019B7 RID: 6583
	private engineFeatures eF_;

	// Token: 0x040019B8 RID: 6584
	private engineScript eS_;
}
