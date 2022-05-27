using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012D RID: 301
public class Menu_Dev_Beschreibung : MonoBehaviour
{
	// Token: 0x06000AAA RID: 2730 RVA: 0x00073AD7 File Offset: 0x00071CD7
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x00073AE8 File Offset: 0x00071CE8
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.mDevAddon_)
		{
			this.mDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.mDevMMOAddon_)
		{
			this.mDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
		if (!this.mDevEntwicklungsbericht_)
		{
			this.mDevEntwicklungsbericht_ = this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>();
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00073C30 File Offset: 0x00071E30
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00073C38 File Offset: 0x00071E38
	private void Init()
	{
		this.FindScripts();
		if (this.mDevGame_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevGame_.g_Beschreibung;
		}
		if (this.mDevAddon_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevAddon_.g_Beschreibung;
		}
		if (this.mDevMMOAddon_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevMMOAddon_.g_Beschreibung;
		}
		if (this.mDevEntwicklungsbericht_.gameObject.activeSelf)
		{
			this.uiObjects[0].GetComponent<InputField>().text = this.mDevEntwicklungsbericht_.GetBeschreibung();
		}
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00073D07 File Offset: 0x00071F07
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00073D24 File Offset: 0x00071F24
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mDevGame_.gameObject.activeSelf)
		{
			this.mDevGame_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevAddon_.gameObject.activeSelf)
		{
			this.mDevAddon_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevMMOAddon_.gameObject.activeSelf)
		{
			this.mDevMMOAddon_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		if (this.mDevEntwicklungsbericht_.gameObject.activeSelf)
		{
			this.mDevEntwicklungsbericht_.SetBeschreibung(this.uiObjects[0].GetComponent<InputField>().text);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000EE2 RID: 3810
	public GameObject[] uiObjects;

	// Token: 0x04000EE3 RID: 3811
	private GameObject main_;

	// Token: 0x04000EE4 RID: 3812
	private mainScript mS_;

	// Token: 0x04000EE5 RID: 3813
	private textScript tS_;

	// Token: 0x04000EE6 RID: 3814
	private GUI_Main guiMain_;

	// Token: 0x04000EE7 RID: 3815
	private sfxScript sfx_;

	// Token: 0x04000EE8 RID: 3816
	private Menu_DevGame mDevGame_;

	// Token: 0x04000EE9 RID: 3817
	private Menu_Dev_AddonDo mDevAddon_;

	// Token: 0x04000EEA RID: 3818
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	// Token: 0x04000EEB RID: 3819
	private Menu_Dev_GameEntwicklungsbericht mDevEntwicklungsbericht_;
}
