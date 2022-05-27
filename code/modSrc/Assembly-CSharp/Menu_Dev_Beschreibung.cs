using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012C RID: 300
public class Menu_Dev_Beschreibung : MonoBehaviour
{
	// Token: 0x06000A99 RID: 2713 RVA: 0x0000795F File Offset: 0x00005B5F
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x000840AC File Offset: 0x000822AC
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

	// Token: 0x06000A9B RID: 2715 RVA: 0x0000796D File Offset: 0x00005B6D
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x000841F4 File Offset: 0x000823F4
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

	// Token: 0x06000A9D RID: 2717 RVA: 0x00007975 File Offset: 0x00005B75
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000842C4 File Offset: 0x000824C4
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

	// Token: 0x04000EDA RID: 3802
	public GameObject[] uiObjects;

	// Token: 0x04000EDB RID: 3803
	private GameObject main_;

	// Token: 0x04000EDC RID: 3804
	private mainScript mS_;

	// Token: 0x04000EDD RID: 3805
	private textScript tS_;

	// Token: 0x04000EDE RID: 3806
	private GUI_Main guiMain_;

	// Token: 0x04000EDF RID: 3807
	private sfxScript sfx_;

	// Token: 0x04000EE0 RID: 3808
	private Menu_DevGame mDevGame_;

	// Token: 0x04000EE1 RID: 3809
	private Menu_Dev_AddonDo mDevAddon_;

	// Token: 0x04000EE2 RID: 3810
	private Menu_Dev_MMOAddon mDevMMOAddon_;

	// Token: 0x04000EE3 RID: 3811
	private Menu_Dev_GameEntwicklungsbericht mDevEntwicklungsbericht_;
}
