using System;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class CameraZoom : MonoBehaviour
{
	// Token: 0x06002007 RID: 8199 RVA: 0x0014CE8C File Offset: 0x0014B08C
	private void Update()
	{
		base.transform.Translate(Vector3.forward * this.zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
		base.transform.Translate(Vector3.forward * this.keyZoomSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
	}

	// Token: 0x0400285F RID: 10335
	public float zoomSpeed = 10f;

	// Token: 0x04002860 RID: 10336
	public float keyZoomSpeed = 20f;
}
