using System;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class arcadeProduktionScript : MonoBehaviour
{
	// Token: 0x060019D1 RID: 6609 RVA: 0x000116C2 File Offset: 0x0000F8C2
	private void Start()
	{
		this.FindScripts();
		this.DisableAllChilds();
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x0010DA78 File Offset: 0x0010BC78
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

	// Token: 0x060019D3 RID: 6611 RVA: 0x0010DB28 File Offset: 0x0010BD28
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

	// Token: 0x060019D4 RID: 6612 RVA: 0x0010DDCC File Offset: 0x0010BFCC
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

	// Token: 0x04002105 RID: 8453
	public Animation myAnimation;

	// Token: 0x04002106 RID: 8454
	public bool force;

	// Token: 0x04002107 RID: 8455
	public GameObject saegeblatt;

	// Token: 0x04002108 RID: 8456
	public GameObject saegeplattPartikel;

	// Token: 0x04002109 RID: 8457
	private GameObject main_;

	// Token: 0x0400210A RID: 8458
	private roomScript roomS_;

	// Token: 0x0400210B RID: 8459
	private mapScript mapS_;

	// Token: 0x0400210C RID: 8460
	private mainScript mS_;

	// Token: 0x0400210D RID: 8461
	private objectScript oS_;

	// Token: 0x0400210E RID: 8462
	private characterScript charS_;
}
