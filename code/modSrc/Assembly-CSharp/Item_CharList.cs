using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item_CharList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	
	private void Start()
	{
		this.Init();
	}

	
	private void Init()
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
		base.gameObject.GetComponent<tooltip>().c = this.cS_.GetTooltip();
	}

	
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			this.guiMain_.uiObjects[15].GetComponent<Menu_PickCharacter>().UpdateData();
			return;
		}
		if (this.mS_.pickedChars.Count > 0)
		{
			if (this.mS_.pickedChars[0] == this.cS_.gameObject)
			{
				this.uiObjects[0].GetComponent<Text>().text = "• " + this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[1];
	}

	
	public void OnPointerExit(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[0];
	}

	
	public void BUTTON_Click()
	{
		if (!this.cS_)
		{
			return;
		}
		for (int i = 0; i < this.mS_.pickedChars.Count; i++)
		{
			if (this.mS_.pickedChars[i] == this.cS_.gameObject)
			{
				this.mS_.pickedChars.RemoveAt(i);
				break;
			}
		}
		this.mS_.pickedChars.Insert(0, this.cS_.gameObject);
		this.guiMain_.uiObjects[15].GetComponent<Menu_PickCharacter>().UpdateData();
	}

	
	public GameObject[] uiObjects;

	
	public Color[] colors;

	
	public int slot;

	
	public characterScript cS_;

	
	public mainScript mS_;

	
	public GUI_Main guiMain_;
}
