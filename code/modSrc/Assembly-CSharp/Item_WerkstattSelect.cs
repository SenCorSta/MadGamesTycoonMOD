using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000104 RID: 260
public class Item_WerkstattSelect : MonoBehaviour
{
	// Token: 0x06000859 RID: 2137 RVA: 0x00006402 File Offset: 0x00004602
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0000640A File Offset: 0x0000460A
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0006C534 File Offset: 0x0006A734
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

	// Token: 0x0600085C RID: 2140 RVA: 0x0006C580 File Offset: 0x0006A780
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(1511) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(1125) + ": " + this.mS_.GetMoney((long)this.game_.vorbestellungen, false);
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0006C70C File Offset: 0x0006A90C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.multiplayer && !this.menu_.CheckGameData(this.game_))
		{
			return;
		}
		taskArcadeProduction taskArcadeProduction = this.guiMain_.AddTask_ArcadeProduction();
		taskArcadeProduction.Init(false);
		taskArcadeProduction.targetID = this.game_.myID;
		taskArcadeProduction.points = 25f;
		taskArcadeProduction.pointsLeft = 25f;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskArcadeProduction.myID;
		}
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[304].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000CB8 RID: 3256
	public gameScript game_;

	// Token: 0x04000CB9 RID: 3257
	public GameObject[] uiObjects;

	// Token: 0x04000CBA RID: 3258
	public mainScript mS_;

	// Token: 0x04000CBB RID: 3259
	public textScript tS_;

	// Token: 0x04000CBC RID: 3260
	public sfxScript sfx_;

	// Token: 0x04000CBD RID: 3261
	public GUI_Main guiMain_;

	// Token: 0x04000CBE RID: 3262
	public tooltip tooltip_;

	// Token: 0x04000CBF RID: 3263
	public genres genres_;

	// Token: 0x04000CC0 RID: 3264
	public Menu_ProductionArcadeSelect menu_;

	// Token: 0x04000CC1 RID: 3265
	public roomScript rS_;

	// Token: 0x04000CC2 RID: 3266
	private float updateTimer;
}
