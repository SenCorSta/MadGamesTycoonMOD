using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000282 RID: 642
public class Menu_MultiplayerSave : MonoBehaviour
{
	// Token: 0x06001919 RID: 6425 RVA: 0x000F943A File Offset: 0x000F763A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000F9444 File Offset: 0x000F7644
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
		if (!this.save_)
		{
			this.save_ = this.main_.GetComponent<savegameScript>();
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

	// Token: 0x0600191B RID: 6427 RVA: 0x000F943A File Offset: 0x000F763A
	public void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000F950C File Offset: 0x000F770C
	public void Init(int saveID_)
	{
		this.saveID = saveID_;
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000F9518 File Offset: 0x000F7718
	private void Update()
	{
		if (this.uiObjects[1].GetComponent<Button>().interactable)
		{
			this.timer += Time.deltaTime;
			if (this.timer > 1f)
			{
				this.BUTTON_Yes();
				return;
			}
		}
		else
		{
			this.timer = 0f;
		}
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000F956A File Offset: 0x000F776A
	public void BUTTON_Yes()
	{
		this.save_.Save(this.saveID);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001C7B RID: 7291
	public GameObject[] uiObjects;

	// Token: 0x04001C7C RID: 7292
	private GameObject main_;

	// Token: 0x04001C7D RID: 7293
	private mainScript mS_;

	// Token: 0x04001C7E RID: 7294
	private textScript tS_;

	// Token: 0x04001C7F RID: 7295
	private GUI_Main guiMain_;

	// Token: 0x04001C80 RID: 7296
	private sfxScript sfx_;

	// Token: 0x04001C81 RID: 7297
	private savegameScript save_;

	// Token: 0x04001C82 RID: 7298
	private int saveID = -1;

	// Token: 0x04001C83 RID: 7299
	private float timer;
}
