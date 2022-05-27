using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009E RID: 158
public class Item_HardFeat_Forschung : MonoBehaviour
{
	// Token: 0x06000600 RID: 1536 RVA: 0x0004CFED File Offset: 0x0004B1ED
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0004CFF8 File Offset: 0x0004B1F8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.hardwareFeatures_.GetSprite(this.myID);
		if ((float)this.hardwareFeatures_.hardFeat_RES_POINTS[this.myID] == this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardwareFeatures_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		if (this.hardwareFeatures_.hardFeat_NEEDINTERNET[this.myID])
		{
			this.uiObjects[6].SetActive(true);
		}
		if (this.hardwareFeatures_.hardFeat_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		if (this.hardwareFeatures_.hardFeat_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.hardwareFeatures_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0004D220 File Offset: 0x0004B420
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

	// Token: 0x06000603 RID: 1539 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0004D254 File Offset: 0x0004B454
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.hardwareFeatures_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 6;
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

	// Token: 0x04000957 RID: 2391
	public int myID;

	// Token: 0x04000958 RID: 2392
	public GameObject[] uiObjects;

	// Token: 0x04000959 RID: 2393
	public mainScript mS_;

	// Token: 0x0400095A RID: 2394
	public textScript tS_;

	// Token: 0x0400095B RID: 2395
	public sfxScript sfx_;

	// Token: 0x0400095C RID: 2396
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x0400095D RID: 2397
	public GUI_Main guiMain_;

	// Token: 0x0400095E RID: 2398
	public tooltip tooltip_;

	// Token: 0x0400095F RID: 2399
	public roomScript rS_;

	// Token: 0x04000960 RID: 2400
	private float updateTimer;
}
