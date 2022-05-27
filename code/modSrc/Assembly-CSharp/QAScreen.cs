using System;
using UnityEngine;


public class QAScreen : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.oS_)
		{
			this.oS_ = base.transform.root.gameObject.GetComponent<objectScript>();
		}
	}

	
	private void Update()
	{
		if (!this.oS_)
		{
			this.FindScripts();
			return;
		}
		if (this.oS_.picked)
		{
			return;
		}
		if (!this.renderer.isVisible)
		{
			return;
		}
		this.roomS_ = this.mapS_.mapRoomScript[Mathf.RoundToInt(base.transform.root.transform.position.x), Mathf.RoundToInt(base.transform.root.transform.position.z)];
		if (!this.roomS_)
		{
			return;
		}
		if (this.roomS_.taskID == -1 || !this.oS_.inUse)
		{
			this.renderer.material = this.mat[0];
			return;
		}
		this.timer += this.mS_.GetDeltaTime();
		if (this.timer > 2f)
		{
			this.timer = 0f;
			if (!this.newMat)
			{
				this.newMat = new Material(this.mat[0]);
			}
			if (this.roomS_.taskGameObject)
			{
				if (this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>() && this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
				if (this.roomS_.taskGameObject.GetComponent<taskBugfixing>() && this.roomS_.taskGameObject.GetComponent<taskBugfixing>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskBugfixing>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
				if (this.roomS_.taskGameObject.GetComponent<taskSpielbericht>() && this.roomS_.taskGameObject.GetComponent<taskSpielbericht>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskSpielbericht>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
			}
			this.newMat.mainTexture = this.games_.arrayGamesScripts[UnityEngine.Random.Range(0, this.games_.arrayGamesScripts.Length)].GetScreenshotTexture2D();
			this.renderer.material = this.newMat;
			return;
		}
	}

	
	public MeshRenderer renderer;

	
	public Material[] mat;

	
	private Material newMat;

	
	private float timer;

	
	private GameObject main_;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private mainScript mS_;

	
	private objectScript oS_;

	
	private games games_;
}
