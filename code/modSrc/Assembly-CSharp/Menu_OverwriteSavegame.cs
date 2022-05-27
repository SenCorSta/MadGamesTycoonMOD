using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017D RID: 381
public class Menu_OverwriteSavegame : MonoBehaviour
{
	// Token: 0x06000E44 RID: 3652 RVA: 0x00009F96 File Offset: 0x00008196
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000A9144 File Offset: 0x000A7344
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

	// Token: 0x06000E46 RID: 3654 RVA: 0x00009F9E File Offset: 0x0000819E
	public void Init(int slot_, string saveGameName)
	{
		this.slot = slot_;
		this.uiObjects[0].GetComponent<Text>().text = saveGameName;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x00009FBA File Offset: 0x000081BA
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000A91F0 File Offset: 0x000A73F0
	public void BUTTON_Yes()
	{
		ES3.DeleteFile(this.mS_.GetSavegameTitle() + this.slot.ToString() + ".txt");
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[156].GetComponent<Menu_SaveGame>().BUTTON_SaveGame(this.slot);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040012C8 RID: 4808
	private ES3Writer writer;

	// Token: 0x040012C9 RID: 4809
	private ES3Reader reader;

	// Token: 0x040012CA RID: 4810
	public GameObject[] uiObjects;

	// Token: 0x040012CB RID: 4811
	private GameObject main_;

	// Token: 0x040012CC RID: 4812
	private mainScript mS_;

	// Token: 0x040012CD RID: 4813
	private textScript tS_;

	// Token: 0x040012CE RID: 4814
	private GUI_Main guiMain_;

	// Token: 0x040012CF RID: 4815
	private sfxScript sfx_;

	// Token: 0x040012D0 RID: 4816
	private int slot;
}
