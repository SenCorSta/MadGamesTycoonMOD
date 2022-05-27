using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class serverLamp : MonoBehaviour
{
	// Token: 0x06001A8A RID: 6794 RVA: 0x00011DA2 File Offset: 0x0000FFA2
	private void Start()
	{
		this.FindScripts();
		this.FindRenderer();
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x00111968 File Offset: 0x0010FB68
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

	// Token: 0x06001A8C RID: 6796 RVA: 0x001119C8 File Offset: 0x0010FBC8
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

	// Token: 0x06001A8D RID: 6797 RVA: 0x00111A20 File Offset: 0x0010FC20
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

	// Token: 0x06001A8E RID: 6798 RVA: 0x00111B48 File Offset: 0x0010FD48
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

	// Token: 0x040021DE RID: 8670
	public objectScript oS_;

	// Token: 0x040021DF RID: 8671
	private mainScript mS_;

	// Token: 0x040021E0 RID: 8672
	private roomScript rS_;

	// Token: 0x040021E1 RID: 8673
	public GameObject[] goLamps;

	// Token: 0x040021E2 RID: 8674
	private Renderer[] goLamps_Renderer;

	// Token: 0x040021E3 RID: 8675
	public Material[] materials;

	// Token: 0x040021E4 RID: 8676
	private float timer;
}
