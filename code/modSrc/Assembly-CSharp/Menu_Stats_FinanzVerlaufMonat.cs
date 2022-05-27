using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023A RID: 570
public class Menu_Stats_FinanzVerlaufMonat : MonoBehaviour
{
	// Token: 0x060015FB RID: 5627 RVA: 0x000E0CB5 File Offset: 0x000DEEB5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x000E0CC0 File Offset: 0x000DEEC0
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

	// Token: 0x060015FD RID: 5629 RVA: 0x000E0DA6 File Offset: 0x000DEFA6
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000E0DAE File Offset: 0x000DEFAE
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000E0DC4 File Offset: 0x000DEFC4
	public void Init()
	{
		this.FindScripts();
		long num = this.InitBalken();
		if (num >= 0L)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=green>" + this.mS_.GetMoney(num, true) + "</color>";
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=red>" + this.mS_.GetMoney(num, true) + "</color>";
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000E0E60 File Offset: 0x000DF060
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.finanzVerlauf.Length; i++)
		{
			num += this.mS_.finanzVerlauf[i];
			long num4 = this.mS_.finanzVerlauf[i];
			if (num4 < 0L)
			{
				num4 *= -1L;
			}
			if (num3 < num4)
			{
				num3 = num4;
			}
		}
		float num5 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.verkaufsverlauf.Length; j++)
		{
			long num6 = this.mS_.finanzVerlauf[j];
			if (num6 < 0L)
			{
				num6 *= -1L;
			}
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)num6 * num5 / num2;
			if (this.mS_.finanzVerlauf[j] < 0L)
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[5];
			}
			else
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.finanzVerlauf[j], true);
			this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "";
		}
		return num;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000E0FE1 File Offset: 0x000DF1E1
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019F6 RID: 6646
	public GameObject[] uiBalken;

	// Token: 0x040019F7 RID: 6647
	public GameObject[] uiObjects;

	// Token: 0x040019F8 RID: 6648
	private GameObject main_;

	// Token: 0x040019F9 RID: 6649
	private mainScript mS_;

	// Token: 0x040019FA RID: 6650
	private textScript tS_;

	// Token: 0x040019FB RID: 6651
	private GUI_Main guiMain_;

	// Token: 0x040019FC RID: 6652
	private sfxScript sfx_;

	// Token: 0x040019FD RID: 6653
	private genres genres_;

	// Token: 0x040019FE RID: 6654
	private engineFeatures eF_;

	// Token: 0x040019FF RID: 6655
	private engineScript eS_;
}
