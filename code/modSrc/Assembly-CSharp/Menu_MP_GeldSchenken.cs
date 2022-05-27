using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MP_GeldSchenken : MonoBehaviour
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
			if (playerID == this.mS_.myID)
			{
				if (this.uiPlayerButtons[j].activeSelf)
				{
					this.uiPlayerButtons[j].SetActive(false);
				}
			}
			else
			{
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

	
	public void BUTTON_Ok()
	{
		if (this.selectedPlayer == -1)
		{
			return;
		}
		if (this.value < 0)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.mS_.money < (long)this.value)
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.mS_.Pay((long)this.value, 9);
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Help(this.mS_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 0, this.value, 0, 0);
		}
		else
		{
			this.mpCalls_.CLIENT_Send_Help(this.mpCalls_.playersMP[this.selectedPlayer].playerID, 0, this.value, 0, 0);
		}
		string text = this.tS_.GetText(1328);
		text = text.Replace("<NAME>", this.mpCalls_.GetCompanyName(this.mpCalls_.playersMP[this.selectedPlayer].playerID));
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.value, true));
		this.guiMain_.MessageBox(text, false);
		base.gameObject.SetActive(false);
	}

	
	public void SLIDER_Money()
	{
		this.value = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value * 10000f);
		this.SetInputFieldData();
	}

	
	public void INPUTFIELD_Money()
	{
		if (this.uiObjects[4].GetComponent<InputField>().text.Length >= 1)
		{
			this.value = int.Parse(this.uiObjects[4].GetComponent<InputField>().text);
			if (this.value < 0)
			{
				this.value = 0;
				this.SetInputFieldData();
				return;
			}
		}
		else
		{
			this.value = 0;
		}
	}

	
	private void SetInputFieldData()
	{
		this.uiObjects[4].GetComponent<InputField>().text = this.value.ToString();
	}

	
	private IEnumerator iMinus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Minus();
		}
		yield break;
	}

	
	public void BUTTON_Minus()
	{
		this.sfx_.PlaySound(3, true);
		this.value -= 10000;
		if (this.value < 0)
		{
			this.value = 0;
		}
		base.StartCoroutine(this.iMinus());
		this.SetInputFieldData();
		this.uiObjects[5].GetComponent<Slider>().value = (float)(this.value / 10000);
	}

	
	private IEnumerator iPlus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Plus();
		}
		yield break;
	}

	
	public void BUTTON_Plus()
	{
		this.sfx_.PlaySound(3, true);
		this.value += 10000;
		if (this.value > 99999999)
		{
			this.value = 99999999;
		}
		base.StartCoroutine(this.iPlus());
		this.SetInputFieldData();
		this.uiObjects[5].GetComponent<Slider>().value = (float)(this.value / 10000);
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

	
	public int value;
}
