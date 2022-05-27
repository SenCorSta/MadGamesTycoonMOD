using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class gummibandScript : MonoBehaviour
{
	
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

	
	private void Update()
	{
		this.UpdateInput();
		this.UpdateGFX();
	}

	
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

	
	private IEnumerator DisableEndOfFrame()
	{
		yield return new WaitForEndOfFrame();
		this.isActive = false;
		yield break;
	}

	
	private RectTransform rT;

	
	public GameObject myObject;

	
	private Vector2 start;

	
	public bool isActive;

	
	private Image myImage;

	
	private GUI_Main guiMain_;

	
	private mainScript mS_;

	
	private Camera camera_;

	
	private settingsScript settings_;

	
	private pickCharacterScript pcS_;

	
	private float timer;

	
	private Vector2 vPos;

	
	private Vector2 vSize;
}
