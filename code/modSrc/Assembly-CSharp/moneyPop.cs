using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000271 RID: 625
public class moneyPop : MonoBehaviour
{
	// Token: 0x0600182F RID: 6191 RVA: 0x00010C77 File Offset: 0x0000EE77
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000F77C8 File Offset: 0x000F59C8
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

	// Token: 0x06001831 RID: 6193 RVA: 0x00010C7F File Offset: 0x0000EE7F
	public void Init(Vector3 v)
	{
		this.FindScripts();
		this.rT_.anchoredPosition = v;
		this.anim_.enabled = true;
		this.anim_.Play();
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000F7848 File Offset: 0x000F5A48
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

	// Token: 0x04001C08 RID: 7176
	public Camera camera_;

	// Token: 0x04001C09 RID: 7177
	public Vector3 myPosition;

	// Token: 0x04001C0A RID: 7178
	public float myTimer;

	// Token: 0x04001C0B RID: 7179
	public settingsScript settings_;

	// Token: 0x04001C0C RID: 7180
	private RectTransform rT_;

	// Token: 0x04001C0D RID: 7181
	private Animation anim_;

	// Token: 0x04001C0E RID: 7182
	public Text text_;
}
