using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class schreibmaschineScript : MonoBehaviour
{
	// Token: 0x06001AD0 RID: 6864 RVA: 0x0010DB49 File Offset: 0x0010BD49
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0010DB54 File Offset: 0x0010BD54
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

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0010DC04 File Offset: 0x0010BE04
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

	// Token: 0x040021F0 RID: 8688
	public Animation myAnimation;

	// Token: 0x040021F1 RID: 8689
	private objectScript oS_;

	// Token: 0x040021F2 RID: 8690
	public MeshRenderer renderer;

	// Token: 0x040021F3 RID: 8691
	private GameObject main_;

	// Token: 0x040021F4 RID: 8692
	private roomScript roomS_;

	// Token: 0x040021F5 RID: 8693
	private mapScript mapS_;

	// Token: 0x040021F6 RID: 8694
	private mainScript mS_;

	// Token: 0x040021F7 RID: 8695
	private float oldGamespeed;
}
