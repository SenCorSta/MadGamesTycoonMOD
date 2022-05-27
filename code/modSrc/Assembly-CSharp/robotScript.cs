﻿using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class robotScript : MonoBehaviour
{
	// Token: 0x06001A78 RID: 6776 RVA: 0x00011C83 File Offset: 0x0000FE83
	private void Start()
	{
		this.FindScripts();
		this.InitPathfinding();
		this.myObjects[1].GetComponent<ParticleSystem>().enableEmission = false;
		this.myObjects[1].GetComponent<AudioSource>().Stop();
		this.mS_.findRobots = true;
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x00011CC2 File Offset: 0x0000FEC2
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findRobots = true;
		}
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x00110FB0 File Offset: 0x0010F1B0
	private void FindScripts()
	{
		if (!this.sound)
		{
			this.sound = base.GetComponent<AudioSource>();
		}
		if (!this.cleanAnimation)
		{
			this.cleanAnimation = this.myObjects[0].GetComponent<Animation>();
		}
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
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

	// Token: 0x06001A7B RID: 6779 RVA: 0x00011CDD File Offset: 0x0000FEDD
	private void InitPathfinding()
	{
		this.aStar = base.GetComponent<IAstarAI>();
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x00011CEB File Offset: 0x0000FEEB
	private void Update()
	{
		this.FindTarget();
		this.Move();
		this.TargetReached();
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x00011CFF File Offset: 0x0000FEFF
	public void RecalculatePath()
	{
		this.aStar.SearchPath();
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x00111068 File Offset: 0x0010F268
	private void Move()
	{
		float num = 1.2f;
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		Vector3 nextPosition;
		Quaternion nextRotation;
		this.aStar.MovementUpdate(this.mS_.GetDeltaTime() * num, out nextPosition, out nextRotation);
		this.aStar.FinalizeMovement(nextPosition, nextRotation);
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x001110DC File Offset: 0x0010F2DC
	private void FindTarget()
	{
		this.findTimer += Time.deltaTime;
		if (this.findTimer < 1f)
		{
			return;
		}
		this.findTimer = 0f;
		if (this.cleanAnimation.isPlaying)
		{
			return;
		}
		if (this.aStar.pathPending)
		{
			return;
		}
		if (!this.aStar.hasPath && this.myTarget)
		{
			if (this.trysToFindPath > 10)
			{
				this.trysToFindPath = 0;
				base.transform.position = this.myTarget.transform.position;
				return;
			}
			this.aStar.destination = this.myTarget.transform.position;
			this.aStar.SearchPath();
			this.trysToFindPath++;
			return;
		}
		else
		{
			if (this.myTarget)
			{
				return;
			}
			if (!this.ladestation)
			{
				this.GetLadestation();
			}
			if (this.mS_.arrayMuell.Length != 0 && this.ladestation)
			{
				int num = Mathf.RoundToInt(this.ladestation.transform.position.x);
				int num2 = Mathf.RoundToInt(this.ladestation.transform.position.z);
				float num3 = 100000000f;
				int num4 = -1;
				for (int i = 0; i < this.mS_.arrayMuell.Length; i++)
				{
					if (this.mS_.arrayMuell[i] && this.mS_.arrayMuell[i].tag == "Muell")
					{
						int num5 = Mathf.RoundToInt(this.mS_.arrayMuell[i].transform.position.x);
						int num6 = Mathf.RoundToInt(this.mS_.arrayMuell[i].transform.position.z);
						if (this.mapS_.IsInMapLimit(num5, num6) && (this.mapS_.mapBuilding[num, num2] == this.mapS_.mapBuilding[num5, num6] || !this.mS_.personal_RobotDontLeaveBuilding))
						{
							float num7 = Vector3.Distance(base.transform.position, this.mS_.arrayMuell[i].transform.position);
							if (num3 > num7 && !this.IsAnotherRobotNaeher(num7, i, this.mapS_.mapBuilding[num5, num6]))
							{
								num3 = num7;
								num4 = i;
							}
						}
					}
				}
				if (num4 != -1)
				{
					this.myTarget = this.mS_.arrayMuell[num4];
					this.myTarget.tag = "Muell_InUse";
					this.aStar.destination = this.myTarget.transform.position;
					this.aStar.SearchPath();
					this.myObjects[1].GetComponent<ParticleSystem>().enableEmission = false;
					this.myObjects[1].GetComponent<AudioSource>().Stop();
					this.sound.Play();
					this.isAufLadestation = false;
					return;
				}
			}
			if (!this.isAufLadestation)
			{
				this.myTarget = this.GetLadestation();
				if (this.ladestation)
				{
					this.aStar.destination = this.myTarget.transform.position;
					this.aStar.SearchPath();
					this.myObjects[1].GetComponent<ParticleSystem>().enableEmission = false;
					this.myObjects[1].GetComponent<AudioSource>().Stop();
					this.sound.Play();
				}
			}
			return;
		}
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x00011D0C File Offset: 0x0000FF0C
	public GameObject GetLadestation()
	{
		if (this.ladestation)
		{
			return this.ladestation;
		}
		this.ladestation = GameObject.Find("O_" + this.stationID.ToString());
		return this.ladestation;
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x00111470 File Offset: 0x0010F670
	private void TargetReached()
	{
		if (!this.myTarget)
		{
			return;
		}
		if (this.aStar.pathPending)
		{
			return;
		}
		if ((double)Vector3.Distance(base.transform.position, this.myTarget.transform.position) < 0.2 || (double)this.aStar.remainingDistance < 0.2 || this.aStar.reachedEndOfPath)
		{
			if (this.myTarget.tag == "Muell_InUse")
			{
				this.cleanAnimation.Play();
				this.myObjects[1].GetComponent<ParticleSystem>().enableEmission = true;
				this.myObjects[1].GetComponent<AudioSource>().Play();
				UnityEngine.Object.Destroy(this.myTarget);
				this.myTarget = null;
				return;
			}
			this.myTarget = null;
			this.isAufLadestation = true;
			if (this.ladestation)
			{
				base.transform.position = this.ladestation.transform.position;
				base.transform.eulerAngles = new Vector3(this.ladestation.transform.eulerAngles.x, this.ladestation.transform.eulerAngles.y, this.ladestation.transform.eulerAngles.z + 180f);
			}
		}
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x00011D48 File Offset: 0x0000FF48
	public int GetPosition_RoomID()
	{
		return this.mapS_.mapRoomID[Mathf.RoundToInt(base.transform.position.x), Mathf.RoundToInt(base.transform.position.z)];
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x001115D4 File Offset: 0x0010F7D4
	public int GetTargetPosition_RoomID()
	{
		if (!this.myTarget)
		{
			return -1;
		}
		return this.mapS_.mapRoomID[Mathf.RoundToInt(this.myTarget.transform.position.x), Mathf.RoundToInt(this.myTarget.transform.position.z)];
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x00111634 File Offset: 0x0010F834
	private bool IsAnotherRobotNaeher(float myDist, int slotMuell, int buildingID)
	{
		for (int i = 0; i < this.mS_.arrayRobots.Length; i++)
		{
			if (this.mS_.arrayRobots[i] && this.mS_.arrayRobots[i] != base.gameObject)
			{
				robotScript component = this.mS_.arrayRobots[i].GetComponent<robotScript>();
				if (!component.ladestation)
				{
					component.GetLadestation();
				}
				int num = 0;
				int num2 = 0;
				if (component.ladestation)
				{
					num = Mathf.RoundToInt(component.ladestation.transform.position.x);
					num2 = Mathf.RoundToInt(component.ladestation.transform.position.z);
				}
				if ((this.mapS_.mapBuilding[num, num2] == buildingID || !this.mS_.personal_RobotDontLeaveBuilding) && Vector3.Distance(this.mS_.arrayRobots[i].transform.position, this.mS_.arrayMuell[slotMuell].transform.position) < myDist)
				{
					robotScript component2 = this.mS_.arrayRobots[i].GetComponent<robotScript>();
					if (component2 && !component2.myTarget)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x040021C8 RID: 8648
	public int stationID = -1;

	// Token: 0x040021C9 RID: 8649
	public GameObject ladestation;

	// Token: 0x040021CA RID: 8650
	private GameObject main_;

	// Token: 0x040021CB RID: 8651
	private mainScript mS_;

	// Token: 0x040021CC RID: 8652
	private mapScript mapS_;

	// Token: 0x040021CD RID: 8653
	private IAstarAI aStar;

	// Token: 0x040021CE RID: 8654
	private Seeker seeker;

	// Token: 0x040021CF RID: 8655
	public GameObject myTarget;

	// Token: 0x040021D0 RID: 8656
	private Animation cleanAnimation;

	// Token: 0x040021D1 RID: 8657
	public GameObject[] myObjects;

	// Token: 0x040021D2 RID: 8658
	private AudioSource sound;

	// Token: 0x040021D3 RID: 8659
	private bool isAufLadestation = true;

	// Token: 0x040021D4 RID: 8660
	private float findTimer;

	// Token: 0x040021D5 RID: 8661
	private int trysToFindPath;
}
