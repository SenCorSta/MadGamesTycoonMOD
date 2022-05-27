using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class mapScript : MonoBehaviour
{
	// Token: 0x06001D7F RID: 7551 RVA: 0x0012484A File Offset: 0x00122A4A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x00124854 File Offset: 0x00122A54
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.brS_)
		{
			this.brS_ = base.GetComponent<buildRoomScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = base.GetComponent<roomDataScript>();
		}
		if (!this.ROOMS)
		{
			this.ROOMS = GameObject.Find("ROOMS");
		}
		if (!this.ROOMS_MP)
		{
			this.ROOMS_MP = GameObject.Find("ROOMS_MP");
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x00124908 File Offset: 0x00122B08
	private void Update()
	{
		this.UpdateMapMuell(false);
		this.UpdateMapFilter(false);
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x00124918 File Offset: 0x00122B18
	public void InitBuilding(bool fromSavegame)
	{
		Debug.Log("InitBuilding()");
		GameObject[] array = GameObject.FindGameObjectsWithTag("BlockFloor");
		if (array.Length == 0)
		{
			return;
		}
		foreach (GameObject gameObject in array)
		{
			this.mapBlock[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)] = 1;
		}
		GameObject[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("BlockDoor");
		if (array3.Length == 0)
		{
			return;
		}
		foreach (GameObject gameObject2 in array3)
		{
			this.mapBlock[Mathf.RoundToInt(gameObject2.transform.position.x), Mathf.RoundToInt(gameObject2.transform.position.z)] = 1;
			this.mapBlockDoor[Mathf.RoundToInt(gameObject2.transform.position.x), Mathf.RoundToInt(gameObject2.transform.position.z)] = 1;
		}
		array2 = array3;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		GameObject[] array4 = GameObject.FindGameObjectsWithTag("BuildingFloor");
		if (array4.Length == 0)
		{
			return;
		}
		foreach (GameObject gameObject3 in array4)
		{
			this.mapRoomID[Mathf.RoundToInt(gameObject3.transform.position.x), Mathf.RoundToInt(gameObject3.transform.position.z)] = mapScript.ID_FLOOR;
			this.mapBuilding[Mathf.RoundToInt(gameObject3.transform.position.x), Mathf.RoundToInt(gameObject3.transform.position.z)] = gameObject3.GetComponent<buildingFloor>().buildingID;
		}
		array2 = array4;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		foreach (GameObject gameObject4 in GameObject.FindGameObjectsWithTag("BuildingWindow"))
		{
			int num = Mathf.RoundToInt(gameObject4.transform.position.x);
			int num2 = Mathf.RoundToInt(gameObject4.transform.position.z);
			this.mapWindows[num, num2] = 99;
			float y = gameObject4.transform.eulerAngles.y;
			if (!270f.Equals(y))
			{
				if (!90f.Equals(y))
				{
					if (!0f.Equals(y))
					{
						if (180f.Equals(y))
						{
							this.mapWindows[num - 1, num2] = 99;
						}
					}
					else
					{
						this.mapWindows[num + 1, num2] = 99;
					}
				}
				else
				{
					this.mapWindows[num, num2 - 1] = 99;
				}
			}
			else
			{
				this.mapWindows[num, num2 + 1] = 99;
			}
		}
		foreach (GameObject gameObject5 in GameObject.FindGameObjectsWithTag("BuildingDoor"))
		{
			int num3 = Mathf.RoundToInt(gameObject5.transform.position.x);
			int num4 = Mathf.RoundToInt(gameObject5.transform.position.z);
			this.mapDoors[num3, num4] = 99;
			float y = gameObject5.transform.eulerAngles.y;
			if (!270f.Equals(y))
			{
				if (!90f.Equals(y))
				{
					if (!0f.Equals(y))
					{
						if (180f.Equals(y))
						{
							this.mapDoors[num3 - 1, num4] = 99;
						}
					}
					else
					{
						this.mapDoors[num3 + 1, num4] = 99;
					}
				}
				else
				{
					this.mapDoors[num3, num4 - 1] = 99;
				}
			}
			else
			{
				this.mapDoors[num3, num4 + 1] = 99;
			}
		}
		if (this.mS_.multiplayer)
		{
			for (int j = 0; j < mapScript.mapSizeX; j++)
			{
				for (int k = 0; k < mapScript.mapSizeY; k++)
				{
					this.mapRoomID_LAYOUT[j, k] = this.mapRoomID[j, k];
					this.mapWindows_LAYOUT[j, k] = this.mapWindows[j, k];
					this.mapDoors_LAYOUT[j, k] = this.mapDoors[j, k];
				}
			}
			if (!fromSavegame)
			{
				for (int l = 0; l < this.mS_.mpCalls_.playersMP.Count; l++)
				{
					this.mS_.mpCalls_.playersMP[l].mapRoomID = (int[,])this.mapRoomID_LAYOUT.Clone();
					this.mS_.mpCalls_.playersMP[l].mapWindows = (int[,])this.mapWindows_LAYOUT.Clone();
					this.mS_.mpCalls_.playersMP[l].mapDoors = (int[,])this.mapDoors_LAYOUT.Clone();
				}
			}
		}
		if (!fromSavegame)
		{
			this.CreateRoomsForBuildingsToBuy();
		}
		this.CreateWalls(-1);
		this.UpdatePathfindingInstant();
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00124EB8 File Offset: 0x001230B8
	public void CreateRoomsForBuildingsToBuy()
	{
		roomScript roomScript = null;
		for (int i = 2; i < 10; i++)
		{
			if (roomScript)
			{
				roomScript.uiPos = this.brS_.FindUiPositionExtern(roomScript.myID);
			}
			bool flag = false;
			roomScript = null;
			for (int j = 0; j < mapScript.mapSizeX; j++)
			{
				for (int k = 0; k < mapScript.mapSizeY; k++)
				{
					if (this.mapBuilding[j, k] == i)
					{
						int num = i;
						this.mapRoomID[j, k] = num;
						if (!flag)
						{
							flag = true;
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.brS_.roomMainObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
							roomScript = gameObject.GetComponent<roomScript>();
							gameObject.name = "Room_" + num;
							roomScript.myID = num;
							roomScript.typ = 16;
							roomScript.taskID = -1;
							roomScript.myName = "";
							roomScript.pause = false;
							roomScript.lockKI = false;
							gameObject.transform.position = new Vector3(roomScript.uiPos.x, 0f, roomScript.uiPos.z);
						}
						this.mapRoomScript[j, k] = roomScript;
					}
				}
			}
		}
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x00125010 File Offset: 0x00123210
	public void RemoveRoom(int id_, bool particle)
	{
		Debug.Log("RemoveRoom()");
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRoomID[i, j] == id_)
				{
					this.mapRoomID[i, j] = mapScript.ID_FLOOR;
					this.mapRoomScript[i, j] = null;
					this.mapDoors[i, j] = 0;
					if (this.mapWindows[i, j] != 99)
					{
						this.mapWindows[i, j] = 0;
					}
					if (particle)
					{
						base.StartCoroutine(this.brS_.CreateParticleDemolish(i, j, UnityEngine.Random.Range(0f, 0.2f)));
					}
					this.mS_.Multiplayer_SendMap(i, j);
				}
			}
		}
		for (int k = 0; k < mapScript.mapSizeX; k++)
		{
			for (int l = 0; l < mapScript.mapSizeY; l++)
			{
				if (this.mapDoors[k, l] == 1 && this.IsInMapLimit(k + 1, l) && this.IsInMapLimit(k - 1, l) && this.IsInMapLimit(k, l + 1) && this.IsInMapLimit(k, l - 1) && this.mapDoors[k + 1, l] != 1 && this.mapDoors[k - 1, l] != 1 && this.mapDoors[k, l + 1] != 1 && this.mapDoors[k, l - 1] != 1)
				{
					Debug.Log("DoorError Removed");
					this.mapDoors[k, l] = 0;
					this.mS_.Multiplayer_SendMap(k, l);
				}
			}
		}
		for (int m = 0; m < mapScript.mapSizeX; m++)
		{
			for (int n = 0; n < mapScript.mapSizeY; n++)
			{
				if (this.mapWindows[m, n] == 1 && this.IsInMapLimit(m + 1, n) && this.IsInMapLimit(m - 1, n) && this.IsInMapLimit(m, n + 1) && this.IsInMapLimit(m, n - 1) && this.mapWindows[m + 1, n] != 1 && this.mapWindows[m - 1, n] != 1 && this.mapWindows[m, n + 1] != 1 && this.mapWindows[m, n - 1] != 1)
				{
					Debug.Log("WindowsError Removed");
					this.mapWindows[m, n] = 0;
					this.mS_.Multiplayer_SendMap(m, n);
				}
			}
		}
		this.CreateWalls(-1);
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x001252D2 File Offset: 0x001234D2
	public void UpdatePathfindingInstant()
	{
		base.StartCoroutine(this.UpdatePathfindingInstantNextFrame());
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x001252E1 File Offset: 0x001234E1
	public void UpdatePathfinding()
	{
		base.StartCoroutine(this.UpdatePathfindingNextFrame());
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x001252F0 File Offset: 0x001234F0
	private IEnumerator UpdatePathfindingNextFrame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (!this.aStar_)
		{
			this.aStar_ = GameObject.Find("AStar").GetComponent<AstarPath>();
		}
		if (this.aStar_)
		{
			this.aStar_.Scan(null);
		}
		yield break;
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x001252FF File Offset: 0x001234FF
	private IEnumerator UpdatePathfindingInstantNextFrame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (!this.aStar_)
		{
			this.aStar_ = GameObject.Find("AStar").GetComponent<AstarPath>();
		}
		if (this.aStar_)
		{
			this.aStar_.Scan(null);
		}
		yield break;
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x00125310 File Offset: 0x00123510
	public void CreateWalls(int buildingID)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] && (this.mapBuilding[Mathf.RoundToInt(array[i].transform.position.x), Mathf.RoundToInt(array[i].transform.position.z)] == buildingID || buildingID == -1))
			{
				array[i].GetComponent<roomScript>().listGameObjects.Clear();
			}
		}
		for (int j = 0; j < this.ROOMS.transform.childCount; j++)
		{
			Transform child = this.ROOMS.transform.GetChild(j);
			if (this.mapBuilding[Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.z)] == buildingID || buildingID == -1)
			{
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
		this.doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];
		GameObject gameObject = null;
		roomScript script_ = null;
		int num = -1;
		for (int k = 0; k < mapScript.mapSizeX; k++)
		{
			for (int l = 0; l < mapScript.mapSizeY; l++)
			{
				if ((this.mapBuilding[k, l] == buildingID || buildingID == -1) && this.mapRoomID[k, l] > 0)
				{
					if (num != this.mapRoomID[k, l])
					{
						script_ = null;
						num = this.mapRoomID[k, l];
						gameObject = GameObject.Find("Room_" + num.ToString());
						if (gameObject)
						{
							script_ = gameObject.GetComponent<roomScript>();
						}
					}
					this.InstantiateMap(k, l, gameObject, script_);
				}
			}
		}
		this.UpdatePathfinding();
		this.guiMain_.filterToggles = -1;
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x001254EC File Offset: 0x001236EC
	public void CreateWalls_Multiplayer(int playerID_)
	{
		player_mp player_mp = this.mS_.mpCalls_.FindPlayer(playerID_);
		for (int i = 0; i < this.ROOMS_MP.transform.childCount; i++)
		{
			Transform child = this.ROOMS_MP.transform.GetChild(i);
			if (child)
			{
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
		this.doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];
		for (int j = 0; j < mapScript.mapSizeX; j++)
		{
			for (int k = 0; k < mapScript.mapSizeY; k++)
			{
				if (player_mp.mapRoomID[j, k] > 0)
				{
					this.InstantiateMap_Multiplayer(player_mp, j, k, player_mp.mapRoomID[j, k], player_mp.mapRoomTyp[j, k]);
				}
			}
		}
		this.guiMain_.filterToggles = -1;
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x001255C8 File Offset: 0x001237C8
	private void InstantiateMap(int x, int y, GameObject room, roomScript script_)
	{
		if (this.mapBlockDoor[x, y] != 0)
		{
			return;
		}
		int roomTyp = 0;
		if (!room)
		{
			roomTyp = 0;
		}
		else if (script_)
		{
			roomTyp = script_.typ;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetFloorPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
		if (this.mapBlock[x, y] != 0)
		{
			gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = this.mS_.specialMaterials[4];
			gameObject.GetComponent<floorScript>().materials[0] = this.mS_.specialMaterials[4];
		}
		this.SetRoomParent(gameObject);
		if (script_)
		{
			script_.listGameObjects.Add(gameObject);
		}
		if (this.mapDoors[x, y] != 99 && this.mapDoors[x, y] > 0 && this.mapRoomID[x, y] == 1)
		{
			this.mapDoors[x, y] = 0;
		}
		if (this.mapDoors[x, y] == 1 && this.mapRoomID[x, y] >= 1)
		{
			this.mapDoors[x, y] = 0;
			this.guiMain_.OpenMenu(false);
			this.guiMain_.MessageBox("Your savegame is out of date. You need to reset the doors of your rooms.", true);
		}
		if (this.mapWindows[x, y] != 99 && this.mapWindows[x, y] > 0 && this.mapWindows[x + 1, y] == 0 && this.mapWindows[x - 1, y] == 0 && this.mapWindows[x, y + 1] == 0 && this.mapWindows[x, y - 1] == 0)
		{
			this.mapWindows[x, y] = 0;
		}
		if (this.mapWindows[x, y] != 99 && this.mapWindows[x, y] > 0 && this.mapRoomID[x, y] == 1 && (this.mapWindows[x + 1, y] == 0 || this.mapRoomID[x + 1, y] <= 1) && (this.mapWindows[x - 1, y] == 0 || this.mapRoomID[x - 1, y] <= 1) && (this.mapWindows[x, y + 1] == 0 || this.mapRoomID[x, y + 1] <= 1) && (this.mapWindows[x, y - 1] == 0 || this.mapRoomID[x, y - 1] <= 1))
		{
			this.mapWindows[x, y] = 0;
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (this.mapRoomID[x, y] == mapScript.ID_FLOOR)
		{
			if (this.mapDoors[x + 1, y] == 1180)
			{
				flag = true;
			}
			if (this.mapDoors[x - 1, y] == 1000)
			{
				flag3 = true;
			}
			if (this.mapDoors[x, y + 1] == 1090)
			{
				flag4 = true;
			}
			if (this.mapDoors[x, y - 1] == 1270)
			{
				flag2 = true;
			}
		}
		if (this.mapDoors[x, y] > 0 && this.mapDoors[x, y] > 0 && this.mapRoomID[x, y] != 0 && !this.doorsPlaced[x, y] && this.mapDoors[x, y] != 99)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetDoorPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, (float)(this.mapDoors[x, y] - 1000), 0f);
			this.SetRoomParent(gameObject);
			script_.myDoor = gameObject;
			int num = this.mapDoors[x, y] - 1000;
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
		bool flag5 = false;
		bool flag6 = false;
		bool flag7 = false;
		bool flag8 = false;
		if (this.mapWindows[x, y] > 0)
		{
			if (this.mapWindows[x, y + 1] > 0 && this.mapRoomID[x, y + 1] != this.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
				this.SetRoomParent(gameObject);
				flag8 = true;
			}
			if (this.mapWindows[x, y - 1] > 0 && this.mapRoomID[x, y - 1] != this.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.SetRoomParent(gameObject);
				flag6 = true;
			}
			if (this.mapWindows[x + 1, y] > 0 && this.mapRoomID[x + 1, y] != this.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				this.SetRoomParent(gameObject);
				flag5 = true;
			}
			if (this.mapWindows[x - 1, y] > 0 && this.mapRoomID[x - 1, y] != this.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
				this.SetRoomParent(gameObject);
				flag7 = true;
			}
		}
		if (this.mapRoomID[x + 1, y] != this.mapRoomID[x, y] && !flag && !flag5)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x - 1, y] != this.mapRoomID[x, y] && !flag3 && !flag7)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y + 1] != this.mapRoomID[x, y] && !flag4 && !flag8)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y - 1] != this.mapRoomID[x, y] && !flag2 && !flag6)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y - 1] == this.mapRoomID[x, y] && this.mapRoomID[x - 1, y] == this.mapRoomID[x, y] && this.mapRoomID[x - 1, y - 1] != this.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y - 1] == this.mapRoomID[x, y] && this.mapRoomID[x + 1, y] == this.mapRoomID[x, y] && this.mapRoomID[x + 1, y - 1] != this.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y + 1] == this.mapRoomID[x, y] && this.mapRoomID[x - 1, y] == this.mapRoomID[x, y] && this.mapRoomID[x - 1, y + 1] != this.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			this.SetRoomParent(gameObject);
		}
		if (this.mapRoomID[x, y + 1] == this.mapRoomID[x, y] && this.mapRoomID[x + 1, y] == this.mapRoomID[x, y] && this.mapRoomID[x + 1, y + 1] != this.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTyp)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.SetRoomParent(gameObject);
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x00126098 File Offset: 0x00124298
	private void InstantiateMap_Multiplayer(player_mp p, int x, int y, int roomID_MP, int roomTYP_MP)
	{
		if (this.mapBlockDoor[x, y] != 0)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetFloorPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
		if (this.mapBlock[x, y] != 0)
		{
			gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = this.mS_.specialMaterials[4];
			UnityEngine.Object.Destroy(gameObject.GetComponent<floorScript>());
		}
		this.SetRoomParent_Multiplayer(gameObject);
		if (p.mapDoors[x, y] != 99 && p.mapDoors[x, y] > 0 && p.mapRoomID[x, y] == 1)
		{
			p.mapDoors[x, y] = 0;
		}
		if (p.mapWindows[x, y] != 99 && p.mapWindows[x, y] > 0 && p.mapWindows[x + 1, y] == 0 && p.mapWindows[x - 1, y] == 0 && p.mapWindows[x, y + 1] == 0 && p.mapWindows[x, y - 1] == 0)
		{
			p.mapWindows[x, y] = 0;
		}
		if (p.mapWindows[x, y] != 99 && p.mapWindows[x, y] > 0 && p.mapRoomID[x, y] == 1 && (p.mapWindows[x + 1, y] == 0 || p.mapRoomID[x + 1, y] <= 1) && (p.mapWindows[x - 1, y] == 0 || p.mapRoomID[x - 1, y] <= 1) && (p.mapWindows[x, y + 1] == 0 || p.mapRoomID[x, y + 1] <= 1) && (p.mapWindows[x, y - 1] == 0 || p.mapRoomID[x, y - 1] <= 1))
		{
			p.mapWindows[x, y] = 0;
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (p.mapRoomID[x, y] == mapScript.ID_FLOOR)
		{
			if (p.mapDoors[x + 1, y] == 1180)
			{
				flag = true;
			}
			if (p.mapDoors[x - 1, y] == 1000)
			{
				flag3 = true;
			}
			if (p.mapDoors[x, y + 1] == 1090)
			{
				flag4 = true;
			}
			if (p.mapDoors[x, y - 1] == 1270)
			{
				flag2 = true;
			}
		}
		if (p.mapDoors[x, y] > 0 && p.mapDoors[x, y] > 0 && p.mapRoomID[x, y] != 0 && !this.doorsPlaced[x, y] && p.mapDoors[x, y] != 99)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetDoorPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, (float)(p.mapDoors[x, y] - 1000), 0f);
			this.SetRoomParent_Multiplayer(gameObject);
			int num = p.mapDoors[x, y] - 1000;
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
		bool flag5 = false;
		bool flag6 = false;
		bool flag7 = false;
		bool flag8 = false;
		if (p.mapWindows[x, y] > 0)
		{
			if (p.mapWindows[x, y + 1] > 0 && p.mapRoomID[x, y + 1] != p.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
				this.SetRoomParent_Multiplayer(gameObject);
				flag8 = true;
			}
			if (p.mapWindows[x, y - 1] > 0 && p.mapRoomID[x, y - 1] != p.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.SetRoomParent_Multiplayer(gameObject);
				flag6 = true;
			}
			if (p.mapWindows[x + 1, y] > 0 && p.mapRoomID[x + 1, y] != p.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				this.SetRoomParent_Multiplayer(gameObject);
				flag5 = true;
			}
			if (p.mapWindows[x - 1, y] > 0 && p.mapRoomID[x - 1, y] != p.mapRoomID[x, y])
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWindowPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
				this.SetRoomParent_Multiplayer(gameObject);
				flag7 = true;
			}
		}
		if (p.mapRoomID[x + 1, y] != p.mapRoomID[x, y] && !flag && !flag5)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x - 1, y] != p.mapRoomID[x, y] && !flag3 && !flag7)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y + 1] != p.mapRoomID[x, y] && !flag4 && !flag8)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y - 1] != p.mapRoomID[x, y] && !flag2 && !flag6)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetWallPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y - 1] == p.mapRoomID[x, y] && p.mapRoomID[x - 1, y] == p.mapRoomID[x, y] && p.mapRoomID[x - 1, y - 1] != p.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y - 1] == p.mapRoomID[x, y] && p.mapRoomID[x + 1, y] == p.mapRoomID[x, y] && p.mapRoomID[x + 1, y - 1] != p.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y + 1] == p.mapRoomID[x, y] && p.mapRoomID[x - 1, y] == p.mapRoomID[x, y] && p.mapRoomID[x - 1, y + 1] != p.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
		if (p.mapRoomID[x, y + 1] == p.mapRoomID[x, y] && p.mapRoomID[x + 1, y] == p.mapRoomID[x, y] && p.mapRoomID[x + 1, y + 1] != p.mapRoomID[x, y])
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsWalls[this.GetEdgeOutPrefab(roomTYP_MP)], new Vector3((float)x, 0f, (float)y), Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.SetRoomParent_Multiplayer(gameObject);
		}
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x00126AD4 File Offset: 0x00124CD4
	private int GetFloorPrefab(int roomTyp)
	{
		switch (roomTyp)
		{
		case 0:
			return 3;
		case 1:
			return 1;
		case 2:
			return 12;
		case 3:
			return 55;
		case 4:
			return 60;
		case 5:
			return 65;
		case 6:
			return 15;
		case 7:
			return 50;
		case 8:
			return 85;
		case 9:
			return 40;
		case 10:
			return 70;
		case 11:
			return 20;
		case 12:
			return 25;
		case 13:
			return 45;
		case 14:
			return 30;
		case 15:
			return 35;
		case 16:
			return 75;
		case 17:
			return 80;
		default:
			return 3;
		}
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x00126B68 File Offset: 0x00124D68
	private int GetWallPrefab(int roomTyp)
	{
		switch (roomTyp)
		{
		case 0:
			return 2;
		case 1:
			return 0;
		case 2:
			return 5;
		case 3:
			return 54;
		case 4:
			return 59;
		case 5:
			return 64;
		case 6:
			return 14;
		case 7:
			return 49;
		case 8:
			return 84;
		case 9:
			return 39;
		case 10:
			return 69;
		case 11:
			return 19;
		case 12:
			return 24;
		case 13:
			return 44;
		case 14:
			return 29;
		case 15:
			return 34;
		case 16:
			return 74;
		case 17:
			return 79;
		default:
			return 2;
		}
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x00126BFC File Offset: 0x00124DFC
	private int GetEdgeOutPrefab(int roomTyp)
	{
		switch (roomTyp)
		{
		case 0:
			return 6;
		case 1:
			return 7;
		case 2:
			return 13;
		case 3:
			return 56;
		case 4:
			return 61;
		case 5:
			return 66;
		case 6:
			return 16;
		case 7:
			return 51;
		case 8:
			return 86;
		case 9:
			return 41;
		case 10:
			return 71;
		case 11:
			return 21;
		case 12:
			return 26;
		case 13:
			return 46;
		case 14:
			return 31;
		case 15:
			return 36;
		case 16:
			return 76;
		case 17:
			return 81;
		default:
			return 6;
		}
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x00126C90 File Offset: 0x00124E90
	private int GetDoorPrefab(int roomTyp)
	{
		switch (roomTyp)
		{
		case 1:
			return 9;
		case 2:
			return 4;
		case 3:
			return 57;
		case 4:
			return 62;
		case 5:
			return 67;
		case 6:
			return 17;
		case 7:
			return 52;
		case 8:
			return 87;
		case 9:
			return 42;
		case 10:
			return 72;
		case 11:
			return 22;
		case 12:
			return 27;
		case 13:
			return 47;
		case 14:
			return 32;
		case 15:
			return 37;
		case 16:
			return 77;
		case 17:
			return 82;
		default:
			return 8;
		}
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x00126D20 File Offset: 0x00124F20
	private int GetWindowPrefab(int roomTyp)
	{
		switch (roomTyp)
		{
		case 0:
			return 10;
		case 1:
			return 11;
		case 2:
			return 8;
		case 3:
			return 58;
		case 4:
			return 63;
		case 5:
			return 68;
		case 6:
			return 18;
		case 7:
			return 53;
		case 8:
			return 88;
		case 9:
			return 43;
		case 10:
			return 73;
		case 11:
			return 23;
		case 12:
			return 28;
		case 13:
			return 48;
		case 14:
			return 33;
		case 15:
			return 38;
		case 16:
			return 78;
		case 17:
			return 83;
		default:
			return 10;
		}
	}

	// Token: 0x06001D92 RID: 7570 RVA: 0x00126DB4 File Offset: 0x00124FB4
	private void SetRoomParent(GameObject go)
	{
		go.transform.SetParent(this.ROOMS.transform, true);
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x00126DD0 File Offset: 0x00124FD0
	private void SetRoomParent_Multiplayer(GameObject go)
	{
		go.transform.SetParent(this.ROOMS_MP.transform, true);
		go.transform.GetChild(0).tag = "Untagged";
		go.transform.GetChild(0).transform.parent = this.ROOMS_MP.transform;
		UnityEngine.Object.Destroy(go);
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00126E34 File Offset: 0x00125034
	public int GetAmountRooms(int t)
	{
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (t == component.typ)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x00126E73 File Offset: 0x00125073
	public GameObject CreateObject(int t)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsInventar[t]);
		gameObject.transform.position = new Vector3(0f, 9999f, 0f);
		gameObject.GetComponent<objectScript>().InitNewObject(t);
		return gameObject;
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00126EB0 File Offset: 0x001250B0
	public Vector2 FindRandomFloorInMyBuilding(int id)
	{
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRoomID[i, j] == mapScript.ID_FLOOR && this.mapBuilding[i, j] == id)
				{
					list.Add(new Vector2((float)i, (float)j));
				}
			}
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x00126F28 File Offset: 0x00125128
	public Vector2 FindRandomFloor()
	{
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				if (this.mapRoomID[i, j] == mapScript.ID_FLOOR)
				{
					list.Add(new Vector2((float)i, (float)j));
				}
			}
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x00126F90 File Offset: 0x00125190
	public bool IsInMapLimit(int x, int y)
	{
		return x >= 0 && x <= mapScript.mapSizeX && y >= 0 && y <= mapScript.mapSizeY;
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x00126FB0 File Offset: 0x001251B0
	public void UpdateMapMuell(bool force)
	{
		if (!force)
		{
			this.muellTimer += Time.deltaTime;
			if (this.muellTimer < 1f)
			{
				return;
			}
			this.muellTimer = 0f;
		}
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				this.mapMuell[i, j] = 0f;
			}
		}
		for (int k = 0; k < this.mS_.arrayMuell.Length; k++)
		{
			if (this.mS_.arrayMuell[k])
			{
				int num = Mathf.RoundToInt(this.mS_.arrayMuell[k].transform.position.x);
				int num2 = Mathf.RoundToInt(this.mS_.arrayMuell[k].transform.position.z);
				if (this.IsInMapLimit(num, num2))
				{
					this.mapMuell[num, num2] = 1f;
				}
				for (int l = num - 4; l < num + 4; l++)
				{
					for (int m = num2 - 4; m < num2 + 4; m++)
					{
						float num3 = Vector2.Distance(new Vector2((float)num, (float)num2), new Vector2((float)l, (float)m));
						if (num3 < 8f && this.IsInMapLimit(l, m) && this.IsInMapLimit(num, num2) && this.mapRoomID[num, num2] == this.mapRoomID[l, m])
						{
							this.mapMuell[l, m] += 1f / (num3 + 0.1f);
						}
					}
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Muell_InUse");
		if (array.Length != 0)
		{
			for (int n = 0; n < array.Length; n++)
			{
				if (array[n])
				{
					int num4 = Mathf.RoundToInt(array[n].transform.position.x);
					int num5 = Mathf.RoundToInt(array[n].transform.position.z);
					if (this.IsInMapLimit(num4, num5))
					{
						this.mapMuell[num4, num5] = 1f;
					}
					for (int num6 = num4 - 4; num6 < num4 + 4; num6++)
					{
						for (int num7 = num5 - 4; num7 < num5 + 4; num7++)
						{
							float num8 = Vector2.Distance(new Vector2((float)num4, (float)num5), new Vector2((float)num6, (float)num7));
							if (num8 < 8f && num8 > 0f && this.IsInMapLimit(num6, num7) && this.IsInMapLimit(num4, num5) && this.mapRoomID[num4, num5] == this.mapRoomID[num6, num7])
							{
								this.mapMuell[num6, num7] += 1f / (num8 + 0.1f);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x001272BC File Offset: 0x001254BC
	public void UpdateMapFilter(bool force)
	{
		if (!force)
		{
			this.updateMapFilterTimer += Time.deltaTime;
			if (this.updateMapFilterTimer < 1f)
			{
				return;
			}
			this.updateMapFilterTimer = 0f;
		}
		for (int i = 0; i < mapScript.mapSizeX; i++)
		{
			for (int j = 0; j < mapScript.mapSizeY; j++)
			{
				this.mapWaerme[i, j] = 0f;
				this.mapAusstattung[i, j] = 0f;
			}
		}
		for (int k = 0; k < this.mS_.arrayObjects.Length; k++)
		{
			if (this.mS_.arrayObjects[k])
			{
				objectScript component = this.mS_.arrayObjects[k].GetComponent<objectScript>();
				if (component)
				{
					if (component.waerme > 0f || component.kaelte > 0f)
					{
						float num = component.waerme;
						num -= component.kaelte;
						if (component.kaelte == 5f)
						{
							num -= 3f;
						}
						int num2 = Mathf.RoundToInt(this.mS_.arrayObjects[k].transform.position.x);
						int num3 = Mathf.RoundToInt(this.mS_.arrayObjects[k].transform.position.z);
						if (this.IsInMapLimit(num2, num3))
						{
							this.mapWaerme[num2, num3] += num * 0.2f;
						}
						for (int l = num2 - 8; l < num2 + 8; l++)
						{
							for (int m = num3 - 8; m < num3 + 8; m++)
							{
								float num4 = Vector2.Distance(new Vector2((float)num2, (float)num3), new Vector2((float)l, (float)m));
								if (num4 < 8f && num4 > 0f && this.IsInMapLimit(l, m) && this.IsInMapLimit(num2, num3) && this.mapRoomID[num2, num3] == this.mapRoomID[l, m])
								{
									this.mapWaerme[l, m] += num * 0.2f / (num4 + 0.1f);
								}
							}
						}
					}
					if (component.ausstattung > 0f)
					{
						float ausstattung = component.ausstattung;
						int num5 = Mathf.RoundToInt(this.mS_.arrayObjects[k].transform.position.x);
						int num6 = Mathf.RoundToInt(this.mS_.arrayObjects[k].transform.position.z);
						if (this.IsInMapLimit(num5, num6))
						{
							this.mapAusstattung[num5, num6] += ausstattung * 0.2f;
						}
						for (int n = num5 - 8; n < num5 + 8; n++)
						{
							for (int num7 = num6 - 8; num7 < num6 + 8; num7++)
							{
								float num8 = Vector2.Distance(new Vector2((float)num5, (float)num6), new Vector2((float)n, (float)num7));
								if (num8 < 8f && num8 > 0f && this.IsInMapLimit(n, num7) && this.IsInMapLimit(num5, num6) && this.mapRoomID[num5, num6] == this.mapRoomID[n, num7])
								{
									this.mapAusstattung[n, num7] += ausstattung * 0.2f / (num8 + 0.1f);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04002500 RID: 9472
	private mainScript mS_;

	// Token: 0x04002501 RID: 9473
	private buildRoomScript brS_;

	// Token: 0x04002502 RID: 9474
	private roomDataScript rdS_;

	// Token: 0x04002503 RID: 9475
	private GUI_Main guiMain_;

	// Token: 0x04002504 RID: 9476
	public static int mapSizeX = 100;

	// Token: 0x04002505 RID: 9477
	public static int mapSizeY = 100;

	// Token: 0x04002506 RID: 9478
	public static int ID_FLOOR = 1;

	// Token: 0x04002507 RID: 9479
	public int[,] mapRoomID = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002508 RID: 9480
	public roomScript[,] mapRoomScript = new roomScript[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002509 RID: 9481
	public int[,] mapDoors = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250A RID: 9482
	public int[,] mapWindows = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250B RID: 9483
	public int[,] mapBlock = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250C RID: 9484
	public int[,] mapBlockDoor = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250D RID: 9485
	public int[,] mapBuilding = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250E RID: 9486
	public int[,] mapRoomID_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400250F RID: 9487
	public int[,] mapRoomTyp_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002510 RID: 9488
	public int[,] mapDoors_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002511 RID: 9489
	public int[,] mapWindows_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002512 RID: 9490
	public GameObject ROOMS_MP;

	// Token: 0x04002513 RID: 9491
	public GameObject[] prefabsWalls;

	// Token: 0x04002514 RID: 9492
	public GameObject[] prefabsInventar;

	// Token: 0x04002515 RID: 9493
	public AstarPath aStar_;

	// Token: 0x04002516 RID: 9494
	private bool[,] doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002517 RID: 9495
	private GameObject ROOMS;

	// Token: 0x04002518 RID: 9496
	public float[,] mapMuell = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x04002519 RID: 9497
	public float[,] mapWaerme = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400251A RID: 9498
	public float[,] mapAusstattung = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	// Token: 0x0400251B RID: 9499
	private float muellTimer;

	// Token: 0x0400251C RID: 9500
	private float updateMapFilterTimer;
}
