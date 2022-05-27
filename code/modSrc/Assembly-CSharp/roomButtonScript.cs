using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000277 RID: 631
public class roomButtonScript : MonoBehaviour
{
	// Token: 0x0600187C RID: 6268 RVA: 0x000F2A45 File Offset: 0x000F0C45
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x000F2A54 File Offset: 0x000F0C54
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
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.brS_)
		{
			this.brS_ = this.main_.GetComponent<buildRoomScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000F2B78 File Offset: 0x000F0D78
	private void Init()
	{
		this.myRectTransform = base.gameObject.GetComponent<RectTransform>();
		this.uiObjects[0].GetComponent<Image>().sprite = this.roomIcons[this.rS_.typ];
		this.RemoveMenus();
		if (this.uiObjects[7])
		{
			this.uiObjects[7].GetComponent<Text>().text = this.rdS_.GetName(this.rS_.typ);
		}
		if (this.uiObjects[11])
		{
			this.uiObjects[11].GetComponent<Text>().text = this.rdS_.GetName(this.rS_.typ);
		}
		if (this.uiObjects[14])
		{
			this.uiObjects[14].GetComponent<Text>().text = this.rdS_.GetName(this.rS_.typ);
		}
		if (this.uiObjects[15])
		{
			this.uiObjects[15].GetComponent<Text>().text = this.rdS_.GetName(this.rS_.typ);
		}
		if (this.uiObjects[16])
		{
			this.uiObjects[16].GetComponent<Text>().text = this.rdS_.GetName(this.rS_.typ);
		}
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x000F2CDB File Offset: 0x000F0EDB
	public void MouseEnter()
	{
		if (this.guiMain_.IsRoomMenuOpen())
		{
			this.mouseOver = false;
			return;
		}
		Debug.Log("KKKKKKKKKKK");
		this.mouseOver = true;
		base.StartCoroutine(this.iSetAsLastSibling());
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000F2D10 File Offset: 0x000F0F10
	public void MouseLeave()
	{
		Debug.Log("SSSSSSS");
		this.mouseOver = false;
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000F2D24 File Offset: 0x000F0F24
	private void SortGuiY()
	{
		if (this.uiObjects[2].activeInHierarchy || this.mouseOver)
		{
			base.StartCoroutine(this.iSetAsLastSibling());
			return;
		}
		int siblingIndex = base.gameObject.transform.GetSiblingIndex();
		Transform child = base.gameObject.transform.parent.GetChild(siblingIndex - 1);
		if (child && child.GetComponent<roomButtonScript>() && child.GetComponent<RectTransform>().position.y < this.myRectTransform.position.y)
		{
			base.gameObject.transform.SetSiblingIndex(siblingIndex - 1);
		}
	}

	// Token: 0x06001882 RID: 6274 RVA: 0x000F2DCA File Offset: 0x000F0FCA
	private IEnumerator iSetAsLastSibling()
	{
		yield return new WaitForEndOfFrame();
		base.gameObject.transform.SetAsLastSibling();
		yield break;
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x000F2DDC File Offset: 0x000F0FDC
	private void Update()
	{
		this.SortGuiY();
		if (this.guiMain_.uiObjects[25].activeSelf || this.guiMain_.uiObjects[15].activeSelf || this.guiMain_.uiObjects[74].activeSelf)
		{
			if (this.uiObjects[6].GetComponent<Button>().interactable)
			{
				this.uiObjects[6].GetComponent<Button>().interactable = false;
				this.uiObjects[6].GetComponent<Image>().raycastTarget = false;
			}
		}
		else if (!this.uiObjects[6].GetComponent<Button>().interactable)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = true;
			this.uiObjects[6].GetComponent<Image>().raycastTarget = true;
		}
		if (this.invisible)
		{
			this.invisibleTimer += Time.deltaTime;
			if (this.invisibleTimer < 0.1f)
			{
				return;
			}
			this.invisibleTimer = 0f;
		}
		if (this.rS_)
		{
			if (this.rS_.camera_)
			{
				Vector2 vector = this.rS_.camera_.WorldToScreenPoint(this.rS_.uiPos);
				if (vector.x < -200f || vector.x >= (float)(Screen.width + 200) || vector.y < -200f || vector.y >= (float)(Screen.height + 200))
				{
					this.invisible = true;
					if (base.transform.GetChild(1).gameObject.activeSelf)
					{
						base.transform.GetChild(1).gameObject.SetActive(false);
					}
					if (base.transform.GetChild(2).gameObject.activeSelf)
					{
						base.transform.GetChild(2).gameObject.SetActive(false);
					}
					return;
				}
				this.invisible = false;
				if (!base.transform.GetChild(1).gameObject.activeSelf)
				{
					base.transform.GetChild(1).gameObject.SetActive(true);
				}
				if (!base.transform.GetChild(2).gameObject.activeSelf)
				{
					base.transform.GetChild(2).gameObject.SetActive(true);
				}
			}
			if (!this.rdS_.KeineMitarbeiter(this.rS_.typ))
			{
				if (this.rS_.myName.Length > 0)
				{
					this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
					{
						this.rS_.myName,
						" [",
						this.rS_.GetMitarbeiter().ToString(),
						"/",
						this.rS_.GetArbeitsplaetze().ToString(),
						"]"
					});
				}
				else
				{
					this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
					{
						"[",
						this.rS_.GetMitarbeiter().ToString(),
						"/",
						this.rS_.GetArbeitsplaetze().ToString(),
						"]"
					});
				}
				if (this.rS_.GetMitarbeiter() > this.rS_.GetArbeitsplaetze())
				{
					this.uiObjects[1].GetComponent<Text>().color = Color.red;
				}
				else
				{
					this.uiObjects[1].GetComponent<Text>().color = Color.white;
				}
				if (this.rS_.pause)
				{
					if (!this.uiObjects[4].activeSelf)
					{
						this.uiObjects[4].SetActive(true);
					}
				}
				else if (this.uiObjects[4].activeSelf)
				{
					this.uiObjects[4].SetActive(false);
				}
				if (this.uiObjects[3].activeSelf)
				{
					if (!this.rS_.pause)
					{
						this.uiObjects[3].GetComponent<Image>().sprite = this.uiSprites[0];
					}
					else
					{
						this.uiObjects[3].GetComponent<Image>().sprite = this.uiSprites[1];
					}
				}
				if (this.uiObjects[5].activeSelf)
				{
					if (!this.rS_.lockKI)
					{
						this.uiObjects[5].GetComponent<Image>().sprite = this.uiSprites[3];
						if (this.uiObjects[12].activeSelf)
						{
							this.uiObjects[12].SetActive(false);
						}
					}
					else
					{
						this.uiObjects[5].GetComponent<Image>().sprite = this.uiSprites[2];
						if (!this.uiObjects[12].activeSelf)
						{
							this.uiObjects[12].SetActive(true);
						}
					}
				}
				if (this.rS_)
				{
					if (this.rS_.IsCrunchtimeRead())
					{
						if (!this.uiObjects[10].activeSelf)
						{
							this.uiObjects[10].SetActive(true);
							return;
						}
					}
					else if (this.uiObjects[10].activeSelf)
					{
						this.uiObjects[10].SetActive(false);
						return;
					}
				}
			}
			else
			{
				if (this.rS_.myName.Length > 0)
				{
					this.uiObjects[1].GetComponent<Text>().text = this.rS_.myName;
					return;
				}
				this.uiObjects[1].GetComponent<Text>().text = "";
			}
		}
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x000F3345 File Offset: 0x000F1545
	private void OnDisable()
	{
		if (!this.rS_)
		{
			return;
		}
		if (this.rS_.myUI_Line)
		{
			this.rS_.myUI_Line.SetActive(false);
		}
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x000F3378 File Offset: 0x000F1578
	private void OnEnable()
	{
		if (!this.rS_)
		{
			return;
		}
		if (this.rS_.myUI_Line)
		{
			this.rS_.myUI_Line.SetActive(true);
		}
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x000F33AC File Offset: 0x000F15AC
	public bool CloseAllMenus()
	{
		for (int i = 0; i < this.uiOptions.Length; i++)
		{
			if (this.uiOptions[i] && this.uiOptions[i].activeSelf)
			{
				this.uiOptions[i].SetActive(false);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000F33FC File Offset: 0x000F15FC
	public bool IsMenuOpen()
	{
		for (int i = 0; i < this.uiOptions.Length; i++)
		{
			if (this.uiOptions[i] && this.uiOptions[i].activeSelf)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x000F3440 File Offset: 0x000F1640
	private void RemoveMenus()
	{
		switch (this.rS_.typ)
		{
		case 1:
			for (int i = 0; i < this.uiOptions.Length; i++)
			{
				if (i != 1 && i != 3 && i != 7 && i != 8 && i != 15 && i != 16 && i != 33 && i != 38 && this.uiOptions[i])
				{
					UnityEngine.Object.Destroy(this.uiOptions[i]);
				}
			}
			for (int j = 0; j < this.uiWindows.Length; j++)
			{
				if (j != 1 && j != 2 && j != 3 && j != 6 && j != 7 && j != 22 && j != 25 && this.uiWindows[j])
				{
					UnityEngine.Object.Destroy(this.uiWindows[j]);
				}
			}
			return;
		case 2:
			for (int k = 0; k < this.uiOptions.Length; k++)
			{
				if (k != 0 && k != 2 && k != 8 && this.uiOptions[k])
				{
					UnityEngine.Object.Destroy(this.uiOptions[k]);
				}
			}
			for (int l = 0; l < this.uiWindows.Length; l++)
			{
				if (l != 0 && l != 3 && this.uiWindows[l])
				{
					UnityEngine.Object.Destroy(this.uiWindows[l]);
				}
			}
			return;
		case 3:
			for (int m = 0; m < this.uiOptions.Length; m++)
			{
				if (m != 20 && m != 8 && m != 21 && m != 15 && m != 28 && m != 31 && m != 38 && m != 39 && this.uiOptions[m])
				{
					UnityEngine.Object.Destroy(this.uiOptions[m]);
				}
			}
			for (int n = 0; n < this.uiWindows.Length; n++)
			{
				if (n != 3 && n != 10 && n != 11 && n != 6 && n != 15 && n != 20 && n != 25 && n != 26 && this.uiWindows[n])
				{
					UnityEngine.Object.Destroy(this.uiWindows[n]);
				}
			}
			return;
		case 4:
			for (int num = 0; num < this.uiOptions.Length; num++)
			{
				if (num != 22 && num != 8 && num != 23 && num != 15 && num != 31 && num != 38 && this.uiOptions[num])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num]);
				}
			}
			for (int num2 = 0; num2 < this.uiWindows.Length; num2++)
			{
				if (num2 != 3 && num2 != 12 && num2 != 6 && num2 != 20 && num2 != 25 && this.uiWindows[num2])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num2]);
				}
			}
			return;
		case 5:
			for (int num3 = 0; num3 < this.uiOptions.Length; num3++)
			{
				if (num3 != 24 && num3 != 8 && num3 != 25 && num3 != 15 && num3 != 31 && num3 != 38 && this.uiOptions[num3])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num3]);
				}
			}
			for (int num4 = 0; num4 < this.uiWindows.Length; num4++)
			{
				if (num4 != 3 && num4 != 13 && num4 != 6 && num4 != 20 && num4 != 25 && this.uiWindows[num4])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num4]);
				}
			}
			return;
		case 6:
			for (int num5 = 0; num5 < this.uiOptions.Length; num5++)
			{
				if (num5 != 11 && num5 != 8 && num5 != 12 && num5 != 30 && num5 != 32 && num5 != 40 && this.uiOptions[num5])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num5]);
				}
			}
			for (int num6 = 0; num6 < this.uiWindows.Length; num6++)
			{
				if (num6 != 3 && num6 != 4 && num6 != 18 && num6 != 21 && num6 != 27 && this.uiWindows[num6])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num6]);
				}
			}
			return;
		case 7:
			for (int num7 = 0; num7 < this.uiOptions.Length; num7++)
			{
				if (num7 != 17 && num7 != 8 && num7 != 18 && num7 != 19 && num7 != 41 && this.uiOptions[num7])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num7]);
				}
			}
			for (int num8 = 0; num8 < this.uiWindows.Length; num8++)
			{
				if (num8 != 3 && num8 != 8 && num8 != 9 && num8 != 28 && this.uiWindows[num8])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num8]);
				}
			}
			return;
		case 8:
			for (int num9 = 0; num9 < this.uiOptions.Length; num9++)
			{
				if (num9 != 8 && num9 != 15 && num9 != 36 && num9 != 37 && num9 != 38 && this.uiOptions[num9])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num9]);
				}
			}
			for (int num10 = 0; num10 < this.uiWindows.Length; num10++)
			{
				if (num10 != 3 && num10 != 6 && num10 != 24 && num10 != 25 && this.uiWindows[num10])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num10]);
				}
			}
			return;
		case 9:
			for (int num11 = 0; num11 < this.uiOptions.Length; num11++)
			{
				if (num11 != 9 && this.uiOptions[num11])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num11]);
				}
			}
			for (int num12 = 0; num12 < this.uiWindows.Length; num12++)
			{
				if (num12 != 17 && this.uiWindows[num12])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num12]);
				}
			}
			return;
		case 10:
			for (int num13 = 0; num13 < this.uiOptions.Length; num13++)
			{
				if (num13 != 26 && num13 != 8 && num13 != 27 && num13 != 15 && num13 != 31 && num13 != 38 && this.uiOptions[num13])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num13]);
				}
			}
			for (int num14 = 0; num14 < this.uiWindows.Length; num14++)
			{
				if (num14 != 3 && num14 != 14 && num14 != 6 && num14 != 20 && num14 != 25 && this.uiWindows[num14])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num14]);
				}
			}
			return;
		case 11:
			for (int num15 = 0; num15 < this.uiOptions.Length; num15++)
			{
				if (num15 != 4 && this.uiOptions[num15])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num15]);
				}
			}
			for (int num16 = 0; num16 < this.uiWindows.Length; num16++)
			{
				if (this.uiWindows[num16])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num16]);
				}
			}
			return;
		case 12:
			for (int num17 = 0; num17 < this.uiOptions.Length; num17++)
			{
				if (num17 != 5 && this.uiOptions[num17])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num17]);
				}
			}
			for (int num18 = 0; num18 < this.uiWindows.Length; num18++)
			{
				if (this.uiWindows[num18])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num18]);
				}
			}
			return;
		case 13:
			for (int num19 = 0; num19 < this.uiOptions.Length; num19++)
			{
				if (num19 != 13 && num19 != 14 && this.uiOptions[num19])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num19]);
				}
			}
			for (int num20 = 0; num20 < this.uiWindows.Length; num20++)
			{
				if (num20 != 5 && this.uiWindows[num20])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num20]);
				}
			}
			return;
		case 14:
			for (int num21 = 0; num21 < this.uiOptions.Length; num21++)
			{
				if (num21 != 6 && num21 != 8 && num21 != 29 && num21 != 15 && num21 != 38 && this.uiOptions[num21])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num21]);
				}
			}
			for (int num22 = 0; num22 < this.uiWindows.Length; num22++)
			{
				if (num22 != 3 && num22 != 6 && num22 != 16 && num22 != 25 && this.uiWindows[num22])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num22]);
				}
			}
			return;
		case 15:
			for (int num23 = 0; num23 < this.uiOptions.Length; num23++)
			{
				if (num23 != 10 && this.uiOptions[num23])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num23]);
				}
			}
			for (int num24 = 0; num24 < this.uiWindows.Length; num24++)
			{
				if (num24 != 19 && this.uiWindows[num24])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num24]);
				}
			}
			return;
		case 16:
			for (int num25 = 0; num25 < this.uiOptions.Length; num25++)
			{
				if (this.uiOptions[num25])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num25]);
				}
			}
			for (int num26 = 0; num26 < this.uiWindows.Length; num26++)
			{
				if (this.uiWindows[num26])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num26]);
				}
			}
			return;
		case 17:
			for (int num27 = 0; num27 < this.uiOptions.Length; num27++)
			{
				if (num27 != 8 && num27 != 15 && num27 != 34 && num27 != 35 && num27 != 38 && this.uiOptions[num27])
				{
					UnityEngine.Object.Destroy(this.uiOptions[num27]);
				}
			}
			for (int num28 = 0; num28 < this.uiWindows.Length; num28++)
			{
				if (num28 != 3 && num28 != 6 && num28 != 23 && num28 != 25 && this.uiWindows[num28])
				{
					UnityEngine.Object.Destroy(this.uiWindows[num28]);
				}
			}
			return;
		default:
			return;
		}
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x000F3E5C File Offset: 0x000F205C
	public void BUTTON_Main()
	{
		this.sfx_.PlaySound(3, true);
		int num = -1;
		this.guiMain_.guiMainButtons_.CloseAllDropdowns();
		switch (this.rS_.typ)
		{
		case 1:
			if (this.rS_.taskID == -1)
			{
				num = 1;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject)
				{
					if (gameObject.GetComponent<taskGame>())
					{
						num = 7;
						if (this.rS_.IsGameDevCompleteOrg())
						{
							this.uiObjects[8].GetComponent<Button>().interactable = true;
							this.uiObjects[8].transform.GetChild(0).GetComponent<Text>().color = Color.black;
						}
						else
						{
							this.uiObjects[8].GetComponent<Button>().interactable = false;
							this.uiObjects[8].transform.GetChild(0).GetComponent<Text>().color = Color.grey;
						}
						if (this.rS_.IsDevAddon())
						{
							this.uiObjects[9].GetComponent<Button>().interactable = false;
							this.uiObjects[9].transform.GetChild(0).GetComponent<Text>().color = Color.grey;
						}
						else
						{
							this.uiObjects[9].GetComponent<Button>().interactable = true;
							this.uiObjects[9].transform.GetChild(0).GetComponent<Text>().color = Color.black;
						}
					}
					else if (gameObject.GetComponent<taskEngine>())
					{
						num = 3;
					}
					else if (gameObject.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
					else if (gameObject.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject.GetComponent<taskUpdate>())
					{
						num = 16;
					}
					else if (gameObject.GetComponent<taskF2PUpdate>())
					{
						num = 33;
					}
				}
			}
			break;
		case 2:
			if (this.rS_.taskID == -1)
			{
				num = 0;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject2 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject2)
				{
					if (gameObject2.GetComponent<taskForschung>())
					{
						num = 2;
					}
					else if (gameObject2.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
				}
			}
			break;
		case 3:
			if (this.rS_.taskID == -1)
			{
				num = 20;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject3 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject3)
				{
					if (gameObject3.GetComponent<taskBugfixing>())
					{
						num = 21;
					}
					else if (gameObject3.GetComponent<taskGameplayVerbessern>())
					{
						num = 21;
					}
					else if (gameObject3.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject3.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject3.GetComponent<taskSpielbericht>())
					{
						num = 28;
					}
					else if (gameObject3.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
					else if (gameObject3.GetComponent<taskPolishing>())
					{
						num = 31;
					}
					else if (gameObject3.GetComponent<taskWait>())
					{
						num = 39;
					}
				}
			}
			break;
		case 4:
			if (this.rS_.taskID == -1)
			{
				num = 22;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject4 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject4)
				{
					if (gameObject4.GetComponent<taskGrafikVerbessern>())
					{
						num = 23;
					}
					else if (gameObject4.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject4.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject4.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
					else if (gameObject4.GetComponent<taskPolishing>())
					{
						num = 31;
					}
				}
			}
			break;
		case 5:
			if (this.rS_.taskID == -1)
			{
				num = 24;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject5 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject5)
				{
					if (gameObject5.GetComponent<taskSoundVerbessern>())
					{
						num = 25;
					}
					else if (gameObject5.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject5.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject5.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
					else if (gameObject5.GetComponent<taskPolishing>())
					{
						num = 31;
					}
				}
			}
			break;
		case 6:
			if (this.rS_.taskID == -1)
			{
				num = 11;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject6 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject6)
				{
					if (gameObject6.GetComponent<taskMarketing>())
					{
						num = 12;
					}
					else if (gameObject6.GetComponent<taskMarktforschung>())
					{
						num = 30;
					}
					else if (gameObject6.GetComponent<taskMarketingSpezial>())
					{
						num = 32;
					}
					else if (gameObject6.GetComponent<taskMitarbeitersuche>())
					{
						num = 40;
					}
					else if (gameObject6.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
				}
			}
			break;
		case 7:
			if (this.rS_.taskID == -1)
			{
				num = 17;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject7 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject7)
				{
					if (gameObject7.GetComponent<taskFankampagne>())
					{
						num = 18;
					}
					else if (gameObject7.GetComponent<taskSupport>())
					{
						num = 19;
					}
					else if (gameObject7.GetComponent<taskFanshop>())
					{
						num = 41;
					}
					else if (gameObject7.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
				}
			}
			break;
		case 8:
			if (this.rS_.taskID == -1)
			{
				num = 36;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject8 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject8)
				{
					if (this.rS_.IsKonsoleDevCompleteOrg())
					{
						this.uiObjects[13].GetComponent<Button>().interactable = true;
						this.uiObjects[13].transform.GetChild(0).GetComponent<Text>().color = Color.black;
					}
					else
					{
						this.uiObjects[13].GetComponent<Button>().interactable = false;
						this.uiObjects[13].transform.GetChild(0).GetComponent<Text>().color = Color.grey;
					}
					if (gameObject8.GetComponent<taskKonsole>())
					{
						num = 37;
					}
					else if (gameObject8.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject8.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject8.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
				}
			}
			break;
		case 9:
			num = 9;
			break;
		case 10:
			if (this.rS_.taskID == -1)
			{
				num = 26;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject9 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject9)
				{
					if (gameObject9.GetComponent<taskAnimationVerbessern>())
					{
						num = 27;
					}
					else if (gameObject9.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject9.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject9.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
					else if (gameObject9.GetComponent<taskPolishing>())
					{
						num = 31;
					}
				}
			}
			break;
		case 11:
			num = 4;
			break;
		case 12:
			num = 5;
			break;
		case 13:
			if (this.rS_.taskID == -1)
			{
				num = 13;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject10 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject10 && gameObject10.GetComponent<taskTraining>())
				{
					num = 14;
				}
			}
			break;
		case 14:
			if (this.rS_.taskID == -1)
			{
				num = 6;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject11 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject11)
				{
					if (gameObject11.GetComponent<taskProduction>())
					{
						num = 29;
					}
					else if (gameObject11.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject11.GetComponent<taskContractWait>())
					{
						num = 38;
					}
				}
			}
			break;
		case 15:
			num = 10;
			break;
		case 16:
			this.guiMain_.uiObjects[213].SetActive(true);
			this.guiMain_.uiObjects[213].GetComponent<Menu_Immobilien>().Init(this.rS_);
			this.guiMain_.OpenMenu(true);
			break;
		case 17:
			if (this.rS_.taskID == -1)
			{
				num = 34;
			}
			if (this.rS_.taskID != -1)
			{
				GameObject gameObject12 = GameObject.Find("Task_" + this.rS_.taskID.ToString());
				if (gameObject12)
				{
					if (gameObject12.GetComponent<taskArcadeProduction>())
					{
						num = 35;
					}
					else if (gameObject12.GetComponent<taskContractWork>())
					{
						num = 15;
					}
					else if (gameObject12.GetComponent<taskContractWait>())
					{
						num = 38;
					}
					else if (gameObject12.GetComponent<taskUnterstuetzen>())
					{
						num = 8;
					}
				}
			}
			break;
		}
		if (num == -1)
		{
			return;
		}
		bool activeSelf = this.uiOptions[num].activeSelf;
		this.guiMain_.CloseAllRoomButtons();
		this.uiOptions[num].SetActive(!activeSelf);
		this.uiObjects[2].SetActive(!activeSelf);
		this.uiObjects[2].transform.parent = this.uiOptions[num].transform;
		if (this.rdS_.KeineMitarbeiter(this.rS_.typ) && this.uiObjects[2].transform.childCount == 8)
		{
			UnityEngine.Object.Destroy(this.uiObjects[2].transform.GetChild(0).gameObject);
			UnityEngine.Object.Destroy(this.uiObjects[2].transform.GetChild(1).gameObject);
			UnityEngine.Object.Destroy(this.uiObjects[2].transform.GetChild(2).gameObject);
		}
		base.gameObject.transform.SetAsLastSibling();
		this.mS_.PauseGame(!activeSelf);
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x000F4A00 File Offset: 0x000F2C00
	public void BUTTON_AutoUpdate()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		if (this.rS_.UpdateInventar(false))
		{
			this.guiMain_.uiObjects[253].SetActive(true);
			this.guiMain_.uiObjects[253].GetComponent<Menu_W_UpdateObjects>().Init(this.rS_);
			return;
		}
		this.guiMain_.MessageBox(this.tS_.GetText(1286), true);
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000F4A8C File Offset: 0x000F2C8C
	public void BUTTON_RenameRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.uiObjects[17].SetActive(true);
		this.guiMain_.uiObjects[17].GetComponent<Menu_RenameRoom>().Init(this.rS_);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x000F4AE4 File Offset: 0x000F2CE4
	public void BUTTON_DemolishRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.uiObjects[18].SetActive(true);
		this.guiMain_.uiObjects[18].GetComponent<Menu_DemolishRoom>().Init(this.rS_);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000F4B3C File Offset: 0x000F2D3C
	public void BUTTON_RedesignRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(true);
		this.guiMain_.uiObjects[19].SetActive(true);
		this.guiMain_.uiObjects[19].GetComponent<Menu_BuildRoom>().BUTTON_SelectRoom(this.rS_.typ);
		this.brS_.CreateOldRoomLayout(this.rS_);
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x000F4BAC File Offset: 0x000F2DAC
	public void BUTTON_MoveRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(true);
		this.guiMain_.uiObjects[19].SetActive(true);
		this.guiMain_.uiObjects[19].GetComponent<Menu_BuildRoom>().BUTTON_SelectRoom(this.rS_.typ);
		this.brS_.MoveRoom(this.rS_);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x000F4C1A File Offset: 0x000F2E1A
	public void BUTTON_PauseRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.rS_.pause = !this.rS_.pause;
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x000F4C42 File Offset: 0x000F2E42
	public void BUTTON_LockKIRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.rS_.lockKI = !this.rS_.lockKI;
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x000F4C6C File Offset: 0x000F2E6C
	public void BUTTON_RoomPersonal()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[26]);
		this.guiMain_.uiObjects[26].GetComponent<Menu_Personal_InRoom>().Init(this.rS_.myID);
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x000F4CD0 File Offset: 0x000F2ED0
	public void BUTTON_Verschiebe_Task()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.disableRoomGUI = false;
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[25]);
		this.guiMain_.uiObjects[25].GetComponent<Menu_Verschiebe_Task>().rS_ = this.rS_;
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x000F4D3C File Offset: 0x000F2F3C
	public void BUTTON_Unterstuetzen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.disableRoomGUI = false;
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[74]);
		this.guiMain_.uiObjects[74].GetComponent<Menu_Unterstuetzen>().rS_ = this.rS_;
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000F4DA8 File Offset: 0x000F2FA8
	public void BUTTON_LagerRestbestand()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[225]);
		this.guiMain_.uiObjects[225].GetComponent<Menu_LagerSelect>().Init(this.rS_);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x000F4E0C File Offset: 0x000F300C
	public void BUTTON_Forschung(int i)
	{
		if ((i == 4 || i == 6) && !this.forschungSonstiges_.IsErforscht(39))
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[21]);
		this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>().Init(this.rS_.myID, i);
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x000F4E88 File Offset: 0x000F3088
	public void BUTTON_Forschung_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskForschung>().automatic;
			this.rS_.taskGameObject.GetComponent<taskForschung>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000F4EF8 File Offset: 0x000F30F8
	public void BUTTON_Spielbericht_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskSpielbericht>().automatic;
			this.rS_.taskGameObject.GetComponent<taskSpielbericht>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x000F4F68 File Offset: 0x000F3168
	public void BUTTON_GameUpdate_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskUpdate>().automatic;
			this.rS_.taskGameObject.GetComponent<taskUpdate>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x000F4FD8 File Offset: 0x000F31D8
	public void BUTTON_F2PUpdate_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskF2PUpdate>().automatic;
			this.rS_.taskGameObject.GetComponent<taskF2PUpdate>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000F5048 File Offset: 0x000F3248
	public void BUTTON_Dev_Game()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		this.guiMain_.uiObjects[57].GetComponent<Menu_DevGameMain>().Init(this.rS_);
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000F50A8 File Offset: 0x000F32A8
	public void BUTTON_Dev_Game_GameplayFeatures()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + component.gameID.ToString());
				if (gameObject2)
				{
					gameScript component2 = gameObject2.GetComponent<gameScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[348]);
						this.guiMain_.uiObjects[348].GetComponent<Menu_Dev_ChangeGameplayFeatures>().Init(component2);
					}
				}
			}
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000F5170 File Offset: 0x000F3370
	public void BUTTON_Dev_Game_Entwicklungsbericht()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + component.gameID.ToString());
				if (gameObject2)
				{
					gameScript component2 = gameObject2.GetComponent<gameScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[73]);
						this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>().Init(component2, this.rS_);
					}
				}
			}
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000F5238 File Offset: 0x000F3438
	public void BUTTON_Dev_Game_Abschliessen()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<taskGame>().CompleteOpenMenue();
		}
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000F5278 File Offset: 0x000F3478
	public void BUTTON_Dev_Konsole_Abschliessen()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<taskKonsole>().CompleteOpenMenue();
		}
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x000F52B8 File Offset: 0x000F34B8
	public void BUTTON_Dev_ChangeDesignprioritaet()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + component.gameID.ToString());
				if (gameObject2)
				{
					gameScript component2 = gameObject2.GetComponent<gameScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[371]);
						this.guiMain_.uiObjects[371].GetComponent<Menu_Dev_ChangeDesignproritaet>().Init(component2);
					}
				}
			}
		}
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000F5380 File Offset: 0x000F3580
	public void BUTTON_Dev_ChangeCopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + component.gameID.ToString());
				if (gameObject2)
				{
					gameScript component2 = gameObject2.GetComponent<gameScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[365]);
						this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().Init(component2);
					}
				}
			}
		}
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000F5448 File Offset: 0x000F3648
	public void BUTTON_Dev_ChangePlatform()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("GAME_" + component.gameID.ToString());
				if (gameObject2)
				{
					gameScript component2 = gameObject2.GetComponent<gameScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[102]);
						this.guiMain_.uiObjects[102].GetComponent<Menu_Dev_ChangePlatform>().Init(component2);
					}
				}
			}
		}
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000F550C File Offset: 0x000F370C
	public void BUTTON_Dev_Auftrag()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[95]);
		this.guiMain_.uiObjects[95].GetComponent<Menu_Dev_Auftrag>().Init(this.rS_);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000F556C File Offset: 0x000F376C
	public void BUTTON_GFX_Auftrag()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000F55CC File Offset: 0x000F37CC
	public void BUTTON_WERK_Auftrag()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000F562C File Offset: 0x000F382C
	public void BUTTON_PROD_Auftrag()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000F568C File Offset: 0x000F388C
	public void BUTTON_HARD_Auftrag()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[96]);
		this.guiMain_.uiObjects[96].GetComponent<Menu_Dev_AuftragSelect>().Init(this.rS_);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000F56EC File Offset: 0x000F38EC
	public void BUTTON_Dev_Engines()
	{
		if (!this.unlock_.Get(58))
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[36]);
		this.guiMain_.uiObjects[36].GetComponent<Menu_Dev_EngineMain>().Init(this.rS_);
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000F575C File Offset: 0x000F395C
	public void BUTTON_Dev_Addons()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x000F57BC File Offset: 0x000F39BC
	public void BUTTON_Unterstuetzung_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject && gameObject.GetComponent<taskUnterstuetzen>())
		{
			gameObject.GetComponent<taskUnterstuetzen>().Abbrechen();
		}
		this.rS_.taskID = -1;
		this.rS_.taskGameObject = null;
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x000F5844 File Offset: 0x000F3A44
	public void BUTTON_Marketing_Mitarbeitersuche()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[344]);
		this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().Init(this.rS_);
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000F58A8 File Offset: 0x000F3AA8
	public void BUTTON_Marketing_NeueKampagne()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[88]);
		this.guiMain_.uiObjects[88].GetComponent<Menu_Marketing_Main>().Init(this.rS_);
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x000F5908 File Offset: 0x000F3B08
	public void BUTTON_Marketing_SpezialKampagne()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[294]);
		this.guiMain_.uiObjects[294].GetComponent<Menu_MarketingSpezial>().Init(this.rS_);
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x000F596C File Offset: 0x000F3B6C
	public void BUTTON_Marketing_Marktforschung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[230]);
		this.guiMain_.uiObjects[230].GetComponent<Menu_Marktforschung>().Init(this.rS_);
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x000F59D0 File Offset: 0x000F3BD0
	public void BUTTON_Support_Fankampagne()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[139]);
		this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>().Init(this.rS_);
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x000F5A34 File Offset: 0x000F3C34
	public void BUTTON_Support_Anrufe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[141]);
		this.guiMain_.uiObjects[141].GetComponent<Menu_Support_Anrufe>().Init(this.rS_);
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x000F5A98 File Offset: 0x000F3C98
	public void BUTTON_Support_Fanshop()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[368]);
		this.guiMain_.uiObjects[368].GetComponent<Menu_Support_Fanshop>().Init(this.rS_);
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x000F5AFC File Offset: 0x000F3CFC
	public void BUTTON_Support_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject && this.rS_.taskGameObject.GetComponent<taskFankampagne>())
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskFankampagne>().automatic;
			this.rS_.taskGameObject.GetComponent<taskFankampagne>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x000F5B84 File Offset: 0x000F3D84
	public void BUTTON_Marketing_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskMarketing>().automatic;
			this.rS_.taskGameObject.GetComponent<taskMarketing>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000F5BF4 File Offset: 0x000F3DF4
	public void BUTTON_Mitarbeitersuche_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskMitarbeitersuche>().automatic;
			this.rS_.taskGameObject.GetComponent<taskMitarbeitersuche>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000F5C64 File Offset: 0x000F3E64
	public void BUTTON_Production_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskProduction>().automatic;
			this.rS_.taskGameObject.GetComponent<taskProduction>().automatic = !automatic;
			if (this.rS_.taskGameObject.GetComponent<taskProduction>().GetAmount() <= 0 && !this.rS_.taskGameObject.GetComponent<taskProduction>().automatic)
			{
				UnityEngine.Object.Destroy(this.rS_.taskGameObject);
			}
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x000F5D14 File Offset: 0x000F3F14
	public void BUTTON_Training_NeuerKurs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[92]);
		this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>().Init(this.rS_);
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x000F5D74 File Offset: 0x000F3F74
	public void BUTTON_Training_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskTraining>().automatic;
			this.rS_.taskGameObject.GetComponent<taskTraining>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x000F5DE4 File Offset: 0x000F3FE4
	public void BUTTON_QA_Bugfixing()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[171]);
		this.guiMain_.uiObjects[171].GetComponent<Menu_QA_BugfixingSelectGame>().Init(this.rS_);
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x000F5E48 File Offset: 0x000F4048
	public void BUTTON_QA_GameplayVerbessern()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[172]);
		this.guiMain_.uiObjects[172].GetComponent<Menu_QA_GameplayVerbessern>().Init(this.rS_);
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000F5EAC File Offset: 0x000F40AC
	public void BUTTON_QA_Spielbericht()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[180]);
		this.guiMain_.uiObjects[180].GetComponent<Menu_QA_SpielberichtMain>().Init(this.rS_);
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x000F5F10 File Offset: 0x000F4110
	public void BUTTON_GFX_Polishing()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[279]);
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().Init(this.rS_);
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x000F5F74 File Offset: 0x000F4174
	public void BUTTON_GFX_GrafikVerbessern()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[174]);
		this.guiMain_.uiObjects[174].GetComponent<Menu_GFX_GrafikVerbessern>().Init(this.rS_);
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x000F5FD8 File Offset: 0x000F41D8
	public void BUTTON_SFX_SoundsVerbessern()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[176]);
		this.guiMain_.uiObjects[176].GetComponent<Menu_SFX_SoundVerbessern>().Init(this.rS_);
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x000F603C File Offset: 0x000F423C
	public void BUTTON_MOCAP_AnimationVerbessern()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[178]);
		this.guiMain_.uiObjects[178].GetComponent<Menu_MOCAP_AnimationVerbessern>().Init(this.rS_);
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x000F60A0 File Offset: 0x000F42A0
	public void BUTTON_PROD_Packung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[220]);
		this.guiMain_.uiObjects[220].GetComponent<Menu_PackungSelect>().Init(this.rS_);
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x000F6104 File Offset: 0x000F4304
	public void BUTTON_PROD_Produce()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[221]);
		this.guiMain_.uiObjects[221].GetComponent<Menu_ProductionSelect>().Init(this.rS_);
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x000F6167 File Offset: 0x000F4367
	public void BUTTON_SERVER_AboPreis()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[245]);
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000F61A0 File Offset: 0x000F43A0
	public void BUTTON_SERVER_Shutdown()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[244]);
		this.guiMain_.uiObjects[244].GetComponent<Menu_W_ServerDown>().Init(this.rS_);
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x000F6203 File Offset: 0x000F4403
	public void BUTTON_SERVER_F2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.MessageBox(this.tS_.GetText(408), true);
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000F623C File Offset: 0x000F443C
	public void BUTTON_SERVER_MMOtoF2P()
	{
		if (!this.unlock_.Get(22))
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[285]);
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x000F6203 File Offset: 0x000F4403
	public void BUTTON_SERVER_GamePass()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.MessageBox(this.tS_.GetText(408), true);
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x000F6290 File Offset: 0x000F4490
	public void BUTTON_WERK_Produce()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[304]);
		this.guiMain_.uiObjects[304].GetComponent<Menu_ProductionArcadeSelect>().Init(this.rS_);
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x000F62F4 File Offset: 0x000F44F4
	public void BUTTON_HARD_NeueKonsoleEntwickeln()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[317]);
		this.guiMain_.uiObjects[317].GetComponent<Menu_Dev_KonsoleMain>().Init(this.rS_);
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x000F6358 File Offset: 0x000F4558
	public void BUTTON_HARD_Entwicklungsbericht()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			taskKonsole component = gameObject.GetComponent<taskKonsole>();
			if (component)
			{
				GameObject gameObject2 = GameObject.Find("PLATFORM_" + component.konsoleID.ToString());
				if (gameObject2)
				{
					platformScript component2 = gameObject2.GetComponent<platformScript>();
					if (component2)
					{
						this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[325]);
						this.guiMain_.uiObjects[325].GetComponent<Menu_Dev_KonsoleEntwicklungsbericht>().Init(component2, this.rS_);
					}
				}
			}
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x000F6428 File Offset: 0x000F4628
	public void BUTTON_ContractWork_AutoEnd()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.taskGameObject)
		{
			bool automatic = this.rS_.taskGameObject.GetComponent<taskContractWork>().automatic;
			this.rS_.taskGameObject.GetComponent<taskContractWork>().automatic = !automatic;
		}
		this.CloseAllMenus();
		this.mS_.PauseGame(false);
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x000F6498 File Offset: 0x000F4698
	public void BUTTON_Aufgabe_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[94]);
		this.guiMain_.uiObjects[94].GetComponent<Menu_W_Aufgabe_Abbrechen>().Init(this.rS_);
	}

	// Token: 0x04001C34 RID: 7220
	public roomScript rS_;

	// Token: 0x04001C35 RID: 7221
	public Sprite[] roomIcons;

	// Token: 0x04001C36 RID: 7222
	public GameObject[] uiObjects;

	// Token: 0x04001C37 RID: 7223
	public GameObject[] uiOptions;

	// Token: 0x04001C38 RID: 7224
	public GameObject[] uiWindows;

	// Token: 0x04001C39 RID: 7225
	public Sprite[] uiSprites;

	// Token: 0x04001C3A RID: 7226
	private GameObject main_;

	// Token: 0x04001C3B RID: 7227
	private mainScript mS_;

	// Token: 0x04001C3C RID: 7228
	private roomDataScript rdS_;

	// Token: 0x04001C3D RID: 7229
	private GUI_Main guiMain_;

	// Token: 0x04001C3E RID: 7230
	private sfxScript sfx_;

	// Token: 0x04001C3F RID: 7231
	private buildRoomScript brS_;

	// Token: 0x04001C40 RID: 7232
	private unlockScript unlock_;

	// Token: 0x04001C41 RID: 7233
	private textScript tS_;

	// Token: 0x04001C42 RID: 7234
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04001C43 RID: 7235
	private RectTransform myRectTransform;

	// Token: 0x04001C44 RID: 7236
	private bool mouseOver;

	// Token: 0x04001C45 RID: 7237
	private bool invisible;

	// Token: 0x04001C46 RID: 7238
	private float invisibleTimer;
}
