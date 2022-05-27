using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class Menu_W_UpdateAllObjects : MonoBehaviour
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x000C7648 File Offset: 0x000C5848
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

	// Token: 0x060011C2 RID: 4546 RVA: 0x0000C6AD File Offset: 0x0000A8AD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000C76F4 File Offset: 0x000C58F4
	public void BUTTON_Yes()
	{
		this.FindScripts();
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				this.rS_ = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (this.rS_)
				{
					this.rS_.UpdateInventar(true);
				}
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400164C RID: 5708
	public GameObject[] uiObjects;

	// Token: 0x0400164D RID: 5709
	private roomScript rS_;

	// Token: 0x0400164E RID: 5710
	private GameObject main_;

	// Token: 0x0400164F RID: 5711
	private mainScript mS_;

	// Token: 0x04001650 RID: 5712
	private textScript tS_;

	// Token: 0x04001651 RID: 5713
	private GUI_Main guiMain_;

	// Token: 0x04001652 RID: 5714
	private sfxScript sfx_;
}
