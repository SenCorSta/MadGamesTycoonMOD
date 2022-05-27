using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E1 RID: 481
public class Menu_MitarberKuendigt : MonoBehaviour
{
	// Token: 0x06001223 RID: 4643 RVA: 0x0000C98F File Offset: 0x0000AB8F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000CC468 File Offset: 0x000CA668
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

	// Token: 0x06001225 RID: 4645 RVA: 0x000CC530 File Offset: 0x000CA730
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

	// Token: 0x06001226 RID: 4646 RVA: 0x0000C997 File Offset: 0x0000AB97
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0000C9B2 File Offset: 0x0000ABB2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000CC5E4 File Offset: 0x000CA7E4
	public void BUTTON_JumpToRoom()
	{
		if (this.rS_ && this.guiMain_ && this.guiMain_.camera_)
		{
			this.guiMain_.camera_.transform.parent.position = new Vector3(this.rS_.uiPos.x, this.guiMain_.camera_.transform.parent.position.y, this.rS_.uiPos.z);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016A5 RID: 5797
	public GameObject[] uiObjects;

	// Token: 0x040016A6 RID: 5798
	private GameObject main_;

	// Token: 0x040016A7 RID: 5799
	private mainScript mS_;

	// Token: 0x040016A8 RID: 5800
	private textScript tS_;

	// Token: 0x040016A9 RID: 5801
	private GUI_Main guiMain_;

	// Token: 0x040016AA RID: 5802
	private sfxScript sfx_;

	// Token: 0x040016AB RID: 5803
	private settingsScript settings_;

	// Token: 0x040016AC RID: 5804
	private roomScript rS_;
}
