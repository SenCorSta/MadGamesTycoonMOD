using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class taskPolishing : MonoBehaviour
{
	// Token: 0x06001BF1 RID: 7153 RVA: 0x0011580D File Offset: 0x00113A0D
	private void Awake()
	{
		base.transform.position = new Vector3(240f, 0f, 0f);
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x0011582E File Offset: 0x00113A2E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x00115838 File Offset: 0x00113A38
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

	// Token: 0x06001BF4 RID: 7156 RVA: 0x001158DE File Offset: 0x00113ADE
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x001158F2 File Offset: 0x00113AF2
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x00115914 File Offset: 0x00113B14
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x00115948 File Offset: 0x00113B48
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

	// Token: 0x06001BF8 RID: 7160 RVA: 0x001159B4 File Offset: 0x00113BB4
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

	// Token: 0x06001BF9 RID: 7161 RVA: 0x00115AB9 File Offset: 0x00113CB9
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x0010EE7D File Offset: 0x0010D07D
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x00115AD8 File Offset: 0x00113CD8
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

	// Token: 0x06001BFC RID: 7164 RVA: 0x00115BC4 File Offset: 0x00113DC4
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

	// Token: 0x06001BFD RID: 7165 RVA: 0x00115C18 File Offset: 0x00113E18
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

	// Token: 0x06001BFE RID: 7166 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002301 RID: 8961
	public int myID = -1;

	// Token: 0x04002302 RID: 8962
	public int targetID = -1;

	// Token: 0x04002303 RID: 8963
	public float points;

	// Token: 0x04002304 RID: 8964
	public float pointsLeft;

	// Token: 0x04002305 RID: 8965
	private GameObject main_;

	// Token: 0x04002306 RID: 8966
	public mainScript mS_;

	// Token: 0x04002307 RID: 8967
	private GUI_Main guiMain_;

	// Token: 0x04002308 RID: 8968
	private textScript tS_;

	// Token: 0x04002309 RID: 8969
	private roomDataScript rdS_;

	// Token: 0x0400230A RID: 8970
	public gameScript gS_;

	// Token: 0x0400230B RID: 8971
	public roomScript rS_;

	// Token: 0x0400230C RID: 8972
	private float findMyRoomTimer;
}
