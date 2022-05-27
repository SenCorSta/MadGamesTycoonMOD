using System;
using UnityEngine;


public class contractWork : MonoBehaviour
{
	
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
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	
	public void Init()
	{
		base.name = "CONTRACTWORK_" + this.myID.ToString();
	}

	
	public string GetName()
	{
		if (this.art == 6)
		{
			return this.tS_.GetText(1560);
		}
		if (this.art == 5)
		{
			return this.tS_.GetText(1130);
		}
		return this.tS_.GetContractWork(this.typ);
	}

	
	public int GetGehalt()
	{
		return this.gehalt;
	}

	
	public int GetStrafe()
	{
		return this.strafe;
	}

	
	public int GetWochen()
	{
		return this.zeitInWochen;
	}

	
	public float GetArbeitsaufwand()
	{
		return this.points;
	}

	
	public float GetAuftragsansehen()
	{
		if (this.art == 5)
		{
			return 0.1f;
		}
		return this.points * 0.001f;
	}

	
	public string GetTooltip()
	{
		return "<b>" + this.GetName() + "</b>";
	}

	
	public bool IsAngenommen()
	{
		return this.angenommen;
	}

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public int myID;

	
	public bool angenommen;

	
	public int typ;

	
	public int gehalt;

	
	public int strafe;

	
	public int auftraggeberID = -1;

	
	public int zeitInWochen;

	
	public int wochenAlsAngebot;

	
	public float points;

	
	public int art;
}
