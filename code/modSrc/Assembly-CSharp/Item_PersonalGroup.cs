using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_PersonalGroup : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.frames++;
		if (this.frames < 3)
		{
			return;
		}
		if (!this.myRect_)
		{
			this.myRect_ = base.GetComponent<RectTransform>();
		}
		if (this.myRect_.position.y >= 0f && this.myRect_.position.y <= (float)Screen.height)
		{
			this.EnableObjects();
		}
	}

	
	public void EnableObjects()
	{
		if (this.hasEnabled)
		{
			return;
		}
		this.hasEnabled = true;
		for (int i = 0; i < this.uiObjects.Length; i++)
		{
			if (this.uiObjects[i] && !this.uiObjects[i].activeSelf)
			{
				this.uiObjects[i].SetActive(true);
			}
		}
	}

	
	public void SetData(string s, float val)
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.cS_.myName;
		this.uiObjects[10].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[4].GetComponent<Image>().fillAmount = this.cS_.s_motivation * 0.01f;
		this.uiObjects[4].GetComponent<Image>().color = this.GetValColor(this.cS_.s_motivation);
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.cS_.s_motivation).ToString();
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[7].transform);
		if (this.cS_.roomS_)
		{
			this.uiObjects[9].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.cS_.roomS_.typ];
		}
		if (this.cS_.krank > 0)
		{
			this.uiObjects[8].SetActive(true);
			return;
		}
		this.uiObjects[8].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		Menu_PersonalGroups component = this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>();
		if (this.cS_.group == -1)
		{
			this.cS_.group = component.uiObjects[6].GetComponent<Dropdown>().value + 1;
			base.gameObject.transform.parent = component.uiObjects[4].transform;
		}
		else
		{
			this.cS_.group = -1;
			base.gameObject.transform.parent = component.uiObjects[0].transform;
		}
		component.DROPDOWN_Sort();
	}

	
	public int characterID = -1;

	
	public characterScript cS_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public roomDataScript rdS_;

	
	private RectTransform myRect_;

	
	private int frames;

	
	private bool hasEnabled;
}
