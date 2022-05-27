using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EC RID: 748
public class gummibandScript : MonoBehaviour
{
	// Token: 0x06001A77 RID: 6775 RVA: 0x0010B130 File Offset: 0x00109330
	private void Start()
	{
		if (!this.mS_)
		{
			this.mS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<mainScript>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<pickCharacterScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = GameObject.FindGameObjectWithTag("Main").GetComponent<settingsScript>();
		}
		if (!this.camera_)
		{
			this.camera_ = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		this.guiMain_ = base.GetComponent<GUI_Main>();
		this.rT = this.myObject.GetComponent<RectTransform>();
		this.myImage = this.myObject.GetComponent<Image>();
		this.myImage.enabled = false;
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x0010B1FF File Offset: 0x001093FF
	private void Update()
	{
		this.UpdateInput();
		this.UpdateGFX();
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x0010B210 File Offset: 0x00109410
	private void UpdateGFX()
	{
		if (!this.myImage.enabled)
		{
			return;
		}
		this.vPos = new Vector2(0f, 0f);
		this.vSize = new Vector2(Mathf.Abs(Input.mousePosition.x - this.start.x), Mathf.Abs(Input.mousePosition.y - this.start.y));
		if (Input.mousePosition.x - this.start.x >= 0f)
		{
			this.vPos = new Vector2(this.start.x, this.vPos.y);
		}
		if (Input.mousePosition.y - this.start.y >= 0f)
		{
			this.vPos = new Vector2(this.vPos.x, this.start.y);
		}
		if (Input.mousePosition.x - this.start.x < 0f)
		{
			this.vPos = new Vector2(this.start.x - this.vSize.x, this.vPos.y);
		}
		if (Input.mousePosition.y - this.start.y < 0f)
		{
			this.vPos = new Vector2(this.vPos.x, this.start.y - this.vSize.y);
		}
		this.rT.anchoredPosition = this.vPos / this.settings_.uiScale;
		this.rT.sizeDelta = this.vSize / this.settings_.uiScale;
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x0010B3D4 File Offset: 0x001095D4
	private void UpdateInput()
	{
		if (this.isActive && this.guiMain_.menuOpen)
		{
			if (this.myImage.enabled)
			{
				this.myImage.enabled = false;
			}
			this.isActive = false;
		}
		if (this.guiMain_.uiObjects[2].GetComponent<Toggle>().isOn)
		{
			return;
		}
		if (!this.guiMain_.IsMouseOverGUI() && !this.isActive && this.mS_.pickedChars.Count == 0 && !this.guiMain_.menuOpen)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.start = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				this.vSize = new Vector2(0f, 0f);
				if (!this.myImage.enabled)
				{
					this.myImage.enabled = true;
				}
			}
			if (Input.GetMouseButton(0) && this.vSize.x > 32f && this.vSize.y > 32f)
			{
				this.isActive = true;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.myImage.enabled)
			{
				this.myImage.enabled = false;
			}
			if (this.isActive)
			{
				this.SelectCharacters();
				base.StartCoroutine(this.DisableEndOfFrame());
			}
		}
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x0010B534 File Offset: 0x00109734
	private void SelectCharacters()
	{
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				Vector3 position = this.mS_.arrayCharacters[i].transform.position;
				position.y += 0.5f;
				Vector2 vector = this.camera_.WorldToScreenPoint(position);
				if (vector.x >= this.vPos.x && vector.x <= this.vPos.x + this.vSize.x && vector.y >= this.vPos.y && vector.y <= this.vPos.y + this.vSize.y)
				{
					base.StartCoroutine(this.pcS_.PickChar(this.mS_.arrayCharacters[i]));
				}
			}
		}
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x0010B633 File Offset: 0x00109833
	private IEnumerator DisableEndOfFrame()
	{
		yield return new WaitForEndOfFrame();
		this.isActive = false;
		yield break;
	}

	// Token: 0x0400217B RID: 8571
	private RectTransform rT;

	// Token: 0x0400217C RID: 8572
	public GameObject myObject;

	// Token: 0x0400217D RID: 8573
	private Vector2 start;

	// Token: 0x0400217E RID: 8574
	public bool isActive;

	// Token: 0x0400217F RID: 8575
	private Image myImage;

	// Token: 0x04002180 RID: 8576
	private GUI_Main guiMain_;

	// Token: 0x04002181 RID: 8577
	private mainScript mS_;

	// Token: 0x04002182 RID: 8578
	private Camera camera_;

	// Token: 0x04002183 RID: 8579
	private settingsScript settings_;

	// Token: 0x04002184 RID: 8580
	private pickCharacterScript pcS_;

	// Token: 0x04002185 RID: 8581
	private float timer;

	// Token: 0x04002186 RID: 8582
	private Vector2 vPos;

	// Token: 0x04002187 RID: 8583
	private Vector2 vSize;
}
