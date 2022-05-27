using System;
using UnityEngine;


public class taskWait : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(260f, 0f, 0f);
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

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
	private void Update()
	{
		this.AutomaticWait(this.art);
	}

	
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.art == 0)
		{
			return this.guiMain_.uiSprites[18];
		}
		return null;
	}

	
	private void AutomaticWait(int art_)
	{
		if (art_ == -1)
		{
			return;
		}
		this.waitTimer += Time.deltaTime;
		if (this.waitTimer < 5f)
		{
			return;
		}
		this.waitTimer = 0f;
		if (this.art == 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					gameScript component = array[i].GetComponent<gameScript>();
					if (component && this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().CheckGameData(component))
					{
						taskSpielbericht taskSpielbericht = this.guiMain_.AddTask_Spielbericht();
						taskSpielbericht.Init(false);
						taskSpielbericht.targetID = component.myID;
						taskSpielbericht.automatic = true;
						taskSpielbericht.automaticWait = true;
						taskSpielbericht.points = (float)this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().GetWorkPoints(component);
						taskSpielbericht.pointsLeft = taskSpielbericht.points;
						for (int j = 0; j < this.mS_.arrayRooms.Length; j++)
						{
							if (this.mS_.arrayRooms[j])
							{
								roomScript component2 = this.mS_.arrayRooms[j].GetComponent<roomScript>();
								if (component2 && component2.taskID == this.myID)
								{
									component2.taskID = taskSpielbericht.myID;
								}
							}
						}
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
				}
			}
		}
	}

	
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int art = -1;

	
	private float waitTimer = 10f;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;
}
