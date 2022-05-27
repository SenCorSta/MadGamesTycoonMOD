using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class taskPolishing : MonoBehaviour
{
	// Token: 0x06001BA7 RID: 7079 RVA: 0x00012E70 File Offset: 0x00011070
	private void Awake()
	{
		base.transform.position = new Vector3(240f, 0f, 0f);
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x00012E91 File Offset: 0x00011091
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x0011835C File Offset: 0x0011655C
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

	// Token: 0x06001BAA RID: 7082 RVA: 0x00012E99 File Offset: 0x00011099
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x00012EAD File Offset: 0x000110AD
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x00012ECF File Offset: 0x000110CF
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x00118404 File Offset: 0x00116604
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

	// Token: 0x06001BAE RID: 7086 RVA: 0x00118470 File Offset: 0x00116670
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

	// Token: 0x06001BAF RID: 7087 RVA: 0x00012F00 File Offset: 0x00011100
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x0001200E File Offset: 0x0001020E
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00118578 File Offset: 0x00116778
	public void Work(float f, roomScript myRoomS_)
	{
		if (this.gS_)
		{
			if (myRoomS_.typ == 4)
			{
				this.gS_.points_grafik += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 5)
			{
				this.gS_.points_sound += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 3)
			{
				this.gS_.points_gameplay += f;
				this.RemoveInvisBug();
			}
			if (myRoomS_.typ == 10)
			{
				this.gS_.points_technik += f;
				this.RemoveInvisBug();
			}
		}
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.pointsLeft <= 0f)
			{
				this.FindMyObject();
				this.pointsLeft = this.points;
				this.Complete(myRoomS_);
			}
		}
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x00118664 File Offset: 0x00116864
	private void RemoveInvisBug()
	{
		if (UnityEngine.Random.Range(0, 100) < 90)
		{
			return;
		}
		this.gS_.points_bugsInvis -= 1f;
		if (this.gS_.points_bugsInvis < 0f)
		{
			this.gS_.points_bugsInvis = 0f;
		}
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x001186B8 File Offset: 0x001168B8
	private void Complete(roomScript myRoomS_)
	{
		if (this.gS_)
		{
			if (myRoomS_.typ == 4)
			{
				this.gS_.points_grafik += 10f;
			}
			if (myRoomS_.typ == 5)
			{
				this.gS_.points_sound += 10f;
			}
			if (myRoomS_.typ == 3)
			{
				this.gS_.points_gameplay += 10f;
			}
			if (myRoomS_.typ == 10)
			{
				this.gS_.points_technik += 10f;
			}
		}
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022E7 RID: 8935
	public int myID = -1;

	// Token: 0x040022E8 RID: 8936
	public int targetID = -1;

	// Token: 0x040022E9 RID: 8937
	public float points;

	// Token: 0x040022EA RID: 8938
	public float pointsLeft;

	// Token: 0x040022EB RID: 8939
	private GameObject main_;

	// Token: 0x040022EC RID: 8940
	public mainScript mS_;

	// Token: 0x040022ED RID: 8941
	private GUI_Main guiMain_;

	// Token: 0x040022EE RID: 8942
	private textScript tS_;

	// Token: 0x040022EF RID: 8943
	private roomDataScript rdS_;

	// Token: 0x040022F0 RID: 8944
	public gameScript gS_;

	// Token: 0x040022F1 RID: 8945
	public roomScript rS_;

	// Token: 0x040022F2 RID: 8946
	private float findMyRoomTimer;
}
