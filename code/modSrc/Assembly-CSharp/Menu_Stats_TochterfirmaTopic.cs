using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000259 RID: 601
public class Menu_Stats_TochterfirmaTopic : MonoBehaviour
{
	// Token: 0x06001756 RID: 5974 RVA: 0x000104CE File Offset: 0x0000E6CE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000F2A08 File Offset: 0x000F0C08
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

	// Token: 0x06001758 RID: 5976 RVA: 0x000104D6 File Offset: 0x0000E6D6
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000F2AF0 File Offset: 0x000F0CF0
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

	// Token: 0x0600175A RID: 5978 RVA: 0x000F2B4C File Offset: 0x000F0D4C
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

	// Token: 0x0600175B RID: 5979 RVA: 0x000F2CA0 File Offset: 0x000F0EA0
	public void BUTTON_RemoveTopic()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_gameTopic = -1;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x00010508 File Offset: 0x0000E708
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000F2CF0 File Offset: 0x000F0EF0
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

	// Token: 0x04001B39 RID: 6969
	public GameObject[] uiPrefabs;

	// Token: 0x04001B3A RID: 6970
	public GameObject[] uiObjects;

	// Token: 0x04001B3B RID: 6971
	private mainScript mS_;

	// Token: 0x04001B3C RID: 6972
	private GameObject main_;

	// Token: 0x04001B3D RID: 6973
	private GUI_Main guiMain_;

	// Token: 0x04001B3E RID: 6974
	private sfxScript sfx_;

	// Token: 0x04001B3F RID: 6975
	private textScript tS_;

	// Token: 0x04001B40 RID: 6976
	private genres genres_;

	// Token: 0x04001B41 RID: 6977
	private themes themes_;

	// Token: 0x04001B42 RID: 6978
	private publisherScript pS_;

	// Token: 0x04001B43 RID: 6979
	private string searchStringA = "";
}
