using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevEngine_Genre : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[2], this.genres_.genres_LEVEL[this.myID]);
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().SetSpezialgenre(this.myID);
		this.guiMain_.uiObjects[38].GetComponent<Menu_Dev_Engine_Genre>().BUTTON_Close();
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public genres genres_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
