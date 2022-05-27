using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class taskTraining : MonoBehaviour
{
	// Token: 0x06001BF0 RID: 7152 RVA: 0x000132A0 File Offset: 0x000114A0
	private void Awake()
	{
		base.transform.position = new Vector3(90f, 0f, 0f);
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x000132C1 File Offset: 0x000114C1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x00119BA4 File Offset: 0x00117DA4
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000132C9 File Offset: 0x000114C9
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000132FA File Offset: 0x000114FA
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x00013316 File Offset: 0x00011516
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.slot == -1)
		{
			return null;
		}
		return this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingSprites[this.slot];
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x00013348 File Offset: 0x00011548
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= f;
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x00119C4C File Offset: 0x00117E4C
	private void Complete()
	{
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		string text = this.tS_.GetText(566);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.tS_.GetText(538 + this.slot) + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[13]);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x00119D34 File Offset: 0x00117F34
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		Menu_Training_Select component = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		if (this.mS_.money < (long)component.trainingCosts[this.slot])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[13]);
			return false;
		}
		this.mS_.Pay((long)component.trainingCosts[this.slot], 13);
		this.pointsLeft = this.points;
		return true;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x00119DD8 File Offset: 0x00117FD8
	private void LeftNews(string c, Sprite icon, Sprite iconRoom)
	{
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		this.guiMain_.CreateLeftNews(roomID_, icon, c, iconRoom);
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x00013383 File Offset: 0x00011583
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingCosts[this.slot] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x00119E38 File Offset: 0x00118038
	public void Abbrechen()
	{
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)Mathf.RoundToInt((float)rueckgeld), 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.RoundToInt((float)rueckgeld), new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002324 RID: 8996
	public int myID = -1;

	// Token: 0x04002325 RID: 8997
	public int slot = -1;

	// Token: 0x04002326 RID: 8998
	public bool automatic;

	// Token: 0x04002327 RID: 8999
	public float points;

	// Token: 0x04002328 RID: 9000
	public float pointsLeft;

	// Token: 0x04002329 RID: 9001
	private GameObject main_;

	// Token: 0x0400232A RID: 9002
	private mainScript mS_;

	// Token: 0x0400232B RID: 9003
	private GUI_Main guiMain_;

	// Token: 0x0400232C RID: 9004
	private textScript tS_;

	// Token: 0x0400232D RID: 9005
	private roomDataScript rdS_;
}
