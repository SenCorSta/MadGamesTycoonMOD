using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023D RID: 573
public class Menu_Stats_History : MonoBehaviour
{
	// Token: 0x060015FF RID: 5631 RVA: 0x0000F27A File Offset: 0x0000D47A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000E98EC File Offset: 0x000E7AEC
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

	// Token: 0x06001601 RID: 5633 RVA: 0x0000F282 File Offset: 0x0000D482
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000E99D4 File Offset: 0x000E7BD4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.uiObjects[4].GetComponent<Text>().text = (this.seite + 1).ToString() + " / " + (this.mS_.history.Count / 100 + 1).ToString();
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000E9A5C File Offset: 0x000E7C5C
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

	// Token: 0x06001604 RID: 5636 RVA: 0x0000F28A File Offset: 0x0000D48A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x000E9B2C File Offset: 0x000E7D2C
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

	// Token: 0x04001A15 RID: 6677
	private mainScript mS_;

	// Token: 0x04001A16 RID: 6678
	private GameObject main_;

	// Token: 0x04001A17 RID: 6679
	private GUI_Main guiMain_;

	// Token: 0x04001A18 RID: 6680
	private sfxScript sfx_;

	// Token: 0x04001A19 RID: 6681
	private textScript tS_;

	// Token: 0x04001A1A RID: 6682
	private engineFeatures eF_;

	// Token: 0x04001A1B RID: 6683
	private genres genres_;

	// Token: 0x04001A1C RID: 6684
	public int seite;

	// Token: 0x04001A1D RID: 6685
	public GameObject[] uiPrefabs;

	// Token: 0x04001A1E RID: 6686
	public GameObject[] uiObjects;
}
