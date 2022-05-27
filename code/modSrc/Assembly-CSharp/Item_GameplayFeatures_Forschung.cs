using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009C RID: 156
public class Item_GameplayFeatures_Forschung : MonoBehaviour
{
	// Token: 0x060005F4 RID: 1524 RVA: 0x0004CAAD File Offset: 0x0004ACAD
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		if ((float)this.gF_.gameplayFeatures_RES_POINTS[this.myID] == this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.gF_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, -1);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0004CC55 File Offset: 0x0004AE55
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

	// Token: 0x060005F7 RID: 1527 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0004CC88 File Offset: 0x0004AE88
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.gF_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 3;
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

	// Token: 0x04000941 RID: 2369
	public int myID;

	// Token: 0x04000942 RID: 2370
	public GameObject[] uiObjects;

	// Token: 0x04000943 RID: 2371
	public Color[] colors;

	// Token: 0x04000944 RID: 2372
	public mainScript mS_;

	// Token: 0x04000945 RID: 2373
	public textScript tS_;

	// Token: 0x04000946 RID: 2374
	public sfxScript sfx_;

	// Token: 0x04000947 RID: 2375
	public gameplayFeatures gF_;

	// Token: 0x04000948 RID: 2376
	public GUI_Main guiMain_;

	// Token: 0x04000949 RID: 2377
	public tooltip tooltip_;

	// Token: 0x0400094A RID: 2378
	public roomScript rS_;

	// Token: 0x0400094B RID: 2379
	private float updateTimer;
}
