using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022F RID: 559
public class Menu_Stats_Awards : MonoBehaviour
{
	// Token: 0x0600157A RID: 5498 RVA: 0x0000EC80 File Offset: 0x0000CE80
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000E4AAC File Offset: 0x000E2CAC
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
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x0000EC88 File Offset: 0x0000CE88
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x0000EC90 File Offset: 0x0000CE90
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000E4B58 File Offset: 0x000E2D58
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

	// Token: 0x0600157F RID: 5503 RVA: 0x0000EC98 File Offset: 0x0000CE98
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x0000ECB3 File Offset: 0x0000CEB3
	private void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000E4BA4 File Offset: 0x000E2DA4
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.awards[4].ToString() + "x";
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.awards[2].ToString() + "x";
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.awards[3].ToString() + "x";
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.awards[0].ToString() + "x";
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.awards[1].ToString() + "x";
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.awards[8].ToString() + "x";
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.awards[7].ToString() + "x";
		this.uiObjects[7].GetComponent<Text>().text = this.mS_.awards[6].ToString() + "x";
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.awards[5].ToString() + "x";
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.awards[9].ToString() + "x";
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.awards[10].ToString() + "x";
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.awards[11].ToString() + "x";
	}

	// Token: 0x04001982 RID: 6530
	public GameObject[] uiObjects;

	// Token: 0x04001983 RID: 6531
	private roomScript rS_;

	// Token: 0x04001984 RID: 6532
	private GameObject main_;

	// Token: 0x04001985 RID: 6533
	private mainScript mS_;

	// Token: 0x04001986 RID: 6534
	private textScript tS_;

	// Token: 0x04001987 RID: 6535
	private GUI_Main guiMain_;

	// Token: 0x04001988 RID: 6536
	private sfxScript sfx_;

	// Token: 0x04001989 RID: 6537
	private float updateTimer;
}
