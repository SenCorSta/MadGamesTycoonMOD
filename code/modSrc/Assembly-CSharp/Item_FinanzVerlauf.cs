using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_FinanzVerlauf : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = (1976 + this.index).ToString();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.finanzVerlaufEinnahmen[this.index], true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.finanzVerlaufAusgaben[this.index], true);
		long num = this.mS_.finanzVerlaufEinnahmen[this.index] - this.mS_.finanzVerlaufAusgaben[this.index];
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(num, true);
		if (num < 0L)
		{
			this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public int index;
}
