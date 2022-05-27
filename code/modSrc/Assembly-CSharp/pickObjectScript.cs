using System;
using UnityEngine;
using UnityEngine.UI;


public class pickObjectScript : MonoBehaviour
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
		if (!this.pcS_)
		{
			this.pcS_ = base.GetComponent<pickCharacterScript>();
		}
		if (!this.guiMain)
		{
			this.guiMain = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = base.GetComponent<mapScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.gummiS_)
		{
			this.gummiS_ = GameObject.Find("CanvasInGameMenu").GetComponent<gummibandScript>();
		}
	}

	
	private void Update()
	{
		this.Pick();
		this.disableMouseButton = false;
	}

	
	private void Pick()
	{
		if (this.gummiS_.isActive)
		{
			return;
		}
		if (this.guiMain.uiObjects[3].GetComponent<Toggle>().isOn)
		{
			return;
		}
		if (this.pcS_.hitOld.transform)
		{
			this.Unpick();
			return;
		}
		if (this.mS_.pickedObject)
		{
			this.Unpick();
			return;
		}
		if (this.guiMain.menuOpen && !this.guiMain.uiObjects[20].activeSelf)
		{
			this.Unpick();
			return;
		}
		if (this.guiMain.IsMouseOverGUI())
		{
			this.Unpick();
			return;
		}
		if (Physics.Raycast(this.myCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out this.hit, 200f, this.layerMask))
		{
			if (this.hit.transform != this.hitOld.transform)
			{
				if (this.hit.transform.tag == "Object")
				{
					if (this.hitOld.transform)
					{
						this.hitOld.transform.gameObject.GetComponent<objectScript>().MouseLeave();
						this.hitOld = this.hitEmpty;
					}
					this.hitOld = this.hit;
					this.hit.transform.gameObject.GetComponent<objectScript>().MouseOver();
				}
				else
				{
					this.Unpick();
				}
			}
		}
		else
		{
			this.Unpick();
		}
		if (Input.GetMouseButtonUp(0) && this.hitOld.transform && !this.disableMouseButton)
		{
			if (!this.mS_.settings_TutorialOff && this.guiMain.GetTutorialStep() < 2)
			{
				return;
			}
			if (!this.guiMain.uiObjects[3].GetComponent<Toggle>().isOn)
			{
				if (this.guiMain.uiObjects[20].activeSelf)
				{
					if (Input.GetKey(KeyCode.LeftControl))
					{
						this.mapS_.CreateObject(this.hitOld.transform.gameObject.GetComponent<objectScript>().typ);
						return;
					}
					this.reopenBuyInventarMenu = true;
					this.buyInventar = this.guiMain.uiObjects[20].GetComponent<Menu_BuyInventar>().buyInventar;
					this.guiMain.uiObjects[20].GetComponent<Menu_BuyInventar>().BUTTON_CloseSelectInventar(false);
				}
				else if (Input.GetKey(KeyCode.LeftControl))
				{
					Debug.Log("1");
					if (this.mS_.guiMain_)
					{
						Debug.Log("2");
						objectScript component = this.hitOld.transform.gameObject.GetComponent<objectScript>();
						if (component)
						{
							Debug.Log("3");
							GameObject gameObject = GameObject.Find("Room_" + component.GetRoomID().ToString());
							if (gameObject)
							{
								Debug.Log("4");
								int typ = gameObject.GetComponent<roomScript>().typ;
								this.mS_.guiMain_.DROPDOWN_BuyInventar(typ);
								this.mapS_.CreateObject(this.hitOld.transform.gameObject.GetComponent<objectScript>().typ);
								return;
							}
							this.mS_.guiMain_.DROPDOWN_BuyInventar(0);
							this.mapS_.CreateObject(this.hitOld.transform.gameObject.GetComponent<objectScript>().typ);
							return;
						}
					}
				}
				this.Click(this.hitOld.transform.gameObject);
				this.hitOld = this.hitEmpty;
			}
		}
	}

	
	private void Unpick()
	{
		if (this.hitOld.transform)
		{
			this.hitOld.transform.gameObject.GetComponent<objectScript>().MouseLeave();
			this.hitOld = this.hitEmpty;
		}
	}

	
	public void Click(GameObject go)
	{
		this.guiMain.OpenMenu(false);
		this.sfx_.PlaySound(8, true);
		this.mS_.objectRotation = go.transform.eulerAngles.y;
		objectScript component = go.transform.gameObject.GetComponent<objectScript>();
		objectScript component2 = this.mapS_.CreateObject(component.typ).GetComponent<objectScript>();
		component2.gekauft = true;
		component2.aufladungenAkt = component.aufladungenAkt;
		if (!this.mS_.settings_TutorialOff && component2.typ == 92)
		{
			this.guiMain.SetTutorialStep(3);
		}
		this.oldPosition = go.transform.position;
		this.oldRotation = go.transform.eulerAngles;
		UnityEngine.Object.Destroy(go);
		this.guiMain.ActivateMenu(this.guiMain.uiObjects[0]);
	}

	
	private mainScript mS_;

	
	private GUI_Main guiMain;

	
	private mapScript mapS_;

	
	private Camera myCamera;

	
	private sfxScript sfx_;

	
	private pickCharacterScript pcS_;

	
	private RaycastHit hit;

	
	private RaycastHit hitOld;

	
	private RaycastHit hitEmpty;

	
	public LayerMask layerMask;

	
	private gummibandScript gummiS_;

	
	public bool disableMouseButton;

	
	public Vector3 oldPosition;

	
	public Vector3 oldRotation;

	
	public bool reopenBuyInventarMenu;

	
	public int buyInventar = -1;
}
