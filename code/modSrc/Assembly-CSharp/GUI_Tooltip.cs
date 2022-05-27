using System;
using UnityEngine;
using UnityEngine.UI;


public class GUI_Tooltip : MonoBehaviour
{
	
	private void Start()
	{
		this.settings_ = GameObject.Find("Main").GetComponent<settingsScript>();
		this.myText = this.tooltipText.GetComponent<Text>();
		this.rt_tooltipPic = this.tooltipPic.GetComponent<RectTransform>();
		this.rt_tooltipText = this.tooltipText.GetComponent<RectTransform>();
	}

	
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

	
	public void SetInactive()
	{
		this.timer = 0f;
		this.tooltipEnabled = false;
		this.myText.text = "";
	}

	
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

	
	public settingsScript settings_;

	
	public float randInPixel = 8f;

	
	public float timeToShow = 1f;

	
	public GameObject tooltipPic;

	
	public GameObject tooltipText;

	
	private RectTransform rt_tooltipPic;

	
	private RectTransform rt_tooltipText;

	
	public Text myText;

	
	private float timer;

	
	public bool tooltipEnabled;
}
