using System;
using UnityEngine;


public class animateMaterial : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.myRenderer = base.GetComponent<MeshRenderer>();
	}

	
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
	}

	
	private void Update()
	{
		this.timer += this.speed * this.mS_.GetDeltaTime();
		if ((double)this.timer > 1.0)
		{
			this.timer = 0f;
			this.aktFrame++;
			if (this.aktFrame >= this.frames.Length)
			{
				this.aktFrame = 0;
			}
			this.myRenderer.material = this.frames[this.aktFrame];
		}
	}

	
	private GameObject main_;

	
	private mainScript mS_;

	
	public Material[] frames;

	
	public float speed = 1f;

	
	private MeshRenderer myRenderer;

	
	private float timer;

	
	private int aktFrame;
}
