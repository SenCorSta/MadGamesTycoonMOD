using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000236 RID: 566
public class Menu_Stats_Fanshopverlauf : MonoBehaviour
{
	// Token: 0x060015DA RID: 5594 RVA: 0x000DE3D0 File Offset: 0x000DC5D0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000DE3D8 File Offset: 0x000DC5D8
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

	// Token: 0x060015DC RID: 5596 RVA: 0x000DE4BE File Offset: 0x000DC6BE
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x000DE4C6 File Offset: 0x000DC6C6
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000DE4DC File Offset: 0x000DC6DC
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": " + this.mS_.GetMoney(i, true);
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x000DE530 File Offset: 0x000DC730
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.fanshopverlauf.Length; i++)
		{
			num += this.mS_.fanshopverlauf[i];
			if (num3 < this.mS_.fanshopverlauf[i])
			{
				num3 = this.mS_.fanshopverlauf[i];
			}
		}
		float num4 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.fanshopverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.fanshopverlauf[j] * num4 / num2;
			if (this.mS_.fanshopverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.fanshopverlauf[j], false);
				if (j < this.mS_.fanshopverlauf.Length - 1)
				{
					long num5 = this.mS_.fanshopverlauf[j] - this.mS_.fanshopverlauf[j + 1];
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

	// Token: 0x060015E0 RID: 5600 RVA: 0x000DE77B File Offset: 0x000DC97B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019C4 RID: 6596
	public GameObject[] uiBalken;

	// Token: 0x040019C5 RID: 6597
	public GameObject[] uiObjects;

	// Token: 0x040019C6 RID: 6598
	private GameObject main_;

	// Token: 0x040019C7 RID: 6599
	private mainScript mS_;

	// Token: 0x040019C8 RID: 6600
	private textScript tS_;

	// Token: 0x040019C9 RID: 6601
	private GUI_Main guiMain_;

	// Token: 0x040019CA RID: 6602
	private sfxScript sfx_;

	// Token: 0x040019CB RID: 6603
	private genres genres_;

	// Token: 0x040019CC RID: 6604
	private engineFeatures eF_;

	// Token: 0x040019CD RID: 6605
	private engineScript eS_;
}
