using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000192 RID: 402
public class Menu_Firmenlogo : MonoBehaviour
{
	// Token: 0x06000F3A RID: 3898 RVA: 0x0000AD03 File Offset: 0x00008F03
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000AF08C File Offset: 0x000AD28C
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

	// Token: 0x06000F3C RID: 3900 RVA: 0x0000AD0B File Offset: 0x00008F0B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0000AD3D File Offset: 0x00008F3D
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000AF138 File Offset: 0x000AD338
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

	// Token: 0x06000F3F RID: 3903 RVA: 0x000AF240 File Offset: 0x000AD440
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

	// Token: 0x06000F40 RID: 3904 RVA: 0x0000AD45 File Offset: 0x00008F45
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001390 RID: 5008
	private mainScript mS_;

	// Token: 0x04001391 RID: 5009
	private GameObject main_;

	// Token: 0x04001392 RID: 5010
	private GUI_Main guiMain_;

	// Token: 0x04001393 RID: 5011
	private sfxScript sfx_;

	// Token: 0x04001394 RID: 5012
	private textScript tS_;

	// Token: 0x04001395 RID: 5013
	public GameObject[] uiPrefabs;

	// Token: 0x04001396 RID: 5014
	public GameObject[] uiObjects;
}
