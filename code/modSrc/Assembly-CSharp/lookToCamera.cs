using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class lookToCamera : MonoBehaviour
{
	// Token: 0x06001C82 RID: 7298 RVA: 0x000138E9 File Offset: 0x00011AE9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x000138F1 File Offset: 0x00011AF1
	private void FindScripts()
	{
		this.camera_ = GameObject.Find("Camera");
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00120794 File Offset: 0x0011E994
	private void Update()
	{
		base.gameObject.transform.rotation = this.camera_.transform.rotation;
		base.gameObject.transform.position = new Vector3(base.transform.position.x, this.camera_.transform.position.z + 5f, base.transform.position.z);
	}

	// Token: 0x040023D9 RID: 9177
	private GameObject camera_;
}
