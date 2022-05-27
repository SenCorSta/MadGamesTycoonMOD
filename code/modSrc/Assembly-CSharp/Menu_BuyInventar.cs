using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_BuyInventar : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.buildRoomScript_)
		{
			this.buildRoomScript_ = this.main_.GetComponent<buildRoomScript>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = GameObject.Find("Camera").GetComponent<mainCameraScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.camera_)
		{
			this.camera_ = GameObject.Find("CamMovement");
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.uiObjects[3].GetComponent<Toggle>().isOn = this.mS_.snapObject;
		this.uiObjects[5].GetComponent<Toggle>().isOn = this.mS_.snapRotation;
		for (int i = 0; i < this.uiObjects[2].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[2].transform.GetChild(i).gameObject);
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.BUTTON_CloseSelectInventar(true);
		}
		this.mS_.snapObject = this.uiObjects[3].GetComponent<Toggle>().isOn;
		this.mS_.snapRotation = this.uiObjects[5].GetComponent<Toggle>().isOn;
		if (Input.GetMouseButtonDown(1))
		{
			this.lastCameraPosition = this.camera_.transform.position;
		}
		if (Input.GetMouseButtonUp(1) && this.timerRightMousebutton < 0.2f && Vector3.Distance(this.lastCameraPosition, this.camera_.transform.position) < 0.01f)
		{
			this.BUTTON_Abwahl();
		}
		if (Input.GetMouseButton(1))
		{
			this.timerRightMousebutton += Time.deltaTime;
			return;
		}
		this.timerRightMousebutton = 0f;
	}

	
	public void OpenDropdown()
	{
		this.FindScripts();
		this.uiObjects[0].SetActive(true);
	}

	
	public void CloseDropdown()
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		this.uiObjects[0].SetActive(false);
	}

	
	private void CreatePlaceholder()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1]).transform.parent = this.uiObjects[2].transform;
	}

	
	private void CreateInventarKaufenButton(int typ)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]);
		gameObject.transform.parent = this.uiObjects[2].transform;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		Item_InventarKaufen component = gameObject.GetComponent<Item_InventarKaufen>();
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.mapS_ = this.mapS_;
		component.guiMain_ = this.guiMain_;
		component.sfx_ = this.sfx_;
		component.typ = typ;
	}

	
	private void CreateFilter(string c, int filterArrayID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2]);
		gameObject.transform.parent = this.uiObjects[2].transform;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.GetChild(2).GetComponent<Text>().text = c;
		Filter_InventarKaufen component = gameObject.GetComponent<Filter_InventarKaufen>();
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.mapS_ = this.mapS_;
		component.guiMain_ = this.guiMain_;
		component.sfx_ = this.sfx_;
		component.filterArrayID = filterArrayID;
		if (this.filter[filterArrayID])
		{
			base.StartCoroutine(this.iButton_Click(component));
		}
	}

	
	public IEnumerator iButton_Click(Filter_InventarKaufen script_)
	{
		yield return new WaitForEndOfFrame();
		script_.BUTTON_Click();
		yield break;
	}

	
	public void BUTTON_SelectInventar(int room)
	{
		this.buyInventar = room;
		this.CloseDropdown();
		this.uiObjects[1].SetActive(true);
		switch (room)
		{
		case 0:
			this.CreateFilter(this.tS_.GetText(1877), 0);
			this.CreateInventarKaufenButton(32);
			this.CreateFilter(this.tS_.GetText(1876), 1);
			this.CreateInventarKaufenButton(25);
			this.CreateInventarKaufenButton(26);
			this.CreateInventarKaufenButton(27);
			this.CreateInventarKaufenButton(161);
			this.CreateInventarKaufenButton(162);
			this.CreateInventarKaufenButton(163);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 1:
			this.CreateFilter(this.tS_.GetText(1866), 3);
			this.CreateInventarKaufenButton(1);
			this.CreateInventarKaufenButton(50);
			this.CreateInventarKaufenButton(51);
			this.CreateInventarKaufenButton(52);
			this.CreateInventarKaufenButton(53);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 2:
			this.CreateFilter(this.tS_.GetText(1866), 6);
			this.CreateInventarKaufenButton(6);
			this.CreateInventarKaufenButton(56);
			this.CreateInventarKaufenButton(66);
			this.CreateInventarKaufenButton(67);
			this.CreateInventarKaufenButton(68);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 3:
			this.CreateFilter(this.tS_.GetText(1866), 26);
			this.CreateInventarKaufenButton(74);
			this.CreateInventarKaufenButton(88);
			this.CreateInventarKaufenButton(89);
			this.CreateInventarKaufenButton(90);
			this.CreateInventarKaufenButton(91);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 4:
			this.CreateFilter(this.tS_.GetText(1866), 29);
			this.CreateInventarKaufenButton(75);
			this.CreateInventarKaufenButton(103);
			this.CreateInventarKaufenButton(104);
			this.CreateInventarKaufenButton(105);
			this.CreateInventarKaufenButton(106);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 5:
			this.CreateFilter(this.tS_.GetText(1885), 32);
			this.CreateInventarKaufenButton(76);
			this.CreateInventarKaufenButton(81);
			this.CreateInventarKaufenButton(82);
			this.CreateInventarKaufenButton(119);
			this.CreateInventarKaufenButton(120);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 6:
			this.CreateFilter(this.tS_.GetText(1866), 9);
			this.CreateInventarKaufenButton(48);
			this.CreateInventarKaufenButton(57);
			this.CreateInventarKaufenButton(58);
			this.CreateInventarKaufenButton(59);
			this.CreateInventarKaufenButton(60);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 7:
			this.CreateFilter(this.tS_.GetText(1866), 23);
			this.CreateInventarKaufenButton(61);
			this.CreateInventarKaufenButton(62);
			this.CreateInventarKaufenButton(63);
			this.CreateInventarKaufenButton(64);
			this.CreateInventarKaufenButton(65);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 8:
			this.CreateFilter(this.tS_.GetText(1888), 41);
			this.CreateInventarKaufenButton(149);
			this.CreateInventarKaufenButton(150);
			this.CreateInventarKaufenButton(151);
			this.CreateInventarKaufenButton(152);
			this.CreateInventarKaufenButton(153);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			break;
		case 9:
			this.CreateFilter(this.tS_.GetText(1883), 19);
			this.CreateInventarKaufenButton(47);
			this.CreateInventarKaufenButton(79);
			this.CreateInventarKaufenButton(80);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 10:
			this.CreateFilter(this.tS_.GetText(1886), 35);
			this.CreateInventarKaufenButton(77);
			this.CreateInventarKaufenButton(121);
			this.CreateInventarKaufenButton(122);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 11:
			this.CreateFilter(this.tS_.GetText(1878), 12);
			this.CreateInventarKaufenButton(10);
			this.CreateInventarKaufenButton(11);
			this.CreateInventarKaufenButton(23);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 12:
			this.CreateFilter(this.tS_.GetText(1879), 13);
			this.CreateInventarKaufenButton(43);
			this.CreateInventarKaufenButton(164);
			this.CreateInventarKaufenButton(24);
			this.CreateInventarKaufenButton(37);
			this.CreateInventarKaufenButton(38);
			this.CreateInventarKaufenButton(78);
			this.CreateInventarKaufenButton(99);
			this.CreateFilter(this.tS_.GetText(1876), 14);
			this.CreateInventarKaufenButton(33);
			this.CreateInventarKaufenButton(34);
			this.CreateInventarKaufenButton(35);
			this.CreateInventarKaufenButton(40);
			this.CreateInventarKaufenButton(41);
			this.CreateInventarKaufenButton(42);
			this.CreateInventarKaufenButton(69);
			this.CreateInventarKaufenButton(70);
			this.CreateInventarKaufenButton(71);
			this.CreateInventarKaufenButton(155);
			this.CreateInventarKaufenButton(156);
			this.CreateInventarKaufenButton(157);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 13:
			this.CreateFilter(this.tS_.GetText(1884), 20);
			this.CreateInventarKaufenButton(54);
			this.CreateInventarKaufenButton(111);
			this.CreateInventarKaufenButton(112);
			this.CreateInventarKaufenButton(113);
			this.CreateInventarKaufenButton(114);
			this.CreateInventarKaufenButton(55);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 14:
			this.CreateFilter(this.tS_.GetText(1880), 16);
			this.CreateInventarKaufenButton(36);
			this.CreateInventarKaufenButton(115);
			this.CreateInventarKaufenButton(116);
			this.CreateInventarKaufenButton(117);
			this.CreateInventarKaufenButton(118);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 15:
			this.CreateFilter(this.tS_.GetText(1881), 17);
			this.CreateInventarKaufenButton(45);
			this.CreateInventarKaufenButton(125);
			this.CreateInventarKaufenButton(126);
			this.CreateInventarKaufenButton(127);
			this.CreateInventarKaufenButton(128);
			this.CreateFilter(this.tS_.GetText(1882), 18);
			this.CreateInventarKaufenButton(46);
			this.CreateInventarKaufenButton(154);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		case 16:
			break;
		case 17:
			this.CreateFilter(this.tS_.GetText(1887), 38);
			this.CreateInventarKaufenButton(144);
			this.CreateInventarKaufenButton(145);
			this.CreateInventarKaufenButton(146);
			this.CreateInventarKaufenButton(147);
			this.CreateInventarKaufenButton(148);
			this.CreateFilter(this.tS_.GetText(1867), 4);
			this.CreateInventarKaufenButton(28);
			this.CreateInventarKaufenButton(29);
			this.CreateInventarKaufenButton(30);
			this.CreateInventarKaufenButton(108);
			this.CreateInventarKaufenButton(109);
			this.CreateInventarKaufenButton(110);
			this.CreateInventarKaufenButton(136);
			this.CreateInventarKaufenButton(137);
			this.CreateInventarKaufenButton(138);
			this.CreateInventarKaufenButton(31);
			this.CreateFilter(this.tS_.GetText(1868), 2);
			this.CreateInventarKaufenButton(100);
			this.CreateInventarKaufenButton(101);
			this.CreateInventarKaufenButton(102);
			this.CreateInventarKaufenButton(107);
			this.CreateInventarKaufenButtons_STANDARD(room);
			return;
		default:
			return;
		}
	}

	
	private void CreateInventarKaufenButtons_STANDARD(int room)
	{
		this.CreateFilter(this.tS_.GetText(1875), 44);
		this.CreateInventarKaufenButton(4);
		this.CreateInventarKaufenButton(5);
		this.CreateInventarKaufenButton(131);
		if (room != 5)
		{
			this.CreateInventarKaufenButton(72);
		}
		this.CreateInventarKaufenButton(2);
		this.CreateInventarKaufenButton(73);
		if (room != 5)
		{
			this.CreateInventarKaufenButton(39);
		}
		this.CreateFilter(this.tS_.GetText(1874), 45);
		this.CreateInventarKaufenButton(3);
		this.CreateInventarKaufenButton(44);
		this.CreateInventarKaufenButton(49);
		this.CreateInventarKaufenButton(178);
		this.CreateInventarKaufenButton(179);
		this.CreateInventarKaufenButton(180);
		this.CreateFilter(this.tS_.GetText(1873), 46);
		this.CreateInventarKaufenButton(7);
		this.CreateInventarKaufenButton(8);
		this.CreateInventarKaufenButton(9);
		this.CreateInventarKaufenButton(181);
		this.CreateInventarKaufenButton(182);
		this.CreateInventarKaufenButton(183);
		this.CreateFilter(this.tS_.GetText(1872), 47);
		this.CreateInventarKaufenButton(158);
		this.CreateInventarKaufenButton(159);
		this.CreateInventarKaufenButton(160);
		this.CreateInventarKaufenButton(184);
		this.CreateFilter(this.tS_.GetText(1871), 48);
		if (room != 5)
		{
			this.CreateInventarKaufenButton(17);
			this.CreateInventarKaufenButton(129);
			this.CreateInventarKaufenButton(130);
		}
		this.CreateInventarKaufenButton(132);
		this.CreateInventarKaufenButton(133);
		this.CreateInventarKaufenButton(143);
		this.CreateInventarKaufenButton(135);
		this.CreateInventarKaufenButton(134);
		this.CreateInventarKaufenButton(142);
		if (room != 5)
		{
			this.CreateFilter(this.tS_.GetText(1869), 49);
			this.CreateInventarKaufenButton(12);
			this.CreateInventarKaufenButton(13);
			this.CreateInventarKaufenButton(14);
			this.CreateInventarKaufenButton(15);
			this.CreateInventarKaufenButton(16);
			this.CreateInventarKaufenButton(18);
			this.CreateInventarKaufenButton(19);
			this.CreateInventarKaufenButton(20);
			this.CreateInventarKaufenButton(21);
			this.CreateInventarKaufenButton(22);
			this.CreateInventarKaufenButton(83);
			this.CreateInventarKaufenButton(84);
			this.CreateInventarKaufenButton(85);
			this.CreateInventarKaufenButton(86);
			this.CreateInventarKaufenButton(87);
			this.CreateInventarKaufenButton(165);
			this.CreateInventarKaufenButton(166);
			this.CreateInventarKaufenButton(167);
			this.CreateInventarKaufenButton(168);
			this.CreateInventarKaufenButton(169);
			this.CreateInventarKaufenButton(170);
			this.CreateInventarKaufenButton(171);
			this.CreateInventarKaufenButton(172);
			this.CreateInventarKaufenButton(173);
			this.CreateInventarKaufenButton(174);
		}
		this.CreateFilter(this.tS_.GetText(1870), 50);
		this.CreateInventarKaufenButton(93);
		this.CreateInventarKaufenButton(94);
		this.CreateInventarKaufenButton(95);
		this.CreateInventarKaufenButton(96);
		this.CreateInventarKaufenButton(97);
		this.CreateInventarKaufenButton(98);
		this.CreateInventarKaufenButton(139);
		this.CreateInventarKaufenButton(140);
		this.CreateInventarKaufenButton(141);
		this.CreateInventarKaufenButton(175);
		this.CreateInventarKaufenButton(176);
		this.CreateInventarKaufenButton(177);
	}

	
	public void BUTTON_CloseSelectInventar(bool resetScrollbar)
	{
		if (resetScrollbar)
		{
			this.uiObjects[4].GetComponent<Scrollbar>().value = 1f;
		}
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(7);
		}
		this.mS_.UpdatePathfindingNextFrameExtern();
		if (this.mS_.pickedObject)
		{
			UnityEngine.Object.Destroy(this.mS_.pickedObject);
		}
		this.sfx_.PlaySound(3, true);
		this.DisableAllMenus();
		this.guiMain_.CloseMenu();
		this.mS_.ResetAllColliderLayer();
		base.gameObject.SetActive(false);
		for (int i = 0; i < this.mS_.arrayObjects.Length; i++)
		{
			if (this.mS_.arrayObjects[i])
			{
				this.mS_.arrayObjects[i].GetComponent<objectScript>().WakeUpObject();
			}
		}
	}

	
	public void BUTTON_Abwahl()
	{
		if (this.mS_.pickedObject)
		{
			UnityEngine.Object.Destroy(this.mS_.pickedObject);
		}
	}

	
	public void DisableAllMenus()
	{
		this.uiObjects[0].SetActive(false);
		this.uiObjects[1].SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPrefabs;

	
	private GameObject camera_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private mapScript mapS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private buildRoomScript buildRoomScript_;

	
	private roomDataScript rdS_;

	
	private mainCameraScript mCamS_;

	
	private sfxScript sfx_;

	
	public int buyInventar;

	
	public bool[] filter = new bool[100];

	
	private float timerRightMousebutton;

	
	private Vector3 lastCameraPosition;
}
