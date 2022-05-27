using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FD RID: 1021
	public class DemoCircleExpositor : MonoBehaviour
	{
		// Token: 0x060023EC RID: 9196 RVA: 0x00171A08 File Offset: 0x0016FC08
		private void Start()
		{
			this.dummyRotation = base.transform.rotation;
			this.iniY = base.transform.position.y;
			this.items = new Transform[base.transform.childCount];
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				this.items[this.count] = transform;
				this.count++;
			}
			this.offsetRotation = 360f / (float)this.count;
			for (int i = 0; i < this.count; i++)
			{
				float f = (float)i * 3.1415927f * 2f / (float)this.count;
				Vector3 position = new Vector3(Mathf.Sin(f) * this.radius, this.iniY, -Mathf.Cos(f) * this.radius);
				this.items[i].position = position;
			}
			this.zOffset = this.radius - 40f;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, this.zOffset);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x0001870C File Offset: 0x0001690C
		private void Update()
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.dummyRotation, this.rotateSpeed * Time.deltaTime);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x00171B70 File Offset: 0x0016FD70
		public void ChangeTarget(int offset)
		{
			this.currentTarget += offset;
			if (this.currentTarget > this.items.Length - 1)
			{
				this.currentTarget = 0;
			}
			else if (this.currentTarget < 0)
			{
				this.currentTarget = this.items.Length - 1;
			}
			this.dummyRotation *= Quaternion.Euler(Vector3.up * (float)offset * this.offsetRotation);
		}

		// Token: 0x04002E44 RID: 11844
		[SerializeField]
		private float radius = 40f;

		// Token: 0x04002E45 RID: 11845
		[SerializeField]
		private float rotateSpeed = 10f;

		// Token: 0x04002E46 RID: 11846
		private float zOffset;

		// Token: 0x04002E47 RID: 11847
		private Transform[] items;

		// Token: 0x04002E48 RID: 11848
		private int count;

		// Token: 0x04002E49 RID: 11849
		private int currentTarget;

		// Token: 0x04002E4A RID: 11850
		private float offsetRotation;

		// Token: 0x04002E4B RID: 11851
		private float iniY;

		// Token: 0x04002E4C RID: 11852
		private Quaternion dummyRotation;
	}
}
