using System;
using UnityEngine;
using UnityEngine.UI;


public class moneyPop : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.rT_)
		{
			this.rT_ = base.gameObject.GetComponent<RectTransform>();
		}
		if (!this.anim_)
		{
			this.anim_ = base.gameObject.transform.GetChild(0).GetComponent<Animation>();
		}
		if (!this.text_)
		{
			this.text_ = base.gameObject.transform.GetChild(0).GetComponent<Text>();
		}
	}

	
	public void Init(Vector3 v)
	{
		this.FindScripts();
		this.rT_.anchoredPosition = v;
		this.anim_.enabled = true;
		this.anim_.Play();
	}

	
	private void Update()
	{
		if (base.transform.position.x >= 99998f)
		{
			return;
		}
		this.myTimer += Time.deltaTime;
		if (this.myTimer > 3f)
		{
			base.transform.position = new Vector3(99999f, 99999f, 0f);
			this.anim_.enabled = false;
			this.text_.text = "";
			return;
		}
		if (this.settings_)
		{
			Vector2 vector = this.camera_.WorldToScreenPoint(this.myPosition);
			if (vector.x >= 0f && vector.x <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height)
			{
				vector = new Vector2(vector.x, vector.y - (float)Screen.height);
				vector /= this.settings_.uiScale;
				this.rT_.anchoredPosition = vector;
				return;
			}
			base.transform.position = new Vector3(99999f, 99999f, 0f);
		}
	}

	
	public Camera camera_;

	
	public Vector3 myPosition;

	
	public float myTimer;

	
	public settingsScript settings_;

	
	private RectTransform rT_;

	
	private Animation anim_;

	
	public Text text_;
}
