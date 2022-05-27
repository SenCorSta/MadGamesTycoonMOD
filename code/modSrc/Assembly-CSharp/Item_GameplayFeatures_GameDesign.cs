using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_GameplayFeatures_GameDesign : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	
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

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private gameplayFeatures gF_;

	
	public tooltip tooltip_;

	
	private float updateTimer;
}
