using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E4 RID: 484
public class Menu_PersonalGroupName : MonoBehaviour
{
	// Token: 0x0600124C RID: 4684 RVA: 0x000C1AE9 File Offset: 0x000BFCE9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000C1AF4 File Offset: 0x000BFCF4
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x000C1BCE File Offset: 0x000BFDCE
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000C1BE2 File Offset: 0x000BFDE2
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000C1C04 File Offset: 0x000BFE04
	public void Init(int group_)
	{
		this.FindScripts();
		this.group = group_;
		if (this.mS_.personal_group_names[this.group].Length > 0)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mS_.personal_group_names[this.group];
			return;
		}
		this.uiObjects[0].GetComponent<InputField>().text = "";
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000C1C74 File Offset: 0x000BFE74
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000C1C90 File Offset: 0x000BFE90
	public void BUTTON_Yes()
	{
		this.mS_.personal_group_names[this.group] = this.uiObjects[0].GetComponent<InputField>().text;
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().InitDropdowns();
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().DROPDOWN_Group();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016BD RID: 5821
	public GameObject[] uiObjects;

	// Token: 0x040016BE RID: 5822
	private GameObject main_;

	// Token: 0x040016BF RID: 5823
	private mainScript mS_;

	// Token: 0x040016C0 RID: 5824
	private textScript tS_;

	// Token: 0x040016C1 RID: 5825
	private GUI_Main guiMain_;

	// Token: 0x040016C2 RID: 5826
	private sfxScript sfx_;

	// Token: 0x040016C3 RID: 5827
	private cameraMovementScript cmS_;

	// Token: 0x040016C4 RID: 5828
	private int group = -1;
}
