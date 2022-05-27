using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000235 RID: 565
public class Menu_Stats_Fanshopverlauf : MonoBehaviour
{
	// Token: 0x060015BC RID: 5564 RVA: 0x0000EF4A File Offset: 0x0000D14A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000E6624 File Offset: 0x000E4824
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

	// Token: 0x060015BE RID: 5566 RVA: 0x0000EF52 File Offset: 0x0000D152
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x0000EF5A File Offset: 0x0000D15A
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000E670C File Offset: 0x000E490C
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": " + this.mS_.GetMoney(i, true);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000E6760 File Offset: 0x000E4960
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

	// Token: 0x060015C2 RID: 5570 RVA: 0x0000EF70 File Offset: 0x0000D170
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019BB RID: 6587
	public GameObject[] uiBalken;

	// Token: 0x040019BC RID: 6588
	public GameObject[] uiObjects;

	// Token: 0x040019BD RID: 6589
	private GameObject main_;

	// Token: 0x040019BE RID: 6590
	private mainScript mS_;

	// Token: 0x040019BF RID: 6591
	private textScript tS_;

	// Token: 0x040019C0 RID: 6592
	private GUI_Main guiMain_;

	// Token: 0x040019C1 RID: 6593
	private sfxScript sfx_;

	// Token: 0x040019C2 RID: 6594
	private genres genres_;

	// Token: 0x040019C3 RID: 6595
	private engineFeatures eF_;

	// Token: 0x040019C4 RID: 6596
	private engineScript eS_;
}
