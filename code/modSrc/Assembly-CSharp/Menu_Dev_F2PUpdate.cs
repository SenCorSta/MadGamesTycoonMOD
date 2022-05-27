using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_F2PUpdate : MonoBehaviour
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(roomScript roomScript_, gameScript gameScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.gS_ = gameScript_;
		this.allAdds = false;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = false;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = this.gS_.f2pInteresse * 0.01f;
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.f2pInteresse).ToString() + "%";
		for (int j = 0; j < this.goButtons.Length; j++)
		{
			if (this.goButtons[j])
			{
				this.goButtons[j].transform.GetChild(3).GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.GetGesamteEntwicklungskosten() * this.devCostsPercent[j]), true);
			}
		}
		this.UpdateGUI();
	}

	
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				this.goButtons[i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.goButtons[i].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
	}

	
	private long GetDevCosts()
	{
		long num = 0L;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)Mathf.RoundToInt((float)this.gS_.GetGesamteEntwicklungskosten() * this.devCostsPercent[i]);
			}
		}
		return num;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[299]);
		this.guiMain_.uiObjects[299].GetComponent<Menu_Dev_F2PUpdateSelectGame>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	
	public void BUTTON_AlleAdds()
	{
		this.sfx_.PlaySound(3, true);
		this.allAdds = !this.allAdds;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = this.allAdds;
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_Start()
	{
		int num = Mathf.RoundToInt((float)this.GetDevCosts());
		if (!this.gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (num <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(662), false);
			return;
		}
		if (this.uiObjects[4].GetComponent<Toggle>().isOn)
		{
			bool flag = false;
			for (int i = 0; i < this.buttonAdds.Length; i++)
			{
				if (this.buttonAdds[i])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.guiMain_.MessageBox(this.tS_.GetText(727), false);
				return;
			}
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)num, 15);
		taskF2PUpdate taskF2PUpdate = this.guiMain_.AddTask_F2PUpdate();
		taskF2PUpdate.Init(false);
		taskF2PUpdate.targetID = this.gS_.myID;
		taskF2PUpdate.devCosts = Mathf.RoundToInt((float)this.GetDevCosts());
		taskF2PUpdate.automatic = this.uiObjects[4].GetComponent<Toggle>().isOn;
		float num2 = (float)this.gS_.GetGesamtDevPoints();
		num2 *= 0.1f;
		taskF2PUpdate.points = (float)Mathf.RoundToInt(num2);
		for (int j = 0; j < this.buttonAdds.Length; j++)
		{
			if (this.buttonAdds[j])
			{
				taskF2PUpdate.quality += this.interestBoost[j];
				taskF2PUpdate.points += (float)Mathf.RoundToInt((float)this.gS_.GetGesamtDevPoints() * this.interestBoost[j] * 0.02f);
			}
		}
		taskF2PUpdate.pointsLeft = taskF2PUpdate.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskF2PUpdate.myID;
		}
		this.guiMain_.uiObjects[299].SetActive(false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	public float[] devCostsPercent;

	
	private bool[] buttonAdds = new bool[12];

	
	public float[] interestBoost;

	
	public GameObject[] goButtons;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private gameScript gS_;

	
	private roomScript rS_;

	
	private bool allAdds;
}
