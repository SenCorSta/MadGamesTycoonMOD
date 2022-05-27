using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class maschineScreen : MonoBehaviour
{
	// Token: 0x06001A5E RID: 6750 RVA: 0x00011BA6 File Offset: 0x0000FDA6
	private void Start()
	{
		this.mS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<mainScript>();
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x001107FC File Offset: 0x0010E9FC
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if (this.timer > this.rnd)
		{
			this.timer = 0f;
			this.rnd = UnityEngine.Random.Range(0.1f, 1.5f);
			this.renderer.material = this.mat[UnityEngine.Random.Range(1, this.mat.Length)];
		}
	}

	// Token: 0x040021A7 RID: 8615
	public MeshRenderer renderer;

	// Token: 0x040021A8 RID: 8616
	public Material[] mat;

	// Token: 0x040021A9 RID: 8617
	private float timer;

	// Token: 0x040021AA RID: 8618
	private float rnd;

	// Token: 0x040021AB RID: 8619
	private roomScript roomS_;

	// Token: 0x040021AC RID: 8620
	private mapScript mapS_;

	// Token: 0x040021AD RID: 8621
	private mainScript mS_;

	// Token: 0x040021AE RID: 8622
	private objectScript oS_;
}
