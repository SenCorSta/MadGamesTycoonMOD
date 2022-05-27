using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015B RID: 347
public class Menu_ROOM_Polishing : MonoBehaviour
{
	// Token: 0x06000CC7 RID: 3271 RVA: 0x00008F01 File Offset: 0x00007101
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0009B74C File Offset: 0x0009994C
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00008F09 File Offset: 0x00007109
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0009B814 File Offset: 0x00099A14
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0009B860 File Offset: 0x00099A60
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Polishing>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00008F41 File Offset: 0x00007141
	public void Init(roomScript script_)
	{
		this.rS_ = script_;
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0009B8BC File Offset: 0x00099ABC
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && component.inDevelopment && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Polishing component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Polishing>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.rS_ = this.rS_;
				}
			}
		}
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00008F56 File Offset: 0x00007156
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0009B9DC File Offset: 0x00099BDC
	public void StartPolishing(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		taskPolishing taskPolishing = this.guiMain_.AddTask_Polishing();
		taskPolishing.Init(false);
		taskPolishing.targetID = gS_.myID;
		taskPolishing.points = 200f;
		taskPolishing.pointsLeft = taskPolishing.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskGameObject = null;
			gameObject.GetComponent<roomScript>().taskID = taskPolishing.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0009BA94 File Offset: 0x00099C94
	public void StartPolishingAutomatic(gameScript gS_, int taskID)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			this.FindScripts();
		}
		taskPolishing taskPolishing = this.guiMain_.AddTask_Polishing();
		taskPolishing.Init(false);
		taskPolishing.targetID = gS_.myID;
		taskPolishing.points = 200f;
		taskPolishing.pointsLeft = taskPolishing.points;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component && component.taskID == taskID)
				{
					component.taskGameObject = taskPolishing.gameObject;
					component.taskID = taskPolishing.myID;
					return;
				}
			}
		}
	}

	// Token: 0x04001157 RID: 4439
	private mainScript mS_;

	// Token: 0x04001158 RID: 4440
	private GameObject main_;

	// Token: 0x04001159 RID: 4441
	private GUI_Main guiMain_;

	// Token: 0x0400115A RID: 4442
	private sfxScript sfx_;

	// Token: 0x0400115B RID: 4443
	private textScript tS_;

	// Token: 0x0400115C RID: 4444
	private genres genres_;

	// Token: 0x0400115D RID: 4445
	public GameObject[] uiPrefabs;

	// Token: 0x0400115E RID: 4446
	public GameObject[] uiObjects;

	// Token: 0x0400115F RID: 4447
	private roomScript rS_;

	// Token: 0x04001160 RID: 4448
	private float updateTimer;
}
