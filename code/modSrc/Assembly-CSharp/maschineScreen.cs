using System;
using UnityEngine;


public class maschineScreen : MonoBehaviour
{
	
	private void Start()
	{
		this.mS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<mainScript>();
	}

	
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if (this.timer > this.rnd)
		{
			this.timer = 0f;
			this.rnd = UnityEngine.Random.Range(0.1f, 1.5f);
			this.renderer.material = this.mat[UnityEngine.Random.Range(1, this.mat.Length)];
		}
	}

	
	public MeshRenderer renderer;

	
	public Material[] mat;

	
	private float timer;

	
	private float rnd;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private mainScript mS_;

	
	private objectScript oS_;
}
