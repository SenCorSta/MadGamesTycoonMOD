using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A6 RID: 166
public class Item_GameplayFeatures_GameDesign : MonoBehaviour
{
	// Token: 0x06000629 RID: 1577 RVA: 0x00005923 File Offset: 0x00003B23
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00061394 File Offset: 0x0005F594
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.Find("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.tS_ = this.main_.GetComponent<textScript>();
		this.gF_ = this.main_.GetComponent<gameplayFeatures>();
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x000613F4 File Offset: 0x0005F5F4
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.uiObjects[2].GetComponent<stars>().amount = this.gF_.gameplayFeatures_LEVEL[this.myID];
		this.tooltip_.c = string.Concat(new string[]
		{
			"<b>",
			this.gF_.GetName(this.myID),
			"</b>\n",
			this.gF_.GetDesc(this.myID),
			"\n\n",
			this.tS_.GetText(8),
			"\n\n<b><i>",
			this.tS_.GetText(6),
			"\n",
			this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true),
			"\n\n</i><color=green>",
			this.tS_.GetText(1),
			" +",
			this.gF_.GetGameplay(this.myID, -1).ToString(),
			"</color>\n<color=blue>",
			this.tS_.GetText(2),
			" +",
			this.gF_.GetGraphic(this.myID, -1).ToString(),
			"</color>\n<color=magenta>",
			this.tS_.GetText(3),
			" +",
			this.gF_.GetSound(this.myID, -1).ToString(),
			"</color>\n<color=orange>",
			this.tS_.GetText(74),
			" +",
			this.gF_.GetSound(this.myID, -1).ToString(),
			"</color>\n</b>"
		});
		for (int i = 0; i < this.gF_.gameplayFeatures_LEVEL[this.myID]; i++)
		{
			tooltip tooltip = this.tooltip_;
			tooltip.c += "<size=22><b><color=orange>★</color></b></size>";
		}
		for (int j = this.gF_.gameplayFeatures_LEVEL[this.myID]; j < 5; j++)
		{
			tooltip tooltip2 = this.tooltip_;
			tooltip2.c += "<size=22><b><color=black>★</color></b></size>";
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00005931 File Offset: 0x00003B31
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x040009B1 RID: 2481
	public int myID;

	// Token: 0x040009B2 RID: 2482
	public GameObject[] uiObjects;

	// Token: 0x040009B3 RID: 2483
	private GameObject main_;

	// Token: 0x040009B4 RID: 2484
	private mainScript mS_;

	// Token: 0x040009B5 RID: 2485
	private textScript tS_;

	// Token: 0x040009B6 RID: 2486
	private gameplayFeatures gF_;

	// Token: 0x040009B7 RID: 2487
	public tooltip tooltip_;

	// Token: 0x040009B8 RID: 2488
	private float updateTimer;
}
