using System;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class taskBugfixing : MonoBehaviour
{
	// Token: 0x06001B14 RID: 6932 RVA: 0x0010F039 File Offset: 0x0010D239
	private void Awake()
	{
		base.transform.position = new Vector3(120f, 0f, 0f);
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x0010F05A File Offset: 0x0010D25A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x0010F064 File Offset: 0x0010D264
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

	// Token: 0x06001B17 RID: 6935 RVA: 0x0010F10A File Offset: 0x0010D30A
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x0010F11E File Offset: 0x0010D31E
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x0010F140 File Offset: 0x0010D340
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x0010F174 File Offset: 0x0010D374
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		if (!this.gS_)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
			if (gameObject)
			{
				this.gS_ = gameObject.GetComponent<gameScript>();
			}
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x0010F1E0 File Offset: 0x0010D3E0
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

	// Token: 0x06001B1C RID: 6940 RVA: 0x0010F2E5 File Offset: 0x0010D4E5
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x0010EE61 File Offset: 0x0010D061
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x0010F304 File Offset: 0x0010D504
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= f;
			if (this.pointsLeft <= 0f)
			{
				this.FindMyObject();
				this.pointsLeft = this.points;
				if (this.gS_)
				{
					this.gS_.points_bugs -= 1f;
					if (this.gS_.points_bugs < 0f)
					{
						this.gS_.points_bugs = 0f;
					}
				}
			}
		}
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x00002715 File Offset: 0x00000915
	private void Complete()
	{
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002234 RID: 8756
	public int myID = -1;

	// Token: 0x04002235 RID: 8757
	public int targetID = -1;

	// Token: 0x04002236 RID: 8758
	public float points;

	// Token: 0x04002237 RID: 8759
	public float pointsLeft;

	// Token: 0x04002238 RID: 8760
	private GameObject main_;

	// Token: 0x04002239 RID: 8761
	public mainScript mS_;

	// Token: 0x0400223A RID: 8762
	private GUI_Main guiMain_;

	// Token: 0x0400223B RID: 8763
	private textScript tS_;

	// Token: 0x0400223C RID: 8764
	private roomDataScript rdS_;

	// Token: 0x0400223D RID: 8765
	public gameScript gS_;

	// Token: 0x0400223E RID: 8766
	public roomScript rS_;

	// Token: 0x0400223F RID: 8767
	private float findMyRoomTimer;
}
