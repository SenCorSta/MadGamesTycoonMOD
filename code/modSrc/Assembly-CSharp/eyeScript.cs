using System;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class eyeScript : MonoBehaviour
{
	// Token: 0x06001A1B RID: 6683 RVA: 0x000119AB File Offset: 0x0000FBAB
	private void Start()
	{
		this.myCamera = GameObject.Find("Camera");
		this.myAnimation = base.GetComponent<Animation>();
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x0010ED7C File Offset: 0x0010CF7C
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

	// Token: 0x04002150 RID: 8528
	public float timer;

	// Token: 0x04002151 RID: 8529
	public GameObject myCamera;

	// Token: 0x04002152 RID: 8530
	public Animation myAnimation;
}
