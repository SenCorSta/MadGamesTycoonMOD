using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A5 RID: 165
public class Item_EngineFeatures_GameDesign : MonoBehaviour
{
	// Token: 0x06000624 RID: 1572 RVA: 0x000058E2 File Offset: 0x00003AE2
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00061030 File Offset: 0x0005F230
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.FindWithTag("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.tS_ = this.main_.GetComponent<textScript>();
		this.eF_ = this.main_.GetComponent<engineFeatures>();
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00061090 File Offset: 0x0005F290
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.GetTypPic(this.myID);
		this.uiObjects[2].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		this.uiObjects[3].GetComponent<stars>().amount = this.eF_.engineFeatures_LEVEL[this.myID];
		this.tooltip_.c = string.Concat(new string[]
		{
			"<b>",
			this.eF_.GetName(this.myID),
			"</b>\n",
			this.eF_.GetDesc(this.myID),
			"\n\n",
			this.tS_.GetText(8),
			"\n\n<b><i>",
			this.tS_.GetText(6),
			"\n",
			this.mS_.GetMoney((long)this.eF_.GetDevCosts(this.myID), true),
			"\n\n</i></b><b><color=grey>",
			this.tS_.GetText(4),
			" ",
			this.eF_.engineFeatures_TECH[this.myID].ToString(),
			"</color>\n<color=green>",
			this.tS_.GetText(1),
			" +",
			this.eF_.GetGameplay(this.myID).ToString(),
			"</color>\n<color=blue>",
			this.tS_.GetText(2),
			" +",
			this.eF_.GetGraphic(this.myID).ToString(),
			"</color>\n<color=magenta>",
			this.tS_.GetText(3),
			" +",
			this.eF_.GetSound(this.myID).ToString(),
			"</color>\n<color=orange>",
			this.tS_.GetText(74),
			" +",
			this.eF_.GetTechnik(this.myID).ToString(),
			"</color>\n</b>"
		});
		for (int i = 0; i < this.eF_.engineFeatures_LEVEL[this.myID]; i++)
		{
			tooltip tooltip = this.tooltip_;
			tooltip.c += "<size=22><b><color=orange>★</color></b></size>";
		}
		for (int j = this.eF_.engineFeatures_LEVEL[this.myID]; j < 5; j++)
		{
			tooltip tooltip2 = this.tooltip_;
			tooltip2.c += "<size=22><b><color=black>★</color></b></size>";
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x000058F0 File Offset: 0x00003AF0
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

	// Token: 0x040009A9 RID: 2473
	public int myID;

	// Token: 0x040009AA RID: 2474
	public GameObject[] uiObjects;

	// Token: 0x040009AB RID: 2475
	private GameObject main_;

	// Token: 0x040009AC RID: 2476
	private mainScript mS_;

	// Token: 0x040009AD RID: 2477
	private textScript tS_;

	// Token: 0x040009AE RID: 2478
	private engineFeatures eF_;

	// Token: 0x040009AF RID: 2479
	public tooltip tooltip_;

	// Token: 0x040009B0 RID: 2480
	private float updateTimer;
}
