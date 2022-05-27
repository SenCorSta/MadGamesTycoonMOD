using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class Menu_DeleteAllSaveGames : MonoBehaviour
{
	// Token: 0x06000DB9 RID: 3513 RVA: 0x00094A79 File Offset: 0x00092C79
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00094A84 File Offset: 0x00092C84
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

	// Token: 0x06000DBB RID: 3515 RVA: 0x00094B2E File Offset: 0x00092D2E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00094B4C File Offset: 0x00092D4C
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i <= 20; i++)
		{
			ES3.DeleteFile(this.mS_.GetSavegameTitle() + i.ToString() + ".txt");
		}
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

	// Token: 0x04001252 RID: 4690
	private ES3Writer writer;

	// Token: 0x04001253 RID: 4691
	private ES3Reader reader;

	// Token: 0x04001254 RID: 4692
	public GameObject[] uiObjects;

	// Token: 0x04001255 RID: 4693
	private GameObject main_;

	// Token: 0x04001256 RID: 4694
	private mainScript mS_;

	// Token: 0x04001257 RID: 4695
	private textScript tS_;

	// Token: 0x04001258 RID: 4696
	private GUI_Main guiMain_;

	// Token: 0x04001259 RID: 4697
	private sfxScript sfx_;

	// Token: 0x0400125A RID: 4698
	private int slot;
}
