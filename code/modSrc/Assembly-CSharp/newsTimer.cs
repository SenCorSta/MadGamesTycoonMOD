using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class newsTimer : MonoBehaviour
{
	// Token: 0x06001AB9 RID: 6841 RVA: 0x0010D06C File Offset: 0x0010B26C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x0010D074 File Offset: 0x0010B274
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

	// Token: 0x06001ABB RID: 6843 RVA: 0x0010D0DC File Offset: 0x0010B2DC
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

	// Token: 0x040021DA RID: 8666
	public GameObject main_;

	// Token: 0x040021DB RID: 8667
	public mainScript mS_;

	// Token: 0x040021DC RID: 8668
	public settingsScript settings_;

	// Token: 0x040021DD RID: 8669
	public float aliveTimer;
}
