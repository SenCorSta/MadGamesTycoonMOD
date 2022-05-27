using System;
using UnityEngine;


public class contractAuftragsspiel : MonoBehaviour
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
		base.name = "CONTRACTGAME_" + this.myID.ToString();
	}

	
	public string GetName()
	{
		return this.gameName;
	}

	
	public int GetGehalt()
	{
		return this.gehalt;
	}

	
	public int GetBonus()
	{
		return this.bonus;
	}

	
	public int GetWochen()
	{
		return this.zeitInWochen;
	}

	
	public float GetAuftragsansehen()
	{
		return (float)this.mindestbewertung * 0.01f;
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

	
	public int gehalt;

	
	public int bonus;

	
	public int auftraggeberID = -1;

	
	public int zeitInWochen;

	
	public int wochenAlsAngebot;

	
	public bool zeitAbgelaufen;

	
	public int mindestbewertung;

	
	public string gameName = "";

	
	public int genre;

	
	public int gameSize;

	
	public int platform;
}
