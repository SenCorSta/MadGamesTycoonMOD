using System;
using UnityEngine;


public class carScript : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		if (!this.aktuellesCar)
		{
			this.aktuellesCar = UnityEngine.Object.Instantiate<GameObject>(this.prefabCars[UnityEngine.Random.Range(0, this.prefabCars.Length)]);
			this.aktuellesCar.GetComponent<Animation>().AddClip(this.carAnimation, "myAnim");
			this.aktuellesCar.GetComponent<Animation>().Play("myAnim");
			return;
		}
		if (!this.aktuellesCar.GetComponent<Animation>().isPlaying)
		{
			UnityEngine.Object.Destroy(this.aktuellesCar);
		}
	}

	
	public GameObject[] prefabCars;

	
	public AnimationClip carAnimation;

	
	private GameObject aktuellesCar;
}
