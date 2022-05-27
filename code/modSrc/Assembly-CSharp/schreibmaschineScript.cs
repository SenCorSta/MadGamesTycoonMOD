using System;
using UnityEngine;


public class schreibmaschineScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.oS_)
		{
			this.oS_ = base.transform.root.gameObject.GetComponent<objectScript>();
			if (this.mS_.multiplayer && !this.oS_)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
		}
	}

	
	private void Update()
	{
		if (!this.oS_)
		{
			this.FindScripts();
			return;
		}
		if (this.oS_.picked)
		{
			return;
		}
		if (!this.renderer.isVisible)
		{
			return;
		}
		this.roomS_ = this.mapS_.mapRoomScript[Mathf.RoundToInt(base.transform.root.transform.position.x), Mathf.RoundToInt(base.transform.root.transform.position.z)];
		if (!this.roomS_)
		{
			return;
		}
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.myAnimation["schreibmaschine1"].speed = this.mS_.GetGameSpeed();
		}
		if (this.roomS_.taskID == -1 || !this.oS_.inUse)
		{
			if (this.myAnimation.isPlaying)
			{
				this.myAnimation.Stop();
				return;
			}
		}
		else if (!this.myAnimation.isPlaying)
		{
			this.myAnimation.Play();
		}
	}

	
	public Animation myAnimation;

	
	private objectScript oS_;

	
	public MeshRenderer renderer;

	
	private GameObject main_;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private mainScript mS_;

	
	private float oldGamespeed;
}
