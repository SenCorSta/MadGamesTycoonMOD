using System;
using UnityEngine;


public class taskUnterstuetzen : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(250f, 0f, 0f);
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
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
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
		this.FindMyRoom();
	}

	
	private void FindMyRoom()
	{
		if (!this.rS_)
		{
			GameObject gameObject = GameObject.Find("Room_" + this.roomID.ToString());
			if (gameObject)
			{
				this.rS_ = gameObject.GetComponent<roomScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public bool IsCrunchtime()
	{
		return this.rS_ && this.rS_.IsCrunchtimeRead();
	}

	
	public void Work(float f, int what)
	{
	}

	
	private void CompleteFeature()
	{
	}

	
	private void Complete()
	{
	}

	
	public void Abbrechen()
	{
		this.FindScripts();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int roomID = -1;

	
	public roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private engineFeatures eF_;

	
	private gameplayFeatures gF_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	private sfxScript sfx_;
}
