using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E3 RID: 227
public class Item_BestGames : MonoBehaviour
{
	// Token: 0x0600079B RID: 1947 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000681AC File Offset: 0x000663AC
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1 && this.game_.multiplayerSlot != this.mS_.GetMyMultiplayerID())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00068328 File Offset: 0x00066528
	private void Update()
	{
		this.frames++;
		if (this.frames >= 3)
		{
			if (!this.myRect_)
			{
				this.myRect_ = base.GetComponent<RectTransform>();
			}
			if (this.myRect_.position.y >= 0f && this.myRect_.position.y <= (float)Screen.height)
			{
				this.EnableObjects();
			}
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		base.gameObject.name = this.game_.reviewTotal.ToString();
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0006847C File Offset: 0x0006667C
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

	// Token: 0x0600079F RID: 1951 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000684E0 File Offset: 0x000666E0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000BA1 RID: 2977
	public GameObject[] uiObjects;

	// Token: 0x04000BA2 RID: 2978
	public mainScript mS_;

	// Token: 0x04000BA3 RID: 2979
	public textScript tS_;

	// Token: 0x04000BA4 RID: 2980
	public sfxScript sfx_;

	// Token: 0x04000BA5 RID: 2981
	public GUI_Main guiMain_;

	// Token: 0x04000BA6 RID: 2982
	public tooltip tooltip_;

	// Token: 0x04000BA7 RID: 2983
	public gameScript game_;

	// Token: 0x04000BA8 RID: 2984
	public genres genres_;

	// Token: 0x04000BA9 RID: 2985
	private RectTransform myRect_;

	// Token: 0x04000BAA RID: 2986
	private int frames;

	// Token: 0x04000BAB RID: 2987
	private bool hasEnabled;
}
