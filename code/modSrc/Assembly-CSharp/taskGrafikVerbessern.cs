using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class taskGrafikVerbessern : MonoBehaviour
{
	// Token: 0x06001B9B RID: 7067 RVA: 0x00113670 File Offset: 0x00111870
	private void Awake()
	{
		base.transform.position = new Vector3(130f, 0f, 0f);
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x00113691 File Offset: 0x00111891
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x0011369C File Offset: 0x0011189C
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
		if (!this.menuGFX_)
		{
			this.menuGFX_ = this.guiMain_.uiObjects[174].GetComponent<Menu_GFX_GrafikVerbessern>();
		}
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x00113789 File Offset: 0x00111989
	private void Update()
	{
		this.FindMyObject();
		this.FindMyRoom();
		this.GamePublished();
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x0011379D File Offset: 0x0011199D
	private void GamePublished()
	{
		if (this.gS_ && !this.gS_.inDevelopment)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x001137BF File Offset: 0x001119BF
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x001137F0 File Offset: 0x001119F0
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

	// Token: 0x06001BA2 RID: 7074 RVA: 0x00113850 File Offset: 0x00111A50
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

	// Token: 0x06001BA3 RID: 7075 RVA: 0x00113955 File Offset: 0x00111B55
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x00113971 File Offset: 0x00111B71
	public Sprite GetPic()
	{
		return this.games_.gameAdds[this.aktuellerAdd + 6];
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x00113988 File Offset: 0x00111B88
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= 1f;
			if (this.gS_)
			{
				this.gS_.points_grafik += f;
			}
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x001139F4 File Offset: 0x00111BF4
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
			this.points = num * this.menuGFX_.pointsInPercent[this.aktuellerAdd];
			this.pointsLeft = this.points;
			return;
		}
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(this.gS_, this.myID);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x00113AAC File Offset: 0x00111CAC
	private void Complete()
	{
		this.FindMyObject();
		this.adds[this.aktuellerAdd] = false;
		this.gS_.grafikStudio[this.aktuellerAdd] = true;
		this.gS_.costs_entwicklung += (long)this.menuGFX_.GetCosts(this.aktuellerAdd);
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
		string text = this.tS_.GetText(918);
		text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[4]);
		this.FindNewAdd();
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x00113BA8 File Offset: 0x00111DA8
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

	// Token: 0x06001BA9 RID: 7081 RVA: 0x00113C08 File Offset: 0x00111E08
	public int GetRueckgeld()
	{
		float num = 0f;
		for (int i = 0; i < this.adds.Length; i++)
		{
			if (this.adds[i])
			{
				num += (float)this.menuGFX_.GetCosts(i);
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x00113C50 File Offset: 0x00111E50
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

	// Token: 0x040022B1 RID: 8881
	public int myID = -1;

	// Token: 0x040022B2 RID: 8882
	public int targetID = -1;

	// Token: 0x040022B3 RID: 8883
	public float points;

	// Token: 0x040022B4 RID: 8884
	public float pointsLeft;

	// Token: 0x040022B5 RID: 8885
	public bool[] adds = new bool[6];

	// Token: 0x040022B6 RID: 8886
	public int aktuellerAdd = -1;

	// Token: 0x040022B7 RID: 8887
	private GameObject main_;

	// Token: 0x040022B8 RID: 8888
	public mainScript mS_;

	// Token: 0x040022B9 RID: 8889
	private GUI_Main guiMain_;

	// Token: 0x040022BA RID: 8890
	private textScript tS_;

	// Token: 0x040022BB RID: 8891
	private roomDataScript rdS_;

	// Token: 0x040022BC RID: 8892
	public gameScript gS_;

	// Token: 0x040022BD RID: 8893
	private Menu_GFX_GrafikVerbessern menuGFX_;

	// Token: 0x040022BE RID: 8894
	private games games_;

	// Token: 0x040022BF RID: 8895
	public roomScript rS_;

	// Token: 0x040022C0 RID: 8896
	private float findMyRoomTimer;
}
