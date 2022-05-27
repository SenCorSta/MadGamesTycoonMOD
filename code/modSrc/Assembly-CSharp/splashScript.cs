using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002FE RID: 766
public class splashScript : MonoBehaviour
{
	// Token: 0x06001A93 RID: 6803 RVA: 0x00011DCE File Offset: 0x0000FFCE
	private void Start()
	{
		this.LoadSettings();
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x00111D20 File Offset: 0x0010FF20
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

	// Token: 0x06001A95 RID: 6805 RVA: 0x00011DD6 File Offset: 0x0000FFD6
	private void LoadSettings()
	{
		PlayerPrefs.SetInt("LoadSavegame", -1);
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x00011DE3 File Offset: 0x0000FFE3
	private void LoadNextScene()
	{
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x040021EE RID: 8686
	private float splashTimer;
}
