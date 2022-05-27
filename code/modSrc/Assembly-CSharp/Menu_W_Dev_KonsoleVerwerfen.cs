using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016C RID: 364
public class Menu_W_Dev_KonsoleVerwerfen : MonoBehaviour
{
	// Token: 0x06000D8D RID: 3469 RVA: 0x00009642 File Offset: 0x00007842
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000A2C38 File Offset: 0x000A0E38
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

	// Token: 0x06000D8F RID: 3471 RVA: 0x0000964A File Offset: 0x0000784A
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

	// Token: 0x06000D90 RID: 3472 RVA: 0x00009689 File Offset: 0x00007889
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000A2CE4 File Offset: 0x000A0EE4
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

	// Token: 0x04001230 RID: 4656
	public GameObject[] uiObjects;

	// Token: 0x04001231 RID: 4657
	private GameObject main_;

	// Token: 0x04001232 RID: 4658
	private mainScript mS_;

	// Token: 0x04001233 RID: 4659
	private textScript tS_;

	// Token: 0x04001234 RID: 4660
	private GUI_Main guiMain_;

	// Token: 0x04001235 RID: 4661
	private sfxScript sfx_;

	// Token: 0x04001236 RID: 4662
	private platformScript pS_;

	// Token: 0x04001237 RID: 4663
	private taskKonsole taskKonsole_;
}
