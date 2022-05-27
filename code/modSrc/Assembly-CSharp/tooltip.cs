using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200027A RID: 634
public class tooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060018BE RID: 6334 RVA: 0x00010F6A File Offset: 0x0000F16A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x000FDB50 File Offset: 0x000FBD50
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x000FDC04 File Offset: 0x000FBE04
	private void Update()
	{
		if (this.guiMain_.selectInputField)
		{
			return;
		}
		if (this.shortcut != KeyCode.None && !Input.GetKey(KeyCode.LeftShift))
		{
			bool flag = false;
			if (this.settings_.middleMouseClose && this.shortcut == KeyCode.Escape)
			{
				if (Input.GetMouseButton(1))
				{
					this.middleMouseTimer += Time.deltaTime;
				}
				if (Input.GetMouseButtonUp(1))
				{
					if (this.middleMouseTimer < 0.15f)
					{
						flag = true;
					}
					this.middleMouseTimer = 0f;
				}
			}
			if (Input.GetKeyUp(this.shortcut) || flag)
			{
				if (base.GetComponent<Button>() && base.GetComponent<Button>().interactable)
				{
					this.FindScripts();
					this.guiMain_.SetUIHotkey(base.gameObject);
					return;
				}
				if (base.transform.parent.GetComponent<Toggle>() && base.transform.parent.GetComponent<Toggle>().interactable)
				{
					base.transform.parent.GetComponent<Toggle>().isOn = !base.transform.parent.GetComponent<Toggle>().isOn;
					return;
				}
				if (base.GetComponent<Toggle>() && base.GetComponent<Toggle>().interactable)
				{
					base.GetComponent<Toggle>().isOn = !base.GetComponent<Toggle>().isOn;
					return;
				}
			}
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000FDD64 File Offset: 0x000FBF64
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.FindScripts();
		if (!this.guiTooltip)
		{
			this.guiTooltip = base.transform.root.GetComponent<GUI_Tooltip>();
		}
		if (this.guiTooltip)
		{
			if (this.textArray.Length > 0 && this.textID > -1)
			{
				string a = this.textArray;
				if (!(a == "text"))
				{
					if (a == "country")
					{
						this.c = this.tS_.GetCountry(this.textID);
					}
				}
				else
				{
					this.c = this.tS_.GetText(this.textID);
				}
			}
			if (this.shortcut != KeyCode.None)
			{
				this.c = string.Concat(new string[]
				{
					this.c,
					"<br><br><color=purple><i>",
					this.tS_.GetText(87),
					" <",
					this.shortcut.ToString(),
					"></i></color>"
				});
			}
			this.guiTooltip.SetActive(this.c);
		}
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x00010F72 File Offset: 0x0000F172
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x00010F72 File Offset: 0x0000F172
	public void OnDisable()
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	// Token: 0x04001C47 RID: 7239
	private textScript tS_;

	// Token: 0x04001C48 RID: 7240
	private settingsScript settings_;

	// Token: 0x04001C49 RID: 7241
	private GameObject main_;

	// Token: 0x04001C4A RID: 7242
	private mainScript mS_;

	// Token: 0x04001C4B RID: 7243
	private GUI_Main guiMain_;

	// Token: 0x04001C4C RID: 7244
	public int textID = -1;

	// Token: 0x04001C4D RID: 7245
	public string textArray = "";

	// Token: 0x04001C4E RID: 7246
	public string c = "";

	// Token: 0x04001C4F RID: 7247
	public KeyCode shortcut;

	// Token: 0x04001C50 RID: 7248
	private GUI_Tooltip guiTooltip;

	// Token: 0x04001C51 RID: 7249
	public Camera mainCamera;

	// Token: 0x04001C52 RID: 7250
	private RaycastHit raycastHit;

	// Token: 0x04001C53 RID: 7251
	private float middleMouseTimer;
}
