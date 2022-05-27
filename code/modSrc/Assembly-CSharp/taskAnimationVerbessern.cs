using System;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class taskAnimationVerbessern : MonoBehaviour
{
	// Token: 0x06001AAA RID: 6826 RVA: 0x00011E76 File Offset: 0x00010076
	private void Awake()
	{
		base.transform.position = new Vector3(150f, 0f, 0f);
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x00011E97 File Offset: 0x00010097
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x0011207C File Offset: 0x0011027C
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

	// Token: 0x06001AAD RID: 6829 RVA: 0x00011E9F File Offset: 0x0001009F
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x00011EB3 File Offset: 0x000100B3
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x00011ED5 File Offset: 0x000100D5
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x0011216C File Offset: 0x0011036C
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

	// Token: 0x06001AB1 RID: 6833 RVA: 0x001121CC File Offset: 0x001103CC
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

	// Token: 0x06001AB2 RID: 6834 RVA: 0x00011F06 File Offset: 0x00010106
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x00011F22 File Offset: 0x00010122
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 18];
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x001122D4 File Offset: 0x001104D4
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

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00112340 File Offset: 0x00110540
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

	// Token: 0x06001AB6 RID: 6838 RVA: 0x001123F8 File Offset: 0x001105F8
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

	// Token: 0x06001AB7 RID: 6839 RVA: 0x001124F8 File Offset: 0x001106F8
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

	// Token: 0x06001AB8 RID: 6840 RVA: 0x00112558 File Offset: 0x00110758
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

	// Token: 0x06001AB9 RID: 6841 RVA: 0x001125A0 File Offset: 0x001107A0
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

	// Token: 0x040021FD RID: 8701
	public int myID = -1;

	// Token: 0x040021FE RID: 8702
	public int targetID = -1;

	// Token: 0x040021FF RID: 8703
	public float points;

	// Token: 0x04002200 RID: 8704
	public float pointsLeft;

	// Token: 0x04002201 RID: 8705
	public bool[] adds = new bool[6];

	// Token: 0x04002202 RID: 8706
	public int aktuellerAdd = -1;

	// Token: 0x04002203 RID: 8707
	private GameObject main_;

	// Token: 0x04002204 RID: 8708
	public mainScript mS_;

	// Token: 0x04002205 RID: 8709
	private GUI_Main guiMain_;

	// Token: 0x04002206 RID: 8710
	private textScript tS_;

	// Token: 0x04002207 RID: 8711
	private roomDataScript rdS_;

	// Token: 0x04002208 RID: 8712
	public gameScript gS_;

	// Token: 0x04002209 RID: 8713
	private Menu_MOCAP_AnimationVerbessern menuMOCAP_;

	// Token: 0x0400220A RID: 8714
	private games games_;

	// Token: 0x0400220B RID: 8715
	public roomScript rS_;

	// Token: 0x0400220C RID: 8716
	private float findMyRoomTimer;
}
