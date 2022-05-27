using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DF RID: 223
public class Item_MMOtoF2P : MonoBehaviour
{
	// Token: 0x06000781 RID: 1921 RVA: 0x00006145 File Offset: 0x00004345
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0000614D File Offset: 0x0000434D
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00067ACC File Offset: 0x00065CCC
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00067B18 File Offset: 0x00065D18
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.abonnements, false);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00067BF4 File Offset: 0x00065DF4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[286]);
		this.guiMain_.uiObjects[286].GetComponent<Menu_W_MMOtoF2P>().Init(this.game_);
	}

	// Token: 0x04000B84 RID: 2948
	public gameScript game_;

	// Token: 0x04000B85 RID: 2949
	public GameObject[] uiObjects;

	// Token: 0x04000B86 RID: 2950
	public mainScript mS_;

	// Token: 0x04000B87 RID: 2951
	public textScript tS_;

	// Token: 0x04000B88 RID: 2952
	public sfxScript sfx_;

	// Token: 0x04000B89 RID: 2953
	public GUI_Main guiMain_;

	// Token: 0x04000B8A RID: 2954
	public tooltip tooltip_;

	// Token: 0x04000B8B RID: 2955
	public genres genres_;

	// Token: 0x04000B8C RID: 2956
	public Menu_MMOtoF2P menu_;

	// Token: 0x04000B8D RID: 2957
	private float updateTimer;
}
