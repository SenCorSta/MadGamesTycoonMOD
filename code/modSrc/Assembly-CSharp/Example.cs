using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class Example : MonoBehaviour
{
	// Token: 0x0600009C RID: 156 RVA: 0x0001BA28 File Offset: 0x00019C28
	public void Update()
	{
		base.transform.parent.eulerAngles = new Vector3(base.transform.parent.eulerAngles.x, base.transform.parent.eulerAngles.y + Time.deltaTime * 10f, base.transform.parent.eulerAngles.z);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000026BB File Offset: 0x000008BB
	public void generateSphereOnLeft()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(-11.09958f, 16f, 3.5f);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000026E6 File Offset: 0x000008E6
	public void generateSphereInCentre()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(UnityEngine.Random.Range(-0.01f, 0.01f), 16f, UnityEngine.Random.Range(-0.01f, 0.01f));
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00002725 File Offset: 0x00000925
	public void generateSphereOnRight()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(11.09958f, 16f, -3.5f);
	}

	// Token: 0x040000C2 RID: 194
	public GameObject sphere;
}
