using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000172 RID: 370
public class Menu_Firmenstandort : MonoBehaviour
{
	// Token: 0x06000DAC RID: 3500 RVA: 0x000097D2 File Offset: 0x000079D2
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000A3228 File Offset: 0x000A1428
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
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

	// Token: 0x06000DAE RID: 3502 RVA: 0x000097E0 File Offset: 0x000079E0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x000A3300 File Offset: 0x000A1500
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			Transform child = this.uiObjects[0].transform.GetChild(i);
			child.gameObject.SetActive(true);
			Transform child2 = child.transform.GetChild(3);
			Component child3 = child.transform.GetChild(2);
			child2.GetComponent<Text>().text = this.tS_.GetCountry(i);
			child3.GetComponent<Image>().sprite = this.guiMain_.flagSprites[i];
			child.name = this.tS_.GetCountry(i);
		}
		this.mS_.SortChildrenByName(this.uiObjects[0]);
		this.EnableDisable();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x000A33C4 File Offset: 0x000A15C4
	private void EnableDisable()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			string text = this.uiObjects[0].transform.GetChild(i).name;
			this.searchStringA = this.searchStringA.ToLower();
			text = text.ToLower();
			if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
			{
				this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(true);
			}
			else
			{
				this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x00009812 File Offset: 0x00007A12
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0000982D File Offset: 0x00007A2D
	public void BUTTON_Country(int i)
	{
		Debug.Log("C: " + i);
		this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetCountry(i);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x000A348C File Offset: 0x000A168C
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.EnableDisable();
	}

	// Token: 0x0400125C RID: 4700
	public GameObject[] uiObjects;

	// Token: 0x0400125D RID: 4701
	private GameObject main_;

	// Token: 0x0400125E RID: 4702
	private mainScript mS_;

	// Token: 0x0400125F RID: 4703
	private textScript tS_;

	// Token: 0x04001260 RID: 4704
	private GUI_Main guiMain_;

	// Token: 0x04001261 RID: 4705
	private sfxScript sfx_;

	// Token: 0x04001262 RID: 4706
	private genres genres_;

	// Token: 0x04001263 RID: 4707
	private string searchStringA = "";
}
