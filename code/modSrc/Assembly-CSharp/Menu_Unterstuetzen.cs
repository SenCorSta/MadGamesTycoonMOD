using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x020001BA RID: 442
public class Menu_Unterstuetzen : MonoBehaviour
{
	// Token: 0x0600109E RID: 4254 RVA: 0x0000BC70 File Offset: 0x00009E70
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x000BC964 File Offset: 0x000BAB64
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

	// Token: 0x060010A0 RID: 4256 RVA: 0x0000BC78 File Offset: 0x00009E78
	private void Update()
	{
		this.DrawLine();
		this.MouseMovement();
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x000BCA70 File Offset: 0x000BAC70
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
							this.Accept(this.mapS_.mapRoomScript[num, num2]);
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
				return;
			}
		}
		else if (this.roomOutlineOld)
		{
			this.roomOutlineOld.DisableOutlineLayer();
			this.roomOutlineOld = null;
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x000BCCB0 File Offset: 0x000BAEB0
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

	// Token: 0x060010A3 RID: 4259 RVA: 0x000BCD20 File Offset: 0x000BAF20
	private void Accept(roomScript script_)
	{
		if (this.rS_ == script_)
		{
			return;
		}
		if (this.rS_ && script_ && this.rS_.typ == script_.typ)
		{
			if (script_.taskID != -1)
			{
				GameObject gameObject = GameObject.Find("Task_" + script_.taskID.ToString());
				if (gameObject && gameObject.GetComponent<taskUnterstuetzen>())
				{
					gameObject.GetComponent<taskUnterstuetzen>().Abbrechen();
				}
			}
			taskUnterstuetzen taskUnterstuetzen = this.guiMain_.AddTask_Unterstuetzen();
			taskUnterstuetzen.Init(false);
			taskUnterstuetzen.roomID = script_.myID;
			this.rS_.taskID = taskUnterstuetzen.myID;
			this.rS_.DisableOutlineLayer();
			script_.DisableOutlineLayer();
		}
		this.BUTTON_Close();
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
	private void DrawLine()
	{
		if (this.rS_ && this.mS_.settings_)
		{
			VectorManager.useDraw3D = true;
			if (!this.initLine)
			{
				this.initLine = true;
				this.line3D = new VectorLine("UntersteutzenLine3D", new List<Vector3>(2), 20f, LineType.Continuous, Joins.Weld);
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

	// Token: 0x0400151D RID: 5405
	private GameObject main_;

	// Token: 0x0400151E RID: 5406
	private mainScript mS_;

	// Token: 0x0400151F RID: 5407
	private Camera myCamera;

	// Token: 0x04001520 RID: 5408
	private sfxScript sfx_;

	// Token: 0x04001521 RID: 5409
	private RaycastHit hit;

	// Token: 0x04001522 RID: 5410
	public RaycastHit hitOld;

	// Token: 0x04001523 RID: 5411
	private RaycastHit hitEmpty;

	// Token: 0x04001524 RID: 5412
	private GUI_Main guiMain_;

	// Token: 0x04001525 RID: 5413
	private mapScript mapS_;

	// Token: 0x04001526 RID: 5414
	private pickObjectScript pOS_;

	// Token: 0x04001527 RID: 5415
	private Camera camera_;

	// Token: 0x04001528 RID: 5416
	public GameObject[] uiObjects;

	// Token: 0x04001529 RID: 5417
	public LayerMask layerMaskFloor;

	// Token: 0x0400152A RID: 5418
	public roomScript rS_;

	// Token: 0x0400152B RID: 5419
	private roomScript roomOutlineOld;

	// Token: 0x0400152C RID: 5420
	private VectorLine line3D;

	// Token: 0x0400152D RID: 5421
	private bool initLine;

	// Token: 0x0400152E RID: 5422
	private GameObject myLine;
}
