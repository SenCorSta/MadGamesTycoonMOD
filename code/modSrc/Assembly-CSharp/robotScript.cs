using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class robotScript : MonoBehaviour
{
	// Token: 0x06001AC2 RID: 6850 RVA: 0x0010D276 File Offset: 0x0010B476
	private void Start()
	{
		this.FindScripts();
		this.InitPathfinding();
		this.myObjects[1].GetComponent<ParticleSystem>().enableEmission = false;
		this.myObjects[1].GetComponent<AudioSource>().Stop();
		this.mS_.findRobots = true;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x0010D2B5 File Offset: 0x0010B4B5
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findRobots = true;
		}
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0010D2D0 File Offset: 0x0010B4D0
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

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0010D388 File Offset: 0x0010B588
	private void InitPathfinding()
	{
		this.aStar = base.GetComponent<IAstarAI>();
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x0010D396 File Offset: 0x0010B596
	private void Update()
	{
		this.FindTarget();
		this.Move();
		this.TargetReached();
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0010D3AA File Offset: 0x0010B5AA
	public void RecalculatePath()
	{
		this.aStar.SearchPath();
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0010D3B8 File Offset: 0x0010B5B8
	private void Move()
	{
		float num = 1.2f;
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		Vector3 nextPosition;
		Quaternion nextRotation;
		this.aStar.MovementUpdate(this.mS_.GetDeltaTime() * num, out nextPosition, out nextRotation);
		this.aStar.FinalizeMovement(nextPosition, nextRotation);
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x0010D42C File Offset: 0x0010B62C
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

	// Token: 0x06001ACA RID: 6858 RVA: 0x0010D7BE File Offset: 0x0010B9BE
	public GameObject GetLadestation()
	{
		if (this.ladestation)
		{
			return this.ladestation;
		}
		this.ladestation = GameObject.Find("O_" + this.stationID.ToString());
		return this.ladestation;
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x0010D7FC File Offset: 0x0010B9FC
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

	// Token: 0x06001ACC RID: 6860 RVA: 0x0010D95E File Offset: 0x0010BB5E
	public int GetPosition_RoomID()
	{
		return this.mapS_.mapRoomID[Mathf.RoundToInt(base.transform.position.x), Mathf.RoundToInt(base.transform.position.z)];
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0010D99C File Offset: 0x0010BB9C
	public int GetTargetPosition_RoomID()
	{
		if (!this.myTarget)
		{
			return -1;
		}
		return this.mapS_.mapRoomID[Mathf.RoundToInt(this.myTarget.transform.position.x), Mathf.RoundToInt(this.myTarget.transform.position.z)];
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0010D9FC File Offset: 0x0010BBFC
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

	// Token: 0x040021E2 RID: 8674
	public int stationID = -1;

	// Token: 0x040021E3 RID: 8675
	public GameObject ladestation;

	// Token: 0x040021E4 RID: 8676
	private GameObject main_;

	// Token: 0x040021E5 RID: 8677
	private mainScript mS_;

	// Token: 0x040021E6 RID: 8678
	private mapScript mapS_;

	// Token: 0x040021E7 RID: 8679
	private IAstarAI aStar;

	// Token: 0x040021E8 RID: 8680
	private Seeker seeker;

	// Token: 0x040021E9 RID: 8681
	public GameObject myTarget;

	// Token: 0x040021EA RID: 8682
	private Animation cleanAnimation;

	// Token: 0x040021EB RID: 8683
	public GameObject[] myObjects;

	// Token: 0x040021EC RID: 8684
	private AudioSource sound;

	// Token: 0x040021ED RID: 8685
	private bool isAufLadestation = true;

	// Token: 0x040021EE RID: 8686
	private float findTimer;

	// Token: 0x040021EF RID: 8687
	private int trysToFindPath;
}
