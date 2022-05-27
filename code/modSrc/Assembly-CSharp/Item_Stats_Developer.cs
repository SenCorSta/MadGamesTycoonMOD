using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F6 RID: 246
public class Item_Stats_Developer : MonoBehaviour
{
	// Token: 0x0600080A RID: 2058 RVA: 0x0000630E File Offset: 0x0000450E
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00006316 File Offset: 0x00004516
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0006A620 File Offset: 0x00068820
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

	// Token: 0x0600080D RID: 2061 RVA: 0x0006A66C File Offset: 0x0006886C
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
			base.gameObject.GetComponent<Image>().color = Color.white;
			if (this.pS_.IsMyTochterfirma())
			{
				base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			if (this.pS_.tf_geschlossen)
			{
				base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[25];
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

	// Token: 0x0600080E RID: 2062 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0006A968 File Offset: 0x00068B68
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
