using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class eyeScript : MonoBehaviour
{
	// Token: 0x06001A65 RID: 6757 RVA: 0x0010AD82 File Offset: 0x00108F82
	private void Start()
	{
		this.myCamera = GameObject.Find("Camera");
		this.myAnimation = base.GetComponent<Animation>();
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x0010ADA0 File Offset: 0x00108FA0
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer < 5f)
		{
			return;
		}
		this.timer = 0f;
		if (this.myCamera.transform.localPosition.z < -9.5f)
		{
			this.myAnimation.Stop();
			Debug.Log("STOP");
			return;
		}
		this.myAnimation.Play();
		Debug.Log("PLAY");
	}

	// Token: 0x0400216A RID: 8554
	public float timer;

	// Token: 0x0400216B RID: 8555
	public GameObject myCamera;

	// Token: 0x0400216C RID: 8556
	public Animation myAnimation;
}
