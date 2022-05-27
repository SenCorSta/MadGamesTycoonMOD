using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

// Token: 0x0200033E RID: 830
public class roomScript : MonoBehaviour
{
	// Token: 0x06001E03 RID: 7683 RVA: 0x0012B300 File Offset: 0x00129500
	private void Start()
	{
		this.FindScripts();
		this.InitUI();
		this.mS_.findRooms = true;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x0012B31A File Offset: 0x0012951A
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findRooms = true;
		}
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x0012B338 File Offset: 0x00129538
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.camera_)
		{
			this.camera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = this.camera_.GetComponent<mainCameraScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.brS_)
		{
			this.brS_ = this.main_.GetComponent<buildRoomScript>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x0012B54A File Offset: 0x0012974A
	private void Update()
	{
		this.FindTasks();
		this.UpdateListInventar();
		this.UpdateLagerraumGFX();
		this.isCrunchTime = this.IsCrunchtime();
		this.UpdateUI();
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x0012B570 File Offset: 0x00129770
	private void InitUI()
	{
		if (!this.myUI)
		{
			this.myUI = UnityEngine.Object.Instantiate<GameObject>(this.uiObjects[0], new Vector3(99999f, 99999f, 0f), Quaternion.identity);
			this.myUI.transform.SetParent(this.mS_.guiPops.transform);
			this.rbS_ = this.myUI.GetComponent<roomButtonScript>();
			this.rbS_.rS_ = this;
			this.myGUIrectTransform = this.myUI.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x0012B604 File Offset: 0x00129804
	private void FindTasks()
	{
		if (this.taskID == -1)
		{
			this.taskGameObject = null;
		}
		if (this.taskGameObject)
		{
			return;
		}
		if (this.taskID != -1)
		{
			GameObject exists = GameObject.Find("Task_" + this.taskID.ToString());
			if (exists)
			{
				this.taskGameObject = exists;
				return;
			}
			this.taskID = -1;
			this.taskGameObject = null;
		}
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x0012B674 File Offset: 0x00129874
	private void UpdateUI()
	{
		if (this.myUI)
		{
			Vector3 vector = this.uiPos;
			vector.y += 2f;
			if (this.guiMain_.disableRoomGUI || this.guiMain_.uiObjects[4].GetComponent<Toggle>().isOn)
			{
				if (this.myUI.activeSelf)
				{
					this.myUI.SetActive(false);
				}
				if (this.myUI_UnterstuetzenLine && this.myUI_UnterstuetzenLine.activeSelf)
				{
					this.myUI_UnterstuetzenLine.SetActive(false);
				}
				if (this.myUI_Line && this.myUI_Line.activeSelf)
				{
					this.myUI_Line.SetActive(false);
				}
				return;
			}
			if (!this.myUI.activeSelf)
			{
				this.myUI.SetActive(true);
			}
			if (this.myUI_UnterstuetzenLine && !this.myUI_UnterstuetzenLine.activeSelf)
			{
				this.myUI_UnterstuetzenLine.SetActive(true);
			}
			if (this.myUI_Line && !this.myUI_Line.activeSelf)
			{
				this.myUI_Line.SetActive(true);
			}
			Vector2 vector2 = this.camera_.WorldToScreenPoint(vector);
			Vector2 vector3 = vector2;
			this.ShouldDrawLine();
			if (vector3.x < -200f || vector3.x >= (float)(Screen.width + 200) || vector3.y < -200f || vector3.y >= (float)(Screen.height + 200))
			{
				this.UpdateWindowForschung(false);
				this.UpdateWindowEngine(false);
				this.UpdateWindowUpdate(false);
				this.UpdateWindowGame(false);
				this.UpdateWindowUnterstuetzen(false);
				this.UpdateWindowMarketing(false);
				this.UpdateWindowMarketingSpezial(false);
				this.UpdateWindowFankampagne(false);
				this.UpdateWindowTraining(false);
				this.UpdateWindowContractWork(false);
				this.UpdateWindowContractWorkWait(false);
				this.UpdateWindowAnrufe(false);
				this.UpdateWindowBugfixing(false);
				this.UpdateWindowGameplayVerbessern(false);
				this.UpdateWindowGrafikVerbessern(false);
				this.UpdateWindowSoundVerbessern(false);
				this.UpdateWindowAnimationVerbessern(false);
				this.UpdateWindowSpielbericht(false);
				this.UpdateWindowProduction(false);
				this.UpdateWindowMarktforschung(false);
				this.UpdateWindowPolishing(false);
				this.UpdateWindowF2PUpdate(false);
				this.UpdateWindowArcadeProduction(false);
				this.UpdateWindowLagerhaus(false);
				this.UpdateWindowServerraum(false);
				this.UpdateWindowKonsole(false);
				this.UpdateWindowWait(false);
				this.myGUIrectTransform.anchoredPosition = this.guiMain_.GetAnchoredPosition(this.invisibleGUI);
				if (this.rbS_.CloseAllMenus())
				{
					this.mS_.PauseGame(false);
					return;
				}
			}
			else
			{
				vector2 = new Vector2(vector2.x, vector2.y - (float)Screen.height - 35f);
				this.myGUIrectTransform.anchoredPosition = this.guiMain_.GetAnchoredPosition(vector2);
				vector = this.uiPos;
				this.DrawRoomLine(vector, new Vector3(vector.x, vector.y + 2f, vector.z));
				if (this.taskGameObject)
				{
					if (this.typ == 2)
					{
						if (this.GetTaskForschung())
						{
							this.UpdateWindowForschung(true);
						}
						else
						{
							this.UpdateWindowForschung(false);
						}
					}
					if (this.typ == 1)
					{
						if (this.GetTaskEngine())
						{
							this.UpdateWindowEngine(true);
						}
						else
						{
							this.UpdateWindowEngine(false);
						}
						if (this.GetTaskUpdate())
						{
							this.UpdateWindowUpdate(true);
						}
						else
						{
							this.UpdateWindowUpdate(false);
						}
						if (this.GetTaskGame())
						{
							this.UpdateWindowGame(true);
						}
						else
						{
							this.UpdateWindowGame(false);
						}
						if (this.GetTaskF2PUpdate())
						{
							this.UpdateWindowF2PUpdate(true);
						}
						else
						{
							this.UpdateWindowF2PUpdate(false);
						}
					}
					if (this.typ == 6)
					{
						if (this.GetTaskMarketing())
						{
							this.UpdateWindowMarketing(true);
						}
						else
						{
							this.UpdateWindowMarketing(false);
						}
						if (this.GetTaskMarketingSpezial())
						{
							this.UpdateWindowMarketingSpezial(true);
						}
						else
						{
							this.UpdateWindowMarketingSpezial(false);
						}
						if (this.GetTaskMitarbeitersuche())
						{
							this.UpdateWindowMitarbeitersuche(true);
						}
						else
						{
							this.UpdateWindowMitarbeitersuche(false);
						}
					}
					if (this.typ == 13)
					{
						if (this.GetTaskTraining())
						{
							this.UpdateWindowTraining(true);
						}
						else
						{
							this.UpdateWindowTraining(false);
						}
					}
					if (this.typ == 3)
					{
						if (this.GetTaskSpielbericht())
						{
							this.UpdateWindowSpielbericht(true);
						}
						else
						{
							this.UpdateWindowSpielbericht(false);
						}
						if (this.GetTaskGameplayVerbessern())
						{
							this.UpdateWindowGameplayVerbessern(true);
						}
						else
						{
							this.UpdateWindowGameplayVerbessern(false);
						}
						if (this.GetTaskBugfixing())
						{
							this.UpdateWindowBugfixing(true);
						}
						else
						{
							this.UpdateWindowBugfixing(false);
						}
					}
					if (this.typ == 4)
					{
						if (this.GetTaskGrafikVerbessern())
						{
							this.UpdateWindowGrafikVerbessern(true);
						}
						else
						{
							this.UpdateWindowGrafikVerbessern(false);
						}
					}
					if (this.typ == 5)
					{
						if (this.GetTaskSoundVerbessern())
						{
							this.UpdateWindowSoundVerbessern(true);
						}
						else
						{
							this.UpdateWindowSoundVerbessern(false);
						}
					}
					if (this.typ == 10)
					{
						if (this.GetTaskAnimationVerbessern())
						{
							this.UpdateWindowAnimationVerbessern(true);
						}
						else
						{
							this.UpdateWindowAnimationVerbessern(false);
						}
					}
					if (this.typ == 14)
					{
						if (this.GetTaskProduction())
						{
							this.UpdateWindowProduction(true);
						}
						else
						{
							this.UpdateWindowProduction(false);
						}
					}
					if (this.typ == 17)
					{
						if (this.GetTaskArcadeProduction())
						{
							this.UpdateWindowArcadeProduction(true);
						}
						else
						{
							this.UpdateWindowArcadeProduction(false);
						}
					}
					if (this.typ == 8)
					{
						if (this.GetTaskKonsole())
						{
							this.UpdateWindowKonsole(true);
						}
						else
						{
							this.UpdateWindowKonsole(false);
						}
					}
					if (this.typ == 7)
					{
						if (this.GetTaskFankampagne())
						{
							this.UpdateWindowFankampagne(true);
						}
						else
						{
							this.UpdateWindowFankampagne(false);
						}
						if (this.GetTaskSupport())
						{
							this.UpdateWindowAnrufe(true);
						}
						else
						{
							this.UpdateWindowAnrufe(false);
						}
						if (this.GetTaskFanshop())
						{
							this.UpdateWindowFanshop(true);
						}
						else
						{
							this.UpdateWindowFanshop(false);
						}
					}
					if (this.GetTaskContractWork())
					{
						this.UpdateWindowContractWork(true);
					}
					else
					{
						this.UpdateWindowContractWork(false);
					}
					if (this.GetTaskContractWait())
					{
						this.UpdateWindowContractWorkWait(true);
					}
					else
					{
						this.UpdateWindowContractWorkWait(false);
					}
					if (this.GetTaskMarktforschung())
					{
						this.UpdateWindowMarktforschung(true);
					}
					else
					{
						this.UpdateWindowMarktforschung(false);
					}
					if (this.GetTaskPolishing())
					{
						this.UpdateWindowPolishing(true);
					}
					else
					{
						this.UpdateWindowPolishing(false);
					}
					if (this.GetTaskUnterstuetzen())
					{
						this.UpdateWindowUnterstuetzen(true);
					}
					else
					{
						this.UpdateWindowUnterstuetzen(false);
					}
					if (this.GetTaskWait())
					{
						this.UpdateWindowWait(true);
						return;
					}
					this.UpdateWindowWait(false);
					return;
				}
				else
				{
					this.UpdateWindowForschung(false);
					this.UpdateWindowEngine(false);
					this.UpdateWindowUpdate(false);
					this.UpdateWindowGame(false);
					this.UpdateWindowUnterstuetzen(false);
					this.UpdateWindowMarketing(false);
					this.UpdateWindowMarketingSpezial(false);
					this.UpdateWindowFankampagne(false);
					this.UpdateWindowTraining(false);
					this.UpdateWindowContractWork(false);
					this.UpdateWindowContractWorkWait(false);
					this.UpdateWindowAnrufe(false);
					this.UpdateWindowFanshop(false);
					this.UpdateWindowBugfixing(false);
					this.UpdateWindowGameplayVerbessern(false);
					this.UpdateWindowGrafikVerbessern(false);
					this.UpdateWindowSoundVerbessern(false);
					this.UpdateWindowAnimationVerbessern(false);
					this.UpdateWindowSpielbericht(false);
					this.UpdateWindowProduction(false);
					this.UpdateWindowMarktforschung(false);
					this.UpdateWindowPolishing(false);
					this.UpdateWindowF2PUpdate(false);
					this.UpdateWindowArcadeProduction(false);
					this.UpdateWindowKonsole(false);
					this.UpdateWindowMitarbeitersuche(false);
					this.UpdateWindowWait(false);
					if (this.typ == 9)
					{
						this.UpdateWindowLagerhaus(true);
					}
					else
					{
						this.UpdateWindowLagerhaus(false);
					}
					if (this.typ == 15)
					{
						this.UpdateWindowServerraum(true);
						return;
					}
					this.UpdateWindowServerraum(false);
				}
			}
		}
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x0012BDD0 File Offset: 0x00129FD0
	private void DrawRoomLine(Vector3 pStart, Vector3 pEnd)
	{
		if (!this.guiMain_.initVectrocity)
		{
			return;
		}
		if (!this.initRoomLine)
		{
			VectorManager.useDraw3D = true;
			this.initRoomLine = true;
			this.roomLine3D = new VectorLine("RoomLine3D_Room" + this.myID.ToString(), new List<Vector3>(2), 12f, LineType.Continuous, Joins.Weld);
			this.roomLine3D.endCap = "RoomLine";
			GameObject gameObject = this.roomLine3D.rectTransform.gameObject;
			this.myUI_Line = gameObject;
			this.roomLine3D.color = Color.white;
			this.roomLine3D.points3[0] = pEnd;
			this.roomLine3D.points3[1] = pStart;
		}
		if (this.camera_.transform.position != this.ROOMLINE_cameraPos || this.camera_.transform.rotation != this.ROOMLINE_cameraRot || this.roomLine3D.points3[0] != pEnd || this.roomLine3D.points3[1] != pStart)
		{
			base.StartCoroutine(this.SetLineShaders());
			this.roomLine3D.points3[0] = pEnd;
			this.roomLine3D.points3[1] = pStart;
			this.ROOMLINE_cameraPos = this.camera_.transform.position;
			this.ROOMLINE_cameraRot = this.camera_.transform.rotation;
			this.roomLine3D.Draw3D();
		}
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x0012BF65 File Offset: 0x0012A165
	public bool IsCrunchtimeRead()
	{
		return this.isCrunchTime;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x0012BF70 File Offset: 0x0012A170
	private bool IsCrunchtime()
	{
		if (!this.taskGameObject)
		{
			return false;
		}
		if (this.typ == 11)
		{
			return false;
		}
		if (this.typ == 9)
		{
			return false;
		}
		if (this.typ == 0)
		{
			return false;
		}
		if (this.typ == 15)
		{
			return false;
		}
		if (this.typ == 12)
		{
			return false;
		}
		if (this.typ == 14)
		{
			return false;
		}
		float num = 0f;
		if (this.typ == 2 && num == 0f && this.GetTaskForschung())
		{
			num = this.GetTaskForschung().GetProzent();
		}
		if (this.typ == 1)
		{
			if (num == 0f && this.GetTaskEngine())
			{
				num = this.GetTaskEngine().GetProzent();
			}
			if (num == 0f && this.GetTaskUpdate())
			{
				num = this.GetTaskUpdate().GetProzent();
			}
			if (num == 0f && this.GetTaskGame())
			{
				num = this.GetTaskGame().GetProzent();
			}
			if (num == 0f && this.GetTaskF2PUpdate())
			{
				num = this.GetTaskF2PUpdate().GetProzent();
			}
		}
		if (this.typ == 6)
		{
			if (num == 0f && this.GetTaskMarketing())
			{
				num = this.GetTaskMarketing().GetProzent();
			}
			if (num == 0f && this.GetTaskMarketingSpezial())
			{
				num = this.GetTaskMarketingSpezial().GetProzent();
			}
			if (num == 0f && this.GetTaskMitarbeitersuche())
			{
				num = this.GetTaskMitarbeitersuche().GetProzent();
			}
		}
		if (this.typ == 13 && num == 0f && this.GetTaskTraining())
		{
			num = this.GetTaskTraining().GetProzent();
		}
		if (num == 0f && this.GetTaskContractWork())
		{
			num = this.GetTaskContractWork().GetProzent();
		}
		if (num == 0f)
		{
			if (this.GetTaskContractWait())
			{
				return false;
			}
			if (this.GetTaskWait())
			{
				return false;
			}
		}
		if (this.typ == 7)
		{
			if (num == 0f && this.GetTaskFankampagne())
			{
				num = this.GetTaskFankampagne().GetProzent();
			}
			if (num == 0f && this.GetTaskSupport())
			{
				num = this.GetTaskSupport().GetProzent();
				if (num > 99.9f)
				{
				}
				return false;
			}
			if (num == 0f && this.GetTaskFanshop())
			{
				return false;
			}
		}
		if (this.typ == 3)
		{
			if (num == 0f && this.GetTaskBugfixing())
			{
				num = this.GetTaskBugfixing().GetProzent();
			}
			if (num == 0f && this.GetTaskGameplayVerbessern())
			{
				num = this.GetTaskGameplayVerbessern().GetProzent();
			}
			if (num == 0f && this.GetTaskSpielbericht())
			{
				num = this.GetTaskSpielbericht().GetProzent();
			}
		}
		if (this.typ == 4 && num == 0f && this.GetTaskGrafikVerbessern())
		{
			num = this.GetTaskGrafikVerbessern().GetProzent();
		}
		if (this.typ == 5 && num == 0f && this.GetTaskSoundVerbessern())
		{
			num = this.GetTaskSoundVerbessern().GetProzent();
		}
		if (this.typ == 10 && num == 0f && this.GetTaskAnimationVerbessern())
		{
			num = this.GetTaskAnimationVerbessern().GetProzent();
		}
		if (this.typ == 17 && num == 0f && this.GetTaskArcadeProduction())
		{
			num = this.GetTaskArcadeProduction().GetProzent();
		}
		if (this.typ == 8 && num == 0f && this.GetTaskKonsole())
		{
			num = this.GetTaskKonsole().GetProzent();
		}
		if (num <= 0f && this.GetTaskUnterstuetzen() && this.GetTaskUnterstuetzen().roomID != this.myID)
		{
			return this.GetTaskUnterstuetzen().IsCrunchtime();
		}
		return num < 100f && num > (float)this.mS_.personal_crunch;
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x0012C374 File Offset: 0x0012A574
	private void ShouldDrawLine()
	{
		if (this.taskID != -1 && this.settings_.roomConnections)
		{
			if (this.taskGameObject)
			{
				taskPolishing taskPolishing = this.GetTaskPolishing();
				if (taskPolishing)
				{
					if (taskPolishing.rS_)
					{
						this.DrawLine(this.guiMain_.colors[22], taskPolishing.rS_.uiPos);
					}
					return;
				}
				taskUnterstuetzen taskUnterstuetzen = this.GetTaskUnterstuetzen();
				if (taskUnterstuetzen)
				{
					if (taskUnterstuetzen.rS_)
					{
						this.DrawLine(new Color(1f, 1f, 1f, 0.5f), taskUnterstuetzen.rS_.uiPos);
					}
					return;
				}
				if (this.typ == 3)
				{
					taskBugfixing taskBugfixing = this.GetTaskBugfixing();
					if (taskBugfixing)
					{
						if (taskBugfixing.rS_)
						{
							this.DrawLine(new Color(0f, 0f, 1f, 0.5f), taskBugfixing.rS_.uiPos);
						}
						return;
					}
					taskGameplayVerbessern taskGameplayVerbessern = this.GetTaskGameplayVerbessern();
					if (taskGameplayVerbessern)
					{
						if (taskGameplayVerbessern.rS_)
						{
							this.DrawLine(new Color(0f, 0f, 1f, 0.5f), taskGameplayVerbessern.rS_.uiPos);
						}
						return;
					}
				}
				if (this.typ == 4)
				{
					taskGrafikVerbessern taskGrafikVerbessern = this.GetTaskGrafikVerbessern();
					if (taskGrafikVerbessern)
					{
						if (taskGrafikVerbessern.rS_)
						{
							this.DrawLine(this.guiMain_.colors[9], taskGrafikVerbessern.rS_.uiPos);
						}
						return;
					}
				}
				if (this.typ == 5)
				{
					taskSoundVerbessern taskSoundVerbessern = this.GetTaskSoundVerbessern();
					if (taskSoundVerbessern)
					{
						if (taskSoundVerbessern.rS_)
						{
							this.DrawLine(this.guiMain_.colors[10], taskSoundVerbessern.rS_.uiPos);
						}
						return;
					}
				}
				if (this.typ == 10)
				{
					taskAnimationVerbessern taskAnimationVerbessern = this.GetTaskAnimationVerbessern();
					if (taskAnimationVerbessern)
					{
						if (taskAnimationVerbessern.rS_)
						{
							this.DrawLine(this.guiMain_.colors[11], taskAnimationVerbessern.rS_.uiPos);
						}
						return;
					}
				}
				if (this.myUI_UnterstuetzenLine)
				{
					this.initLine = false;
					UnityEngine.Object.Destroy(this.myUI_UnterstuetzenLine);
					return;
				}
			}
		}
		else if (this.myUI_UnterstuetzenLine)
		{
			this.initLine = false;
			UnityEngine.Object.Destroy(this.myUI_UnterstuetzenLine);
		}
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x0012C5F8 File Offset: 0x0012A7F8
	private void DrawLine(Color color_, Vector3 uiPos_)
	{
		if (this.guiMain_.uiObjects[150].activeSelf || this.guiMain_.uiObjects[154].activeSelf)
		{
			return;
		}
		if (!this.initLine)
		{
			VectorManager.useDraw3D = true;
			this.initLine = true;
			this.drawLine3D = new VectorLine("Line3D_Room" + this.myID.ToString(), new List<Vector3>(2), 20f, LineType.Continuous, Joins.Weld);
			this.drawLine3D.endCap = "Arrows";
			GameObject gameObject = this.drawLine3D.rectTransform.gameObject;
			this.myUI_UnterstuetzenLine = gameObject;
			this.mS_.gameObject.transform.position = this.uiPos;
			this.mS_.gameObject.transform.LookAt(uiPos_);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.4f);
			Vector3 position = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = uiPos_;
			this.mS_.gameObject.transform.LookAt(this.uiPos);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.4f);
			Vector3 position2 = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			this.drawLine3D.color = color_;
			this.drawLine3D.points3[0] = position2;
			this.drawLine3D.points3[1] = position;
			this.cameraPos = new Vector3(-1f, -1f, -1f);
		}
		this.DrawLine_timer += Time.deltaTime;
		if (this.DrawLine_timer > 0.3f)
		{
			this.DrawLine_timer = 0f;
			this.mS_.gameObject.transform.position = this.uiPos;
			this.mS_.gameObject.transform.LookAt(uiPos_);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.4f);
			Vector3 position3 = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = uiPos_;
			this.mS_.gameObject.transform.LookAt(this.uiPos);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.4f);
			Vector3 position4 = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			if (this.drawLine3D.points3[0] != position4 || this.drawLine3D.points3[1] != position3)
			{
				this.drawLine3D.points3[0] = position4;
				this.drawLine3D.points3[1] = position3;
				this.cameraPos = new Vector3(-1f, -1f, -1f);
			}
		}
		if (this.camera_.transform.position != this.cameraPos || this.camera_.transform.rotation != this.cameraRot)
		{
			base.StartCoroutine(this.SetLineShaders());
			this.cameraPos = this.camera_.transform.position;
			this.cameraRot = this.camera_.transform.rotation;
			this.drawLine3D.Draw3D();
		}
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x0012CA0E File Offset: 0x0012AC0E
	private IEnumerator SetLineShaders()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.myUI_Line)
		{
			MeshRenderer component = this.myUI_Line.GetComponent<MeshRenderer>();
			if (component)
			{
				component.material.shader = this.mS_.shaders[0];
			}
		}
		if (this.myUI_UnterstuetzenLine)
		{
			MeshRenderer component2 = this.myUI_UnterstuetzenLine.GetComponent<MeshRenderer>();
			if (component2)
			{
				component2.material.shader = this.mS_.shaders[0];
			}
		}
		yield break;
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x0012CA20 File Offset: 0x0012AC20
	public Vector2 Get2DPos()
	{
		Vector3 position = this.uiPos;
		return this.camera_.WorldToScreenPoint(position);
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x0012CA48 File Offset: 0x0012AC48
	private void UpdateWindowForschung(bool show)
	{
		if (!this.rbS_.uiWindows[0])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[0].activeSelf)
			{
				this.rbS_.uiWindows[0].SetActive(false);
			}
			return;
		}
		taskForschung taskForschung = this.GetTaskForschung();
		if (taskForschung.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[0].activeSelf)
		{
			this.rbS_.uiWindows[0].SetActive(true);
		}
		if (this.taskGameObject)
		{
			switch (taskForschung.typ)
			{
			case 0:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.genres_.GetName(taskForschung.slot), this.genres_.GetProzent(taskForschung.slot), this.genres_.GetPic(taskForschung.slot), taskForschung);
				return;
			case 1:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.tS_.GetThemes(taskForschung.slot), this.themes_.GetProzent(taskForschung.slot), this.themes_.icon, taskForschung);
				return;
			case 2:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.eF_.GetName(taskForschung.slot), this.eF_.GetProzent(taskForschung.slot), this.eF_.GetTypPic(taskForschung.slot), taskForschung);
				return;
			case 3:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.gF_.GetName(taskForschung.slot), this.gF_.GetProzent(taskForschung.slot), this.gF_.GetTypSprite(taskForschung.slot), taskForschung);
				return;
			case 4:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.hardware_.GetName(taskForschung.slot), this.hardware_.GetProzent(taskForschung.slot), this.hardware_.GetTypPic(taskForschung.slot), taskForschung);
				return;
			case 5:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.fS_.GetName(taskForschung.slot), this.fS_.GetProzent(taskForschung.slot), this.fS_.RES_SPRITE[taskForschung.slot], taskForschung);
				return;
			case 6:
				this.rbS_.uiWindows[0].GetComponent<roomWindow>().Window_Forschung(this.hardwareFeatures_.GetName(taskForschung.slot), this.hardwareFeatures_.GetProzent(taskForschung.slot), this.hardwareFeatures_.GetSprite(taskForschung.slot), taskForschung);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x0012CD24 File Offset: 0x0012AF24
	private void UpdateWindowEngine(bool show)
	{
		if (!this.rbS_.uiWindows[1])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[1].activeSelf)
			{
				this.rbS_.uiWindows[1].SetActive(false);
			}
			return;
		}
		taskEngine taskEngine = this.GetTaskEngine();
		if (taskEngine.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[1].activeSelf)
		{
			this.rbS_.uiWindows[1].SetActive(true);
		}
		if (this.taskGameObject && taskEngine.eS_)
		{
			if (!taskEngine.eS_.updating)
			{
				this.rbS_.uiWindows[1].GetComponent<roomWindow>().Window_DevEngine(taskEngine.eS_.GetName(), taskEngine.eS_.GetProzent(), this.guiMain_.uiSprites[4]);
				return;
			}
			this.rbS_.uiWindows[1].GetComponent<roomWindow>().Window_DevEngine(taskEngine.eS_.GetName(), taskEngine.eS_.GetProzent(), this.guiMain_.uiSprites[5]);
		}
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x0012CE58 File Offset: 0x0012B058
	private void UpdateWindowGame(bool show)
	{
		if (!this.rbS_.uiWindows[2])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[2].activeSelf)
			{
				this.rbS_.uiWindows[2].SetActive(false);
			}
			return;
		}
		taskGame taskGame = this.GetTaskGame();
		if (taskGame.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[2].activeSelf)
		{
			this.rbS_.uiWindows[2].SetActive(true);
		}
		if (this.taskGameObject && taskGame.gS_)
		{
			this.rbS_.uiWindows[2].GetComponent<roomWindow>().Window_DevGame(taskGame.gS_, taskGame);
		}
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x0012CF24 File Offset: 0x0012B124
	private void UpdateWindowUnterstuetzen(bool show)
	{
		if (!this.rbS_.uiWindows[3])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[3].activeSelf)
			{
				this.rbS_.uiWindows[3].SetActive(false);
			}
			return;
		}
		taskUnterstuetzen taskUnterstuetzen = this.GetTaskUnterstuetzen();
		if (taskUnterstuetzen.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[3].activeSelf)
		{
			this.rbS_.uiWindows[3].SetActive(true);
		}
		if (this.taskGameObject && taskUnterstuetzen.rS_)
		{
			float prozent = -1f;
			if (taskUnterstuetzen.rS_.taskGameObject)
			{
				bool flag = false;
				if (!flag && taskUnterstuetzen.rS_.GetTaskGame())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskGame().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskEngine())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskEngine().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskForschung())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskForschung().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskPolishing())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskPolishing().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskMarketing())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskMarketing().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskMarketingSpezial())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskMarketingSpezial().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskContractWork())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskContractWork().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskContractWait())
				{
					prozent = -1f;
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskWait())
				{
					prozent = -1f;
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskUpdate())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskUpdate().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskF2PUpdate())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskF2PUpdate().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskFankampagne())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskFankampagne().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskMitarbeitersuche())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskMitarbeitersuche().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskSupport())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskSupport().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskFanshop())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskFanshop().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskBugfixing())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskBugfixing().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskGameplayVerbessern())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskGameplayVerbessern().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskGrafikVerbessern())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskGrafikVerbessern().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskSoundVerbessern())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskSoundVerbessern().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskAnimationVerbessern())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskAnimationVerbessern().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskSpielbericht())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskSpielbericht().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskProduction())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskProduction().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskArcadeProduction())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskArcadeProduction().GetProzent();
					flag = true;
				}
				if (!flag && taskUnterstuetzen.rS_.GetTaskKonsole())
				{
					prozent = taskUnterstuetzen.rS_.GetTaskKonsole().GetProzent();
				}
			}
			if (taskUnterstuetzen.rS_.myName.Length > 0)
			{
				this.rbS_.uiWindows[3].GetComponent<roomWindow>().Window_Unterstuetzen(taskUnterstuetzen.rS_.myName, prozent);
				return;
			}
			this.rbS_.uiWindows[3].GetComponent<roomWindow>().Window_Unterstuetzen(this.rdS_.GetName(taskUnterstuetzen.rS_.typ), prozent);
		}
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x0012D404 File Offset: 0x0012B604
	private void UpdateWindowContractWork(bool show)
	{
		if (!this.rbS_.uiWindows[6])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[6].activeSelf)
			{
				this.rbS_.uiWindows[6].SetActive(false);
			}
			return;
		}
		taskContractWork taskContractWork = this.GetTaskContractWork();
		if (taskContractWork.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[6].activeSelf)
		{
			this.rbS_.uiWindows[6].SetActive(true);
		}
		if (this.taskGameObject && taskContractWork.contract_)
		{
			this.rbS_.uiWindows[6].GetComponent<roomWindow>().Window_ContractWork(taskContractWork, this);
		}
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x0012D4C8 File Offset: 0x0012B6C8
	private void UpdateWindowContractWorkWait(bool show)
	{
		if (!this.rbS_.uiWindows[25])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[25].activeSelf)
			{
				this.rbS_.uiWindows[25].SetActive(false);
			}
			return;
		}
		taskContractWait taskContractWait = this.GetTaskContractWait();
		if (taskContractWait.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[25].activeSelf)
		{
			this.rbS_.uiWindows[25].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[25].GetComponent<roomWindow>().Window_ContractWorkWait(taskContractWait);
		}
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x0012D584 File Offset: 0x0012B784
	private void UpdateWindowWait(bool show)
	{
		if (!this.rbS_.uiWindows[26])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[26].activeSelf)
			{
				this.rbS_.uiWindows[26].SetActive(false);
			}
			return;
		}
		taskWait taskWait = this.GetTaskWait();
		if (taskWait.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[26].activeSelf)
		{
			this.rbS_.uiWindows[26].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[26].GetComponent<roomWindow>().Window_Wait(taskWait);
		}
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x0012D640 File Offset: 0x0012B840
	private void UpdateWindowKonsole(bool show)
	{
		if (!this.rbS_.uiWindows[24])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[24].activeSelf)
			{
				this.rbS_.uiWindows[24].SetActive(false);
			}
			return;
		}
		taskKonsole taskKonsole = this.GetTaskKonsole();
		if (taskKonsole.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[24].activeSelf)
		{
			this.rbS_.uiWindows[24].SetActive(true);
		}
		if (this.taskGameObject && taskKonsole.pS_)
		{
			this.rbS_.uiWindows[24].GetComponent<roomWindow>().Window_Konsole(taskKonsole);
		}
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x0012D70C File Offset: 0x0012B90C
	private void UpdateWindowArcadeProduction(bool show)
	{
		if (!this.rbS_.uiWindows[23])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[23].activeSelf)
			{
				this.rbS_.uiWindows[23].SetActive(false);
			}
			return;
		}
		taskArcadeProduction taskArcadeProduction = this.GetTaskArcadeProduction();
		if (taskArcadeProduction.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[23].activeSelf)
		{
			this.rbS_.uiWindows[23].SetActive(true);
		}
		if (this.taskGameObject && taskArcadeProduction.gS_)
		{
			this.rbS_.uiWindows[23].GetComponent<roomWindow>().Window_ArcadeProduction(taskArcadeProduction);
		}
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0012D7D8 File Offset: 0x0012B9D8
	private void UpdateWindowF2PUpdate(bool show)
	{
		if (!this.rbS_.uiWindows[22])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[22].activeSelf)
			{
				this.rbS_.uiWindows[22].SetActive(false);
			}
			return;
		}
		taskF2PUpdate taskF2PUpdate = this.GetTaskF2PUpdate();
		if (taskF2PUpdate.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[22].activeSelf)
		{
			this.rbS_.uiWindows[22].SetActive(true);
		}
		if (this.taskGameObject && taskF2PUpdate.gS_)
		{
			this.rbS_.uiWindows[22].GetComponent<roomWindow>().Window_F2PUpdate(taskF2PUpdate);
		}
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x0012D8A4 File Offset: 0x0012BAA4
	private void UpdateWindowUpdate(bool show)
	{
		if (!this.rbS_.uiWindows[7])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[7].activeSelf)
			{
				this.rbS_.uiWindows[7].SetActive(false);
			}
			return;
		}
		taskUpdate taskUpdate = this.GetTaskUpdate();
		if (taskUpdate.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[7].activeSelf)
		{
			this.rbS_.uiWindows[7].SetActive(true);
		}
		if (this.taskGameObject && taskUpdate.gS_)
		{
			this.rbS_.uiWindows[7].GetComponent<roomWindow>().Window_Update(taskUpdate.gS_.GetNameWithTag(), taskUpdate.GetProzent(), taskUpdate.automatic);
		}
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x0012D980 File Offset: 0x0012BB80
	private void UpdateWindowTraining(bool show)
	{
		if (!this.rbS_.uiWindows[5])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[5].activeSelf)
			{
				this.rbS_.uiWindows[5].SetActive(false);
			}
			return;
		}
		taskTraining taskTraining = this.GetTaskTraining();
		if (taskTraining.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[5].activeSelf)
		{
			this.rbS_.uiWindows[5].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[5].GetComponent<roomWindow>().Window_Training(this.tS_.GetText(taskTraining.slot + 538), taskTraining.GetProzent(), taskTraining.GetPic(), taskTraining.automatic);
		}
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x0012DA60 File Offset: 0x0012BC60
	private void UpdateWindowFankampagne(bool show)
	{
		if (!this.rbS_.uiWindows[8])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[8].activeSelf)
			{
				this.rbS_.uiWindows[8].SetActive(false);
			}
			return;
		}
		taskFankampagne taskFankampagne = this.GetTaskFankampagne();
		if (taskFankampagne.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[8].activeSelf)
		{
			this.rbS_.uiWindows[8].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[8].GetComponent<roomWindow>().Window_Fankampagne(taskFankampagne);
		}
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x0012DB18 File Offset: 0x0012BD18
	private void UpdateWindowMitarbeitersuche(bool show)
	{
		if (!this.rbS_.uiWindows[27])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[27].activeSelf)
			{
				this.rbS_.uiWindows[27].SetActive(false);
			}
			return;
		}
		taskMitarbeitersuche taskMitarbeitersuche = this.GetTaskMitarbeitersuche();
		if (taskMitarbeitersuche.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[27].activeSelf)
		{
			this.rbS_.uiWindows[27].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[27].GetComponent<roomWindow>().Window_Mitarbeitersuche(taskMitarbeitersuche);
		}
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x0012DBD4 File Offset: 0x0012BDD4
	private void UpdateWindowMarktforschung(bool show)
	{
		if (!this.rbS_.uiWindows[18])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[18].activeSelf)
			{
				this.rbS_.uiWindows[18].SetActive(false);
			}
			return;
		}
		taskMarktforschung taskMarktforschung = this.GetTaskMarktforschung();
		if (taskMarktforschung.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[18].activeSelf)
		{
			this.rbS_.uiWindows[18].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[18].GetComponent<roomWindow>().Window_Marktforschung(taskMarktforschung);
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x0012DC90 File Offset: 0x0012BE90
	private void UpdateWindowAnrufe(bool show)
	{
		if (!this.rbS_.uiWindows[9])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[9].activeSelf)
			{
				this.rbS_.uiWindows[9].SetActive(false);
			}
			return;
		}
		taskSupport taskSupport = this.GetTaskSupport();
		if (taskSupport.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[9].activeSelf)
		{
			this.rbS_.uiWindows[9].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[9].GetComponent<roomWindow>().Window_Anrufe(taskSupport);
		}
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0012DD4C File Offset: 0x0012BF4C
	private void UpdateWindowFanshop(bool show)
	{
		if (!this.rbS_.uiWindows[28])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[28].activeSelf)
			{
				this.rbS_.uiWindows[28].SetActive(false);
			}
			return;
		}
		taskFanshop taskFanshop = this.GetTaskFanshop();
		if (taskFanshop.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[28].activeSelf)
		{
			this.rbS_.uiWindows[28].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[28].GetComponent<roomWindow>().Window_Fanshop(taskFanshop);
		}
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x0012DE08 File Offset: 0x0012C008
	private void UpdateWindowBugfixing(bool show)
	{
		if (!this.rbS_.uiWindows[10])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[10].activeSelf)
			{
				this.rbS_.uiWindows[10].SetActive(false);
			}
			return;
		}
		taskBugfixing taskBugfixing = this.GetTaskBugfixing();
		if (taskBugfixing.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[10].activeSelf)
		{
			this.rbS_.uiWindows[10].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[10].GetComponent<roomWindow>().Window_Bugfixing(taskBugfixing);
		}
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x0012DEC4 File Offset: 0x0012C0C4
	private void UpdateWindowPolishing(bool show)
	{
		if (!this.rbS_.uiWindows[20])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[20].activeSelf)
			{
				this.rbS_.uiWindows[20].SetActive(false);
			}
			return;
		}
		taskPolishing taskPolishing = this.GetTaskPolishing();
		if (taskPolishing.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[20].activeSelf)
		{
			this.rbS_.uiWindows[20].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[20].GetComponent<roomWindow>().Window_Polishing(taskPolishing);
		}
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x0012DF80 File Offset: 0x0012C180
	private void UpdateWindowSpielbericht(bool show)
	{
		if (!this.rbS_.uiWindows[15])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[15].activeSelf)
			{
				this.rbS_.uiWindows[15].SetActive(false);
			}
			return;
		}
		taskSpielbericht taskSpielbericht = this.GetTaskSpielbericht();
		if (taskSpielbericht.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[15].activeSelf)
		{
			this.rbS_.uiWindows[15].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[15].GetComponent<roomWindow>().Window_Spielbericht(taskSpielbericht);
		}
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x0012E03C File Offset: 0x0012C23C
	private void UpdateWindowProduction(bool show)
	{
		if (!this.rbS_.uiWindows[16])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[16].activeSelf)
			{
				this.rbS_.uiWindows[16].SetActive(false);
			}
			return;
		}
		taskProduction taskProduction = this.GetTaskProduction();
		if (taskProduction.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[16].activeSelf)
		{
			this.rbS_.uiWindows[16].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[16].GetComponent<roomWindow>().Window_Production(taskProduction);
		}
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x0012E0F8 File Offset: 0x0012C2F8
	private void UpdateWindowLagerhaus(bool show)
	{
		if (!this.rbS_.uiWindows[17])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[17].activeSelf)
			{
				this.rbS_.uiWindows[17].SetActive(false);
			}
			return;
		}
		if (!this.rbS_.uiWindows[17].activeSelf)
		{
			this.rbS_.uiWindows[17].SetActive(true);
		}
		this.rbS_.uiWindows[17].GetComponent<roomWindow>().Window_Lagerhaus(this);
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x0012E18C File Offset: 0x0012C38C
	private void UpdateWindowServerraum(bool show)
	{
		if (!this.rbS_.uiWindows[19])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[19].activeSelf)
			{
				this.rbS_.uiWindows[19].SetActive(false);
			}
			return;
		}
		if (!this.rbS_.uiWindows[19].activeSelf)
		{
			this.rbS_.uiWindows[19].SetActive(true);
		}
		this.rbS_.uiWindows[19].GetComponent<roomWindow>().Window_Serverraum(this);
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x0012E220 File Offset: 0x0012C420
	private void UpdateWindowGameplayVerbessern(bool show)
	{
		if (!this.rbS_.uiWindows[11])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[11].activeSelf)
			{
				this.rbS_.uiWindows[11].SetActive(false);
			}
			return;
		}
		taskGameplayVerbessern taskGameplayVerbessern = this.GetTaskGameplayVerbessern();
		if (taskGameplayVerbessern.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[11].activeSelf)
		{
			this.rbS_.uiWindows[11].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[11].GetComponent<roomWindow>().Window_GameplayVerbessern(taskGameplayVerbessern);
		}
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x0012E2DC File Offset: 0x0012C4DC
	private void UpdateWindowGrafikVerbessern(bool show)
	{
		if (!this.rbS_.uiWindows[12])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[12].activeSelf)
			{
				this.rbS_.uiWindows[12].SetActive(false);
			}
			return;
		}
		taskGrafikVerbessern taskGrafikVerbessern = this.GetTaskGrafikVerbessern();
		if (taskGrafikVerbessern.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[12].activeSelf)
		{
			this.rbS_.uiWindows[12].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[12].GetComponent<roomWindow>().Window_GrafikVerbessern(taskGrafikVerbessern);
		}
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x0012E398 File Offset: 0x0012C598
	private void UpdateWindowSoundVerbessern(bool show)
	{
		if (!this.rbS_.uiWindows[13])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[13].activeSelf)
			{
				this.rbS_.uiWindows[13].SetActive(false);
			}
			return;
		}
		taskSoundVerbessern taskSoundVerbessern = this.GetTaskSoundVerbessern();
		if (taskSoundVerbessern.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[13].activeSelf)
		{
			this.rbS_.uiWindows[13].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[13].GetComponent<roomWindow>().Window_SoundVerbessern(taskSoundVerbessern);
		}
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x0012E454 File Offset: 0x0012C654
	private void UpdateWindowAnimationVerbessern(bool show)
	{
		if (!this.rbS_.uiWindows[14])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[14].activeSelf)
			{
				this.rbS_.uiWindows[14].SetActive(false);
			}
			return;
		}
		taskAnimationVerbessern taskAnimationVerbessern = this.GetTaskAnimationVerbessern();
		if (taskAnimationVerbessern.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[14].activeSelf)
		{
			this.rbS_.uiWindows[14].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[14].GetComponent<roomWindow>().Window_AnimationVerbessern(taskAnimationVerbessern);
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x0012E510 File Offset: 0x0012C710
	private void UpdateWindowMarketingSpezial(bool show)
	{
		if (!this.rbS_.uiWindows[21])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[21].activeSelf)
			{
				this.rbS_.uiWindows[21].SetActive(false);
			}
			return;
		}
		taskMarketingSpezial taskMarketingSpezial = this.GetTaskMarketingSpezial();
		if (taskMarketingSpezial.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[21].activeSelf)
		{
			this.rbS_.uiWindows[21].SetActive(true);
		}
		if (this.taskGameObject)
		{
			this.rbS_.uiWindows[21].GetComponent<roomWindow>().Window_MarketingSpezial(taskMarketingSpezial);
		}
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x0012E5CC File Offset: 0x0012C7CC
	private void UpdateWindowMarketing(bool show)
	{
		if (!this.rbS_.uiWindows[4])
		{
			return;
		}
		if (!show)
		{
			if (this.rbS_.uiWindows[4].activeSelf)
			{
				this.rbS_.uiWindows[4].SetActive(false);
			}
			return;
		}
		taskMarketing taskMarketing = this.GetTaskMarketing();
		if (taskMarketing.myID != this.taskID)
		{
			this.taskGameObject = null;
			return;
		}
		if (!this.rbS_.uiWindows[4].activeSelf)
		{
			this.rbS_.uiWindows[4].SetActive(true);
		}
		if (this.taskGameObject)
		{
			int num = taskMarketing.typ;
			if (num != 0)
			{
				if (num != 1)
				{
					return;
				}
				if (taskMarketing.pS_)
				{
					this.rbS_.uiWindows[4].GetComponent<roomWindow>().Window_Marketing(taskMarketing.pS_.GetName(), taskMarketing.GetProzent(), taskMarketing.GetPic(), taskMarketing);
				}
			}
			else if (taskMarketing.gS_)
			{
				this.rbS_.uiWindows[4].GetComponent<roomWindow>().Window_Marketing(taskMarketing.gS_.GetNameWithTag(), taskMarketing.GetProzent(), taskMarketing.GetPic(), taskMarketing);
				return;
			}
		}
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0012E6F8 File Offset: 0x0012C8F8
	public void SetOutlineLayer()
	{
		if (!this.outline)
		{
			this.outline = true;
			this.mCamS_.SetOutlineColor(2, 0.3f, 4);
			for (int i = 0; i < this.listGameObjects.Count; i++)
			{
				if (this.listGameObjects[i])
				{
					this.SetLayer(11, this.listGameObjects[i].transform.GetChild(0));
				}
			}
		}
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x0012E770 File Offset: 0x0012C970
	public void SetListGameObjectsLayer(int l)
	{
		for (int i = 0; i < this.listGameObjects.Count; i++)
		{
			if (this.listGameObjects[i])
			{
				this.SetLayer(l, this.listGameObjects[i].transform.GetChild(0));
			}
		}
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0012E7C4 File Offset: 0x0012C9C4
	public void DisableOutlineLayer()
	{
		if (this.outline)
		{
			this.outline = false;
			for (int i = 0; i < this.listGameObjects.Count; i++)
			{
				if (this.listGameObjects[i])
				{
					this.SetLayer(0, this.listGameObjects[i].transform.GetChild(0));
				}
			}
		}
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0012E828 File Offset: 0x0012CA28
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

	// Token: 0x06001E32 RID: 7730 RVA: 0x0012E8A4 File Offset: 0x0012CAA4
	public void UpdateListInventar()
	{
		this.arbeitsplaetze = 0;
		this.lagerplatz = 0;
		this.serverplatz = 0;
		this.serverOverheat = false;
		int count = this.listInventar.Count;
		for (int i = 0; i < count; i++)
		{
			if (!this.listInventar[i])
			{
				this.listInventar.RemoveAt(i);
				this.UpdateListInventar();
				return;
			}
			if (this.typ != 11 && this.typ != 12 && this.typ != 16 && this.typ != 14 && this.typ != 0)
			{
				if (this.listInventar[i].isArbeitsplatz)
				{
					this.arbeitsplaetze++;
				}
				else if (this.listInventar[i].isLager)
				{
					this.lagerplatz += this.listInventar[i].lagerplatz;
				}
				else if (this.listInventar[i].isServer)
				{
					this.serverplatz += this.listInventar[i].GetServerplatz();
				}
			}
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x0012E9D7 File Offset: 0x0012CBD7
	public bool IsUberberfuell()
	{
		return this.arbeitsplaetze > 0 && this.GetMitarbeiter() > this.AnzahlArbeitsplaetzeBisUberfuellt();
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x0012E9F8 File Offset: 0x0012CBF8
	public int AnzahlArbeitsplaetzeBisUberfuellt()
	{
		float num = 3.3f;
		int num2 = this.typ;
		if (num2 != 5)
		{
			if (num2 != 10)
			{
				if (num2 == 13)
				{
					num = 2.7f;
				}
			}
			else
			{
				num = 10f;
			}
		}
		else
		{
			num = 5f;
		}
		int num3 = Mathf.FloorToInt((float)this.listGameObjects.Count / num);
		num3--;
		if (num3 < 0)
		{
			num3 = 0;
		}
		return num3;
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x0012EA57 File Offset: 0x0012CC57
	public GameObject GetRandomFloor()
	{
		return this.listGameObjects[UnityEngine.Random.Range(0, this.listGameObjects.Count)];
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x0012EA75 File Offset: 0x0012CC75
	public int GetArbeitsplaetze()
	{
		return this.arbeitsplaetze;
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x0012EA7D File Offset: 0x0012CC7D
	public int GetMitarbeiter()
	{
		return this.mitarbeiterZugeteilt;
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x0012EA88 File Offset: 0x0012CC88
	public void Demolish()
	{
		if (this.myUI)
		{
			UnityEngine.Object.Destroy(this.myUI);
		}
		if (this.myUI_Line)
		{
			UnityEngine.Object.Destroy(this.myUI_Line);
		}
		if (this.myUI_UnterstuetzenLine)
		{
			UnityEngine.Object.Destroy(this.myUI_UnterstuetzenLine);
		}
		for (int i = 0; i < this.listInventar.Count; i++)
		{
			if (this.listInventar[i])
			{
				UnityEngine.Object.Destroy(this.listInventar[i].gameObject);
			}
		}
		this.mS_.findRooms = true;
		this.mapS_.RemoveRoom(this.myID, true);
		UnityEngine.Object.Destroy(base.gameObject);
		this.camera_.gameObject.GetComponent<Animation>().Play();
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x0012EB5C File Offset: 0x0012CD5C
	public bool KeineAnrufe()
	{
		return this.mS_.anrufe <= 0 && this.typ == 7 && this.taskID != -1 && (this.taskGameObject && this.GetTaskSupport() && this.mS_.anrufe <= 0);
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x0012EBBC File Offset: 0x0012CDBC
	public bool WERK_GameHasBestellungen()
	{
		if (this.typ != 17)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskArcadeProduction taskArcadeProduction = this.GetTaskArcadeProduction();
			if (taskArcadeProduction && taskArcadeProduction.gS_ && taskArcadeProduction.gS_.vorbestellungen > 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x0012EC1C File Offset: 0x0012CE1C
	public bool GameIsPort()
	{
		if (this.typ != 1 && this.typ != 3 && this.typ != 5 && this.typ != 4 && this.typ != 10)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskGame taskGame = this.GetTaskGame();
			if (taskGame && taskGame.gS_ && taskGame.gS_.portID != -1)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x0012ECA0 File Offset: 0x0012CEA0
	public bool GameIsMMO()
	{
		if (this.typ != 1 && this.typ != 3 && this.typ != 5 && this.typ != 4 && this.typ != 10)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskGame taskGame = this.GetTaskGame();
			if (taskGame && taskGame.gS_ && taskGame.gS_.gameTyp == 1)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x0012ED24 File Offset: 0x0012CF24
	public bool QA_GameHasNoBugs()
	{
		if (this.typ != 3)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskBugfixing taskBugfixing = this.GetTaskBugfixing();
			if (taskBugfixing && taskBugfixing.gS_ && taskBugfixing.gS_.points_bugs <= 0f)
			{
				if (taskBugfixing.gS_.devPoints_Gesamt <= 0f)
				{
					this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishingAutomatic(taskBugfixing.gS_, taskBugfixing.myID);
					UnityEngine.Object.Destroy(taskBugfixing.gameObject);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x0012EDCC File Offset: 0x0012CFCC
	public bool WaitForMinimumHype()
	{
		if (this.typ != 6)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskMarketing taskMarketing = this.GetTaskMarketing();
			if (taskMarketing)
			{
				return taskMarketing.WaitForMinimumHype();
			}
		}
		return false;
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x0012EE14 File Offset: 0x0012D014
	public bool IsDevAddon()
	{
		if (this.typ != 1)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskGame taskGame = this.GetTaskGame();
			if (taskGame && taskGame.gS_ && (taskGame.gS_.typ_addon || taskGame.gS_.typ_addonStandalone || taskGame.gS_.typ_contractGame || taskGame.gS_.typ_mmoaddon))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0012EE98 File Offset: 0x0012D098
	public bool KeineAutomatenBestellungen()
	{
		if (this.typ != 17)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskArcadeProduction taskArcadeProduction = this.GetTaskArcadeProduction();
			if (taskArcadeProduction && taskArcadeProduction.gS_ && taskArcadeProduction.gS_.vorbestellungen <= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x0012EEF6 File Offset: 0x0012D0F6
	public bool IsGameDevComplete()
	{
		if (this.typ != 1)
		{
			return false;
		}
		int num = this.taskID;
		return false;
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0012EF10 File Offset: 0x0012D110
	public bool IsGameDevCompleteOrg()
	{
		if (this.typ != 1)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskGame taskGame = this.GetTaskGame();
			if (taskGame && taskGame.gS_ && taskGame.gS_.devPoints_Gesamt <= 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0012EF74 File Offset: 0x0012D174
	public bool IsKonsoleDevCompleteOrg()
	{
		if (this.typ != 8)
		{
			return false;
		}
		if (this.taskID == -1)
		{
			return false;
		}
		if (this.taskGameObject)
		{
			taskKonsole taskKonsole = this.GetTaskKonsole();
			if (taskKonsole && taskKonsole.pS_ && taskKonsole.pS_.devPoints <= 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x0012EFD5 File Offset: 0x0012D1D5
	public bool IstContractWorkWait()
	{
		return this.taskID != -1 && (this.taskGameObject && this.GetTaskContractWait());
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x0012EFFF File Offset: 0x0012D1FF
	public bool IstTaskWait()
	{
		return this.taskID != -1 && (this.taskGameObject && this.GetTaskWait());
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x0012F029 File Offset: 0x0012D229
	public int GetLagerplatz()
	{
		return this.lagerplatz;
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x0012F031 File Offset: 0x0012D231
	public int GetFreeLagerplatz()
	{
		return this.lagerplatz - this.lagerplatzUsed;
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x0012F040 File Offset: 0x0012D240
	public int GetServerplatz()
	{
		return this.serverplatz;
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x0012F048 File Offset: 0x0012D248
	public int GetFreeServerplatz()
	{
		return this.serverplatz - this.serverplatzUsed;
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x0012F058 File Offset: 0x0012D258
	public int SetAbos(int i)
	{
		if (this.serverDown)
		{
			return i;
		}
		int num = this.serverplatz - this.serverplatzUsed;
		if (num <= 0)
		{
			return i;
		}
		if (num >= i)
		{
			this.serverplatzUsed += i;
			if (this.serverplatzUsed < 0)
			{
				this.serverplatzUsed = 0;
			}
			return 0;
		}
		this.serverplatzUsed = this.serverplatz;
		if (this.serverplatzUsed < 0)
		{
			this.serverplatzUsed = 0;
		}
		return i - num;
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0012F0C8 File Offset: 0x0012D2C8
	private void UpdateLagerraumGFX()
	{
		if (this.typ != 9)
		{
			return;
		}
		this.lagerraumTimer += Time.deltaTime;
		if (this.lagerraumTimer < 1f)
		{
			return;
		}
		this.lagerraumTimer = 0f;
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.listInventar.Count; i++)
		{
			if (this.listInventar[i] && this.listInventar[i].isLager)
			{
				if (!this.listInventar[i].lagerScript_)
				{
					this.listInventar[i].lagerScript_ = this.listInventar[i].gameObject.GetComponent<lagerScript>();
				}
				else
				{
					for (int j = 0; j < this.listInventar[i].lagerScript_.goKartons.Length; j++)
					{
						list.Add(this.listInventar[i].lagerScript_.goKartons[j]);
					}
				}
			}
		}
		float num = (float)this.GetLagerplatz();
		if (num > 0f)
		{
			num *= 0.01f;
			num = (float)this.lagerplatzUsed / num;
		}
		else
		{
			num = 0f;
		}
		float num2 = 100f / (float)list.Count;
		for (int k = 0; k < list.Count; k++)
		{
			if ((float)(k + 1) * num2 <= num)
			{
				if (!list[k].activeSelf)
				{
					list[k].SetActive(true);
				}
			}
			else if (list[k].activeSelf)
			{
				list[k].SetActive(false);
			}
		}
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0012F273 File Offset: 0x0012D473
	public void ServerAbschalten(bool shutdown)
	{
		this.serverDown = shutdown;
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x0012F27C File Offset: 0x0012D47C
	public bool UpdateInventar(bool buy)
	{
		bool result = false;
		if (this.typ != -1)
		{
			int oldTyp = 102;
			int newTyp = 107;
			if (this.mS_.year >= 2005 && this.RemoveOldInventar(oldTyp, newTyp, buy))
			{
				result = true;
			}
		}
		if (this.typ == 1)
		{
			int oldTyp2 = 1;
			int num = 50;
			int num2 = 51;
			int num3 = 52;
			int newTyp2 = 53;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp2, newTyp2, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num, newTyp2, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num2, newTyp2, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num3, newTyp2, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp2, num3, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num, num3, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num2, num3, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp2, num2, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num, num2, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp2, num, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 2)
		{
			int oldTyp3 = 6;
			int num4 = 56;
			int num5 = 66;
			int num6 = 67;
			int newTyp3 = 68;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp3, newTyp3, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num4, newTyp3, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num5, newTyp3, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num6, newTyp3, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp3, num6, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num4, num6, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num5, num6, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp3, num5, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num4, num5, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp3, num4, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 6)
		{
			int oldTyp4 = 48;
			int num7 = 57;
			int num8 = 58;
			int num9 = 59;
			int newTyp4 = 60;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp4, newTyp4, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num7, newTyp4, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num8, newTyp4, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num9, newTyp4, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp4, num9, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num7, num9, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num8, num9, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp4, num8, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num7, num8, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp4, num7, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 15)
		{
			int oldTyp5 = 45;
			int num10 = 125;
			int num11 = 126;
			int num12 = 127;
			int newTyp5 = 128;
			int oldTyp6 = 46;
			int newTyp6 = 154;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp5, newTyp5, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num10, newTyp5, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num11, newTyp5, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num12, newTyp5, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(oldTyp6, newTyp6, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp5, num12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num10, num12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num11, num12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(oldTyp6, newTyp6, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp5, num11, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num10, num11, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp5, num10, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 9)
		{
			int oldTyp7 = 47;
			int num13 = 79;
			int newTyp7 = 80;
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp7, newTyp7, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num13, newTyp7, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp7, num13, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 13)
		{
			int oldTyp8 = 54;
			int num14 = 111;
			int num15 = 112;
			int num16 = 113;
			int newTyp8 = 114;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp8, newTyp8, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num14, newTyp8, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num15, newTyp8, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num16, newTyp8, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp8, num16, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num14, num16, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num15, num16, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp8, num15, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num14, num15, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp8, num14, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 7)
		{
			int oldTyp9 = 61;
			int num17 = 62;
			int num18 = 63;
			int num19 = 64;
			int newTyp9 = 65;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp9, newTyp9, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num17, newTyp9, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num18, newTyp9, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num19, newTyp9, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp9, num19, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num17, num19, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num18, num19, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp9, num18, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num17, num18, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp9, num17, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 3)
		{
			int oldTyp10 = 74;
			int num20 = 88;
			int num21 = 89;
			int num22 = 90;
			int newTyp10 = 91;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp10, newTyp10, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num20, newTyp10, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num21, newTyp10, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num22, newTyp10, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp10, num22, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num20, num22, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num21, num22, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp10, num21, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num20, num21, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp10, num20, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 4)
		{
			int oldTyp11 = 75;
			int num23 = 103;
			int num24 = 104;
			int num25 = 105;
			int newTyp11 = 106;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp11, newTyp11, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num23, newTyp11, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num24, newTyp11, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num25, newTyp11, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp11, num25, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num23, num25, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num24, num25, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp11, num24, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num23, num24, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp11, num23, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 5)
		{
			int oldTyp12 = 76;
			int num26 = 81;
			int num27 = 82;
			int num28 = 119;
			int newTyp12 = 120;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp12, newTyp12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num26, newTyp12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num27, newTyp12, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num28, newTyp12, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp12, num28, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num26, num28, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num27, num28, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp12, num27, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num26, num27, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp12, num26, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 10)
		{
			int oldTyp13 = 77;
			int num29 = 121;
			int newTyp13 = 122;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp13, newTyp13, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num29, newTyp13, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp13, num29, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 17)
		{
			int oldTyp14 = 144;
			int num30 = 145;
			int num31 = 146;
			int num32 = 147;
			int newTyp14 = 148;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp14, newTyp14, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num30, newTyp14, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num31, newTyp14, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num32, newTyp14, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp14, num32, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num30, num32, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num31, num32, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp14, num31, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num30, num31, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp14, num30, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else if (this.typ == 14)
		{
			int oldTyp15 = 36;
			int num33 = 115;
			int num34 = 116;
			int num35 = 117;
			int newTyp15 = 118;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp15, newTyp15, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num33, newTyp15, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num34, newTyp15, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num35, newTyp15, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp15, num35, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num33, num35, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num34, num35, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp15, num34, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num33, num34, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp15, num33, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
		else
		{
			if (this.typ != 8)
			{
				return result;
			}
			int oldTyp16 = 149;
			int num36 = 150;
			int num37 = 151;
			int num38 = 152;
			int newTyp16 = 153;
			if (this.mS_.year >= 2015)
			{
				if (this.RemoveOldInventar(oldTyp16, newTyp16, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num36, newTyp16, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num37, newTyp16, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num38, newTyp16, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 2005)
			{
				if (this.RemoveOldInventar(oldTyp16, num38, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num36, num38, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num37, num38, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1995)
			{
				if (this.RemoveOldInventar(oldTyp16, num37, buy))
				{
					result = true;
				}
				if (this.RemoveOldInventar(num36, num37, buy))
				{
					result = true;
				}
				return result;
			}
			if (this.mS_.year >= 1985)
			{
				if (this.RemoveOldInventar(oldTyp16, num36, buy))
				{
					result = true;
				}
				return result;
			}
			return result;
		}
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x0013003C File Offset: 0x0012E23C
	private bool RemoveOldInventar(int oldTyp, int newTyp, bool buy)
	{
		for (int i = 0; i < this.listInventar.Count; i++)
		{
			if (this.listInventar[i])
			{
				objectScript objectScript = this.listInventar[i];
				if (objectScript && oldTyp == objectScript.typ)
				{
					if (!buy)
					{
						return true;
					}
					objectScript component = UnityEngine.Object.Instantiate<GameObject>(this.mapS_.prefabsInventar[newTyp]).GetComponent<objectScript>();
					component.mS_ = this.mS_;
					component.sfx_ = this.mS_.sfx_;
					component.tS_ = this.tS_;
					component.mapS_ = this.mapS_;
					component.myID = Mathf.RoundToInt((float)UnityEngine.Random.Range(1, 1999999999));
					component.typ = newTyp;
					component.InitObjectFromSavegame();
					this.mS_.objectRotation = objectScript.transform.eulerAngles.y;
					component.PlatziereObject(objectScript.transform.position, true, false);
					this.mS_.Pay((long)component.preis, 1);
					this.guiMain_.MoneyPop(component.preis, component.transform.position, false);
					this.mS_.Multiplayer_SendObject(component.myID, newTyp, component.transform.position.x, component.transform.position.z, component.transform.eulerAngles.y);
					this.mS_.Earn((long)Mathf.RoundToInt((float)objectScript.GetVerkaufspreis()), 0);
					this.mS_.Multiplayer_SendObjectDelete(objectScript.myID);
					UnityEngine.Object.Destroy(objectScript.gameObject);
				}
			}
		}
		return false;
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x001301F0 File Offset: 0x0012E3F0
	private bool TaskCheckFailed(int taskID_)
	{
		return this.taskID == -1 || !this.taskGameObject || Mathf.RoundToInt(this.taskGameObject.transform.position.x) != taskID_;
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x0013022C File Offset: 0x0012E42C
	public taskFanshop GetTaskFanshop()
	{
		if (this.TaskCheckFailed(270))
		{
			return null;
		}
		if (!this.myTaskFanshop)
		{
			this.myTaskFanshop = this.taskGameObject.GetComponent<taskFanshop>();
			return this.myTaskFanshop;
		}
		if (this.taskID == this.myTaskFanshop.myID)
		{
			return this.myTaskFanshop;
		}
		this.myTaskFanshop = this.taskGameObject.GetComponent<taskFanshop>();
		return this.myTaskFanshop;
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x001302A0 File Offset: 0x0012E4A0
	public taskWait GetTaskWait()
	{
		if (this.TaskCheckFailed(260))
		{
			return null;
		}
		if (!this.myTaskWait)
		{
			this.myTaskWait = this.taskGameObject.GetComponent<taskWait>();
			return this.myTaskWait;
		}
		if (this.taskID == this.myTaskWait.myID)
		{
			return this.myTaskWait;
		}
		this.myTaskWait = this.taskGameObject.GetComponent<taskWait>();
		return this.myTaskWait;
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x00130314 File Offset: 0x0012E514
	public taskUnterstuetzen GetTaskUnterstuetzen()
	{
		if (this.TaskCheckFailed(250))
		{
			return null;
		}
		if (!this.myTaskUnterstuetzen)
		{
			this.myTaskUnterstuetzen = this.taskGameObject.GetComponent<taskUnterstuetzen>();
			return this.myTaskUnterstuetzen;
		}
		if (this.taskID == this.myTaskUnterstuetzen.myID)
		{
			return this.myTaskUnterstuetzen;
		}
		this.myTaskUnterstuetzen = this.taskGameObject.GetComponent<taskUnterstuetzen>();
		return this.myTaskUnterstuetzen;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x00130388 File Offset: 0x0012E588
	public taskPolishing GetTaskPolishing()
	{
		if (this.TaskCheckFailed(240))
		{
			return null;
		}
		if (!this.myTaskPolishing)
		{
			this.myTaskPolishing = this.taskGameObject.GetComponent<taskPolishing>();
			return this.myTaskPolishing;
		}
		if (this.taskID == this.myTaskPolishing.myID)
		{
			return this.myTaskPolishing;
		}
		this.myTaskPolishing = this.taskGameObject.GetComponent<taskPolishing>();
		return this.myTaskPolishing;
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x001303FC File Offset: 0x0012E5FC
	public taskMarktforschung GetTaskMarktforschung()
	{
		if (this.TaskCheckFailed(230))
		{
			return null;
		}
		if (!this.myTaskMarktforschung)
		{
			this.myTaskMarktforschung = this.taskGameObject.GetComponent<taskMarktforschung>();
			return this.myTaskMarktforschung;
		}
		if (this.taskID == this.myTaskMarktforschung.myID)
		{
			return this.myTaskMarktforschung;
		}
		this.myTaskMarktforschung = this.taskGameObject.GetComponent<taskMarktforschung>();
		return this.myTaskMarktforschung;
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x00130470 File Offset: 0x0012E670
	public taskContractWait GetTaskContractWait()
	{
		if (this.TaskCheckFailed(220))
		{
			return null;
		}
		if (!this.myTaskContractWait)
		{
			this.myTaskContractWait = this.taskGameObject.GetComponent<taskContractWait>();
			return this.myTaskContractWait;
		}
		if (this.taskID == this.myTaskContractWait.myID)
		{
			return this.myTaskContractWait;
		}
		this.myTaskContractWait = this.taskGameObject.GetComponent<taskContractWait>();
		return this.myTaskContractWait;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x001304E4 File Offset: 0x0012E6E4
	public taskContractWork GetTaskContractWork()
	{
		if (this.TaskCheckFailed(210))
		{
			return null;
		}
		if (!this.myTaskContractWork)
		{
			this.myTaskContractWork = this.taskGameObject.GetComponent<taskContractWork>();
			return this.myTaskContractWork;
		}
		if (this.taskID == this.myTaskContractWork.myID)
		{
			return this.myTaskContractWork;
		}
		this.myTaskContractWork = this.taskGameObject.GetComponent<taskContractWork>();
		return this.myTaskContractWork;
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x00130558 File Offset: 0x0012E758
	public taskSupport GetTaskSupport()
	{
		if (this.TaskCheckFailed(200))
		{
			return null;
		}
		if (!this.myTaskSupport)
		{
			this.myTaskSupport = this.taskGameObject.GetComponent<taskSupport>();
			return this.myTaskSupport;
		}
		if (this.taskID == this.myTaskSupport.myID)
		{
			return this.myTaskSupport;
		}
		this.myTaskSupport = this.taskGameObject.GetComponent<taskSupport>();
		return this.myTaskSupport;
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x001305CC File Offset: 0x0012E7CC
	public taskFankampagne GetTaskFankampagne()
	{
		if (this.TaskCheckFailed(190))
		{
			return null;
		}
		if (!this.myTaskFankampagne)
		{
			this.myTaskFankampagne = this.taskGameObject.GetComponent<taskFankampagne>();
			return this.myTaskFankampagne;
		}
		if (this.taskID == this.myTaskFankampagne.myID)
		{
			return this.myTaskFankampagne;
		}
		this.myTaskFankampagne = this.taskGameObject.GetComponent<taskFankampagne>();
		return this.myTaskFankampagne;
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x00130640 File Offset: 0x0012E840
	public taskKonsole GetTaskKonsole()
	{
		if (this.TaskCheckFailed(180))
		{
			return null;
		}
		if (!this.myTaskKonsole)
		{
			this.myTaskKonsole = this.taskGameObject.GetComponent<taskKonsole>();
			return this.myTaskKonsole;
		}
		if (this.taskID == this.myTaskKonsole.myID)
		{
			return this.myTaskKonsole;
		}
		this.myTaskKonsole = this.taskGameObject.GetComponent<taskKonsole>();
		return this.myTaskKonsole;
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x001306B4 File Offset: 0x0012E8B4
	public taskArcadeProduction GetTaskArcadeProduction()
	{
		if (this.TaskCheckFailed(170))
		{
			return null;
		}
		if (!this.myTaskArcadeProduction)
		{
			this.myTaskArcadeProduction = this.taskGameObject.GetComponent<taskArcadeProduction>();
			return this.myTaskArcadeProduction;
		}
		if (this.taskID == this.myTaskArcadeProduction.myID)
		{
			return this.myTaskArcadeProduction;
		}
		this.myTaskArcadeProduction = this.taskGameObject.GetComponent<taskArcadeProduction>();
		return this.myTaskArcadeProduction;
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x00130728 File Offset: 0x0012E928
	public taskProduction GetTaskProduction()
	{
		if (this.TaskCheckFailed(160))
		{
			return null;
		}
		if (!this.myTaskProduction)
		{
			this.myTaskProduction = this.taskGameObject.GetComponent<taskProduction>();
			return this.myTaskProduction;
		}
		if (this.taskID == this.myTaskProduction.myID)
		{
			return this.myTaskProduction;
		}
		this.myTaskProduction = this.taskGameObject.GetComponent<taskProduction>();
		return this.myTaskProduction;
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x0013079C File Offset: 0x0012E99C
	public taskAnimationVerbessern GetTaskAnimationVerbessern()
	{
		if (this.TaskCheckFailed(150))
		{
			return null;
		}
		if (!this.myTaskAnimationVerbessern)
		{
			this.myTaskAnimationVerbessern = this.taskGameObject.GetComponent<taskAnimationVerbessern>();
			return this.myTaskAnimationVerbessern;
		}
		if (this.taskID == this.myTaskAnimationVerbessern.myID)
		{
			return this.myTaskAnimationVerbessern;
		}
		this.myTaskAnimationVerbessern = this.taskGameObject.GetComponent<taskAnimationVerbessern>();
		return this.myTaskAnimationVerbessern;
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x00130810 File Offset: 0x0012EA10
	public taskSoundVerbessern GetTaskSoundVerbessern()
	{
		if (this.TaskCheckFailed(140))
		{
			return null;
		}
		if (!this.myTaskSoundVerbessern)
		{
			this.myTaskSoundVerbessern = this.taskGameObject.GetComponent<taskSoundVerbessern>();
			return this.myTaskSoundVerbessern;
		}
		if (this.taskID == this.myTaskSoundVerbessern.myID)
		{
			return this.myTaskSoundVerbessern;
		}
		this.myTaskSoundVerbessern = this.taskGameObject.GetComponent<taskSoundVerbessern>();
		return this.myTaskSoundVerbessern;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x00130884 File Offset: 0x0012EA84
	public taskGrafikVerbessern GetTaskGrafikVerbessern()
	{
		if (this.TaskCheckFailed(130))
		{
			return null;
		}
		if (!this.myTaskGrafikVerbessern)
		{
			this.myTaskGrafikVerbessern = this.taskGameObject.GetComponent<taskGrafikVerbessern>();
			return this.myTaskGrafikVerbessern;
		}
		if (this.taskID == this.myTaskGrafikVerbessern.myID)
		{
			return this.myTaskGrafikVerbessern;
		}
		this.myTaskGrafikVerbessern = this.taskGameObject.GetComponent<taskGrafikVerbessern>();
		return this.myTaskGrafikVerbessern;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x001308F8 File Offset: 0x0012EAF8
	public taskBugfixing GetTaskBugfixing()
	{
		if (this.TaskCheckFailed(120))
		{
			return null;
		}
		if (!this.myTaskBugfixing)
		{
			this.myTaskBugfixing = this.taskGameObject.GetComponent<taskBugfixing>();
			return this.myTaskBugfixing;
		}
		if (this.taskID == this.myTaskBugfixing.myID)
		{
			return this.myTaskBugfixing;
		}
		this.myTaskBugfixing = this.taskGameObject.GetComponent<taskBugfixing>();
		return this.myTaskBugfixing;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00130968 File Offset: 0x0012EB68
	public taskGameplayVerbessern GetTaskGameplayVerbessern()
	{
		if (this.TaskCheckFailed(110))
		{
			return null;
		}
		if (!this.myTaskGameplayVerbessern)
		{
			this.myTaskGameplayVerbessern = this.taskGameObject.GetComponent<taskGameplayVerbessern>();
			return this.myTaskGameplayVerbessern;
		}
		if (this.taskID == this.myTaskGameplayVerbessern.myID)
		{
			return this.myTaskGameplayVerbessern;
		}
		this.myTaskGameplayVerbessern = this.taskGameObject.GetComponent<taskGameplayVerbessern>();
		return this.myTaskGameplayVerbessern;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x001309D8 File Offset: 0x0012EBD8
	public taskSpielbericht GetTaskSpielbericht()
	{
		if (this.TaskCheckFailed(100))
		{
			return null;
		}
		if (!this.myTaskSpielbericht)
		{
			this.myTaskSpielbericht = this.taskGameObject.GetComponent<taskSpielbericht>();
			return this.myTaskSpielbericht;
		}
		if (this.taskID == this.myTaskSpielbericht.myID)
		{
			return this.myTaskSpielbericht;
		}
		this.myTaskSpielbericht = this.taskGameObject.GetComponent<taskSpielbericht>();
		return this.myTaskSpielbericht;
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x00130A48 File Offset: 0x0012EC48
	public taskTraining GetTaskTraining()
	{
		if (this.TaskCheckFailed(90))
		{
			return null;
		}
		if (!this.myTaskTraining)
		{
			this.myTaskTraining = this.taskGameObject.GetComponent<taskTraining>();
			return this.myTaskTraining;
		}
		if (this.taskID == this.myTaskTraining.myID)
		{
			return this.myTaskTraining;
		}
		this.myTaskTraining = this.taskGameObject.GetComponent<taskTraining>();
		return this.myTaskTraining;
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x00130AB8 File Offset: 0x0012ECB8
	public taskMitarbeitersuche GetTaskMitarbeitersuche()
	{
		if (this.TaskCheckFailed(80))
		{
			return null;
		}
		if (!this.myTaskMitarbeitersuche)
		{
			this.myTaskMitarbeitersuche = this.taskGameObject.GetComponent<taskMitarbeitersuche>();
			return this.myTaskMitarbeitersuche;
		}
		if (this.taskID == this.myTaskMitarbeitersuche.myID)
		{
			return this.myTaskMitarbeitersuche;
		}
		this.myTaskMitarbeitersuche = this.taskGameObject.GetComponent<taskMitarbeitersuche>();
		return this.myTaskMitarbeitersuche;
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x00130B28 File Offset: 0x0012ED28
	public taskMarketingSpezial GetTaskMarketingSpezial()
	{
		if (this.TaskCheckFailed(70))
		{
			return null;
		}
		if (!this.myTaskMarketingSpezial)
		{
			this.myTaskMarketingSpezial = this.taskGameObject.GetComponent<taskMarketingSpezial>();
			return this.myTaskMarketingSpezial;
		}
		if (this.taskID == this.myTaskMarketingSpezial.myID)
		{
			return this.myTaskMarketingSpezial;
		}
		this.myTaskMarketingSpezial = this.taskGameObject.GetComponent<taskMarketingSpezial>();
		return this.myTaskMarketingSpezial;
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x00130B98 File Offset: 0x0012ED98
	public taskMarketing GetTaskMarketing()
	{
		if (this.TaskCheckFailed(60))
		{
			return null;
		}
		if (!this.myTaskMarketing)
		{
			this.myTaskMarketing = this.taskGameObject.GetComponent<taskMarketing>();
			return this.myTaskMarketing;
		}
		if (this.taskID == this.myTaskMarketing.myID)
		{
			return this.myTaskMarketing;
		}
		this.myTaskMarketing = this.taskGameObject.GetComponent<taskMarketing>();
		return this.myTaskMarketing;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x00130C08 File Offset: 0x0012EE08
	public taskF2PUpdate GetTaskF2PUpdate()
	{
		if (this.TaskCheckFailed(50))
		{
			return null;
		}
		if (!this.myTaskF2PUpdate)
		{
			this.myTaskF2PUpdate = this.taskGameObject.GetComponent<taskF2PUpdate>();
			return this.myTaskF2PUpdate;
		}
		if (this.taskID == this.myTaskF2PUpdate.myID)
		{
			return this.myTaskF2PUpdate;
		}
		this.myTaskF2PUpdate = this.taskGameObject.GetComponent<taskF2PUpdate>();
		return this.myTaskF2PUpdate;
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x00130C78 File Offset: 0x0012EE78
	public taskGame GetTaskGame()
	{
		if (this.TaskCheckFailed(40))
		{
			return null;
		}
		if (!this.myTaskGame)
		{
			this.myTaskGame = this.taskGameObject.GetComponent<taskGame>();
			return this.myTaskGame;
		}
		if (this.taskID == this.myTaskGame.myID)
		{
			return this.myTaskGame;
		}
		this.myTaskGame = this.taskGameObject.GetComponent<taskGame>();
		return this.myTaskGame;
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x00130CE8 File Offset: 0x0012EEE8
	public taskForschung GetTaskForschung()
	{
		if (this.TaskCheckFailed(10))
		{
			return null;
		}
		if (!this.myTaskForschung)
		{
			this.myTaskForschung = this.taskGameObject.GetComponent<taskForschung>();
			return this.myTaskForschung;
		}
		if (this.taskID == this.myTaskForschung.myID)
		{
			return this.myTaskForschung;
		}
		this.myTaskForschung = this.taskGameObject.GetComponent<taskForschung>();
		return this.myTaskForschung;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x00130D58 File Offset: 0x0012EF58
	public taskEngine GetTaskEngine()
	{
		if (this.TaskCheckFailed(20))
		{
			return null;
		}
		if (!this.myTaskEngine)
		{
			this.myTaskEngine = this.taskGameObject.GetComponent<taskEngine>();
			return this.myTaskEngine;
		}
		if (this.taskID == this.myTaskEngine.myID)
		{
			return this.myTaskEngine;
		}
		this.myTaskEngine = this.taskGameObject.GetComponent<taskEngine>();
		return this.myTaskEngine;
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x00130DC8 File Offset: 0x0012EFC8
	public taskUpdate GetTaskUpdate()
	{
		if (this.TaskCheckFailed(30))
		{
			return null;
		}
		if (!this.myTaskUpdate)
		{
			this.myTaskUpdate = this.taskGameObject.GetComponent<taskUpdate>();
			return this.myTaskUpdate;
		}
		if (this.taskID == this.myTaskUpdate.myID)
		{
			return this.myTaskUpdate;
		}
		this.myTaskUpdate = this.taskGameObject.GetComponent<taskUpdate>();
		return this.myTaskUpdate;
	}

	// Token: 0x040025D9 RID: 9689
	private GameObject main_;

	// Token: 0x040025DA RID: 9690
	public mainScript mS_;

	// Token: 0x040025DB RID: 9691
	public Camera camera_;

	// Token: 0x040025DC RID: 9692
	private settingsScript settings_;

	// Token: 0x040025DD RID: 9693
	private mapScript mapS_;

	// Token: 0x040025DE RID: 9694
	private GUI_Main guiMain_;

	// Token: 0x040025DF RID: 9695
	private mainCameraScript mCamS_;

	// Token: 0x040025E0 RID: 9696
	private genres genres_;

	// Token: 0x040025E1 RID: 9697
	private themes themes_;

	// Token: 0x040025E2 RID: 9698
	private engineFeatures eF_;

	// Token: 0x040025E3 RID: 9699
	private gameplayFeatures gF_;

	// Token: 0x040025E4 RID: 9700
	private hardware hardware_;

	// Token: 0x040025E5 RID: 9701
	private hardwareFeatures hardwareFeatures_;

	// Token: 0x040025E6 RID: 9702
	private textScript tS_;

	// Token: 0x040025E7 RID: 9703
	private buildRoomScript brS_;

	// Token: 0x040025E8 RID: 9704
	private roomDataScript rdS_;

	// Token: 0x040025E9 RID: 9705
	private forschungSonstiges fS_;

	// Token: 0x040025EA RID: 9706
	public int myID;

	// Token: 0x040025EB RID: 9707
	public int typ;

	// Token: 0x040025EC RID: 9708
	public string myName;

	// Token: 0x040025ED RID: 9709
	public int taskID = -1;

	// Token: 0x040025EE RID: 9710
	public GameObject taskGameObject;

	// Token: 0x040025EF RID: 9711
	public bool pause;

	// Token: 0x040025F0 RID: 9712
	public bool lockKI;

	// Token: 0x040025F1 RID: 9713
	private int arbeitsplaetze;

	// Token: 0x040025F2 RID: 9714
	private int lagerplatz;

	// Token: 0x040025F3 RID: 9715
	public int lagerplatzUsed;

	// Token: 0x040025F4 RID: 9716
	private int serverplatz;

	// Token: 0x040025F5 RID: 9717
	public int serverplatzUsed;

	// Token: 0x040025F6 RID: 9718
	public int mitarbeiterZugeteilt;

	// Token: 0x040025F7 RID: 9719
	public bool serverDown;

	// Token: 0x040025F8 RID: 9720
	public bool serverOverheat;

	// Token: 0x040025F9 RID: 9721
	public int leitenderGamedesigner = -1;

	// Token: 0x040025FA RID: 9722
	public int leitenderTechniker = -1;

	// Token: 0x040025FB RID: 9723
	public Vector3 uiPos;

	// Token: 0x040025FC RID: 9724
	public GameObject myDoor;

	// Token: 0x040025FD RID: 9725
	public GameObject[] uiObjects;

	// Token: 0x040025FE RID: 9726
	public GameObject myUI;

	// Token: 0x040025FF RID: 9727
	private roomButtonScript rbS_;

	// Token: 0x04002600 RID: 9728
	public GameObject myUI_Line;

	// Token: 0x04002601 RID: 9729
	public GameObject myUI_UnterstuetzenLine;

	// Token: 0x04002602 RID: 9730
	private bool outline;

	// Token: 0x04002603 RID: 9731
	public List<GameObject> listGameObjects = new List<GameObject>();

	// Token: 0x04002604 RID: 9732
	public List<objectScript> listInventar = new List<objectScript>();

	// Token: 0x04002605 RID: 9733
	private Vector2 invisibleGUI = new Vector2(-300f, 0f);

	// Token: 0x04002606 RID: 9734
	private RectTransform myGUIrectTransform;

	// Token: 0x04002607 RID: 9735
	private Vector3 ROOMLINE_cameraPos;

	// Token: 0x04002608 RID: 9736
	private Quaternion ROOMLINE_cameraRot;

	// Token: 0x04002609 RID: 9737
	private VectorLine roomLine3D;

	// Token: 0x0400260A RID: 9738
	private bool initRoomLine;

	// Token: 0x0400260B RID: 9739
	private bool isCrunchTime;

	// Token: 0x0400260C RID: 9740
	private float DrawLine_timer;

	// Token: 0x0400260D RID: 9741
	private Vector3 cameraPos;

	// Token: 0x0400260E RID: 9742
	private Quaternion cameraRot;

	// Token: 0x0400260F RID: 9743
	private VectorLine drawLine3D;

	// Token: 0x04002610 RID: 9744
	private bool initLine;

	// Token: 0x04002611 RID: 9745
	private float lagerraumTimer;

	// Token: 0x04002612 RID: 9746
	private taskFanshop myTaskFanshop;

	// Token: 0x04002613 RID: 9747
	private taskWait myTaskWait;

	// Token: 0x04002614 RID: 9748
	private taskUnterstuetzen myTaskUnterstuetzen;

	// Token: 0x04002615 RID: 9749
	private taskPolishing myTaskPolishing;

	// Token: 0x04002616 RID: 9750
	private taskMarktforschung myTaskMarktforschung;

	// Token: 0x04002617 RID: 9751
	private taskContractWait myTaskContractWait;

	// Token: 0x04002618 RID: 9752
	private taskContractWork myTaskContractWork;

	// Token: 0x04002619 RID: 9753
	private taskSupport myTaskSupport;

	// Token: 0x0400261A RID: 9754
	private taskFankampagne myTaskFankampagne;

	// Token: 0x0400261B RID: 9755
	private taskKonsole myTaskKonsole;

	// Token: 0x0400261C RID: 9756
	private taskArcadeProduction myTaskArcadeProduction;

	// Token: 0x0400261D RID: 9757
	private taskProduction myTaskProduction;

	// Token: 0x0400261E RID: 9758
	private taskAnimationVerbessern myTaskAnimationVerbessern;

	// Token: 0x0400261F RID: 9759
	private taskSoundVerbessern myTaskSoundVerbessern;

	// Token: 0x04002620 RID: 9760
	private taskGrafikVerbessern myTaskGrafikVerbessern;

	// Token: 0x04002621 RID: 9761
	private taskBugfixing myTaskBugfixing;

	// Token: 0x04002622 RID: 9762
	private taskGameplayVerbessern myTaskGameplayVerbessern;

	// Token: 0x04002623 RID: 9763
	private taskSpielbericht myTaskSpielbericht;

	// Token: 0x04002624 RID: 9764
	private taskTraining myTaskTraining;

	// Token: 0x04002625 RID: 9765
	private taskMitarbeitersuche myTaskMitarbeitersuche;

	// Token: 0x04002626 RID: 9766
	private taskMarketingSpezial myTaskMarketingSpezial;

	// Token: 0x04002627 RID: 9767
	private taskMarketing myTaskMarketing;

	// Token: 0x04002628 RID: 9768
	private taskF2PUpdate myTaskF2PUpdate;

	// Token: 0x04002629 RID: 9769
	private taskGame myTaskGame;

	// Token: 0x0400262A RID: 9770
	private taskForschung myTaskForschung;

	// Token: 0x0400262B RID: 9771
	private taskEngine myTaskEngine;

	// Token: 0x0400262C RID: 9772
	private taskUpdate myTaskUpdate;
}
