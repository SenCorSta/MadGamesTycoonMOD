using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000239 RID: 569
public class Menu_Stats_FinanzVerlaufMonat : MonoBehaviour
{
	// Token: 0x060015DD RID: 5597 RVA: 0x0000F07F File Offset: 0x0000D27F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000E8DDC File Offset: 0x000E6FDC
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

	// Token: 0x060015DF RID: 5599 RVA: 0x0000F087 File Offset: 0x0000D287
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x0000F08F File Offset: 0x0000D28F
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x000E8EC4 File Offset: 0x000E70C4
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

	// Token: 0x060015E2 RID: 5602 RVA: 0x000E8F60 File Offset: 0x000E7160
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

	// Token: 0x060015E3 RID: 5603 RVA: 0x0000F0A5 File Offset: 0x0000D2A5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019ED RID: 6637
	public GameObject[] uiBalken;

	// Token: 0x040019EE RID: 6638
	public GameObject[] uiObjects;

	// Token: 0x040019EF RID: 6639
	private GameObject main_;

	// Token: 0x040019F0 RID: 6640
	private mainScript mS_;

	// Token: 0x040019F1 RID: 6641
	private textScript tS_;

	// Token: 0x040019F2 RID: 6642
	private GUI_Main guiMain_;

	// Token: 0x040019F3 RID: 6643
	private sfxScript sfx_;

	// Token: 0x040019F4 RID: 6644
	private genres genres_;

	// Token: 0x040019F5 RID: 6645
	private engineFeatures eF_;

	// Token: 0x040019F6 RID: 6646
	private engineScript eS_;
}
