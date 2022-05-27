using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class doorScript : MonoBehaviour
{
	// Token: 0x06001A60 RID: 6752 RVA: 0x0010A966 File Offset: 0x00108B66
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x0010A974 File Offset: 0x00108B74
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.FindWithTag("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.mapS_ = this.main_.GetComponent<mapScript>();
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x0010A9C4 File Offset: 0x00108BC4
	private void Init()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		this.roomID = this.mapS_.mapRoomID[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x0010AA24 File Offset: 0x00108C24
	private void Update()
	{
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.myAnim["doorOpen"].speed = this.mS_.GetGameSpeed();
			this.myAnim["doorClose"].speed = this.mS_.GetGameSpeed();
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.1f)
		{
			return;
		}
		this.updateTimer = 0f;
		for (int i = 0; i < this.mS_.arrayRobots.Length; i++)
		{
			if (this.mS_.arrayRobots[i] && Vector3.Distance(base.transform.position, this.mS_.arrayRobots[i].transform.position) < 2f)
			{
				robotScript component = this.mS_.arrayRobots[i].GetComponent<robotScript>();
				if (component && component.myTarget)
				{
					if (this.isOpen)
					{
						return;
					}
					bool flag = false;
					if (!this.buildingDoor && ((component.GetPosition_RoomID() == this.roomID && component.GetTargetPosition_RoomID() != this.roomID) || (component.GetPosition_RoomID() != this.roomID && component.GetTargetPosition_RoomID() == this.roomID)))
					{
						flag = true;
					}
					if (flag || this.buildingDoor)
					{
						if (!this.isOpen && !this.myAnim.isPlaying)
						{
							this.isOpen = true;
							this.myAnim.Play("doorOpen");
						}
						return;
					}
				}
			}
		}
		for (int j = 0; j < this.mS_.arrayCharactersForDoors.Count; j++)
		{
			if (this.mS_.arrayCharactersForDoors[j] && Vector3.Distance(base.transform.position, this.mS_.arrayCharactersForDoors[j].transform.position) < 2f)
			{
				characterScript component2 = this.mS_.arrayCharactersForDoors[j].GetComponent<characterScript>();
				if (component2 && component2.moveS_ && component2.moveS_.myTarget && !component2.picked)
				{
					if (this.isOpen)
					{
						return;
					}
					bool flag2 = false;
					if (!this.buildingDoor && ((component2.moveS_.GetPosition_RoomID() == this.roomID && component2.moveS_.GetTargetPosition_RoomID() != this.roomID) || (component2.moveS_.GetPosition_RoomID() != this.roomID && component2.moveS_.GetTargetPosition_RoomID() == this.roomID)))
					{
						flag2 = true;
					}
					if (flag2 || this.buildingDoor)
					{
						if (!this.isOpen && !this.myAnim.isPlaying)
						{
							this.isOpen = true;
							this.myAnim.Play("doorOpen");
						}
						return;
					}
				}
			}
		}
		if (this.isOpen && !this.myAnim.isPlaying)
		{
			this.isOpen = false;
			this.myAnim.Play("doorClose");
		}
	}

	// Token: 0x04002161 RID: 8545
	private GameObject main_;

	// Token: 0x04002162 RID: 8546
	private mainScript mS_;

	// Token: 0x04002163 RID: 8547
	private mapScript mapS_;

	// Token: 0x04002164 RID: 8548
	public Animation myAnim;

	// Token: 0x04002165 RID: 8549
	public int roomID = -1;

	// Token: 0x04002166 RID: 8550
	private bool isOpen;

	// Token: 0x04002167 RID: 8551
	private float oldGamespeed;

	// Token: 0x04002168 RID: 8552
	public bool buildingDoor;

	// Token: 0x04002169 RID: 8553
	private float updateTimer;
}
