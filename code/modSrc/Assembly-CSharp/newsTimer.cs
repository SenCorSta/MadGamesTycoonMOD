using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class newsTimer : MonoBehaviour
{
	// Token: 0x06001A6F RID: 6767 RVA: 0x00011C6D File Offset: 0x0000FE6D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x00110DBC File Offset: 0x0010EFBC
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x00110E24 File Offset: 0x0010F024
	private void Update()
	{
		if (this.mS_.gameSpeed <= 0f)
		{
			return;
		}
		this.aliveTimer += Time.deltaTime;
		if (this.aliveTimer > this.settings_.newsTime)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040021C0 RID: 8640
	public GameObject main_;

	// Token: 0x040021C1 RID: 8641
	public mainScript mS_;

	// Token: 0x040021C2 RID: 8642
	public settingsScript settings_;

	// Token: 0x040021C3 RID: 8643
	public float aliveTimer;
}
