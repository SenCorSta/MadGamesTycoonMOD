using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class taskFanshop : MonoBehaviour
{
	// Token: 0x06001B1A RID: 6938 RVA: 0x000125B6 File Offset: 0x000107B6
	private void Awake()
	{
		base.transform.position = new Vector3(270f, 0f, 0f);
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x000125D7 File Offset: 0x000107D7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x00114194 File Offset: 0x00112394
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

	// Token: 0x06001B1D RID: 6941 RVA: 0x000125DF File Offset: 0x000107DF
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x00012610 File Offset: 0x00010810
	public float GetProzent()
	{
		return 0f;
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x00012617 File Offset: 0x00010817
	public void Work(int artikel, int amount, int v)
	{
		if (!this.mS_)
		{
			return;
		}
		this.bestellungen[artikel] += amount;
		this.verdienst += v;
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x00114264 File Offset: 0x00112464
	public void ResetData()
	{
		this.verdienst = 0;
		for (int i = 0; i < this.bestellungen.Length; i++)
		{
			this.bestellungen[i] = 0;
		}
	}

	// Token: 0x0400225D RID: 8797
	public int myID = -1;

	// Token: 0x0400225E RID: 8798
	public int[] bestellungen;

	// Token: 0x0400225F RID: 8799
	public int verdienst;

	// Token: 0x04002260 RID: 8800
	private GameObject main_;

	// Token: 0x04002261 RID: 8801
	public mainScript mS_;

	// Token: 0x04002262 RID: 8802
	private GUI_Main guiMain_;

	// Token: 0x04002263 RID: 8803
	private textScript tS_;

	// Token: 0x04002264 RID: 8804
	private roomDataScript rdS_;

	// Token: 0x04002265 RID: 8805
	public Menu_Fanshop menuFanshop_;
}
