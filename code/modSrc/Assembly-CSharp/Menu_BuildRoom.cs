using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_BuildRoom : MonoBehaviour
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
		this.main_ = GameObject.Find("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.tS_ = this.main_.GetComponent<textScript>();
		this.mapS_ = this.main_.GetComponent<mapScript>();
		this.unlock_ = this.main_.GetComponent<unlockScript>();
		this.rdS_ = this.main_.GetComponent<roomDataScript>();
		this.buildRoomScript_ = this.main_.GetComponent<buildRoomScript>();
		this.mCamS_ = GameObject.Find("Camera").GetComponent<mainCameraScript>();
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(5);
		}
	}

	
	private void OnDisable()
	{
		this.uiObjects[27].SetActive(false);
		this.uiObjects[29].SetActive(false);
	}

	
	private void Update()
	{
		if (this.mS_.multiplayer && !this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.Update_DesignRoomMenu();
	}

	
	private void Update_DesignRoomMenu()
	{
		if (!Input.GetMouseButton(0) && this.buildRoomScript_.modus == 0 && Input.GetKeyDown(KeyCode.LeftShift))
		{
			this.BUTTON_RemoveRoom();
		}
		if (this.buildRoomScript_.modus == 1 && Input.GetKeyUp(KeyCode.LeftShift))
		{
			this.buildRoomScript_.Remove_DeleteMap();
			this.BUTTON_SetRoom();
		}
		int num = this.buildRoomScript_.AmountTiles();
		this.uiObjects[2].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.roomTyp];
		this.uiObjects[23].GetComponent<Text>().text = num.ToString() + " " + this.tS_.GetText(72);
		this.uiObjects[24].GetComponent<Text>().text = this.mS_.GetMoney((long)this.rdS_.GetPrice(this.roomTyp), true);
		this.uiObjects[25].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetRoomPrice(), true);
		this.uiObjects[26].GetComponent<Text>().text = this.rdS_.GetName(this.roomTyp);
		if (!this.rdS_.KeineMitarbeiter(this.roomTyp))
		{
			string text = this.tS_.GetText(1681);
			text = text.Replace("<NUM>", this.AnzahlArbeitsplaetze().ToString());
			this.uiObjects[31].GetComponent<Text>().text = text;
		}
		else
		{
			this.uiObjects[31].GetComponent<Text>().text = "";
		}
		this.UpdateSizePanel();
		this.UpdateAcceptButton();
		this.UpdateCloseButton();
		if (this.buildRoomScript_.modus == 0 || this.buildRoomScript_.modus == 1 || this.buildRoomScript_.modus == 3)
		{
			if (!this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(true);
			}
			if (this.uiObjects[1].activeSelf)
			{
				this.uiObjects[1].SetActive(false);
				return;
			}
		}
		else
		{
			if (this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(false);
			}
			if (!this.uiObjects[1].activeSelf)
			{
				this.uiObjects[1].SetActive(true);
			}
		}
	}

	
	public int AnzahlArbeitsplaetze()
	{
		float num = 3.3f;
		int num2 = this.roomTyp;
		if (num2 != 5)
		{
			if (num2 != 10)
			{
				if (num2 == 13)
				{
					num = 2.7f;
				}
			}
			else
			{
				num = 10f;
			}
		}
		else
		{
			num = 5f;
		}
		int num3 = Mathf.FloorToInt((float)this.buildRoomScript_.AmountTiles() / num);
		if (num3 < 0)
		{
			num3 = 0;
		}
		return num3;
	}

	
	private int GetRoomPrice()
	{
		int num = this.buildRoomScript_.AmountTiles();
		int num2 = this.rdS_.GetPrice(this.roomTyp) * num;
		num2 -= this.buildRoomScript_.moneyRedesign;
		if (num2 < 0)
		{
			num2 = 0;
		}
		return num2;
	}

	
	private void UpdateSizePanel()
	{
		if ((this.buildRoomScript_.modus == 0 || this.buildRoomScript_.modus == 1) && !this.guiMain_.IsMouseOverGUI() && Input.GetMouseButton(0))
		{
			if (!this.uiObjects[27].activeSelf)
			{
				this.uiObjects[27].SetActive(true);
			}
			Vector2 v = Input.mousePosition;
			v.x += 10f;
			v.y += 10f;
			this.uiObjects[27].GetComponent<RectTransform>().anchoredPosition = this.guiMain_.GetAnchoredPosition(v);
			this.uiObjects[28].GetComponent<Text>().text = (Mathf.Abs(this.buildRoomScript_.roomStartX - this.buildRoomScript_.posX) + 1).ToString() + "x" + (Mathf.Abs(this.buildRoomScript_.roomStartY - this.buildRoomScript_.posY) + 1).ToString();
			return;
		}
		if (this.uiObjects[27].activeSelf)
		{
			this.uiObjects[27].SetActive(false);
		}
	}

	
	private void UpdateCloseButton()
	{
		if (this.buildRoomScript_.replaceRoomID == -1)
		{
			if (!this.uiObjects[16].activeSelf)
			{
				this.uiObjects[16].SetActive(true);
			}
			if (!this.uiObjects[3].activeSelf)
			{
				this.uiObjects[3].SetActive(true);
				return;
			}
		}
		else
		{
			if (this.uiObjects[16].activeSelf)
			{
				this.uiObjects[16].SetActive(false);
			}
			if (this.uiObjects[3].activeSelf)
			{
				this.uiObjects[3].SetActive(false);
			}
		}
	}

	
	private void UpdateAcceptButton()
	{
		Button component = this.uiObjects[17].GetComponent<Button>();
		int num = 3;
		if (this.roomTyp == 5)
		{
			num = 4;
		}
		if (this.roomTyp == 10)
		{
			num = 5;
		}
		if (this.roomTyp == 14)
		{
			num = 5;
		}
		if (this.roomTyp == 9)
		{
			num = 2;
		}
		if (!this.buildRoomScript_.ExistRoom())
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			return;
		}
		if (this.buildRoomScript_.GetBiggestRoomQuad() < (float)num)
		{
			string text = this.tS_.GetText(75);
			text = text.Replace("<NUM>", num.ToString() + "x" + num.ToString());
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = text;
			return;
		}
		if (!this.buildRoomScript_.IsDoor())
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(76);
			return;
		}
		if (this.buildRoomScript_.noPath)
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(73);
			return;
		}
		component.interactable = true;
		this.uiObjects[18].GetComponent<Image>().color = Color.white;
		if (this.uiObjects[29].activeSelf)
		{
			this.uiObjects[29].SetActive(false);
		}
	}

	
	public void BUTTON_AcceptRoomDesign()
	{
		if (!this.mS_.settings_TutorialOff && this.roomTyp == 1)
		{
			this.guiMain_.SetTutorialStep(6);
		}
		this.sfx_.PlaySound(7, true);
		this.buildRoomScript_.CreateRoom(this.roomTyp, this.GetRoomPrice());
		this.buildRoomScript_.SetInactive();
		this.buildRoomScript_.modus = 0;
		this.buildRoomScript_.moneyRedesign = 0;
		this.Close_DesignRoom();
		this.guiMain_.TOGGLE_Walls();
		this.guiMain_.DROPDOWN_BuyInventar(this.roomTyp);
	}

	
	private void ResetButtonColors()
	{
		this.uiObjects[19].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[20].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[21].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[22].GetComponent<Image>().color = this.colors[1];
	}

	
	public void BUTTON_SetRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 0;
		this.ResetButtonColors();
		this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
	}

	
	public void BUTTON_RemoveRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 1;
		this.ResetButtonColors();
		this.uiObjects[20].GetComponent<Image>().color = this.colors[0];
	}

	
	public void BUTTON_SetDoor()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 2;
		this.ResetButtonColors();
		this.uiObjects[21].GetComponent<Image>().color = this.colors[0];
	}

	
	public void BUTTON_SetWindow()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 3;
		this.ResetButtonColors();
		this.uiObjects[22].GetComponent<Image>().color = this.colors[0];
	}

	
	public void BUTTON_Grab()
	{
		this.buildRoomScript_.modus = 4;
	}

	
	public void BUTTON_SelectRoom(int i)
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		this.uiObjects[27].SetActive(false);
		this.uiObjects[29].SetActive(false);
		this.roomTyp = i;
		this.buildRoomScript_.noPath = true;
		this.buildRoomScript_.SetActive();
		this.BUTTON_SetRoom();
		this.mS_.SetBuildGrid(true);
	}

	
	public void Close_DesignRoom()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				array[i].GetComponent<roomScript>().SetListGameObjectsLayer(0);
			}
		}
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.SetInactive();
		this.buildRoomScript_.modus = 0;
		base.StartCoroutine(this.mS_.UpdatePathfindingNextFrame());
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.mS_.SetBuildGrid(false);
	}

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPrefabs;

	
	public Color[] colors;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private mapScript mapS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private buildRoomScript buildRoomScript_;

	
	private roomDataScript rdS_;

	
	private mainCameraScript mCamS_;

	
	private sfxScript sfx_;

	
	public int roomTyp;
}
