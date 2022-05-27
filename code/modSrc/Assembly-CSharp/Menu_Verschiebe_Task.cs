using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Menu_Verschiebe_Task : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		this.DrawLine();
		this.MouseMovement();
	}

	
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

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private Camera myCamera;

	
	private sfxScript sfx_;

	
	private RaycastHit hit;

	
	public RaycastHit hitOld;

	
	private RaycastHit hitEmpty;

	
	private GUI_Main guiMain_;

	
	private mapScript mapS_;

	
	private pickObjectScript pOS_;

	
	private Camera camera_;

	
	public GameObject[] uiObjects;

	
	public LayerMask layerMaskFloor;

	
	public roomScript rS_;

	
	private roomScript roomOutlineOld;

	
	private VectorLine line3D;

	
	private bool initLine;

	
	private GameObject myLine;
}
