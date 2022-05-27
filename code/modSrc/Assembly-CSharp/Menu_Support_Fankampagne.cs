using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026C RID: 620
public class Menu_Support_Fankampagne : MonoBehaviour
{
	// Token: 0x06001806 RID: 6150 RVA: 0x00010A65 File Offset: 0x0000EC65
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x000F6860 File Offset: 0x000F4A60
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x00010A6D File Offset: 0x0000EC6D
	private void Update()
	{
		if (this.selectedKampagne == -1)
		{
			this.uiObjects[25].GetComponent<Button>().interactable = false;
			return;
		}
		this.uiObjects[25].GetComponent<Button>().interactable = true;
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x000F694C File Offset: 0x000F4B4C
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedKampagne = -1;
		this.SetButtonColor(-1);
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[0], true);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[1], true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[2], true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[3], true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[4], true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[5], true);
		this.uiObjects[16].GetComponent<Text>().text = "+" + this.fans[0].ToString();
		this.uiObjects[17].GetComponent<Text>().text = "+" + this.fans[1].ToString();
		this.uiObjects[18].GetComponent<Text>().text = "+" + this.fans[2].ToString();
		this.uiObjects[19].GetComponent<Text>().text = "+" + this.fans[3].ToString();
		this.uiObjects[20].GetComponent<Text>().text = "+" + this.fans[4].ToString();
		this.uiObjects[21].GetComponent<Text>().text = "+" + this.fans[5].ToString();
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000F6B78 File Offset: 0x000F4D78
	private void SetButtonColor(int i)
	{
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[4].GetComponent<Image>().color = Color.white;
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
		this.uiObjects[6].GetComponent<Image>().color = Color.white;
		this.uiObjects[7].GetComponent<Image>().color = Color.white;
		if (i != -1)
		{
			switch (i)
			{
			case 0:
				this.uiObjects[2].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 1:
				this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 2:
				this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 3:
				this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 4:
				this.uiObjects[6].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 5:
				this.uiObjects[7].GetComponent<Image>().color = this.guiMain_.colors[7];
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x00010AA1 File Offset: 0x0000ECA1
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x00010AC7 File Offset: 0x0000ECC7
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000F6D0C File Offset: 0x000F4F0C
	public void BUTTON_OK()
	{
		if (this.selectedKampagne == -1)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (this.mS_.NotEnoughMoney(this.preise[this.selectedKampagne]))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.preise[this.selectedKampagne], 16);
		taskFankampagne taskFankampagne = this.guiMain_.AddTask_Fankampagne();
		taskFankampagne.Init(false);
		taskFankampagne.kampagne = this.selectedKampagne;
		taskFankampagne.automatic = this.uiObjects[14].GetComponent<Toggle>().isOn;
		taskFankampagne.points = (float)this.workPoints[this.selectedKampagne];
		taskFankampagne.pointsLeft = (float)this.workPoints[this.selectedKampagne];
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskFankampagne.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x04001BD6 RID: 7126
	public GameObject[] uiObjects;

	// Token: 0x04001BD7 RID: 7127
	public int[] preise;

	// Token: 0x04001BD8 RID: 7128
	public int[] fans;

	// Token: 0x04001BD9 RID: 7129
	public int[] workPoints;

	// Token: 0x04001BDA RID: 7130
	public Sprite[] sprites;

	// Token: 0x04001BDB RID: 7131
	private GameObject main_;

	// Token: 0x04001BDC RID: 7132
	private mainScript mS_;

	// Token: 0x04001BDD RID: 7133
	private textScript tS_;

	// Token: 0x04001BDE RID: 7134
	private unlockScript unlock_;

	// Token: 0x04001BDF RID: 7135
	private GUI_Main guiMain_;

	// Token: 0x04001BE0 RID: 7136
	private sfxScript sfx_;

	// Token: 0x04001BE1 RID: 7137
	private cameraMovementScript cmS_;

	// Token: 0x04001BE2 RID: 7138
	private roomScript rS_;

	// Token: 0x04001BE3 RID: 7139
	private int selectedKampagne = -1;
}
