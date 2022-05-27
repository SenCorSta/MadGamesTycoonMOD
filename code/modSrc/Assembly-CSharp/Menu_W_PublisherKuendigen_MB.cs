using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A3 RID: 419
public class Menu_W_PublisherKuendigen_MB : MonoBehaviour
{
	// Token: 0x06000FBA RID: 4026 RVA: 0x0000B291 File Offset: 0x00009491
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000B4548 File Offset: 0x000B2748
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

	// Token: 0x06000FBC RID: 4028 RVA: 0x000B45F4 File Offset: 0x000B27F4
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

	// Token: 0x06000FBD RID: 4029 RVA: 0x0000B299 File Offset: 0x00009499
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000B466C File Offset: 0x000B286C
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

	// Token: 0x0400143F RID: 5183
	public GameObject[] uiObjects;

	// Token: 0x04001440 RID: 5184
	private GameObject main_;

	// Token: 0x04001441 RID: 5185
	private mainScript mS_;

	// Token: 0x04001442 RID: 5186
	private textScript tS_;

	// Token: 0x04001443 RID: 5187
	private GUI_Main guiMain_;

	// Token: 0x04001444 RID: 5188
	private sfxScript sfx_;

	// Token: 0x04001445 RID: 5189
	private publisherScript pS_;
}
