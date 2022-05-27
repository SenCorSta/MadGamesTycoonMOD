using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000095 RID: 149
public class Item_Fanbrief : MonoBehaviour
{
	// Token: 0x060005CC RID: 1484 RVA: 0x0004B984 File Offset: 0x00049B84
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0004B98C File Offset: 0x00049B8C
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0004B994 File Offset: 0x00049B94
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

	// Token: 0x060005CF RID: 1487 RVA: 0x0004B9E0 File Offset: 0x00049BE0
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.game_.GetAmountFanbriefe().ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		if (this.game_.subgenre == -1)
		{
			this.uiObjects[3].GetComponent<Text>().text = this.game_.GetGenreString();
		}
		else
		{
			this.uiObjects[3].GetComponent<Text>().text = this.game_.GetGenreString() + " / " + this.game_.GetSubGenreString();
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0004BAD0 File Offset: 0x00049CD0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[112].SetActive(true);
		this.guiMain_.uiObjects[112].GetComponent<Menu_Dev_Fanbriefe>().Init(this.game_);
	}

	// Token: 0x040008FF RID: 2303
	public GameObject[] uiObjects;

	// Token: 0x04000900 RID: 2304
	public mainScript mS_;

	// Token: 0x04000901 RID: 2305
	public textScript tS_;

	// Token: 0x04000902 RID: 2306
	public sfxScript sfx_;

	// Token: 0x04000903 RID: 2307
	public GUI_Main guiMain_;

	// Token: 0x04000904 RID: 2308
	public tooltip tooltip_;

	// Token: 0x04000905 RID: 2309
	public gameScript game_;

	// Token: 0x04000906 RID: 2310
	public genres genres_;

	// Token: 0x04000907 RID: 2311
	private float updateTimer;
}
