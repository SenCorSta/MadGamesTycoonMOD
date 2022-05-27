using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000262 RID: 610
public class Menu_W_FirmaAufwerten : MonoBehaviour
{
	// Token: 0x060017A8 RID: 6056 RVA: 0x000107E4 File Offset: 0x0000E9E4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x000F4718 File Offset: 0x000F2918
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

	// Token: 0x060017AA RID: 6058 RVA: 0x000F47C4 File Offset: 0x000F29C4
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

	// Token: 0x060017AB RID: 6059 RVA: 0x000107EC File Offset: 0x0000E9EC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000F48B0 File Offset: 0x000F2AB0
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

	// Token: 0x04001B82 RID: 7042
	public GameObject[] uiObjects;

	// Token: 0x04001B83 RID: 7043
	private publisherScript pS_;

	// Token: 0x04001B84 RID: 7044
	private GameObject main_;

	// Token: 0x04001B85 RID: 7045
	private mainScript mS_;

	// Token: 0x04001B86 RID: 7046
	private textScript tS_;

	// Token: 0x04001B87 RID: 7047
	private GUI_Main guiMain_;

	// Token: 0x04001B88 RID: 7048
	private sfxScript sfx_;

	// Token: 0x04001B89 RID: 7049
	public long[] costs;
}
