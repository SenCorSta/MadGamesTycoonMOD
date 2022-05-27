using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A7 RID: 167
public class Item_DevKonsole_Componente : MonoBehaviour
{
	// Token: 0x0600062E RID: 1582 RVA: 0x00005964 File Offset: 0x00003B64
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0006168C File Offset: 0x0005F88C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardware_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.myID);
		this.uiObjects[3].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.myID].ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardware_.GetDevCosts(this.myID), true);
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(1604) + ": <color=blue>" + this.mS_.GetMoney((long)this.hardware_.GetPerformance(this.myID), false) + "</color>";
		this.tooltip_.c = this.hardware_.GetTooltip(this.myID);
		if (!this.hardware_.IsTechComponent(this.myID))
		{
			this.uiObjects[5].SetActive(false);
			this.uiObjects[6].SetActive(true);
		}
		Menu_Dev_Konsole component = this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>();
		if (component.component_cpu == this.myID || component.component_gfx == this.myID || component.component_ram == this.myID || component.component_hdd == this.myID || component.component_sfx == this.myID || component.component_cooling == this.myID || component.component_disc == this.myID || component.component_controller == this.myID || component.component_case == this.myID || component.component_monitor == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00061898 File Offset: 0x0005FA98
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().SetComponent(this.hardware_.hardware_TYP[this.myID], this.myID);
		this.guiMain_.uiObjects[319].GetComponent<Menu_Dev_KonsoleComponent>().BUTTON_Close();
	}

	// Token: 0x040009B9 RID: 2489
	public int myID;

	// Token: 0x040009BA RID: 2490
	public GameObject[] uiObjects;

	// Token: 0x040009BB RID: 2491
	public mainScript mS_;

	// Token: 0x040009BC RID: 2492
	public textScript tS_;

	// Token: 0x040009BD RID: 2493
	public sfxScript sfx_;

	// Token: 0x040009BE RID: 2494
	public hardware hardware_;

	// Token: 0x040009BF RID: 2495
	public GUI_Main guiMain_;

	// Token: 0x040009C0 RID: 2496
	public tooltip tooltip_;
}
