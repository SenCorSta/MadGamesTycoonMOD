using System;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class particleFollowCamera : MonoBehaviour
{
	// Token: 0x06001A73 RID: 6771 RVA: 0x00011C75 File Offset: 0x0000FE75
	private void Start()
	{
		base.transform.SetParent(null);
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x00110E74 File Offset: 0x0010F074
	private void Update()
	{
		base.transform.position = new Vector3(this.cameraObject.transform.position.x, base.transform.position.y, this.cameraObject.transform.position.z);
	}

	// Token: 0x040021C4 RID: 8644
	public GameObject cameraObject;
}
