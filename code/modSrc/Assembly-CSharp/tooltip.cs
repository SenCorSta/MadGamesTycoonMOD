using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200027E RID: 638
public class tooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001903 RID: 6403 RVA: 0x000F8D87 File Offset: 0x000F6F87
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x000F8D90 File Offset: 0x000F6F90
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

	// Token: 0x06001905 RID: 6405 RVA: 0x000F8E44 File Offset: 0x000F7044
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

	// Token: 0x06001906 RID: 6406 RVA: 0x000F8FA4 File Offset: 0x000F71A4
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

	// Token: 0x06001907 RID: 6407 RVA: 0x000F90C3 File Offset: 0x000F72C3
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x000F90C3 File Offset: 0x000F72C3
	public void OnDisable()
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	// Token: 0x04001C62 RID: 7266
	private textScript tS_;

	// Token: 0x04001C63 RID: 7267
	private settingsScript settings_;

	// Token: 0x04001C64 RID: 7268
	private GameObject main_;

	// Token: 0x04001C65 RID: 7269
	private mainScript mS_;

	// Token: 0x04001C66 RID: 7270
	private GUI_Main guiMain_;

	// Token: 0x04001C67 RID: 7271
	public int textID = -1;

	// Token: 0x04001C68 RID: 7272
	public string textArray = "";

	// Token: 0x04001C69 RID: 7273
	public string c = "";

	// Token: 0x04001C6A RID: 7274
	public KeyCode shortcut;

	// Token: 0x04001C6B RID: 7275
	private GUI_Tooltip guiTooltip;

	// Token: 0x04001C6C RID: 7276
	public Camera mainCamera;

	// Token: 0x04001C6D RID: 7277
	private RaycastHit raycastHit;

	// Token: 0x04001C6E RID: 7278
	private float middleMouseTimer;
}
