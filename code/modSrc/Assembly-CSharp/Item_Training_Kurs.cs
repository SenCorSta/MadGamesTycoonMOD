using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000103 RID: 259
public class Item_Training_Kurs : MonoBehaviour
{
	// Token: 0x06000854 RID: 2132 RVA: 0x000063F4 File Offset: 0x000045F4
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0006C248 File Offset: 0x0006A448
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

	// Token: 0x06000856 RID: 2134 RVA: 0x0006C318 File Offset: 0x0006A518
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

	// Token: 0x06000857 RID: 2135 RVA: 0x0006C430 File Offset: 0x0006A630
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

	// Token: 0x04000CB0 RID: 3248
	public int myID;

	// Token: 0x04000CB1 RID: 3249
	public GameObject[] uiObjects;

	// Token: 0x04000CB2 RID: 3250
	private GameObject main_;

	// Token: 0x04000CB3 RID: 3251
	private mainScript mS_;

	// Token: 0x04000CB4 RID: 3252
	private textScript tS_;

	// Token: 0x04000CB5 RID: 3253
	private sfxScript sfx_;

	// Token: 0x04000CB6 RID: 3254
	private GUI_Main guiMain_;

	// Token: 0x04000CB7 RID: 3255
	private Menu_Training_Select menuTraining_;
}
