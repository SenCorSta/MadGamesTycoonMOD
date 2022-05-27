using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009D RID: 157
public class Item_Genre_Forschung : MonoBehaviour
{
	// Token: 0x060005FA RID: 1530 RVA: 0x0004CD4D File Offset: 0x0004AF4D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0004CD58 File Offset: 0x0004AF58
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		if ((float)this.genres_.genres_RES_POINTS[this.myID] == this.genres_.genres_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.genres_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.genres_.genres_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.genres_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.genres_.GetTooltip(this.myID);
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0004CF28 File Offset: 0x0004B128
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.genres_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 0;
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

	// Token: 0x0400094C RID: 2380
	public int myID;

	// Token: 0x0400094D RID: 2381
	public GameObject[] uiObjects;

	// Token: 0x0400094E RID: 2382
	public Color[] colors;

	// Token: 0x0400094F RID: 2383
	public mainScript mS_;

	// Token: 0x04000950 RID: 2384
	public textScript tS_;

	// Token: 0x04000951 RID: 2385
	public sfxScript sfx_;

	// Token: 0x04000952 RID: 2386
	public genres genres_;

	// Token: 0x04000953 RID: 2387
	public GUI_Main guiMain_;

	// Token: 0x04000954 RID: 2388
	public tooltip tooltip_;

	// Token: 0x04000955 RID: 2389
	public roomScript rS_;

	// Token: 0x04000956 RID: 2390
	private float updateTimer;
}
