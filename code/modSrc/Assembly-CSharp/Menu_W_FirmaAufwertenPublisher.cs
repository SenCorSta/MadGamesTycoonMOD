using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000268 RID: 616
public class Menu_W_FirmaAufwertenPublisher : MonoBehaviour
{
	// Token: 0x060017F7 RID: 6135 RVA: 0x000EF4BC File Offset: 0x000ED6BC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000EF4C4 File Offset: 0x000ED6C4
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

	// Token: 0x060017F9 RID: 6137 RVA: 0x000EF570 File Offset: 0x000ED770
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_)
		{
			this.pS_.GetStarsAmount();
			string text = this.tS_.GetText(1939);
			text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
			text = text.Replace("<NUM>", "<color=blue>" + this.mS_.GetMoney(this.costs, true) + "</color>");
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			return;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000EF641 File Offset: 0x000ED841
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000EF65C File Offset: 0x000ED85C
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
			this.pS_.publisher = true;
			this.pS_.tf_publisher = true;
			if (this.guiMain_.uiObjects[387].activeSelf)
			{
				this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().UpdateData();
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001BAC RID: 7084
	public GameObject[] uiObjects;

	// Token: 0x04001BAD RID: 7085
	private publisherScript pS_;

	// Token: 0x04001BAE RID: 7086
	private GameObject main_;

	// Token: 0x04001BAF RID: 7087
	private mainScript mS_;

	// Token: 0x04001BB0 RID: 7088
	private textScript tS_;

	// Token: 0x04001BB1 RID: 7089
	private GUI_Main guiMain_;

	// Token: 0x04001BB2 RID: 7090
	private sfxScript sfx_;

	// Token: 0x04001BB3 RID: 7091
	public long costs;
}
