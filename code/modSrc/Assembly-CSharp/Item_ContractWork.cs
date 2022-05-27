using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_ContractWork : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void Update()
	{
		if (!this.contract_)
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

	
	private void SetData()
	{
		if (this.contract_.art != 5 && this.contract_.art != 6)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.contract_.GetName();
			this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(603) + ": " + this.mS_.Round(this.contract_.GetArbeitsaufwand() * 0.1f, 2);
		}
		if (this.contract_.art == 6)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1560);
			this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(603) + ": " + this.mS_.Round(this.contract_.GetArbeitsaufwand() * 0.1f, 2);
		}
		if (this.contract_.art == 5)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.contract_.points), false);
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.contract_.GetGehalt(), true);
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(601) + ": " + this.mS_.GetMoney((long)this.contract_.GetStrafe(), true);
		string text = this.tS_.GetText(605);
		text = text.Replace("<NUM>", this.contract_.GetWochen().ToString());
		this.uiObjects[5].GetComponent<Text>().text = text;
		if (!this.pS_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + this.contract_.auftraggeberID.ToString());
			if (gameObject)
			{
				this.pS_ = gameObject.GetComponent<publisherScript>();
			}
		}
		if (this.pS_)
		{
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			this.uiObjects[3].GetComponent<Text>().text = this.pS_.GetName();
		}
		this.tooltip_.c = this.contract_.GetTooltip();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		if (this.contract_)
		{
			UnityEngine.Object.Destroy(this.contract_.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public void BUTTON_Click()
	{
		Menu_Dev_AuftragSelect component = this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>();
		this.contract_.angenommen = true;
		taskContractWork taskContractWork = this.guiMain_.AddTask_ContractWork();
		taskContractWork.Init(false);
		taskContractWork.contractID = this.contract_.myID;
		taskContractWork.points = this.contract_.GetArbeitsaufwand();
		taskContractWork.pointsLeft = this.contract_.GetArbeitsaufwand();
		taskContractWork.automatic = component.uiObjects[4].GetComponent<Toggle>().isOn;
		if (taskContractWork.automatic)
		{
			taskContractWork.automaticWait = component.uiObjects[7].GetComponent<Toggle>().isOn;
		}
		component.rS_.taskID = taskContractWork.myID;
		this.sfx_.PlaySound(3, true);
		component.BUTTON_Close();
		this.guiMain_.uiObjects[95].SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public contractWork contract_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	private publisherScript pS_;

	
	private float updateTimer;
}
