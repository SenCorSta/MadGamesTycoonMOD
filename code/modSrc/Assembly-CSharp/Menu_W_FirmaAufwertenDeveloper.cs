using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000263 RID: 611
public class Menu_W_FirmaAufwertenDeveloper : MonoBehaviour
{
	// Token: 0x060017AE RID: 6062 RVA: 0x00010807 File Offset: 0x0000EA07
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x000F495C File Offset: 0x000F2B5C
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

	// Token: 0x060017B0 RID: 6064 RVA: 0x000F4A08 File Offset: 0x000F2C08
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_)
		{
			this.pS_.GetStarsAmount();
			string text = this.tS_.GetText(1940);
			text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
			text = text.Replace("<NUM>", "<color=blue>" + this.mS_.GetMoney(this.costs, true) + "</color>");
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			return;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x0001080F File Offset: 0x0000EA0F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000F4ADC File Offset: 0x000F2CDC
	public void BUTTON_Yes()
	{
		if (this.pS_)
		{
			this.pS_.GetStarsAmount();
			if (this.mS_.money < this.costs)
			{
				this.guiMain_.ShowNoMoney();
				return;
			}
			this.mS_.Pay(this.costs, 29);
			this.pS_.developer = true;
			this.pS_.tf_developer = true;
			if (this.guiMain_.uiObjects[387].activeSelf)
			{
				this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().UpdateData();
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001B8A RID: 7050
	public GameObject[] uiObjects;

	// Token: 0x04001B8B RID: 7051
	private publisherScript pS_;

	// Token: 0x04001B8C RID: 7052
	private GameObject main_;

	// Token: 0x04001B8D RID: 7053
	private mainScript mS_;

	// Token: 0x04001B8E RID: 7054
	private textScript tS_;

	// Token: 0x04001B8F RID: 7055
	private GUI_Main guiMain_;

	// Token: 0x04001B90 RID: 7056
	private sfxScript sfx_;

	// Token: 0x04001B91 RID: 7057
	public long costs;
}
