using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023E RID: 574
public class Menu_Stats_History : MonoBehaviour
{
	// Token: 0x0600161D RID: 5661 RVA: 0x000E1990 File Offset: 0x000DFB90
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000E1998 File Offset: 0x000DFB98
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000E1A7E File Offset: 0x000DFC7E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000E1A88 File Offset: 0x000DFC88
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.uiObjects[4].GetComponent<Text>().text = (this.seite + 1).ToString() + " / " + (this.mS_.history.Count / 100 + 1).ToString();
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000E1B10 File Offset: 0x000DFD10
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[1].GetComponent<Text>().text = "";
		for (int i = 0; i < this.mS_.history.Count; i++)
		{
			if (i >= this.seite * 100 && i < this.seite * 100 + 100)
			{
				Text component = this.uiObjects[1].GetComponent<Text>();
				component.text = component.text + this.mS_.history[i] + "\n\n";
			}
		}
		if (this.uiObjects[1].GetComponent<Text>().text.Length <= 0)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(303);
		}
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000E1BDF File Offset: 0x000DFDDF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000E1BFC File Offset: 0x000DFDFC
	public void BUTTON_Seite(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > this.mS_.history.Count / 100)
		{
			this.seite = this.mS_.history.Count / 100;
		}
		this.Init();
	}

	// Token: 0x04001A1E RID: 6686
	private mainScript mS_;

	// Token: 0x04001A1F RID: 6687
	private GameObject main_;

	// Token: 0x04001A20 RID: 6688
	private GUI_Main guiMain_;

	// Token: 0x04001A21 RID: 6689
	private sfxScript sfx_;

	// Token: 0x04001A22 RID: 6690
	private textScript tS_;

	// Token: 0x04001A23 RID: 6691
	private engineFeatures eF_;

	// Token: 0x04001A24 RID: 6692
	private genres genres_;

	// Token: 0x04001A25 RID: 6693
	public int seite;

	// Token: 0x04001A26 RID: 6694
	public GameObject[] uiPrefabs;

	// Token: 0x04001A27 RID: 6695
	public GameObject[] uiObjects;
}
