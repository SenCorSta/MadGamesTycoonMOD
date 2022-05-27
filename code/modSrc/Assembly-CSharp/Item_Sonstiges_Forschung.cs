using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A0 RID: 160
public class Item_Sonstiges_Forschung : MonoBehaviour
{
	// Token: 0x06000603 RID: 1539 RVA: 0x000057E2 File Offset: 0x000039E2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00060344 File Offset: 0x0005E544
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.fS_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.fS_.GetPic(this.myID);
		if (this.fS_.RES_POINTS[this.myID] == this.fS_.RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.fS_.RES_PRICE[this.myID], true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.fS_.RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.fS_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.fS_.GetTooltip(this.myID);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000057EA File Offset: 0x000039EA
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

	// Token: 0x06000606 RID: 1542 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x000604E0 File Offset: 0x0005E6E0
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.fS_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 5;
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

	// Token: 0x0400096C RID: 2412
	public int myID;

	// Token: 0x0400096D RID: 2413
	public GameObject[] uiObjects;

	// Token: 0x0400096E RID: 2414
	public Color[] colors;

	// Token: 0x0400096F RID: 2415
	public mainScript mS_;

	// Token: 0x04000970 RID: 2416
	public textScript tS_;

	// Token: 0x04000971 RID: 2417
	public sfxScript sfx_;

	// Token: 0x04000972 RID: 2418
	public unlockScript unlock_;

	// Token: 0x04000973 RID: 2419
	public forschungSonstiges fS_;

	// Token: 0x04000974 RID: 2420
	public GUI_Main guiMain_;

	// Token: 0x04000975 RID: 2421
	public tooltip tooltip_;

	// Token: 0x04000976 RID: 2422
	public roomScript rS_;

	// Token: 0x04000977 RID: 2423
	private float updateTimer;
}
