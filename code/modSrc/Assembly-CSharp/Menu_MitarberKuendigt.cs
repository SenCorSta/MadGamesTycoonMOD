using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E2 RID: 482
public class Menu_MitarberKuendigt : MonoBehaviour
{
	// Token: 0x0600123E RID: 4670 RVA: 0x000C166D File Offset: 0x000BF86D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000C1678 File Offset: 0x000BF878
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
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

	// Token: 0x06001240 RID: 4672 RVA: 0x000C1740 File Offset: 0x000BF940
	public void Init(characterScript cS_)
	{
		Debug.Log("MITARBEITER KÜNDIGT: " + cS_.myName);
		this.FindScripts();
		this.guiMain_.OpenMenu(false);
		if (cS_)
		{
			this.rS_ = cS_.roomS_;
			this.sfx_.PlaySound(41, false);
			string text = this.tS_.GetText(509);
			text = text.Replace("<NAME>", cS_.myName);
			this.uiObjects[0].GetComponent<Text>().text = text;
			cS_.RemoveObjectUsing();
			cS_.Entlassen(false);
		}
		else
		{
			this.BUTTON_Abbrechen();
		}
		if (this.settings_.hideKuendigungen)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000C17F4 File Offset: 0x000BF9F4
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000C180F File Offset: 0x000BFA0F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000C1835 File Offset: 0x000BFA35
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000C1840 File Offset: 0x000BFA40
	public void BUTTON_JumpToRoom()
	{
		if (this.rS_ && this.guiMain_ && this.guiMain_.camera_)
		{
			this.guiMain_.camera_.transform.parent.position = new Vector3(this.rS_.uiPos.x, this.guiMain_.camera_.transform.parent.position.y, this.rS_.uiPos.z);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016AE RID: 5806
	public GameObject[] uiObjects;

	// Token: 0x040016AF RID: 5807
	private GameObject main_;

	// Token: 0x040016B0 RID: 5808
	private mainScript mS_;

	// Token: 0x040016B1 RID: 5809
	private textScript tS_;

	// Token: 0x040016B2 RID: 5810
	private GUI_Main guiMain_;

	// Token: 0x040016B3 RID: 5811
	private sfxScript sfx_;

	// Token: 0x040016B4 RID: 5812
	private settingsScript settings_;

	// Token: 0x040016B5 RID: 5813
	private roomScript rS_;
}
