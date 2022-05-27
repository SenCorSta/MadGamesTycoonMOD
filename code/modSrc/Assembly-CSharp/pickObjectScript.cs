using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000338 RID: 824
public class pickObjectScript : MonoBehaviour
{
	// Token: 0x06001D97 RID: 7575 RVA: 0x00014191 File Offset: 0x00012391
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x0012BB70 File Offset: 0x00129D70
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

	// Token: 0x06001D99 RID: 7577 RVA: 0x00014199 File Offset: 0x00012399
	private void Update()
	{
		this.Pick();
		this.disableMouseButton = false;
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x0012BC50 File Offset: 0x00129E50
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

	// Token: 0x06001D9B RID: 7579 RVA: 0x000141A8 File Offset: 0x000123A8
	private void Unpick()
	{
		if (this.hitOld.transform)
		{
			this.hitOld.transform.gameObject.GetComponent<objectScript>().MouseLeave();
			this.hitOld = this.hitEmpty;
		}
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x0012C00C File Offset: 0x0012A20C
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

	// Token: 0x0400258D RID: 9613
	private mainScript mS_;

	// Token: 0x0400258E RID: 9614
	private GUI_Main guiMain;

	// Token: 0x0400258F RID: 9615
	private mapScript mapS_;

	// Token: 0x04002590 RID: 9616
	private Camera myCamera;

	// Token: 0x04002591 RID: 9617
	private sfxScript sfx_;

	// Token: 0x04002592 RID: 9618
	private pickCharacterScript pcS_;

	// Token: 0x04002593 RID: 9619
	private RaycastHit hit;

	// Token: 0x04002594 RID: 9620
	private RaycastHit hitOld;

	// Token: 0x04002595 RID: 9621
	private RaycastHit hitEmpty;

	// Token: 0x04002596 RID: 9622
	public LayerMask layerMask;

	// Token: 0x04002597 RID: 9623
	private gummibandScript gummiS_;

	// Token: 0x04002598 RID: 9624
	public bool disableMouseButton;

	// Token: 0x04002599 RID: 9625
	public Vector3 oldPosition;

	// Token: 0x0400259A RID: 9626
	public Vector3 oldRotation;

	// Token: 0x0400259B RID: 9627
	public bool reopenBuyInventarMenu;

	// Token: 0x0400259C RID: 9628
	public int buyInventar = -1;
}
