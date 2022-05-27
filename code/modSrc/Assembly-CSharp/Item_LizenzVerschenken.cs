using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BD RID: 189
public class Item_LizenzVerschenken : MonoBehaviour
{
	// Token: 0x060006A5 RID: 1701 RVA: 0x00005C09 File Offset: 0x00003E09
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00063D4C File Offset: 0x00061F4C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetSellPrice(this.myID), true);
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_GEKAUFT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
		if (this.menu_.selectedLizenz == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00005C11 File Offset: 0x00003E11
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00005C44 File Offset: 0x00003E44
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.selectedLizenz = this.myID;
		this.SetData();
	}

	// Token: 0x04000A56 RID: 2646
	public int myID = -1;

	// Token: 0x04000A57 RID: 2647
	public licences licences_;

	// Token: 0x04000A58 RID: 2648
	public GameObject[] uiObjects;

	// Token: 0x04000A59 RID: 2649
	public mainScript mS_;

	// Token: 0x04000A5A RID: 2650
	public textScript tS_;

	// Token: 0x04000A5B RID: 2651
	public sfxScript sfx_;

	// Token: 0x04000A5C RID: 2652
	public GUI_Main guiMain_;

	// Token: 0x04000A5D RID: 2653
	public tooltip tooltip_;

	// Token: 0x04000A5E RID: 2654
	public Menu_MP_LizenzSchenken menu_;

	// Token: 0x04000A5F RID: 2655
	private float updateTimer;
}
