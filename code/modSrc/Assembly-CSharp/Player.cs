using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class Player : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x000026AD File Offset: 0x000008AD
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0001B9E0 File Offset: 0x00019BE0
	private void FixedUpdate()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		Vector3 a = new Vector3(axis, 0f, axis2);
		this.rb.AddForce(a * this.speed);
	}

	// Token: 0x040000C0 RID: 192
	public float speed;

	// Token: 0x040000C1 RID: 193
	private Rigidbody rb;
}
