using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E9 RID: 745
public class gummibandScript : MonoBehaviour
{
	// Token: 0x06001A2D RID: 6701 RVA: 0x0010F068 File Offset: 0x0010D268
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

	// Token: 0x06001A2E RID: 6702 RVA: 0x00011A40 File Offset: 0x0000FC40
	private void Update()
	{
		this.UpdateInput();
		this.UpdateGFX();
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x0010F138 File Offset: 0x0010D338
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

	// Token: 0x06001A30 RID: 6704 RVA: 0x0010F2FC File Offset: 0x0010D4FC
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

	// Token: 0x06001A31 RID: 6705 RVA: 0x0010F45C File Offset: 0x0010D65C
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

	// Token: 0x06001A32 RID: 6706 RVA: 0x00011A4E File Offset: 0x0000FC4E
	private IEnumerator DisableEndOfFrame()
	{
		yield return new WaitForEndOfFrame();
		this.isActive = false;
		yield break;
	}

	// Token: 0x04002161 RID: 8545
	private RectTransform rT;

	// Token: 0x04002162 RID: 8546
	public GameObject myObject;

	// Token: 0x04002163 RID: 8547
	private Vector2 start;

	// Token: 0x04002164 RID: 8548
	public bool isActive;

	// Token: 0x04002165 RID: 8549
	private Image myImage;

	// Token: 0x04002166 RID: 8550
	private GUI_Main guiMain_;

	// Token: 0x04002167 RID: 8551
	private mainScript mS_;

	// Token: 0x04002168 RID: 8552
	private Camera camera_;

	// Token: 0x04002169 RID: 8553
	private settingsScript settings_;

	// Token: 0x0400216A RID: 8554
	private pickCharacterScript pcS_;

	// Token: 0x0400216B RID: 8555
	private float timer;

	// Token: 0x0400216C RID: 8556
	private Vector2 vPos;

	// Token: 0x0400216D RID: 8557
	private Vector2 vSize;
}
