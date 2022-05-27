using System;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class taskUnterstuetzen : MonoBehaviour
{
	// Token: 0x06001BFD RID: 7165 RVA: 0x000133D3 File Offset: 0x000115D3
	private void Awake()
	{
		base.transform.position = new Vector3(250f, 0f, 0f);
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000133F4 File Offset: 0x000115F4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x00119EEC File Offset: 0x001180EC
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

	// Token: 0x06001C00 RID: 7168 RVA: 0x000133FC File Offset: 0x000115FC
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x0001342D File Offset: 0x0001162D
	private void Update()
	{
		this.FindMyRoom();
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x00119FF0 File Offset: 0x001181F0
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

	// Token: 0x06001C03 RID: 7171 RVA: 0x00013435 File Offset: 0x00011635
	public bool IsCrunchtime()
	{
		return this.rS_ && this.rS_.IsCrunchtimeRead();
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x00002098 File Offset: 0x00000298
	public void Work(float f, int what)
	{
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x00002098 File Offset: 0x00000298
	private void CompleteFeature()
	{
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x00002098 File Offset: 0x00000298
	private void Complete()
	{
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00013451 File Offset: 0x00011651
	public void Abbrechen()
	{
		this.FindScripts();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400232E RID: 9006
	public int myID = -1;

	// Token: 0x0400232F RID: 9007
	public int roomID = -1;

	// Token: 0x04002330 RID: 9008
	public roomScript rS_;

	// Token: 0x04002331 RID: 9009
	private GameObject main_;

	// Token: 0x04002332 RID: 9010
	private mainScript mS_;

	// Token: 0x04002333 RID: 9011
	private engineFeatures eF_;

	// Token: 0x04002334 RID: 9012
	private gameplayFeatures gF_;

	// Token: 0x04002335 RID: 9013
	private GUI_Main guiMain_;

	// Token: 0x04002336 RID: 9014
	private textScript tS_;

	// Token: 0x04002337 RID: 9015
	private roomDataScript rdS_;

	// Token: 0x04002338 RID: 9016
	private sfxScript sfx_;
}
