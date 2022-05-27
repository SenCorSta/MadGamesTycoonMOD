using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000272 RID: 626
public class objectTooltip : MonoBehaviour
{
	// Token: 0x06001834 RID: 6196 RVA: 0x000F7980 File Offset: 0x000F5B80
	private void Start()
	{
		this.myText = this.tooltipText.GetComponent<Text>();
		this.myFill = this.tooltipFill.GetComponent<Image>();
		this.rt_tooltipPic = this.tooltipPic.GetComponent<RectTransform>();
		this.rt_tooltipText = this.tooltipText.GetComponent<RectTransform>();
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00010CB0 File Offset: 0x0000EEB0
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

	// Token: 0x06001836 RID: 6198 RVA: 0x00010CDA File Offset: 0x0000EEDA
	public void SetInactive()
	{
		this.timer = 0f;
		this.tooltipEnabled = false;
		this.myText.text = "";
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000F79D4 File Offset: 0x000F5BD4
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

	// Token: 0x04001C0F RID: 7183
	public float timeToShow = 1f;

	// Token: 0x04001C10 RID: 7184
	public GameObject tooltipPic;

	// Token: 0x04001C11 RID: 7185
	public GameObject tooltipText;

	// Token: 0x04001C12 RID: 7186
	public GameObject tooltipFill;

	// Token: 0x04001C13 RID: 7187
	private RectTransform rt_tooltipPic;

	// Token: 0x04001C14 RID: 7188
	private RectTransform rt_tooltipText;

	// Token: 0x04001C15 RID: 7189
	public Text myText;

	// Token: 0x04001C16 RID: 7190
	public Image myFill;

	// Token: 0x04001C17 RID: 7191
	private float timer;

	// Token: 0x04001C18 RID: 7192
	public bool tooltipEnabled;

	// Token: 0x04001C19 RID: 7193
	private objectScript objectScript_;
}
