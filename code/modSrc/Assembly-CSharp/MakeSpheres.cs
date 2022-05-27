using System;
using UnityEngine;


public class MakeSpheres : MonoBehaviour
{
	
	private void Start()
	{
		for (int i = 0; i < this.numberOfSpheres; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.spherePrefab, new Vector3(UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area)), UnityEngine.Random.rotation);
		}
	}

	
	public GameObject spherePrefab;

	
	public int numberOfSpheres = 12;

	
	public float area = 4.5f;
}
