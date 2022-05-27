using System;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class Menu_PersonalMotivation : MonoBehaviour
{
	// Token: 0x06001248 RID: 4680 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x000CDB44 File Offset: 0x000CBD44
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

	// Token: 0x0600124A RID: 4682 RVA: 0x000CDBF0 File Offset: 0x000CBDF0
	public void Init()
	{
		this.FindScripts();
		this.closeMenu = false;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				characterScript component = array[i].GetComponent<characterScript>();
				if (component)
				{
					component.AddMotivation(-10f);
				}
			}
		}
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x0000CAFB File Offset: 0x0000ACFB
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
			this.closeMenu = true;
		}
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x0000CB1D File Offset: 0x0000AD1D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0000CB4B File Offset: 0x0000AD4B
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016C5 RID: 5829
	public GameObject[] uiObjects;

	// Token: 0x040016C6 RID: 5830
	private GameObject main_;

	// Token: 0x040016C7 RID: 5831
	private mainScript mS_;

	// Token: 0x040016C8 RID: 5832
	private textScript tS_;

	// Token: 0x040016C9 RID: 5833
	private GUI_Main guiMain_;

	// Token: 0x040016CA RID: 5834
	private sfxScript sfx_;

	// Token: 0x040016CB RID: 5835
	private bool closeMenu;
}
