using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class Menu_DeleteAllSaveGames : MonoBehaviour
{
	// Token: 0x06000DA1 RID: 3489 RVA: 0x00009770 File Offset: 0x00007970
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x000A2F70 File Offset: 0x000A1170
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

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00009778 File Offset: 0x00007978
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x000A301C File Offset: 0x000A121C
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

	// Token: 0x0400124A RID: 4682
	private ES3Writer writer;

	// Token: 0x0400124B RID: 4683
	private ES3Reader reader;

	// Token: 0x0400124C RID: 4684
	public GameObject[] uiObjects;

	// Token: 0x0400124D RID: 4685
	private GameObject main_;

	// Token: 0x0400124E RID: 4686
	private mainScript mS_;

	// Token: 0x0400124F RID: 4687
	private textScript tS_;

	// Token: 0x04001250 RID: 4688
	private GUI_Main guiMain_;

	// Token: 0x04001251 RID: 4689
	private sfxScript sfx_;

	// Token: 0x04001252 RID: 4690
	private int slot;
}
