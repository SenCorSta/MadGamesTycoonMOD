using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C7 RID: 455
public class Menu_MP_GeldSchenken : MonoBehaviour
{
	// Token: 0x06001131 RID: 4401 RVA: 0x000B7002 File Offset: 0x000B5202
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000B700C File Offset: 0x000B520C
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

	// Token: 0x06001133 RID: 4403 RVA: 0x000B70F6 File Offset: 0x000B52F6
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000B70FE File Offset: 0x000B52FE
	public void Init()
	{
		this.selectedPlayer = -1;
		this.FindScripts();
		this.InitPlayerButtons();
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000B7113 File Offset: 0x000B5313
	private void Update()
	{
		this.UpdatePlayerButtons();
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000B711C File Offset: 0x000B531C
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

	// Token: 0x06001137 RID: 4407 RVA: 0x000B718C File Offset: 0x000B538C
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

	// Token: 0x06001138 RID: 4408 RVA: 0x000B72B7 File Offset: 0x000B54B7
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000B72CE File Offset: 0x000B54CE
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000B72EC File Offset: 0x000B54EC
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

	// Token: 0x0600113B RID: 4411 RVA: 0x000B7441 File Offset: 0x000B5641
	public void SLIDER_Money()
	{
		this.value = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value * 10000f);
		this.SetInputFieldData();
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000B746C File Offset: 0x000B566C
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

	// Token: 0x0600113D RID: 4413 RVA: 0x000B74CE File Offset: 0x000B56CE
	private void SetInputFieldData()
	{
		this.uiObjects[4].GetComponent<InputField>().text = this.value.ToString();
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000B74ED File Offset: 0x000B56ED
	private IEnumerator iMinus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Minus();
		}
		yield break;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x000B74FC File Offset: 0x000B56FC
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

	// Token: 0x06001140 RID: 4416 RVA: 0x000B756A File Offset: 0x000B576A
	private IEnumerator iPlus()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_Plus();
		}
		yield break;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000B757C File Offset: 0x000B577C
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

	// Token: 0x040015BA RID: 5562
	public GameObject[] uiPlayerButtons;

	// Token: 0x040015BB RID: 5563
	public GameObject[] uiObjects;

	// Token: 0x040015BC RID: 5564
	private roomScript rS_;

	// Token: 0x040015BD RID: 5565
	private GameObject main_;

	// Token: 0x040015BE RID: 5566
	private mainScript mS_;

	// Token: 0x040015BF RID: 5567
	private textScript tS_;

	// Token: 0x040015C0 RID: 5568
	private GUI_Main guiMain_;

	// Token: 0x040015C1 RID: 5569
	private sfxScript sfx_;

	// Token: 0x040015C2 RID: 5570
	private unlockScript unlock_;

	// Token: 0x040015C3 RID: 5571
	private mpCalls mpCalls_;

	// Token: 0x040015C4 RID: 5572
	public int selectedPlayer = -1;

	// Token: 0x040015C5 RID: 5573
	public int value;
}
