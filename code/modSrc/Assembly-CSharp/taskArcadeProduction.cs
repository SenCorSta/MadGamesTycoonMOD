using System;
using UnityEngine;


public class taskArcadeProduction : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(170f, 0f, 0f);
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
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
		this.FindMyRoom();
		this.IsGameFromMarket();
	}

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
	private void IsGameFromMarket()
	{
		if (this.gS_ && !this.gS_.isOnMarket)
		{
			this.Abbrechen();
		}
	}

	
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
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	
	private void FindMyRoom()
	{
		if (!this.gS_)
		{
			return;
		}
		if (!this.mS_)
		{
			return;
		}
		this.findMyRoomTimer += Time.deltaTime;
		if (this.findMyRoomTimer < 0.2f)
		{
			return;
		}
		this.findMyRoomTimer = 0f;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				this.rS_ = component;
				return;
			}
		}
	}

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		return null;
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
		this.pointsLeft = this.points;
		if (!this.gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		int num = 50;
		if (num > this.gS_.vorbestellungen)
		{
			num = this.gS_.vorbestellungen;
		}
		if (num > 0)
		{
			int num2 = this.gS_.verkaufspreis[0] * num;
			int num3 = this.gS_.arcadeProdCosts * num;
			this.gS_.vorbestellungen -= num;
			this.gS_.sellsTotal += (long)num;
			this.gS_.umsatzTotal += (long)num2;
			this.gS_.costs_production += (long)num3;
			this.mS_.Earn((long)num2, 3);
			this.mS_.Pay((long)num3, 21);
			this.gS_.PlayerPayEngineLicence((long)num2);
			if (this.rS_)
			{
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num2 - num3, this.rS_.uiPos, true));
			}
		}
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

	
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int targetID = -1;

	
	public float points;

	
	public float pointsLeft;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public gameScript gS_;

	
	private games games_;

	
	public roomScript rS_;

	
	private float findMyRoomTimer;
}
