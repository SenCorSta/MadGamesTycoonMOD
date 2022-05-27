using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E9 RID: 489
public class Menu_Personaleinstellungen : MonoBehaviour
{
	// Token: 0x06001278 RID: 4728 RVA: 0x0000CC4C File Offset: 0x0000AE4C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x000D07D0 File Offset: 0x000CE9D0
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

	// Token: 0x0600127A RID: 4730 RVA: 0x0000CC54 File Offset: 0x0000AE54
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x000D087C File Offset: 0x000CEA7C
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

	// Token: 0x0600127C RID: 4732 RVA: 0x000D0968 File Offset: 0x000CEB68
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

	// Token: 0x0600127D RID: 4733 RVA: 0x0000CC62 File Offset: 0x0000AE62
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000D0A58 File Offset: 0x000CEC58
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

	// Token: 0x0600127F RID: 4735 RVA: 0x000D0B60 File Offset: 0x000CED60
	public void SLIDER_Motivation()
	{
		this.uiObjects[2].GetComponent<Text>().text = this.uiObjects[4].GetComponent<Slider>().value.ToString() + "%";
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000D0BA4 File Offset: 0x000CEDA4
	public void SLIDER_Crunch()
	{
		this.uiObjects[3].GetComponent<Text>().text = this.uiObjects[5].GetComponent<Slider>().value.ToString() + "%";
		if (Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value) >= 100)
		{
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(1626);
		}
	}

	// Token: 0x040016EB RID: 5867
	public GameObject[] uiObjects;

	// Token: 0x040016EC RID: 5868
	private roomScript rS_;

	// Token: 0x040016ED RID: 5869
	private GameObject main_;

	// Token: 0x040016EE RID: 5870
	private mainScript mS_;

	// Token: 0x040016EF RID: 5871
	private textScript tS_;

	// Token: 0x040016F0 RID: 5872
	private GUI_Main guiMain_;

	// Token: 0x040016F1 RID: 5873
	private sfxScript sfx_;
}
