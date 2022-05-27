using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000233 RID: 563
public class Menu_Stats_Downloadverlauf : MonoBehaviour
{
	// Token: 0x060015A3 RID: 5539 RVA: 0x0000EE39 File Offset: 0x0000D039
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x000E5B20 File Offset: 0x000E3D20
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

	// Token: 0x060015A5 RID: 5541 RVA: 0x0000EE41 File Offset: 0x0000D041
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x0000EE49 File Offset: 0x0000D049
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000E5C08 File Offset: 0x000E3E08
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000E5C5C File Offset: 0x000E3E5C
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

	// Token: 0x060015A9 RID: 5545 RVA: 0x0000EE5F File Offset: 0x0000D05F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019A6 RID: 6566
	public GameObject[] uiBalken;

	// Token: 0x040019A7 RID: 6567
	public GameObject[] uiObjects;

	// Token: 0x040019A8 RID: 6568
	private GameObject main_;

	// Token: 0x040019A9 RID: 6569
	private mainScript mS_;

	// Token: 0x040019AA RID: 6570
	private textScript tS_;

	// Token: 0x040019AB RID: 6571
	private GUI_Main guiMain_;

	// Token: 0x040019AC RID: 6572
	private sfxScript sfx_;

	// Token: 0x040019AD RID: 6573
	private genres genres_;

	// Token: 0x040019AE RID: 6574
	private engineFeatures eF_;

	// Token: 0x040019AF RID: 6575
	private engineScript eS_;
}
