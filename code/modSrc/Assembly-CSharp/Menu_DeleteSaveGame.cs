using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000171 RID: 369
public class Menu_DeleteSaveGame : MonoBehaviour
{
	// Token: 0x06000DA6 RID: 3494 RVA: 0x00009793 File Offset: 0x00007993
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x000A30D0 File Offset: 0x000A12D0
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

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0000979B File Offset: 0x0000799B
	public void Init(int slot_, string saveGameName)
	{
		this.slot = slot_;
		this.uiObjects[0].GetComponent<Text>().text = saveGameName;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x000097B7 File Offset: 0x000079B7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x000A317C File Offset: 0x000A137C
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

	// Token: 0x04001253 RID: 4691
	private ES3Writer writer;

	// Token: 0x04001254 RID: 4692
	private ES3Reader reader;

	// Token: 0x04001255 RID: 4693
	public GameObject[] uiObjects;

	// Token: 0x04001256 RID: 4694
	private GameObject main_;

	// Token: 0x04001257 RID: 4695
	private mainScript mS_;

	// Token: 0x04001258 RID: 4696
	private textScript tS_;

	// Token: 0x04001259 RID: 4697
	private GUI_Main guiMain_;

	// Token: 0x0400125A RID: 4698
	private sfxScript sfx_;

	// Token: 0x0400125B RID: 4699
	private int slot;
}
