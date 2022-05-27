using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class CFX_Demo_RotateCamera : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x0000381E File Offset: 0x00001A1E
	private void Update()
	{
		if (CFX_Demo_RotateCamera.rotating)
		{
			base.transform.RotateAround(this.rotationCenter.position, Vector3.up, this.speed * Time.deltaTime);
		}
	}

	// Token: 0x0400003A RID: 58
	public static bool rotating = true;

	// Token: 0x0400003B RID: 59
	public float speed = 30f;

	// Token: 0x0400003C RID: 60
	public Transform rotationCenter;
}
