using System;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class taskWait : MonoBehaviour
{
	// Token: 0x06001C63 RID: 7267 RVA: 0x001181D3 File Offset: 0x001163D3
	private void Awake()
	{
		base.transform.position = new Vector3(260f, 0f, 0f);
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x001181F4 File Offset: 0x001163F4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x001181FC File Offset: 0x001163FC
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

	// Token: 0x06001C66 RID: 7270 RVA: 0x001182A2 File Offset: 0x001164A2
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x001182D3 File Offset: 0x001164D3
	private void Update()
	{
		this.AutomaticWait(this.art);
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x001182E4 File Offset: 0x001164E4
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.art == 0)
		{
			return this.guiMain_.uiSprites[18];
		}
		return null;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00118314 File Offset: 0x00116514
	private void AutomaticWait(int art_)
	{
		if (art_ == -1)
		{
			return;
		}
		this.waitTimer += Time.deltaTime;
		if (this.waitTimer < 5f)
		{
			return;
		}
		this.waitTimer = 0f;
		if (this.art == 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					gameScript component = array[i].GetComponent<gameScript>();
					if (component && this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().CheckGameData(component))
					{
						taskSpielbericht taskSpielbericht = this.guiMain_.AddTask_Spielbericht();
						taskSpielbericht.Init(false);
						taskSpielbericht.targetID = component.myID;
						taskSpielbericht.automatic = true;
						taskSpielbericht.automaticWait = true;
						taskSpielbericht.points = (float)this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().GetWorkPoints(component);
						taskSpielbericht.pointsLeft = taskSpielbericht.points;
						for (int j = 0; j < this.mS_.arrayRooms.Length; j++)
						{
							if (this.mS_.arrayRooms[j])
							{
								roomScript component2 = this.mS_.arrayRooms[j].GetComponent<roomScript>();
								if (component2 && component2.taskID == this.myID)
								{
									component2.taskID = taskSpielbericht.myID;
								}
							}
						}
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
				}
			}
		}
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002366 RID: 9062
	public int myID = -1;

	// Token: 0x04002367 RID: 9063
	public int art = -1;

	// Token: 0x04002368 RID: 9064
	private float waitTimer = 10f;

	// Token: 0x04002369 RID: 9065
	private GameObject main_;

	// Token: 0x0400236A RID: 9066
	public mainScript mS_;

	// Token: 0x0400236B RID: 9067
	private GUI_Main guiMain_;

	// Token: 0x0400236C RID: 9068
	private textScript tS_;

	// Token: 0x0400236D RID: 9069
	private roomDataScript rdS_;
}
