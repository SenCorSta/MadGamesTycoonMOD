using System;
using UnityEngine;
using UnityEngine.UI;


public class stars : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		for (int i = 0; i < this.myObjects.Length; i++)
		{
			if (this.amount > i)
			{
				this.myObjects[i].GetComponent<Image>().color = this.myColors[0];
			}
			else
			{
				this.myObjects[i].GetComponent<Image>().color = this.myColors[1];
			}
		}
	}

	
	public Color[] myColors;

	
	public GameObject[] myObjects;

	
	public int amount;
}
