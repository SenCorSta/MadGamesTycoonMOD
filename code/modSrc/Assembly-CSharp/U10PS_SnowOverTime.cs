using System;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class U10PS_SnowOverTime : MonoBehaviour
{
	
	private void Start()
	{
		this.meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
		this.totalTime = 1f / this.speed * 4.71f;
	}

	
	private void Update()
	{
		Material[] materials = this.meshRenderer.materials;
		materials[0].SetFloat("_SnowAmount", (Mathf.Sin(this.totalTime * this.speed) + 1f) / 2f);
		this.totalTime += Time.deltaTime;
		this.meshRenderer.materials = materials;
	}

	
	private MeshRenderer meshRenderer;

	
	public float speed = 0.6f;

	
	private float totalTime;
}
