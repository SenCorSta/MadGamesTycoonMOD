using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Fanshop : MonoBehaviour
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

	
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.selectedGame = gS_;
		this.uiObjects[70].GetComponent<Toggle>().isOn = this.selectedGame.merchKeinVerkauf;
		if (this.selectedGame.merchVerkaufspreis[0] <= 0f)
		{
			for (int i = 0; i < this.optimalerPreis.Length; i++)
			{
				this.selectedGame.merchVerkaufspreis[i] = this.optimalerPreis[i];
			}
		}
		this.SetData();
		this.SetUnlocks();
	}

	
	private void SetUnlocks()
	{
		for (int i = 0; i < this.needStars.Length; i++)
		{
			if (Mathf.RoundToInt(this.selectedGame.GetIpBekanntheit()) >= this.needStars[i])
			{
				if (this.uiObjects[54 + i].activeSelf)
				{
					this.uiObjects[54 + i].SetActive(false);
				}
			}
			else
			{
				string text = this.tS_.GetText(1847);
				switch (this.needStars[i])
				{
				case 0:
					text += "<br><size=22>☆☆☆☆☆</size>";
					break;
				case 1:
					text += "<br><size=22>★☆☆☆☆</size>";
					break;
				case 2:
					text += "<br><size=22>★★☆☆☆</size>";
					break;
				case 3:
					text += "<br><size=22>★★★☆☆</size>";
					break;
				case 4:
					text += "<br><size=22>★★★★☆</size>";
					break;
				case 5:
					text += "<br><size=22>★★★★★</size>";
					break;
				}
				this.uiObjects[54 + i].GetComponent<tooltip>().c = text;
				if (!this.uiObjects[54 + i].activeSelf)
				{
					this.uiObjects[54 + i].SetActive(true);
				}
			}
		}
	}

	
	private int UpdateBestellpreis()
	{
		int num = 0;
		for (int i = 0; i < this.einkaufspreis.Length; i++)
		{
			int bestellpreis = this.GetBestellpreis(i);
			num += bestellpreis;
			if (bestellpreis > 0)
			{
				this.uiObjects[10 + i].GetComponent<Text>().text = this.mS_.GetMoney((long)bestellpreis, true);
			}
			else
			{
				this.uiObjects[10 + i].GetComponent<Text>().text = "";
			}
		}
		this.uiObjects[43].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		return num;
	}

	
	private int GetBestellpreis(int i)
	{
		return Mathf.RoundToInt((float)this.bestellmenge[i] * this.einkaufspreis[i]);
	}

	
	private void SetData()
	{
		if (this.selectedGame)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.selectedGame.GetIpName();
			this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.selectedGame);
			this.UpdateBestellpreis();
			for (int i = 0; i < this.bestellmenge.Length; i++)
			{
				int num = this.selectedGame.merchBestellungen[i];
				num /= 50;
				this.guiMain_.DrawStars10_Color(this.uiObjects[71 + i], num, Color.white);
			}
			this.uiObjects[44].GetComponent<Text>().text = this.mS_.GetMoney(this.selectedGame.merchGesamtGewinn, true);
			if (this.selectedGame.merchGesamtGewinn < 0L)
			{
				this.uiObjects[44].GetComponent<Text>().color = this.guiMain_.colors[18];
			}
			else
			{
				this.uiObjects[44].GetComponent<Text>().color = this.guiMain_.colors[13];
			}
			for (int j = 0; j < this.einkaufspreis.Length; j++)
			{
				this.uiObjects[2 + j].GetComponent<Text>().text = this.GetMoneyString(this.einkaufspreis[j]);
				if (this.selectedGame.merchVerkaufspreis[j] < this.GetMindestVerkaufspreis(j))
				{
					this.selectedGame.merchVerkaufspreis[j] = this.GetMindestVerkaufspreis(j);
				}
				this.uiObjects[18 + j].GetComponent<Text>().text = this.GetMoneyString(this.selectedGame.merchVerkaufspreis[j]);
				this.uiObjects[34 + j].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.merchDiesenMonat[j], false) + " / " + this.mS_.GetMoney((long)this.selectedGame.merchLetzterMonat[j], false);
				this.uiObjects[62 + j].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.merchGesamtSells[j], false);
			}
		}
	}

	
	public void INPUTFIELD_Bestellmenge(int i)
	{
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Ok()
	{
		if (this.selectedGame)
		{
			this.selectedGame.merchKeinVerkauf = this.uiObjects[70].GetComponent<Toggle>().isOn;
		}
		this.BUTTON_Abbrechen();
	}

	
	public void BUTTON_Bestellen()
	{
	}

	
	private string GetMoneyString(float f)
	{
		string text = "$" + this.mS_.Round(f, 2);
		int length = text.Length;
		if (length != 2)
		{
			if (length == 4)
			{
				text += "0";
			}
		}
		else
		{
			text += ",00";
		}
		if (text[text.Length - 2] == ',')
		{
			text += "0";
		}
		return text;
	}

	
	public void BUTTON_MinusBestellmenge(int i)
	{
	}

	
	public void BUTTON_PlusBestellmenge(int i)
	{
	}

	
	private void SetInputFieldData()
	{
	}

	
	private IEnumerator iMinusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusVerkaufspreis(i);
		}
		yield break;
	}

	
	public void BUTTON_MinusVerkaufspreis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedGame.merchVerkaufspreis[i] -= 1f;
		if (this.selectedGame.merchVerkaufspreis[i] <= this.GetMindestVerkaufspreis(i))
		{
			this.selectedGame.merchVerkaufspreis[i] = this.GetMindestVerkaufspreis(i);
		}
		if (this.selectedGame.merchVerkaufspreis[i] > 29.99f)
		{
			this.selectedGame.merchVerkaufspreis[i] = 29.99f;
		}
		base.StartCoroutine(this.iMinusVerkaufspreis(i));
		this.SetData();
	}

	
	private IEnumerator iPlusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusVerkaufspreis(i);
		}
		yield break;
	}

	
	public void BUTTON_PlusVerkaufspreis(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedGame.merchVerkaufspreis[i] += 1f;
		if (this.selectedGame.merchVerkaufspreis[i] <= this.GetMindestVerkaufspreis(i))
		{
			this.selectedGame.merchVerkaufspreis[i] = this.GetMindestVerkaufspreis(i);
		}
		if (this.selectedGame.merchVerkaufspreis[i] > 29.99f)
		{
			this.selectedGame.merchVerkaufspreis[i] = 29.99f;
		}
		base.StartCoroutine(this.iPlusVerkaufspreis(i));
		this.SetData();
	}

	
	public float GetMindestVerkaufspreis(int i)
	{
		return (float)Mathf.RoundToInt(this.einkaufspreis[i]) + 0.99f;
	}

	
	public void BUTTON_GlobalVerkaufspreis()
	{
		this.sfx_.PlaySound(3, true);
		int num = 1;
		for (int i = 0; i < this.mS_.games_.arrayGamesScripts.Length; i++)
		{
			if (this.mS_.games_.arrayGamesScripts[i] && this.selectedGame.myID != this.mS_.games_.arrayGamesScripts[i].myID && this.mS_.games_.arrayGamesScripts[i].playerGame && this.mS_.games_.arrayGamesScripts[i].mainIP == this.mS_.games_.arrayGamesScripts[i].myID)
			{
				num++;
				for (int j = 0; j < this.mS_.games_.arrayGamesScripts[i].merchVerkaufspreis.Length; j++)
				{
					this.mS_.games_.arrayGamesScripts[i].merchVerkaufspreis[j] = this.selectedGame.merchVerkaufspreis[j];
				}
			}
		}
		string text = this.tS_.GetText(1840);
		text = text.Replace("<NUM>", "<color=blue>" + num.ToString() + "</color>");
		this.guiMain_.MessageBox(text, false);
	}

	
	public void TOGGLE_Automatic()
	{
	}

	
	public void TOGGLE_VerkaufEinstellen()
	{
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private gameScript selectedGame;

	
	public int[] bestellmenge;

	
	public float[] einkaufspreis;

	
	public float[] beliebtheit;

	
	public float[] optimalerPreis;

	
	public int[] needStars;

	
	private float updateTimer;
}
