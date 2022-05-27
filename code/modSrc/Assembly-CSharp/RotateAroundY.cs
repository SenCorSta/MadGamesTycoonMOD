using System;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class RotateAroundY : MonoBehaviour
{
	// Token: 0x06001F8A RID: 8074 RVA: 0x00014E41 File Offset: 0x00013041
	private void Update()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * this.rotateSpeed);
	}

	// Token: 0x04002809 RID: 10249
	public float rotateSpeed = 10f;
}
