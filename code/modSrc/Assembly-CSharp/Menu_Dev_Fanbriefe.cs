using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000135 RID: 309
public class Menu_Dev_Fanbriefe : MonoBehaviour
{
	// Token: 0x06000B13 RID: 2835 RVA: 0x00077C9D File Offset: 0x00075E9D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00077CA8 File Offset: 0x00075EA8
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

	// Token: 0x06000B15 RID: 2837 RVA: 0x00077D70 File Offset: 0x00075F70
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00077DA4 File Offset: 0x00075FA4
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

	// Token: 0x06000B17 RID: 2839 RVA: 0x00077E9C File Offset: 0x0007609C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000F66 RID: 3942
	private mainScript mS_;

	// Token: 0x04000F67 RID: 3943
	private GameObject main_;

	// Token: 0x04000F68 RID: 3944
	private GUI_Main guiMain_;

	// Token: 0x04000F69 RID: 3945
	private sfxScript sfx_;

	// Token: 0x04000F6A RID: 3946
	private textScript tS_;

	// Token: 0x04000F6B RID: 3947
	private genres genres_;

	// Token: 0x04000F6C RID: 3948
	private gameScript gS_;

	// Token: 0x04000F6D RID: 3949
	public GameObject[] uiPrefabs;

	// Token: 0x04000F6E RID: 3950
	public GameObject[] uiObjects;
}
