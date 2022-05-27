using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FC RID: 252
public class Item_Stats_Tochterfirma : MonoBehaviour
{
	// Token: 0x0600082B RID: 2091 RVA: 0x00006378 File Offset: 0x00004578
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00006380 File Offset: 0x00004580
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0006B3A4 File Offset: 0x000695A4
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

	// Token: 0x0600082E RID: 2094 RVA: 0x0006B3F0 File Offset: 0x000695F0
	private void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetAmountGames().ToString();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.GetFirmenwertString();
		this.uiObjects[7].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.tf_geschlossen)
		{
			base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[25];
		}
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
		else if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		if (!this.pS_.developer)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1949);
			this.uiObjects[8].GetComponent<Image>().fillAmount = 0f;
			return;
		}
		float num = (float)this.pS_.newGameInWeeksORG;
		if (num <= (float)this.pS_.newGameInWeeks)
		{
			num = (float)this.pS_.newGameInWeeks;
		}
		num = 100f / num;
		num = 100f - num * (float)this.pS_.newGameInWeeks;
		this.uiObjects[8].GetComponent<Image>().fillAmount = num * 0.01f;
		if (this.pS_.newGameInWeeks <= 2)
		{
			this.uiObjects[8].GetComponent<Image>().fillAmount = 1f;
		}
		if (this.pS_.newGameInWeeks > 2)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1944) + ": " + Mathf.RoundToInt(num).ToString() + "%";
			return;
		}
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1947);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0006B6A0 File Offset: 0x000698A0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[387]);
		this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().Init(this.pS_);
	}

	// Token: 0x04000C6E RID: 3182
	public GameObject[] uiObjects;

	// Token: 0x04000C6F RID: 3183
	public mainScript mS_;

	// Token: 0x04000C70 RID: 3184
	public textScript tS_;

	// Token: 0x04000C71 RID: 3185
	public sfxScript sfx_;

	// Token: 0x04000C72 RID: 3186
	public genres genres_;

	// Token: 0x04000C73 RID: 3187
	public GUI_Main guiMain_;

	// Token: 0x04000C74 RID: 3188
	public tooltip tooltip_;

	// Token: 0x04000C75 RID: 3189
	public publisherScript pS_;

	// Token: 0x04000C76 RID: 3190
	public int playerID = -1;

	// Token: 0x04000C77 RID: 3191
	private float updateTimer;
}
