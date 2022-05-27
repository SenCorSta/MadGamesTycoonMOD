using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class GUI_MainButtons : MonoBehaviour
{
	// Token: 0x060004EF RID: 1263 RVA: 0x000458EB File Offset: 0x00043AEB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x000458F4 File Offset: 0x00043AF4
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

	// Token: 0x060004F1 RID: 1265 RVA: 0x000459DA File Offset: 0x00043BDA
	private void Update()
	{
		this.CloseDropdownsWithClick();
		this.CloseRoomMenuWithClick();
		this.UpdatePressedButtons();
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x000459F0 File Offset: 0x00043BF0
	private void CloseDropdownsWithClick()
	{
		if (Input.GetMouseButtonDown(0) && !this.guiMain_.IsMouseOverGUI() && !this.guiMain_.uiObjects[19].activeSelf && !this.guiMain_.uiObjects[20].activeSelf && !this.guiMain_.uiObjects[0].activeSelf && !this.guiMain_.uiObjects[15].activeSelf)
		{
			this.mS_.PauseGame(false);
			this.CloseAllDropdowns();
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00045A78 File Offset: 0x00043C78
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

	// Token: 0x060004F4 RID: 1268 RVA: 0x00045AC8 File Offset: 0x00043CC8
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

	// Token: 0x060004F5 RID: 1269 RVA: 0x00045B90 File Offset: 0x00043D90
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

	// Token: 0x060004F6 RID: 1270 RVA: 0x00045D69 File Offset: 0x00043F69
	private void CheckPause(bool b)
	{
		if (!b)
		{
			this.mS_.PauseGame(this.settings_.pauseUI);
			return;
		}
		this.mS_.PauseGame(false);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00045D94 File Offset: 0x00043F94
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

	// Token: 0x060004F8 RID: 1272 RVA: 0x00045E04 File Offset: 0x00044004
	public void BUTTON_BuyInventar()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[5].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[5].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00045E58 File Offset: 0x00044058
	public void BUTTON_Personal()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[7].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[7].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00045EAC File Offset: 0x000440AC
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

	// Token: 0x060004FB RID: 1275 RVA: 0x00045F4C File Offset: 0x0004414C
	public void BUTTON_Publishing()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[13].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[13].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00045FA0 File Offset: 0x000441A0
	public void BUTTON_Statistics()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[11].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[11].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x040007CF RID: 1999
	public GameObject[] uiObjects;

	// Token: 0x040007D0 RID: 2000
	public GameObject[] uiPrefabs;

	// Token: 0x040007D1 RID: 2001
	public Color[] uiColors;

	// Token: 0x040007D2 RID: 2002
	private GameObject main_;

	// Token: 0x040007D3 RID: 2003
	private mainScript mS_;

	// Token: 0x040007D4 RID: 2004
	private textScript tS_;

	// Token: 0x040007D5 RID: 2005
	private sfxScript sfx_;

	// Token: 0x040007D6 RID: 2006
	private GUI_Main guiMain_;

	// Token: 0x040007D7 RID: 2007
	private settingsScript settings_;

	// Token: 0x040007D8 RID: 2008
	private buildRoomScript buildRoomScript_;
}
