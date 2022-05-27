using System;
using UnityEngine;
using UnityEngine.UI;


public class GUI_Tabs : MonoBehaviour
{
	
	private void Start()
	{
		this.tabInactivHeight = this.tabs[0].GetComponent<RectTransform>().sizeDelta.y;
		for (int i = 0; i < this.tabs.Length; i++)
		{
			this.tabs[i].GetComponent<Image>().color = this.colorOff;
			RectTransform component = this.tabs[i].GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, this.tabInactivHeight);
		}
	}

	
	public void Click_Tab(int t)
	{
		this.activTab = t;
		for (int i = 0; i < this.tabs.Length; i++)
		{
			this.tabs[i].GetComponent<Image>().color = this.colorOff;
			RectTransform component = this.tabs[i].GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, this.tabInactivHeight);
		}
		this.tabs[t].GetComponent<Image>().color = this.colorOn;
		RectTransform component2 = this.tabs[this.activTab].GetComponent<RectTransform>();
		component2.sizeDelta = new Vector2(component2.sizeDelta.x, (float)this.tabHeight);
		if (this.menus.Length != 0)
		{
			for (int j = 0; j < this.menus.Length; j++)
			{
				this.menus[j].SetActive(false);
			}
			this.menus[this.activTab].SetActive(true);
		}
	}

	
	public int activTab;

	
	public int tabHeight = 50;

	
	private float tabInactivHeight = 50f;

	
	public GameObject[] tabs;

	
	public GameObject[] menus;

	
	public Color colorOn;

	
	public Color colorOff;
}
