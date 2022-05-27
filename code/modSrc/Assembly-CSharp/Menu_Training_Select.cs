using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026E RID: 622
public class Menu_Training_Select : MonoBehaviour
{
	// Token: 0x0600181A RID: 6170 RVA: 0x00010B64 File Offset: 0x0000ED64
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000F70E4 File Offset: 0x000F52E4
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

	// Token: 0x0600181C RID: 6172 RVA: 0x00010B6C File Offset: 0x0000ED6C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000F7190 File Offset: 0x000F5390
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		for (int i = 0; i < this.trainingMaxLearn.Length; i++)
		{
			this.trainingMaxLearn[i] = (float)(255 / (3 - i % 3));
		}
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x00010B9E File Offset: 0x0000ED9E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001BEF RID: 7151
	public int[] trainingCosts;

	// Token: 0x04001BF0 RID: 7152
	public float[] trainingMaxLearn;

	// Token: 0x04001BF1 RID: 7153
	public int[] trainingEffekt;

	// Token: 0x04001BF2 RID: 7154
	public float[] workPoints;

	// Token: 0x04001BF3 RID: 7155
	public Sprite[] trainingSprites;

	// Token: 0x04001BF4 RID: 7156
	public GameObject[] uiPrefabs;

	// Token: 0x04001BF5 RID: 7157
	public GameObject[] uiObjects;

	// Token: 0x04001BF6 RID: 7158
	private mainScript mS_;

	// Token: 0x04001BF7 RID: 7159
	private GameObject main_;

	// Token: 0x04001BF8 RID: 7160
	private GUI_Main guiMain_;

	// Token: 0x04001BF9 RID: 7161
	private sfxScript sfx_;

	// Token: 0x04001BFA RID: 7162
	private textScript tS_;

	// Token: 0x04001BFB RID: 7163
	public roomScript rS_;
}
