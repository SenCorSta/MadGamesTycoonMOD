using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000079 RID: 121
public class Item_ContractWork : MonoBehaviour
{
	// Token: 0x06000515 RID: 1301 RVA: 0x00047125 File Offset: 0x00045325
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0004712D File Offset: 0x0004532D
	private void Update()
	{
		if (!this.contract_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00047150 File Offset: 0x00045350
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

	// Token: 0x06000518 RID: 1304 RVA: 0x0004719C File Offset: 0x0004539C
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

	// Token: 0x06000519 RID: 1305 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0004743A File Offset: 0x0004563A
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		if (this.contract_)
		{
			UnityEngine.Object.Destroy(this.contract_.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00047474 File Offset: 0x00045674
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

	// Token: 0x04000801 RID: 2049
	public contractWork contract_;

	// Token: 0x04000802 RID: 2050
	public GameObject[] uiObjects;

	// Token: 0x04000803 RID: 2051
	public mainScript mS_;

	// Token: 0x04000804 RID: 2052
	public textScript tS_;

	// Token: 0x04000805 RID: 2053
	public sfxScript sfx_;

	// Token: 0x04000806 RID: 2054
	public GUI_Main guiMain_;

	// Token: 0x04000807 RID: 2055
	public tooltip tooltip_;

	// Token: 0x04000808 RID: 2056
	private publisherScript pS_;

	// Token: 0x04000809 RID: 2057
	private float updateTimer;
}
