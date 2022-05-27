using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000266 RID: 614
public class Menu_W_FirmaAufwerten : MonoBehaviour
{
	// Token: 0x060017EB RID: 6123 RVA: 0x000EEFD9 File Offset: 0x000ED1D9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x000EEFE4 File Offset: 0x000ED1E4
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

	// Token: 0x060017ED RID: 6125 RVA: 0x000EF090 File Offset: 0x000ED290
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_)
		{
			int starsAmount = this.pS_.GetStarsAmount();
			string text = this.tS_.GetText(1938);
			text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
			text = text.Replace("<NUM>", "<color=blue>" + this.mS_.GetMoney(this.costs[starsAmount], true) + "</color>");
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			this.guiMain_.DrawStarsColor(this.uiObjects[2], starsAmount, Color.white);
			return;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000EF17C File Offset: 0x000ED37C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000EF198 File Offset: 0x000ED398
	public void BUTTON_Yes()
	{
		if (this.pS_)
		{
			int starsAmount = this.pS_.GetStarsAmount();
			if (this.mS_.money < this.costs[starsAmount])
			{
				this.guiMain_.ShowNoMoney();
				return;
			}
			this.mS_.Pay(this.costs[starsAmount], 29);
			this.pS_.stars = (float)(starsAmount * 20 + 20);
			if (this.guiMain_.uiObjects[387].activeSelf)
			{
				this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().UpdateData();
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001B9C RID: 7068
	public GameObject[] uiObjects;

	// Token: 0x04001B9D RID: 7069
	private publisherScript pS_;

	// Token: 0x04001B9E RID: 7070
	private GameObject main_;

	// Token: 0x04001B9F RID: 7071
	private mainScript mS_;

	// Token: 0x04001BA0 RID: 7072
	private textScript tS_;

	// Token: 0x04001BA1 RID: 7073
	private GUI_Main guiMain_;

	// Token: 0x04001BA2 RID: 7074
	private sfxScript sfx_;

	// Token: 0x04001BA3 RID: 7075
	public long[] costs;
}
