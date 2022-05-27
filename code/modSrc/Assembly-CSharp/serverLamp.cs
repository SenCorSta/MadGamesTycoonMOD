using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class serverLamp : MonoBehaviour
{
	// Token: 0x06001AD4 RID: 6868 RVA: 0x0010DD34 File Offset: 0x0010BF34
	private void Start()
	{
		this.FindScripts();
		this.FindRenderer();
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0010DD44 File Offset: 0x0010BF44
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = GameObject.FindWithTag("Main").GetComponent<mainScript>();
		}
		if (!this.oS_ && this.mS_.multiplayer && !this.oS_)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0010DDA4 File Offset: 0x0010BFA4
	private void FindRenderer()
	{
		this.goLamps_Renderer = new Renderer[this.goLamps.Length];
		for (int i = 0; i < this.goLamps.Length; i++)
		{
			if (this.goLamps[i])
			{
				this.goLamps_Renderer[i] = this.goLamps[i].GetComponent<Renderer>();
			}
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0010DDFC File Offset: 0x0010BFFC
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if (this.timer < 0.1f)
		{
			return;
		}
		this.timer = 0f;
		if (this.goLamps_Renderer[0] && !this.goLamps_Renderer[0].isVisible)
		{
			return;
		}
		this.FindRoomScript();
		if (this.rS_)
		{
			if (this.rS_.serverDown)
			{
				for (int i = 0; i < this.goLamps.Length; i++)
				{
					if (this.goLamps[i] && this.goLamps_Renderer[i])
					{
						this.goLamps_Renderer[i].material = this.materials[1];
					}
				}
				return;
			}
			for (int j = 0; j < this.goLamps.Length; j++)
			{
				if (this.goLamps[j] && UnityEngine.Random.Range(0, 100) > 80 && this.goLamps_Renderer[j])
				{
					this.goLamps_Renderer[j].material = this.materials[UnityEngine.Random.Range(0, this.materials.Length)];
				}
			}
		}
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0010DF24 File Offset: 0x0010C124
	private void FindRoomScript()
	{
		if (this.rS_)
		{
			return;
		}
		if (!this.mS_)
		{
			return;
		}
		if (!this.mS_.mapScript_)
		{
			return;
		}
		int num = Mathf.RoundToInt(this.oS_.gameObject.transform.position.x);
		int num2 = Mathf.RoundToInt(this.oS_.gameObject.transform.position.z);
		roomScript exists = this.mS_.mapScript_.mapRoomScript[num, num2];
		if (exists)
		{
			this.rS_ = exists;
		}
	}

	// Token: 0x040021F8 RID: 8696
	public objectScript oS_;

	// Token: 0x040021F9 RID: 8697
	private mainScript mS_;

	// Token: 0x040021FA RID: 8698
	private roomScript rS_;

	// Token: 0x040021FB RID: 8699
	public GameObject[] goLamps;

	// Token: 0x040021FC RID: 8700
	private Renderer[] goLamps_Renderer;

	// Token: 0x040021FD RID: 8701
	public Material[] materials;

	// Token: 0x040021FE RID: 8702
	private float timer;
}
