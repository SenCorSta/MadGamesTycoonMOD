using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class objectScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.RemoveObjects();
		this.RemoveTilesView();
		if (this.mS_)
		{
			this.mS_.findObjects = true;
		}
	}

	
	private void OnDestroy()
	{
		if (this.multiplayerObject)
		{
			return;
		}
		if (!this.mS_)
		{
			if (GameObject.FindWithTag("Main"))
			{
				this.mS_ = GameObject.FindWithTag("Main").GetComponent<mainScript>();
			}
			if (!this.mS_)
			{
				return;
			}
		}
		if (this.gekauft && !this.picked)
		{
			int num = this.typ;
			if (num != 17)
			{
				switch (num)
				{
				case 129:
					this.mS_.platinSchallplatten++;
					break;
				case 130:
					this.mS_.diamantSchallplatten++;
					break;
				case 132:
					this.mS_.award_GOTY++;
					break;
				case 133:
					this.mS_.award_Studio++;
					break;
				case 134:
					this.mS_.award_Sound++;
					break;
				case 135:
					this.mS_.award_Grafik++;
					break;
				case 142:
					this.mS_.award_Trendsetter++;
					break;
				case 143:
					this.mS_.award_Publisher++;
					break;
				}
			}
			else
			{
				this.mS_.goldeneSchallplatten++;
			}
		}
		if (!this.picked)
		{
			this.mS_.Multiplayer_SendObjectDelete(this.myID);
		}
		if (this.mS_)
		{
			this.mS_.findObjects = true;
		}
	}

	
	public void InitNewObject(int typ_)
	{
		this.FindScripts();
		this.InitGFX();
		base.transform.eulerAngles = new Vector3(0f, this.mS_.objectRotation, 0f);
		this.myID = Mathf.RoundToInt((float)UnityEngine.Random.Range(1, 1999999999));
		base.gameObject.name = "O_" + this.myID.ToString();
		this.typ = typ_;
		this.gekauft = false;
		this.PickUp();
	}

	
	public void InitObjectFromSavegame()
	{
		this.FindScripts();
		this.InitGFX();
		base.gameObject.name = "O_" + this.myID.ToString();
		this.gekauft = true;
	}

	
	public void InitGhostObject(int typ_)
	{
		this.FindScripts();
		this.myID = Mathf.RoundToInt((float)UnityEngine.Random.Range(1, 1999999999));
		base.gameObject.name = "O_" + this.myID.ToString();
		this.typ = -1;
		this.typGhost = typ_;
		this.gekauft = true;
	}

	
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = GameObject.FindWithTag("Main").GetComponent<mainScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = GameObject.FindWithTag("Main").GetComponent<mapScript>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.guiMain)
		{
			this.guiMain = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = GameObject.FindWithTag("MainCamera").GetComponent<mainCameraScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = GameObject.FindWithTag("Main").GetComponent<textScript>();
		}
		if (!this.pickObject_)
		{
			this.pickObject_ = GameObject.FindWithTag("Main").GetComponent<pickObjectScript>();
		}
		if (!this.rigidbody)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
	}

	
	private void RemoveObjects()
	{
		for (int i = 0; i < this.removeObjects.Length; i++)
		{
			if (this.removeObjects[i])
			{
				UnityEngine.Object.Destroy(this.removeObjects[i]);
			}
		}
	}

	
	private void InitGFX()
	{
		if (this.GFX.Length == 0)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, this.GFX.Length);
		for (int i = 0; i < this.GFX.Length; i++)
		{
			if (i != num)
			{
				UnityEngine.Object.Destroy(this.GFX[i]);
			}
		}
		this.GFX[num].SetActive(true);
	}

	
	public void UpdateMe()
	{
		this.MouseMovement();
		this.UpdateUnkorrekterRoom();
	}

	
	public void WakeUpObject()
	{
		if (this.rigidbody.IsSleeping())
		{
			this.rigidbody.WakeUp();
		}
	}

	
	public void UpdateUnkorrekterRoom()
	{
		if (!this.gekauft)
		{
			return;
		}
		if (this.guiMain.menuOpen)
		{
			return;
		}
		if (this.mS_.multiplayer)
		{
			if (this.guiMain.uiObjects[19].activeSelf)
			{
				return;
			}
			if (this.guiMain.uiObjects[20].activeSelf)
			{
				return;
			}
		}
		if (this.isGhost)
		{
			return;
		}
		int num = Mathf.RoundToInt(base.transform.position.x);
		int num2 = Mathf.RoundToInt(base.transform.position.z);
		if (!this.mapS_.IsInMapLimit(num, num2))
		{
			return;
		}
		if (!this.mapS_.IsInMapLimit(num + 1, num2))
		{
			return;
		}
		if (!this.mapS_.IsInMapLimit(num - 1, num2))
		{
			return;
		}
		if (!this.mapS_.IsInMapLimit(num, num2 + 1))
		{
			return;
		}
		if (!this.mapS_.IsInMapLimit(num, num2 - 1))
		{
			return;
		}
		if (this.mapS_.mapRoomID[num, num2] != this.myRoomID)
		{
			Debug.Log("UpdateUnkorrekterRoom(): " + base.gameObject.name);
			this.mCamS_.SetOutlineColor(1, 0.3f, 1);
			this.colided = true;
			this.pickObject_.Click(base.gameObject);
			return;
		}
		if (this.wallObject)
		{
			int num3 = Mathf.RoundToInt(base.transform.eulerAngles.y);
			if ((num3 == 270 && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num + 1, num2]) || (num3 == 90 && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num - 1, num2]) || (num3 == 180 && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num, num2 + 1]) || (num3 == 0 && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num, num2 - 1]))
			{
				Debug.Log("UpdateUnkorrekterRoom() -> Wandobjekt -> Objekt steht in Luft:" + base.gameObject.name);
				this.mCamS_.SetOutlineColor(1, 0.3f, 1);
				this.colided = true;
				this.pickObject_.Click(base.gameObject);
			}
		}
	}

	
	public void PickUp()
	{
		this.collisionAmount = 0;
		this.picked = true;
		this.mS_.pickedObject = base.gameObject;
		this.mCamS_.SetOutlineColor(2, 0.1f, 4);
		this.SetLayer(11, base.gameObject.transform.GetChild(0));
	}

	
	private void SetLayer(int newLayer, Transform trans)
	{
		trans.gameObject.layer = newLayer;
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			if ((newLayer == 11 && transform.tag != "NoOutline") || newLayer != 11)
			{
				transform.gameObject.layer = newLayer;
				if (transform.childCount > 0)
				{
					this.SetLayer(newLayer, transform.transform);
				}
			}
		}
	}

	
	public void MouseMovement()
	{
		if (!this.picked)
		{
			return;
		}
		if (this.checkCollideWithDelay)
		{
			return;
		}
		float y = 0.05f;
		this.mapS_.UpdateMapFilter(true);
		this.mS_.DrawFilter(this.guiMain.filterToggles, true);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		if (this.guiMain.IsMouseOverGUI())
		{
			base.transform.position = new Vector3(-50f, -50f, -50f);
			return;
		}
		if (!Input.GetKeyUp(KeyCode.Escape) || this.pickObject_.oldPosition.x < 0f)
		{
			float y2 = Mathf.LerpAngle(base.transform.eulerAngles.y, this.mS_.objectRotation, 0.3f);
			base.transform.eulerAngles = new Vector3(0f, y2, 0f);
			RaycastHit raycastHit;
			if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out raycastHit, 200f, this.layerMask))
			{
				float x;
				float z;
				if (this.mS_.snapObject)
				{
					x = objectScript.SnapTo(raycastHit.point.x, 0.5f);
					z = objectScript.SnapTo(raycastHit.point.z, 0.5f);
				}
				else
				{
					x = raycastHit.point.x;
					z = raycastHit.point.z;
				}
				Vector3 position = new Vector3(x, y, z);
				base.transform.position = Vector3.Lerp(base.transform.position, position, 0.3f);
				if (base.transform.position.y > 100f)
				{
					base.transform.position = position;
				}
				if (this.mS_.snapRotation)
				{
					this.mS_.objectRotation = (float)(Mathf.RoundToInt(this.mS_.objectRotation / 90f) * 90);
					if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.F))
					{
						this.mS_.objectRotation += 90f;
						this.sfx_.PlaySound(6, true);
					}
					if (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.R))
					{
						this.mS_.objectRotation -= 90f;
						this.sfx_.PlaySound(6, true);
					}
					if (Input.GetMouseButton(1))
					{
						if (Input.mouseScrollDelta.y > 0f)
						{
							this.mS_.objectRotation -= 90f;
						}
						if (Input.mouseScrollDelta.y < 0f)
						{
							this.mS_.objectRotation += 90f;
						}
					}
				}
				else
				{
					if (Input.GetKey(KeyCode.Period) || Input.GetKey(KeyCode.F))
					{
						this.mS_.objectRotation += 130f * Time.deltaTime;
					}
					if (Input.GetKey(KeyCode.Comma) || Input.GetKey(KeyCode.R))
					{
						this.mS_.objectRotation -= 130f * Time.deltaTime;
					}
					if (Input.GetMouseButton(1) && Input.mouseScrollDelta.y != 0f)
					{
						this.mS_.objectRotation -= Input.mouseScrollDelta.y * 10f;
					}
				}
				int num = Mathf.RoundToInt(raycastHit.transform.position.x);
				int num2 = Mathf.RoundToInt(raycastHit.transform.position.z);
				if (this.wallObject)
				{
					this.mS_.objectRotation = (float)Mathf.RoundToInt(raycastHit.transform.eulerAngles.y - 90f);
					if (this.mapS_.mapRoomID[num, num2] != this.mapS_.mapRoomID[num + 1, num2])
					{
						base.transform.position = new Vector3(raycastHit.transform.position.x, y, base.transform.position.z);
					}
					if (this.mapS_.mapRoomID[num, num2] != this.mapS_.mapRoomID[num - 1, num2])
					{
						base.transform.position = new Vector3(raycastHit.transform.position.x, y, base.transform.position.z);
					}
					if (this.mapS_.mapRoomID[num, num2] != this.mapS_.mapRoomID[num, num2 + 1])
					{
						base.transform.position = new Vector3(base.transform.position.x, y, raycastHit.transform.position.z);
					}
					if (this.mapS_.mapRoomID[num, num2] != this.mapS_.mapRoomID[num, num2 - 1])
					{
						base.transform.position = new Vector3(base.transform.position.x, y, raycastHit.transform.position.z);
					}
					position = base.transform.position;
				}
				bool flag = false;
				if (!this.colided)
				{
					if (!this.mS_.IsMyBuilding(this.mapS_.mapBuilding[num, num2]))
					{
						flag = true;
					}
					if (!this.IsCorrectRoomForThisObject(position))
					{
						flag = true;
					}
					if (this.wallObject && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num + 1, num2] && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num - 1, num2] && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num, num2 + 1] && this.mapS_.mapRoomID[num, num2] == this.mapS_.mapRoomID[num, num2 - 1])
					{
						flag = true;
					}
					if (this.typ == 17 && this.mS_.goldeneSchallplatten <= 0)
					{
						flag = true;
					}
					if (this.typ == 129 && this.mS_.platinSchallplatten <= 0)
					{
						flag = true;
					}
					if (this.typ == 130 && this.mS_.diamantSchallplatten <= 0)
					{
						flag = true;
					}
					if (this.typ == 132 && this.mS_.award_GOTY <= 0)
					{
						flag = true;
					}
					if (this.typ == 133 && this.mS_.award_Studio <= 0)
					{
						flag = true;
					}
					if (this.typ == 134 && this.mS_.award_Sound <= 0)
					{
						flag = true;
					}
					if (this.typ == 135 && this.mS_.award_Grafik <= 0)
					{
						flag = true;
					}
					if (this.typ == 142 && this.mS_.award_Trendsetter <= 0)
					{
						flag = true;
					}
					if (this.typ == 143 && this.mS_.award_Publisher <= 0)
					{
						flag = true;
					}
					if (flag)
					{
						this.mCamS_.SetOutlineColor(1, 0.3f, 1);
						if (mouseButtonUp)
						{
							this.sfx_.PlaySound(2, true);
						}
					}
					else
					{
						this.mCamS_.SetOutlineColor(2, 0.1f, 4);
					}
				}
				if (flag)
				{
					return;
				}
				if (mouseButtonUp && !this.colided)
				{
					if (this.typ == 17 && this.mS_.goldeneSchallplatten <= 0)
					{
						return;
					}
					if (this.typ == 129 && this.mS_.platinSchallplatten <= 0)
					{
						return;
					}
					if (this.typ == 130 && this.mS_.diamantSchallplatten <= 0)
					{
						return;
					}
					if (this.typ == 132 && this.mS_.award_GOTY <= 0)
					{
						return;
					}
					if (this.typ == 133 && this.mS_.award_Studio <= 0)
					{
						return;
					}
					if (this.typ == 134 && this.mS_.award_Sound <= 0)
					{
						return;
					}
					if (this.typ == 135 && this.mS_.award_Grafik <= 0)
					{
						return;
					}
					if (this.typ == 142 && this.mS_.award_Trendsetter <= 0)
					{
						return;
					}
					if (this.typ == 143 && this.mS_.award_Publisher <= 0)
					{
						return;
					}
					base.StartCoroutine(this.CheckCollideWithDelay(position));
					return;
				}
			}
			else
			{
				base.transform.position = new Vector3(0f, 9999f, 0f);
			}
			return;
		}
		Vector3 oldPosition = this.pickObject_.oldPosition;
		this.pickObject_.oldPosition = new Vector3(-1000f, 0f, 0f);
		if (this.typ == 17 && this.mS_.goldeneSchallplatten <= 0)
		{
			return;
		}
		if (this.typ == 129 && this.mS_.platinSchallplatten <= 0)
		{
			return;
		}
		if (this.typ == 130 && this.mS_.diamantSchallplatten <= 0)
		{
			return;
		}
		if (this.typ == 132 && this.mS_.award_GOTY <= 0)
		{
			return;
		}
		if (this.typ == 133 && this.mS_.award_Studio <= 0)
		{
			return;
		}
		if (this.typ == 134 && this.mS_.award_Sound <= 0)
		{
			return;
		}
		if (this.typ == 135 && this.mS_.award_Grafik <= 0)
		{
			return;
		}
		if (this.typ == 142 && this.mS_.award_Trendsetter <= 0)
		{
			return;
		}
		if (this.typ == 143 && this.mS_.award_Publisher <= 0)
		{
			return;
		}
		this.mS_.objectRotation = this.pickObject_.oldRotation.y;
		base.StartCoroutine(this.CheckCollideWithDelay(oldPosition));
	}

	
	public static float SnapTo(float a, float snap)
	{
		return Mathf.Round(a / snap) * snap;
	}

	
	private void OnCollisionEnter(Collision collision)
	{
		this.mCamS_.SetOutlineColor(1, 0.3f, 1);
		this.colided = true;
		if (this.picked)
		{
			this.collisionAmount++;
		}
	}

	
	private void OnCollisionStay(Collision collision)
	{
		this.mCamS_.SetOutlineColor(1, 0.3f, 1);
		this.colided = true;
		if (this.picked && collision.transform != base.transform)
		{
			if (!this.mCamS_.additionalCamera[0].activeSelf)
			{
				this.mCamS_.additionalCamera[0].SetActive(true);
			}
			if (collision.gameObject.transform.childCount > 0)
			{
				if (collision.gameObject.tag == "HideWall")
				{
					if (this.mS_.guiMain_ && !this.mS_.guiMain_.uiObjects[241].GetComponent<Toggle>().isOn)
					{
						this.SetLayer(15, collision.gameObject.transform.GetChild(0));
						this.mS_.AddColliderLayer(collision.gameObject.transform.GetChild(0));
					}
				}
				else
				{
					this.SetLayer(15, collision.gameObject.transform.GetChild(0));
					this.mS_.AddColliderLayer(collision.gameObject.transform.GetChild(0));
				}
			}
			else
			{
				this.SetLayer(15, collision.gameObject.transform.parent.GetChild(0));
				this.mS_.AddColliderLayer(collision.gameObject.transform.parent.GetChild(0));
			}
		}
		if (!this.guiMain.menuOpen && this.gekauft)
		{
			this.pickObject_.Click(base.gameObject);
		}
	}

	
	private void OnCollisionExit(Collision collision)
	{
		if (this.picked)
		{
			this.collisionAmount--;
		}
		if (this.collisionAmount <= 0)
		{
			this.colided = false;
		}
		if (this.picked)
		{
			if (this.collisionAmount <= 0)
			{
				this.mCamS_.SetOutlineColor(2, 0.1f, 4);
			}
			if (collision.transform != base.transform)
			{
				if (this.mCamS_.additionalCamera[0].activeSelf)
				{
					this.mCamS_.additionalCamera[0].SetActive(false);
				}
				if (collision.gameObject.transform.childCount > 0)
				{
					this.SetLayer(0, collision.gameObject.transform.GetChild(0));
					return;
				}
				this.SetLayer(0, collision.gameObject.transform.parent.GetChild(0));
			}
		}
	}

	
	private IEnumerator CheckCollideWithDelay(Vector3 pos)
	{
		this.checkCollideWithDelay = true;
		base.transform.position = new Vector3(pos.x, pos.y, pos.z);
		base.transform.eulerAngles = new Vector3(0f, this.mS_.objectRotation, 0f);
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.guiMain.uiObjects[20].activeSelf)
		{
			this.PlatziereObject(pos, false, false);
		}
		else
		{
			this.PlatziereObject(pos, false, true);
		}
		this.checkCollideWithDelay = false;
		yield break;
	}

	
	private bool IsCorrectRoomForThisObject(Vector3 pos)
	{
		int num = Mathf.RoundToInt(pos.x);
		int num2 = Mathf.RoundToInt(pos.z);
		if (!this.mapS_.IsInMapLimit(num, num2))
		{
			return false;
		}
		if (this.mapS_.mapRoomID[num, num2] != 1)
		{
			roomScript roomScript = this.mapS_.mapRoomScript[num, num2];
			if (!roomScript)
			{
				return false;
			}
			if (roomScript.typ == 1 && !this.canSet_Development)
			{
				return false;
			}
			if (roomScript.typ == 2 && !this.canSet_Research)
			{
				return false;
			}
			if (roomScript.typ == 3 && !this.canSet_QA)
			{
				return false;
			}
			if (roomScript.typ == 4 && !this.canSet_Grafikstudio)
			{
				return false;
			}
			if (roomScript.typ == 5 && !this.canSet_Soundstudio)
			{
				return false;
			}
			if (roomScript.typ == 6 && !this.canSet_Marketing)
			{
				return false;
			}
			if (roomScript.typ == 7 && !this.canSet_Support)
			{
				return false;
			}
			if (roomScript.typ == 8 && !this.canSet_Hardware)
			{
				return false;
			}
			if (roomScript.typ == 9 && !this.canSet_Lager)
			{
				return false;
			}
			if (roomScript.typ == 10 && !this.canSet_Motion)
			{
				return false;
			}
			if (roomScript.typ == 11 && !this.canSet_WC)
			{
				return false;
			}
			if (roomScript.typ == 12 && !this.canSet_Aufenthalt)
			{
				return false;
			}
			if (roomScript.typ == 13 && !this.canSet_Training)
			{
				return false;
			}
			if (roomScript.typ == 14 && !this.canSet_Produktion)
			{
				return false;
			}
			if (roomScript.typ == 15 && !this.canSet_Server)
			{
				return false;
			}
			if (roomScript.typ == 17 && !this.canSet_Werkstatt)
			{
				return false;
			}
		}
		else if (!this.canSet_Floor)
		{
			return false;
		}
		return true;
	}

	
	public void PlatziereObject(Vector3 pos, bool fromSavegame, bool updatePathfinding)
	{
		if (!fromSavegame)
		{
			if (!this.IsCorrectRoomForThisObject(pos))
			{
				this.sfx_.PlaySound(2, true);
				return;
			}
			if (this.colided)
			{
				this.sfx_.PlaySound(2, true);
				return;
			}
			if (base.transform.position.y > 100f)
			{
				this.sfx_.PlaySound(2, true);
				return;
			}
		}
		base.transform.position = new Vector3(pos.x, 0f, pos.z);
		base.transform.eulerAngles = new Vector3(0f, this.mS_.objectRotation, 0f);
		if (this.footprint)
		{
			UnityEngine.Object.Destroy(this.footprint);
		}
		this.mS_.pickedObject = null;
		this.picked = false;
		this.SetLayer(0, base.gameObject.transform.GetChild(0));
		if (!fromSavegame)
		{
			base.GetComponent<Animation>().Play();
		}
		int num = Mathf.RoundToInt(pos.x);
		int num2 = Mathf.RoundToInt(pos.z);
		if (!this.mapS_.IsInMapLimit(num, num2))
		{
			num = 0;
			num2 = 0;
			base.transform.position = new Vector3((float)num, 0f, (float)num2);
		}
		if (this.mapS_.mapRoomScript[num, num2])
		{
			this.mapS_.mapRoomScript[num, num2].listInventar.Add(base.gameObject.GetComponent<objectScript>());
			this.myRoomID = this.mapS_.mapRoomScript[num, num2].myID;
		}
		if (!fromSavegame)
		{
			this.mS_.Multiplayer_SendObject(this.myID, this.typ, base.transform.position.x, base.transform.position.z, base.transform.eulerAngles.y);
			UnityEngine.Object.Instantiate<GameObject>(this.mS_.miscParticlePrefabs[0]).transform.position = base.transform.position;
			this.sfx_.PlaySound(4, true);
			int num3 = this.typ;
			if (num3 != 17)
			{
				switch (num3)
				{
				case 129:
					this.mS_.platinSchallplatten--;
					break;
				case 130:
					this.mS_.diamantSchallplatten--;
					break;
				case 132:
					this.mS_.award_GOTY--;
					break;
				case 133:
					this.mS_.award_Studio--;
					break;
				case 134:
					this.mS_.award_Sound--;
					break;
				case 135:
					this.mS_.award_Grafik--;
					break;
				case 142:
					this.mS_.award_Trendsetter--;
					break;
				case 143:
					this.mS_.award_Publisher--;
					break;
				}
			}
			else
			{
				this.mS_.goldeneSchallplatten--;
			}
			if (!this.gekauft)
			{
				this.gekauft = true;
				if (this.typ != 17)
				{
					this.mS_.Pay((long)this.preis, 1);
					this.guiMain.MoneyPop(this.preis, base.transform.position, false);
				}
				this.mapS_.CreateObject(this.typ);
			}
			else
			{
				this.guiMain.DeactivateMenu(this.guiMain.uiObjects[0]);
				this.guiMain.CloseMenu();
				this.ReOpenBuyInventarMenu();
			}
		}
		if (this.isRobotClean)
		{
			base.GetComponent<createRobot>().Init(this.myID);
		}
		base.StartCoroutine(this.DestroyUnnoetigeComponents());
		this.RemoveTilesView();
	}

	
	public bool ReOpenBuyInventarMenu()
	{
		this.FindScripts();
		if (this.pickObject_.reopenBuyInventarMenu)
		{
			this.pickObject_.reopenBuyInventarMenu = false;
			this.guiMain.DROPDOWN_BuyInventar(this.pickObject_.buyInventar);
			return true;
		}
		return false;
	}

	
	private void RemoveTilesView()
	{
		Transform transform = base.transform.Find("TilesView");
		if (transform)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	
	private IEnumerator DestroyUnnoetigeComponents()
	{
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(base.GetComponent<Animation>());
		yield break;
	}

	
	public void MouseOver()
	{
		this.SetOutlineLayer();
		if (this.aufladungenMax > 0)
		{
			this.guiMain.ShowObjectTooltip(this);
		}
	}

	
	public void MouseLeave()
	{
		this.DisableOutlineLayer();
		this.guiMain.DisableObjectTooltip();
	}

	
	public void SetOutlineLayer()
	{
		if (!this.outline)
		{
			this.outline = true;
			this.mCamS_.SetOutlineColor(2, 0.3f, 4);
			this.SetLayer(11, base.gameObject.transform.GetChild(0));
		}
	}

	
	private void DisableOutlineLayer()
	{
		if (this.outline)
		{
			this.outline = false;
			this.SetLayer(0, base.gameObject.transform.GetChild(0));
		}
	}

	
	public void SetBesetzt(int i)
	{
		this.besetztCharID = i;
	}

	
	public void SetUnbesetzt(int i)
	{
		this.besetztCharID = -1;
	}

	
	public bool IsUnbesetzt()
	{
		return this.besetztCharID == -1;
	}

	
	public int GetVerkaufspreis()
	{
		return Mathf.RoundToInt((float)this.preis * 0.5f);
	}

	
	public void Monatskosten()
	{
		if (this.monatsKosten <= 0)
		{
			return;
		}
		int num = this.monatsKosten;
		if (this.isServer)
		{
			if (this.mS_.globalEvent == 9)
			{
				num = this.monatsKosten * 2;
			}
			if (this.myRoomID != 1)
			{
				int num2 = Mathf.RoundToInt(base.transform.position.x);
				int num3 = Mathf.RoundToInt(base.transform.position.z);
				roomScript roomScript = this.mapS_.mapRoomScript[num2, num3];
				if (roomScript && roomScript.serverDown)
				{
					return;
				}
			}
		}
		this.mS_.Pay((long)num, 8);
		base.StartCoroutine(this.guiMain.MoneyPopEnumerate(num, base.transform.position, false));
	}

	
	public void AddAufladungen()
	{
		if (this.aufladungenMax > this.aufladungenAkt)
		{
			this.aufladungenAkt = this.aufladungenMax;
		}
	}

	
	private string GetTooltip()
	{
		this.FindScripts();
		string text = "<b>" + this.tS_.GetObjects(this.typ) + "</b>";
		text = text + "<br>" + this.tS_.GetObjectsTooltip(this.typ) + "<br>";
		if (this.aufladungenMax > 0)
		{
			string text2 = this.tS_.GetText(775);
			text2 = text2.Replace("<NUM>", this.aufladungenAkt.ToString() + "/" + this.aufladungenMax.ToString());
			text = text + "<br>" + text2;
		}
		return text;
	}

	
	private string GetQualitatStars(int i)
	{
		string result;
		switch (i)
		{
		case 0:
			result = "☆☆☆☆☆";
			break;
		case 1:
			result = "★☆☆☆☆";
			break;
		case 2:
			result = "★★☆☆☆";
			break;
		case 3:
			result = "★★★☆☆";
			break;
		case 4:
			result = "★★★★☆";
			break;
		case 5:
			result = "★★★★★";
			break;
		default:
			result = "☆☆☆☆☆";
			break;
		}
		return result;
	}

	
	public int GetServerplatz()
	{
		int num = this.serverplatz;
		int num2 = Mathf.RoundToInt(base.transform.position.x);
		int num3 = Mathf.RoundToInt(base.transform.position.z);
		if (!this.mapS_.IsInMapLimit(num2, num3))
		{
			return num;
		}
		if (this.mapS_.mapRoomScript[num2, num3] && this.mapS_.mapRoomScript[num2, num3].serverDown)
		{
			this.mapS_.mapRoomScript[num2, num3].serverOverheat = false;
			if (this.particle.activeSelf)
			{
				this.particle.SetActive(false);
			}
		}
		if (this.mS_.globalEvent == 11)
		{
			num = Mathf.RoundToInt((float)num * 0.7f);
		}
		if (this.mapS_.mapWaerme[num2, num3] > 0.5f)
		{
			if (!this.particle.activeSelf)
			{
				this.particle.SetActive(true);
			}
			if (this.mapS_.mapRoomScript[num2, num3])
			{
				this.mapS_.mapRoomScript[num2, num3].serverOverheat = true;
			}
			return num / 2;
		}
		if (this.particle.activeSelf)
		{
			this.particle.SetActive(false);
		}
		return num;
	}

	
	public int GetRoomID()
	{
		return this.myRoomID;
	}

	
	public mainScript mS_;

	
	private Camera myCamera;

	
	private GUI_Main guiMain;

	
	private mainCameraScript mCamS_;

	
	public sfxScript sfx_;

	
	public mapScript mapS_;

	
	public textScript tS_;

	
	private pickObjectScript pickObject_;

	
	public lagerScript lagerScript_;

	
	public GameObject[] GFX;

	
	public GameObject waypoint;

	
	public GameObject pointMale;

	
	public GameObject pointFemale;

	
	public Animation gfxAnimation;

	
	public GameObject gfxShow;

	
	public GameObject gfxHide;

	
	public GameObject[] removeObjects;

	
	public GameObject particle;

	
	public GameObject footprint;

	
	private Rigidbody rigidbody;

	
	public int myID;

	
	public int typ;

	
	public int typGhost = -1;

	
	public int preis;

	
	public int monatsKosten;

	
	public float waerme;

	
	public float kaelte;

	
	public float ausstattung;

	
	public float motivationRegen;

	
	public bool wallObject;

	
	public bool dontBuildOnWindows;

	
	public int unlockYear = -1;

	
	public int aufladungenMax;

	
	public int aufladungenAkt;

	
	public float maschieneTimer;

	
	public bool isArbeitsplatz;

	
	public bool isHeizung;

	
	public bool isServer;

	
	public bool isLager;

	
	public bool isMaschine;

	
	public bool isMuelleimer;

	
	public bool isPlant;

	
	public bool isWC;

	
	public bool isSink;

	
	public bool isHandtrockner;

	
	public bool isMedizinSchrank;

	
	public bool isFreezer;

	
	public bool isTV;

	
	public bool isRadio;

	
	public bool isArcade;

	
	public bool isDart;

	
	public bool isMinigolf;

	
	public bool isPiano;

	
	public bool isSeat;

	
	public bool isSeatAufenthalt;

	
	public bool isRobotClean;

	
	public bool isGhost;

	
	public bool isGhostMuelleimer;

	
	public bool isGhostDrink;

	
	public bool isGhostWC;

	
	public bool isGhostSink;

	
	public bool isGhostPlant;

	
	public bool isGhostPause1;

	
	public bool isGhostPause2;

	
	public bool isGhostPause3;

	
	public bool isGhostPause4;

	
	public bool canDrink;

	
	public bool canEat;

	
	public int qualitaet;

	
	public int lagerplatz;

	
	public int serverplatz;

	
	private int myRoomID = 1;

	
	public int besetztCharID = -1;

	
	public bool inUse;

	
	public bool gekauft;

	
	public bool picked;

	
	public bool colided;

	
	private bool outline;

	
	public bool canSet_Floor;

	
	public bool canSet_Development;

	
	public bool canSet_Research;

	
	public bool canSet_QA;

	
	public bool canSet_Grafikstudio;

	
	public bool canSet_Soundstudio;

	
	public bool canSet_Marketing;

	
	public bool canSet_Support;

	
	public bool canSet_Hardware;

	
	public bool canSet_Lager;

	
	public bool canSet_Motion;

	
	public bool canSet_WC;

	
	public bool canSet_Aufenthalt;

	
	public bool canSet_Training;

	
	public bool canSet_Produktion;

	
	public bool canSet_Server;

	
	public bool canSet_Werkstatt;

	
	public LayerMask layerMask;

	
	public bool multiplayerObject;

	
	public int collisionAmount;

	
	private bool checkCollideWithDelay;
}
