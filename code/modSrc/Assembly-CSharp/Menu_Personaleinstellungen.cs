using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EA RID: 490
public class Menu_Personaleinstellungen : MonoBehaviour
{
	// Token: 0x06001293 RID: 4755 RVA: 0x000C5C93 File Offset: 0x000C3E93
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000C5C9C File Offset: 0x000C3E9C
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x000C5D46 File Offset: 0x000C3F46
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x000C5D54 File Offset: 0x000C3F54
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Dropdown>().value = this.mS_.personal_pausen;
		this.uiObjects[1].GetComponent<Dropdown>().value = this.mS_.personal_druck;
		this.uiObjects[4].GetComponent<Slider>().value = (float)this.mS_.personal_motivation;
		this.uiObjects[5].GetComponent<Slider>().value = (float)this.mS_.personal_crunch;
		this.uiObjects[6].GetComponent<Toggle>().isOn = this.mS_.personal_dontLeaveBuilding;
		this.uiObjects[7].GetComponent<Toggle>().isOn = this.mS_.personal_RobotDontLeaveBuilding;
		this.uiObjects[8].GetComponent<Toggle>().isOn = this.mS_.personal_ki;
		this.SLIDER_Motivation();
		this.SLIDER_Crunch();
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x000C5E40 File Offset: 0x000C4040
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1002));
		list.Add(this.tS_.GetText(1003));
		list.Add(this.tS_.GetText(1004));
		this.uiObjects[0].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[0].GetComponent<Dropdown>().AddOptions(list);
		List<string> list2 = new List<string>();
		list2.Add(this.tS_.GetText(1005));
		list2.Add(this.tS_.GetText(1006));
		list2.Add(this.tS_.GetText(1007));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list2);
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000C5F2D File Offset: 0x000C412D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x000C5F54 File Offset: 0x000C4154
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.mS_.personal_pausen = this.uiObjects[0].GetComponent<Dropdown>().value;
		this.mS_.personal_druck = this.uiObjects[1].GetComponent<Dropdown>().value;
		this.mS_.personal_motivation = Mathf.RoundToInt(this.uiObjects[4].GetComponent<Slider>().value);
		this.mS_.personal_crunch = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.mS_.personal_dontLeaveBuilding = this.uiObjects[6].GetComponent<Toggle>().isOn;
		this.mS_.personal_RobotDontLeaveBuilding = this.uiObjects[7].GetComponent<Toggle>().isOn;
		this.mS_.personal_ki = this.uiObjects[8].GetComponent<Toggle>().isOn;
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x000C605C File Offset: 0x000C425C
	public void SLIDER_Motivation()
	{
		this.uiObjects[2].GetComponent<Text>().text = this.uiObjects[4].GetComponent<Slider>().value.ToString() + "%";
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000C60A0 File Offset: 0x000C42A0
	public void SLIDER_Crunch()
	{
		this.uiObjects[3].GetComponent<Text>().text = this.uiObjects[5].GetComponent<Slider>().value.ToString() + "%";
		if (Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value) >= 100)
		{
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(1626);
		}
	}

	// Token: 0x040016F4 RID: 5876
	public GameObject[] uiObjects;

	// Token: 0x040016F5 RID: 5877
	private roomScript rS_;

	// Token: 0x040016F6 RID: 5878
	private GameObject main_;

	// Token: 0x040016F7 RID: 5879
	private mainScript mS_;

	// Token: 0x040016F8 RID: 5880
	private textScript tS_;

	// Token: 0x040016F9 RID: 5881
	private GUI_Main guiMain_;

	// Token: 0x040016FA RID: 5882
	private sfxScript sfx_;
}
