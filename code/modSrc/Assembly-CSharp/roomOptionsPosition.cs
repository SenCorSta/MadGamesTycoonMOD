using System;
using UnityEngine;


public class roomOptionsPosition : MonoBehaviour
{
	
	private void Start()
	{
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.rect.anchoredPosition = new Vector2(90f, -40f);
	}

	
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

	
	public settingsScript settings_;

	
	private RectTransform rect;

	
	private RectTransform globalRect;
}
