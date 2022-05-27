using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000336 RID: 822
public class objectScript : MonoBehaviour
{
	// Token: 0x06001DA9 RID: 7593 RVA: 0x0012798F File Offset: 0x00125B8F
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

	// Token: 0x06001DAA RID: 7594 RVA: 0x001279BC File Offset: 0x00125BBC
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

	// Token: 0x06001DAB RID: 7595 RVA: 0x00127B78 File Offset: 0x00125D78
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

	// Token: 0x06001DAC RID: 7596 RVA: 0x00127C01 File Offset: 0x00125E01
	public void InitObjectFromSavegame()
	{
		this.FindScripts();
		this.InitGFX();
		base.gameObject.name = "O_" + this.myID.ToString();
		this.gekauft = true;
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x00127C38 File Offset: 0x00125E38
	public void InitGhostObject(int typ_)
	{
		this.FindScripts();
		this.myID = Mathf.RoundToInt((float)UnityEngine.Random.Range(1, 1999999999));
		base.gameObject.name = "O_" + this.myID.ToString();
		this.typ = -1;
		this.typGhost = typ_;
		this.gekauft = true;
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x00127C98 File Offset: 0x00125E98
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

	// Token: 0x06001DAF RID: 7599 RVA: 0x00127DD0 File Offset: 0x00125FD0
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

	// Token: 0x06001DB0 RID: 7600 RVA: 0x00127E0C File Offset: 0x0012600C
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

	// Token: 0x06001DB1 RID: 7601 RVA: 0x00127E64 File Offset: 0x00126064
	public void UpdateMe()
	{
		this.MouseMovement();
		this.UpdateUnkorrekterRoom();
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00127E72 File Offset: 0x00126072
	public void WakeUpObject()
	{
		if (this.rigidbody.IsSleeping())
		{
			this.rigidbody.WakeUp();
		}
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x00127E8C File Offset: 0x0012608C
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

	// Token: 0x06001DB4 RID: 7604 RVA: 0x001280F8 File Offset: 0x001262F8
	public void PickUp()
	{
		this.collisionAmount = 0;
		this.picked = true;
		this.mS_.pickedObject = base.gameObject;
		this.mCamS_.SetOutlineColor(2, 0.1f, 4);
		this.SetLayer(11, base.gameObject.transform.GetChild(0));
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x00128150 File Offset: 0x00126350
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

	// Token: 0x06001DB6 RID: 7606 RVA: 0x001281E8 File Offset: 0x001263E8
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

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00128C1B File Offset: 0x00126E1B
	public static float SnapTo(float a, float snap)
	{
		return Mathf.Round(a / snap) * snap;
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00128C27 File Offset: 0x00126E27
	private void OnCollisionEnter(Collision collision)
	{
		this.mCamS_.SetOutlineColor(1, 0.3f, 1);
		this.colided = true;
		if (this.picked)
		{
			this.collisionAmount++;
		}
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x00128C58 File Offset: 0x00126E58
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

	// Token: 0x06001DBA RID: 7610 RVA: 0x00128E00 File Offset: 0x00127000
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

	// Token: 0x06001DBB RID: 7611 RVA: 0x00128EDC File Offset: 0x001270DC
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

	// Token: 0x06001DBC RID: 7612 RVA: 0x00128EF4 File Offset: 0x001270F4
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

	// Token: 0x06001DBD RID: 7613 RVA: 0x001290A8 File Offset: 0x001272A8
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

	// Token: 0x06001DBE RID: 7614 RVA: 0x00129493 File Offset: 0x00127693
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

	// Token: 0x06001DBF RID: 7615 RVA: 0x001294D0 File Offset: 0x001276D0
	private void RemoveTilesView()
	{
		Transform transform = base.transform.Find("TilesView");
		if (transform)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00129501 File Offset: 0x00127701
	private IEnumerator DestroyUnnoetigeComponents()
	{
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(base.GetComponent<Animation>());
		yield break;
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x00129510 File Offset: 0x00127710
	public void MouseOver()
	{
		this.SetOutlineLayer();
		if (this.aufladungenMax > 0)
		{
			this.guiMain.ShowObjectTooltip(this);
		}
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x0012952D File Offset: 0x0012772D
	public void MouseLeave()
	{
		this.DisableOutlineLayer();
		this.guiMain.DisableObjectTooltip();
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x00129540 File Offset: 0x00127740
	public void SetOutlineLayer()
	{
		if (!this.outline)
		{
			this.outline = true;
			this.mCamS_.SetOutlineColor(2, 0.3f, 4);
			this.SetLayer(11, base.gameObject.transform.GetChild(0));
		}
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x0012957C File Offset: 0x0012777C
	private void DisableOutlineLayer()
	{
		if (this.outline)
		{
			this.outline = false;
			this.SetLayer(0, base.gameObject.transform.GetChild(0));
		}
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x001295A5 File Offset: 0x001277A5
	public void SetBesetzt(int i)
	{
		this.besetztCharID = i;
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x001295AE File Offset: 0x001277AE
	public void SetUnbesetzt(int i)
	{
		this.besetztCharID = -1;
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x001295B7 File Offset: 0x001277B7
	public bool IsUnbesetzt()
	{
		return this.besetztCharID == -1;
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x001295C5 File Offset: 0x001277C5
	public int GetVerkaufspreis()
	{
		return Mathf.RoundToInt((float)this.preis * 0.5f);
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x001295DC File Offset: 0x001277DC
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

	// Token: 0x06001DCA RID: 7626 RVA: 0x001296A0 File Offset: 0x001278A0
	public void AddAufladungen()
	{
		if (this.aufladungenMax > this.aufladungenAkt)
		{
			this.aufladungenAkt = this.aufladungenMax;
		}
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x001296BC File Offset: 0x001278BC
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

	// Token: 0x06001DCC RID: 7628 RVA: 0x0012976C File Offset: 0x0012796C
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

	// Token: 0x06001DCD RID: 7629 RVA: 0x001297D8 File Offset: 0x001279D8
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

	// Token: 0x06001DCE RID: 7630 RVA: 0x0012992A File Offset: 0x00127B2A
	public int GetRoomID()
	{
		return this.myRoomID;
	}

	// Token: 0x04002523 RID: 9507
	public mainScript mS_;

	// Token: 0x04002524 RID: 9508
	private Camera myCamera;

	// Token: 0x04002525 RID: 9509
	private GUI_Main guiMain;

	// Token: 0x04002526 RID: 9510
	private mainCameraScript mCamS_;

	// Token: 0x04002527 RID: 9511
	public sfxScript sfx_;

	// Token: 0x04002528 RID: 9512
	public mapScript mapS_;

	// Token: 0x04002529 RID: 9513
	public textScript tS_;

	// Token: 0x0400252A RID: 9514
	private pickObjectScript pickObject_;

	// Token: 0x0400252B RID: 9515
	public lagerScript lagerScript_;

	// Token: 0x0400252C RID: 9516
	public GameObject[] GFX;

	// Token: 0x0400252D RID: 9517
	public GameObject waypoint;

	// Token: 0x0400252E RID: 9518
	public GameObject pointMale;

	// Token: 0x0400252F RID: 9519
	public GameObject pointFemale;

	// Token: 0x04002530 RID: 9520
	public Animation gfxAnimation;

	// Token: 0x04002531 RID: 9521
	public GameObject gfxShow;

	// Token: 0x04002532 RID: 9522
	public GameObject gfxHide;

	// Token: 0x04002533 RID: 9523
	public GameObject[] removeObjects;

	// Token: 0x04002534 RID: 9524
	public GameObject particle;

	// Token: 0x04002535 RID: 9525
	public GameObject footprint;

	// Token: 0x04002536 RID: 9526
	private Rigidbody rigidbody;

	// Token: 0x04002537 RID: 9527
	public int myID;

	// Token: 0x04002538 RID: 9528
	public int typ;

	// Token: 0x04002539 RID: 9529
	public int typGhost = -1;

	// Token: 0x0400253A RID: 9530
	public int preis;

	// Token: 0x0400253B RID: 9531
	public int monatsKosten;

	// Token: 0x0400253C RID: 9532
	public float waerme;

	// Token: 0x0400253D RID: 9533
	public float kaelte;

	// Token: 0x0400253E RID: 9534
	public float ausstattung;

	// Token: 0x0400253F RID: 9535
	public float motivationRegen;

	// Token: 0x04002540 RID: 9536
	public bool wallObject;

	// Token: 0x04002541 RID: 9537
	public bool dontBuildOnWindows;

	// Token: 0x04002542 RID: 9538
	public int unlockYear = -1;

	// Token: 0x04002543 RID: 9539
	public int aufladungenMax;

	// Token: 0x04002544 RID: 9540
	public int aufladungenAkt;

	// Token: 0x04002545 RID: 9541
	public float maschieneTimer;

	// Token: 0x04002546 RID: 9542
	public bool isArbeitsplatz;

	// Token: 0x04002547 RID: 9543
	public bool isHeizung;

	// Token: 0x04002548 RID: 9544
	public bool isServer;

	// Token: 0x04002549 RID: 9545
	public bool isLager;

	// Token: 0x0400254A RID: 9546
	public bool isMaschine;

	// Token: 0x0400254B RID: 9547
	public bool isMuelleimer;

	// Token: 0x0400254C RID: 9548
	public bool isPlant;

	// Token: 0x0400254D RID: 9549
	public bool isWC;

	// Token: 0x0400254E RID: 9550
	public bool isSink;

	// Token: 0x0400254F RID: 9551
	public bool isHandtrockner;

	// Token: 0x04002550 RID: 9552
	public bool isMedizinSchrank;

	// Token: 0x04002551 RID: 9553
	public bool isFreezer;

	// Token: 0x04002552 RID: 9554
	public bool isTV;

	// Token: 0x04002553 RID: 9555
	public bool isRadio;

	// Token: 0x04002554 RID: 9556
	public bool isArcade;

	// Token: 0x04002555 RID: 9557
	public bool isDart;

	// Token: 0x04002556 RID: 9558
	public bool isMinigolf;

	// Token: 0x04002557 RID: 9559
	public bool isPiano;

	// Token: 0x04002558 RID: 9560
	public bool isSeat;

	// Token: 0x04002559 RID: 9561
	public bool isSeatAufenthalt;

	// Token: 0x0400255A RID: 9562
	public bool isRobotClean;

	// Token: 0x0400255B RID: 9563
	public bool isGhost;

	// Token: 0x0400255C RID: 9564
	public bool isGhostMuelleimer;

	// Token: 0x0400255D RID: 9565
	public bool isGhostDrink;

	// Token: 0x0400255E RID: 9566
	public bool isGhostWC;

	// Token: 0x0400255F RID: 9567
	public bool isGhostSink;

	// Token: 0x04002560 RID: 9568
	public bool isGhostPlant;

	// Token: 0x04002561 RID: 9569
	public bool isGhostPause1;

	// Token: 0x04002562 RID: 9570
	public bool isGhostPause2;

	// Token: 0x04002563 RID: 9571
	public bool isGhostPause3;

	// Token: 0x04002564 RID: 9572
	public bool isGhostPause4;

	// Token: 0x04002565 RID: 9573
	public bool canDrink;

	// Token: 0x04002566 RID: 9574
	public bool canEat;

	// Token: 0x04002567 RID: 9575
	public int qualitaet;

	// Token: 0x04002568 RID: 9576
	public int lagerplatz;

	// Token: 0x04002569 RID: 9577
	public int serverplatz;

	// Token: 0x0400256A RID: 9578
	private int myRoomID = 1;

	// Token: 0x0400256B RID: 9579
	public int besetztCharID = -1;

	// Token: 0x0400256C RID: 9580
	public bool inUse;

	// Token: 0x0400256D RID: 9581
	public bool gekauft;

	// Token: 0x0400256E RID: 9582
	public bool picked;

	// Token: 0x0400256F RID: 9583
	public bool colided;

	// Token: 0x04002570 RID: 9584
	private bool outline;

	// Token: 0x04002571 RID: 9585
	public bool canSet_Floor;

	// Token: 0x04002572 RID: 9586
	public bool canSet_Development;

	// Token: 0x04002573 RID: 9587
	public bool canSet_Research;

	// Token: 0x04002574 RID: 9588
	public bool canSet_QA;

	// Token: 0x04002575 RID: 9589
	public bool canSet_Grafikstudio;

	// Token: 0x04002576 RID: 9590
	public bool canSet_Soundstudio;

	// Token: 0x04002577 RID: 9591
	public bool canSet_Marketing;

	// Token: 0x04002578 RID: 9592
	public bool canSet_Support;

	// Token: 0x04002579 RID: 9593
	public bool canSet_Hardware;

	// Token: 0x0400257A RID: 9594
	public bool canSet_Lager;

	// Token: 0x0400257B RID: 9595
	public bool canSet_Motion;

	// Token: 0x0400257C RID: 9596
	public bool canSet_WC;

	// Token: 0x0400257D RID: 9597
	public bool canSet_Aufenthalt;

	// Token: 0x0400257E RID: 9598
	public bool canSet_Training;

	// Token: 0x0400257F RID: 9599
	public bool canSet_Produktion;

	// Token: 0x04002580 RID: 9600
	public bool canSet_Server;

	// Token: 0x04002581 RID: 9601
	public bool canSet_Werkstatt;

	// Token: 0x04002582 RID: 9602
	public LayerMask layerMask;

	// Token: 0x04002583 RID: 9603
	public bool multiplayerObject;

	// Token: 0x04002584 RID: 9604
	public int collisionAmount;

	// Token: 0x04002585 RID: 9605
	private bool checkCollideWithDelay;
}
