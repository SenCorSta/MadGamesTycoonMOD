using System;
using UnityEngine;


public class taskContractWait : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(220f, 0f, 0f);
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
		return this.guiMain_.uiSprites[10];
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
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				contractWork component = array[i].GetComponent<contractWork>();
				if (component && component.art == art_ && !component.IsAngenommen())
				{
					component.angenommen = true;
					taskContractWork taskContractWork = this.guiMain_.AddTask_ContractWork();
					taskContractWork.Init(false);
					taskContractWork.contractID = component.myID;
					taskContractWork.points = component.GetArbeitsaufwand();
					taskContractWork.pointsLeft = component.GetArbeitsaufwand();
					taskContractWork.automatic = true;
					taskContractWork.automaticWait = true;
					for (int j = 0; j < this.mS_.arrayRooms.Length; j++)
					{
						if (this.mS_.arrayRooms[j])
						{
							roomScript component2 = this.mS_.arrayRooms[j].GetComponent<roomScript>();
							if (component2 && component2.taskID == this.myID)
							{
								component2.taskID = taskContractWork.myID;
							}
						}
					}
					UnityEngine.Object.Destroy(base.gameObject);
					return;
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
