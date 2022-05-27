using System;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class taskUpdate : MonoBehaviour
{
	// Token: 0x06001C09 RID: 7177 RVA: 0x0001347A File Offset: 0x0001167A
	private void Awake()
	{
		base.transform.position = new Vector3(30f, 0f, 0f);
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x0001349B File Offset: 0x0001169B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x0011A048 File Offset: 0x00118248
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

	// Token: 0x06001C0C RID: 7180 RVA: 0x000134A3 File Offset: 0x000116A3
	private void Update()
	{
		this.FindMyObject();
		this.CheckAbbruch();
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000134B1 File Offset: 0x000116B1
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x0011A0F0 File Offset: 0x001182F0
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

	// Token: 0x06001C0F RID: 7183 RVA: 0x0011A1C8 File Offset: 0x001183C8
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

	// Token: 0x06001C10 RID: 7184 RVA: 0x000134E2 File Offset: 0x000116E2
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x000134FE File Offset: 0x000116FE
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[15];
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x0001350E File Offset: 0x0001170E
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

	// Token: 0x06001C13 RID: 7187 RVA: 0x0011A214 File Offset: 0x00118414
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

	// Token: 0x06001C14 RID: 7188 RVA: 0x0011A420 File Offset: 0x00118620
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

	// Token: 0x06001C15 RID: 7189 RVA: 0x0011A510 File Offset: 0x00118710
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

	// Token: 0x06001C16 RID: 7190 RVA: 0x00013549 File Offset: 0x00011749
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.devCosts * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0011A570 File Offset: 0x00118770
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

	// Token: 0x04002339 RID: 9017
	public int myID = -1;

	// Token: 0x0400233A RID: 9018
	public int targetID = -1;

	// Token: 0x0400233B RID: 9019
	public float points;

	// Token: 0x0400233C RID: 9020
	public float pointsLeft;

	// Token: 0x0400233D RID: 9021
	public float quality;

	// Token: 0x0400233E RID: 9022
	public bool[] sprachen;

	// Token: 0x0400233F RID: 9023
	public int devCosts;

	// Token: 0x04002340 RID: 9024
	public int pointsGameplay;

	// Token: 0x04002341 RID: 9025
	public int pointsSound;

	// Token: 0x04002342 RID: 9026
	public int pointsGrafik;

	// Token: 0x04002343 RID: 9027
	public int pointsTechnik;

	// Token: 0x04002344 RID: 9028
	public int pointsBugs;

	// Token: 0x04002345 RID: 9029
	public bool automatic;

	// Token: 0x04002346 RID: 9030
	private GameObject main_;

	// Token: 0x04002347 RID: 9031
	private mainScript mS_;

	// Token: 0x04002348 RID: 9032
	private GUI_Main guiMain_;

	// Token: 0x04002349 RID: 9033
	private textScript tS_;

	// Token: 0x0400234A RID: 9034
	private roomDataScript rdS_;

	// Token: 0x0400234B RID: 9035
	public gameScript gS_;
}
