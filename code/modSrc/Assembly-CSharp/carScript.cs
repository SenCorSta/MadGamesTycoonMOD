using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class carScript : MonoBehaviour
{
	// Token: 0x06001A36 RID: 6710 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x0010A060 File Offset: 0x00108260
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

	// Token: 0x0400213B RID: 8507
	public GameObject[] prefabCars;

	// Token: 0x0400213C RID: 8508
	public AnimationClip carAnimation;

	// Token: 0x0400213D RID: 8509
	private GameObject aktuellesCar;
}
