using System;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class CameraZoom : MonoBehaviour
{
	// Token: 0x06001FB4 RID: 8116 RVA: 0x0014D710 File Offset: 0x0014B910
	private void Update()
	{
		base.transform.Translate(Vector3.forward * this.zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
		base.transform.Translate(Vector3.forward * this.keyZoomSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
	}

	// Token: 0x04002849 RID: 10313
	public float zoomSpeed = 10f;

	// Token: 0x0400284A RID: 10314
	public float keyZoomSpeed = 20f;
}
