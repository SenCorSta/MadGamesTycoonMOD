using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class taskArcadeProduction : MonoBehaviour
{
	// Token: 0x06001ABB RID: 6843 RVA: 0x00011F62 File Offset: 0x00010162
	private void Awake()
	{
		base.transform.position = new Vector3(170f, 0f, 0f);
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x00011F83 File Offset: 0x00010183
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x00112654 File Offset: 0x00110854
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
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x00011F8B File Offset: 0x0001018B
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.IsGameFromMarket();
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x00011F9F File Offset: 0x0001019F
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x00011FD0 File Offset: 0x000101D0
	private void IsGameFromMarket()
	{
		if (this.gS_ && !this.gS_.isOnMarket)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x00112718 File Offset: 0x00110918
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

	// Token: 0x06001AC2 RID: 6850 RVA: 0x00112778 File Offset: 0x00110978
	private void FindMyRoom()
	{
		if (!this.gS_)
		{
			return;
		}
		if (!this.mS_)
		{
			return;
		}
		this.findMyRoomTimer += Time.deltaTime;
		if (this.findMyRoomTimer < 0.2f)
		{
			return;
		}
		this.findMyRoomTimer = 0f;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
			if (component && component.taskID == this.myID)
			{
				this.rS_ = component;
				return;
			}
		}
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x00011FF2 File Offset: 0x000101F2
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0001200E File Offset: 0x0001020E
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x00012011 File Offset: 0x00010211
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= f;
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x00112818 File Offset: 0x00110A18
	private void Complete()
	{
		this.FindMyObject();
		this.pointsLeft = this.points;
		if (!this.gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		int num = 50;
		if (num > this.gS_.vorbestellungen)
		{
			num = this.gS_.vorbestellungen;
		}
		if (num > 0)
		{
			int num2 = this.gS_.verkaufspreis[0] * num;
			int num3 = this.gS_.arcadeProdCosts * num;
			this.gS_.vorbestellungen -= num;
			this.gS_.sellsTotal += (long)num;
			this.gS_.umsatzTotal += (long)num2;
			this.gS_.costs_production += (long)num3;
			this.mS_.Earn((long)num2, 3);
			this.mS_.Pay((long)num3, 21);
			this.gS_.PlayerPayEngineLicence((long)num2);
			if (this.rS_)
			{
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num2 - num3, this.rS_.uiPos, true));
			}
		}
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0011293C File Offset: 0x00110B3C
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

	// Token: 0x06001AC8 RID: 6856 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400220D RID: 8717
	public int myID = -1;

	// Token: 0x0400220E RID: 8718
	public int targetID = -1;

	// Token: 0x0400220F RID: 8719
	public float points;

	// Token: 0x04002210 RID: 8720
	public float pointsLeft;

	// Token: 0x04002211 RID: 8721
	private GameObject main_;

	// Token: 0x04002212 RID: 8722
	public mainScript mS_;

	// Token: 0x04002213 RID: 8723
	private GUI_Main guiMain_;

	// Token: 0x04002214 RID: 8724
	private textScript tS_;

	// Token: 0x04002215 RID: 8725
	private roomDataScript rdS_;

	// Token: 0x04002216 RID: 8726
	public gameScript gS_;

	// Token: 0x04002217 RID: 8727
	private games games_;

	// Token: 0x04002218 RID: 8728
	public roomScript rS_;

	// Token: 0x04002219 RID: 8729
	private float findMyRoomTimer;
}
