using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class GUI_MainButtons : MonoBehaviour
{
	// Token: 0x060004E6 RID: 1254 RVA: 0x000051C0 File Offset: 0x000033C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00058C88 File Offset: 0x00056E88
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

	// Token: 0x060004E8 RID: 1256 RVA: 0x000051C8 File Offset: 0x000033C8
	private void Update()
	{
		this.CloseDropdownsWithClick();
		this.CloseRoomMenuWithClick();
		this.UpdatePressedButtons();
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00058D70 File Offset: 0x00056F70
	private void CloseDropdownsWithClick()
	{
		if (Input.GetMouseButtonDown(0) && !this.guiMain_.IsMouseOverGUI() && !this.guiMain_.uiObjects[19].activeSelf && !this.guiMain_.uiObjects[20].activeSelf && !this.guiMain_.uiObjects[0].activeSelf && !this.guiMain_.uiObjects[15].activeSelf)
		{
			this.mS_.PauseGame(false);
			this.CloseAllDropdowns();
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00058DF8 File Offset: 0x00056FF8
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

	// Token: 0x060004EB RID: 1259 RVA: 0x00058E48 File Offset: 0x00057048
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

	// Token: 0x060004EC RID: 1260 RVA: 0x00058F10 File Offset: 0x00057110
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

	// Token: 0x060004ED RID: 1261 RVA: 0x000051DC File Offset: 0x000033DC
	private void CheckPause(bool b)
	{
		if (!b)
		{
			this.mS_.PauseGame(this.settings_.pauseUI);
			return;
		}
		this.mS_.PauseGame(false);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000590EC File Offset: 0x000572EC
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

	// Token: 0x060004EF RID: 1263 RVA: 0x0005915C File Offset: 0x0005735C
	public void BUTTON_BuyInventar()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[5].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[5].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x000591B0 File Offset: 0x000573B0
	public void BUTTON_Personal()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[7].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[7].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00059204 File Offset: 0x00057404
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

	// Token: 0x060004F2 RID: 1266 RVA: 0x000592A4 File Offset: 0x000574A4
	public void BUTTON_Publishing()
	{
		this.sfx_.PlaySound(5, true);
		bool activeSelf = this.uiObjects[13].activeSelf;
		this.CloseAllDropdowns();
		this.guiMain_.CloseAllRoomButtons();
		this.uiObjects[13].SetActive(!activeSelf);
		this.CheckPause(activeSelf);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000592F8 File Offset: 0x000574F8
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
