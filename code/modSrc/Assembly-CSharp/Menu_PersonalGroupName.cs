using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E3 RID: 483
public class Menu_PersonalGroupName : MonoBehaviour
{
	// Token: 0x06001231 RID: 4657 RVA: 0x0000CA21 File Offset: 0x0000AC21
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000CC850 File Offset: 0x000CAA50
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

	// Token: 0x06001233 RID: 4659 RVA: 0x0000CA29 File Offset: 0x0000AC29
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x0000CA3D File Offset: 0x0000AC3D
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x000CC92C File Offset: 0x000CAB2C
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

	// Token: 0x06001236 RID: 4662 RVA: 0x0000CA5F File Offset: 0x0000AC5F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000CC99C File Offset: 0x000CAB9C
	public void BUTTON_Yes()
	{
		this.mS_.personal_group_names[this.group] = this.uiObjects[0].GetComponent<InputField>().text;
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().InitDropdowns();
		this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>().DROPDOWN_Group();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040016B4 RID: 5812
	public GameObject[] uiObjects;

	// Token: 0x040016B5 RID: 5813
	private GameObject main_;

	// Token: 0x040016B6 RID: 5814
	private mainScript mS_;

	// Token: 0x040016B7 RID: 5815
	private textScript tS_;

	// Token: 0x040016B8 RID: 5816
	private GUI_Main guiMain_;

	// Token: 0x040016B9 RID: 5817
	private sfxScript sfx_;

	// Token: 0x040016BA RID: 5818
	private cameraMovementScript cmS_;

	// Token: 0x040016BB RID: 5819
	private int group = -1;
}
