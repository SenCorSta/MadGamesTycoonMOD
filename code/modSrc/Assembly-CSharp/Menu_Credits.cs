using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000170 RID: 368
public class Menu_Credits : MonoBehaviour
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x0009498C File Offset: 0x00092B8C
	private void Start()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.credits;
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x000949B4 File Offset: 0x00092BB4
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

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00094A5E File Offset: 0x00092C5E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

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
}
