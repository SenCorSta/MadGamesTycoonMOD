using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class carScript : MonoBehaviour
{
	// Token: 0x060019EC RID: 6636 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x0010E244 File Offset: 0x0010C444
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

	// Token: 0x04002121 RID: 8481
	public GameObject[] prefabCars;

	// Token: 0x04002122 RID: 8482
	public AnimationClip carAnimation;

	// Token: 0x04002123 RID: 8483
	private GameObject aktuellesCar;
}
