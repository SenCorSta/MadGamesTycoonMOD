using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Stats_GenreBeliebtheit : MonoBehaviour
{
	
	public void Init(string text_, float prozent, Sprite pic)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(prozent) + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = pic;
		this.uiObjects[3].GetComponent<Image>().fillAmount = prozent * 0.01f;
		if (prozent >= 100f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1381);
		}
		if (prozent <= 20f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1382);
		}
		if (prozent <= 50f)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[0];
			return;
		}
		if (prozent < 70f)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[1];
			return;
		}
		this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[2];
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public GUI_Main guiMain_;

	
	public textScript tS_;
}
