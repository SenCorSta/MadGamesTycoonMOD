using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class roomOptionsPosition : MonoBehaviour
{
	// Token: 0x060018D1 RID: 6353 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x000F6538 File Offset: 0x000F4738
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

	// Token: 0x060018D3 RID: 6355 RVA: 0x000F65A3 File Offset: 0x000F47A3
	private void OnEnable()
	{
		this.FindScripts();
		this.rect.anchoredPosition = new Vector2(90f, -40f);
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000F65C8 File Offset: 0x000F47C8
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

	// Token: 0x04001C4A RID: 7242
	public settingsScript settings_;

	// Token: 0x04001C4B RID: 7243
	private RectTransform rect;

	// Token: 0x04001C4C RID: 7244
	private RectTransform globalRect;
}
