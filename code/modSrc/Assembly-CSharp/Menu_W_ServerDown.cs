using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000211 RID: 529
public class Menu_W_ServerDown : MonoBehaviour
{
	// Token: 0x06001444 RID: 5188 RVA: 0x0000DCC3 File Offset: 0x0000BEC3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000DD740 File Offset: 0x000DB940
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

	// Token: 0x06001446 RID: 5190 RVA: 0x000DD7EC File Offset: 0x000DB9EC
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		if (this.rS_)
		{
			if (!this.rS_.serverDown)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1240);
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1241);
		}
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0000DCCB File Offset: 0x0000BECB
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0000DCF1 File Offset: 0x0000BEF1
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.ServerAbschalten(!this.rS_.serverDown);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001863 RID: 6243
	public GameObject[] uiObjects;

	// Token: 0x04001864 RID: 6244
	private roomScript rS_;

	// Token: 0x04001865 RID: 6245
	private GameObject main_;

	// Token: 0x04001866 RID: 6246
	private mainScript mS_;

	// Token: 0x04001867 RID: 6247
	private textScript tS_;

	// Token: 0x04001868 RID: 6248
	private GUI_Main guiMain_;

	// Token: 0x04001869 RID: 6249
	private sfxScript sfx_;
}
