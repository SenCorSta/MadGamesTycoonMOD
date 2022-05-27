using System;
using UnityEngine;
using UnityEngine.UI;


public class Filter_InventarKaufen : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.show = !this.show;
		this.guiMain_.uiObjects[20].GetComponent<Menu_BuyInventar>().filter[this.filterArrayID] = !this.show;
		if (this.show)
		{
			this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.uiSprites[60];
			base.GetComponent<Image>().color = this.colors[0];
		}
		else
		{
			this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.uiSprites[59];
			base.GetComponent<Image>().color = this.colors[1];
		}
		for (int i = base.transform.GetSiblingIndex() + 1; i < base.transform.parent.childCount; i++)
		{
			Transform child = base.transform.parent.GetChild(i);
			if (child)
			{
				if (!child.GetComponent<Item_InventarKaufen>())
				{
					return;
				}
				child.gameObject.SetActive(!child.gameObject.activeSelf);
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	public int filterArrayID;

	
	public Color[] colors;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public mapScript mapS_;

	
	public GUI_Main guiMain_;

	
	public sfxScript sfx_;

	
	public bool show = true;
}
