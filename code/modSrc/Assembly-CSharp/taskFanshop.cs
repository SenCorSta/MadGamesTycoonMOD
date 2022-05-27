using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class taskFanshop : MonoBehaviour
{
	// Token: 0x06001B64 RID: 7012 RVA: 0x00110D9D File Offset: 0x0010EF9D
	private void Awake()
	{
		base.transform.position = new Vector3(270f, 0f, 0f);
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x00110DBE File Offset: 0x0010EFBE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x00110DC8 File Offset: 0x0010EFC8
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

	// Token: 0x06001B67 RID: 7015 RVA: 0x00110E97 File Offset: 0x0010F097
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00110EC8 File Offset: 0x0010F0C8
	public float GetProzent()
	{
		return 0f;
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x00110ECF File Offset: 0x0010F0CF
	public void Work(int artikel, int amount, int v)
	{
		if (!this.mS_)
		{
			return;
		}
		this.bestellungen[artikel] += amount;
		this.verdienst += v;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x00110F00 File Offset: 0x0010F100
	public void ResetData()
	{
		this.verdienst = 0;
		for (int i = 0; i < this.bestellungen.Length; i++)
		{
			this.bestellungen[i] = 0;
		}
	}

	// Token: 0x04002277 RID: 8823
	public int myID = -1;

	// Token: 0x04002278 RID: 8824
	public int[] bestellungen;

	// Token: 0x04002279 RID: 8825
	public int verdienst;

	// Token: 0x0400227A RID: 8826
	private GameObject main_;

	// Token: 0x0400227B RID: 8827
	public mainScript mS_;

	// Token: 0x0400227C RID: 8828
	private GUI_Main guiMain_;

	// Token: 0x0400227D RID: 8829
	private textScript tS_;

	// Token: 0x0400227E RID: 8830
	private roomDataScript rdS_;

	// Token: 0x0400227F RID: 8831
	public Menu_Fanshop menuFanshop_;
}
