using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MP_Awards : MonoBehaviour
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
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	public void Init()
	{
		this.selectedPlayer = -1;
		this.FindScripts();
		this.InitPlayerButtons();
	}

	
	private void Update()
	{
		this.UpdatePlayerButtons();
		this.SetData(this.selectedPlayer);
	}

	
	public void UpdatePlayerButtons()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.uiPlayerButtons[i].activeSelf)
			{
				if (this.selectedPlayer == i)
				{
					this.uiPlayerButtons[i].GetComponent<Image>().color = this.guiMain_.colors[20];
				}
				else
				{
					this.uiPlayerButtons[i].GetComponent<Image>().color = Color.white;
				}
			}
		}
	}

	
	public void InitPlayerButtons()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.uiPlayerButtons[i].activeSelf)
			{
				this.uiPlayerButtons[i].SetActive(false);
			}
		}
		for (int j = 0; j < this.mpCalls_.playersMP.Count; j++)
		{
			int playerID = this.mpCalls_.playersMP[j].playerID;
			if (!this.uiPlayerButtons[j].activeSelf)
			{
				this.uiPlayerButtons[j].SetActive(true);
			}
			if (this.selectedPlayer == -1)
			{
				this.selectedPlayer = j;
			}
			this.uiPlayerButtons[j].transform.GetChild(1).GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mpCalls_.GetLogo(playerID));
			this.uiPlayerButtons[j].transform.GetChild(2).GetComponent<Text>().text = this.mpCalls_.GetCompanyName(playerID);
		}
	}

	
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	private void SetData(int p)
	{
		if (p >= this.mpCalls_.playersMP.Count)
		{
			return;
		}
		int[] array = new int[this.mpCalls_.playersMP[p].awards.Length];
		array = (int[])this.mpCalls_.playersMP[p].awards.Clone();
		this.uiObjects[0].GetComponent<Text>().text = array[4].ToString() + "x";
		this.uiObjects[1].GetComponent<Text>().text = array[2].ToString() + "x";
		this.uiObjects[2].GetComponent<Text>().text = array[3].ToString() + "x";
		this.uiObjects[3].GetComponent<Text>().text = array[0].ToString() + "x";
		this.uiObjects[4].GetComponent<Text>().text = array[1].ToString() + "x";
		this.uiObjects[5].GetComponent<Text>().text = array[8].ToString() + "x";
		this.uiObjects[6].GetComponent<Text>().text = array[7].ToString() + "x";
		this.uiObjects[7].GetComponent<Text>().text = array[6].ToString() + "x";
		this.uiObjects[8].GetComponent<Text>().text = array[5].ToString() + "x";
		this.uiObjects[9].GetComponent<Text>().text = array[9].ToString() + "x";
		this.uiObjects[10].GetComponent<Text>().text = array[10].ToString() + "x";
		this.uiObjects[11].GetComponent<Text>().text = array[11].ToString() + "x";
	}

	
	public GameObject[] uiPlayerButtons;

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private unlockScript unlock_;

	
	private mpCalls mpCalls_;

	
	public int selectedPlayer = -1;
}
