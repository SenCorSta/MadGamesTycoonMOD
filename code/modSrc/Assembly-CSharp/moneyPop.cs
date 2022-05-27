using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000275 RID: 629
public class moneyPop : MonoBehaviour
{
	// Token: 0x06001872 RID: 6258 RVA: 0x000F25C8 File Offset: 0x000F07C8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x000F25D0 File Offset: 0x000F07D0
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

	// Token: 0x06001874 RID: 6260 RVA: 0x000F264D File Offset: 0x000F084D
	public void Init(Vector3 v)
	{
		this.FindScripts();
		this.rT_.anchoredPosition = v;
		this.anim_.enabled = true;
		this.anim_.Play();
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x000F2680 File Offset: 0x000F0880
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

	// Token: 0x04001C22 RID: 7202
	public Camera camera_;

	// Token: 0x04001C23 RID: 7203
	public Vector3 myPosition;

	// Token: 0x04001C24 RID: 7204
	public float myTimer;

	// Token: 0x04001C25 RID: 7205
	public settingsScript settings_;

	// Token: 0x04001C26 RID: 7206
	private RectTransform rT_;

	// Token: 0x04001C27 RID: 7207
	private Animation anim_;

	// Token: 0x04001C28 RID: 7208
	public Text text_;
}
