using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class taskMarketing : MonoBehaviour
{
	// Token: 0x06001B70 RID: 7024 RVA: 0x00012A09 File Offset: 0x00010C09
	private void Awake()
	{
		base.transform.position = new Vector3(60f, 0f, 0f);
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x00012A2A File Offset: 0x00010C2A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x001172C8 File Offset: 0x001154C8
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
		if (!this.scriptMarketing_)
		{
			this.scriptMarketing_ = this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>();
		}
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x00012A32 File Offset: 0x00010C32
	private void Update()
	{
		this.FindMyObject();
		if (this.gS_ && !this.gS_.isOnMarket && this.gS_.sellsTotal > 0L)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x00012A69 File Offset: 0x00010C69
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x00117394 File Offset: 0x00115594
	private void FindMyObject()
	{
		if (this.gS_ || this.pS_)
		{
			return;
		}
		int num = this.typ;
		if (num != 0)
		{
			if (num == 1)
			{
				if (!this.pS_)
				{
					GameObject gameObject = GameObject.Find("PLATFORM_" + this.targetID.ToString());
					if (gameObject)
					{
						this.pS_ = gameObject.GetComponent<platformScript>();
					}
				}
			}
		}
		else if (!this.gS_)
		{
			GameObject gameObject2 = GameObject.Find("GAME_" + this.targetID.ToString());
			if (gameObject2)
			{
				this.gS_ = gameObject2.GetComponent<gameScript>();
			}
		}
		if (!this.gS_ && !this.pS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x00012A9A File Offset: 0x00010C9A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x00012AB6 File Offset: 0x00010CB6
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[this.kampagne];
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x00012AD7 File Offset: 0x00010CD7
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

	// Token: 0x06001B79 RID: 7033 RVA: 0x00117468 File Offset: 0x00115668
	private void Complete()
	{
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
		int num = this.typ;
		if (num != 0)
		{
			if (num == 1)
			{
				string text = this.tS_.GetText(529);
				text = text.Replace("<NAME1>", "<b><color=blue>" + this.pS_.GetName() + "</color></b>");
				this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
				if (this.pS_.hype < (float)this.scriptMarketing_.maxHype[this.kampagne])
				{
					this.pS_.AddHype((float)this.scriptMarketing_.hypeProKampagne[this.kampagne]);
					if (this.pS_.hype > (float)this.scriptMarketing_.maxHype[this.kampagne])
					{
						this.pS_.hype = (float)this.scriptMarketing_.maxHype[this.kampagne];
					}
				}
				this.pS_.costs_marketing += this.scriptMarketing_.preise[this.kampagne];
			}
		}
		else
		{
			string text = this.tS_.GetText(529);
			text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
			if (this.gS_.hype < (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.gS_.AddHype((float)this.scriptMarketing_.hypeProKampagne[this.kampagne]);
				if (this.gS_.hype > (float)this.scriptMarketing_.maxHype[this.kampagne])
				{
					this.gS_.hype = (float)this.scriptMarketing_.maxHype[this.kampagne];
				}
			}
			this.gS_.costs_marketing += (long)this.scriptMarketing_.preise[this.kampagne];
		}
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x001176E8 File Offset: 0x001158E8
	public bool WaitForMinimumHype()
	{
		if (this.automatic)
		{
			this.FindScripts();
			this.FindMyObject();
			if (this.disableWarten)
			{
				return false;
			}
			if (this.typ == 0)
			{
				if (this.gS_)
				{
					return this.gS_.hype + (float)this.scriptMarketing_.hypeProKampagne[this.kampagne] >= (float)this.scriptMarketing_.maxHype[this.kampagne];
				}
			}
			else if (this.pS_)
			{
				return this.pS_.hype + (float)this.scriptMarketing_.hypeProKampagne[this.kampagne] >= (float)this.scriptMarketing_.maxHype[this.kampagne];
			}
		}
		return false;
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x001177AC File Offset: 0x001159AC
	private bool DoAutomatic()
	{
		this.FindMyObject();
		if (!this.automatic)
		{
			return false;
		}
		if (this.stopAutomatic)
		{
			if (this.gS_ && this.gS_.hype >= (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.LeftNews(this.tS_.GetText(730), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
				return false;
			}
			if (this.pS_ && this.pS_.hype >= (float)this.scriptMarketing_.maxHype[this.kampagne])
			{
				this.LeftNews(this.tS_.GetText(730), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
				return false;
			}
		}
		if (this.mS_.money < (long)this.scriptMarketing_.preise[this.kampagne])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
			return false;
		}
		this.mS_.Pay((long)this.scriptMarketing_.preise[this.kampagne], 12);
		this.pointsLeft = this.points;
		return true;
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x00117914 File Offset: 0x00115B14
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

	// Token: 0x06001B7D RID: 7037 RVA: 0x00012B12 File Offset: 0x00010D12
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.scriptMarketing_.preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x00117974 File Offset: 0x00115B74
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

	// Token: 0x040022B4 RID: 8884
	public int myID = -1;

	// Token: 0x040022B5 RID: 8885
	public int typ = -1;

	// Token: 0x040022B6 RID: 8886
	public int targetID = -1;

	// Token: 0x040022B7 RID: 8887
	public int kampagne = -1;

	// Token: 0x040022B8 RID: 8888
	public bool automatic;

	// Token: 0x040022B9 RID: 8889
	public bool stopAutomatic;

	// Token: 0x040022BA RID: 8890
	public bool disableWarten;

	// Token: 0x040022BB RID: 8891
	public float points;

	// Token: 0x040022BC RID: 8892
	public float pointsLeft;

	// Token: 0x040022BD RID: 8893
	private GameObject main_;

	// Token: 0x040022BE RID: 8894
	private mainScript mS_;

	// Token: 0x040022BF RID: 8895
	private GUI_Main guiMain_;

	// Token: 0x040022C0 RID: 8896
	private textScript tS_;

	// Token: 0x040022C1 RID: 8897
	private roomDataScript rdS_;

	// Token: 0x040022C2 RID: 8898
	public Menu_Marketing_GameKampagne scriptMarketing_;

	// Token: 0x040022C3 RID: 8899
	public gameScript gS_;

	// Token: 0x040022C4 RID: 8900
	public platformScript pS_;
}
