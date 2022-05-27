using System;
using UnityEngine;


public class taskSpielbericht : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(100f, 0f, 0f);
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		this.FindMyObject();
	}

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
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

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[18];
	}

	
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

	
	private void Complete()
	{
		this.FindMyObject();
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		roomScript roomScript = null;
		for (int i = 0; i < array.Length; i++)
		{
			roomScript = array[i].GetComponent<roomScript>();
			if (roomScript && roomScript.taskID == this.myID)
			{
				roomID_ = roomScript.myID;
				break;
			}
		}
		string text = this.tS_.GetText(929);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[3]);
		this.gS_.SetSpielbericht();
		if (!this.DoAutomatic())
		{
			if (this.automatic && this.automaticWait)
			{
				taskWait taskWait = this.guiMain_.AddTask_Wait();
				taskWait.Init(false);
				taskWait.art = 0;
				roomScript.taskID = taskWait.myID;
				Debug.Log("SpielberichtWorkWait");
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private bool DoAutomatic()
	{
		this.FindMyObject();
		if (!this.automatic)
		{
			return false;
		}
		Menu_QA_NewSpielberichtSelectGame component = this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>();
		if (component)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					gameScript component2 = array[i].GetComponent<gameScript>();
					if (component2 && component.CheckGameData(component2))
					{
						this.targetID = component2.myID;
						this.gS_ = component2;
						this.points = (float)component.GetWorkPoints(component2);
						this.pointsLeft = this.points;
						return true;
					}
				}
			}
		}
		return false;
	}

	
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

	
	public int GetRueckgeld()
	{
		return 0;
	}

	
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

	
	public int myID = -1;

	
	public int targetID = -1;

	
	public bool automatic;

	
	public float points;

	
	public float pointsLeft;

	
	public bool automaticWait;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public gameScript gS_;
}
