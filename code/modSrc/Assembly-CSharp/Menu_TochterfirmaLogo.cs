using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000260 RID: 608
public class Menu_TochterfirmaLogo : MonoBehaviour
{
	// Token: 0x06001797 RID: 6039 RVA: 0x000106C3 File Offset: 0x0000E8C3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x000F437C File Offset: 0x000F257C
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

	// Token: 0x06001799 RID: 6041 RVA: 0x000106CB File Offset: 0x0000E8CB
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000F4428 File Offset: 0x000F2628
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

	// Token: 0x0600179B RID: 6043 RVA: 0x000AF240 File Offset: 0x000AD440
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

	// Token: 0x0600179C RID: 6044 RVA: 0x000106FD File Offset: 0x0000E8FD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B71 RID: 7025
	private mainScript mS_;

	// Token: 0x04001B72 RID: 7026
	private GameObject main_;

	// Token: 0x04001B73 RID: 7027
	private GUI_Main guiMain_;

	// Token: 0x04001B74 RID: 7028
	private sfxScript sfx_;

	// Token: 0x04001B75 RID: 7029
	private textScript tS_;

	// Token: 0x04001B76 RID: 7030
	public GameObject[] uiPrefabs;

	// Token: 0x04001B77 RID: 7031
	public GameObject[] uiObjects;

	// Token: 0x04001B78 RID: 7032
	private publisherScript pS_;
}
