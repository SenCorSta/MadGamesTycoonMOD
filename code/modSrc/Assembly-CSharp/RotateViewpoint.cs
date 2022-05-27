using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class RotateViewpoint : MonoBehaviour
{
	// Token: 0x06001FEB RID: 8171 RVA: 0x0014C3D8 File Offset: 0x0014A5D8
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.right, this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x04002835 RID: 10293
	public float rotateSpeed = 5f;
}
