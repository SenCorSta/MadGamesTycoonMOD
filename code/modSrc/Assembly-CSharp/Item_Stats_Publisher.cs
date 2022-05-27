using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FB RID: 251
public class Item_Stats_Publisher : MonoBehaviour
{
	// Token: 0x0600082D RID: 2093 RVA: 0x0005918D File Offset: 0x0005738D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00059195 File Offset: 0x00057395
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x000591A0 File Offset: 0x000573A0
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

	// Token: 0x06000830 RID: 2096 RVA: 0x000591EC File Offset: 0x000573EC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[5].GetComponent<Text>().text = "$" + this.mS_.Round(this.pS_.share, 1).ToString();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.guiMain_.DrawStarsColor(this.uiObjects[4], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), this.guiMain_.colors[5]);
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.IsMyTochterfirma())
		{
			if (!this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(true);
			}
		}
		else if (this.uiObjects[8].activeSelf)
		{
			this.uiObjects[8].SetActive(false);
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
			if (!this.uiObjects[7].activeSelf)
			{
				this.uiObjects[7].SetActive(true);
			}
		}
		else if (this.uiObjects[7].activeSelf)
		{
			this.uiObjects[7].SetActive(false);
		}
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[6].activeSelf)
		{
			this.uiObjects[6].SetActive(false);
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00059444 File Offset: 0x00057644
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[373]);
		this.guiMain_.uiObjects[373].GetComponent<Menu_Stats_Publisher_Main>().Init(this.pS_);
	}

	// Token: 0x04000C65 RID: 3173
	public GameObject[] uiObjects;

	// Token: 0x04000C66 RID: 3174
	public mainScript mS_;

	// Token: 0x04000C67 RID: 3175
	public textScript tS_;

	// Token: 0x04000C68 RID: 3176
	public sfxScript sfx_;

	// Token: 0x04000C69 RID: 3177
	public genres genres_;

	// Token: 0x04000C6A RID: 3178
	public GUI_Main guiMain_;

	// Token: 0x04000C6B RID: 3179
	public tooltip tooltip_;

	// Token: 0x04000C6C RID: 3180
	public publisherScript pS_;

	// Token: 0x04000C6D RID: 3181
	private float updateTimer;
}
