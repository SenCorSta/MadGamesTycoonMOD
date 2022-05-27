using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000105 RID: 261
public class Item_WerkstattSelect : MonoBehaviour
{
	// Token: 0x06000868 RID: 2152 RVA: 0x0005A9D2 File Offset: 0x00058BD2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0005A9DA File Offset: 0x00058BDA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0005A9E4 File Offset: 0x00058BE4
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

	// Token: 0x0600086B RID: 2155 RVA: 0x0005AA30 File Offset: 0x00058C30
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

	// Token: 0x0600086C RID: 2156 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0005ABBC File Offset: 0x00058DBC
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

	// Token: 0x04000CC0 RID: 3264
	public gameScript game_;

	// Token: 0x04000CC1 RID: 3265
	public GameObject[] uiObjects;

	// Token: 0x04000CC2 RID: 3266
	public mainScript mS_;

	// Token: 0x04000CC3 RID: 3267
	public textScript tS_;

	// Token: 0x04000CC4 RID: 3268
	public sfxScript sfx_;

	// Token: 0x04000CC5 RID: 3269
	public GUI_Main guiMain_;

	// Token: 0x04000CC6 RID: 3270
	public tooltip tooltip_;

	// Token: 0x04000CC7 RID: 3271
	public genres genres_;

	// Token: 0x04000CC8 RID: 3272
	public Menu_ProductionArcadeSelect menu_;

	// Token: 0x04000CC9 RID: 3273
	public roomScript rS_;

	// Token: 0x04000CCA RID: 3274
	private float updateTimer;
}
