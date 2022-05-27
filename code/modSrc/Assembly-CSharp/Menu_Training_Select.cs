using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000272 RID: 626
public class Menu_Training_Select : MonoBehaviour
{
	// Token: 0x0600185D RID: 6237 RVA: 0x000F1DB5 File Offset: 0x000EFFB5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000F1DC0 File Offset: 0x000EFFC0
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000F1E6A File Offset: 0x000F006A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000F1E9C File Offset: 0x000F009C
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000F1EAB File Offset: 0x000F00AB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001C09 RID: 7177
	public int[] trainingCosts;

	// Token: 0x04001C0A RID: 7178
	public float[] trainingMaxLearn;

	// Token: 0x04001C0B RID: 7179
	public int[] trainingEffekt;

	// Token: 0x04001C0C RID: 7180
	public float[] workPoints;

	// Token: 0x04001C0D RID: 7181
	public Sprite[] trainingSprites;

	// Token: 0x04001C0E RID: 7182
	public GameObject[] uiPrefabs;

	// Token: 0x04001C0F RID: 7183
	public GameObject[] uiObjects;

	// Token: 0x04001C10 RID: 7184
	private mainScript mS_;

	// Token: 0x04001C11 RID: 7185
	private GameObject main_;

	// Token: 0x04001C12 RID: 7186
	private GUI_Main guiMain_;

	// Token: 0x04001C13 RID: 7187
	private sfxScript sfx_;

	// Token: 0x04001C14 RID: 7188
	private textScript tS_;

	// Token: 0x04001C15 RID: 7189
	public roomScript rS_;
}
