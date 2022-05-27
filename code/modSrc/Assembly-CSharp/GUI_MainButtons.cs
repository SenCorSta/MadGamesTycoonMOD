using System;
using UnityEngine;


public class GUI_MainButtons : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.buildRoomScript_)
		{
			this.buildRoomScript_ = this.main_.GetComponent<buildRoomScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	private void Update()
	{
		this.CloseDropdownsWithClick();
		this.CloseRoomMenuWithClick();
		this.UpdatePressedButtons();
	}

	
	private void CloseDropdownsWithClick()
	{
		if (Input.GetMouseButtonDown(0) && !this.guiMain_.IsMouseOverGUI() && !this.guiMain_.uiObjects[19].activeSelf && !this.guiMain_.uiObjects[20].activeSelf && !this.guiMain_.uiObjects[0].activeSelf && !this.guiMain_.uiObjects[15].activeSelf)
		{
			this.mS_.PauseGame(false);
			this.CloseAllDropdowns();
		}
	}

	
	private void CloseRoomMenuWithClick()
	{
		if (Input.GetMouseButtonDown(0) && !this.guiMain_.IsMouseOverGUI() && this.guiMain_.IsRoomMenuOpen())
		{
			this.CloseAllDropdowns();
			this.guiMain_.CloseAllRoomButtons();
			this.mS_.PauseGame(false);
			return;
		}
	}

	
	public void CloseAllDropdowns()
	{
		this.FindScripts();
		if (this.uiObjects[4].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
		}
		if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		if (this.uiObjects[7].activeSelf)
		{
			this.uiObjects[7].SetActive(false);
		}
		if (this.uiObjects[9].activeSelf)
		{
			this.uiObjects[9].SetActive(false);
		}
		if (this.uiObjects[11].activeSelf)
		{
			this.uiObjects[11].SetActive(false);
		}
		if (this.uiObjects[13].activeSelf)
		{
			this.uiObjects[13].SetActive(false);
		}
	}

	
	private void UpdatePressedButtons()
	{
		if (this.uiObjects[4].activeSelf)
		{
			if (!this.uiObjects[2].activeSelf)
			{
				this.uiObjects[2].SetActive(true);
			}
		}
		else if (this.uiObjects[2].activeSelf)
		{
			this.uiObjects[2].SetActive(false);
		}
		if (this.uiObjects[5].activeSelf)
		{
			if (!this.uiObjects[3].activeSelf)
			{
				this.uiObjects[3].SetActive(true);
			}
		}
		else if (this.uiObjects[3].activeSelf)
		{
			this.uiObjects[3].SetActive(false);
		}
		if (this.uiObjects[7].activeSelf)
		{
			if (!this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(true);
			}
		}
		else if (this.uiObjects[6].activeSelf)
		{
			this.uiObjects[6].SetActive(false);
		}
		if (this.uiObjects[9].activeSelf)
		{
			if (!this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(true);
			}
		}
		else if (this.uiObjects[8].activeSelf)
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.uiObjects[13].activeSelf)
		{
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
			}
		}
		else if (this.uiObjects[12].activeSelf)
		{
			this.uiObjects[12].SetActive(false);
		}
		if (this.uiObjects[11].activeSelf)
		{
			if (!this.uiObjects[10].activeSelf)
			{
				this.uiObjects[10].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[10].activeSelf)
		{
			this.uiObjects[10].SetActive(false);
		}
	}

	
	private void CheckPause(bool b)
	{
		if (!b)
		{
			this.mS_.PauseGame(this.settings_.pauseUI);
			return;
		}
		this.mS_.PauseGame(false);
	}

	
	public void BUTTON_BuildRoom()
	{
		if (!this.mS_.settings_TutorialOff && this.guiMain_.GetTutorialStep() < 4)
		{
			return;
		}
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[4].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[4].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public void BUTTON_BuyInventar()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[5].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[5].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public void BUTTON_Personal()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[7].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[7].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public void BUTTON_Management()
	{
		if (!this.mS_.multiplayer)
		{
			if (!this.uiObjects[14].activeSelf)
			{
				this.uiObjects[14].SetActive(true);
			}
		}
		else if (this.uiObjects[14].activeSelf)
		{
			this.uiObjects[14].SetActive(false);
		}
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[9].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[9].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public void BUTTON_Publishing()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[13].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[13].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public void BUTTON_Statistics()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[11].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[11].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPrefabs;

	
	public Color[] uiColors;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private sfxScript sfx_;

	
	private GUI_Main guiMain_;

	
	private settingsScript settings_;

	
	private buildRoomScript buildRoomScript_;
}
