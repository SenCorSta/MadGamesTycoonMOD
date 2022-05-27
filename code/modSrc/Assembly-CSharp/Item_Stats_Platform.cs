using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FA RID: 250
public class Item_Stats_Platform : MonoBehaviour
{
	// Token: 0x0600081D RID: 2077 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0006AD88 File Offset: 0x00068F88
	private void Update()
	{
		this.frames++;
		if (this.frames < 3)
		{
			return;
		}
		if (!this.myRect_)
		{
			this.myRect_ = base.GetComponent<RectTransform>();
		}
		if (this.myRect_.position.y >= 0f && this.myRect_.position.y <= (float)Screen.height)
		{
			this.EnableObjects();
			this.MultiplayerUpdate();
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0006AE04 File Offset: 0x00069004
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

	// Token: 0x06000820 RID: 2080 RVA: 0x0006AE50 File Offset: 0x00069050
	public void EnableObjects()
	{
		if (this.hasEnabled)
		{
			return;
		}
		this.hasEnabled = true;
		for (int i = 0; i < this.uiObjects.Length; i++)
		{
			if (this.uiObjects[i] && !this.uiObjects[i].activeSelf)
			{
				this.uiObjects[i].SetActive(true);
				this.SetData();
			}
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0006AEB4 File Offset: 0x000690B4
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(220) + ": " + this.pS_.GetGames().ToString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(219) + ": " + this.pS_.GetMarktanteilString();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetPrice(), true);
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.guiMain_.DrawStars(this.uiObjects[7], this.pS_.erfahrung);
		this.uiObjects[9].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		if (this.pS_.internet)
		{
			this.uiObjects[10].SetActive(true);
		}
		else
		{
			this.uiObjects[10].SetActive(false);
		}
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.inBesitz)
		{
			this.uiObjects[5].GetComponent<Text>().text = "<color=black>" + this.tS_.GetText(682) + "</color>";
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
		}
		this.uiObjects[11].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[11].GetComponent<tooltip>().c = this.pS_.GetTypString();
		if (this.pS_.vomMarktGenommen)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[2];
			return;
		}
		this.uiObjects[8].SetActive(false);
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000C59 RID: 3161
	public int myID;

	// Token: 0x04000C5A RID: 3162
	public GameObject[] uiObjects;

	// Token: 0x04000C5B RID: 3163
	public mainScript mS_;

	// Token: 0x04000C5C RID: 3164
	public textScript tS_;

	// Token: 0x04000C5D RID: 3165
	public sfxScript sfx_;

	// Token: 0x04000C5E RID: 3166
	public GUI_Main guiMain_;

	// Token: 0x04000C5F RID: 3167
	public tooltip tooltip_;

	// Token: 0x04000C60 RID: 3168
	public platformScript pS_;

	// Token: 0x04000C61 RID: 3169
	private RectTransform myRect_;

	// Token: 0x04000C62 RID: 3170
	private int frames;

	// Token: 0x04000C63 RID: 3171
	private bool hasEnabled;

	// Token: 0x04000C64 RID: 3172
	private float updateTimer;
}
