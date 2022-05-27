using System;
using UnityEngine;


public class taskSupport : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(200f, 0f, 0f);
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
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return this.mS_.GetAnrufe100Prozent();
	}

	
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[0];
	}

	
	public void Work(float f)
	{
		if (!this.mS_)
		{
			return;
		}
		if (this.mS_.anrufe > 0)
		{
			this.mS_.anrufe -= 15 + Mathf.RoundToInt(f * 1.5f);
			if (this.mS_.anrufe <= 0)
			{
				this.mS_.anrufe = 0;
			}
		}
	}

	
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	private GameObject main_;

	
	public mainScript mS_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;
}
