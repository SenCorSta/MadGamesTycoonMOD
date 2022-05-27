using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class taskMarktforschung : MonoBehaviour
{
	// Token: 0x06001BD8 RID: 7128 RVA: 0x0011501C File Offset: 0x0011321C
	private void Awake()
	{
		base.transform.position = new Vector3(230f, 0f, 0f);
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x0011503D File Offset: 0x0011323D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x00115048 File Offset: 0x00113248
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

	// Token: 0x06001BDB RID: 7131 RVA: 0x001150EE File Offset: 0x001132EE
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x0011511F File Offset: 0x0011331F
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x0011513B File Offset: 0x0011333B
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiSprites[28];
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x00115151 File Offset: 0x00113351
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

	// Token: 0x06001BDF RID: 7135 RVA: 0x0011518C File Offset: 0x0011338C
	private void Complete()
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
		string text = this.tS_.GetText(1165);
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[6]);
		this.mS_.NewMarktforschung();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00115228 File Offset: 0x00113428
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

	// Token: 0x06001BE1 RID: 7137 RVA: 0x0001A799 File Offset: 0x00018999
	public int GetRueckgeld()
	{
		return 0;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022EB RID: 8939
	public int myID = -1;

	// Token: 0x040022EC RID: 8940
	public float points;

	// Token: 0x040022ED RID: 8941
	public float pointsLeft;

	// Token: 0x040022EE RID: 8942
	private GameObject main_;

	// Token: 0x040022EF RID: 8943
	public mainScript mS_;

	// Token: 0x040022F0 RID: 8944
	private GUI_Main guiMain_;

	// Token: 0x040022F1 RID: 8945
	private textScript tS_;

	// Token: 0x040022F2 RID: 8946
	private roomDataScript rdS_;
}
