using System;
using UnityEngine;


public class taskPolishing : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(240f, 0f, 0f);
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
		this.FindMyRoom();
		this.GamePublished();
	}

	
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
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

	
	private void FindMyRoom()
	{
		if (!this.gS_)
		{
			return;
		}
		this.findMyRoomTimer += Time.deltaTime;
		if (this.findMyRoomTimer < 0.2f)
		{
			return;
		}
		this.findMyRoomTimer = 0f;
		if (this.rS_ && this.rS_.taskID != -1)
		{
			GameObject taskGameObject = this.rS_.taskGameObject;
			if (taskGameObject)
			{
				taskGame component = taskGameObject.GetComponent<taskGame>();
				if (component && component.gameID == this.targetID)
				{
					return;
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component2 = array[i].GetComponent<roomScript>();
			if (component2 && component2.taskID != -1)
			{
				GameObject taskGameObject2 = component2.taskGameObject;
				if (taskGameObject2)
				{
					taskGame component3 = taskGameObject2.GetComponent<taskGame>();
					if (component3 && component3.gameID == this.targetID)
					{
						this.rS_ = component2;
						return;
					}
				}
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

	
	public void Work(float f, roomScript myRoomS_)
	{
		if (this.gS_)
		{
			if (myRoomS_.typ == 4)
			{
				this.gS_.points_grafik += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 5)
			{
				this.gS_.points_sound += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 3)
			{
				this.gS_.points_gameplay += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 10)
			{
				this.gS_.points_technik += f;
				this.RemoveInvisBug();
			}
		}
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.pointsLeft <= 0f)
			{
				this.FindMyObject();
				this.pointsLeft = this.points;
				this.Complete(myRoomS_);
			}
		}
	}

	
	private void RemoveInvisBug()
	{
		if (UnityEngine.Random.Range(0, 100) < 90)
		{
			return;
		}
		this.gS_.points_bugsInvis -= 1f;
		if (this.gS_.points_bugsInvis < 0f)
		{
			this.gS_.points_bugsInvis = 0f;
		}
	}

	
	private void Complete(roomScript myRoomS_)
	{
		if (this.gS_)
		{
			if (myRoomS_.typ == 4)
			{
				this.gS_.points_grafik += 10f;
			}
			if (myRoomS_.typ == 5)
			{
				this.gS_.points_sound += 10f;
			}
			if (myRoomS_.typ == 3)
			{
				this.gS_.points_gameplay += 10f;
			}
			if (myRoomS_.typ == 10)
			{
				this.gS_.points_technik += 10f;
			}
		}
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

	
	public roomScript rS_;

	
	private float findMyRoomTimer;
}
