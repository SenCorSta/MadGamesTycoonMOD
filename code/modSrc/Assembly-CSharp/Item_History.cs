using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_History : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.history[this.index];
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
