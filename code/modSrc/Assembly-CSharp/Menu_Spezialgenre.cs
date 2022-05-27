using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000180 RID: 384
public class Menu_Spezialgenre : MonoBehaviour
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x0009BB9B File Offset: 0x00099D9B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0009BBA4 File Offset: 0x00099DA4
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

	// Token: 0x06000E6F RID: 3695 RVA: 0x0009BC6C File Offset: 0x00099E6C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0009BC9E File Offset: 0x00099E9E
	private void OnEnable()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0009BCAC File Offset: 0x00099EAC
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

	// Token: 0x06000E72 RID: 3698 RVA: 0x0009BD4E File Offset: 0x00099F4E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040012E3 RID: 4835
	private mainScript mS_;

	// Token: 0x040012E4 RID: 4836
	private GameObject main_;

	// Token: 0x040012E5 RID: 4837
	private GUI_Main guiMain_;

	// Token: 0x040012E6 RID: 4838
	private sfxScript sfx_;

	// Token: 0x040012E7 RID: 4839
	private textScript tS_;

	// Token: 0x040012E8 RID: 4840
	private genres genres_;

	// Token: 0x040012E9 RID: 4841
	public GameObject[] uiPrefabs;

	// Token: 0x040012EA RID: 4842
	public GameObject[] uiObjects;
}
