using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000264 RID: 612
public class Menu_W_FirmaAufwertenPublisher : MonoBehaviour
{
	// Token: 0x060017B4 RID: 6068 RVA: 0x0001082A File Offset: 0x0000EA2A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x000F4B88 File Offset: 0x000F2D88
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

	// Token: 0x060017B6 RID: 6070 RVA: 0x000F4C34 File Offset: 0x000F2E34
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

	// Token: 0x060017B7 RID: 6071 RVA: 0x00010832 File Offset: 0x0000EA32
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x000F4D08 File Offset: 0x000F2F08
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

	// Token: 0x04001B92 RID: 7058
	public GameObject[] uiObjects;

	// Token: 0x04001B93 RID: 7059
	private publisherScript pS_;

	// Token: 0x04001B94 RID: 7060
	private GameObject main_;

	// Token: 0x04001B95 RID: 7061
	private mainScript mS_;

	// Token: 0x04001B96 RID: 7062
	private textScript tS_;

	// Token: 0x04001B97 RID: 7063
	private GUI_Main guiMain_;

	// Token: 0x04001B98 RID: 7064
	private sfxScript sfx_;

	// Token: 0x04001B99 RID: 7065
	public long costs;
}
