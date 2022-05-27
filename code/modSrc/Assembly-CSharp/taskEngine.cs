using System;
using UnityEngine;


public class taskEngine : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(20f, 0f, 0f);
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
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
		this.FindMyEngine();
	}

	
	private void FindMyEngine()
	{
		if (!this.eS_)
		{
			GameObject gameObject = GameObject.Find("ENGINE_" + this.engineID.ToString());
			if (gameObject)
			{
				this.eS_ = gameObject.GetComponent<engineScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			return -1f;
		}
		return this.eS_.GetProzent();
	}

	
	public void Work(float f)
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
		{
			return;
		}
		if (this.eS_.devPoints > 0f)
		{
			this.eS_.devPoints -= f;
			if (this.eS_.devPoints <= 0f)
			{
				this.eS_.devPoints = 0f;
				this.Complete();
			}
		}
	}

	
	private void Complete()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
		{
			return;
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
		string tooltip_ = this.tS_.GetText(284) + "\n<b>" + this.eS_.GetName() + "</b>";
		this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[4], tooltip_, this.rdS_.roomData_SPRITE[1]);
		this.eS_.SetComplete();
		if (this.mS_.achScript_)
		{
			this.mS_.achScript_.SetAchivement(23);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int GetRueckgeld()
	{
		int num = 0;
		for (int i = 0; i < this.eS_.featuresInDev.Length; i++)
		{
			if (this.eS_.featuresInDev[i])
			{
				num += this.eF_.GetDevCostsForEngine(i);
			}
		}
		return num;
	}

	
	public void Abbrechen()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
		{
			return;
		}
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)rueckgeld, 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(rueckgeld, new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		if (!this.eS_.updating)
		{
			UnityEngine.Object.Destroy(this.eS_.gameObject);
		}
		else
		{
			this.eS_.EntwicklungBeenden();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int engineID = -1;

	
	public engineScript eS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private engineFeatures eF_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;
}
