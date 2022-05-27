using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class doorScript : MonoBehaviour
{
	// Token: 0x06001A16 RID: 6678 RVA: 0x0001198E File Offset: 0x0000FB8E
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x0010E97C File Offset: 0x0010CB7C
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

	// Token: 0x06001A18 RID: 6680 RVA: 0x0010E9CC File Offset: 0x0010CBCC
	private void Init()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		this.roomID = this.mapS_.mapRoomID[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x0010EA2C File Offset: 0x0010CC2C
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

	// Token: 0x04002147 RID: 8519
	private GameObject main_;

	// Token: 0x04002148 RID: 8520
	private mainScript mS_;

	// Token: 0x04002149 RID: 8521
	private mapScript mapS_;

	// Token: 0x0400214A RID: 8522
	public Animation myAnim;

	// Token: 0x0400214B RID: 8523
	public int roomID = -1;

	// Token: 0x0400214C RID: 8524
	private bool isOpen;

	// Token: 0x0400214D RID: 8525
	private float oldGamespeed;

	// Token: 0x0400214E RID: 8526
	public bool buildingDoor;

	// Token: 0x0400214F RID: 8527
	private float updateTimer;
}
