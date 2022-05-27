using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_WerkstattSelect : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	
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

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
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

	
	public gameScript game_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public Menu_ProductionArcadeSelect menu_;

	
	public roomScript rS_;

	
	private float updateTimer;
}
