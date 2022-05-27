using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200016E RID: 366
public class Menu_BackToMainMenu : MonoBehaviour
{
	// Token: 0x06000DAB RID: 3499 RVA: 0x000947AA File Offset: 0x000929AA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000947B4 File Offset: 0x000929B4
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

	// Token: 0x06000DAD RID: 3501 RVA: 0x0009485E File Offset: 0x00092A5E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x00094879 File Offset: 0x00092A79
	public void BUTTON_Yes()
	{
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
		}
		this.guiMain_.RemoveVectrocity();
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x04001240 RID: 4672
	public GameObject[] uiObjects;

	// Token: 0x04001241 RID: 4673
	private GameObject main_;

	// Token: 0x04001242 RID: 4674
	private mainScript mS_;

	// Token: 0x04001243 RID: 4675
	private textScript tS_;

	// Token: 0x04001244 RID: 4676
	private GUI_Main guiMain_;

	// Token: 0x04001245 RID: 4677
	private sfxScript sfx_;
}
