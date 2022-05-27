using System;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class RotateViewpoint : MonoBehaviour
{
	// Token: 0x06001F98 RID: 8088 RVA: 0x00014F1B File Offset: 0x0001311B
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.right, this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x0400281F RID: 10271
	public float rotateSpeed = 5f;
}
