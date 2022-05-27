using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class golfScript : MonoBehaviour
{
	// Token: 0x06001A72 RID: 6770 RVA: 0x0010AF5B File Offset: 0x0010915B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x0010AF64 File Offset: 0x00109164
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

	// Token: 0x06001A74 RID: 6772 RVA: 0x0010AFCE File Offset: 0x001091CE
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x0010AFDC File Offset: 0x001091DC
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

	// Token: 0x04002175 RID: 8565
	private GameObject main_;

	// Token: 0x04002176 RID: 8566
	private mainScript mS_;

	// Token: 0x04002177 RID: 8567
	private sfxScript sfx_;

	// Token: 0x04002178 RID: 8568
	public GameObject prefabFlyingBall;

	// Token: 0x04002179 RID: 8569
	private GameObject myBall;

	// Token: 0x0400217A RID: 8570
	public float timer;
}
