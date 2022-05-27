using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026B RID: 619
public class Menu_Fanshop : MonoBehaviour
{
	// Token: 0x06001809 RID: 6153 RVA: 0x000F00E6 File Offset: 0x000EE2E6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000F00F0 File Offset: 0x000EE2F0
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

	// Token: 0x0600180B RID: 6155 RVA: 0x000F01DA File Offset: 0x000EE3DA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000F01E4 File Offset: 0x000EE3E4
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

	// Token: 0x0600180D RID: 6157 RVA: 0x000F0230 File Offset: 0x000EE430
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

	// Token: 0x0600180E RID: 6158 RVA: 0x000F02C0 File Offset: 0x000EE4C0
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

	// Token: 0x0600180F RID: 6159 RVA: 0x000F03F0 File Offset: 0x000EE5F0
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

	// Token: 0x06001810 RID: 6160 RVA: 0x000F0483 File Offset: 0x000EE683
	private int GetBestellpreis(int i)
	{
		return Mathf.RoundToInt((float)this.bestellmenge[i] * this.einkaufspreis[i]);
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000F049C File Offset: 0x000EE69C
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

	// Token: 0x06001812 RID: 6162 RVA: 0x00002715 File Offset: 0x00000915
	public void INPUTFIELD_Bestellmenge(int i)
	{
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000F06CC File Offset: 0x000EE8CC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x000F06E7 File Offset: 0x000EE8E7
	public void BUTTON_Ok()
	{
		if (this.selectedGame)
		{
			this.selectedGame.merchKeinVerkauf = this.uiObjects[70].GetComponent<Toggle>().isOn;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x00002715 File Offset: 0x00000915
	public void BUTTON_Bestellen()
	{
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000F071C File Offset: 0x000EE91C
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

	// Token: 0x06001817 RID: 6167 RVA: 0x00002715 File Offset: 0x00000915
	public void BUTTON_MinusBestellmenge(int i)
	{
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x00002715 File Offset: 0x00000915
	public void BUTTON_PlusBestellmenge(int i)
	{
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x00002715 File Offset: 0x00000915
	private void SetInputFieldData()
	{
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000F0790 File Offset: 0x000EE990
	private IEnumerator iMinusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000F07A8 File Offset: 0x000EE9A8
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

	// Token: 0x0600181C RID: 6172 RVA: 0x000F0840 File Offset: 0x000EEA40
	private IEnumerator iPlusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000F0858 File Offset: 0x000EEA58
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

	// Token: 0x0600181E RID: 6174 RVA: 0x000F08F0 File Offset: 0x000EEAF0
	public float GetMindestVerkaufspreis(int i)
	{
		return (float)Mathf.RoundToInt(this.einkaufspreis[i]) + 0.99f;
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000F0908 File Offset: 0x000EEB08
	public void BUTTON_GlobalVerkaufspreis()
	{
		this.sfx_.PlaySound(3, true);
		int num = 1;
		for (int i = 0; i < this.mS_.games_.arrayGamesScripts.Length; i++)
		{
			if (this.mS_.games_.arrayGamesScripts[i] && this.selectedGame.myID != this.mS_.games_.arrayGamesScripts[i].myID && this.mS_.games_.arrayGamesScripts[i].ownerID == this.mS_.myID && this.mS_.games_.arrayGamesScripts[i].mainIP == this.mS_.games_.arrayGamesScripts[i].myID)
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

	// Token: 0x06001820 RID: 6176 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Automatic()
	{
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_VerkaufEinstellen()
	{
	}

	// Token: 0x04001BC6 RID: 7110
	public GameObject[] uiObjects;

	// Token: 0x04001BC7 RID: 7111
	private GameObject main_;

	// Token: 0x04001BC8 RID: 7112
	private mainScript mS_;

	// Token: 0x04001BC9 RID: 7113
	private textScript tS_;

	// Token: 0x04001BCA RID: 7114
	private unlockScript unlock_;

	// Token: 0x04001BCB RID: 7115
	private GUI_Main guiMain_;

	// Token: 0x04001BCC RID: 7116
	private sfxScript sfx_;

	// Token: 0x04001BCD RID: 7117
	private cameraMovementScript cmS_;

	// Token: 0x04001BCE RID: 7118
	private gameScript selectedGame;

	// Token: 0x04001BCF RID: 7119
	public int[] bestellmenge;

	// Token: 0x04001BD0 RID: 7120
	public float[] einkaufspreis;

	// Token: 0x04001BD1 RID: 7121
	public float[] beliebtheit;

	// Token: 0x04001BD2 RID: 7122
	public float[] optimalerPreis;

	// Token: 0x04001BD3 RID: 7123
	public int[] needStars;

	// Token: 0x04001BD4 RID: 7124
	private float updateTimer;
}
