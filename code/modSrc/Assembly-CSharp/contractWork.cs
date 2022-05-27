using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class contractWork : MonoBehaviour
{
	// Token: 0x0600019A RID: 410 RVA: 0x00018BF3 File Offset: 0x00016DF3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00018BFC File Offset: 0x00016DFC
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

	// Token: 0x0600019C RID: 412 RVA: 0x00018C62 File Offset: 0x00016E62
	public void Init()
	{
		base.name = "CONTRACTWORK_" + this.myID.ToString();
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00018C80 File Offset: 0x00016E80
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

	// Token: 0x0600019E RID: 414 RVA: 0x00018CD2 File Offset: 0x00016ED2
	public int GetGehalt()
	{
		return this.gehalt;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00018CDA File Offset: 0x00016EDA
	public int GetStrafe()
	{
		return this.strafe;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00018CE2 File Offset: 0x00016EE2
	public int GetWochen()
	{
		return this.zeitInWochen;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00018CEA File Offset: 0x00016EEA
	public float GetArbeitsaufwand()
	{
		return this.points;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00018CF2 File Offset: 0x00016EF2
	public float GetAuftragsansehen()
	{
		if (this.art == 5)
		{
			return 0.1f;
		}
		return this.points * 0.001f;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00018D0F File Offset: 0x00016F0F
	public string GetTooltip()
	{
		return "<b>" + this.GetName() + "</b>";
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00018D26 File Offset: 0x00016F26
	public bool IsAngenommen()
	{
		return this.angenommen;
	}

	// Token: 0x04000396 RID: 918
	public GameObject main_;

	// Token: 0x04000397 RID: 919
	public mainScript mS_;

	// Token: 0x04000398 RID: 920
	public textScript tS_;

	// Token: 0x04000399 RID: 921
	public int myID;

	// Token: 0x0400039A RID: 922
	public bool angenommen;

	// Token: 0x0400039B RID: 923
	public int typ;

	// Token: 0x0400039C RID: 924
	public int gehalt;

	// Token: 0x0400039D RID: 925
	public int strafe;

	// Token: 0x0400039E RID: 926
	public int auftraggeberID = -1;

	// Token: 0x0400039F RID: 927
	public int zeitInWochen;

	// Token: 0x040003A0 RID: 928
	public int wochenAlsAngebot;

	// Token: 0x040003A1 RID: 929
	public float points;

	// Token: 0x040003A2 RID: 930
	public int art;
}
