using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009F RID: 159
public class Item_Hardware_Forschung : MonoBehaviour
{
	// Token: 0x06000606 RID: 1542 RVA: 0x0004D319 File Offset: 0x0004B519
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0004D324 File Offset: 0x0004B524
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardware_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.myID);
		this.uiObjects[7].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.myID].ToString();
		if ((float)this.hardware_.hardware_RES_POINTS[this.myID] == this.hardware_.hardware_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardware_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		if (!this.hardware_.IsTechComponent(this.myID))
		{
			this.uiObjects[6].SetActive(false);
			this.uiObjects[12].SetActive(true);
		}
		if (this.hardware_.hardware_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		if (this.hardware_.hardware_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.hardware_.hardware_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.hardware_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.hardware_.GetTooltip(this.myID);
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0004D587 File Offset: 0x0004B787
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

	// Token: 0x06000609 RID: 1545 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0004D5BC File Offset: 0x0004B7BC
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.hardware_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 4;
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

	// Token: 0x04000961 RID: 2401
	public int myID;

	// Token: 0x04000962 RID: 2402
	public GameObject[] uiObjects;

	// Token: 0x04000963 RID: 2403
	public Color[] colors;

	// Token: 0x04000964 RID: 2404
	public mainScript mS_;

	// Token: 0x04000965 RID: 2405
	public textScript tS_;

	// Token: 0x04000966 RID: 2406
	public sfxScript sfx_;

	// Token: 0x04000967 RID: 2407
	public hardware hardware_;

	// Token: 0x04000968 RID: 2408
	public GUI_Main guiMain_;

	// Token: 0x04000969 RID: 2409
	public tooltip tooltip_;

	// Token: 0x0400096A RID: 2410
	public roomScript rS_;

	// Token: 0x0400096B RID: 2411
	private float updateTimer;
}
