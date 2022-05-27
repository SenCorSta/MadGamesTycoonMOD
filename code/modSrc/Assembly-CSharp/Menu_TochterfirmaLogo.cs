using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000264 RID: 612
public class Menu_TochterfirmaLogo : MonoBehaviour
{
	// Token: 0x060017DA RID: 6106 RVA: 0x000EEB02 File Offset: 0x000ECD02
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x000EEB0C File Offset: 0x000ECD0C
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
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000EEBB6 File Offset: 0x000ECDB6
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x000EEBE8 File Offset: 0x000ECDE8
	public void Init(publisherScript pubScript_)
	{
		this.FindScripts();
		this.pS_ = pubScript_;
		for (int i = 0; i < this.guiMain_.logoSprites.Length; i++)
		{
			if (this.guiMain_.logoSprites[i])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_FirmenlogoTochterfirma component = gameObject.GetComponent<Item_FirmenlogoTochterfirma>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				if (this.LogoUsed(i))
				{
					gameObject.name = "A";
				}
				else
				{
					gameObject.name = "B";
				}
			}
		}
		this.mS_.SortChildrenByName(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x000EECF8 File Offset: 0x000ECEF8
	private bool LogoUsed(int id_)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] && array[i].GetComponent<publisherScript>().logoID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x000EED3B File Offset: 0x000ECF3B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B8B RID: 7051
	private mainScript mS_;

	// Token: 0x04001B8C RID: 7052
	private GameObject main_;

	// Token: 0x04001B8D RID: 7053
	private GUI_Main guiMain_;

	// Token: 0x04001B8E RID: 7054
	private sfxScript sfx_;

	// Token: 0x04001B8F RID: 7055
	private textScript tS_;

	// Token: 0x04001B90 RID: 7056
	public GameObject[] uiPrefabs;

	// Token: 0x04001B91 RID: 7057
	public GameObject[] uiObjects;

	// Token: 0x04001B92 RID: 7058
	private publisherScript pS_;
}
