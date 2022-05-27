using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class buildRoomScript : MonoBehaviour
{
	// Token: 0x06001C7D RID: 7293 RVA: 0x00118E90 File Offset: 0x00117090
	private void Awake()
	{
		this.mapOld = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapNew = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapDoors = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapWindows = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapRemove = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapRoomGO = new GameObject[mapScript.mapSizeX, mapScript.mapSizeY];
		this.doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x00118F30 File Offset: 0x00117130
	private void Start()
	{
		this.FindScripts();
		this.InitPointers();
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00118F40 File Offset: 0x00117140
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = base.GetComponent<roomDataScript>();
		}
		if (!this.mapScript_)
		{
			this.mapScript_ = base.GetComponent<mapScript>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.guiMain)
		{
			this.guiMain = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.seeker)
		{
			this.seeker = base.GetComponent<Seeker>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = GameObject.Find("Camera").GetComponent<mainCameraScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x0011903C File Offset: 0x0011723C
	private void InitPointers()
	{
		for (int i = 0; i < this.pointersInstantiate.Length; i++)
		{
			if (!this.pointersInstantiate[i])
			{
				this.pointersInstantiate[i] = UnityEngine.Object.Instantiate<GameObject>(this.pointers[i]);
			}
		}
		this.DisableAllPointers();
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x00119088 File Offset: 0x00117288
	public void DisableAllPointers()
	{
		for (int i = 0; i < this.pointersInstantiate.Length; i++)
		{
			if (this.pointersInstantiate[i])
			{
				this.pointersInstantiate[i].SetActive(false);
			}
		}
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x001190C8 File Offset: 0x001172C8
	private void Update()
	{
		if (!this.activ)
		{
			return;
		}
		if (this.guiMain.IsMouseOverGUI())
		{
			this.DisableAllPointers();
		}
		this.GetMousePosition();
		this.ResizeRoom();
		this.ResizeRoom_Move();
		this.SetDoor();
		this.SetWindow();
		this.CreateRoomVorschau();
		this.SetPointer();
		this.CreateRoomVorschau_MoveRoom();
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x00119121 File Offset: 0x00117321
	public void SetActive()
	{
		this.activ = true;
		if (!this.mCamS_.additionalCamera[0].activeSelf)
		{
			this.mCamS_.additionalCamera[0].SetActive(true);
		}
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00119151 File Offset: 0x00117351
	public void SetInactive()
	{
		this.activ = false;
		this.DisableAllPointers();
		this.DeleteComplete();
		if (this.mCamS_.additionalCamera[0].activeSelf)
		{
			this.mCamS_.additionalCamera[0].SetActive(false);
		}
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x00119190 File Offset: 0x00117390
	private void GetMousePosition()
	{
		this.makeUpdate = false;
		if (this.guiMain.IsMouseOverGUI())
		{
			return;
		}
		int layerMask = 0;
		if (this.modus == 0 || this.modus == 1 || this.modus == 4)
		{
			layerMask = 512;
		}
		if (this.modus == 2)
		{
			layerMask = 1024;
		}
		if (this.modus == 3)
		{
			layerMask = 1024;
		}
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out this.hit, 200f, layerMask))
		{
			if ((this.modus == 0 || this.modus == 1) && Input.GetMouseButton(0) && (this.posX != Mathf.RoundToInt(this.hit.transform.position.x) || this.posY != Mathf.RoundToInt(this.hit.transform.position.z)))
			{
				this.makeUpdate = true;
			}
			if (this.moveRoomID != -1 && (this.posX != Mathf.RoundToInt(this.hit.transform.position.x) || this.posY != Mathf.RoundToInt(this.hit.transform.position.z)))
			{
				this.makeUpdateMoveRoom = true;
			}
			this.posX = Mathf.RoundToInt(this.hit.transform.position.x);
			this.posY = Mathf.RoundToInt(this.hit.transform.position.z);
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x0011932C File Offset: 0x0011752C
	private void SetPointer()
	{
		if (this.guiMain.IsMouseOverGUI())
		{
			return;
		}
		for (int i = 0; i < this.pointersInstantiate.Length; i++)
		{
			if (this.pointersInstantiate[i])
			{
				Vector3 vector = this.pointersInstantiate[i].transform.position;
				vector = Vector3.Lerp(vector, new Vector3((float)this.posX, -0.4f, (float)this.posY), 0.3f);
				this.pointersInstantiate[i].transform.position = vector;
				if ((double)Vector3.Distance(vector, new Vector3((float)this.posX, -0.4f, (float)this.posY)) > 2.0)
				{
					this.pointersInstantiate[i].transform.position = new Vector3((float)this.posX, -0.4f, (float)this.posY);
				}
				if (i == 3)
				{
					this.pointersInstantiate[i].transform.position = new Vector3((float)this.posX, -0.4f, (float)this.posY);
				}
			}
		}
		switch (this.modus)
		{
		case 0:
			this.DisableAllPointers(0, -1, -1);
			if (this.GetMergedMap(this.posX, this.posY) == 0)
			{
				if (!this.pointersInstantiate[0].activeSelf)
				{
					this.pointersInstantiate[0].SetActive(true);
					return;
				}
			}
			else if (this.pointersInstantiate[0].activeSelf)
			{
				this.pointersInstantiate[0].SetActive(false);
				return;
			}
			break;
		case 1:
			this.DisableAllPointers(1, -1, -1);
			if (!this.pointersInstantiate[1].activeSelf)
			{
				this.pointersInstantiate[1].SetActive(true);
				return;
			}
			break;
		case 2:
			this.DisableAllPointers(2, 3, -1);
			if (this.hit.transform)
			{
				if (!this.errorDoor)
				{
					this.mCamS_.SetOutlineColor(0, 1f, 3);
					if (!this.pointersInstantiate[2].activeSelf)
					{
						this.pointersInstantiate[2].SetActive(true);
					}
					if (this.pointersInstantiate[3].activeSelf)
					{
						this.pointersInstantiate[3].SetActive(false);
					}
					this.pointersInstantiate[2].transform.localEulerAngles = new Vector3(90f, this.hit.transform.localEulerAngles.y, 0f);
					return;
				}
				this.mCamS_.SetOutlineColor(1, 1f, 3);
				if (this.pointersInstantiate[2].activeSelf)
				{
					this.pointersInstantiate[2].SetActive(false);
				}
				if (!this.pointersInstantiate[3].activeSelf)
				{
					this.pointersInstantiate[3].SetActive(true);
				}
				this.pointersInstantiate[3].transform.localEulerAngles = new Vector3(90f, this.hit.transform.localEulerAngles.y, 0f);
				return;
			}
			else
			{
				if (this.pointersInstantiate[2].activeSelf)
				{
					this.pointersInstantiate[2].SetActive(false);
				}
				if (this.pointersInstantiate[3].activeSelf)
				{
					this.pointersInstantiate[3].SetActive(false);
					return;
				}
			}
			break;
		case 3:
			this.DisableAllPointers(4, 5, -1);
			if (this.hit.transform)
			{
				if (!Input.GetKey(KeyCode.LeftShift))
				{
					if (this.errorWindow)
					{
						this.mCamS_.SetOutlineColor(1, 1f, 3);
						if (this.pointersInstantiate[4].activeSelf)
						{
							this.pointersInstantiate[4].SetActive(false);
						}
						if (!this.pointersInstantiate[5].activeSelf)
						{
							this.pointersInstantiate[5].SetActive(true);
						}
						this.pointersInstantiate[5].transform.localEulerAngles = new Vector3(90f, this.hit.transform.localEulerAngles.y, 0f);
						return;
					}
					if (this.mapWindows[this.posX, this.posY] <= 0)
					{
						this.mCamS_.SetOutlineColor(0, 1f, 3);
						if (!this.pointersInstantiate[4].activeSelf)
						{
							this.pointersInstantiate[4].SetActive(true);
						}
						if (this.pointersInstantiate[5].activeSelf)
						{
							this.pointersInstantiate[5].SetActive(false);
						}
						this.pointersInstantiate[4].transform.localEulerAngles = new Vector3(90f, this.hit.transform.localEulerAngles.y, 0f);
						return;
					}
					if (this.pointersInstantiate[4].activeSelf)
					{
						this.pointersInstantiate[4].SetActive(false);
					}
					if (this.pointersInstantiate[5].activeSelf)
					{
						this.pointersInstantiate[5].SetActive(false);
						return;
					}
				}
				else
				{
					if (this.mapWindows[this.posX, this.posY] > 0)
					{
						this.mCamS_.SetOutlineColor(1, 1f, 3);
						if (!this.pointersInstantiate[4].activeSelf)
						{
							this.pointersInstantiate[4].SetActive(true);
						}
						if (this.pointersInstantiate[5].activeSelf)
						{
							this.pointersInstantiate[5].SetActive(false);
						}
						this.pointersInstantiate[4].transform.localEulerAngles = new Vector3(90f, this.hit.transform.localEulerAngles.y, 0f);
						this.pointersInstantiate[4].transform.position = new Vector3((float)this.posX, -0.5f, (float)this.posY);
						return;
					}
					if (this.pointersInstantiate[4].activeSelf)
					{
						this.pointersInstantiate[4].SetActive(false);
					}
					if (this.pointersInstantiate[5].activeSelf)
					{
						this.pointersInstantiate[5].SetActive(false);
						return;
					}
				}
			}
			else
			{
				if (this.pointersInstantiate[4].activeSelf)
				{
					this.pointersInstantiate[4].SetActive(false);
				}
				if (this.pointersInstantiate[5].activeSelf)
				{
					this.pointersInstantiate[5].SetActive(false);
				}
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x0011992C File Offset: 0x00117B2C
	private void DisableAllPointers(int a, int b, int c)
	{
		for (int i = 0; i < this.pointersInstantiate.Length; i++)
		{
			if (i != a && i != b && i != c && this.pointersInstantiate[i].activeSelf)
			{
				this.pointersInstantiate[i].SetActive(false);
			}
		}
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x00119978 File Offset: 0x00117B78
	private void SetDoor()
	{
		if (this.modus != 2)
		{
			return;
		}
		if (Input.GetKeyUp(KeyCode.Delete))
		{
			this.sfx_.PlaySound(1, true);
			for (int i = 0; i < mapScript.mapSizeX; i++)
			{
				for (int j = 0; j < mapScript.mapSizeY; j++)
				{
					this.mapDoors[i, j] = 0;
				}
			}
			this.makeUpdate = true;
			return;
		}
		if (!this.hit.transform)
		{
			return;
		}
		if (this.guiMain.IsMouseOverGUI())
		{
			return;
		}
		this.errorDoor = false;
		if (this.mapScript_.mapDoors[this.posX, this.posY] > 0 || this.mapScript_.mapWindows[this.posX, this.posY] > 0)
		{
			this.errorDoor = true;
			return;
		}
		int num = Mathf.RoundToInt(this.hit.transform.eulerAngles.y);
		if (num <= 90)
		{
			if (num != 0)
			{
				if (num == 90)
				{
					if (!this.OutOfMap(this.posX, this.posY - 1))
					{
						if (this.mapScript_.mapRoomID[this.posX, this.posY - 1] == 0)
						{
							this.errorDoor = true;
						}
						if (this.mapScript_.mapRoomID[this.posX, this.posY - 1] > 1)
						{
							this.errorDoor = true;
						}
					}
				}
			}
			else if (!this.OutOfMap(this.posX + 1, this.posY))
			{
				if (this.mapScript_.mapRoomID[this.posX + 1, this.posY] == 0)
				{
					this.errorDoor = true;
				}
				if (this.mapScript_.mapRoomID[this.posX + 1, this.posY] > 1)
				{
					this.errorDoor = true;
				}
			}
		}
		else if (num != 180)
		{
			if (num == 270)
			{
				if (!this.OutOfMap(this.posX, this.posY + 1))
				{
					if (this.mapScript_.mapRoomID[this.posX, this.posY + 1] == 0)
					{
						this.errorDoor = true;
					}
					if (this.mapScript_.mapRoomID[this.posX, this.posY + 1] > 1)
					{
						this.errorDoor = true;
					}
				}
			}
		}
		else if (!this.OutOfMap(this.posX - 1, this.posY))
		{
			if (this.mapScript_.mapRoomID[this.posX - 1, this.posY] == 0)
			{
				this.errorDoor = true;
			}
			if (this.mapScript_.mapRoomID[this.posX - 1, this.posY] > 1)
			{
				this.errorDoor = true;
			}
		}
		if (this.errorDoor)
		{
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.sfx_.PlaySound(0, true);
			for (int k = 0; k < mapScript.mapSizeX; k++)
			{
				for (int l = 0; l < mapScript.mapSizeY; l++)
				{
					this.mapDoors[k, l] = 0;
				}
			}
			if (this.mapWindows[this.posX, this.posY] > 0)
			{
				this.RemoveWindow(this.mapWindows[this.posX, this.posY]);
			}
			this.mapDoors[this.posX, this.posY] = Mathf.RoundToInt(this.hit.transform.localEulerAngles.y + 1000f);
			this.makeUpdate = true;
			Debug.Log(string.Concat(new object[]
			{
				"Set Door: ",
				this.posX,
				",",
				this.posY,
				" / Rot: ",
				this.mapDoors[this.posX, this.posY]
			}));
		}
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x00119D70 File Offset: 0x00117F70
	private void SetWindow()
	{
		if (this.modus != 3)
		{
			return;
		}
		if (!this.hit.transform)
		{
			return;
		}
		if (this.guiMain.IsMouseOverGUI())
		{
			return;
		}
		this.errorWindow = false;
		if (this.mapScript_.mapDoors[this.posX, this.posY] > 0 || this.mapScript_.mapWindows[this.posX, this.posY] > 0)
		{
			this.errorWindow = true;
			return;
		}
		int num = Mathf.RoundToInt(this.hit.transform.eulerAngles.y);
		if (num <= 90)
		{
			if (num != 0)
			{
				if (num == 90)
				{
					if (!this.OutOfMap(this.posX, this.posY - 1) && this.mapScript_.mapRoomID[this.posX, this.posY - 1] == 0)
					{
						this.errorWindow = true;
					}
				}
			}
			else if (!this.OutOfMap(this.posX + 1, this.posY) && this.mapScript_.mapRoomID[this.posX + 1, this.posY] == 0)
			{
				this.errorWindow = true;
			}
		}
		else if (num != 180)
		{
			if (num == 270)
			{
				if (!this.OutOfMap(this.posX, this.posY + 1) && this.mapScript_.mapRoomID[this.posX, this.posY + 1] == 0)
				{
					this.errorWindow = true;
				}
			}
		}
		else if (!this.OutOfMap(this.posX - 1, this.posY) && this.mapScript_.mapRoomID[this.posX - 1, this.posY] == 0)
		{
			this.errorWindow = true;
		}
		if (this.errorWindow)
		{
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (!Input.GetKey(KeyCode.LeftShift))
			{
				if (this.mapWindows[this.posX, this.posY] <= 0)
				{
					this.sfx_.PlaySound(0, true);
					this.mapWindows[this.posX, this.posY] = UnityEngine.Random.Range(1, 99999999);
					this.makeUpdate = true;
					num = Mathf.RoundToInt(this.hit.transform.localEulerAngles.y);
					if (num <= 90)
					{
						if (num == 0)
						{
							this.mapWindows[this.posX + 1, this.posY] = this.mapWindows[this.posX, this.posY];
							return;
						}
						if (num != 90)
						{
							return;
						}
						this.mapWindows[this.posX, this.posY - 1] = this.mapWindows[this.posX, this.posY];
						return;
					}
					else
					{
						if (num == 180)
						{
							this.mapWindows[this.posX - 1, this.posY] = this.mapWindows[this.posX, this.posY];
							return;
						}
						if (num != 270)
						{
							return;
						}
						this.mapWindows[this.posX, this.posY + 1] = this.mapWindows[this.posX, this.posY];
						return;
					}
				}
			}
			else if (this.mapWindows[this.posX, this.posY] > 0)
			{
				this.sfx_.PlaySound(1, true);
				this.RemoveWindow(this.mapWindows[this.posX, this.posY]);
				this.makeUpdate = true;
			}
		}
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x0011A108 File Offset: 0x00118308
	private void RemoveWindow(int i)
	{
		for (int j = 0; j < mapScript.mapSizeX; j++)
		{
			for (int k = 0; k < mapScript.mapSizeY; k++)
			{
				if (this.mapWindows[j, k] == i)
				{
					Debug.Log("Remove Window: " + this.mapWindows[j, k].ToString());
					this.mapWindows[j, k] = 0;
				}
			}
		}
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x0011A174 File Offset: 0x00118374
	private void RemoveDoor(int i)
	{
		for (int j = 0; j < mapScript.mapSizeX; j++)
		{
			for (int k = 0; k < mapScript.mapSizeY; k++)
			{
				if (this.mapDoors[j, k] == i)
				{
					Debug.Log("Remove Door: " + this.mapDoors[j, k].ToString());
					this.mapDoors[j, k] = 0;
				}
			}
		}
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x0011A1E0 File Offset: 0x001183E0
	private void ResizeRoom_Move()
	{
		if (this.modus != 4)
		{
			return;
		}
		Vector2 aussenrand = this.GetAussenrand();
		int num = Mathf.RoundToInt(aussenrand.x);
		int num2 = Mathf.RoundToInt(aussenrand.y);
		if (!this.guiMain.IsMouseOverGUI() && Input.GetMouseButton(1))
		{
			for (int i = 0; i < mapScript.mapSizeX; i++)
			{
				for (int j = 0; j < mapScript.mapSizeY; j++)
				{
					this.mapOld[i, j] = 0;
				}
			}
			this.error = false;
			for (int k = 0; k < mapScript.mapSizeX; k++)
			{
				for (int l = 0; l < mapScript.mapSizeY; l++)
				{
					if (this.mapMove[k, l] != 0)
					{
						int num3 = k + this.posX - num;
						int num4 = l + this.posY - num2;
						this.mapOld[num3, num4] = 1;
						if (!this.mS_.IsMyBuilding(this.mapScript_.mapBuilding[num3, num4]))
						{
							this.error = true;
							Debug.Log("NICHT MEINS");
						}
						if (this.mapScript_.mapBlock[num3, num4] != 0)
						{
							this.error = true;
						}
						if (this.mapScript_.mapRoomID[num3, num4] != 1)
						{
							this.error = true;
						}
						if (this.mapScript_.mapDoors[num3, num4] > 0)
						{
							this.error = true;
						}
					}
				}
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			if (this.guiMain.IsMouseOverGUI())
			{
				this.error = true;
			}
			if (!this.error)
			{
				this.sfx_.PlaySound(0, true);
			}
			else if (!this.guiMain.IsMouseOverGUI())
			{
				this.sfx_.PlaySound(2, true);
			}
			this.makeUpdateMoveRoom = true;
			if (this.error)
			{
				this.error = false;
				return;
			}
			for (int m = 0; m < mapScript.mapSizeX; m++)
			{
				for (int n = 0; n < mapScript.mapSizeY; n++)
				{
					if (this.mapScript_.mapRoomID[m, n] == this.moveRoomScript_.myID)
					{
						this.mapScript_.mapRoomID[m, n] = mapScript.ID_FLOOR;
						this.mapScript_.mapWindows[m, n] = 0;
						this.mapScript_.mapDoors[m, n] = 0;
					}
				}
			}
			for (int num5 = 0; num5 < mapScript.mapSizeX; num5++)
			{
				for (int num6 = 0; num6 < mapScript.mapSizeY; num6++)
				{
					if (this.mapOld[num5, num6] != 0)
					{
						Debug.Log("KLKL" + UnityEngine.Random.Range(0, 100000));
						this.mapScript_.mapRoomID[num5, num6] = this.moveRoomScript_.myID;
						this.mapScript_.mapRoomScript[num5, num6] = this.moveRoomScript_;
					}
				}
			}
			for (int num7 = 0; num7 < this.moveRoomScript_.listInventar.Count; num7++)
			{
				if (this.moveRoomScript_.listInventar[num7])
				{
					Vector3 position = this.moveRoomScript_.listInventar[num7].transform.position;
					this.moveRoomScript_.listInventar[num7].transform.position = new Vector3(position.x - (float)num + (float)this.posX, 0f, position.z - (float)num2 + (float)this.posY);
				}
			}
			this.modus = 0;
			this.CreateOldRoomLayout(this.moveRoomScript_);
		}
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x0011A5A8 File Offset: 0x001187A8
	private void ResizeRoom()
	{
		if (this.modus != 0 && this.modus != 1)
		{
			return;
		}
		if (!this.guiMain.IsMouseOverGUI())
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.roomStartX = this.posX;
				this.roomStartY = this.posY;
				this.makeUpdate = true;
				Debug.Log(string.Concat(new object[]
				{
					this.roomStartX.ToString(),
					", ",
					this.roomStartY.ToString(),
					"/ ID: ",
					this.mapScript_.mapRoomID[this.posX, this.posY],
					" DOOR: ",
					this.mapScript_.mapDoors[this.posX, this.posY],
					" WINDOW: ",
					this.mapScript_.mapWindows[this.posX, this.posY]
				}));
			}
			if (Input.GetMouseButton(0) && this.roomStartX > 0 && this.roomStartX < mapScript.mapSizeX && this.roomStartY > 0 && this.roomStartY < mapScript.mapSizeY)
			{
				for (int i = 0; i < mapScript.mapSizeX; i++)
				{
					for (int j = 0; j < mapScript.mapSizeY; j++)
					{
						if (this.modus == 0)
						{
							this.mapNew[i, j] = 0;
						}
						if (this.modus == 1)
						{
							this.mapRemove[i, j] = 0;
						}
					}
				}
				this.error = false;
				bool flag = this.ExistRoom();
				bool flag2 = false;
				int num;
				int num2;
				if (this.roomStartX < this.posX)
				{
					num = this.roomStartX;
					num2 = this.posX;
				}
				else
				{
					num = this.posX;
					num2 = this.roomStartX;
				}
				int num3;
				int num4;
				if (this.roomStartY < this.posY)
				{
					num3 = this.roomStartY;
					num4 = this.posY;
				}
				else
				{
					num3 = this.posY;
					num4 = this.roomStartY;
				}
				for (int k = num; k <= num2; k++)
				{
					for (int l = num3; l <= num4; l++)
					{
						int num5 = this.modus;
						if (num5 != 0)
						{
							if (num5 == 1)
							{
								this.mapRemove[k, l] = 1;
							}
						}
						else
						{
							this.mapNew[k, l] = 1;
							if (!this.mS_.IsMyBuilding(this.mapScript_.mapBuilding[k, l]))
							{
								this.error = true;
								Debug.Log("NCIHT MEH");
							}
							if (this.mapScript_.mapBlock[k, l] != 0)
							{
								this.error = true;
							}
							if (this.mapScript_.mapRoomID[k, l] != 1)
							{
								this.error = true;
							}
							if (this.mapScript_.mapDoors[k, l] > 0)
							{
								this.error = true;
							}
							if (!flag2 && flag && (this.mapOld[k - 1, l] == 1 || this.mapOld[k + 1, l] == 1 || this.mapOld[k, l - 1] == 1 || this.mapOld[k, l + 1] == 1))
							{
								flag2 = true;
							}
						}
					}
				}
				if (this.modus == 1 && this.ErrorCut())
				{
					this.error = true;
				}
				if (this.modus == 0 && flag && !flag2)
				{
					this.error = true;
				}
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.guiMain.IsMouseOverGUI())
			{
				this.error = true;
			}
			if (!this.error)
			{
				if (this.modus == 0)
				{
					this.sfx_.PlaySound(0, true);
				}
				if (this.modus == 1)
				{
					this.sfx_.PlaySound(1, true);
				}
			}
			else if (!this.guiMain.IsMouseOverGUI())
			{
				this.sfx_.PlaySound(2, true);
			}
			for (int m = 0; m < mapScript.mapSizeX; m++)
			{
				for (int n = 0; n < mapScript.mapSizeY; n++)
				{
					int num5 = this.modus;
					if (num5 != 0)
					{
						if (num5 == 1)
						{
							if (this.error)
							{
								this.mapRemove[m, n] = 0;
							}
							else if (this.mapRemove[m, n] == 1)
							{
								if (this.mapOld[m, n] > 0)
								{
									this.mapOld[m, n] = 0;
									if (this.mapDoors[m, n] > 0)
									{
										this.RemoveDoor(this.mapDoors[m, n]);
									}
									if (this.mapWindows[m, n] > 0)
									{
										this.RemoveWindow(this.mapWindows[m, n]);
									}
								}
								this.mapRemove[m, n] = 0;
							}
						}
					}
					else if (this.error)
					{
						this.mapNew[m, n] = 0;
					}
					else if (this.mapNew[m, n] > 0)
					{
						if (this.mapWindows[m, n] > 0)
						{
							this.RemoveWindow(this.mapWindows[m, n]);
						}
						this.mapOld[m, n] = 1;
						this.mapNew[m, n] = 0;
					}
				}
			}
			this.error = false;
			this.makeUpdate = true;
		}
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x0011AB40 File Offset: 0x00118D40
	public void CreateRoomVorschau_MoveRoom()
	{
		if (this.modus != 4)
		{
			return;
		}
		if (!this.makeUpdateMoveRoom)
		{
			return;
		}
		Vector2 aussenrand = this.GetAussenrand();
		int num = Mathf.RoundToInt(aussenrand.x);
		int num2 = Mathf.RoundToInt(aussenrand.y);
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRoomGO[i, j])
				{
					UnityEngine.Object.Destroy(this.mapRoomGO[i, j]);
				}
				if (this.mapMove[i, j] > 0)
				{
					if (!this.error)
					{
						this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[0], new Vector3((float)(i + this.posX - num), 0f, (float)(j + this.posY - num2)), Quaternion.identity);
					}
					else
					{
						this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[7], new Vector3((float)(i + this.posX - num), 0f, (float)(j + this.posY - num2)), Quaternion.identity);
					}
				}
			}
		}
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x0011AC6C File Offset: 0x00118E6C
	public void CreateRoomVorschau()
	{
		if (this.modus == 4)
		{
			return;
		}
		if (!this.makeUpdate)
		{
			return;
		}
		this.doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRoomGO[i, j])
				{
					UnityEngine.Object.Destroy(this.mapRoomGO[i, j]);
				}
				if (this.mapNew[i, j] > 0 || this.mapOld[i, j] > 0 || this.mapRemove[i, j] > 0)
				{
					GameObject gameObject = null;
					if (this.mapRemove[i, j] <= 0)
					{
						if (!this.error)
						{
							this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[0], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
						}
						else
						{
							this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[7], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
						}
					}
					if (this.mapRemove[i, j] > 0 && this.mapOld[i, j] > 0)
					{
						this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[4], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
					}
					if (this.mapRemove[i, j] > 0 && this.mapOld[i, j] <= 0)
					{
						this.mapRoomGO[i, j] = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[6], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
					}
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					if (this.mapDoors[i, j] > 0 && this.mapRemove[i, j] == 0)
					{
						if (this.mapDoors[i, j] > 0)
						{
							if ((this.mapDoors[i, j] - 1000 == 180 && this.mapScript_.mapRoomID[i - 1, j] == mapScript.ID_FLOOR && this.mapNew[i - 1, j] == 0 && this.mapOld[i - 1, j] == 0) || (this.mapDoors[i, j] - 1000 == 0 && this.mapScript_.mapRoomID[i + 1, j] == mapScript.ID_FLOOR && this.mapNew[i + 1, j] == 0 && this.mapOld[i + 1, j] == 0) || (this.mapDoors[i, j] - 1000 == 270 && this.mapScript_.mapRoomID[i, j + 1] == mapScript.ID_FLOOR && this.mapNew[i, j + 1] == 0 && this.mapOld[i, j + 1] == 0) || (this.mapDoors[i, j] - 1000 == 90 && this.mapScript_.mapRoomID[i, j - 1] == mapScript.ID_FLOOR && this.mapNew[i, j - 1] == 0 && this.mapOld[i, j - 1] == 0))
							{
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[9], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								gameObject.transform.eulerAngles = new Vector3(0f, (float)(this.mapDoors[i, j] - 1000), 0f);
								gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
								this.doorsPlaced[i, j] = true;
								int num = this.mapDoors[i, j] - 1000;
								if (num <= 90)
								{
									if (num != 0)
									{
										if (num == 90)
										{
											flag2 = true;
										}
									}
									else
									{
										flag = true;
									}
								}
								else if (num != 180)
								{
									if (num == 270)
									{
										flag4 = true;
									}
								}
								else
								{
									flag3 = true;
								}
							}
							else
							{
								this.mapDoors[i, j] = 0;
								this.doorsPlaced[i, j] = false;
							}
						}
						if (Input.GetMouseButtonUp(0) && !this.doorsPlaced[i, j])
						{
							this.mapDoors[i, j] = 0;
							Debug.Log("RemoveDoor " + UnityEngine.Random.Range(0, 10000));
						}
					}
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					if (this.mapWindows[i, j] > 0 && this.mapRemove[i, j] == 0)
					{
						if (this.mapWindows[i, j + 1] > 0 && this.GetMergedMap(i, j + 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[10], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
							flag8 = true;
						}
						if (this.mapWindows[i, j - 1] > 0 && this.GetMergedMap(i, j - 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[10], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
							flag6 = true;
						}
						if (this.mapWindows[i + 1, j] > 0 && this.GetMergedMap(i + 1, j) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[10], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
							flag5 = true;
						}
						if (this.mapWindows[i - 1, j] > 0 && this.GetMergedMap(i - 1, j) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[10], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
							flag7 = true;
						}
					}
					if (this.GetMergedMap(i, j) > 0)
					{
						if (this.GetMergedMap(i + 1, j) != this.GetMergedMap(i, j) && !flag && !flag5)
						{
							if (this.error)
							{
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[8], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							}
							else
							{
								if (this.mapRemove[i, j] <= 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[1], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
								if (this.mapRemove[i, j] > 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[5], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
							}
							gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i - 1, j) != this.GetMergedMap(i, j) && !flag3 && !flag7)
						{
							if (this.error)
							{
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[8], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							}
							else
							{
								if (this.mapRemove[i, j] <= 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[1], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
								if (this.mapRemove[i, j] > 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[5], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
							}
							gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i, j + 1) != this.GetMergedMap(i, j) && !flag4 && !flag8)
						{
							if (this.error)
							{
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[8], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							}
							else
							{
								if (this.mapRemove[i, j] <= 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[1], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
								if (this.mapRemove[i, j] > 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[5], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
							}
							gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i, j - 1) != this.GetMergedMap(i, j) && !flag2 && !flag6)
						{
							if (this.error)
							{
								gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[8], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							}
							else
							{
								if (this.mapRemove[i, j] <= 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[1], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
								if (this.mapRemove[i, j] > 0)
								{
									gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[5], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
								}
							}
							gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
					}
					if (this.GetMergedMap(i, j) > 0)
					{
						if (this.GetMergedMap(i, j - 1) == this.GetMergedMap(i, j) && this.GetMergedMap(i - 1, j) == this.GetMergedMap(i, j) && this.GetMergedMap(i - 1, j - 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[3], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i, j - 1) == this.GetMergedMap(i, j) && this.GetMergedMap(i + 1, j) == this.GetMergedMap(i, j) && this.GetMergedMap(i + 1, j - 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[3], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i, j + 1) == this.GetMergedMap(i, j) && this.GetMergedMap(i - 1, j) == this.GetMergedMap(i, j) && this.GetMergedMap(i - 1, j + 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[3], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
						if (this.GetMergedMap(i, j + 1) == this.GetMergedMap(i, j) && this.GetMergedMap(i + 1, j) == this.GetMergedMap(i, j) && this.GetMergedMap(i + 1, j + 1) != this.GetMergedMap(i, j))
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabRoomElements[3], new Vector3((float)i, 0f, (float)j), Quaternion.identity);
							gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
							gameObject.transform.SetParent(this.mapRoomGO[i, j].transform);
						}
					}
				}
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("MakePathfindUpdate");
			base.StartCoroutine(this.UpdatePathfindingNextFrame());
			return;
		}
		this.sfx_.PlaySound(36, true);
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x0011B9CE File Offset: 0x00119BCE
	private IEnumerator UpdatePathfindingNextFrame()
	{
		this.noPath = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.mapScript_.aStar_)
		{
			this.mapScript_.aStar_.Scan(null);
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.noPath = false;
		bool flag = true;
		int num = 0;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mS_.IsMyBuilding(this.mapScript_.mapBuilding[i, j]))
				{
					if (this.mapDoors[i, j] > 0)
					{
						flag = false;
					}
					if (this.mapDoors[i, j] > 0 || this.mapScript_.mapDoors[i, j] > 0)
					{
						num++;
					}
				}
			}
		}
		if (flag)
		{
			this.noPath = true;
		}
		else
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("BuildingDoor");
			this.endVectors = new Vector3[num];
			Debug.Log("AmountDoors: " + this.endVectors.Length.ToString());
			for (int k = 0; k < num; k++)
			{
				this.endVectors[k] = new Vector3(1f, 0f, 1f);
			}
			int num2 = 0;
			for (int l = 0; l < mapScript.mapSizeX; l++)
			{
				for (int m = 0; m < mapScript.mapSizeY; m++)
				{
					if (this.mS_.IsMyBuilding(this.mapScript_.mapBuilding[l, m]) && (this.mapScript_.mapDoors[l, m] > 0 || this.mapDoors[l, m] > 0))
					{
						if (this.mapScript_.mapDoors[l, m] > 0)
						{
							this.endVectors[num2] = new Vector3((float)l, 0f, (float)m);
						}
						else
						{
							this.endVectors[num2] = new Vector3((float)l, 0f, (float)m);
						}
						num2++;
					}
				}
			}
			this.seeker.StartMultiTargetPath(gameObject.transform.position, this.endVectors, true, new OnPathDelegate(this.OnPathComplete), -1);
		}
		yield break;
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x0011B9E0 File Offset: 0x00119BE0
	public void OnPathComplete(Path p)
	{
		Debug.Log("Got Callback");
		if (p.error)
		{
			Debug.Log("Ouch, the path returned an error\nError: " + p.errorLog);
			this.noPath = true;
			return;
		}
		MultiTargetPath multiTargetPath = p as MultiTargetPath;
		if (multiTargetPath == null)
		{
			Debug.LogError("The Path was no MultiTargetPath");
			this.noPath = true;
			return;
		}
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i] && this.mS_.arrayRooms[i].GetComponent<roomScript>().typ != 16)
			{
				this.mS_.arrayRooms[i].GetComponent<roomScript>().SetListGameObjectsLayer(15);
			}
		}
		List<Vector3>[] vectorPaths = multiTargetPath.vectorPaths;
		for (int j = 0; j < vectorPaths.Length; j++)
		{
			List<Vector3> list = vectorPaths[j];
			if (list == null)
			{
				Debug.Log("Path number " + j + " could not be found");
				this.noPath = true;
			}
			else
			{
				int num = Mathf.RoundToInt(list[list.Count - 1].x);
				int num2 = Mathf.RoundToInt(list[list.Count - 1].z);
				if (this.mapScript_.mapRoomScript[num, num2] && this.mapScript_.mapRoomScript[num, num2].typ != 16)
				{
					this.mapScript_.mapRoomScript[num, num2].SetListGameObjectsLayer(0);
				}
			}
		}
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x0011BB6C File Offset: 0x00119D6C
	public bool IsDoor()
	{
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapDoors[i, j] > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x0011BBAC File Offset: 0x00119DAC
	public void CreateRoom(int typ_, int price)
	{
		int num = UnityEngine.Random.Range(100, 999999999);
		GameObject gameObject;
		if (this.replaceRoomID == -1)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.roomMainObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
			gameObject.name = "Room_" + num;
		}
		else
		{
			gameObject = GameObject.Find("Room_" + this.replaceRoomID.ToString());
			num = this.replaceRoomID;
		}
		roomScript component = gameObject.GetComponent<roomScript>();
		component.listGameObjects.Clear();
		component.myID = num;
		component.typ = typ_;
		component.uiPos = this.FindUiPosition();
		gameObject.transform.position = new Vector3(component.uiPos.x, 0f, component.uiPos.z);
		float num2 = 0f;
		Vector3 vector = new Vector3(-1f, -1f, -1f);
		int buildingID = -1;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapOld[i, j] != 0)
				{
					if (UnityEngine.Random.Range(0, 100) > 90 || vector == new Vector3(-1f, -1f, -1f))
					{
						vector = new Vector3((float)i, 1f, (float)j);
					}
					base.StartCoroutine(this.CreateParticle(i, j, num2));
					num2 += 0.005f;
				}
				if (this.mapOld[i, j] != 0)
				{
					this.mapScript_.mapRoomID[i, j] = num;
					this.mapScript_.mapRoomScript[i, j] = component;
					buildingID = this.mapScript_.mapBuilding[i, j];
				}
				if (this.mapDoors[i, j] != 0 && this.mapScript_.mapDoors[i, j] != 99)
				{
					this.mapScript_.mapDoors[i, j] = this.mapDoors[i, j];
				}
				if (this.mapWindows[i, j] != 0 && this.mapScript_.mapWindows[i, j] != 99)
				{
					this.mapScript_.mapWindows[i, j] = 1;
				}
				if (this.mS_.multiplayer && (this.mapOld[i, j] != 0 || this.mapDoors[i, j] != 0 || this.mapWindows[i, j] != 0))
				{
					this.mS_.Multiplayer_SendMap(i, j);
				}
			}
		}
		this.mapScript_.CreateWalls(buildingID);
		this.guiMain.MoneyPop(price, vector, false);
		this.mS_.Pay((long)price, 0);
		this.DeleteComplete();
		this.replaceRoomID = -1;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x0011BEA4 File Offset: 0x0011A0A4
	public float GetBiggestRoomQuad()
	{
		float num = 0f;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapOld[i, j] != 0)
				{
					float num2 = this.QuaderSizeTest(i, j);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x0011BEF8 File Offset: 0x0011A0F8
	public Vector3 FindUiPosition()
	{
		float num = 0f;
		float x = 0f;
		float z = 0f;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapOld[i, j] != 0)
				{
					float num2 = this.QuaderSizeTest(i, j);
					if (num2 > num)
					{
						num = num2;
						x = (float)i + num2 * 0.5f - 0.5f;
						z = (float)j + num2 * 0.5f - 0.5f;
					}
				}
			}
		}
		return new Vector3(x, -0.5f, z);
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x0011BF90 File Offset: 0x0011A190
	public Vector3 FindUiPositionExtern(int id)
	{
		float num = 0f;
		float x = 0f;
		float z = 0f;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapScript_.mapRoomID[i, j] == id)
				{
					float num2 = this.QuaderSizeTestExtern(i, j, id);
					if (num2 > num)
					{
						num = num2;
						x = (float)i + num2 * 0.5f - 0.5f;
						z = (float)j + num2 * 0.5f - 0.5f;
					}
				}
			}
		}
		return new Vector3(x, -0.5f, z);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x0011C030 File Offset: 0x0011A230
	private float QuaderSizeTest(int x, int y)
	{
		float result = 0f;
		int num = 1;
		while (num < 20 && this.QuaderSize(x, y, num))
		{
			result = (float)num;
			num++;
		}
		return result;
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x0011C060 File Offset: 0x0011A260
	private float QuaderSizeTestExtern(int x, int y, int id)
	{
		float result = 0f;
		int num = 1;
		while (num < 20 && this.QuaderSizeExtern(x, y, num, id))
		{
			result = (float)num;
			num++;
		}
		return result;
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x0011C090 File Offset: 0x0011A290
	private bool QuaderSize(int px, int py, int size)
	{
		for (int i = px; i < px + size; i++)
		{
			for (int j = py; j < py + size; j++)
			{
				if (this.OutOfMap(i, j))
				{
					return false;
				}
				if (this.mapOld[i, j] == 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x0011C0D8 File Offset: 0x0011A2D8
	private bool QuaderSizeExtern(int px, int py, int size, int id)
	{
		for (int i = px; i < px + size; i++)
		{
			for (int j = py; j < py + size; j++)
			{
				if (this.OutOfMap(i, j))
				{
					return false;
				}
				if (this.mapScript_.mapRoomID[i, j] != id)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x0011C126 File Offset: 0x0011A326
	private IEnumerator CreateParticle(int x, int y, float t)
	{
		yield return new WaitForSeconds(t);
		UnityEngine.Object.Instantiate<GameObject>(this.prefabParticles[0], new Vector3((float)x, 0.5f, (float)y), Quaternion.identity);
		yield break;
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x0011C14A File Offset: 0x0011A34A
	public IEnumerator CreateParticleDemolish(int x, int y, float t)
	{
		yield return new WaitForSeconds(t);
		UnityEngine.Object.Instantiate<GameObject>(this.prefabParticles[0], new Vector3((float)x, 0.5f, (float)y), Quaternion.identity);
		yield break;
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x0011C170 File Offset: 0x0011A370
	public bool ExistRoom()
	{
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapOld[i, j] > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x0011C1B0 File Offset: 0x0011A3B0
	private int GetMergedMap(int x, int y)
	{
		if (this.mapRemove[x, y] > 0)
		{
			return 0;
		}
		if (this.mapOld[x, y] > 0 || this.mapNew[x, y] > 0)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x0011C1E8 File Offset: 0x0011A3E8
	public int AmountTiles()
	{
		int num = 0;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRemove[i, j] <= 0 && (this.mapOld[i, j] > 0 || this.mapNew[i, j] > 0))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x0011C24C File Offset: 0x0011A44C
	private bool ErrorCut()
	{
		bool flag = false;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (!flag && this.mapOld[i, j] > 0 && this.mapRemove[i, j] <= 0)
				{
					this.mapOld[i, j] = 2;
					flag = true;
					break;
				}
			}
		}
		bool flag2 = false;
		int num = 0;
		while (!flag2)
		{
			bool flag3 = false;
			for (int k = 0; k < mapScript.mapSizeX; k++)
			{
				for (int l = 0; l < mapScript.mapSizeY; l++)
				{
					if (this.mapOld[k, l] == 2 && this.mapRemove[k, l] <= 0)
					{
						if (!this.OutOfMap(k - 1, l) && this.mapOld[k - 1, l] == 1 && this.mapRemove[k - 1, l] <= 0)
						{
							this.mapOld[k - 1, l] = 2;
							flag3 = true;
						}
						if (!this.OutOfMap(k + 1, l) && this.mapOld[k + 1, l] == 1 && this.mapRemove[k + 1, l] <= 0)
						{
							this.mapOld[k + 1, l] = 2;
							flag3 = true;
						}
						if (!this.OutOfMap(k, l - 1) && this.mapOld[k, l - 1] == 1 && this.mapRemove[k, l - 1] <= 0)
						{
							this.mapOld[k, l - 1] = 2;
							flag3 = true;
						}
						if (!this.OutOfMap(k, l + 1) && this.mapOld[k, l + 1] == 1 && this.mapRemove[k, l + 1] <= 0)
						{
							this.mapOld[k, l + 1] = 2;
							flag3 = true;
						}
					}
				}
			}
			if (!flag3)
			{
				flag2 = true;
			}
			num++;
			if (num > 100000)
			{
				flag2 = true;
			}
		}
		bool result = false;
		for (int m = 0; m < mapScript.mapSizeX; m++)
		{
			for (int n = 0; n < mapScript.mapSizeY; n++)
			{
				if (this.mapOld[m, n] > 0 && this.mapOld[m, n] != 2 && this.mapRemove[m, n] <= 0)
				{
					result = true;
				}
				if (this.mapOld[m, n] > 0)
				{
					this.mapOld[m, n] = 1;
				}
			}
		}
		return result;
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x0011C4F3 File Offset: 0x0011A6F3
	private bool OutOfMap(int x, int y)
	{
		return x < 0 || x >= mapScript.mapSizeX || y < 0 || y >= mapScript.mapSizeY;
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x0011C518 File Offset: 0x0011A718
	public void Remove_DeleteMap()
	{
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRemove[i, j] != 0)
				{
					this.mapRemove[i, j] = 0;
					if (this.mapRoomGO[i, j])
					{
						UnityEngine.Object.Destroy(this.mapRoomGO[i, j]);
					}
				}
			}
		}
		this.makeUpdate = true;
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x0011C590 File Offset: 0x0011A790
	private void DeleteComplete()
	{
		Debug.Log("DeleteComplete()");
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				this.mapOld[i, j] = 0;
				this.mapNew[i, j] = 0;
				this.mapDoors[i, j] = 0;
				this.mapWindows[i, j] = 0;
				this.mapRemove[i, j] = 0;
				if (this.mapRoomGO[i, j])
				{
					UnityEngine.Object.Destroy(this.mapRoomGO[i, j]);
				}
				this.doorsPlaced[i, j] = false;
			}
		}
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x0011C64C File Offset: 0x0011A84C
	public void CreateOldRoomLayout(roomScript script_)
	{
		this.moneyRedesign = 0;
		this.replaceRoomID = script_.myID;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapScript_.mapRoomID[i, j] == script_.myID)
				{
					this.moneyRedesign += this.rdS_.roomData_PRICE[script_.typ];
					this.mapOld[i, j] = 1;
					if (this.mapScript_.mapWindows[i, j] > 0)
					{
						this.mapWindows[i, j] = this.mapScript_.mapWindows[i, j];
						this.mapWindows[i + 1, j] = this.mapScript_.mapWindows[i + 1, j];
						this.mapWindows[i - 1, j] = this.mapScript_.mapWindows[i - 1, j];
						this.mapWindows[i, j + 1] = this.mapScript_.mapWindows[i, j + 1];
						this.mapWindows[i, j - 1] = this.mapScript_.mapWindows[i, j - 1];
					}
					if (this.mapScript_.mapDoors[i, j] > 0)
					{
						this.mapDoors[i, j] = this.mapScript_.mapDoors[i, j];
						this.mapDoors[i + 1, j] = this.mapScript_.mapDoors[i + 1, j];
						this.mapDoors[i - 1, j] = this.mapScript_.mapDoors[i - 1, j];
						this.mapDoors[i, j + 1] = this.mapScript_.mapDoors[i, j + 1];
						this.mapDoors[i, j - 1] = this.mapScript_.mapDoors[i, j - 1];
					}
				}
			}
		}
		this.mapScript_.RemoveRoom(script_.myID, false);
		this.makeUpdate = true;
		this.CreateRoomVorschau();
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x0011C888 File Offset: 0x0011AA88
	private Vector2 GetAussenrand()
	{
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapMove[i, j] > 0)
				{
					return new Vector2((float)i, (float)j);
				}
			}
		}
		return new Vector2(0f, 0f);
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x0011C8E0 File Offset: 0x0011AAE0
	public void MoveRoom(roomScript script_)
	{
		this.moveRoomScript_ = script_;
		this.modus = 4;
		this.mapMove = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.moveRoomID = script_.myID;
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapScript_.mapRoomID[i, j] == script_.myID)
				{
					this.mapMove[i, j] = 1;
				}
			}
		}
	}

	// Token: 0x0400239B RID: 9115
	private mainScript mS_;

	// Token: 0x0400239C RID: 9116
	private mapScript mapScript_;

	// Token: 0x0400239D RID: 9117
	private GUI_Main guiMain;

	// Token: 0x0400239E RID: 9118
	private Camera myCamera;

	// Token: 0x0400239F RID: 9119
	private Seeker seeker;

	// Token: 0x040023A0 RID: 9120
	private mainCameraScript mCamS_;

	// Token: 0x040023A1 RID: 9121
	private sfxScript sfx_;

	// Token: 0x040023A2 RID: 9122
	private roomDataScript rdS_;

	// Token: 0x040023A3 RID: 9123
	public bool activ;

	// Token: 0x040023A4 RID: 9124
	public GameObject[] pointers;

	// Token: 0x040023A5 RID: 9125
	public GameObject[] pointersInstantiate;

	// Token: 0x040023A6 RID: 9126
	public GameObject[] prefabRoomElements;

	// Token: 0x040023A7 RID: 9127
	public GameObject[] prefabParticles;

	// Token: 0x040023A8 RID: 9128
	public GameObject roomMainObject;

	// Token: 0x040023A9 RID: 9129
	public int posX;

	// Token: 0x040023AA RID: 9130
	public int posY;

	// Token: 0x040023AB RID: 9131
	public int modus;

	// Token: 0x040023AC RID: 9132
	public bool noPath;

	// Token: 0x040023AD RID: 9133
	public int replaceRoomID = -1;

	// Token: 0x040023AE RID: 9134
	public int moveRoomID = -1;

	// Token: 0x040023AF RID: 9135
	private bool error;

	// Token: 0x040023B0 RID: 9136
	private bool errorDoor;

	// Token: 0x040023B1 RID: 9137
	private bool errorWindow;

	// Token: 0x040023B2 RID: 9138
	public int moneyRedesign;

	// Token: 0x040023B3 RID: 9139
	public int[,] mapOld;

	// Token: 0x040023B4 RID: 9140
	private int[,] mapNew;

	// Token: 0x040023B5 RID: 9141
	private int[,] mapDoors;

	// Token: 0x040023B6 RID: 9142
	private int[,] mapWindows;

	// Token: 0x040023B7 RID: 9143
	private int[,] mapRemove;

	// Token: 0x040023B8 RID: 9144
	private int[,] mapMove;

	// Token: 0x040023B9 RID: 9145
	private bool[,] doorsPlaced;

	// Token: 0x040023BA RID: 9146
	private GameObject[,] mapRoomGO;

	// Token: 0x040023BB RID: 9147
	public int roomStartX;

	// Token: 0x040023BC RID: 9148
	public int roomStartY;

	// Token: 0x040023BD RID: 9149
	private bool makeUpdate;

	// Token: 0x040023BE RID: 9150
	private bool makeUpdateMoveRoom;

	// Token: 0x040023BF RID: 9151
	private RaycastHit hit;

	// Token: 0x040023C0 RID: 9152
	private GameObject roomVorschau;

	// Token: 0x040023C1 RID: 9153
	private Vector3[] endVectors;

	// Token: 0x040023C2 RID: 9154
	private roomScript moveRoomScript_;
}
