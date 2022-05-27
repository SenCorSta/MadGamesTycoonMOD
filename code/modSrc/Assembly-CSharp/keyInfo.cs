using System;
using UnityEngine;
using UnityEngine.UI;


public class keyInfo : MonoBehaviour
{
	
	private void Start()
	{
		string text = "";
		for (int i = 0; i < this.keys.Length; i++)
		{
			text += this.keys[i].ToString();
			if (i < this.keys.Length - 1)
			{
				text += " & ";
			}
		}
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	
	public KeyCode[] keys;

	
	public GameObject[] uiObjects;
}
