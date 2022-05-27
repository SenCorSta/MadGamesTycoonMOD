using System;
using UnityEngine;


public class taskTraining : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(90f, 0f, 0f);
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

	
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.slot == -1)
		{
			return null;
		}
		return this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingSprites[this.slot];
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
		this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		string text = this.tS_.GetText(566);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.tS_.GetText(538 + this.slot) + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[13]);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		Menu_Training_Select component = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		if (this.mS_.money < (long)component.trainingCosts[this.slot])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[13]);
			return false;
		}
		this.mS_.Pay((long)component.trainingCosts[this.slot], 13);
		this.pointsLeft = this.points;
		return true;
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
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().trainingCosts[this.slot] * ((100f - this.GetProzent()) * 0.01f));
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

	
	public int slot = -1;

	
	public bool automatic;

	
	public float points;

	
	public float pointsLeft;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;
}
