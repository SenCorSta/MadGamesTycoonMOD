using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000270 RID: 624
public class Menu_Support_Fankampagne : MonoBehaviour
{
	// Token: 0x06001849 RID: 6217 RVA: 0x000F142D File Offset: 0x000EF62D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000F1438 File Offset: 0x000EF638
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

	// Token: 0x0600184B RID: 6219 RVA: 0x000F1522 File Offset: 0x000EF722
	private void Update()
	{
		if (this.selectedKampagne == -1)
		{
			this.uiObjects[25].GetComponent<Button>().interactable = false;
			return;
		}
		this.uiObjects[25].GetComponent<Button>().interactable = true;
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000F1558 File Offset: 0x000EF758
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

	// Token: 0x0600184D RID: 6221 RVA: 0x000F1784 File Offset: 0x000EF984
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

	// Token: 0x0600184E RID: 6222 RVA: 0x000F1918 File Offset: 0x000EFB18
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000F193E File Offset: 0x000EFB3E
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000F195C File Offset: 0x000EFB5C
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

	// Token: 0x06001851 RID: 6225 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x04001BF0 RID: 7152
	public GameObject[] uiObjects;

	// Token: 0x04001BF1 RID: 7153
	public int[] preise;

	// Token: 0x04001BF2 RID: 7154
	public int[] fans;

	// Token: 0x04001BF3 RID: 7155
	public int[] workPoints;

	// Token: 0x04001BF4 RID: 7156
	public Sprite[] sprites;

	// Token: 0x04001BF5 RID: 7157
	private GameObject main_;

	// Token: 0x04001BF6 RID: 7158
	private mainScript mS_;

	// Token: 0x04001BF7 RID: 7159
	private textScript tS_;

	// Token: 0x04001BF8 RID: 7160
	private unlockScript unlock_;

	// Token: 0x04001BF9 RID: 7161
	private GUI_Main guiMain_;

	// Token: 0x04001BFA RID: 7162
	private sfxScript sfx_;

	// Token: 0x04001BFB RID: 7163
	private cameraMovementScript cmS_;

	// Token: 0x04001BFC RID: 7164
	private roomScript rS_;

	// Token: 0x04001BFD RID: 7165
	private int selectedKampagne = -1;
}
