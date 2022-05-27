using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000276 RID: 630
public class objectTooltip : MonoBehaviour
{
	// Token: 0x06001877 RID: 6263 RVA: 0x000F278C File Offset: 0x000F098C
	private void Start()
	{
		this.myText = this.tooltipText.GetComponent<Text>();
		this.myFill = this.tooltipFill.GetComponent<Image>();
		this.rt_tooltipPic = this.tooltipPic.GetComponent<RectTransform>();
		this.rt_tooltipText = this.tooltipText.GetComponent<RectTransform>();
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x000F27DD File Offset: 0x000F09DD
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

	// Token: 0x06001879 RID: 6265 RVA: 0x000F2807 File Offset: 0x000F0A07
	public void SetInactive()
	{
		this.timer = 0f;
		this.tooltipEnabled = false;
		this.myText.text = "";
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x000F282C File Offset: 0x000F0A2C
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

	// Token: 0x04001C29 RID: 7209
	public float timeToShow = 1f;

	// Token: 0x04001C2A RID: 7210
	public GameObject tooltipPic;

	// Token: 0x04001C2B RID: 7211
	public GameObject tooltipText;

	// Token: 0x04001C2C RID: 7212
	public GameObject tooltipFill;

	// Token: 0x04001C2D RID: 7213
	private RectTransform rt_tooltipPic;

	// Token: 0x04001C2E RID: 7214
	private RectTransform rt_tooltipText;

	// Token: 0x04001C2F RID: 7215
	public Text myText;

	// Token: 0x04001C30 RID: 7216
	public Image myFill;

	// Token: 0x04001C31 RID: 7217
	private float timer;

	// Token: 0x04001C32 RID: 7218
	public bool tooltipEnabled;

	// Token: 0x04001C33 RID: 7219
	private objectScript objectScript_;
}
