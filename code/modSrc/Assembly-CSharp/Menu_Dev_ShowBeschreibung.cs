using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000144 RID: 324
public class Menu_Dev_ShowBeschreibung : MonoBehaviour
{
	// Token: 0x06000BD2 RID: 3026 RVA: 0x000085A7 File Offset: 0x000067A7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0008FD4C File Offset: 0x0008DF4C
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0008FE1C File Offset: 0x0008E01C
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		if (this.gS_ == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung.Length <= 0)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		this.uiObjects[1].GetComponent<InputField>().text = this.gS_.beschreibung;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000085AF File Offset: 0x000067AF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000085CA File Offset: 0x000067CA
	public void BUTTON_OK()
	{
		this.BUTTON_Close();
	}

	// Token: 0x04001023 RID: 4131
	public GameObject[] uiObjects;

	// Token: 0x04001024 RID: 4132
	private GameObject main_;

	// Token: 0x04001025 RID: 4133
	private mainScript mS_;

	// Token: 0x04001026 RID: 4134
	private textScript tS_;

	// Token: 0x04001027 RID: 4135
	private GUI_Main guiMain_;

	// Token: 0x04001028 RID: 4136
	private sfxScript sfx_;

	// Token: 0x04001029 RID: 4137
	private Menu_DevGame mDevGame_;

	// Token: 0x0400102A RID: 4138
	private gameScript gS_;
}
