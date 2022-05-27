using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class taskContractWait : MonoBehaviour
{
	// Token: 0x06001B22 RID: 6946 RVA: 0x0010F3C3 File Offset: 0x0010D5C3
	private void Awake()
	{
		base.transform.position = new Vector3(220f, 0f, 0f);
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x0010F3E4 File Offset: 0x0010D5E4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x0010F3EC File Offset: 0x0010D5EC
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

	// Token: 0x06001B25 RID: 6949 RVA: 0x0010F492 File Offset: 0x0010D692
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x0010F4C3 File Offset: 0x0010D6C3
	private void Update()
	{
		this.AutomaticWait(this.art);
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x0010F4D1 File Offset: 0x0010D6D1
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[10];
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x0010F4E4 File Offset: 0x0010D6E4
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

	// Token: 0x06001B29 RID: 6953 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002240 RID: 8768
	public int myID = -1;

	// Token: 0x04002241 RID: 8769
	public int art = -1;

	// Token: 0x04002242 RID: 8770
	private float waitTimer = 10f;

	// Token: 0x04002243 RID: 8771
	private GameObject main_;

	// Token: 0x04002244 RID: 8772
	public mainScript mS_;

	// Token: 0x04002245 RID: 8773
	private GUI_Main guiMain_;

	// Token: 0x04002246 RID: 8774
	private textScript tS_;

	// Token: 0x04002247 RID: 8775
	private roomDataScript rdS_;
}
