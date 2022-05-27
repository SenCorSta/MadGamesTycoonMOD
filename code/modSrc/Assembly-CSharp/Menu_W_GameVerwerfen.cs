using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000157 RID: 343
public class Menu_W_GameVerwerfen : MonoBehaviour
{
	// Token: 0x06000C97 RID: 3223 RVA: 0x00008CD7 File Offset: 0x00006ED7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00098C58 File Offset: 0x00096E58
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

	// Token: 0x06000C99 RID: 3225 RVA: 0x00008CDF File Offset: 0x00006EDF
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

	// Token: 0x06000C9A RID: 3226 RVA: 0x00008D15 File Offset: 0x00006F15
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00098D04 File Offset: 0x00096F04
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

	// Token: 0x0400111B RID: 4379
	public GameObject[] uiObjects;

	// Token: 0x0400111C RID: 4380
	private platformScript pS_;

	// Token: 0x0400111D RID: 4381
	private GameObject main_;

	// Token: 0x0400111E RID: 4382
	private mainScript mS_;

	// Token: 0x0400111F RID: 4383
	private textScript tS_;

	// Token: 0x04001120 RID: 4384
	private GUI_Main guiMain_;

	// Token: 0x04001121 RID: 4385
	private sfxScript sfx_;

	// Token: 0x04001122 RID: 4386
	private gameScript gS_;

	// Token: 0x04001123 RID: 4387
	private taskGame taskGame_;
}
