using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;


public class roomScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.InitUI();
		this.mS_.findRooms = true;
	}

	
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findRooms = true;
		}
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

	
	private void Update()
	{
		this.FindTasks();
		this.UpdateListInventar();
		this.UpdateLagerraumGFX();
		this.isCrunchTime = this.IsCrunchtime();
		this.UpdateUI();
	}

	
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

	
	public bool IsCrunchtimeRead()
	{
		return this.isCrunchTime;
	}

	
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

	
	public Vector2 Get2DPos()
	{
		Vector3 position = this.uiPos;
		return this.camera_.WorldToScreenPoint(position);
	}

	
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

	
	public bool IsUberberfuell()
	{
		return this.arbeitsplaetze > 0 && this.GetMitarbeiter() > this.AnzahlArbeitsplaetzeBisUberfuellt();
	}

	
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

	
	public GameObject GetRandomFloor()
	{
		return this.listGameObjects[UnityEngine.Random.Range(0, this.listGameObjects.Count)];
	}

	
	public int GetArbeitsplaetze()
	{
		return this.arbeitsplaetze;
	}

	
	public int GetMitarbeiter()
	{
		return this.mitarbeiterZugeteilt;
	}

	
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

	
	public bool KeineAnrufe()
	{
		return this.mS_.anrufe <= 0 && this.typ == 7 && this.taskID != -1 && (this.taskGameObject && this.GetTaskSupport() && this.mS_.anrufe <= 0);
	}

	
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

	
	public bool IsGameDevComplete()
	{
		if (this.typ != 1)
		{
			return false;
		}
		int num = this.taskID;
		return false;
	}

	
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

	
	public bool IstContractWorkWait()
	{
		return this.taskID != -1 && (this.taskGameObject && this.GetTaskContractWait());
	}

	
	public bool IstTaskWait()
	{
		return this.taskID != -1 && (this.taskGameObject && this.GetTaskWait());
	}

	
	public int GetLagerplatz()
	{
		return this.lagerplatz;
	}

	
	public int GetFreeLagerplatz()
	{
		return this.lagerplatz - this.lagerplatzUsed;
	}

	
	public int GetServerplatz()
	{
		return this.serverplatz;
	}

	
	public int GetFreeServerplatz()
	{
		return this.serverplatz - this.serverplatzUsed;
	}

	
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

	
	public void ServerAbschalten(bool shutdown)
	{
		this.serverDown = shutdown;
	}

	
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

	
	private bool TaskCheckFailed(int taskID_)
	{
		return this.taskID == -1 || !this.taskGameObject || Mathf.RoundToInt(this.taskGameObject.transform.position.x) != taskID_;
	}

	
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

	
	private GameObject main_;

	
	public mainScript mS_;

	
	public Camera camera_;

	
	private settingsScript settings_;

	
	private mapScript mapS_;

	
	private GUI_Main guiMain_;

	
	private mainCameraScript mCamS_;

	
	private genres genres_;

	
	private themes themes_;

	
	private engineFeatures eF_;

	
	private gameplayFeatures gF_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private textScript tS_;

	
	private buildRoomScript brS_;

	
	private roomDataScript rdS_;

	
	private forschungSonstiges fS_;

	
	public int myID;

	
	public int typ;

	
	public string myName;

	
	public int taskID = -1;

	
	public GameObject taskGameObject;

	
	public bool pause;

	
	public bool lockKI;

	
	private int arbeitsplaetze;

	
	private int lagerplatz;

	
	public int lagerplatzUsed;

	
	private int serverplatz;

	
	public int serverplatzUsed;

	
	public int mitarbeiterZugeteilt;

	
	public bool serverDown;

	
	public bool serverOverheat;

	
	public int leitenderGamedesigner = -1;

	
	public int leitenderTechniker = -1;

	
	public Vector3 uiPos;

	
	public GameObject myDoor;

	
	public GameObject[] uiObjects;

	
	public GameObject myUI;

	
	private roomButtonScript rbS_;

	
	public GameObject myUI_Line;

	
	public GameObject myUI_UnterstuetzenLine;

	
	private bool outline;

	
	public List<GameObject> listGameObjects = new List<GameObject>();

	
	public List<objectScript> listInventar = new List<objectScript>();

	
	private Vector2 invisibleGUI = new Vector2(-300f, 0f);

	
	private RectTransform myGUIrectTransform;

	
	private Vector3 ROOMLINE_cameraPos;

	
	private Quaternion ROOMLINE_cameraRot;

	
	private VectorLine roomLine3D;

	
	private bool initRoomLine;

	
	private bool isCrunchTime;

	
	private float DrawLine_timer;

	
	private Vector3 cameraPos;

	
	private Quaternion cameraRot;

	
	private VectorLine drawLine3D;

	
	private bool initLine;

	
	private float lagerraumTimer;

	
	private taskFanshop myTaskFanshop;

	
	private taskWait myTaskWait;

	
	private taskUnterstuetzen myTaskUnterstuetzen;

	
	private taskPolishing myTaskPolishing;

	
	private taskMarktforschung myTaskMarktforschung;

	
	private taskContractWait myTaskContractWait;

	
	private taskContractWork myTaskContractWork;

	
	private taskSupport myTaskSupport;

	
	private taskFankampagne myTaskFankampagne;

	
	private taskKonsole myTaskKonsole;

	
	private taskArcadeProduction myTaskArcadeProduction;

	
	private taskProduction myTaskProduction;

	
	private taskAnimationVerbessern myTaskAnimationVerbessern;

	
	private taskSoundVerbessern myTaskSoundVerbessern;

	
	private taskGrafikVerbessern myTaskGrafikVerbessern;

	
	private taskBugfixing myTaskBugfixing;

	
	private taskGameplayVerbessern myTaskGameplayVerbessern;

	
	private taskSpielbericht myTaskSpielbericht;

	
	private taskTraining myTaskTraining;

	
	private taskMitarbeitersuche myTaskMitarbeitersuche;

	
	private taskMarketingSpezial myTaskMarketingSpezial;

	
	private taskMarketing myTaskMarketing;

	
	private taskF2PUpdate myTaskF2PUpdate;

	
	private taskGame myTaskGame;

	
	private taskForschung myTaskForschung;

	
	private taskEngine myTaskEngine;

	
	private taskUpdate myTaskUpdate;
}
