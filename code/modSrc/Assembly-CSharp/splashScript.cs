using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class splashScript : MonoBehaviour
{
	
	private void Start()
	{
		this.LoadSettings();
	}

	
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

	
	private void LoadSettings()
	{
		PlayerPrefs.SetInt("LoadSavegame", -1);
	}

	
	private void LoadNextScene()
	{
		SceneManager.LoadScene("scene01");
	}

	
	private float splashTimer;
}
