using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D1 RID: 465
public class Menu_MultiplayerView : MonoBehaviour
{
	// Token: 0x06001187 RID: 4487 RVA: 0x0000C4BA File Offset: 0x0000A6BA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000C5220 File Offset: 0x000C3420
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

	// Token: 0x06001189 RID: 4489 RVA: 0x000C530C File Offset: 0x000C350C
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

	// Token: 0x0600118A RID: 4490 RVA: 0x000C53EC File Offset: 0x000C35EC
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

	// Token: 0x0600118B RID: 4491 RVA: 0x000C546C File Offset: 0x000C366C
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

	// Token: 0x0600118C RID: 4492 RVA: 0x000C54E4 File Offset: 0x000C36E4
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

	// Token: 0x04001613 RID: 5651
	public GameObject[] uiObjects;

	// Token: 0x04001614 RID: 5652
	private GameObject main_;

	// Token: 0x04001615 RID: 5653
	private mainScript mS_;

	// Token: 0x04001616 RID: 5654
	private textScript tS_;

	// Token: 0x04001617 RID: 5655
	private GUI_Main guiMain_;

	// Token: 0x04001618 RID: 5656
	private sfxScript sfx_;

	// Token: 0x04001619 RID: 5657
	private mpCalls mpCalls_;

	// Token: 0x0400161A RID: 5658
	private GameObject cameraMovement;

	// Token: 0x0400161B RID: 5659
	public int playerID = -1;

	// Token: 0x0400161C RID: 5660
	private Vector3 cameraRotation;
}
