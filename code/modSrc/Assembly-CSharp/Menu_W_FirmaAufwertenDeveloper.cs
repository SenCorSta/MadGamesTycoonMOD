using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000267 RID: 615
public class Menu_W_FirmaAufwertenDeveloper : MonoBehaviour
{
	// Token: 0x060017F1 RID: 6129 RVA: 0x000EF26F File Offset: 0x000ED46F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000EF278 File Offset: 0x000ED478
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

	// Token: 0x060017F3 RID: 6131 RVA: 0x000EF324 File Offset: 0x000ED524
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

	// Token: 0x060017F4 RID: 6132 RVA: 0x000EF3F5 File Offset: 0x000ED5F5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x000EF410 File Offset: 0x000ED610
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

	// Token: 0x04001BA4 RID: 7076
	public GameObject[] uiObjects;

	// Token: 0x04001BA5 RID: 7077
	private publisherScript pS_;

	// Token: 0x04001BA6 RID: 7078
	private GameObject main_;

	// Token: 0x04001BA7 RID: 7079
	private mainScript mS_;

	// Token: 0x04001BA8 RID: 7080
	private textScript tS_;

	// Token: 0x04001BA9 RID: 7081
	private GUI_Main guiMain_;

	// Token: 0x04001BAA RID: 7082
	private sfxScript sfx_;

	// Token: 0x04001BAB RID: 7083
	public long costs;
}
