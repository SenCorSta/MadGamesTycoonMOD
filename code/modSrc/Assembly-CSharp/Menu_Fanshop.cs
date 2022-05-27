using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000267 RID: 615
public class Menu_Fanshop : MonoBehaviour
{
	// Token: 0x060017C6 RID: 6086 RVA: 0x00010893 File Offset: 0x0000EA93
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000F5728 File Offset: 0x000F3928
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

	// Token: 0x060017C8 RID: 6088 RVA: 0x0001089B File Offset: 0x0000EA9B
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000F5814 File Offset: 0x000F3A14
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

	// Token: 0x060017CA RID: 6090 RVA: 0x000F5860 File Offset: 0x000F3A60
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

	// Token: 0x060017CB RID: 6091 RVA: 0x000F58F0 File Offset: 0x000F3AF0
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

	// Token: 0x060017CC RID: 6092 RVA: 0x000F5A20 File Offset: 0x000F3C20
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

	// Token: 0x060017CD RID: 6093 RVA: 0x000108A3 File Offset: 0x0000EAA3
	private int GetBestellpreis(int i)
	{
		return Mathf.RoundToInt((float)this.bestellmenge[i] * this.einkaufspreis[i]);
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000F5AB4 File Offset: 0x000F3CB4
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

	// Token: 0x060017CF RID: 6095 RVA: 0x00002098 File Offset: 0x00000298
	public void INPUTFIELD_Bestellmenge(int i)
	{
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000108BC File Offset: 0x0000EABC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000108D7 File Offset: 0x0000EAD7
	public void BUTTON_Ok()
	{
		if (this.selectedGame)
		{
			this.selectedGame.merchKeinVerkauf = this.uiObjects[70].GetComponent<Toggle>().isOn;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x00002098 File Offset: 0x00000298
	public void BUTTON_Bestellen()
	{
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x000F5CE4 File Offset: 0x000F3EE4
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

	// Token: 0x060017D4 RID: 6100 RVA: 0x00002098 File Offset: 0x00000298
	public void BUTTON_MinusBestellmenge(int i)
	{
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x00002098 File Offset: 0x00000298
	public void BUTTON_PlusBestellmenge(int i)
	{
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x00002098 File Offset: 0x00000298
	private void SetInputFieldData()
	{
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x0001090A File Offset: 0x0000EB0A
	private IEnumerator iMinusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000F5D58 File Offset: 0x000F3F58
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

	// Token: 0x060017D9 RID: 6105 RVA: 0x00010920 File Offset: 0x0000EB20
	private IEnumerator iPlusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x000F5DF0 File Offset: 0x000F3FF0
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

	// Token: 0x060017DB RID: 6107 RVA: 0x00010936 File Offset: 0x0000EB36
	public float GetMindestVerkaufspreis(int i)
	{
		return (float)Mathf.RoundToInt(this.einkaufspreis[i]) + 0.99f;
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000F5E88 File Offset: 0x000F4088
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

	// Token: 0x060017DD RID: 6109 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_Automatic()
	{
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_VerkaufEinstellen()
	{
	}

	// Token: 0x04001BAC RID: 7084
	public GameObject[] uiObjects;

	// Token: 0x04001BAD RID: 7085
	private GameObject main_;

	// Token: 0x04001BAE RID: 7086
	private mainScript mS_;

	// Token: 0x04001BAF RID: 7087
	private textScript tS_;

	// Token: 0x04001BB0 RID: 7088
	private unlockScript unlock_;

	// Token: 0x04001BB1 RID: 7089
	private GUI_Main guiMain_;

	// Token: 0x04001BB2 RID: 7090
	private sfxScript sfx_;

	// Token: 0x04001BB3 RID: 7091
	private cameraMovementScript cmS_;

	// Token: 0x04001BB4 RID: 7092
	private gameScript selectedGame;

	// Token: 0x04001BB5 RID: 7093
	public int[] bestellmenge;

	// Token: 0x04001BB6 RID: 7094
	public float[] einkaufspreis;

	// Token: 0x04001BB7 RID: 7095
	public float[] beliebtheit;

	// Token: 0x04001BB8 RID: 7096
	public float[] optimalerPreis;

	// Token: 0x04001BB9 RID: 7097
	public int[] needStars;

	// Token: 0x04001BBA RID: 7098
	private float updateTimer;
}
