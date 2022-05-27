using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E3 RID: 227
public class Item_BestGames : MonoBehaviour
{
	// Token: 0x060007A4 RID: 1956 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00055F80 File Offset: 0x00054180
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
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

	// Token: 0x060007A6 RID: 1958 RVA: 0x000560FC File Offset: 0x000542FC
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

	// Token: 0x060007A7 RID: 1959 RVA: 0x00056250 File Offset: 0x00054450
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

	// Token: 0x060007A8 RID: 1960 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000562B4 File Offset: 0x000544B4
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
