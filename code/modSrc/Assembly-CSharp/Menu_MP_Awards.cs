using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C2 RID: 450
public class Menu_MP_Awards : MonoBehaviour
{
	// Token: 0x060010E0 RID: 4320 RVA: 0x0000BE5F File Offset: 0x0000A05F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000BFF60 File Offset: 0x000BE160
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

	// Token: 0x060010E2 RID: 4322 RVA: 0x0000BE67 File Offset: 0x0000A067
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0000BE6F File Offset: 0x0000A06F
	public void Init()
	{
		this.selectedPlayer = -1;
		this.FindScripts();
		this.InitPlayerButtons();
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0000BE84 File Offset: 0x0000A084
	private void Update()
	{
		this.UpdatePlayerButtons();
		this.SetData(this.selectedPlayer);
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000C004C File Offset: 0x000BE24C
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

	// Token: 0x060010E6 RID: 4326 RVA: 0x000C00BC File Offset: 0x000BE2BC
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

	// Token: 0x060010E7 RID: 4327 RVA: 0x0000BE98 File Offset: 0x0000A098
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0000BEAF File Offset: 0x0000A0AF
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x000C01B4 File Offset: 0x000BE3B4
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

	// Token: 0x0400157A RID: 5498
	public GameObject[] uiPlayerButtons;

	// Token: 0x0400157B RID: 5499
	public GameObject[] uiObjects;

	// Token: 0x0400157C RID: 5500
	private roomScript rS_;

	// Token: 0x0400157D RID: 5501
	private GameObject main_;

	// Token: 0x0400157E RID: 5502
	private mainScript mS_;

	// Token: 0x0400157F RID: 5503
	private textScript tS_;

	// Token: 0x04001580 RID: 5504
	private GUI_Main guiMain_;

	// Token: 0x04001581 RID: 5505
	private sfxScript sfx_;

	// Token: 0x04001582 RID: 5506
	private unlockScript unlock_;

	// Token: 0x04001583 RID: 5507
	private mpCalls mpCalls_;

	// Token: 0x04001584 RID: 5508
	public int selectedPlayer = -1;
}
