using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class arcadeProduktionScript : MonoBehaviour
{
	// Token: 0x06001A1B RID: 6683 RVA: 0x00109799 File Offset: 0x00107999
	private void Start()
	{
		this.FindScripts();
		this.DisableAllChilds();
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x001097A8 File Offset: 0x001079A8
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

	// Token: 0x06001A1D RID: 6685 RVA: 0x00109858 File Offset: 0x00107A58
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

	// Token: 0x06001A1E RID: 6686 RVA: 0x00109AFC File Offset: 0x00107CFC
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

	// Token: 0x0400211F RID: 8479
	public Animation myAnimation;

	// Token: 0x04002120 RID: 8480
	public bool force;

	// Token: 0x04002121 RID: 8481
	public GameObject saegeblatt;

	// Token: 0x04002122 RID: 8482
	public GameObject saegeplattPartikel;

	// Token: 0x04002123 RID: 8483
	private GameObject main_;

	// Token: 0x04002124 RID: 8484
	private roomScript roomS_;

	// Token: 0x04002125 RID: 8485
	private mapScript mapS_;

	// Token: 0x04002126 RID: 8486
	private mainScript mS_;

	// Token: 0x04002127 RID: 8487
	private objectScript oS_;

	// Token: 0x04002128 RID: 8488
	private characterScript charS_;
}
