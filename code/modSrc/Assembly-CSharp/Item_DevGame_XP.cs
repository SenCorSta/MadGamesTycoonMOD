using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_XP : MonoBehaviour
{
	
	public void SetData(string text_, Sprite sprite_, int stars_)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Image>().sprite = sprite_;
		this.guiMain_.DrawStars(this.uiObjects[2], stars_);
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public GUI_Main guiMain_;
}
