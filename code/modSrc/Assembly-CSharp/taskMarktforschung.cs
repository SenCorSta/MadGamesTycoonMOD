using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class taskMarktforschung : MonoBehaviour
{
	// Token: 0x06001B8E RID: 7054 RVA: 0x00012C80 File Offset: 0x00010E80
	private void Awake()
	{
		base.transform.position = new Vector3(230f, 0f, 0f);
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x00012CA1 File Offset: 0x00010EA1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x00117D78 File Offset: 0x00115F78
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

	// Token: 0x06001B91 RID: 7057 RVA: 0x00012CA9 File Offset: 0x00010EA9
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B92 RID: 7058 RVA: 0x00012CDA File Offset: 0x00010EDA
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B93 RID: 7059 RVA: 0x00012CF6 File Offset: 0x00010EF6
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiSprites[28];
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x00012D0C File Offset: 0x00010F0C
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

	// Token: 0x06001B95 RID: 7061 RVA: 0x00117E20 File Offset: 0x00116020
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
		string text = this.tS_.GetText(1165);
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
		this.mS_.NewMarktforschung();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x00117EBC File Offset: 0x001160BC
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

	// Token: 0x06001B97 RID: 7063 RVA: 0x000030EA File Offset: 0x000012EA
	public int GetRueckgeld()
	{
		return 0;
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022D1 RID: 8913
	public int myID = -1;

	// Token: 0x040022D2 RID: 8914
	public float points;

	// Token: 0x040022D3 RID: 8915
	public float pointsLeft;

	// Token: 0x040022D4 RID: 8916
	private GameObject main_;

	// Token: 0x040022D5 RID: 8917
	public mainScript mS_;

	// Token: 0x040022D6 RID: 8918
	private GUI_Main guiMain_;

	// Token: 0x040022D7 RID: 8919
	private textScript tS_;

	// Token: 0x040022D8 RID: 8920
	private roomDataScript rdS_;
}
