using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class taskF2PUpdate : MonoBehaviour
{
	// Token: 0x06001AFD RID: 6909 RVA: 0x00012382 File Offset: 0x00010582
	private void Awake()
	{
		base.transform.position = new Vector3(50f, 0f, 0f);
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x000123A3 File Offset: 0x000105A3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x001139C8 File Offset: 0x00111BC8
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

	// Token: 0x06001B00 RID: 6912 RVA: 0x000123AB File Offset: 0x000105AB
	private void Update()
	{
		this.FindMyObject();
		this.CheckAbbruch();
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x000123B9 File Offset: 0x000105B9
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x00113A70 File Offset: 0x00111C70
	private void CheckAbbruch()
	{
		if (!this.gS_)
		{
			return;
		}
		if (!this.gS_.isOnMarket)
		{
			if (!this.guiMain_)
			{
				this.FindScripts();
			}
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
			string text = this.tS_.GetText(842);
			text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[3], text, this.rdS_.roomData_SPRITE[1]);
			this.Abbrechen();
		}
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x00113B5C File Offset: 0x00111D5C
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
		if (gameObject)
		{
			this.gS_ = gameObject.GetComponent<gameScript>();
		}
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x000123EA File Offset: 0x000105EA
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x00012406 File Offset: 0x00010606
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[39];
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x00012416 File Offset: 0x00010616
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

	// Token: 0x06001B07 RID: 6919 RVA: 0x00113BA8 File Offset: 0x00111DA8
	private void Complete()
	{
		this.FindMyObject();
		this.gS_.costs_updates += (long)this.devCosts;
		this.gS_.AddF2PInteresse(this.quality);
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
		string text = this.tS_.GetText(1491);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[1]);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x00113C98 File Offset: 0x00111E98
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		this.pointsLeft = this.points;
		if (this.mS_.money < (long)this.devCosts)
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[1]);
			return false;
		}
		this.mS_.Pay((long)this.devCosts, 15);
		return true;
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x00113D18 File Offset: 0x00111F18
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

	// Token: 0x06001B0A RID: 6922 RVA: 0x00012451 File Offset: 0x00010651
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.devCosts * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x00113D78 File Offset: 0x00111F78
	public void Abbrechen()
	{
		this.FindMyObject();
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

	// Token: 0x04002243 RID: 8771
	public int myID = -1;

	// Token: 0x04002244 RID: 8772
	public int targetID = -1;

	// Token: 0x04002245 RID: 8773
	public float points;

	// Token: 0x04002246 RID: 8774
	public float pointsLeft;

	// Token: 0x04002247 RID: 8775
	public float quality;

	// Token: 0x04002248 RID: 8776
	public int devCosts;

	// Token: 0x04002249 RID: 8777
	public bool automatic;

	// Token: 0x0400224A RID: 8778
	private GameObject main_;

	// Token: 0x0400224B RID: 8779
	private mainScript mS_;

	// Token: 0x0400224C RID: 8780
	private GUI_Main guiMain_;

	// Token: 0x0400224D RID: 8781
	private textScript tS_;

	// Token: 0x0400224E RID: 8782
	private roomDataScript rdS_;

	// Token: 0x0400224F RID: 8783
	public gameScript gS_;
}
