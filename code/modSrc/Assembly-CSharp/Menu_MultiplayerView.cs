using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D2 RID: 466
public class Menu_MultiplayerView : MonoBehaviour
{
	// Token: 0x060011A1 RID: 4513 RVA: 0x000B9E87 File Offset: 0x000B8087
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000B9E90 File Offset: 0x000B8090
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
		if (!this.cameraMovement)
		{
			this.cameraMovement = GameObject.Find("CamMovement");
		}
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x000B9F7C File Offset: 0x000B817C
	public void Init(int p)
	{
		this.FindScripts();
		this.cameraRotation = this.cameraMovement.transform.eulerAngles;
		this.cameraMovement.transform.eulerAngles = new Vector3(this.cameraMovement.transform.eulerAngles.x, (float)UnityEngine.Random.Range(0, 359), this.cameraMovement.transform.eulerAngles.z);
		this.playerID = p;
		this.SetMainGuiToggles(false);
		this.guiMain_.CameraBlend();
		this.sfx_.PlaySound(58);
		if (p != -1)
		{
			string text = this.tS_.GetText(1277);
			text = text.Replace("<NAME>", this.mpCalls_.GetPlayerName(this.playerID));
			this.uiObjects[1].GetComponent<Text>().text = text;
		}
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000BA05C File Offset: 0x000B825C
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(true);
		}
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				this.mS_.arrayCharacters[i].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x000BA0DC File Offset: 0x000B82DC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(58);
		this.guiMain_.CameraBlend();
		this.playerID = -1;
		this.SetMainGuiToggles(true);
		this.mS_.CloseMultiplayerView();
		this.guiMain_.CloseMenu();
		this.sfx_.PlaySound(3, true);
		this.cameraMovement.transform.eulerAngles = this.cameraRotation;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000BA154 File Offset: 0x000B8354
	private void SetMainGuiToggles(bool b)
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			if (this.uiObjects[0].transform.GetChild(i) && this.uiObjects[0].transform.GetChild(i).GetComponent<Toggle>())
			{
				this.uiObjects[0].transform.GetChild(i).GetComponent<Toggle>().interactable = b;
			}
		}
	}

	// Token: 0x0400161C RID: 5660
	public GameObject[] uiObjects;

	// Token: 0x0400161D RID: 5661
	private GameObject main_;

	// Token: 0x0400161E RID: 5662
	private mainScript mS_;

	// Token: 0x0400161F RID: 5663
	private textScript tS_;

	// Token: 0x04001620 RID: 5664
	private GUI_Main guiMain_;

	// Token: 0x04001621 RID: 5665
	private sfxScript sfx_;

	// Token: 0x04001622 RID: 5666
	private mpCalls mpCalls_;

	// Token: 0x04001623 RID: 5667
	private GameObject cameraMovement;

	// Token: 0x04001624 RID: 5668
	public int playerID = -1;

	// Token: 0x04001625 RID: 5669
	private Vector3 cameraRotation;
}
