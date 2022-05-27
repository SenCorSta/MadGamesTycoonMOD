using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class taskMarketingSpezial : MonoBehaviour
{
	// Token: 0x06001BCA RID: 7114 RVA: 0x00114BCF File Offset: 0x00112DCF
	private void Awake()
	{
		base.transform.position = new Vector3(70f, 0f, 0f);
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x00114BF0 File Offset: 0x00112DF0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x00114BF8 File Offset: 0x00112DF8
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

	// Token: 0x06001BCD RID: 7117 RVA: 0x00114CC7 File Offset: 0x00112EC7
	private void Update()
	{
		this.FindMyObject();
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x00114CCF File Offset: 0x00112ECF
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x00114D00 File Offset: 0x00112F00
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

	// Token: 0x06001BD0 RID: 7120 RVA: 0x00114D6A File Offset: 0x00112F6A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x00114D86 File Offset: 0x00112F86
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.menu_.sprites[this.kampagne];
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x00114DA0 File Offset: 0x00112FA0
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

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00114DDC File Offset: 0x00112FDC
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

	// Token: 0x06001BD4 RID: 7124 RVA: 0x00114EDC File Offset: 0x001130DC
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

	// Token: 0x06001BD5 RID: 7125 RVA: 0x00114F3B File Offset: 0x0011313B
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.menu_.preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x00114F68 File Offset: 0x00113168
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

	// Token: 0x040022DF RID: 8927
	public int myID = -1;

	// Token: 0x040022E0 RID: 8928
	public int targetID = -1;

	// Token: 0x040022E1 RID: 8929
	public int kampagne = -1;

	// Token: 0x040022E2 RID: 8930
	public float points;

	// Token: 0x040022E3 RID: 8931
	public float pointsLeft;

	// Token: 0x040022E4 RID: 8932
	private GameObject main_;

	// Token: 0x040022E5 RID: 8933
	public mainScript mS_;

	// Token: 0x040022E6 RID: 8934
	private GUI_Main guiMain_;

	// Token: 0x040022E7 RID: 8935
	private textScript tS_;

	// Token: 0x040022E8 RID: 8936
	private roomDataScript rdS_;

	// Token: 0x040022E9 RID: 8937
	public Menu_MarketingSpezial menu_;

	// Token: 0x040022EA RID: 8938
	public gameScript gS_;
}
