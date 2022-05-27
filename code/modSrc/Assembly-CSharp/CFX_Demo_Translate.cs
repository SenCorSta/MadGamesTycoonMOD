using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CFX_Demo_Translate : MonoBehaviour
{
	// Token: 0x06000048 RID: 72 RVA: 0x0000386C File Offset: 0x00001A6C
	private void Start()
	{
		this.dir = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		this.dir.Scale(this.rotation);
		base.transform.localEulerAngles = this.dir;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000038D3 File Offset: 0x00001AD3
	private void Update()
	{
		base.transform.Translate(this.axis * this.speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x0400003D RID: 61
	public float speed = 30f;

	// Token: 0x0400003E RID: 62
	public Vector3 rotation = Vector3.forward;

	// Token: 0x0400003F RID: 63
	public Vector3 axis = Vector3.forward;

	// Token: 0x04000040 RID: 64
	public bool gravity;

	// Token: 0x04000041 RID: 65
	private Vector3 dir;
}
