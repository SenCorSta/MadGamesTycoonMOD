using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017F RID: 383
public class Menu_Spezialgenre : MonoBehaviour
{
	// Token: 0x06000E55 RID: 3669 RVA: 0x0000A05E File Offset: 0x0000825E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x000A96CC File Offset: 0x000A78CC
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
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0000A066 File Offset: 0x00008266
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0000A098 File Offset: 0x00008298
	private void OnEnable()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000A9794 File Offset: 0x000A7994
	private void SetData()
	{
		for (int i = 0; i < this.genres_.genres_RES_POINTS.Length; i++)
		{
			Item_MM_SpezialGenre component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MM_SpezialGenre>();
			component.myID = i;
			component.mS_ = this.mS_;
			component.tS_ = this.tS_;
			component.sfx_ = this.sfx_;
			component.guiMain_ = this.guiMain_;
			component.genres_ = this.genres_;
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0000A0A6 File Offset: 0x000082A6
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040012DA RID: 4826
	private mainScript mS_;

	// Token: 0x040012DB RID: 4827
	private GameObject main_;

	// Token: 0x040012DC RID: 4828
	private GUI_Main guiMain_;

	// Token: 0x040012DD RID: 4829
	private sfxScript sfx_;

	// Token: 0x040012DE RID: 4830
	private textScript tS_;

	// Token: 0x040012DF RID: 4831
	private genres genres_;

	// Token: 0x040012E0 RID: 4832
	public GameObject[] uiPrefabs;

	// Token: 0x040012E1 RID: 4833
	public GameObject[] uiObjects;
}
