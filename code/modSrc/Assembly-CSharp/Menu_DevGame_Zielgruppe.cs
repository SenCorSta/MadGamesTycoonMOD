using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000123 RID: 291
public class Menu_DevGame_Zielgruppe : MonoBehaviour
{
	// Token: 0x06000A15 RID: 2581 RVA: 0x00007491 File Offset: 0x00005691
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0007F8D0 File Offset: 0x0007DAD0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00007499 File Offset: 0x00005699
	private void OnEnable()
	{
		this.FindScripts();
		this.zielgruppe = this.mDevGame_.g_GameZielgruppe;
		this.UpdateGUI();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0007F9C0 File Offset: 0x0007DBC0
	private void UpdateGUI()
	{
		genres component = this.main_.GetComponent<genres>();
		for (int i = 0; i < 5; i++)
		{
			this.uiObjects[i + 1].GetComponent<Image>().color = (component.genres_TARGETGROUP[this.mDevGame_.g_GameMainGenre, i] ? Color.green : Color.red);
		}
		this.uiObjects[1 + this.zielgruppe].GetComponent<Image>().color = this.guiMain_.colors[4];
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x000074B8 File Offset: 0x000056B8
	public void BUTTON_GameZielgruppe(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.zielgruppe = i;
		this.UpdateGUI();
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x000074D4 File Offset: 0x000056D4
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.mDevGame_.SetZielgruppe(this.zielgruppe);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00007500 File Offset: 0x00005700
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E66 RID: 3686
	public GameObject[] uiObjects;

	// Token: 0x04000E67 RID: 3687
	private GameObject main_;

	// Token: 0x04000E68 RID: 3688
	private mainScript mS_;

	// Token: 0x04000E69 RID: 3689
	private textScript tS_;

	// Token: 0x04000E6A RID: 3690
	private GUI_Main guiMain_;

	// Token: 0x04000E6B RID: 3691
	private sfxScript sfx_;

	// Token: 0x04000E6C RID: 3692
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E6D RID: 3693
	private games games_;

	// Token: 0x04000E6E RID: 3694
	private int zielgruppe;
}
