using System;
using UnityEngine;
using UnityEngine.UI;


public class objectTooltip : MonoBehaviour
{
	
	private void Start()
	{
		this.myText = this.tooltipText.GetComponent<Text>();
		this.myFill = this.tooltipFill.GetComponent<Image>();
		this.rt_tooltipPic = this.tooltipPic.GetComponent<RectTransform>();
		this.rt_tooltipText = this.tooltipText.GetComponent<RectTransform>();
	}

	
	public void SetActive(objectScript script_)
	{
		if (!script_)
		{
			this.SetInactive();
			return;
		}
		this.objectScript_ = script_;
		this.timer = 0f;
		this.tooltipEnabled = true;
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
			if (!this.objectScript_)
			{
				this.SetInactive();
				return;
			}
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
			float num = 100f / (float)this.objectScript_.aufladungenMax * (float)this.objectScript_.aufladungenAkt;
			this.myText.text = Mathf.RoundToInt(num).ToString() + "%";
			this.myFill.fillAmount = num * 0.01f;
			if (this.myFill.fillAmount < 0.05f)
			{
				this.myFill.fillAmount = 0.05f;
			}
		}
		float num2 = Input.mousePosition.x + 15f;
		float num3 = Input.mousePosition.y - 10f;
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		if (this.rt_tooltipPic.sizeDelta.x + Input.mousePosition.x > (float)Screen.width)
		{
			num2 = (float)Screen.width - this.rt_tooltipPic.sizeDelta.x;
		}
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		if (this.rt_tooltipPic.sizeDelta.y + Input.mousePosition.y > (float)Screen.height)
		{
			num3 = (float)Screen.height - this.rt_tooltipPic.sizeDelta.y;
		}
		this.rt_tooltipPic.anchoredPosition = new Vector2(num2, num3);
	}

	
	public float timeToShow = 1f;

	
	public GameObject tooltipPic;

	
	public GameObject tooltipText;

	
	public GameObject tooltipFill;

	
	private RectTransform rt_tooltipPic;

	
	private RectTransform rt_tooltipText;

	
	public Text myText;

	
	public Image myFill;

	
	private float timer;

	
	public bool tooltipEnabled;

	
	private objectScript objectScript_;
}
