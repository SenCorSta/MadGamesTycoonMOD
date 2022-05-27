using System;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class Menu_Statistics_DevPubsMain : MonoBehaviour
{
	// Token: 0x06001541 RID: 5441 RVA: 0x000D98B0 File Offset: 0x000D7AB0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000D98B8 File Offset: 0x000D7AB8
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

	// Token: 0x06001543 RID: 5443 RVA: 0x000D9962 File Offset: 0x000D7B62
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000D9988 File Offset: 0x000D7B88
	public void BUTTON_Publisher()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[119]);
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000D99B0 File Offset: 0x000D7BB0
	public void BUTTON_Entwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[120]);
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000D99D8 File Offset: 0x000D7BD8
	public void BUTTON_Tochterfirmen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[385]);
	}

	// Token: 0x04001933 RID: 6451
	public GameObject[] uiObjects;

	// Token: 0x04001934 RID: 6452
	private roomScript rS_;

	// Token: 0x04001935 RID: 6453
	private GameObject main_;

	// Token: 0x04001936 RID: 6454
	private mainScript mS_;

	// Token: 0x04001937 RID: 6455
	private textScript tS_;

	// Token: 0x04001938 RID: 6456
	private GUI_Main guiMain_;

	// Token: 0x04001939 RID: 6457
	private sfxScript sfx_;
}
