using System;
using UnityEngine;


public class serverLamp : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.FindRenderer();
	}

	
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = GameObject.FindWithTag("Main").GetComponent<mainScript>();
		}
		if (!this.oS_ && this.mS_.multiplayer && !this.oS_)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
	}

	
	private void FindRenderer()
	{
		this.goLamps_Renderer = new Renderer[this.goLamps.Length];
		for (int i = 0; i < this.goLamps.Length; i++)
		{
			if (this.goLamps[i])
			{
				this.goLamps_Renderer[i] = this.goLamps[i].GetComponent<Renderer>();
			}
		}
	}

	
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if (this.timer < 0.1f)
		{
			return;
		}
		this.timer = 0f;
		if (this.goLamps_Renderer[0] && !this.goLamps_Renderer[0].isVisible)
		{
			return;
		}
		this.FindRoomScript();
		if (this.rS_)
		{
			if (this.rS_.serverDown)
			{
				for (int i = 0; i < this.goLamps.Length; i++)
				{
					if (this.goLamps[i] && this.goLamps_Renderer[i])
					{
						this.goLamps_Renderer[i].material = this.materials[1];
					}
				}
				return;
			}
			for (int j = 0; j < this.goLamps.Length; j++)
			{
				if (this.goLamps[j] && UnityEngine.Random.Range(0, 100) > 80 && this.goLamps_Renderer[j])
				{
					this.goLamps_Renderer[j].material = this.materials[UnityEngine.Random.Range(0, this.materials.Length)];
				}
			}
		}
	}

	
	private void FindRoomScript()
	{
		if (this.rS_)
		{
			return;
		}
		if (!this.mS_)
		{
			return;
		}
		if (!this.mS_.mapScript_)
		{
			return;
		}
		int num = Mathf.RoundToInt(this.oS_.gameObject.transform.position.x);
		int num2 = Mathf.RoundToInt(this.oS_.gameObject.transform.position.z);
		roomScript exists = this.mS_.mapScript_.mapRoomScript[num, num2];
		if (exists)
		{
			this.rS_ = exists;
		}
	}

	
	public objectScript oS_;

	
	private mainScript mS_;

	
	private roomScript rS_;

	
	public GameObject[] goLamps;

	
	private Renderer[] goLamps_Renderer;

	
	public Material[] materials;

	
	private float timer;
}
