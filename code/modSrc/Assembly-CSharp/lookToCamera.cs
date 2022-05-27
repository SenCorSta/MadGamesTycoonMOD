using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class lookToCamera : MonoBehaviour
{
	// Token: 0x06001CCC RID: 7372 RVA: 0x0011E6F4 File Offset: 0x0011C8F4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x0011E6FC File Offset: 0x0011C8FC
	private void FindScripts()
	{
		this.camera_ = GameObject.Find("Camera");
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x0011E710 File Offset: 0x0011C910
	private void Update()
	{
		base.gameObject.transform.rotation = this.camera_.transform.rotation;
		base.gameObject.transform.position = new Vector3(base.transform.position.x, this.camera_.transform.position.z + 5f, base.transform.position.z);
	}

	// Token: 0x040023F3 RID: 9203
	private GameObject camera_;
}
