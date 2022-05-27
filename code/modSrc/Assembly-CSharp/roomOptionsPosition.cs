using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class roomOptionsPosition : MonoBehaviour
{
	// Token: 0x0600188C RID: 6284 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000FB408 File Offset: 0x000F9608
	private void FindScripts()
	{
		if (!this.settings_)
		{
			this.settings_ = GameObject.Find("Main").GetComponent<settingsScript>();
		}
		if (!this.rect)
		{
			this.rect = base.GetComponent<RectTransform>();
		}
		if (!this.globalRect)
		{
			this.globalRect = base.transform.parent.GetComponent<RectTransform>();
		}
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x00010E69 File Offset: 0x0000F069
	private void OnEnable()
	{
		this.FindScripts();
		this.rect.anchoredPosition = new Vector2(90f, -40f);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x000FB474 File Offset: 0x000F9674
	private void Update()
	{
		float x = 174f;
		float y = -81f;
		if (this.globalRect.anchoredPosition.x + this.globalRect.sizeDelta.x + this.rect.sizeDelta.x > (float)Screen.width / this.settings_.uiScale)
		{
			x = -65f;
		}
		if (Mathf.Abs(this.globalRect.anchoredPosition.y) + this.globalRect.sizeDelta.y > (float)Screen.height / this.settings_.uiScale)
		{
			y = 81f;
		}
		this.rect.anchoredPosition = Vector2.Lerp(this.rect.anchoredPosition, new Vector2(x, y), 0.2f);
	}

	// Token: 0x04001C2F RID: 7215
	public settingsScript settings_;

	// Token: 0x04001C30 RID: 7216
	private RectTransform rect;

	// Token: 0x04001C31 RID: 7217
	private RectTransform globalRect;
}
