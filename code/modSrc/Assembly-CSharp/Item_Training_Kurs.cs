using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000104 RID: 260
public class Item_Training_Kurs : MonoBehaviour
{
	// Token: 0x06000863 RID: 2147 RVA: 0x0005A6D9 File Offset: 0x000588D9
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0005A6E8 File Offset: 0x000588E8
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.menuTraining_)
		{
			this.menuTraining_ = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0005A7B8 File Offset: 0x000589B8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(538 + this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.menuTraining_.trainingSprites[this.myID];
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.menuTraining_.trainingCosts[this.myID], true);
		string text = this.tS_.GetText(562);
		text = text.Replace("<NUM>", this.mS_.Round(this.menuTraining_.trainingMaxLearn[this.myID], 2).ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(563 + this.menuTraining_.trainingEffekt[this.myID]);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0005A8D0 File Offset: 0x00058AD0
	public void BUTTON_Click()
	{
		if (!this.menuTraining_.rS_)
		{
			return;
		}
		this.mS_.Pay((long)this.menuTraining_.trainingCosts[this.myID], 13);
		taskTraining taskTraining = this.guiMain_.AddTask_Training();
		taskTraining.Init(false);
		taskTraining.slot = this.myID;
		taskTraining.points = this.menuTraining_.workPoints[this.myID];
		taskTraining.pointsLeft = this.menuTraining_.workPoints[this.myID];
		taskTraining.automatic = this.menuTraining_.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject gameObject = GameObject.Find("Room_" + this.menuTraining_.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskTraining.myID;
		}
		this.sfx_.PlaySound(3, true);
		this.menuTraining_.BUTTON_Close();
	}

	// Token: 0x04000CB8 RID: 3256
	public int myID;

	// Token: 0x04000CB9 RID: 3257
	public GameObject[] uiObjects;

	// Token: 0x04000CBA RID: 3258
	private GameObject main_;

	// Token: 0x04000CBB RID: 3259
	private mainScript mS_;

	// Token: 0x04000CBC RID: 3260
	private textScript tS_;

	// Token: 0x04000CBD RID: 3261
	private sfxScript sfx_;

	// Token: 0x04000CBE RID: 3262
	private GUI_Main guiMain_;

	// Token: 0x04000CBF RID: 3263
	private Menu_Training_Select menuTraining_;
}
