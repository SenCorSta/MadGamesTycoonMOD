using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Stats_Tochterfirma : MonoBehaviour
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
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetAmountGames().ToString();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.GetFirmenwertString();
		this.uiObjects[7].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.tf_geschlossen)
		{
			base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[25];
		}
		if (this.pS_.tf_geschlossen)
		{
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
		else if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		if (!this.pS_.developer)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1949);
			this.uiObjects[8].GetComponent<Image>().fillAmount = 0f;
			return;
		}
		float num = (float)this.pS_.newGameInWeeksORG;
		if (num <= (float)this.pS_.newGameInWeeks)
		{
			num = (float)this.pS_.newGameInWeeks;
		}
		num = 100f / num;
		num = 100f - num * (float)this.pS_.newGameInWeeks;
		this.uiObjects[8].GetComponent<Image>().fillAmount = num * 0.01f;
		if (this.pS_.newGameInWeeks <= 2)
		{
			this.uiObjects[8].GetComponent<Image>().fillAmount = 1f;
		}
		if (this.pS_.newGameInWeeks > 2)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1944) + ": " + Mathf.RoundToInt(num).ToString() + "%";
			return;
		}
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(1947);
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[387]);
		this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().Init(this.pS_);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public genres genres_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public publisherScript pS_;

	
	public int playerID = -1;

	
	private float updateTimer;
}
