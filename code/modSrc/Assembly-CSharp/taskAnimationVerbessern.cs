using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class taskAnimationVerbessern : MonoBehaviour
{
	// Token: 0x06001AF4 RID: 6900 RVA: 0x0010E530 File Offset: 0x0010C730
	private void Awake()
	{
		base.transform.position = new Vector3(150f, 0f, 0f);
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0010E551 File Offset: 0x0010C751
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0010E55C File Offset: 0x0010C75C
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
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
		if (!this.menuMOCAP_)
		{
			this.menuMOCAP_ = this.guiMain_.uiObjects[178].GetComponent<Menu_MOCAP_AnimationVerbessern>();
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0010E649 File Offset: 0x0010C849
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0010E65D File Offset: 0x0010C85D
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x0010E67F File Offset: 0x0010C87F
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x0010E6B0 File Offset: 0x0010C8B0
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
		if (gameObject)
		{
			this.gS_ = gameObject.GetComponent<gameScript>();
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0010E710 File Offset: 0x0010C910
	private void FindMyRoom()
	{
		if (!this.gS_)
		{
			return;
		}
		this.findMyRoomTimer += Time.deltaTime;
		if (this.findMyRoomTimer < 0.2f)
		{
			return;
		}
		this.findMyRoomTimer = 0f;
		if (this.rS_ && this.rS_.taskID != -1)
		{
			GameObject taskGameObject = this.rS_.taskGameObject;
			if (taskGameObject)
			{
				taskGame component = taskGameObject.GetComponent<taskGame>();
				if (component && component.gameID == this.targetID)
				{
					return;
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component2 = array[i].GetComponent<roomScript>();
			if (component2 && component2.taskID != -1)
			{
				GameObject taskGameObject2 = component2.taskGameObject;
				if (taskGameObject2)
				{
					taskGame component3 = taskGameObject2.GetComponent<taskGame>();
					if (component3 && component3.gameID == this.targetID)
					{
						this.rS_ = component2;
						return;
					}
				}
			}
		}
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x0010E815 File Offset: 0x0010CA15
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001AFD RID: 6909 RVA: 0x0010E831 File Offset: 0x0010CA31
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 18];
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0010E848 File Offset: 0x0010CA48
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.gS_)
			{
				this.gS_.points_technik += f;
			}
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x0010E8B4 File Offset: 0x0010CAB4
	public void FindNewAdd()
	{
		this.FindScripts();
		this.FindMyObject();
		this.aktuellerAdd = -1;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				this.aktuellerAdd = i;
				break;
			}
		}
		if (this.aktuellerAdd != -1)
		{
			float num = (float)this.gS_.GetGesamtDevPoints();
			this.points = num * this.menuMOCAP_.pointsInPercent[this.aktuellerAdd];
			this.pointsLeft = this.points;
			return;
		}
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(this.gS_, this.myID);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x0010E96C File Offset: 0x0010CB6C
	private void Complete()
	{
		this.FindMyObject();
		this.adds[this.aktuellerAdd] = false;
		this.gS_.motionCaptureStudio[this.aktuellerAdd] = true;
		this.gS_.costs_entwicklung += (long)this.menuMOCAP_.GetCosts(this.aktuellerAdd);
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		string text = this.tS_.GetText(923);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[10]);
		this.FindNewAdd();
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x0010EA6C File Offset: 0x0010CC6C
	private void LeftNews(string c, Sprite icon, Sprite iconRoom)
	{
		int roomID_ = -1;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				roomID_ = component.myID;
				break;
			}
		}
		this.guiMain_.CreateLeftNews(roomID_, icon, c, iconRoom);
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x0010EACC File Offset: 0x0010CCCC
	public int GetRueckgeld()
	{
		float num = 0f;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				num += (float)this.menuMOCAP_.GetCosts(i);
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x0010EB14 File Offset: 0x0010CD14
	public void Abbrechen()
	{
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)Mathf.RoundToInt((float)rueckgeld), 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.RoundToInt((float)rueckgeld), new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002217 RID: 8727
	public int myID = -1;

	// Token: 0x04002218 RID: 8728
	public int targetID = -1;

	// Token: 0x04002219 RID: 8729
	public float points;

	// Token: 0x0400221A RID: 8730
	public float pointsLeft;

	// Token: 0x0400221B RID: 8731
	public bool[] adds = new bool[6];

	// Token: 0x0400221C RID: 8732
	public int aktuellerAdd = -1;

	// Token: 0x0400221D RID: 8733
	private GameObject main_;

	// Token: 0x0400221E RID: 8734
	public mainScript mS_;

	// Token: 0x0400221F RID: 8735
	private GUI_Main guiMain_;

	// Token: 0x04002220 RID: 8736
	private textScript tS_;

	// Token: 0x04002221 RID: 8737
	private roomDataScript rdS_;

	// Token: 0x04002222 RID: 8738
	public gameScript gS_;

	// Token: 0x04002223 RID: 8739
	private Menu_MOCAP_AnimationVerbessern menuMOCAP_;

	// Token: 0x04002224 RID: 8740
	private games games_;

	// Token: 0x04002225 RID: 8741
	public roomScript rS_;

	// Token: 0x04002226 RID: 8742
	private float findMyRoomTimer;
}
