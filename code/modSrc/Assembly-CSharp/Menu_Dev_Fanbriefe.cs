using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000134 RID: 308
public class Menu_Dev_Fanbriefe : MonoBehaviour
{
	// Token: 0x06000B01 RID: 2817 RVA: 0x00007D65 File Offset: 0x00005F65
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00087E34 File Offset: 0x00086034
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

	// Token: 0x06000B03 RID: 2819 RVA: 0x00007D6D File Offset: 0x00005F6D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00087EFC File Offset: 0x000860FC
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		string text = this.tS_.GetText(668);
		text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
		this.uiObjects[1].GetComponent<Text>().text = text;
		for (int i = 0; i < game_.fanbrief.Length; i++)
		{
			if (game_.fanbrief[i])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				text = this.tS_.GetFanLetter(i);
				text = text.Replace("<NAME>", game_.GetNameWithTag());
				gameObject.transform.GetChild(0).GetComponent<Text>().text = text;
			}
		}
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00007D9F File Offset: 0x00005F9F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000F5E RID: 3934
	private mainScript mS_;

	// Token: 0x04000F5F RID: 3935
	private GameObject main_;

	// Token: 0x04000F60 RID: 3936
	private GUI_Main guiMain_;

	// Token: 0x04000F61 RID: 3937
	private sfxScript sfx_;

	// Token: 0x04000F62 RID: 3938
	private textScript tS_;

	// Token: 0x04000F63 RID: 3939
	private genres genres_;

	// Token: 0x04000F64 RID: 3940
	private gameScript gS_;

	// Token: 0x04000F65 RID: 3941
	public GameObject[] uiPrefabs;

	// Token: 0x04000F66 RID: 3942
	public GameObject[] uiObjects;
}
