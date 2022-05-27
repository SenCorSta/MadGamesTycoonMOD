using System;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class RotateAroundY : MonoBehaviour
{
	// Token: 0x06001FDD RID: 8157 RVA: 0x0014BE51 File Offset: 0x0014A051
	private void Update()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * this.rotateSpeed);
	}

	// Token: 0x0400281F RID: 10271
	public float rotateSpeed = 10f;
}
