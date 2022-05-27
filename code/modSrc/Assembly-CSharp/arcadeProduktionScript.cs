using System;
using UnityEngine;


public class arcadeProduktionScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.DisableAllChilds();
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
		if (!this.oS_)
		{
			this.oS_ = base.transform.root.gameObject.GetComponent<objectScript>();
			if (this.mS_.multiplayer && !this.oS_)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
		}
	}

	
	private void Update()
	{
		if (!this.force)
		{
			if (!this.oS_)
			{
				this.FindScripts();
				return;
			}
			if (this.oS_.picked)
			{
				if (this.myAnimation.isPlaying)
				{
					this.myAnimation.Stop();
					this.DisableAllChilds();
				}
				return;
			}
			this.roomS_ = this.mapS_.mapRoomScript[Mathf.RoundToInt(base.transform.root.transform.position.x), Mathf.RoundToInt(base.transform.root.transform.position.z)];
			if (!this.roomS_)
			{
				return;
			}
			if (this.roomS_.taskID == -1 || !this.oS_.inUse || this.roomS_.pause || this.oS_.picked)
			{
				if (this.myAnimation.isPlaying)
				{
					this.myAnimation.Stop();
					this.DisableAllChilds();
					if (this.saegeblatt)
					{
						this.saegeblatt.GetComponent<Animation>().Stop();
						if (this.saegeplattPartikel && this.saegeplattPartikel.activeSelf)
						{
							this.saegeplattPartikel.SetActive(false);
						}
					}
				}
				return;
			}
			if (!this.charS_ && this.oS_.besetztCharID != -1)
			{
				GameObject gameObject = GameObject.Find("CHAR_" + this.oS_.besetztCharID.ToString());
				if (gameObject)
				{
					this.charS_ = gameObject.GetComponent<characterScript>();
				}
			}
			if (this.charS_)
			{
				if (this.charS_.myID != this.oS_.besetztCharID)
				{
					this.charS_ = null;
					return;
				}
				if (this.charS_.moveS_)
				{
					if (this.charS_.moveS_.waitForceAnimation <= 0f && this.roomS_.WERK_GameHasBestellungen())
					{
						if (!this.myAnimation.isPlaying)
						{
							this.myAnimation.Play();
							if (this.saegeblatt)
							{
								this.saegeblatt.GetComponent<Animation>().Play();
								if (this.saegeplattPartikel && !this.saegeplattPartikel.activeSelf)
								{
									this.saegeplattPartikel.SetActive(true);
								}
							}
						}
						this.myAnimation["arcadeProduction"].speed = this.mS_.gameSpeed;
						return;
					}
					this.myAnimation["arcadeProduction"].speed = 0f;
				}
			}
		}
	}

	
	private void DisableAllChilds()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeSelf)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	
	public Animation myAnimation;

	
	public bool force;

	
	public GameObject saegeblatt;

	
	public GameObject saegeplattPartikel;

	
	private GameObject main_;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private mainScript mS_;

	
	private objectScript oS_;

	
	private characterScript charS_;
}
