using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000301 RID: 769
public class splashScript : MonoBehaviour
{
	// Token: 0x06001ADD RID: 6877 RVA: 0x0010E119 File Offset: 0x0010C319
	private void Start()
	{
		this.LoadSettings();
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0010E124 File Offset: 0x0010C324
	private void Update()
	{
		if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			this.splashTimer = 5f;
		}
		this.splashTimer += Time.deltaTime;
		if (this.splashTimer < 5f)
		{
			return;
		}
		this.LoadNextScene();
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0010E179 File Offset: 0x0010C379
	private void LoadSettings()
	{
		PlayerPrefs.SetInt("LoadSavegame", -1);
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0010E186 File Offset: 0x0010C386
	private void LoadNextScene()
	{
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x04002208 RID: 8712
	private float splashTimer;
}
