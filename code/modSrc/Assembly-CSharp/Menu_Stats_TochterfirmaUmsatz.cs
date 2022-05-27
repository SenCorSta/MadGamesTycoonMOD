using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025A RID: 602
public class Menu_Stats_TochterfirmaUmsatz : MonoBehaviour
{
	// Token: 0x0600175F RID: 5983 RVA: 0x00010536 File Offset: 0x0000E736
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000F2D6C File Offset: 0x000F0F6C
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
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000F2E18 File Offset: 0x000F1018
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_)
		{
			long num = this.InitBalken();
			if (num >= 0L)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=green>" + this.mS_.GetMoney(num, true) + "</color>";
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(724) + ": <color=red>" + this.mS_.GetMoney(num, true) + "</color>";
		}
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000F2ECC File Offset: 0x000F10CC
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.pS_.tf_umsatz.Length; i++)
		{
			num += this.pS_.tf_umsatz[i];
			long num4 = this.pS_.tf_umsatz[i];
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
		for (int j = 0; j < this.pS_.tf_umsatz.Length; j++)
		{
			long num6 = this.pS_.tf_umsatz[j];
			if (num6 < 0L)
			{
				num6 *= -1L;
			}
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)num6 * num5 / num2;
			if (this.pS_.tf_umsatz[j] < 0L)
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[5];
			}
			else
			{
				this.uiBalken[j].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.pS_.tf_umsatz[j], true);
			this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "";
		}
		return num;
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x0001053E File Offset: 0x0000E73E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B44 RID: 6980
	public GameObject[] uiBalken;

	// Token: 0x04001B45 RID: 6981
	public GameObject[] uiObjects;

	// Token: 0x04001B46 RID: 6982
	private GameObject main_;

	// Token: 0x04001B47 RID: 6983
	private mainScript mS_;

	// Token: 0x04001B48 RID: 6984
	private textScript tS_;

	// Token: 0x04001B49 RID: 6985
	private GUI_Main guiMain_;

	// Token: 0x04001B4A RID: 6986
	private sfxScript sfx_;

	// Token: 0x04001B4B RID: 6987
	private publisherScript pS_;
}
