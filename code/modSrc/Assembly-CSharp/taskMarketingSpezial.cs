using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class taskMarketingSpezial : MonoBehaviour
{
	// Token: 0x06001B80 RID: 7040 RVA: 0x00012B63 File Offset: 0x00010D63
	private void Awake()
	{
		base.transform.position = new Vector3(70f, 0f, 0f);
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00012B84 File Offset: 0x00010D84
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00117A28 File Offset: 0x00115C28
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
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[294].GetComponent<Menu_MarketingSpezial>();
		}
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x00012B8C File Offset: 0x00010D8C
	private void Update()
	{
		this.FindMyObject();
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x00012B94 File Offset: 0x00010D94
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x00117AF8 File Offset: 0x00115CF8
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		if (!this.gS_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
			if (gameObject)
			{
				this.gS_ = gameObject.GetComponent<gameScript>();
			}
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x00012BC5 File Offset: 0x00010DC5
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00012BE1 File Offset: 0x00010DE1
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.menu_.sprites[this.kampagne];
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x00012BFB File Offset: 0x00010DFB
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

	// Token: 0x06001B89 RID: 7049 RVA: 0x00117B64 File Offset: 0x00115D64
	private void Complete()
	{
		this.FindScripts();
		this.FindMyObject();
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
		string text = this.tS_.GetText(1425);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
		this.gS_.costs_marketing += (long)this.menu_.preise[this.kampagne];
		this.gS_.specialMarketing[this.kampagne] = 1;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x00117C64 File Offset: 0x00115E64
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

	// Token: 0x06001B8B RID: 7051 RVA: 0x00012C36 File Offset: 0x00010E36
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.menu_.preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x00117CC4 File Offset: 0x00115EC4
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

	// Token: 0x040022C5 RID: 8901
	public int myID = -1;

	// Token: 0x040022C6 RID: 8902
	public int targetID = -1;

	// Token: 0x040022C7 RID: 8903
	public int kampagne = -1;

	// Token: 0x040022C8 RID: 8904
	public float points;

	// Token: 0x040022C9 RID: 8905
	public float pointsLeft;

	// Token: 0x040022CA RID: 8906
	private GameObject main_;

	// Token: 0x040022CB RID: 8907
	public mainScript mS_;

	// Token: 0x040022CC RID: 8908
	private GUI_Main guiMain_;

	// Token: 0x040022CD RID: 8909
	private textScript tS_;

	// Token: 0x040022CE RID: 8910
	private roomDataScript rdS_;

	// Token: 0x040022CF RID: 8911
	public Menu_MarketingSpezial menu_;

	// Token: 0x040022D0 RID: 8912
	public gameScript gS_;
}
