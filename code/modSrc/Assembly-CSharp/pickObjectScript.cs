using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033B RID: 827
public class pickObjectScript : MonoBehaviour
{
	// Token: 0x06001DEE RID: 7662 RVA: 0x0012A80A File Offset: 0x00128A0A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x0012A814 File Offset: 0x00128A14
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

	// Token: 0x06001DF0 RID: 7664 RVA: 0x0012A8F4 File Offset: 0x00128AF4
	private void Update()
	{
		this.Pick();
		this.disableMouseButton = false;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x0012A904 File Offset: 0x00128B04
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

	// Token: 0x06001DF2 RID: 7666 RVA: 0x0012ACBF File Offset: 0x00128EBF
	private void Unpick()
	{
		if (this.hitOld.transform)
		{
			this.hitOld.transform.gameObject.GetComponent<objectScript>().MouseLeave();
			this.hitOld = this.hitEmpty;
		}
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x0012ACFC File Offset: 0x00128EFC
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

	// Token: 0x040025A4 RID: 9636
	private mainScript mS_;

	// Token: 0x040025A5 RID: 9637
	private GUI_Main guiMain;

	// Token: 0x040025A6 RID: 9638
	private mapScript mapS_;

	// Token: 0x040025A7 RID: 9639
	private Camera myCamera;

	// Token: 0x040025A8 RID: 9640
	private sfxScript sfx_;

	// Token: 0x040025A9 RID: 9641
	private pickCharacterScript pcS_;

	// Token: 0x040025AA RID: 9642
	private RaycastHit hit;

	// Token: 0x040025AB RID: 9643
	private RaycastHit hitOld;

	// Token: 0x040025AC RID: 9644
	private RaycastHit hitEmpty;

	// Token: 0x040025AD RID: 9645
	public LayerMask layerMask;

	// Token: 0x040025AE RID: 9646
	private gummibandScript gummiS_;

	// Token: 0x040025AF RID: 9647
	public bool disableMouseButton;

	// Token: 0x040025B0 RID: 9648
	public Vector3 oldPosition;

	// Token: 0x040025B1 RID: 9649
	public Vector3 oldRotation;

	// Token: 0x040025B2 RID: 9650
	public bool reopenBuyInventarMenu;

	// Token: 0x040025B3 RID: 9651
	public int buyInventar = -1;
}
