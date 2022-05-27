using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		this.UpdateMapMuell(false);
		this.UpdateMapFilter(false);
	}

	
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
			Debug.Log("BLOCK: " + Mathf.RoundToInt(gameObject.transform.position.x).ToString() + ", " + Mathf.RoundToInt(gameObject.transform.position.z).ToString());
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
			Debug.Log("BLOCK DOOR: " + Mathf.RoundToInt(gameObject2.transform.position.x).ToString() + ", " + Mathf.RoundToInt(gameObject2.transform.position.z).ToString());
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

	
	public void UpdatePathfindingInstant()
	{
		base.StartCoroutine(this.UpdatePathfindingInstantNextFrame());
	}

	
	public void UpdatePathfinding()
	{
		base.StartCoroutine(this.UpdatePathfindingNextFrame());
	}

	
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

	
	private void SetRoomParent(GameObject go)
	{
		go.transform.SetParent(this.ROOMS.transform, true);
	}

	
	private void SetRoomParent_Multiplayer(GameObject go)
	{
		go.transform.SetParent(this.ROOMS_MP.transform, true);
		go.transform.GetChild(0).tag = "Untagged";
		go.transform.GetChild(0).transform.parent = this.ROOMS_MP.transform;
		UnityEngine.Object.Destroy(go);
	}

	
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

	
	public GameObject CreateObject(int t)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabsInventar[t]);
		gameObject.transform.position = new Vector3(0f, 9999f, 0f);
		gameObject.GetComponent<objectScript>().InitNewObject(t);
		return gameObject;
	}

	
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

	
	public bool IsInMapLimit(int x, int y)
	{
		return x >= 0 && x <= mapScript.mapSizeX && y >= 0 && y <= mapScript.mapSizeY;
	}

	
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

	
	private mainScript mS_;

	
	private buildRoomScript brS_;

	
	private roomDataScript rdS_;

	
	private GUI_Main guiMain_;

	
	public static int mapSizeX = 100;

	
	public static int mapSizeY = 100;

	
	public static int ID_FLOOR = 1;

	
	public int[,] mapRoomID = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public roomScript[,] mapRoomScript = new roomScript[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapDoors = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapWindows = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapBlock = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapBlockDoor = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapBuilding = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapRoomID_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapRoomTyp_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapDoors_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public int[,] mapWindows_LAYOUT = new int[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public GameObject ROOMS_MP;

	
	public GameObject[] prefabsWalls;

	
	public GameObject[] prefabsInventar;

	
	public AstarPath aStar_;

	
	private bool[,] doorsPlaced = new bool[mapScript.mapSizeX, mapScript.mapSizeY];

	
	private GameObject ROOMS;

	
	public float[,] mapMuell = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public float[,] mapWaerme = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	
	public float[,] mapAusstattung = new float[mapScript.mapSizeX, mapScript.mapSizeY];

	
	private float muellTimer;

	
	private float updateMapFilterTimer;
}
