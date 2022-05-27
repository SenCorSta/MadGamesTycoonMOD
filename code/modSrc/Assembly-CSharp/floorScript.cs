using System;
using UnityEngine;


public class floorScript : MonoBehaviour
{
	
	public void SetFilterTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[1];
	}

	
	public void SetStandardTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[0];
	}

	
	public GameObject myObject;

	
	public Material[] materials;
}
