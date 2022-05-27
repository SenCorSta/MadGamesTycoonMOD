using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000212 RID: 530
public class Menu_W_ServerDown : MonoBehaviour
{
	// Token: 0x06001461 RID: 5217 RVA: 0x000D3DD0 File Offset: 0x000D1FD0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x000D3DD8 File Offset: 0x000D1FD8
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

	// Token: 0x06001463 RID: 5219 RVA: 0x000D3E84 File Offset: 0x000D2084
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

	// Token: 0x06001464 RID: 5220 RVA: 0x000D3EFD File Offset: 0x000D20FD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x000D3F23 File Offset: 0x000D2123
	public void BUTTON_Yes()
	{
		if (this.rS_)
		{
			this.rS_.ServerAbschalten(!this.rS_.serverDown);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400186C RID: 6252
	public GameObject[] uiObjects;

	// Token: 0x0400186D RID: 6253
	private roomScript rS_;

	// Token: 0x0400186E RID: 6254
	private GameObject main_;

	// Token: 0x0400186F RID: 6255
	private mainScript mS_;

	// Token: 0x04001870 RID: 6256
	private textScript tS_;

	// Token: 0x04001871 RID: 6257
	private GUI_Main guiMain_;

	// Token: 0x04001872 RID: 6258
	private sfxScript sfx_;
}
