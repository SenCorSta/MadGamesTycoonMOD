using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class taskArcadeProduction : MonoBehaviour
{
	// Token: 0x06001B05 RID: 6917 RVA: 0x0010EC0C File Offset: 0x0010CE0C
	private void Awake()
	{
		base.transform.position = new Vector3(170f, 0f, 0f);
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x0010EC2D File Offset: 0x0010CE2D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x0010EC38 File Offset: 0x0010CE38
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

	// Token: 0x06001B08 RID: 6920 RVA: 0x0010ECFC File Offset: 0x0010CEFC
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.IsGameFromMarket();
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0010ED10 File Offset: 0x0010CF10
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0010ED41 File Offset: 0x0010CF41
	private void IsGameFromMarket()
	{
		if (this.gS_ && !this.gS_.isOnMarket)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x0010ED64 File Offset: 0x0010CF64
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

	// Token: 0x06001B0C RID: 6924 RVA: 0x0010EDC4 File Offset: 0x0010CFC4
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

	// Token: 0x06001B0D RID: 6925 RVA: 0x0010EE61 File Offset: 0x0010D061
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x0010EE7D File Offset: 0x0010D07D
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x0010EE80 File Offset: 0x0010D080
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

	// Token: 0x06001B10 RID: 6928 RVA: 0x0010EEBC File Offset: 0x0010D0BC
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

	// Token: 0x06001B11 RID: 6929 RVA: 0x0010EFE0 File Offset: 0x0010D1E0
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

	// Token: 0x06001B12 RID: 6930 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002227 RID: 8743
	public int myID = -1;

	// Token: 0x04002228 RID: 8744
	public int targetID = -1;

	// Token: 0x04002229 RID: 8745
	public float points;

	// Token: 0x0400222A RID: 8746
	public float pointsLeft;

	// Token: 0x0400222B RID: 8747
	private GameObject main_;

	// Token: 0x0400222C RID: 8748
	public mainScript mS_;

	// Token: 0x0400222D RID: 8749
	private GUI_Main guiMain_;

	// Token: 0x0400222E RID: 8750
	private textScript tS_;

	// Token: 0x0400222F RID: 8751
	private roomDataScript rdS_;

	// Token: 0x04002230 RID: 8752
	public gameScript gS_;

	// Token: 0x04002231 RID: 8753
	private games games_;

	// Token: 0x04002232 RID: 8754
	public roomScript rS_;

	// Token: 0x04002233 RID: 8755
	private float findMyRoomTimer;
}
