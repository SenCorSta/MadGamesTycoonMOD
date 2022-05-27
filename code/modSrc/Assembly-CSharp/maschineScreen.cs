using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class maschineScreen : MonoBehaviour
{
	// Token: 0x06001AA8 RID: 6824 RVA: 0x0010C9E3 File Offset: 0x0010ABE3
	private void Start()
	{
		this.mS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<mainScript>();
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x0010C9FC File Offset: 0x0010ABFC
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

	// Token: 0x040021C1 RID: 8641
	public MeshRenderer renderer;

	// Token: 0x040021C2 RID: 8642
	public Material[] mat;

	// Token: 0x040021C3 RID: 8643
	private float timer;

	// Token: 0x040021C4 RID: 8644
	private float rnd;

	// Token: 0x040021C5 RID: 8645
	private roomScript roomS_;

	// Token: 0x040021C6 RID: 8646
	private mapScript mapS_;

	// Token: 0x040021C7 RID: 8647
	private mainScript mS_;

	// Token: 0x040021C8 RID: 8648
	private objectScript oS_;
}
