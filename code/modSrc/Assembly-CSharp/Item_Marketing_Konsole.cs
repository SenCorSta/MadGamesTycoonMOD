using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Marketing_Konsole : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (this.pS_ && this.pS_.vomMarktGenommen)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
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

	
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		if (this.pS_.isUnlocked)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(528);
		}
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[322].SetActive(false);
		this.guiMain_.uiObjects[321].GetComponent<Menu_Marketing_KonsoleKampagne>().SetKonsole(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;

	
	private float updateTimer;
}
