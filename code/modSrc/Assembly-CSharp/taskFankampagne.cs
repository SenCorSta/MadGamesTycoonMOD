using System;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class taskFankampagne : MonoBehaviour
{
	// Token: 0x06001B57 RID: 6999 RVA: 0x00110913 File Offset: 0x0010EB13
	private void Awake()
	{
		base.transform.position = new Vector3(190f, 0f, 0f);
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x00110934 File Offset: 0x0010EB34
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x0011093C File Offset: 0x0010EB3C
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

	// Token: 0x06001B5A RID: 7002 RVA: 0x001109E2 File Offset: 0x0010EBE2
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x00110A13 File Offset: 0x0010EC13
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x00110A2F File Offset: 0x0010EC2F
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>().sprites[this.kampagne];
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x00110A59 File Offset: 0x0010EC59
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

	// Token: 0x06001B5E RID: 7006 RVA: 0x00110A94 File Offset: 0x0010EC94
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
		string text = this.tS_.GetText(748);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.tS_.GetText(740 + this.kampagne) + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[7]);
		Menu_Support_Fankampagne component2 = this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>();
		this.mS_.AddFans(component2.fans[this.kampagne], -1);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x00110B94 File Offset: 0x0010ED94
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		Menu_Support_Fankampagne component = this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>();
		if (this.mS_.money < (long)component.preise[this.kampagne])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[7]);
			return false;
		}
		this.mS_.Pay((long)component.preise[this.kampagne], 16);
		this.pointsLeft = this.points;
		return true;
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x00110C38 File Offset: 0x0010EE38
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

	// Token: 0x06001B61 RID: 7009 RVA: 0x00110C97 File Offset: 0x0010EE97
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>().preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x00110CD4 File Offset: 0x0010EED4
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

	// Token: 0x0400226A RID: 8810
	public int myID = -1;

	// Token: 0x0400226B RID: 8811
	public int kampagne = -1;

	// Token: 0x0400226C RID: 8812
	public bool automatic;

	// Token: 0x0400226D RID: 8813
	public bool stopAutomatic;

	// Token: 0x0400226E RID: 8814
	public float points;

	// Token: 0x0400226F RID: 8815
	public float pointsLeft;

	// Token: 0x04002270 RID: 8816
	private GameObject main_;

	// Token: 0x04002271 RID: 8817
	public mainScript mS_;

	// Token: 0x04002272 RID: 8818
	private GUI_Main guiMain_;

	// Token: 0x04002273 RID: 8819
	private textScript tS_;

	// Token: 0x04002274 RID: 8820
	private roomDataScript rdS_;

	// Token: 0x04002275 RID: 8821
	public gameScript gS_;

	// Token: 0x04002276 RID: 8822
	public platformScript pS_;
}
