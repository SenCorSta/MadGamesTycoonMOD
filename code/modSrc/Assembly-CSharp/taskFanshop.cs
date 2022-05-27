using System;
using UnityEngine;


public class taskFanshop : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(270f, 0f, 0f);
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
		if (!this.menuFanshop_)
		{
			this.menuFanshop_ = this.guiMain_.uiObjects[367].GetComponent<Menu_Fanshop>();
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
		return 0f;
	}

	
	public void Work(int artikel, int amount, int v)
	{
		if (!this.mS_)
		{
			return;
		}
		this.bestellungen[artikel] += amount;
		this.verdienst += v;
	}

	
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void ResetData()
	{
		this.verdienst = 0;
		for (int i = 0; i < this.bestellungen.Length; i++)
		{
			this.bestellungen[i] = 0;
		}
	}

	
	public int myID = -1;

	
	public int[] bestellungen;

	
	public int verdienst;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	public Menu_Fanshop menuFanshop_;
}
