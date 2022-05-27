using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C6 RID: 454
public class Menu_MP_GeldSchenken : MonoBehaviour
{
	// Token: 0x06001117 RID: 4375 RVA: 0x0000C003 File Offset: 0x0000A203
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x000C2890 File Offset: 0x000C0A90
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

	// Token: 0x06001119 RID: 4377 RVA: 0x0000C00B File Offset: 0x0000A20B
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0000C013 File Offset: 0x0000A213
	public void Init()
	{
		this.selectedPlayer = -1;
		this.FindScripts();
		this.InitPlayerButtons();
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0000C028 File Offset: 0x0000A228
	private void Update()
	{
		this.UpdatePlayerButtons();
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x000C297C File Offset: 0x000C0B7C
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

	// Token: 0x0600111D RID: 4381 RVA: 0x000C29EC File Offset: 0x000C0BEC
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
			if (playerID == this.mpCalls_.myID)
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

	// Token: 0x0600111E RID: 4382 RVA: 0x0000C030 File Offset: 0x0000A230
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0000C047 File Offset: 0x0000A247
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x000C2B18 File Offset: 0x000C0D18
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
			this.mpCalls_.SERVER_Send_Help(this.mpCalls_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 0, this.value, 0, 0);
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

	// Token: 0x06001121 RID: 4385 RVA: 0x0000C062 File Offset: 0x0000A262
	public void SLIDER_Money()
	{
		this.value = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value * 10000f);
		this.SetInputFieldData();
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000C2C70 File Offset: 0x000C0E70
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

	// Token: 0x06001123 RID: 4387 RVA: 0x0000C08D File Offset: 0x0000A28D
	private void SetInputFieldData()
	{
		this.uiObjects[4].GetComponent<InputField>().text = this.value.ToString();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0000C0AC File Offset: 0x0000A2AC
	private IEnumerator iMinus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Minus();
		}
		yield break;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000C2CD4 File Offset: 0x000C0ED4
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

	// Token: 0x06001126 RID: 4390 RVA: 0x0000C0BB File Offset: 0x0000A2BB
	private IEnumerator iPlus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Plus();
		}
		yield break;
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x000C2D44 File Offset: 0x000C0F44
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

	// Token: 0x040015B1 RID: 5553
	public GameObject[] uiPlayerButtons;

	// Token: 0x040015B2 RID: 5554
	public GameObject[] uiObjects;

	// Token: 0x040015B3 RID: 5555
	private roomScript rS_;

	// Token: 0x040015B4 RID: 5556
	private GameObject main_;

	// Token: 0x040015B5 RID: 5557
	private mainScript mS_;

	// Token: 0x040015B6 RID: 5558
	private textScript tS_;

	// Token: 0x040015B7 RID: 5559
	private GUI_Main guiMain_;

	// Token: 0x040015B8 RID: 5560
	private sfxScript sfx_;

	// Token: 0x040015B9 RID: 5561
	private unlockScript unlock_;

	// Token: 0x040015BA RID: 5562
	private mpCalls mpCalls_;

	// Token: 0x040015BB RID: 5563
	public int selectedPlayer = -1;

	// Token: 0x040015BC RID: 5564
	public int value;
}
