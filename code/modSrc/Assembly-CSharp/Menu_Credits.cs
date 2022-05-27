using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016F RID: 367
public class Menu_Credits : MonoBehaviour
{
	// Token: 0x06000D9D RID: 3485 RVA: 0x00009730 File Offset: 0x00007930
	private void Start()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.credits;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000A2EC4 File Offset: 0x000A10C4
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

	// Token: 0x06000D9F RID: 3487 RVA: 0x00009755 File Offset: 0x00007955
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001244 RID: 4676
	public GameObject[] uiObjects;

	// Token: 0x04001245 RID: 4677
	private GameObject main_;

	// Token: 0x04001246 RID: 4678
	private mainScript mS_;

	// Token: 0x04001247 RID: 4679
	private textScript tS_;

	// Token: 0x04001248 RID: 4680
	private GUI_Main guiMain_;

	// Token: 0x04001249 RID: 4681
	private sfxScript sfx_;
}
