using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022E RID: 558
public class Menu_Stats_Aboverlauf : MonoBehaviour
{
	// Token: 0x06001572 RID: 5490 RVA: 0x0000EC3F File Offset: 0x0000CE3F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x000E46DC File Offset: 0x000E28DC
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

	// Token: 0x06001574 RID: 5492 RVA: 0x0000EC47 File Offset: 0x0000CE47
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x0000EC4F File Offset: 0x0000CE4F
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000E47C4 File Offset: 0x000E29C4
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

	// Token: 0x06001577 RID: 5495 RVA: 0x000E487C File Offset: 0x000E2A7C
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

	// Token: 0x06001578 RID: 5496 RVA: 0x0000EC65 File Offset: 0x0000CE65
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001978 RID: 6520
	public GameObject[] uiBalken;

	// Token: 0x04001979 RID: 6521
	public GameObject[] uiObjects;

	// Token: 0x0400197A RID: 6522
	private GameObject main_;

	// Token: 0x0400197B RID: 6523
	private mainScript mS_;

	// Token: 0x0400197C RID: 6524
	private textScript tS_;

	// Token: 0x0400197D RID: 6525
	private GUI_Main guiMain_;

	// Token: 0x0400197E RID: 6526
	private sfxScript sfx_;

	// Token: 0x0400197F RID: 6527
	private genres genres_;

	// Token: 0x04001980 RID: 6528
	private engineFeatures eF_;

	// Token: 0x04001981 RID: 6529
	private engineScript eS_;
}
