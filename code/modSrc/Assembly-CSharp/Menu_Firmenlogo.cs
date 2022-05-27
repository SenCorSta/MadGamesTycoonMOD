using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000193 RID: 403
public class Menu_Firmenlogo : MonoBehaviour
{
	// Token: 0x06000F52 RID: 3922 RVA: 0x000A22A2 File Offset: 0x000A04A2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x000A22AC File Offset: 0x000A04AC
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

	// Token: 0x06000F54 RID: 3924 RVA: 0x000A2356 File Offset: 0x000A0556
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x000A2388 File Offset: 0x000A0588
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000A2390 File Offset: 0x000A0590
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.guiMain_.logoSprites.Length; i++)
		{
			if (this.guiMain_.logoSprites[i])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_Firmenlogo component = gameObject.GetComponent<Item_Firmenlogo>();
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

	// Token: 0x06000F57 RID: 3927 RVA: 0x000A2498 File Offset: 0x000A0698
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

	// Token: 0x06000F58 RID: 3928 RVA: 0x000A24DB File Offset: 0x000A06DB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001399 RID: 5017
	private mainScript mS_;

	// Token: 0x0400139A RID: 5018
	private GameObject main_;

	// Token: 0x0400139B RID: 5019
	private GUI_Main guiMain_;

	// Token: 0x0400139C RID: 5020
	private sfxScript sfx_;

	// Token: 0x0400139D RID: 5021
	private textScript tS_;

	// Token: 0x0400139E RID: 5022
	public GameObject[] uiPrefabs;

	// Token: 0x0400139F RID: 5023
	public GameObject[] uiObjects;
}
