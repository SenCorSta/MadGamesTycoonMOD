using System;
using UnityEngine;


public class computerScreenScript : MonoBehaviour
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
		if (!this.force)
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
			if (this.roomS_.taskID == -1 || !this.oS_.inUse)
			{
				this.renderer.material = this.mat[0];
				return;
			}
			this.timer += this.mS_.GetDeltaTime();
		}
		else
		{
			this.timer += Time.deltaTime;
		}
		if (this.timer > this.rnd)
		{
			this.timer = 0f;
			this.rnd = UnityEngine.Random.Range(0.5f, 1.5f);
			this.renderer.material = this.mat[UnityEngine.Random.Range(1, this.mat.Length)];
		}
	}

	
	public MeshRenderer renderer;

	
	public Material[] mat;

	
	public bool force;

	
	private float timer;

	
	private float rnd;

	
	private GameObject main_;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private mainScript mS_;

	
	private objectScript oS_;
}
