using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022F RID: 559
public class Menu_Stats_Aboverlauf : MonoBehaviour
{
	// Token: 0x06001590 RID: 5520 RVA: 0x000DBF27 File Offset: 0x000DA127
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x000DBF30 File Offset: 0x000DA130
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

	// Token: 0x06001592 RID: 5522 RVA: 0x000DC016 File Offset: 0x000DA216
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000DC01E File Offset: 0x000DA21E
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000DC034 File Offset: 0x000DA234
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

	// Token: 0x06001595 RID: 5525 RVA: 0x000DC0EC File Offset: 0x000DA2EC
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

	// Token: 0x06001596 RID: 5526 RVA: 0x000DC31C File Offset: 0x000DA51C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400197F RID: 6527
	public GameObject[] uiBalken;

	// Token: 0x04001980 RID: 6528
	public GameObject[] uiObjects;

	// Token: 0x04001981 RID: 6529
	private GameObject main_;

	// Token: 0x04001982 RID: 6530
	private mainScript mS_;

	// Token: 0x04001983 RID: 6531
	private textScript tS_;

	// Token: 0x04001984 RID: 6532
	private GUI_Main guiMain_;

	// Token: 0x04001985 RID: 6533
	private sfxScript sfx_;

	// Token: 0x04001986 RID: 6534
	private genres genres_;

	// Token: 0x04001987 RID: 6535
	private engineFeatures eF_;

	// Token: 0x04001988 RID: 6536
	private engineScript eS_;
}
