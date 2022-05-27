using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class movementScript : MonoBehaviour
{
	// Token: 0x06000167 RID: 359 RVA: 0x0001536E File Offset: 0x0001356E
	private void Start()
	{
		this.randomDeskEmoteTime = UnityEngine.Random.Range(5f, 30f);
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00015394 File Offset: 0x00013594
	private void Init()
	{
		if (base.gameObject.tag == "CharAM")
		{
			this.charArbeitsmarkt = true;
		}
		if (!this.cS_)
		{
			this.cS_ = base.GetComponent<characterScript>();
		}
		if (this.charGFX == null)
		{
			this.charGFX = base.transform.GetChild(0).gameObject;
		}
		if (this.charAnimation == null)
		{
			this.charAnimation = this.charGFX.GetComponent<Animator>();
		}
		this.InitPathfinding();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00015424 File Offset: 0x00013624
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.clipS_)
		{
			this.clipS_ = this.main_.GetComponent<clipScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.seeker)
		{
			this.seeker = base.GetComponent<Seeker>();
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000154E3 File Offset: 0x000136E3
	public void InitUpdate()
	{
		if (base.gameObject.tag == "CharAM")
		{
			this.charArbeitsmarkt = true;
			return;
		}
		this.TargetReached(true);
		this.SetAnimation();
		this.charAnimation.speed = 100f;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00015521 File Offset: 0x00013721
	private void Update()
	{
		if (this.charArbeitsmarkt)
		{
			return;
		}
		this.Move();
		this.FindTarget();
		this.SetAnimation();
		this.TargetReached(false);
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00015545 File Offset: 0x00013745
	private void InitPathfinding()
	{
		if (this.aStar == null)
		{
			this.aStar = base.GetComponent<IAstarAI>();
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0001555C File Offset: 0x0001375C
	private void Move()
	{
		if (this.cS_.picked)
		{
			return;
		}
		if (this.waitForceAnimation > 0f)
		{
			return;
		}
		if (this.cS_.objectUsingID != -1)
		{
			return;
		}
		if (this.waitOnFloor > 0f)
		{
			this.waitOnFloor -= this.mS_.GetDeltaTime();
			return;
		}
		float num = 1f;
		if (this.cS_.perks[7])
		{
			num = 1.5f;
		}
		if (this.cS_.objectBelegtS_ && this.cS_.objectBelegtS_.isGhostWC)
		{
			num = 3f;
		}
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		if (this.mS_)
		{
			Vector3 nextPosition;
			Quaternion nextRotation;
			this.aStar.MovementUpdate(this.mS_.GetDeltaTime() * num, out nextPosition, out nextRotation);
			this.aStar.FinalizeMovement(nextPosition, nextRotation);
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0001566A File Offset: 0x0001386A
	public void SetAnimationForce(string c, AnimationClip clip)
	{
		this.PlayAnimation(c);
		this.waitForceAnimation = clip.length;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00015680 File Offset: 0x00013880
	private void SetAnimation()
	{
		this.charAnimation.speed = this.mS_.GetGameSpeed();
		if (this.cS_.picked)
		{
			this.cS_.HideAddObjects();
			this.charAnimation.speed = 1f;
			this.waitOnFloor = 0f;
			this.PlayAnimation("picked");
			return;
		}
		if (this.waitForceAnimation > 0f)
		{
			this.waitForceAnimation -= this.mS_.GetDeltaTime();
			if (this.waitForceAnimation < 0f)
			{
				this.cS_.HideAddObjects();
				if (this.currentAnimation == "standDrink")
				{
					this.cS_.ResetDurst(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "wc")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxAnimation.Play("wcKabine1Auf");
					}
					this.cS_.ResetWC(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
					this.FindObjectInRoom(5, null, true);
				}
				if (this.currentAnimation == "sink")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
					}
					this.cS_.ResetWaschbecken(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
					this.FindObjectInRoom(6, null, true);
				}
				if (this.currentAnimation == "handtrockner")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "arcade")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
						this.cS_.objectUsingS_.gfxHide.SetActive(true);
						this.cS_.ResetMotivation(true, this.cS_.objectUsingS_.motivationRegen);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "motSit1")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.ResetMotivation(true, this.cS_.objectUsingS_.motivationRegen);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "dart")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
						this.cS_.ResetMotivation(true, this.cS_.objectUsingS_.motivationRegen);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "golf")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
						this.cS_.ResetMotivation(true, this.cS_.objectUsingS_.motivationRegen);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "piano")
				{
					if (this.cS_.objectUsingS_)
					{
						this.cS_.objectUsingS_.gfxShow.SetActive(false);
						this.cS_.ResetMotivation(true, this.cS_.objectUsingS_.motivationRegen);
					}
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "arztSchrank")
				{
					this.cS_.ResetKrank();
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "muell")
				{
					this.cS_.ResetMuell(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "muellFloor")
				{
					this.cS_.ResetMuell(false);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "waterCan")
				{
					this.cS_.ResetGiessen(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "ghostPlant")
				{
					this.cS_.ResetGiessen(false);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "ghostDrink")
				{
					this.cS_.ResetDurst(false);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "ghostWC")
				{
					this.cS_.ResetWC(false);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "ghostSink")
				{
					this.cS_.ResetWaschbecken(false);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
				}
				if (this.currentAnimation == "pause1" || this.currentAnimation == "pause2" || this.currentAnimation == "pause3" || this.currentAnimation == "pause4" || this.currentAnimation == "pauseSit1" || this.currentAnimation == "freezer" || this.currentAnimation == "tv" || this.currentAnimation == "dance")
				{
					if (this.cS_.objectUsingS_)
					{
						if (this.cS_.objectUsingS_.gfxShow)
						{
							this.cS_.objectUsingS_.gfxShow.SetActive(false);
						}
						if (this.cS_.objectUsingS_.gfxHide)
						{
							this.cS_.objectUsingS_.gfxHide.SetActive(true);
						}
					}
					this.cS_.ResetPause(true);
					this.cS_.RemoveObjectUsing();
					this.DeleteTarget();
					int num = UnityEngine.Random.Range(0, 3);
					if (num != 0)
					{
						if (num != 1)
						{
							return;
						}
						this.FindObjectInRoom(1, null, false);
					}
					else if (!this.cS_.perks[13])
					{
						this.FindObjectInRoom(4, null, false);
						return;
					}
				}
			}
			return;
		}
		if (this.cS_.objectUsingID != -1)
		{
			this.randomDeskEmoteTime -= this.mS_.GetDeltaTime();
			if ((double)this.randomDeskEmoteTime > 0.0)
			{
				if (this.cS_.roomS_)
				{
					if (this.cS_.iDoWork)
					{
						switch (this.cS_.roomS_.typ)
						{
						case 1:
							this.PlayAnimation("sitTyping");
							break;
						case 2:
							this.PlayAnimation("sitTyping");
							break;
						case 3:
							this.PlayAnimation("gaming");
							this.cS_.ShowAddObject(6);
							break;
						case 4:
							this.PlayAnimation("writing");
							this.cS_.ShowAddObject(7);
							break;
						case 5:
							this.PlayAnimation("piano");
							break;
						case 6:
							this.PlayAnimation("sitTyping");
							break;
						case 7:
							this.PlayAnimation("sitTyping");
							break;
						case 8:
							this.PlayAnimation("writing");
							this.cS_.ShowAddObject(9);
							break;
						case 10:
							this.PlayAnimation("sitTyping");
							break;
						case 13:
							this.PlayAnimation("training");
							this.cS_.ShowAddObject(3);
							break;
						case 17:
							this.PlayAnimation("prodArcade");
							this.cS_.ShowAddObject(8);
							break;
						}
						if (this.cS_.IsVisible() && !this.cS_.hided && (this.audioTyping == -1 || (this.audioTyping > 0 && !this.sfx_.GetAudioSource(this.audioTyping).isPlaying)))
						{
							switch (UnityEngine.Random.Range(0, 5))
							{
							case 0:
								this.sfx_.PlaySound(19, false);
								this.audioTyping = 19;
								return;
							case 1:
								this.sfx_.PlaySound(20, false);
								this.audioTyping = 20;
								return;
							case 2:
								this.sfx_.PlaySound(21, false);
								this.audioTyping = 21;
								return;
							case 3:
								this.sfx_.PlaySound(22, false);
								this.audioTyping = 22;
								return;
							case 4:
								this.sfx_.PlaySound(23, false);
								this.audioTyping = 23;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						this.cS_.HideAddObjects();
						switch (this.cS_.roomS_.typ)
						{
						case 1:
							this.PlayAnimation("sitIdle");
							return;
						case 2:
							this.PlayAnimation("sitIdle");
							return;
						case 3:
							this.PlayAnimation("sitIdle");
							return;
						case 4:
							this.PlayAnimation("sitIdle");
							return;
						case 5:
							this.PlayAnimation("sitIdle");
							return;
						case 6:
							this.PlayAnimation("sitIdle");
							return;
						case 7:
							this.PlayAnimation("sitIdle");
							return;
						case 8:
							this.PlayAnimation("sitIdle");
							break;
						case 9:
						case 11:
						case 12:
						case 14:
						case 15:
						case 16:
							break;
						case 10:
							this.PlayAnimation("sitIdle");
							return;
						case 13:
							this.PlayAnimation("sitIdle");
							return;
						case 17:
							this.PlayAnimation("idle");
							return;
						default:
							return;
						}
					}
				}
				return;
			}
			this.cS_.HideAddObjects();
			this.randomDeskEmoteTime = UnityEngine.Random.Range(10f, 30f);
			if (this.cS_.roomS_.typ != 17)
			{
				switch (UnityEngine.Random.Range(0, 8))
				{
				case 0:
					this.PlayAnimation("sitAngry2");
					this.waitForceAnimation = this.clipS_.clip_sitAngry2.length;
					return;
				case 1:
					this.PlayAnimation("sitGesture");
					this.waitForceAnimation = this.clipS_.clip_sitGesture.length;
					return;
				case 2:
					this.PlayAnimation("sitClap3");
					this.waitForceAnimation = this.clipS_.clip_sitClap3.length;
					return;
				case 3:
					this.PlayAnimation("sitFistPump");
					this.waitForceAnimation = this.clipS_.clip_sitFistPump.length;
					return;
				case 4:
					this.PlayAnimation("sitVictory");
					this.waitForceAnimation = this.clipS_.clip_sitVictory.length;
					return;
				case 5:
					this.PlayAnimation("sitDisbelief");
					this.waitForceAnimation = this.clipS_.clip_sitDisbelief.length;
					return;
				case 6:
					this.PlayAnimation("sitYell2");
					this.waitForceAnimation = this.clipS_.clip_sitYell2.length;
					return;
				case 7:
					this.PlayAnimation("sitCheer");
					this.waitForceAnimation = this.clipS_.clip_sitCheer.length;
					return;
				default:
					return;
				}
			}
			else
			{
				switch (UnityEngine.Random.Range(0, 5))
				{
				case 0:
					this.PlayAnimation("yawn");
					this.waitForceAnimation = this.clipS_.clip_sitYawn.length;
					return;
				case 1:
					this.PlayAnimation("IdleArmGesture");
					this.waitForceAnimation = this.clipS_.clip_IdleArmGesture.length;
					return;
				case 2:
					this.PlayAnimation("IdleBriefCase");
					this.waitForceAnimation = this.clipS_.clip_IdleBriefCase.length;
					return;
				case 3:
					this.PlayAnimation("IdleMouthWipe");
					this.waitForceAnimation = this.clipS_.clip_IdleMouthWipe.length;
					return;
				case 4:
					this.PlayAnimation("IdleSandCover");
					this.waitForceAnimation = this.clipS_.clip_IdleSandCover.length;
					return;
				default:
					return;
				}
			}
		}
		else
		{
			if (this.waitOnFloor > 0f)
			{
				this.cS_.HideAddObjects();
				this.PlayAnimation("idle");
				return;
			}
			if (this.mS_.GetGameSpeed() <= 0f)
			{
				return;
			}
			if (Mathf.Abs(this.aStar.velocity.x) > 0f || Mathf.Abs(this.aStar.velocity.y) > 0f || Mathf.Abs(this.aStar.velocity.z) > 0f)
			{
				this.cS_.HideAddObjects();
				bool flag = false;
				if (this.cS_.objectBelegtS_ && this.cS_.objectBelegtS_.isGhostWC)
				{
					flag = true;
				}
				if (flag)
				{
					this.PlayAnimation("run");
					return;
				}
				if (this.cS_.male)
				{
					this.PlayAnimation("walk_m");
				}
				else
				{
					this.PlayAnimation("walk_f");
				}
				if (this.cS_.perks[7])
				{
					this.charAnimation.speed *= 1.5f;
					return;
				}
			}
			else
			{
				this.cS_.HideAddObjects();
				this.PlayAnimation("idle");
			}
			return;
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00016418 File Offset: 0x00014618
	private bool IsForceAnimation(string c)
	{
		return this.currentAnimation == c && this.waitForceAnimation > 0f;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00016438 File Offset: 0x00014638
	private void PlayAnimation(string c)
	{
		if (this.currentAnimation != c)
		{
			this.charAnimation.CrossFade(c, 0.1f, 0, 0f, 0.4f);
			this.currentAnimation = c;
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0001646C File Offset: 0x0001466C
	public void TargetReached(bool teleport)
	{
		if (!this.myTarget)
		{
			return;
		}
		if (!teleport && this.aStar.pathPending)
		{
			return;
		}
		if (this.walkDistance <= 0f)
		{
			this.walkDistance = 0.2f;
			objectScript component = this.myTarget.GetComponent<objectScript>();
			if (component)
			{
				if (component.isGhost)
				{
					if (this.aStar.remainingDistance > 20f)
					{
						this.walkDistance = UnityEngine.Random.Range(this.aStar.remainingDistance - 10f, this.aStar.remainingDistance - 15f);
					}
					else
					{
						this.walkDistance = UnityEngine.Random.Range(0f, this.aStar.remainingDistance);
					}
				}
			}
			else if (this.myTarget.tag == "Floor")
			{
				this.walkDistance = this.aStar.remainingDistance * 0.5f;
			}
		}
		if (Vector3.Distance(base.transform.position, this.myTarget.transform.position) < 0.2f || this.aStar.remainingDistance < 0.2f || this.aStar.reachedEndOfPath || this.aStar.remainingDistance < this.walkDistance || teleport)
		{
			this.walkDistance = -1f;
			if (this.myTarget.tag == "Object")
			{
				objectScript component2 = this.myTarget.GetComponent<objectScript>();
				GameObject gameObject;
				if (this.cS_.male)
				{
					gameObject = component2.pointMale;
				}
				else
				{
					gameObject = component2.pointFemale;
				}
				if (gameObject)
				{
					this.aStar.Teleport(gameObject.transform.position, true);
					base.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, -180f);
				}
				else
				{
					base.transform.LookAt(this.myTarget.transform);
					base.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, -180f);
				}
				this.cS_.objectUsingID = component2.myID;
				this.cS_.objectUsingS_ = component2;
				if (component2.isArbeitsplatz)
				{
					this.randomDeskEmoteTime = UnityEngine.Random.Range(5f, 15f);
				}
				if (component2.canDrink)
				{
					this.PlayAnimation("standDrink");
					this.waitForceAnimation = this.clipS_.clip_standDrink.length;
					this.cS_.ShowAddObject(0);
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(14, 1.09f, true, base.transform.position);
					}
				}
				if (component2.isGhostDrink)
				{
					this.PlayAnimation("ghostDrink");
					this.waitForceAnimation = this.clipS_.clip_ghostDrink.length;
					this.cS_.ShowAddObject(0);
				}
				if (component2.isMuelleimer)
				{
					this.PlayAnimation("muell");
					this.waitForceAnimation = this.clipS_.clip_muell.length;
					this.cS_.ShowAddObject(1);
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(17, 0.9f, true, base.transform.position);
					}
				}
				if (component2.isWC)
				{
					this.PlayAnimation("wc");
					this.waitForceAnimation = this.clipS_.clip_wc.length * 10f;
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(34, 11f, false, base.transform.position);
					}
					component2.gfxAnimation.Play("wcKabine1Zu");
				}
				if (component2.isSink)
				{
					this.PlayAnimation("sink");
					this.waitForceAnimation = this.clipS_.clip_sink.length;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
				}
				if (component2.isHandtrockner)
				{
					this.PlayAnimation("handtrockner");
					this.waitForceAnimation = this.clipS_.clip_handtrockner.length;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
				}
				if (component2.isArcade)
				{
					this.PlayAnimation("arcade");
					this.waitForceAnimation = this.clipS_.clip_arcade.length * 2f;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
					if (component2.gfxHide)
					{
						component2.gfxHide.SetActive(false);
					}
				}
				if (component2.isDart)
				{
					this.PlayAnimation("dart");
					this.waitForceAnimation = this.clipS_.clip_dart.length * 10f;
					this.cS_.ShowAddObject(5);
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
				}
				if (component2.isMinigolf)
				{
					this.PlayAnimation("golf");
					this.waitForceAnimation = this.clipS_.clip_Minigolf.length * 10f;
					this.cS_.ShowAddObject(10);
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
				}
				if (component2.isPiano)
				{
					this.PlayAnimation("piano");
					this.waitForceAnimation = this.clipS_.clip_piano.length * 2f;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
				}
				if (component2.isMedizinSchrank)
				{
					this.PlayAnimation("arztSchrank");
					this.waitForceAnimation = this.clipS_.clip_arztSchrank.length * 1f;
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(47, 0f, false, base.transform.position);
					}
					component2.gfxAnimation.Play("arztSchrank");
				}
				if (component2.isFreezer)
				{
					this.PlayAnimation("freezer");
					this.waitForceAnimation = this.clipS_.clip_freezer.length * 1f;
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(52, 0f, false, base.transform.position);
					}
					component2.gfxAnimation.Play("freezer1");
				}
				if (component2.isTV)
				{
					this.PlayAnimation("tv");
					this.waitForceAnimation = this.clipS_.clip_tv.length * 5f;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
					if (component2.gfxHide)
					{
						component2.gfxHide.SetActive(false);
					}
				}
				if (component2.isRadio)
				{
					this.PlayAnimation("dance");
					this.waitForceAnimation = this.clipS_.clip_dance.length * 5f;
					if (component2.gfxShow)
					{
						component2.gfxShow.SetActive(true);
					}
					if (component2.gfxHide)
					{
						component2.gfxHide.SetActive(false);
					}
				}
				if (component2.isGhostSink)
				{
					this.PlayAnimation("ghostSink");
					this.waitForceAnimation = this.clipS_.clip_ghostSink.length * 2f;
				}
				if (component2.isGhostMuelleimer)
				{
					this.PlayAnimation("muellFloor");
					this.waitForceAnimation = this.clipS_.clip_muellFloor.length;
					this.cS_.ShowAddObject(1);
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(18, 0.9f, true, base.transform.position);
					}
				}
				if (component2.isPlant)
				{
					this.PlayAnimation("waterCan");
					this.waitForceAnimation = this.clipS_.clip_waterCan.length;
					this.cS_.ShowAddObject(2);
					if (this.cS_.IsVisible())
					{
						this.sfx_.Play3DSound(26, 0f, true, base.transform.position);
					}
				}
				if (component2.isGhostPlant)
				{
					this.PlayAnimation("ghostPlant");
					this.waitForceAnimation = this.clipS_.clip_ghostPlant.length;
					this.cS_.ShowAddObject(2);
				}
				if (component2.isGhostWC)
				{
					this.PlayAnimation("ghostWC");
					this.waitForceAnimation = this.clipS_.clip_ghostWC.length * 2f;
				}
				if (component2.isGhostPause1)
				{
					this.PlayAnimation("pause1");
					this.waitForceAnimation = this.clipS_.clip_pause1.length * 3f;
				}
				if (component2.isGhostPause2)
				{
					this.PlayAnimation("pause2");
					this.waitForceAnimation = this.clipS_.clip_pause2.length * 1f;
				}
				if (component2.isGhostPause3)
				{
					this.PlayAnimation("pause3");
					this.waitForceAnimation = this.clipS_.clip_pause3.length * 5f;
					this.cS_.ShowAddObject(3);
				}
				if (component2.isGhostPause4)
				{
					this.PlayAnimation("pause4");
					this.waitForceAnimation = this.clipS_.clip_pause4.length * 5f;
					this.cS_.ShowAddObject(4);
				}
				if (component2.isSeat)
				{
					this.PlayAnimation("pauseSit1");
					this.waitForceAnimation = this.clipS_.clip_pauseSit1.length * 15f;
				}
				if (component2.isSeatAufenthalt)
				{
					this.PlayAnimation("motSit1");
					this.waitForceAnimation = this.clipS_.clip_motSit1.length * 15f;
				}
			}
			else if (this.waitOnFloor <= 0f)
			{
				this.waitOnFloor = UnityEngine.Random.Range(3f, 6f);
			}
			this.DeleteTarget();
			return;
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00016E50 File Offset: 0x00015050
	private void FindTarget()
	{
		if (this.cS_.picked)
		{
			return;
		}
		if (this.cS_.objectUsingID != -1)
		{
			return;
		}
		if (this.aStar.pathPending)
		{
			return;
		}
		if (this.waitOnFloor > 0f)
		{
			return;
		}
		if (!this.aStar.hasPath && this.myTarget)
		{
			objectScript component = this.myTarget.GetComponent<objectScript>();
			if (this.trysToFindPath > 10)
			{
				this.trysToFindPath = 0;
				if (component)
				{
					if (component.waypoint)
					{
						base.transform.position = component.waypoint.transform.position;
						return;
					}
				}
				else
				{
					base.transform.position = this.myTarget.transform.position;
				}
				return;
			}
			if (component && component.waypoint)
			{
				this.aStar.destination = component.waypoint.transform.position;
				this.aStar.SearchPath();
				this.trysToFindPath++;
				Debug.Log("Hat keinen Path, aber ein Target -> Also nochmal den Weg berechnen (mit Waypoint)");
				return;
			}
			this.aStar.destination = this.myTarget.transform.position;
			this.aStar.SearchPath();
			this.trysToFindPath++;
			Debug.Log("Hat keinen Path, aber ein Target -> Also nochmal den Weg berechnen (ohne Waypoint)");
			return;
		}
		else
		{
			if (this.myTarget)
			{
				return;
			}
			if (this.cS_.roomID <= -1)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Floor");
				if (array.Length != 0)
				{
					int i = 0;
					while (i < 10000)
					{
						i++;
						this.myTarget = array[UnityEngine.Random.Range(0, array.Length)];
						int num = Mathf.RoundToInt(this.myTarget.transform.position.x);
						int num2 = Mathf.RoundToInt(this.myTarget.transform.position.z);
						if (this.mapS_.mapRoomID[num, num2] == mapScript.ID_FLOOR && (this.mapS_.mapAusstattung[num, num2] > 0f || this.mapS_.mapWaerme[num, num2] > 0f))
						{
							this.aStar.destination = this.myTarget.transform.position;
							this.aStar.SearchPath();
							return;
						}
					}
					i = 0;
					while (i < 10000)
					{
						i++;
						this.myTarget = array[UnityEngine.Random.Range(0, array.Length)];
						int num3 = Mathf.RoundToInt(this.myTarget.transform.position.x);
						int num4 = Mathf.RoundToInt(this.myTarget.transform.position.z);
						if (this.mapS_.mapRoomID[num3, num4] == mapScript.ID_FLOOR)
						{
							this.aStar.destination = this.myTarget.transform.position;
							this.aStar.SearchPath();
							return;
						}
					}
					this.myTarget = array[UnityEngine.Random.Range(0, array.Length)];
					this.aStar.destination = this.myTarget.transform.position;
					this.aStar.SearchPath();
					return;
				}
			}
			else
			{
				if (this.cS_.roomS_ && this.FindObjectInRoom(0, null, false))
				{
					return;
				}
				this.FindRandomFloorInRoom();
			}
			return;
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000171B4 File Offset: 0x000153B4
	public bool FindObjectInRoom(int what, GameObject forceObject, bool onlyInRoom)
	{
		this.Init();
		objectScript objectScript = null;
		GameObject gameObject = null;
		List<GameObject> list = new List<GameObject>();
		if (!forceObject)
		{
			if (what == 0 && this.cS_.mainArbeitsplatzS_)
			{
				if (this.cS_.mainArbeitsplatzS_.besetztCharID == -1)
				{
					gameObject = this.cS_.mainArbeitsplatzS_.gameObject;
					objectScript = this.cS_.mainArbeitsplatzS_;
				}
				else
				{
					this.cS_.mainArbeitsplatzS_ = null;
				}
			}
			if (!gameObject && this.cS_.roomS_)
			{
				for (int i = 0; i < this.cS_.roomS_.listInventar.Count; i++)
				{
					if (this.cS_.roomS_.listInventar[i])
					{
						objectScript = this.cS_.roomS_.listInventar[i];
						if (!objectScript.picked && objectScript.gekauft)
						{
							if (what == 0 && objectScript.isArbeitsplatz && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 1 && objectScript.canDrink && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 2 && objectScript.isMuelleimer && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 3 && objectScript.isPlant && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 4 && objectScript.isWC && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 5 && objectScript.isSink && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 6 && objectScript.isHandtrockner && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 7 && objectScript.isSeat && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 8 && objectScript.isArcade && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 9 && objectScript.isSeatAufenthalt && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 10 && objectScript.isDart && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 11 && objectScript.isMedizinSchrank && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 12 && objectScript.isPiano && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 13 && objectScript.isFreezer && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 14 && objectScript.isTV && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 15 && objectScript.isRadio && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
							if (what == 16 && objectScript.isMinigolf && objectScript.IsUnbesetzt())
							{
								list.Add(this.cS_.roomS_.listInventar[i].gameObject);
							}
						}
					}
				}
			}
			int num = Mathf.RoundToInt(base.transform.position.x);
			int num2 = Mathf.RoundToInt(base.transform.position.z);
			if (!gameObject && this.mapS_.IsInMapLimit(num, num2))
			{
				roomScript roomScript = this.mapS_.mapRoomScript[num, num2];
				if (roomScript)
				{
					for (int j = 0; j < roomScript.listInventar.Count; j++)
					{
						if (roomScript.listInventar[j])
						{
							objectScript = roomScript.listInventar[j].GetComponent<objectScript>();
							if (!objectScript.picked && objectScript.gekauft)
							{
								if (what == 1 && objectScript.canDrink && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 2 && objectScript.isMuelleimer && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 3 && objectScript.isPlant && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 4 && objectScript.isWC && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 5 && objectScript.isSink && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 6 && objectScript.isHandtrockner && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 7 && objectScript.isSeat && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 8 && objectScript.isArcade && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 9 && objectScript.isSeatAufenthalt && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 10 && objectScript.isDart && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 11 && objectScript.isMedizinSchrank && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 12 && objectScript.isPiano && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 13 && objectScript.isFreezer && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 14 && objectScript.isTV && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 15 && objectScript.isRadio && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
								if (what == 10 && objectScript.isMinigolf && objectScript.IsUnbesetzt())
								{
									list.Add(roomScript.listInventar[j].gameObject);
								}
							}
						}
					}
				}
			}
			num = Mathf.RoundToInt(base.transform.position.x);
			num2 = Mathf.RoundToInt(base.transform.position.z);
			if (!onlyInRoom && this.mapS_.IsInMapLimit(num, num2) && list.Count <= 0 && !gameObject)
			{
				foreach (GameObject gameObject2 in this.mS_.arrayObjects)
				{
					if (gameObject2)
					{
						objectScript = gameObject2.GetComponent<objectScript>();
						if (!objectScript.picked && objectScript.gekauft)
						{
							int num3 = Mathf.RoundToInt(gameObject2.transform.position.x);
							int num4 = Mathf.RoundToInt(gameObject2.transform.position.z);
							if (this.mapS_.IsInMapLimit(num3, num4) && this.mapS_.mapBuilding[num, num2] == this.mapS_.mapBuilding[num3, num4])
							{
								if (what == 1 && objectScript.canDrink && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(gameObject2);
								}
								if (what == 2 && objectScript.isMuelleimer && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(gameObject2);
								}
								if (what == 3 && objectScript.isPlant && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 4 && objectScript.isWC && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 5 && objectScript.isSink && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 6 && objectScript.isHandtrockner && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 7 && objectScript.isSeat && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 8 && objectScript.isArcade && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 9 && objectScript.isSeatAufenthalt && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 10 && objectScript.isDart && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 11 && objectScript.isMedizinSchrank && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
								{
									list.Add(gameObject2);
								}
								if (what == 12 && objectScript.isPiano && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 13 && objectScript.isFreezer && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 14 && objectScript.isTV && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 15 && objectScript.isRadio && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
								if (what == 16 && objectScript.isMinigolf && objectScript.IsUnbesetzt())
								{
									list.Add(gameObject2);
								}
							}
						}
					}
				}
			}
			if (!onlyInRoom && !this.mS_.personal_dontLeaveBuilding && list.Count <= 0 && !gameObject)
			{
				foreach (GameObject gameObject3 in this.mS_.arrayObjects)
				{
					if (gameObject3)
					{
						objectScript = gameObject3.GetComponent<objectScript>();
						if (!objectScript.picked && objectScript.gekauft)
						{
							if (what == 1 && objectScript.canDrink && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(gameObject3);
							}
							if (what == 2 && objectScript.isMuelleimer && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(gameObject3);
							}
							if (what == 3 && objectScript.isPlant && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 4 && objectScript.isWC && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 5 && objectScript.isSink && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 6 && objectScript.isHandtrockner && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 7 && objectScript.isSeat && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 8 && objectScript.isArcade && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 9 && objectScript.isSeatAufenthalt && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 10 && objectScript.isDart && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 11 && objectScript.isMedizinSchrank && objectScript.IsUnbesetzt() && objectScript.aufladungenAkt > 0)
							{
								list.Add(gameObject3);
							}
							if (what == 12 && objectScript.isPiano && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 13 && objectScript.isFreezer && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 14 && objectScript.isTV && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 15 && objectScript.isRadio && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
							if (what == 16 && objectScript.isMinigolf && objectScript.IsUnbesetzt())
							{
								list.Add(gameObject3);
							}
						}
					}
				}
			}
			if (list.Count > 0)
			{
				gameObject = this.ShortestWay(list);
				objectScript = gameObject.GetComponent<objectScript>();
				if (what == 0)
				{
					this.cS_.mainArbeitsplatzS_ = objectScript;
				}
			}
		}
		else
		{
			gameObject = forceObject;
			objectScript = forceObject.GetComponent<objectScript>();
		}
		if (gameObject)
		{
			this.cS_.RemoveObjectUsing();
			this.cS_.objectBelegtID = objectScript.myID;
			this.cS_.objectBelegtS_ = objectScript;
			objectScript.SetBesetzt(this.cS_.myID);
			this.myTarget = gameObject;
			this.aStar.destination = objectScript.waypoint.transform.position;
			this.aStar.SearchPath();
			return true;
		}
		return false;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00018054 File Offset: 0x00016254
	public void FindRandomFloorInRoom()
	{
		if (!this.cS_.roomS_)
		{
			return;
		}
		if (this.cS_.roomS_.listGameObjects.Count <= 0)
		{
			return;
		}
		int index = UnityEngine.Random.Range(0, this.cS_.roomS_.listGameObjects.Count);
		if (this.cS_.roomS_.listGameObjects[index])
		{
			this.cS_.RemoveObjectUsing();
			this.myTarget = this.cS_.roomS_.listGameObjects[index];
			this.aStar.destination = this.myTarget.transform.position;
			this.aStar.SearchPath();
			return;
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00018118 File Offset: 0x00016318
	private GameObject ShortestWay(List<GameObject> objects)
	{
		int index = 0;
		float num = 9999999f;
		for (int i = 0; i < objects.Count; i++)
		{
			float num2 = Vector3.Distance(base.transform.position, objects[i].transform.position);
			if (num2 < num)
			{
				num = num2;
				index = i;
			}
		}
		return objects[index];
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0001816F File Offset: 0x0001636F
	public void RecalculatePath()
	{
		this.aStar.SearchPath();
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0001817C File Offset: 0x0001637C
	public int GetPosition_RoomID()
	{
		return this.mapS_.mapRoomID[Mathf.RoundToInt(base.transform.position.x), Mathf.RoundToInt(base.transform.position.z)];
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000181B8 File Offset: 0x000163B8
	public int GetTargetPosition_RoomID()
	{
		if (!this.myTarget)
		{
			return -1;
		}
		return this.mapS_.mapRoomID[Mathf.RoundToInt(this.myTarget.transform.position.x), Mathf.RoundToInt(this.myTarget.transform.position.z)];
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00018218 File Offset: 0x00016418
	public void DeleteTarget()
	{
		this.myTarget = null;
	}

	// Token: 0x04000358 RID: 856
	public GameObject myTarget;

	// Token: 0x04000359 RID: 857
	private GameObject main_;

	// Token: 0x0400035A RID: 858
	private mainScript mS_;

	// Token: 0x0400035B RID: 859
	private GameObject charGFX;

	// Token: 0x0400035C RID: 860
	private characterScript cS_;

	// Token: 0x0400035D RID: 861
	private sfxScript sfx_;

	// Token: 0x0400035E RID: 862
	private clipScript clipS_;

	// Token: 0x0400035F RID: 863
	private mapScript mapS_;

	// Token: 0x04000360 RID: 864
	private IAstarAI aStar;

	// Token: 0x04000361 RID: 865
	public Animator charAnimation;

	// Token: 0x04000362 RID: 866
	public string currentAnimation;

	// Token: 0x04000363 RID: 867
	private int audioTyping = -1;

	// Token: 0x04000364 RID: 868
	private Seeker seeker;

	// Token: 0x04000365 RID: 869
	public float waitForceAnimation;

	// Token: 0x04000366 RID: 870
	private float randomDeskEmoteTime;

	// Token: 0x04000367 RID: 871
	private float waitOnFloor;

	// Token: 0x04000368 RID: 872
	private bool charArbeitsmarkt;

	// Token: 0x04000369 RID: 873
	private float walkDistance;

	// Token: 0x0400036A RID: 874
	public int trysToFindPath;
}
