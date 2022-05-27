using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class Menu_PersonalMotivation : MonoBehaviour
{
	// Token: 0x06001263 RID: 4707 RVA: 0x000C2EA2 File Offset: 0x000C10A2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x000C2EAC File Offset: 0x000C10AC
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

	// Token: 0x06001265 RID: 4709 RVA: 0x000C2F58 File Offset: 0x000C1158
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

	// Token: 0x06001266 RID: 4710 RVA: 0x000C2FB1 File Offset: 0x000C11B1
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
			this.closeMenu = true;
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000C2FD3 File Offset: 0x000C11D3
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000C3001 File Offset: 0x000C1201
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016CE RID: 5838
	public GameObject[] uiObjects;

	// Token: 0x040016CF RID: 5839
	private GameObject main_;

	// Token: 0x040016D0 RID: 5840
	private mainScript mS_;

	// Token: 0x040016D1 RID: 5841
	private textScript tS_;

	// Token: 0x040016D2 RID: 5842
	private GUI_Main guiMain_;

	// Token: 0x040016D3 RID: 5843
	private sfxScript sfx_;

	// Token: 0x040016D4 RID: 5844
	private bool closeMenu;
}
