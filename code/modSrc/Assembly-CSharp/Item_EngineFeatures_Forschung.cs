using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009B RID: 155
public class Item_EngineFeatures_Forschung : MonoBehaviour
{
	// Token: 0x060005EE RID: 1518 RVA: 0x0004C7D2 File Offset: 0x0004A9D2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0004C7DC File Offset: 0x0004A9DC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.engineFeatures_PICTYP[this.eF_.engineFeatures_TYP[this.myID]];
		this.uiObjects[7].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		if ((float)this.eF_.engineFeatures_RES_POINTS[this.myID] == this.eF_.engineFeatures_RES_POINTS_LEFT[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eF_.GetPrice(this.myID), true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(157);
		}
		string text = this.tS_.GetText(156);
		text = text.Replace("<NUM>", this.mS_.Round(this.eF_.engineFeatures_RES_POINTS_LEFT[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		float prozent = this.eF_.GetProzent(this.myID);
		this.uiObjects[4].GetComponent<Image>().fillAmount = prozent * 0.01f;
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(prozent, 1).ToString() + "%";
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0004C9B2 File Offset: 0x0004ABB2
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

	// Token: 0x060005F1 RID: 1521 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
	public void BUTTON_Click()
	{
		Menu_Forschung component = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>();
		if (!this.eF_.Pay(this.myID))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		taskForschung taskForschung = this.guiMain_.AddTask_Forschung();
		taskForschung.Init(false);
		taskForschung.typ = 2;
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

	// Token: 0x04000936 RID: 2358
	public int myID;

	// Token: 0x04000937 RID: 2359
	public GameObject[] uiObjects;

	// Token: 0x04000938 RID: 2360
	public Color[] colors;

	// Token: 0x04000939 RID: 2361
	public mainScript mS_;

	// Token: 0x0400093A RID: 2362
	public textScript tS_;

	// Token: 0x0400093B RID: 2363
	public sfxScript sfx_;

	// Token: 0x0400093C RID: 2364
	public engineFeatures eF_;

	// Token: 0x0400093D RID: 2365
	public GUI_Main guiMain_;

	// Token: 0x0400093E RID: 2366
	public tooltip tooltip_;

	// Token: 0x0400093F RID: 2367
	public roomScript rS_;

	// Token: 0x04000940 RID: 2368
	private float updateTimer;
}
