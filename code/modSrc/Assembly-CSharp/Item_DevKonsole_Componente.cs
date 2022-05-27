using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevKonsole_Componente : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
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

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().SetComponent(this.hardware_.hardware_TYP[this.myID], this.myID);
		this.guiMain_.uiObjects[319].GetComponent<Menu_Dev_KonsoleComponent>().BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public hardware hardware_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
