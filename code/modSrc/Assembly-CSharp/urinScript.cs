using System;
using UnityEngine;


public class urinScript : MonoBehaviour
{
	
	private void Start()
	{
		this.myGFX.GetComponent<MeshRenderer>().material = this.myMaterial[UnityEngine.Random.Range(0, this.myMaterial.Length)];
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 360f), base.transform.eulerAngles.z);
	}

	
	public Material[] myMaterial;

	
	public GameObject myGFX;
}
