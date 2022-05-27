using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C3 RID: 451
public class Menu_MP_Awards : MonoBehaviour
{
	// Token: 0x060010FA RID: 4346 RVA: 0x000B44E8 File Offset: 0x000B26E8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000B44F0 File Offset: 0x000B26F0
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

	// Token: 0x060010FC RID: 4348 RVA: 0x000B45DA File Offset: 0x000B27DA
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x000B45E2 File Offset: 0x000B27E2
	public void Init()
	{
		this.selectedPlayer = -1;
		this.FindScripts();
		this.InitPlayerButtons();
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000B45F7 File Offset: 0x000B27F7
	private void Update()
	{
		this.UpdatePlayerButtons();
		this.SetData(this.selectedPlayer);
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x000B460C File Offset: 0x000B280C
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

	// Token: 0x06001100 RID: 4352 RVA: 0x000B467C File Offset: 0x000B287C
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

	// Token: 0x06001101 RID: 4353 RVA: 0x000B4774 File Offset: 0x000B2974
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x000B478B File Offset: 0x000B298B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000B47A8 File Offset: 0x000B29A8
	private void SetData(int p)
	{
		if (p >= this.mpCalls_.playersMP.Count)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("PUB_" + this.mpCalls_.playersMP[p].playerID.ToString());
		if (gameObject)
		{
			publisherScript component = gameObject.GetComponent<publisherScript>();
			this.uiObjects[0].GetComponent<Text>().text = component.awards[4].ToString() + "x";
			this.uiObjects[1].GetComponent<Text>().text = component.awards[2].ToString() + "x";
			this.uiObjects[2].GetComponent<Text>().text = component.awards[3].ToString() + "x";
			this.uiObjects[3].GetComponent<Text>().text = component.awards[0].ToString() + "x";
			this.uiObjects[4].GetComponent<Text>().text = component.awards[1].ToString() + "x";
			this.uiObjects[5].GetComponent<Text>().text = component.awards[8].ToString() + "x";
			this.uiObjects[6].GetComponent<Text>().text = component.awards[7].ToString() + "x";
			this.uiObjects[7].GetComponent<Text>().text = component.awards[6].ToString() + "x";
			this.uiObjects[8].GetComponent<Text>().text = component.awards[5].ToString() + "x";
			this.uiObjects[9].GetComponent<Text>().text = component.awards[9].ToString() + "x";
			this.uiObjects[10].GetComponent<Text>().text = component.awards[10].ToString() + "x";
			this.uiObjects[11].GetComponent<Text>().text = component.awards[11].ToString() + "x";
		}
	}

	// Token: 0x04001583 RID: 5507
	public GameObject[] uiPlayerButtons;

	// Token: 0x04001584 RID: 5508
	public GameObject[] uiObjects;

	// Token: 0x04001585 RID: 5509
	private roomScript rS_;

	// Token: 0x04001586 RID: 5510
	private GameObject main_;

	// Token: 0x04001587 RID: 5511
	private mainScript mS_;

	// Token: 0x04001588 RID: 5512
	private textScript tS_;

	// Token: 0x04001589 RID: 5513
	private GUI_Main guiMain_;

	// Token: 0x0400158A RID: 5514
	private sfxScript sfx_;

	// Token: 0x0400158B RID: 5515
	private unlockScript unlock_;

	// Token: 0x0400158C RID: 5516
	private mpCalls mpCalls_;

	// Token: 0x0400158D RID: 5517
	public int selectedPlayer = -1;
}
