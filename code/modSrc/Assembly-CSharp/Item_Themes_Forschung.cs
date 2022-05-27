using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A1 RID: 161
public class Item_Themes_Forschung : MonoBehaviour
{
	// Token: 0x06000612 RID: 1554 RVA: 0x0004D921 File Offset: 0x0004BB21
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0004D92C File Offset: 0x0004BB2C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetThemes(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.themes_.icon;
		if ((float)this.themes_.RES_POINTS == this.themes_.themes_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.themes_.PRICE, true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.themes_.themes_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.themes_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.themes_.GetTooltip(this.myID);
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0004DAB8 File Offset: 0x0004BCB8
	private void Update()
	{
		if (!this.hasEnabled)
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
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0004DB64 File Offset: 0x0004BD64
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

	// Token: 0x06000616 RID: 1558 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0004DBC8 File Offset: 0x0004BDC8
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.themes_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 1;
		taskForschung.slot = this.myID;
		taskForschung.automatic = component.uiObjects[4].GetComponent<Toggle>().isOn;
		GameObject gameObject = GameObject.Find("Room_" + component.roomID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskForschung.myID;
		}
		this.sfx_.PlaySound(3, true);
		component.BUTTON_Close();
	}

	// Token: 0x04000978 RID: 2424
	public int myID;

	// Token: 0x04000979 RID: 2425
	public GameObject[] uiObjects;

	// Token: 0x0400097A RID: 2426
	public Color[] colors;

	// Token: 0x0400097B RID: 2427
	public mainScript mS_;

	// Token: 0x0400097C RID: 2428
	public textScript tS_;

	// Token: 0x0400097D RID: 2429
	public sfxScript sfx_;

	// Token: 0x0400097E RID: 2430
	public themes themes_;

	// Token: 0x0400097F RID: 2431
	public GUI_Main guiMain_;

	// Token: 0x04000980 RID: 2432
	public tooltip tooltip_;

	// Token: 0x04000981 RID: 2433
	public roomScript rS_;

	// Token: 0x04000982 RID: 2434
	private float updateTimer;

	// Token: 0x04000983 RID: 2435
	private RectTransform myRect_;

	// Token: 0x04000984 RID: 2436
	private int frames;

	// Token: 0x04000985 RID: 2437
	private bool hasEnabled;
}
