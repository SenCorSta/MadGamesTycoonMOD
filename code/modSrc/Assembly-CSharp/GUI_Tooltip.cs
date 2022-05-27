using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000076 RID: 118
public class GUI_Tooltip : MonoBehaviour
{
	// Token: 0x06000501 RID: 1281 RVA: 0x0004617C File Offset: 0x0004437C
	private void Start()
	{
		this.settings_ = GameObject.Find("Main").GetComponent<settingsScript>();
		this.myText = this.tooltipText.GetComponent<Text>();
		this.rt_tooltipPic = this.tooltipPic.GetComponent<RectTransform>();
		this.rt_tooltipText = this.tooltipText.GetComponent<RectTransform>();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000461D4 File Offset: 0x000443D4
	public void SetActive(string s)
	{
		if (s == null)
		{
			return;
		}
		if (s.Length > 0)
		{
			s = s.Replace("<br>", "\n");
			s = s.Replace("<c>", "</color>");
			s = s.Replace("<red>", "<color=red>");
			s = s.Replace("<blue>", "<color=blue>");
			s = s.Replace("<green>", "<color=green>");
			this.myText.text = s;
			this.timer = 0f;
			this.tooltipEnabled = true;
			return;
		}
		this.SetInactive();
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0004626D File Offset: 0x0004446D
	public void SetInactive()
	{
		this.timer = 0f;
		this.tooltipEnabled = false;
		this.myText.text = "";
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00046294 File Offset: 0x00044494
	private void Update()
	{
		if (!this.tooltipEnabled)
		{
			if (this.tooltipPic.activeSelf)
			{
				this.tooltipPic.SetActive(false);
			}
		}
		else
		{
			this.timer += Time.deltaTime;
			if (this.timer < this.timeToShow)
			{
				if (this.tooltipPic.activeSelf)
				{
					this.tooltipPic.SetActive(false);
				}
				return;
			}
			if (!this.tooltipPic.activeSelf)
			{
				this.tooltipPic.SetActive(true);
			}
		}
		Vector2 sizeDelta = this.rt_tooltipText.sizeDelta;
		sizeDelta.x += this.randInPixel;
		sizeDelta.y += this.randInPixel;
		this.rt_tooltipPic.sizeDelta = sizeDelta;
		float num = Input.mousePosition.x;
		float num2 = Input.mousePosition.y;
		num /= this.settings_.uiScale;
		num2 /= this.settings_.uiScale;
		if (num < 0f)
		{
			num = 0f;
		}
		if (this.rt_tooltipPic.sizeDelta.x + num > (float)Screen.width / this.settings_.uiScale)
		{
			num = (float)Screen.width / this.settings_.uiScale - this.rt_tooltipPic.sizeDelta.x;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		if (this.rt_tooltipPic.sizeDelta.y + num2 > (float)Screen.height / this.settings_.uiScale)
		{
			num2 = (float)Screen.height / this.settings_.uiScale - this.rt_tooltipPic.sizeDelta.y;
		}
		this.rt_tooltipPic.anchoredPosition = new Vector2(num, num2);
	}

	// Token: 0x040007E0 RID: 2016
	public settingsScript settings_;

	// Token: 0x040007E1 RID: 2017
	public float randInPixel = 8f;

	// Token: 0x040007E2 RID: 2018
	public float timeToShow = 1f;

	// Token: 0x040007E3 RID: 2019
	public GameObject tooltipPic;

	// Token: 0x040007E4 RID: 2020
	public GameObject tooltipText;

	// Token: 0x040007E5 RID: 2021
	private RectTransform rt_tooltipPic;

	// Token: 0x040007E6 RID: 2022
	private RectTransform rt_tooltipText;

	// Token: 0x040007E7 RID: 2023
	public Text myText;

	// Token: 0x040007E8 RID: 2024
	private float timer;

	// Token: 0x040007E9 RID: 2025
	public bool tooltipEnabled;
}
