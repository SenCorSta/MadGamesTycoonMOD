using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A4 RID: 420
public class Menu_W_PublisherKuendigen_MB : MonoBehaviour
{
	// Token: 0x06000FD2 RID: 4050 RVA: 0x000A7C6E File Offset: 0x000A5E6E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x000A7C78 File Offset: 0x000A5E78
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

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000A7D24 File Offset: 0x000A5F24
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		if (!this.pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.FindScripts();
		string text = this.tS_.GetText(1915);
		text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x000A7D9C File Offset: 0x000A5F9C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000A7DB8 File Offset: 0x000A5FB8
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		Menu_W_PublisherExklusivKuendigen component = this.guiMain_.uiObjects[382].GetComponent<Menu_W_PublisherExklusivKuendigen>();
		this.guiMain_.uiObjects[382].SetActive(false);
		this.mS_.Pay(component.GetStrafzahlung(), 14);
		this.pS_.relation = 0f;
		this.mS_.RemovePublisherExklusivVertrag();
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001448 RID: 5192
	public GameObject[] uiObjects;

	// Token: 0x04001449 RID: 5193
	private GameObject main_;

	// Token: 0x0400144A RID: 5194
	private mainScript mS_;

	// Token: 0x0400144B RID: 5195
	private textScript tS_;

	// Token: 0x0400144C RID: 5196
	private GUI_Main guiMain_;

	// Token: 0x0400144D RID: 5197
	private sfxScript sfx_;

	// Token: 0x0400144E RID: 5198
	private publisherScript pS_;
}
