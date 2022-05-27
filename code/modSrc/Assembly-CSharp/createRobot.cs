using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class createRobot : MonoBehaviour
{
	// Token: 0x06001A43 RID: 6723 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x0010A418 File Offset: 0x00108618
	public void Init(int id_)
	{
		if (this.myRobot)
		{
			UnityEngine.Object.Destroy(this.myRobot);
		}
		this.myRobot = UnityEngine.Object.Instantiate<GameObject>(this.prefabRobot, base.transform.position, Quaternion.identity);
		this.myRobot.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, base.transform.eulerAngles.z + 180f);
		this.myRobot.GetComponent<Animation>().Play();
		this.myRobot.GetComponent<robotScript>().stationID = id_;
		UnityEngine.Object.Destroy(this.destroyThis);
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0010A4D8 File Offset: 0x001086D8
	private void OnDestroy()
	{
		if (this.myRobot)
		{
			GameObject myTarget = this.myRobot.GetComponent<robotScript>().myTarget;
			if (myTarget && myTarget.tag == "Muell_InUse")
			{
				myTarget.tag = "Muell";
			}
			UnityEngine.Object.Destroy(this.myRobot);
		}
	}

	// Token: 0x0400214D RID: 8525
	public GameObject prefabRobot;

	// Token: 0x0400214E RID: 8526
	public GameObject destroyThis;

	// Token: 0x0400214F RID: 8527
	public GameObject myRobot;
}
