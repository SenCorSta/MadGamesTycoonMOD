using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class Menu_W_UpdateAllObjects : MonoBehaviour
{
	// Token: 0x060011DA RID: 4570 RVA: 0x000BC4C0 File Offset: 0x000BA6C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000BC4C8 File Offset: 0x000BA6C8
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

	// Token: 0x060011DC RID: 4572 RVA: 0x000BC572 File Offset: 0x000BA772
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x000BC598 File Offset: 0x000BA798
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

	// Token: 0x04001655 RID: 5717
	public GameObject[] uiObjects;

	// Token: 0x04001656 RID: 5718
	private roomScript rS_;

	// Token: 0x04001657 RID: 5719
	private GameObject main_;

	// Token: 0x04001658 RID: 5720
	private mainScript mS_;

	// Token: 0x04001659 RID: 5721
	private textScript tS_;

	// Token: 0x0400165A RID: 5722
	private GUI_Main guiMain_;

	// Token: 0x0400165B RID: 5723
	private sfxScript sfx_;
}
