using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000075 RID: 117
public class GUI_Tabs : MonoBehaviour
{
	// Token: 0x060004FE RID: 1278 RVA: 0x00045FF4 File Offset: 0x000441F4
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

	// Token: 0x060004FF RID: 1279 RVA: 0x00046074 File Offset: 0x00044274
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

	// Token: 0x040007D9 RID: 2009
	public int activTab;

	// Token: 0x040007DA RID: 2010
	public int tabHeight = 50;

	// Token: 0x040007DB RID: 2011
	private float tabInactivHeight = 50f;

	// Token: 0x040007DC RID: 2012
	public GameObject[] tabs;

	// Token: 0x040007DD RID: 2013
	public GameObject[] menus;

	// Token: 0x040007DE RID: 2014
	public Color colorOn;

	// Token: 0x040007DF RID: 2015
	public Color colorOff;
}
