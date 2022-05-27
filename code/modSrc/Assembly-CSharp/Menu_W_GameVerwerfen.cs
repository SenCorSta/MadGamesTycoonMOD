using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000158 RID: 344
public class Menu_W_GameVerwerfen : MonoBehaviour
{
	// Token: 0x06000CAD RID: 3245 RVA: 0x00089BCF File Offset: 0x00087DCF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00089BD8 File Offset: 0x00087DD8
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00089C82 File Offset: 0x00087E82
	public void Init(gameScript script_, taskGame task_)
	{
		if (!script_)
		{
			return;
		}
		this.gS_ = script_;
		this.taskGame_ = task_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00089CB8 File Offset: 0x00087EB8
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00089CD4 File Offset: 0x00087ED4
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		if (this.taskGame_)
		{
			this.taskGame_.Abbrechen();
		}
		else
		{
			UnityEngine.Object.Destroy(this.gS_.gameObject);
		}
		base.gameObject.SetActive(false);
		if (this.guiMain_.uiObjects[69].activeSelf)
		{
			this.guiMain_.uiObjects[69].SetActive(false);
		}
		if (this.guiMain_.uiObjects[397].activeSelf)
		{
			this.guiMain_.uiObjects[397].SetActive(false);
		}
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04001123 RID: 4387
	public GameObject[] uiObjects;

	// Token: 0x04001124 RID: 4388
	private platformScript pS_;

	// Token: 0x04001125 RID: 4389
	private GameObject main_;

	// Token: 0x04001126 RID: 4390
	private mainScript mS_;

	// Token: 0x04001127 RID: 4391
	private textScript tS_;

	// Token: 0x04001128 RID: 4392
	private GUI_Main guiMain_;

	// Token: 0x04001129 RID: 4393
	private sfxScript sfx_;

	// Token: 0x0400112A RID: 4394
	private gameScript gS_;

	// Token: 0x0400112B RID: 4395
	private taskGame taskGame_;
}
