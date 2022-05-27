using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FA RID: 250
public class Item_Stats_Platform : MonoBehaviour
{
	// Token: 0x06000826 RID: 2086 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00058DF8 File Offset: 0x00056FF8
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

	// Token: 0x06000828 RID: 2088 RVA: 0x00058E74 File Offset: 0x00057074
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

	// Token: 0x06000829 RID: 2089 RVA: 0x00058EC0 File Offset: 0x000570C0
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

	// Token: 0x0600082A RID: 2090 RVA: 0x00058F24 File Offset: 0x00057124
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

	// Token: 0x0600082B RID: 2091 RVA: 0x0003D679 File Offset: 0x0003B879
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
