using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_CopyProtect : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cpS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.cpS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.cpS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.cpS_.effekt);
		this.tooltip_.c = this.cpS_.GetTooltip();
		if (this.guiMain_.uiObjects[365].activeSelf && this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().gS_.gameCopyProtect == this.cpS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
			this.uiObjects[1].GetComponent<Text>().text = "$0";
		}
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

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetCopyProtect(this.cpS_.myID);
			this.guiMain_.uiObjects[68].GetComponent<Menu_DevGame_CopyProtect>().BUTTON_Close();
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().SetCopyProtect(this.cpS_.myID);
			this.guiMain_.uiObjects[68].GetComponent<Menu_DevGame_CopyProtect>().BUTTON_Close();
		}
	}

	
	public copyProtectScript cpS_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	private float updateTimer;
}
