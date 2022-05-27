using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class taskF2PUpdate : MonoBehaviour
{
	// Token: 0x06001B47 RID: 6983 RVA: 0x00110386 File Offset: 0x0010E586
	private void Awake()
	{
		base.transform.position = new Vector3(50f, 0f, 0f);
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x001103A7 File Offset: 0x0010E5A7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x001103B0 File Offset: 0x0010E5B0
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

	// Token: 0x06001B4A RID: 6986 RVA: 0x00110456 File Offset: 0x0010E656
	private void Update()
	{
		this.FindMyObject();
		this.CheckAbbruch();
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x00110464 File Offset: 0x0010E664
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x00110498 File Offset: 0x0010E698
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

	// Token: 0x06001B4D RID: 6989 RVA: 0x00110584 File Offset: 0x0010E784
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

	// Token: 0x06001B4E RID: 6990 RVA: 0x001105CE File Offset: 0x0010E7CE
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x001105EA File Offset: 0x0010E7EA
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[39];
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x001105FA File Offset: 0x0010E7FA
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

	// Token: 0x06001B51 RID: 6993 RVA: 0x00110638 File Offset: 0x0010E838
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

	// Token: 0x06001B52 RID: 6994 RVA: 0x00110728 File Offset: 0x0010E928
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

	// Token: 0x06001B53 RID: 6995 RVA: 0x001107A8 File Offset: 0x0010E9A8
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

	// Token: 0x06001B54 RID: 6996 RVA: 0x00110807 File Offset: 0x0010EA07
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.devCosts * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x00110828 File Offset: 0x0010EA28
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

	// Token: 0x0400225D RID: 8797
	public int myID = -1;

	// Token: 0x0400225E RID: 8798
	public int targetID = -1;

	// Token: 0x0400225F RID: 8799
	public float points;

	// Token: 0x04002260 RID: 8800
	public float pointsLeft;

	// Token: 0x04002261 RID: 8801
	public float quality;

	// Token: 0x04002262 RID: 8802
	public int devCosts;

	// Token: 0x04002263 RID: 8803
	public bool automatic;

	// Token: 0x04002264 RID: 8804
	private GameObject main_;

	// Token: 0x04002265 RID: 8805
	private mainScript mS_;

	// Token: 0x04002266 RID: 8806
	private GUI_Main guiMain_;

	// Token: 0x04002267 RID: 8807
	private textScript tS_;

	// Token: 0x04002268 RID: 8808
	private roomDataScript rdS_;

	// Token: 0x04002269 RID: 8809
	public gameScript gS_;
}
