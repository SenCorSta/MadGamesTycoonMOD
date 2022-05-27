using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000124 RID: 292
public class Menu_DevGame_Zielgruppe : MonoBehaviour
{
	// Token: 0x06000A24 RID: 2596 RVA: 0x0006EDD6 File Offset: 0x0006CFD6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
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

	// Token: 0x06000A26 RID: 2598 RVA: 0x0006EECE File Offset: 0x0006D0CE
	private void OnEnable()
	{
		this.FindScripts();
		this.zielgruppe = this.mDevGame_.g_GameZielgruppe;
		this.UpdateGUI();
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0006EEF0 File Offset: 0x0006D0F0
	private void UpdateGUI()
	{
		this.uiObjects[1].GetComponent<Image>().color = Color.white;
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[4].GetComponent<Image>().color = Color.white;
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
		this.uiObjects[1 + this.zielgruppe].GetComponent<Image>().color = this.guiMain_.colors[4];
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0006EF9A File Offset: 0x0006D19A
	public void BUTTON_GameZielgruppe(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.zielgruppe = i;
		this.UpdateGUI();
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0006EFB6 File Offset: 0x0006D1B6
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.mDevGame_.SetZielgruppe(this.zielgruppe);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0006EFE2 File Offset: 0x0006D1E2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E6E RID: 3694
	public GameObject[] uiObjects;

	// Token: 0x04000E6F RID: 3695
	private GameObject main_;

	// Token: 0x04000E70 RID: 3696
	private mainScript mS_;

	// Token: 0x04000E71 RID: 3697
	private textScript tS_;

	// Token: 0x04000E72 RID: 3698
	private GUI_Main guiMain_;

	// Token: 0x04000E73 RID: 3699
	private sfxScript sfx_;

	// Token: 0x04000E74 RID: 3700
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E75 RID: 3701
	private games games_;

	// Token: 0x04000E76 RID: 3702
	private int zielgruppe;
}
