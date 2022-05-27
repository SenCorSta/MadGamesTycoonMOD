using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F6 RID: 246
public class Item_Stats_Developer : MonoBehaviour
{
	// Token: 0x06000813 RID: 2067 RVA: 0x0005859F File Offset: 0x0005679F
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x000585A7 File Offset: 0x000567A7
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000585B0 File Offset: 0x000567B0
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

	// Token: 0x06000816 RID: 2070 RVA: 0x000585FC File Offset: 0x000567FC
	private void SetData()
	{
		if (this.pS_)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
			this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetAmountGames().ToString();
			this.uiObjects[4].GetComponent<Text>().text = this.pS_.GetFirmenwertString();
			this.uiObjects[7].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
			this.tooltip_.c = this.pS_.GetTooltip();
			if (this.pS_.IsMyTochterfirma())
			{
				if (!this.uiObjects[6].activeSelf)
				{
					this.uiObjects[6].SetActive(true);
				}
			}
			else if (this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(false);
			}
			if (this.pS_.isPlayer)
			{
				if (!this.uiObjects[9].activeSelf)
				{
					this.uiObjects[9].SetActive(true);
				}
			}
			else if (this.uiObjects[9].activeSelf)
			{
				this.uiObjects[9].SetActive(false);
			}
			if (!this.pS_.isPlayer && !this.pS_.IsTochterfirma())
			{
				if (!this.uiObjects[10].activeSelf)
				{
					this.uiObjects[10].SetActive(true);
				}
			}
			else if (this.uiObjects[10].activeSelf)
			{
				this.uiObjects[10].SetActive(false);
			}
			if (this.pS_.tf_geschlossen)
			{
				if (!this.uiObjects[8].activeSelf)
				{
					this.uiObjects[8].SetActive(true);
					return;
				}
			}
			else if (this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(false);
			}
			return;
		}
		if (this.mS_.multiplayer && this.playerID != -1)
		{
			base.gameObject.transform.SetAsFirstSibling();
			base.gameObject.GetComponent<Button>().interactable = false;
			this.uiObjects[0].GetComponent<Text>().text = this.mS_.mpCalls_.GetCompanyName(this.playerID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mS_.mpCalls_.GetLogo(this.playerID));
			this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(5f));
			this.uiObjects[2].GetComponent<Text>().text = "---";
			this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.mpCalls_.GetMoney(this.playerID), true);
			this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(432) + " & " + this.tS_.GetText(274);
			this.tooltip_.c = "";
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00058978 File Offset: 0x00056B78
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[359]);
		this.guiMain_.uiObjects[359].GetComponent<Menu_Stats_Developer_Main>().Init(this.pS_);
	}

	// Token: 0x04000C3E RID: 3134
	public GameObject[] uiObjects;

	// Token: 0x04000C3F RID: 3135
	public mainScript mS_;

	// Token: 0x04000C40 RID: 3136
	public textScript tS_;

	// Token: 0x04000C41 RID: 3137
	public sfxScript sfx_;

	// Token: 0x04000C42 RID: 3138
	public genres genres_;

	// Token: 0x04000C43 RID: 3139
	public GUI_Main guiMain_;

	// Token: 0x04000C44 RID: 3140
	public tooltip tooltip_;

	// Token: 0x04000C45 RID: 3141
	public publisherScript pS_;

	// Token: 0x04000C46 RID: 3142
	public int playerID = -1;

	// Token: 0x04000C47 RID: 3143
	private float updateTimer;
}
