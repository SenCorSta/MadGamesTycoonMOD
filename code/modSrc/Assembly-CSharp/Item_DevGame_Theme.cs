using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_Theme : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetThemes(this.myID);
		if (this.themes_.themes_MGSR[this.myID] != 0)
		{
			this.uiObjects[1].GetComponent<Image>().sprite = this.mS_.games_.gamePEGI[this.themes_.themes_MGSR[this.myID]];
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.themes_.GetSpriteMarkt(this.myID);
		switch (this.fitGenre)
		{
		case -1:
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
			break;
		case 1:
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
			break;
		}
		this.guiMain_.DrawStars(this.uiObjects[2], this.themes_.themes_LEVEL[this.myID]);
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.themeArt == 0)
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMainTheme(this.myID);
			}
		}
		else
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetSubTheme(this.myID);
			}
			if (this.guiMain_.uiObjects[193].activeSelf)
			{
				this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetSubTheme(this.myID);
			}
			if (this.guiMain_.uiObjects[247].activeSelf)
			{
				this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetSubTheme(this.myID);
			}
		}
		this.guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>().BUTTON_Close();
	}

	
	public int themeArt;

	
	public int myID;

	
	public int fitGenre;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public themes themes_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public bool debug;
}
