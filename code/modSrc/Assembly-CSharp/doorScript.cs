using System;
using UnityEngine;


public class doorScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	
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

	
	private void Init()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		this.roomID = this.mapS_.mapRoomID[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
	}

	
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

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private mapScript mapS_;

	
	public Animation myAnim;

	
	public int roomID = -1;

	
	private bool isOpen;

	
	private float oldGamespeed;

	
	public bool buildingDoor;

	
	private float updateTimer;
}
