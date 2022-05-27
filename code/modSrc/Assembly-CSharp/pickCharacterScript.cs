using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pickCharacterScript : MonoBehaviour
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
		if (!this.guiMain)
		{
			this.guiMain = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.gummiS_)
		{
			this.gummiS_ = GameObject.Find("CanvasInGameMenu").GetComponent<gummibandScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = base.GetComponent<mapScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = base.GetComponent<roomDataScript>();
		}
		if (!this.pOS_)
		{
			this.pOS_ = base.GetComponent<pickObjectScript>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = this.myCamera.GetComponent<mainCameraScript>();
		}
	}

	
	private void Update()
	{
		this.Pick();
		this.MouseMovement();
	}

	
	private void Pick()
	{
		if (this.gummiS_.isActive)
		{
			return;
		}
		if (this.guiMain.menuOpen && !this.guiMain.uiObjects[15].activeSelf)
		{
			return;
		}
		this.PickupGroupWithKey();
		if (this.guiMain.IsMouseOverGUI())
		{
			if (this.hitOld.transform)
			{
				this.hitOld.transform.gameObject.GetComponent<characterScript>().MouseLeave();
				this.hitOld = this.hitEmpty;
			}
			return;
		}
		int layerMask = 4096;
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out this.hit, 200f, layerMask))
		{
			if (this.hit.transform != this.hitOld.transform)
			{
				if (this.hitOld.transform)
				{
					this.hitOld.transform.gameObject.GetComponent<characterScript>().MouseLeave();
					this.hitOld = this.hitEmpty;
				}
				this.hitOld = this.hit;
				this.hit.transform.gameObject.GetComponent<characterScript>().MouseOver();
			}
		}
		else if (this.hitOld.transform)
		{
			this.hitOld.transform.gameObject.GetComponent<characterScript>().MouseLeave();
			this.hitOld = this.hitEmpty;
		}
		if (this.guiMain.uiObjects[2].GetComponent<Toggle>().isOn)
		{
			return;
		}
		if (Input.GetMouseButtonUp(0) && this.hitOld.transform && !this.guiMain.uiObjects[2].GetComponent<Toggle>().isOn)
		{
			base.StartCoroutine(this.PickChar(this.hitOld.transform.gameObject));
		}
	}

	
	public void PickFromExternObject(GameObject go)
	{
		base.StartCoroutine(this.PickChar(go));
	}

	
	public IEnumerator PickChar(GameObject go)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (!this.guiMain_.menuOpen || this.guiMain.uiObjects[15].activeSelf)
		{
			characterScript component = go.GetComponent<characterScript>();
			if (!component.picked)
			{
				this.oldRoomID = component.roomID;
				this.oldPosition = go.transform.position;
				this.guiMain.OpenMenu(false);
				this.guiMain_.disableRoomGUI = false;
				this.sfx_.PlaySound(9, true);
				component.PickUp();
				this.guiMain.ActivateMenu(this.guiMain.uiObjects[15]);
			}
		}
		else
		{
			Debug.Log("Picking abbrechen!");
		}
		yield break;
	}

	
	private void SetLayer(int newLayer, Transform trans)
	{
		trans.gameObject.layer = newLayer;
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.layer = newLayer;
			if (transform.childCount > 0)
			{
				this.SetLayer(newLayer, transform.transform);
			}
		}
	}

	
	public bool ESC_DropChar()
	{
		if (this.mS_.pickedChars.Count == 1 && this.oldRoomID != -1 && this.oldPosition.x != (float)Mathf.RoundToInt(9999f))
		{
			characterScript component = this.mS_.pickedChars[0].GetComponent<characterScript>();
			component.DropChar(this.oldPosition);
			this.oldPosition = new Vector3(9999f, 9999f, 9999f);
			component.roomID = this.oldRoomID;
			this.oldRoomID = -1;
			if (this.roomOutlineOld)
			{
				this.roomOutlineOld.DisableOutlineLayer();
				this.roomOutlineOld = null;
			}
			if (this.mCamS_.additionalCamera[1].activeSelf)
			{
				this.mCamS_.additionalCamera[1].SetActive(false);
			}
			return true;
		}
		return false;
	}

	
	private void MouseMovement()
	{
		if (!this.mS_)
		{
			return;
		}
		if (this.mS_.pickedChars.Count <= 0)
		{
			return;
		}
		if (this.guiMain_.IsMouseOverGUI())
		{
			return;
		}
		bool flag = Input.GetMouseButtonUp(0);
		this.pOS_.disableMouseButton = flag;
		bool flag2 = false;
		if (this.lastFrameESC && this.ESC_DropChar())
		{
			this.lastFrameESC = false;
			return;
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			this.lastFrameESC = true;
		}
		else
		{
			this.lastFrameESC = false;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out raycastHit, 200f, this.layerMaskChar))
		{
			flag = false;
			flag2 = true;
		}
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out raycastHit, 200f, this.layerMaskFloor))
		{
			float x = raycastHit.point.x;
			float z = raycastHit.point.z;
			Vector3 vector = new Vector3(x, 0.3f, z);
			for (int i = 0; i < this.mS_.pickedChars.Count; i++)
			{
				if (this.mS_.pickedChars[i])
				{
					if (i == 0)
					{
						this.mS_.pickedChars[i].transform.position = Vector3.Lerp(this.mS_.pickedChars[i].transform.position, vector, 0.3f);
						if (this.mS_.pickedChars[i].transform.position.y > 100f)
						{
							this.mS_.pickedChars[i].transform.position = vector;
						}
						if (this.mS_.pickedChars[i].transform.GetChild(0).gameObject.layer != 16)
						{
							this.SetLayer(16, this.mS_.pickedChars[i].transform.GetChild(0));
						}
						if (!this.mCamS_.additionalCamera[1].activeSelf)
						{
							this.mCamS_.additionalCamera[1].SetActive(true);
						}
					}
					else
					{
						this.mS_.pickedChars[i].transform.position = new Vector3(0f, 5000f, 0f);
					}
				}
			}
			this.mCamS_.SetOutlineColor(2, 0.3f, 4);
			int num = Mathf.RoundToInt(vector.x);
			int num2 = Mathf.RoundToInt(vector.z);
			if (this.mapS_.mapRoomID[num, num2] != 1 && !flag2)
			{
				if (this.mapS_.mapRoomScript[num, num2])
				{
					if (!this.rdS_.KeineMitarbeiter(this.mapS_.mapRoomScript[num, num2].typ))
					{
						if (this.roomOutlineOld != this.mapS_.mapRoomScript[num, num2])
						{
							if (this.roomOutlineOld)
							{
								this.roomOutlineOld.DisableOutlineLayer();
							}
							this.roomOutlineOld = this.mapS_.mapRoomScript[num, num2];
							this.mapS_.mapRoomScript[num, num2].SetOutlineLayer();
						}
					}
					else if (this.roomOutlineOld)
					{
						this.roomOutlineOld.DisableOutlineLayer();
						this.roomOutlineOld = null;
					}
				}
			}
			else if (this.roomOutlineOld)
			{
				this.roomOutlineOld.DisableOutlineLayer();
				this.roomOutlineOld = null;
			}
			if (flag)
			{
				if (this.mS_.IsMyBuilding(this.mapS_.mapBuilding[num, num2]))
				{
					if (this.roomOutlineOld)
					{
						this.roomOutlineOld.DisableOutlineLayer();
						this.roomOutlineOld = null;
					}
					if (this.mCamS_.additionalCamera[1].activeSelf)
					{
						this.mCamS_.additionalCamera[1].SetActive(false);
					}
					List<GameObject> list = new List<GameObject>();
					for (int j = 0; j < this.mS_.pickedChars.Count; j++)
					{
						if (this.mS_.pickedChars[j])
						{
							list.Add(this.mS_.pickedChars[j]);
						}
					}
					for (int k = 0; k < list.Count; k++)
					{
						if (list[k])
						{
							list[k].GetComponent<characterScript>().DropChar(vector);
						}
					}
				}
				return;
			}
		}
		else
		{
			for (int l = 0; l < this.mS_.pickedChars.Count; l++)
			{
				if (this.mS_.pickedChars[l])
				{
					this.mS_.pickedChars[l].transform.position = new Vector3(0f, 9999f, 0f);
				}
			}
			if (this.roomOutlineOld)
			{
				this.roomOutlineOld.DisableOutlineLayer();
				this.roomOutlineOld = null;
			}
		}
	}

	
	private void PickupGroupWithKey()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyUp(KeyCode.F1))
			{
				this.SelectGroup(1);
			}
			if (Input.GetKeyUp(KeyCode.F2))
			{
				this.SelectGroup(2);
			}
			if (Input.GetKeyUp(KeyCode.F3))
			{
				this.SelectGroup(3);
			}
			if (Input.GetKeyUp(KeyCode.F4))
			{
				this.SelectGroup(4);
			}
			if (Input.GetKeyUp(KeyCode.F5))
			{
				this.SelectGroup(5);
			}
			if (Input.GetKeyUp(KeyCode.F6))
			{
				this.SelectGroup(6);
			}
			if (Input.GetKeyUp(KeyCode.F7))
			{
				this.SelectGroup(7);
			}
			if (Input.GetKeyUp(KeyCode.F8))
			{
				this.SelectGroup(8);
			}
			if (Input.GetKeyUp(KeyCode.F9))
			{
				this.SelectGroup(9);
			}
			if (Input.GetKeyUp(KeyCode.F10))
			{
				this.SelectGroup(10);
			}
			if (Input.GetKeyUp(KeyCode.F11))
			{
				this.SelectGroup(11);
			}
			if (Input.GetKeyUp(KeyCode.F12))
			{
				this.SelectGroup(12);
			}
		}
	}

	
	private void SelectGroup(int g)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				characterScript component = array[i].GetComponent<characterScript>();
				if (component && component.group == g)
				{
					base.StartCoroutine(this.PickChar(array[i]));
				}
			}
		}
	}

	
	private mainScript mS_;

	
	private GUI_Main guiMain;

	
	private Camera myCamera;

	
	private sfxScript sfx_;

	
	private RaycastHit hit;

	
	public RaycastHit hitOld;

	
	private RaycastHit hitEmpty;

	
	private gummibandScript gummiS_;

	
	private GUI_Main guiMain_;

	
	private mapScript mapS_;

	
	private roomDataScript rdS_;

	
	private pickObjectScript pOS_;

	
	private mainCameraScript mCamS_;

	
	public LayerMask layerMaskChar;

	
	public LayerMask layerMaskFloor;

	
	private int oldRoomID = -1;

	
	private Vector3 oldPosition;

	
	private roomScript roomOutlineOld;

	
	private bool lastFrameESC;
}
