using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Stats_Developer : MonoBehaviour
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
		if (this.pS_)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
			this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetAmountGames().ToString();
			this.uiObjects[4].GetComponent<Text>().text = this.pS_.GetFirmenwertString();
			this.uiObjects[7].GetComponent<Text>().text = this.pS_.GetDeveloperPublisherString();
			this.tooltip_.c = this.pS_.GetTooltip();
			base.gameObject.GetComponent<Image>().color = Color.white;
			if (this.pS_.IsMyTochterfirma())
			{
				base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			if (this.pS_.tf_geschlossen)
			{
				base.gameObject.GetComponent<Image>().color = this.guiMain_.colors[25];
			}
			if (this.pS_.tf_geschlossen)
			{
				if (!this.uiObjects[8].activeSelf)
				{
					this.uiObjects[8].SetActive(true);
					return;
				}
			}
			else if (this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(false);
			}
			return;
		}
		if (this.mS_.multiplayer && this.playerID != -1)
		{
			base.gameObject.transform.SetAsFirstSibling();
			base.gameObject.GetComponent<Button>().interactable = false;
			this.uiObjects[0].GetComponent<Text>().text = this.mS_.mpCalls_.GetCompanyName(this.playerID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mS_.mpCalls_.GetLogo(this.playerID));
			this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(5f));
			this.uiObjects[2].GetComponent<Text>().text = "---";
			this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.mpCalls_.GetMoney(this.playerID), true);
			this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(432) + " & " + this.tS_.GetText(274);
			this.tooltip_.c = "";
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[359]);
		this.guiMain_.uiObjects[359].GetComponent<Menu_Stats_Developer_Main>().Init(this.pS_);
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
