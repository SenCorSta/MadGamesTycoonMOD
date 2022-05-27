using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class taskWait : MonoBehaviour
{
	// Token: 0x06001C19 RID: 7193 RVA: 0x00013580 File Offset: 0x00011780
	private void Awake()
	{
		base.transform.position = new Vector3(260f, 0f, 0f);
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x000135A1 File Offset: 0x000117A1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0011A62C File Offset: 0x0011882C
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

	// Token: 0x06001C1C RID: 7196 RVA: 0x000135A9 File Offset: 0x000117A9
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x000135DA File Offset: 0x000117DA
	private void Update()
	{
		this.AutomaticWait(this.art);
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x0011A6D4 File Offset: 0x001188D4
	public Sprite GetPic()
	{
		this.FindScripts();
		if (this.art == 0)
		{
			return this.guiMain_.uiSprites[18];
		}
		return null;
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x0011A704 File Offset: 0x00118904
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

	// Token: 0x06001C20 RID: 7200 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400234C RID: 9036
	public int myID = -1;

	// Token: 0x0400234D RID: 9037
	public int art = -1;

	// Token: 0x0400234E RID: 9038
	private float waitTimer = 10f;

	// Token: 0x0400234F RID: 9039
	private GameObject main_;

	// Token: 0x04002350 RID: 9040
	public mainScript mS_;

	// Token: 0x04002351 RID: 9041
	private GUI_Main guiMain_;

	// Token: 0x04002352 RID: 9042
	private textScript tS_;

	// Token: 0x04002353 RID: 9043
	private roomDataScript rdS_;
}
