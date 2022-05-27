using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025E RID: 606
public class Menu_Stats_TochterfirmaUmsatz : MonoBehaviour
{
	// Token: 0x0600179F RID: 6047 RVA: 0x000ED1F9 File Offset: 0x000EB3F9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000ED204 File Offset: 0x000EB404
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

	// Token: 0x060017A1 RID: 6049 RVA: 0x000ED2B0 File Offset: 0x000EB4B0
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

	// Token: 0x060017A2 RID: 6050 RVA: 0x000ED364 File Offset: 0x000EB564
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

	// Token: 0x060017A3 RID: 6051 RVA: 0x000ED4E5 File Offset: 0x000EB6E5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B5E RID: 7006
	public GameObject[] uiBalken;

	// Token: 0x04001B5F RID: 7007
	public GameObject[] uiObjects;

	// Token: 0x04001B60 RID: 7008
	private GameObject main_;

	// Token: 0x04001B61 RID: 7009
	private mainScript mS_;

	// Token: 0x04001B62 RID: 7010
	private textScript tS_;

	// Token: 0x04001B63 RID: 7011
	private GUI_Main guiMain_;

	// Token: 0x04001B64 RID: 7012
	private sfxScript sfx_;

	// Token: 0x04001B65 RID: 7013
	private publisherScript pS_;
}
