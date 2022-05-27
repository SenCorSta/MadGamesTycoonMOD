using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class schreibmaschineScript : MonoBehaviour
{
	// Token: 0x06001A86 RID: 6790 RVA: 0x00011D9A File Offset: 0x0000FF9A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x00111788 File Offset: 0x0010F988
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

	// Token: 0x06001A88 RID: 6792 RVA: 0x00111838 File Offset: 0x0010FA38
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
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.myAnimation["schreibmaschine1"].speed = this.mS_.GetGameSpeed();
		}
		if (this.roomS_.taskID == -1 || !this.oS_.inUse)
		{
			if (this.myAnimation.isPlaying)
			{
				this.myAnimation.Stop();
				return;
			}
		}
		else if (!this.myAnimation.isPlaying)
		{
			this.myAnimation.Play();
		}
	}

	// Token: 0x040021D6 RID: 8662
	public Animation myAnimation;

	// Token: 0x040021D7 RID: 8663
	private objectScript oS_;

	// Token: 0x040021D8 RID: 8664
	public MeshRenderer renderer;

	// Token: 0x040021D9 RID: 8665
	private GameObject main_;

	// Token: 0x040021DA RID: 8666
	private roomScript roomS_;

	// Token: 0x040021DB RID: 8667
	private mapScript mapS_;

	// Token: 0x040021DC RID: 8668
	private mainScript mS_;

	// Token: 0x040021DD RID: 8669
	private float oldGamespeed;
}
