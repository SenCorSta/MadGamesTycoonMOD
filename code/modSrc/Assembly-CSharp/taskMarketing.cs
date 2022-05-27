using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class taskMarketing : MonoBehaviour
{
	// Token: 0x06001BBA RID: 7098 RVA: 0x001142FB File Offset: 0x001124FB
	private void Awake()
	{
		base.transform.position = new Vector3(60f, 0f, 0f);
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x0011431C File Offset: 0x0011251C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x00114324 File Offset: 0x00112524
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

	// Token: 0x06001BBD RID: 7101 RVA: 0x001143F0 File Offset: 0x001125F0
	private void Update()
	{
		this.FindMyObject();
		if (this.gS_ && !this.gS_.isOnMarket && this.gS_.sellsTotal > 0L)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x00114427 File Offset: 0x00112627
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00114458 File Offset: 0x00112658
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

	// Token: 0x06001BC0 RID: 7104 RVA: 0x0011452A File Offset: 0x0011272A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00114546 File Offset: 0x00112746
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[this.kampagne];
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x00114567 File Offset: 0x00112767
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

	// Token: 0x06001BC3 RID: 7107 RVA: 0x001145A4 File Offset: 0x001127A4
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

	// Token: 0x06001BC4 RID: 7108 RVA: 0x00114824 File Offset: 0x00112A24
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

	// Token: 0x06001BC5 RID: 7109 RVA: 0x001148E8 File Offset: 0x00112AE8
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

	// Token: 0x06001BC6 RID: 7110 RVA: 0x00114A50 File Offset: 0x00112C50
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

	// Token: 0x06001BC7 RID: 7111 RVA: 0x00114AAF File Offset: 0x00112CAF
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.scriptMarketing_.preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00114ADC File Offset: 0x00112CDC
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

	// Token: 0x040022CE RID: 8910
	public int myID = -1;

	// Token: 0x040022CF RID: 8911
	public int typ = -1;

	// Token: 0x040022D0 RID: 8912
	public int targetID = -1;

	// Token: 0x040022D1 RID: 8913
	public int kampagne = -1;

	// Token: 0x040022D2 RID: 8914
	public bool automatic;

	// Token: 0x040022D3 RID: 8915
	public bool stopAutomatic;

	// Token: 0x040022D4 RID: 8916
	public bool disableWarten;

	// Token: 0x040022D5 RID: 8917
	public float points;

	// Token: 0x040022D6 RID: 8918
	public float pointsLeft;

	// Token: 0x040022D7 RID: 8919
	private GameObject main_;

	// Token: 0x040022D8 RID: 8920
	private mainScript mS_;

	// Token: 0x040022D9 RID: 8921
	private GUI_Main guiMain_;

	// Token: 0x040022DA RID: 8922
	private textScript tS_;

	// Token: 0x040022DB RID: 8923
	private roomDataScript rdS_;

	// Token: 0x040022DC RID: 8924
	public Menu_Marketing_GameKampagne scriptMarketing_;

	// Token: 0x040022DD RID: 8925
	public gameScript gS_;

	// Token: 0x040022DE RID: 8926
	public platformScript pS_;
}
