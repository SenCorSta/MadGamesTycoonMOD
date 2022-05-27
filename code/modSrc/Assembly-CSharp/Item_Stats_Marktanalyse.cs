using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Stats_Marktanalyse : MonoBehaviour
{
	
	public void Init(string text_, string amountGames, Sprite pic, Sprite marktanalyse, int anzGames_, int typ_)
	{
		this.myName = text_;
		this.anzGames = anzGames_;
		this.typ = typ_;
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Text>().text = amountGames;
		this.uiObjects[2].GetComponent<Image>().sprite = pic;
		this.uiObjects[3].GetComponent<Image>().sprite = marktanalyse;
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public string myName;

	
	public int anzGames;

	
	public int typ;
}
