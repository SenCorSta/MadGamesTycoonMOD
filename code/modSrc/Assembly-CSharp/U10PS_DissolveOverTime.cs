using System;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
	
	private void Start()
	{
		this.meshRenderer = base.GetComponent<MeshRenderer>();
	}

	
	private void Update()
	{
		Material[] materials = this.meshRenderer.materials;
		materials[0].SetFloat("_Cutoff", Mathf.Sin(this.t * this.speed));
		this.t += Time.deltaTime;
		this.meshRenderer.materials = materials;
	}

	
	private MeshRenderer meshRenderer;

	
	public float speed = 0.5f;

	
	private float t;
}
