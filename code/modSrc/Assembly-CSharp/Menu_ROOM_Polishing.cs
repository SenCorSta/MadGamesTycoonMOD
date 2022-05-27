using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015C RID: 348
public class Menu_ROOM_Polishing : MonoBehaviour
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x0008C919 File Offset: 0x0008AB19
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0008C924 File Offset: 0x0008AB24
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

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0008C9EC File Offset: 0x0008ABEC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0008CA24 File Offset: 0x0008AC24
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

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0008CA70 File Offset: 0x0008AC70
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

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0008CACC File Offset: 0x0008ACCC
	public void Init(roomScript script_)
	{
		this.rS_ = script_;
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0008CAE4 File Offset: 0x0008ACE4
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0008CBF7 File Offset: 0x0008ADF7
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0008CC1F File Offset: 0x0008AE1F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0008CC48 File Offset: 0x0008AE48
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

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0008CD00 File Offset: 0x0008AF00
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

	// Token: 0x0400115F RID: 4447
	private mainScript mS_;

	// Token: 0x04001160 RID: 4448
	private GameObject main_;

	// Token: 0x04001161 RID: 4449
	private GUI_Main guiMain_;

	// Token: 0x04001162 RID: 4450
	private sfxScript sfx_;

	// Token: 0x04001163 RID: 4451
	private textScript tS_;

	// Token: 0x04001164 RID: 4452
	private genres genres_;

	// Token: 0x04001165 RID: 4453
	public GameObject[] uiPrefabs;

	// Token: 0x04001166 RID: 4454
	public GameObject[] uiObjects;

	// Token: 0x04001167 RID: 4455
	private roomScript rS_;

	// Token: 0x04001168 RID: 4456
	private float updateTimer;
}
