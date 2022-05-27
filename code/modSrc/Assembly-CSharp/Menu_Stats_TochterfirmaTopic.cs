using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025D RID: 605
public class Menu_Stats_TochterfirmaTopic : MonoBehaviour
{
	// Token: 0x06001796 RID: 6038 RVA: 0x000ECE33 File Offset: 0x000EB033
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000ECE3C File Offset: 0x000EB03C
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x000ECF22 File Offset: 0x000EB122
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x000ECF54 File Offset: 0x000EB154
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000ECFB0 File Offset: 0x000EB1B0
	private void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		for (int i = 0; i < this.themes_.themes_LEVEL.Length; i++)
		{
			string text = this.tS_.GetThemes(i);
			this.searchStringA = this.searchStringA.ToLower();
			text = text.ToLower();
			if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_Tochterfirma_Theme component = gameObject.GetComponent<Item_Tochterfirma_Theme>();
				gameObject.name = this.tS_.GetThemes(i);
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.themes_ = this.themes_;
				component.pS_ = this.pS_;
			}
		}
		this.mS_.SortChildrenByName(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x000ED104 File Offset: 0x000EB304
	public void BUTTON_RemoveTopic()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_gameTopic = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x000ED151 File Offset: 0x000EB351
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000ED16C File Offset: 0x000EB36C
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
		this.Init(this.pS_);
	}

	// Token: 0x04001B53 RID: 6995
	public GameObject[] uiPrefabs;

	// Token: 0x04001B54 RID: 6996
	public GameObject[] uiObjects;

	// Token: 0x04001B55 RID: 6997
	private mainScript mS_;

	// Token: 0x04001B56 RID: 6998
	private GameObject main_;

	// Token: 0x04001B57 RID: 6999
	private GUI_Main guiMain_;

	// Token: 0x04001B58 RID: 7000
	private sfxScript sfx_;

	// Token: 0x04001B59 RID: 7001
	private textScript tS_;

	// Token: 0x04001B5A RID: 7002
	private genres genres_;

	// Token: 0x04001B5B RID: 7003
	private themes themes_;

	// Token: 0x04001B5C RID: 7004
	private publisherScript pS_;

	// Token: 0x04001B5D RID: 7005
	private string searchStringA = "";
}
