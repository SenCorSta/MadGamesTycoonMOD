using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016D RID: 365
public class Menu_W_Dev_KonsoleVerwerfen : MonoBehaviour
{
	// Token: 0x06000DA5 RID: 3493 RVA: 0x00094615 File Offset: 0x00092815
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00094620 File Offset: 0x00092820
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

	// Token: 0x06000DA7 RID: 3495 RVA: 0x000946CA File Offset: 0x000928CA
	public void Init(platformScript script_, taskKonsole task_)
	{
		if (!script_)
		{
			return;
		}
		if (!task_)
		{
			return;
		}
		this.pS_ = script_;
		this.taskKonsole_ = task_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00094709 File Offset: 0x00092909
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00094724 File Offset: 0x00092924
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("PLATFORM_" + this.taskKonsole_.konsoleID.ToString());
		if (gameObject)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		UnityEngine.Object.Destroy(this.taskKonsole_.gameObject);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[326].SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04001238 RID: 4664
	public GameObject[] uiObjects;

	// Token: 0x04001239 RID: 4665
	private GameObject main_;

	// Token: 0x0400123A RID: 4666
	private mainScript mS_;

	// Token: 0x0400123B RID: 4667
	private textScript tS_;

	// Token: 0x0400123C RID: 4668
	private GUI_Main guiMain_;

	// Token: 0x0400123D RID: 4669
	private sfxScript sfx_;

	// Token: 0x0400123E RID: 4670
	private platformScript pS_;

	// Token: 0x0400123F RID: 4671
	private taskKonsole taskKonsole_;
}
