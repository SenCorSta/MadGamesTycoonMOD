using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class DemoCircleExpositor : MonoBehaviour
	{
		
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

		
		private void Update()
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.dummyRotation, this.rotateSpeed * Time.deltaTime);
		}

		
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

		
		[SerializeField]
		private float radius = 40f;

		
		[SerializeField]
		private float rotateSpeed = 10f;

		
		private float zOffset;

		
		private Transform[] items;

		
		private int count;

		
		private int currentTarget;

		
		private float offsetRotation;

		
		private float iniY;

		
		private Quaternion dummyRotation;
	}
}
