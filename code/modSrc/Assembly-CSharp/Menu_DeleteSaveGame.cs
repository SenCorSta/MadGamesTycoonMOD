using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000172 RID: 370
public class Menu_DeleteSaveGame : MonoBehaviour
{
	// Token: 0x06000DBE RID: 3518 RVA: 0x00094C00 File Offset: 0x00092E00
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00094C08 File Offset: 0x00092E08
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

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00094CB2 File Offset: 0x00092EB2
	public void Init(int slot_, string saveGameName)
	{
		this.slot = slot_;
		this.uiObjects[0].GetComponent<Text>().text = saveGameName;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00094CCE File Offset: 0x00092ECE
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00094CEC File Offset: 0x00092EEC
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		ES3.DeleteFile(this.mS_.GetSavegameTitle() + this.slot.ToString() + ".txt");
		if (this.guiMain_.uiObjects[150].activeSelf)
		{
			this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().Init();
		}
		if (this.guiMain_.uiObjects[156].activeSelf)
		{
			this.guiMain_.uiObjects[156].GetComponent<Menu_SaveGame>().Init();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400125B RID: 4699
	private ES3Writer writer;

	// Token: 0x0400125C RID: 4700
	private ES3Reader reader;

	// Token: 0x0400125D RID: 4701
	public GameObject[] uiObjects;

	// Token: 0x0400125E RID: 4702
	private GameObject main_;

	// Token: 0x0400125F RID: 4703
	private mainScript mS_;

	// Token: 0x04001260 RID: 4704
	private textScript tS_;

	// Token: 0x04001261 RID: 4705
	private GUI_Main guiMain_;

	// Token: 0x04001262 RID: 4706
	private sfxScript sfx_;

	// Token: 0x04001263 RID: 4707
	private int slot;
}
