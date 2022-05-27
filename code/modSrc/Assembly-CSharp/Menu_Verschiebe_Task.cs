using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x020001BC RID: 444
public class Menu_Verschiebe_Task : MonoBehaviour
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x000B1306 File Offset: 0x000AF506
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000B1310 File Offset: 0x000AF510
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
		if (!this.camera_)
		{
			this.camera_ = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.pOS_)
		{
			this.pOS_ = this.main_.GetComponent<pickObjectScript>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x000B141C File Offset: 0x000AF61C
	private void Update()
	{
		this.DrawLine();
		this.MouseMovement();
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000B142C File Offset: 0x000AF62C
	private void MouseMovement()
	{
		if (!this.mS_)
		{
			return;
		}
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		this.pOS_.disableMouseButton = mouseButtonUp;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out raycastHit, 200f, this.layerMaskFloor))
		{
			float x = raycastHit.point.x;
			float z = raycastHit.point.z;
			int num = Mathf.RoundToInt(x);
			int num2 = Mathf.RoundToInt(z);
			if (this.mapS_.mapRoomID[num, num2] != 1)
			{
				if (this.mapS_.mapRoomScript[num, num2])
				{
					if (this.roomOutlineOld != this.mapS_.mapRoomScript[num, num2])
					{
						if (this.roomOutlineOld)
						{
							this.roomOutlineOld.DisableOutlineLayer();
						}
						this.roomOutlineOld = this.mapS_.mapRoomScript[num, num2];
						if (this.mapS_.mapRoomScript[num, num2].typ == this.rS_.typ && this.mapS_.mapRoomScript[num, num2].myID != this.rS_.myID)
						{
							this.mapS_.mapRoomScript[num, num2].SetOutlineLayer();
						}
					}
					if (mouseButtonUp)
					{
						if (this.mapS_.mapRoomScript[num, num2].typ == this.rS_.typ && this.mapS_.mapRoomScript[num, num2].myID != this.rS_.myID)
						{
							this.TaskTauschen(this.mapS_.mapRoomScript[num, num2]);
							return;
						}
						this.sfx_.PlaySound(2, true);
						return;
					}
				}
			}
			else if (this.roomOutlineOld)
			{
				this.roomOutlineOld.DisableOutlineLayer();
				this.roomOutlineOld = null;
			}
		}
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x000B164C File Offset: 0x000AF84C
	public void BUTTON_Close()
	{
		this.initLine = false;
		if (this.myLine)
		{
			UnityEngine.Object.Destroy(this.myLine);
		}
		this.sfx_.PlaySound(3, true);
		if (this.roomOutlineOld)
		{
			this.roomOutlineOld.DisableOutlineLayer();
			this.roomOutlineOld = null;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000B16BC File Offset: 0x000AF8BC
	private void TaskTauschen(roomScript script_)
	{
		if (this.rS_ == script_)
		{
			return;
		}
		if (this.rS_ && script_ && this.rS_.typ == script_.typ)
		{
			int taskID = this.rS_.taskID;
			int taskID2 = script_.taskID;
			if (this.rS_.taskGameObject && this.rS_.taskGameObject.GetComponent<taskUnterstuetzen>())
			{
				this.rS_.taskGameObject.GetComponent<taskUnterstuetzen>().roomID = this.rS_.myID;
				this.rS_.taskGameObject.GetComponent<taskUnterstuetzen>().rS_ = null;
				this.rS_.taskGameObject.GetComponent<taskUnterstuetzen>().Abbrechen();
				Debug.Log("A");
			}
			if (script_.taskGameObject && script_.taskGameObject.GetComponent<taskUnterstuetzen>())
			{
				script_.taskGameObject.GetComponent<taskUnterstuetzen>().roomID = script_.myID;
				script_.taskGameObject.GetComponent<taskUnterstuetzen>().rS_ = null;
				Debug.Log("B");
			}
			this.rS_.taskID = taskID2;
			script_.taskID = taskID;
			this.rS_.taskGameObject = null;
			script_.taskGameObject = null;
			this.rS_.DisableOutlineLayer();
			script_.DisableOutlineLayer();
		}
		this.BUTTON_Close();
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x000B1828 File Offset: 0x000AFA28
	private void DrawLine()
	{
		if (this.rS_)
		{
			VectorManager.useDraw3D = true;
			if (!this.initLine)
			{
				this.initLine = true;
				this.line3D = new VectorLine("VerschiebeTaskLine3D", new List<Vector3>(2), 20f, LineType.Continuous, Joins.Weld);
				this.line3D.endCap = "VerschiebeTask";
				GameObject gameObject = this.line3D.rectTransform.gameObject;
				this.myLine = gameObject;
			}
			if (this.myLine && this.myLine.GetComponent<MeshRenderer>())
			{
				this.myLine.GetComponent<MeshRenderer>().material.shader = this.mS_.shaders[0];
			}
			Vector3 point = new Vector3(0f, 0f, 0f);
			RaycastHit raycastHit;
			if (!Physics.Raycast(this.camera_.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out raycastHit))
			{
				this.initLine = false;
				if (this.myLine)
				{
					UnityEngine.Object.Destroy(this.myLine);
				}
				return;
			}
			point = raycastHit.point;
			this.line3D.color = this.guiMain_.colors[21];
			this.line3D.points3[0] = point;
			this.line3D.points3[1] = new Vector3(this.rS_.uiPos.x, this.rS_.uiPos.y - 0.2f, this.rS_.uiPos.z);
			this.line3D.Draw3D();
		}
	}

	// Token: 0x0400153A RID: 5434
	private GameObject main_;

	// Token: 0x0400153B RID: 5435
	private mainScript mS_;

	// Token: 0x0400153C RID: 5436
	private Camera myCamera;

	// Token: 0x0400153D RID: 5437
	private sfxScript sfx_;

	// Token: 0x0400153E RID: 5438
	private RaycastHit hit;

	// Token: 0x0400153F RID: 5439
	public RaycastHit hitOld;

	// Token: 0x04001540 RID: 5440
	private RaycastHit hitEmpty;

	// Token: 0x04001541 RID: 5441
	private GUI_Main guiMain_;

	// Token: 0x04001542 RID: 5442
	private mapScript mapS_;

	// Token: 0x04001543 RID: 5443
	private pickObjectScript pOS_;

	// Token: 0x04001544 RID: 5444
	private Camera camera_;

	// Token: 0x04001545 RID: 5445
	public GameObject[] uiObjects;

	// Token: 0x04001546 RID: 5446
	public LayerMask layerMaskFloor;

	// Token: 0x04001547 RID: 5447
	public roomScript rS_;

	// Token: 0x04001548 RID: 5448
	private roomScript roomOutlineOld;

	// Token: 0x04001549 RID: 5449
	private VectorLine line3D;

	// Token: 0x0400154A RID: 5450
	private bool initLine;

	// Token: 0x0400154B RID: 5451
	private GameObject myLine;
}
