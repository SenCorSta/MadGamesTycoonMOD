using System;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class taskBugfixing : MonoBehaviour
{
	// Token: 0x06001ACA RID: 6858 RVA: 0x00012062 File Offset: 0x00010262
	private void Awake()
	{
		base.transform.position = new Vector3(120f, 0f, 0f);
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x00012083 File Offset: 0x00010283
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x0011299C File Offset: 0x00110B9C
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

	// Token: 0x06001ACD RID: 6861 RVA: 0x0001208B File Offset: 0x0001028B
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0001209F File Offset: 0x0001029F
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x000120C1 File Offset: 0x000102C1
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x00112A44 File Offset: 0x00110C44
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

	// Token: 0x06001AD1 RID: 6865 RVA: 0x00112AB0 File Offset: 0x00110CB0
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

	// Token: 0x06001AD2 RID: 6866 RVA: 0x000120F2 File Offset: 0x000102F2
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0001200E File Offset: 0x0001020E
	public Sprite GetPic()
	{
		return null;
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x00112BB8 File Offset: 0x00110DB8
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

	// Token: 0x06001AD5 RID: 6869 RVA: 0x00002098 File Offset: 0x00000298
	private void Complete()
	{
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400221A RID: 8730
	public int myID = -1;

	// Token: 0x0400221B RID: 8731
	public int targetID = -1;

	// Token: 0x0400221C RID: 8732
	public float points;

	// Token: 0x0400221D RID: 8733
	public float pointsLeft;

	// Token: 0x0400221E RID: 8734
	private GameObject main_;

	// Token: 0x0400221F RID: 8735
	public mainScript mS_;

	// Token: 0x04002220 RID: 8736
	private GUI_Main guiMain_;

	// Token: 0x04002221 RID: 8737
	private textScript tS_;

	// Token: 0x04002222 RID: 8738
	private roomDataScript rdS_;

	// Token: 0x04002223 RID: 8739
	public gameScript gS_;

	// Token: 0x04002224 RID: 8740
	public roomScript rS_;

	// Token: 0x04002225 RID: 8741
	private float findMyRoomTimer;
}
