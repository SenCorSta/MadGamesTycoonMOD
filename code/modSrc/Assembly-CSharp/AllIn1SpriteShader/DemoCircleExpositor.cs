using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000400 RID: 1024
	public class DemoCircleExpositor : MonoBehaviour
	{
		// Token: 0x0600243F RID: 9279 RVA: 0x001749F4 File Offset: 0x00172BF4
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

		// Token: 0x06002440 RID: 9280 RVA: 0x00174B5C File Offset: 0x00172D5C
		private void Update()
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.dummyRotation, this.rotateSpeed * Time.deltaTime);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00174B8C File Offset: 0x00172D8C
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

		// Token: 0x04002E5A RID: 11866
		[SerializeField]
		private float radius = 40f;

		// Token: 0x04002E5B RID: 11867
		[SerializeField]
		private float rotateSpeed = 10f;

		// Token: 0x04002E5C RID: 11868
		private float zOffset;

		// Token: 0x04002E5D RID: 11869
		private Transform[] items;

		// Token: 0x04002E5E RID: 11870
		private int count;

		// Token: 0x04002E5F RID: 11871
		private int currentTarget;

		// Token: 0x04002E60 RID: 11872
		private float offsetRotation;

		// Token: 0x04002E61 RID: 11873
		private float iniY;

		// Token: 0x04002E62 RID: 11874
		private Quaternion dummyRotation;
	}
}
