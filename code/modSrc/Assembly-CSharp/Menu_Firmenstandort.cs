using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000173 RID: 371
public class Menu_Firmenstandort : MonoBehaviour
{
	// Token: 0x06000DC4 RID: 3524 RVA: 0x00094D97 File Offset: 0x00092F97
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00094DA8 File Offset: 0x00092FA8
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

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00094E7E File Offset: 0x0009307E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00094EB0 File Offset: 0x000930B0
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

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00094F74 File Offset: 0x00093174
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

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0009503B File Offset: 0x0009323B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x00095056 File Offset: 0x00093256
	public void BUTTON_Country(int i)
	{
		Debug.Log("C: " + i);
		this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetCountry(i);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00095090 File Offset: 0x00093290
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

	// Token: 0x04001264 RID: 4708
	public GameObject[] uiObjects;

	// Token: 0x04001265 RID: 4709
	private GameObject main_;

	// Token: 0x04001266 RID: 4710
	private mainScript mS_;

	// Token: 0x04001267 RID: 4711
	private textScript tS_;

	// Token: 0x04001268 RID: 4712
	private GUI_Main guiMain_;

	// Token: 0x04001269 RID: 4713
	private sfxScript sfx_;

	// Token: 0x0400126A RID: 4714
	private genres genres_;

	// Token: 0x0400126B RID: 4715
	private string searchStringA = "";
}
