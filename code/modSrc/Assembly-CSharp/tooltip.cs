using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class tooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	
	public void OnDisable()
	{
		if (this.guiTooltip)
		{
			this.guiTooltip.SetInactive();
		}
	}

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private GUI_Main guiMain_;

	
	public int textID = -1;

	
	public string textArray = "";

	
	public string c = "";

	
	public KeyCode shortcut;

	
	private GUI_Tooltip guiTooltip;

	
	public Camera mainCamera;

	
	private RaycastHit raycastHit;

	
	private float middleMouseTimer;
}
