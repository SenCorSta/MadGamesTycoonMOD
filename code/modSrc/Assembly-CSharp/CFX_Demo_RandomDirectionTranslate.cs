using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class CFX_Demo_RandomDirectionTranslate : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x00003728 File Offset: 0x00001928
	private void Start()
	{
		this.dir = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f)).normalized;
		this.dir.Scale(this.axis);
		this.dir += this.baseDir;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000037A0 File Offset: 0x000019A0
	private void Update()
	{
		base.transform.Translate(this.dir * this.speed * Time.deltaTime);
		if (this.gravity)
		{
			base.transform.Translate(Physics.gravity * Time.deltaTime);
		}
	}

	// Token: 0x04000035 RID: 53
	public float speed = 30f;

	// Token: 0x04000036 RID: 54
	public Vector3 baseDir = Vector3.zero;

	// Token: 0x04000037 RID: 55
	public Vector3 axis = Vector3.forward;

	// Token: 0x04000038 RID: 56
	public bool gravity;

	// Token: 0x04000039 RID: 57
	private Vector3 dir;
}
