using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class golfScript : MonoBehaviour
{
	// Token: 0x06001A28 RID: 6696 RVA: 0x00011A2B File Offset: 0x0000FC2B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x0010EED8 File Offset: 0x0010D0D8
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x00011A33 File Offset: 0x0000FC33
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x0010EF44 File Offset: 0x0010D144
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if ((double)this.timer >= 1.417)
		{
			this.timer = 0f;
			if (this.myBall)
			{
				UnityEngine.Object.Destroy(this.myBall);
			}
			this.myBall = UnityEngine.Object.Instantiate<GameObject>(this.prefabFlyingBall);
			this.myBall.transform.position = base.gameObject.transform.position;
			this.myBall.transform.rotation = base.gameObject.transform.root.transform.rotation;
			this.myBall.transform.eulerAngles = new Vector3(this.myBall.transform.eulerAngles.x + (float)UnityEngine.Random.Range(-15, 15), this.myBall.transform.eulerAngles.y + (float)UnityEngine.Random.Range(-30, -15), this.myBall.transform.eulerAngles.z);
		}
	}

	// Token: 0x0400215B RID: 8539
	private GameObject main_;

	// Token: 0x0400215C RID: 8540
	private mainScript mS_;

	// Token: 0x0400215D RID: 8541
	private sfxScript sfx_;

	// Token: 0x0400215E RID: 8542
	public GameObject prefabFlyingBall;

	// Token: 0x0400215F RID: 8543
	private GameObject myBall;

	// Token: 0x04002160 RID: 8544
	public float timer;
}
