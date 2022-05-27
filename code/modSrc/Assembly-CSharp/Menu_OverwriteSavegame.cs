using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class Menu_OverwriteSavegame : MonoBehaviour
{
	// Token: 0x06000E5C RID: 3676 RVA: 0x0009B54C File Offset: 0x0009974C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0009B554 File Offset: 0x00099754
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

	// Token: 0x06000E5E RID: 3678 RVA: 0x0009B5FE File Offset: 0x000997FE
	public void Init(int slot_, string saveGameName)
	{
		this.slot = slot_;
		this.uiObjects[0].GetComponent<Text>().text = saveGameName;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0009B61A File Offset: 0x0009981A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0009B638 File Offset: 0x00099838
	public void BUTTON_Yes()
	{
		ES3.DeleteFile(this.mS_.GetSavegameTitle() + this.slot.ToString() + ".txt");
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[156].GetComponent<Menu_SaveGame>().BUTTON_SaveGame(this.slot);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040012D1 RID: 4817
	private ES3Writer writer;

	// Token: 0x040012D2 RID: 4818
	private ES3Reader reader;

	// Token: 0x040012D3 RID: 4819
	public GameObject[] uiObjects;

	// Token: 0x040012D4 RID: 4820
	private GameObject main_;

	// Token: 0x040012D5 RID: 4821
	private mainScript mS_;

	// Token: 0x040012D6 RID: 4822
	private textScript tS_;

	// Token: 0x040012D7 RID: 4823
	private GUI_Main guiMain_;

	// Token: 0x040012D8 RID: 4824
	private sfxScript sfx_;

	// Token: 0x040012D9 RID: 4825
	private int slot;
}
