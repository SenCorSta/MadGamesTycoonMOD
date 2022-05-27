using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Support_Fankampagne : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void Update()
	{
		if (this.selectedKampagne == -1)
		{
			this.uiObjects[25].GetComponent<Button>().interactable = false;
			return;
		}
		this.uiObjects[25].GetComponent<Button>().interactable = true;
	}

	
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedKampagne = -1;
		this.SetButtonColor(-1);
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[0], true);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[1], true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[2], true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[3], true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[4], true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[5], true);
		this.uiObjects[16].GetComponent<Text>().text = "+" + this.fans[0].ToString();
		this.uiObjects[17].GetComponent<Text>().text = "+" + this.fans[1].ToString();
		this.uiObjects[18].GetComponent<Text>().text = "+" + this.fans[2].ToString();
		this.uiObjects[19].GetComponent<Text>().text = "+" + this.fans[3].ToString();
		this.uiObjects[20].GetComponent<Text>().text = "+" + this.fans[4].ToString();
		this.uiObjects[21].GetComponent<Text>().text = "+" + this.fans[5].ToString();
	}

	
	private void SetButtonColor(int i)
	{
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[4].GetComponent<Image>().color = Color.white;
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
		this.uiObjects[6].GetComponent<Image>().color = Color.white;
		this.uiObjects[7].GetComponent<Image>().color = Color.white;
		if (i != -1)
		{
			switch (i)
			{
			case 0:
				this.uiObjects[2].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 1:
				this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 2:
				this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 3:
				this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 4:
				this.uiObjects[6].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 5:
				this.uiObjects[7].GetComponent<Image>().color = this.guiMain_.colors[7];
				break;
			default:
				return;
			}
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	
	public void BUTTON_OK()
	{
		if (this.selectedKampagne == -1)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (this.mS_.NotEnoughMoney(this.preise[this.selectedKampagne]))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.preise[this.selectedKampagne], 16);
		taskFankampagne taskFankampagne = this.guiMain_.AddTask_Fankampagne();
		taskFankampagne.Init(false);
		taskFankampagne.kampagne = this.selectedKampagne;
		taskFankampagne.automatic = this.uiObjects[14].GetComponent<Toggle>().isOn;
		taskFankampagne.points = (float)this.workPoints[this.selectedKampagne];
		taskFankampagne.pointsLeft = (float)this.workPoints[this.selectedKampagne];
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskFankampagne.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void TOGGLE_Auto()
	{
	}

	
	public GameObject[] uiObjects;

	
	public int[] preise;

	
	public int[] fans;

	
	public int[] workPoints;

	
	public Sprite[] sprites;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private roomScript rS_;

	
	private int selectedKampagne = -1;
}
