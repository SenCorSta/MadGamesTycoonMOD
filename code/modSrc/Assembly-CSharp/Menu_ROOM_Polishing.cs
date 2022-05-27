using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_ROOM_Polishing : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Polishing>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	
	public void Init(roomScript script_)
	{
		this.rS_ = script_;
		this.FindScripts();
		this.SetData();
	}

	
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && component.inDevelopment && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Polishing component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Polishing>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.rS_ = this.rS_;
				}
			}
		}
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void StartPolishing(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		taskPolishing taskPolishing = this.guiMain_.AddTask_Polishing();
		taskPolishing.Init(false);
		taskPolishing.targetID = gS_.myID;
		taskPolishing.points = 200f;
		taskPolishing.pointsLeft = taskPolishing.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskGameObject = null;
			gameObject.GetComponent<roomScript>().taskID = taskPolishing.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void StartPolishingAutomatic(gameScript gS_, int taskID)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			this.FindScripts();
		}
		taskPolishing taskPolishing = this.guiMain_.AddTask_Polishing();
		taskPolishing.Init(false);
		taskPolishing.targetID = gS_.myID;
		taskPolishing.points = 200f;
		taskPolishing.pointsLeft = taskPolishing.points;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component && component.taskID == taskID)
				{
					component.taskGameObject = taskPolishing.gameObject;
					component.taskID = taskPolishing.myID;
					return;
				}
			}
		}
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private float updateTimer;
}
