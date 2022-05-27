using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class Example : MonoBehaviour
{
	// Token: 0x0600009C RID: 156 RVA: 0x00005310 File Offset: 0x00003510
	public void Update()
	{
		base.transform.parent.eulerAngles = new Vector3(base.transform.parent.eulerAngles.x, base.transform.parent.eulerAngles.y + Time.deltaTime * 10f, base.transform.parent.eulerAngles.z);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000537D File Offset: 0x0000357D
	public void generateSphereOnLeft()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(-11.09958f, 16f, 3.5f);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000053A8 File Offset: 0x000035A8
	public void generateSphereInCentre()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(UnityEngine.Random.Range(-0.01f, 0.01f), 16f, UnityEngine.Random.Range(-0.01f, 0.01f));
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000053E7 File Offset: 0x000035E7
	public void generateSphereOnRight()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.sphere).transform.position = new Vector3(11.09958f, 16f, -3.5f);
	}

	// Token: 0x040000C2 RID: 194
	public GameObject sphere;
}
