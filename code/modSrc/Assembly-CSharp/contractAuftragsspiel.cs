using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class contractAuftragsspiel : MonoBehaviour
{
	// Token: 0x0600018F RID: 399 RVA: 0x00002E67 File Offset: 0x00001067
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0002EA38 File Offset: 0x0002CC38
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

	// Token: 0x06000191 RID: 401 RVA: 0x00002E6F File Offset: 0x0000106F
	public void Init()
	{
		base.name = "CONTRACTGAME_" + this.myID.ToString();
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00002E8C File Offset: 0x0000108C
	public string GetName()
	{
		return this.gameName;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00002E94 File Offset: 0x00001094
	public int GetGehalt()
	{
		return this.gehalt;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00002E9C File Offset: 0x0000109C
	public int GetBonus()
	{
		return this.bonus;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00002EA4 File Offset: 0x000010A4
	public int GetWochen()
	{
		return this.zeitInWochen;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00002EAC File Offset: 0x000010AC
	public float GetAuftragsansehen()
	{
		return (float)this.mindestbewertung * 0.01f;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00002EBB File Offset: 0x000010BB
	public string GetTooltip()
	{
		return "<b>" + this.GetName() + "</b>";
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00002ED2 File Offset: 0x000010D2
	public bool IsAngenommen()
	{
		return this.angenommen;
	}

	// Token: 0x04000386 RID: 902
	public GameObject main_;

	// Token: 0x04000387 RID: 903
	public mainScript mS_;

	// Token: 0x04000388 RID: 904
	public textScript tS_;

	// Token: 0x04000389 RID: 905
	public int myID;

	// Token: 0x0400038A RID: 906
	public bool angenommen;

	// Token: 0x0400038B RID: 907
	public int gehalt;

	// Token: 0x0400038C RID: 908
	public int bonus;

	// Token: 0x0400038D RID: 909
	public int auftraggeberID = -1;

	// Token: 0x0400038E RID: 910
	public int zeitInWochen;

	// Token: 0x0400038F RID: 911
	public int wochenAlsAngebot;

	// Token: 0x04000390 RID: 912
	public bool zeitAbgelaufen;

	// Token: 0x04000391 RID: 913
	public int mindestbewertung;

	// Token: 0x04000392 RID: 914
	public string gameName = "";

	// Token: 0x04000393 RID: 915
	public int genre;

	// Token: 0x04000394 RID: 916
	public int gameSize;

	// Token: 0x04000395 RID: 917
	public int platform;
}
