using System;
using UnityEngine;


public class golfScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if ((double)this.timer >= 1.417)
		{
			this.timer = 0f;
			if (this.myBall)
			{
				UnityEngine.Object.Destroy(this.myBall);
			}
			this.myBall = UnityEngine.Object.Instantiate<GameObject>(this.prefabFlyingBall);
			this.myBall.transform.position = base.gameObject.transform.position;
			this.myBall.transform.rotation = base.gameObject.transform.root.transform.rotation;
			this.myBall.transform.eulerAngles = new Vector3(this.myBall.transform.eulerAngles.x + (float)UnityEngine.Random.Range(-15, 15), this.myBall.transform.eulerAngles.y + (float)UnityEngine.Random.Range(-30, -15), this.myBall.transform.eulerAngles.z);
		}
	}

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private sfxScript sfx_;

	
	public GameObject prefabFlyingBall;

	
	private GameObject myBall;

	
	public float timer;
}
