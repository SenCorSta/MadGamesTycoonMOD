using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class taskContractWait : MonoBehaviour
{
	// Token: 0x06001AD8 RID: 6872 RVA: 0x00012124 File Offset: 0x00010324
	private void Awake()
	{
		base.transform.position = new Vector3(220f, 0f, 0f);
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x00012145 File Offset: 0x00010345
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x00112C48 File Offset: 0x00110E48
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

	// Token: 0x06001ADB RID: 6875 RVA: 0x0001214D File Offset: 0x0001034D
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0001217E File Offset: 0x0001037E
	private void Update()
	{
		this.AutomaticWait(this.art);
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0001218C File Offset: 0x0001038C
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[10];
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x00112CF0 File Offset: 0x00110EF0
	private void AutomaticWait(int art_)
	{
		if (art_ == -1)
		{
			return;
		}
		this.waitTimer += Time.deltaTime;
		if (this.waitTimer < 5f)
		{
			return;
		}
		this.waitTimer = 0f;
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				contractWork component = array[i].GetComponent<contractWork>();
				if (component && component.art == art_ && !component.IsAngenommen())
				{
					component.angenommen = true;
					taskContractWork taskContractWork = this.guiMain_.AddTask_ContractWork();
					taskContractWork.Init(false);
					taskContractWork.contractID = component.myID;
					taskContractWork.points = component.GetArbeitsaufwand();
					taskContractWork.pointsLeft = component.GetArbeitsaufwand();
					taskContractWork.automatic = true;
					taskContractWork.automaticWait = true;
					for (int j = 0; j < this.mS_.arrayRooms.Length; j++)
					{
						if (this.mS_.arrayRooms[j])
						{
							roomScript component2 = this.mS_.arrayRooms[j].GetComponent<roomScript>();
							if (component2 && component2.taskID == this.myID)
							{
								component2.taskID = taskContractWork.myID;
							}
						}
					}
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
			}
		}
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002226 RID: 8742
	public int myID = -1;

	// Token: 0x04002227 RID: 8743
	public int art = -1;

	// Token: 0x04002228 RID: 8744
	private float waitTimer = 10f;

	// Token: 0x04002229 RID: 8745
	private GameObject main_;

	// Token: 0x0400222A RID: 8746
	public mainScript mS_;

	// Token: 0x0400222B RID: 8747
	private GUI_Main guiMain_;

	// Token: 0x0400222C RID: 8748
	private textScript tS_;

	// Token: 0x0400222D RID: 8749
	private roomDataScript rdS_;
}
