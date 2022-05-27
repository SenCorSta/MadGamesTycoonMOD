using System;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class taskUpdate : MonoBehaviour
{
	// Token: 0x06001C53 RID: 7251 RVA: 0x00117B06 File Offset: 0x00115D06
	private void Awake()
	{
		base.transform.position = new Vector3(30f, 0f, 0f);
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x00117B27 File Offset: 0x00115D27
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x00117B30 File Offset: 0x00115D30
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

	// Token: 0x06001C56 RID: 7254 RVA: 0x00117BD6 File Offset: 0x00115DD6
	private void Update()
	{
		this.FindMyObject();
		this.CheckAbbruch();
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x00117BE4 File Offset: 0x00115DE4
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x00117C18 File Offset: 0x00115E18
	private void CheckAbbruch()
	{
		if (!this.gS_)
		{
			return;
		}
		if (!this.gS_.isOnMarket)
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
			string text = this.tS_.GetText(842);
			text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[3], text, this.rdS_.roomData_SPRITE[1]);
			this.Abbrechen();
		}
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x00117CF0 File Offset: 0x00115EF0
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

	// Token: 0x06001C5A RID: 7258 RVA: 0x00117D3A File Offset: 0x00115F3A
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x00117D56 File Offset: 0x00115F56
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[15];
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x00117D66 File Offset: 0x00115F66
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

	// Token: 0x06001C5D RID: 7261 RVA: 0x00117DA4 File Offset: 0x00115FA4
	private void Complete()
	{
		this.FindMyObject();
		this.gS_.costs_updates += (long)this.devCosts;
		this.gS_.amountUpdates++;
		this.gS_.bonusSellsUpdates += this.quality / (float)this.gS_.amountUpdates;
		if ((double)this.gS_.bonusSellsUpdates > 1.0)
		{
			this.gS_.bonusSellsUpdates = 1f;
		}
		this.gS_.points_gameplay += (float)this.pointsGameplay;
		this.gS_.points_grafik += (float)this.pointsGrafik;
		this.gS_.points_sound += (float)this.pointsSound;
		this.gS_.points_technik += (float)this.pointsTechnik;
		this.gS_.points_bugs -= (float)this.pointsBugs;
		if (this.gS_.points_bugs < 0f)
		{
			this.gS_.points_bugs = 0f;
		}
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.sprachen[i])
			{
				this.gS_.gameLanguage[i] = true;
			}
		}
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int j = 0; j < array.Length; j++)
		{
			roomScript component = array[j].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		string text = this.tS_.GetText(663);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[1]);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x00117FB0 File Offset: 0x001161B0
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.sprachen[i])
			{
				this.points -= 10f;
				if (!this.mS_.Muttersprache(i))
				{
					this.devCosts -= this.gS_.GetGesamtDevPoints() * 5;
				}
				if (this.devCosts < 1)
				{
					this.devCosts = 1;
				}
				this.sprachen[i] = false;
			}
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

	// Token: 0x06001C5F RID: 7263 RVA: 0x001180A0 File Offset: 0x001162A0
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

	// Token: 0x06001C60 RID: 7264 RVA: 0x001180FF File Offset: 0x001162FF
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.devCosts * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x00118120 File Offset: 0x00116320
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

	// Token: 0x04002353 RID: 9043
	public int myID = -1;

	// Token: 0x04002354 RID: 9044
	public int targetID = -1;

	// Token: 0x04002355 RID: 9045
	public float points;

	// Token: 0x04002356 RID: 9046
	public float pointsLeft;

	// Token: 0x04002357 RID: 9047
	public float quality;

	// Token: 0x04002358 RID: 9048
	public bool[] sprachen;

	// Token: 0x04002359 RID: 9049
	public int devCosts;

	// Token: 0x0400235A RID: 9050
	public int pointsGameplay;

	// Token: 0x0400235B RID: 9051
	public int pointsSound;

	// Token: 0x0400235C RID: 9052
	public int pointsGrafik;

	// Token: 0x0400235D RID: 9053
	public int pointsTechnik;

	// Token: 0x0400235E RID: 9054
	public int pointsBugs;

	// Token: 0x0400235F RID: 9055
	public bool automatic;

	// Token: 0x04002360 RID: 9056
	private GameObject main_;

	// Token: 0x04002361 RID: 9057
	private mainScript mS_;

	// Token: 0x04002362 RID: 9058
	private GUI_Main guiMain_;

	// Token: 0x04002363 RID: 9059
	private textScript tS_;

	// Token: 0x04002364 RID: 9060
	private roomDataScript rdS_;

	// Token: 0x04002365 RID: 9061
	public gameScript gS_;
}
