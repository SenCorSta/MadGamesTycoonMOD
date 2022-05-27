using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class taskTraining : MonoBehaviour
{
	// Token: 0x06001C3A RID: 7226 RVA: 0x0011746B File Offset: 0x0011566B
	private void Awake()
	{
		base.transform.position = new Vector3(90f, 0f, 0f);
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x0011748C File Offset: 0x0011568C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x00117494 File Offset: 0x00115694
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

	// Token: 0x06001C3D RID: 7229 RVA: 0x0011753A File Offset: 0x0011573A
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x0011756B File Offset: 0x0011576B
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x00117587 File Offset: 0x00115787
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.slot == -1)
		{
			return null;
		}
		return this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingSprites[this.slot];
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x001175B9 File Offset: 0x001157B9
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

	// Token: 0x06001C41 RID: 7233 RVA: 0x001175F4 File Offset: 0x001157F4
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

	// Token: 0x06001C42 RID: 7234 RVA: 0x001176DC File Offset: 0x001158DC
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

	// Token: 0x06001C43 RID: 7235 RVA: 0x00117780 File Offset: 0x00115980
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

	// Token: 0x06001C44 RID: 7236 RVA: 0x001177DF File Offset: 0x001159DF
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingCosts[this.slot] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x0011781C File Offset: 0x00115A1C
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

	// Token: 0x0400233E RID: 9022
	public int myID = -1;

	// Token: 0x0400233F RID: 9023
	public int slot = -1;

	// Token: 0x04002340 RID: 9024
	public bool automatic;

	// Token: 0x04002341 RID: 9025
	public float points;

	// Token: 0x04002342 RID: 9026
	public float pointsLeft;

	// Token: 0x04002343 RID: 9027
	private GameObject main_;

	// Token: 0x04002344 RID: 9028
	private mainScript mS_;

	// Token: 0x04002345 RID: 9029
	private GUI_Main guiMain_;

	// Token: 0x04002346 RID: 9030
	private textScript tS_;

	// Token: 0x04002347 RID: 9031
	private roomDataScript rdS_;
}
