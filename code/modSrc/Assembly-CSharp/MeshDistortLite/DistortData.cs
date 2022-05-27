using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E6 RID: 998
	[Serializable]
	public class DistortData
	{
		// Token: 0x06002381 RID: 9089 RVA: 0x0016F820 File Offset: 0x0016DA20
		public void SetBounds(Bounds bounds)
		{
			this.bMin = bounds.min;
			this.bNormalized = bounds.max - bounds.min;
			if (this.bNormalized.x == 0f)
			{
				this.bNormalized.x = 0.1f;
			}
			if (this.bNormalized.y == 0f)
			{
				this.bNormalized.y = 0.1f;
			}
			if (this.bNormalized.z == 0f)
			{
				this.bNormalized.z = 0.1f;
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0016F8BC File Offset: 0x0016DABC
		public void DistortVertice(ref Vector3 vertice)
		{
			this.x = 0f;
			this.y = 0f;
			this.z = 0f;
			this.percentage = vertice;
			if (this.calculateInWorldSpace)
			{
				this.multiplier = this.staticForceX.Evaluate((this.percentage.x - this.bMin.x) / this.bNormalized.x) * this.staticForceY.Evaluate((this.percentage.y - this.bMin.y) / this.bNormalized.y) * this.staticForceZ.Evaluate((this.percentage.z - this.bMin.z) / this.bNormalized.z);
				this.percentage.x = this.percentage.x / this.bNormalized.x;
				this.percentage.y = this.percentage.y / this.bNormalized.y;
				this.percentage.z = this.percentage.z / this.bNormalized.z;
			}
			else
			{
				this.percentage.x = this.percentage.x - this.bMin.x;
				this.percentage.y = this.percentage.y - this.bMin.y;
				this.percentage.z = this.percentage.z - this.bMin.z;
				this.percentage.x = this.percentage.x / this.bNormalized.x;
				this.percentage.y = this.percentage.y / this.bNormalized.y;
				this.percentage.z = this.percentage.z / this.bNormalized.z;
				this.multiplier = this.staticForceX.Evaluate(this.percentage.x) * this.staticForceY.Evaluate(this.percentage.y) * this.staticForceZ.Evaluate(this.percentage.z);
			}
			if (this.isPingPong)
			{
				this.percentage.x = Math.PingPong((this.percentage.x + this.movementDisplacement) * this.tile.x, 0f, 1f);
				this.percentage.y = Math.PingPong((this.percentage.y + this.movementDisplacement) * this.tile.y, 0f, 1f);
				this.percentage.z = Math.PingPong((this.percentage.z + this.movementDisplacement) * this.tile.z, 0f, 1f);
			}
			else
			{
				this.percentage.x = (this.percentage.x + this.movementDisplacement) * this.tile.x;
				this.percentage.y = (this.percentage.y + this.movementDisplacement) * this.tile.y;
				this.percentage.z = (this.percentage.z + this.movementDisplacement) * this.tile.z;
			}
			this.x += this.displacedForceXY.Evaluate(this.percentage.y) * this.force;
			this.x += this.displacedForceXZ.Evaluate(this.percentage.z) * this.force;
			this.y += this.displacedForceYX.Evaluate(this.percentage.x) * this.force;
			this.y += this.displacedForceYZ.Evaluate(this.percentage.z) * this.force;
			this.z += this.displacedForceZX.Evaluate(this.percentage.x) * this.force;
			this.z += this.displacedForceZY.Evaluate(this.percentage.y) * this.force;
			vertice.x += this.x * this.multiplier;
			vertice.y += this.y * this.multiplier;
			vertice.z += this.z * this.multiplier;
		}

		// Token: 0x04002D99 RID: 11673
		public bool enabled = true;

		// Token: 0x04002D9A RID: 11674
		public string name = "Effect";

		// Token: 0x04002D9B RID: 11675
		public float animationSpeed = 1f;

		// Token: 0x04002D9C RID: 11676
		public Distort.Type type;

		// Token: 0x04002D9D RID: 11677
		public float force = 1f;

		// Token: 0x04002D9E RID: 11678
		public float movementDisplacement;

		// Token: 0x04002D9F RID: 11679
		public Vector3 tile = Vector3.one;

		// Token: 0x04002DA0 RID: 11680
		public AnimationCurve displacedForceX = new AnimationCurve();

		// Token: 0x04002DA1 RID: 11681
		public AnimationCurve displacedForceY = new AnimationCurve();

		// Token: 0x04002DA2 RID: 11682
		public AnimationCurve displacedForceZ = new AnimationCurve();

		// Token: 0x04002DA3 RID: 11683
		public AnimationCurve displacedForceXY = new AnimationCurve();

		// Token: 0x04002DA4 RID: 11684
		public AnimationCurve displacedForceXZ = new AnimationCurve();

		// Token: 0x04002DA5 RID: 11685
		public AnimationCurve displacedForceYX = new AnimationCurve();

		// Token: 0x04002DA6 RID: 11686
		public AnimationCurve displacedForceYZ = new AnimationCurve();

		// Token: 0x04002DA7 RID: 11687
		public AnimationCurve displacedForceZX = new AnimationCurve();

		// Token: 0x04002DA8 RID: 11688
		public AnimationCurve displacedForceZY = new AnimationCurve();

		// Token: 0x04002DA9 RID: 11689
		public AnimationCurve staticForceX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DAA RID: 11690
		public AnimationCurve staticForceY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DAB RID: 11691
		public AnimationCurve staticForceZ = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DAC RID: 11692
		public bool isPingPong = true;

		// Token: 0x04002DAD RID: 11693
		public bool showInEditor = true;

		// Token: 0x04002DAE RID: 11694
		public bool calculateInWorldSpace;

		// Token: 0x04002DAF RID: 11695
		private float x;

		// Token: 0x04002DB0 RID: 11696
		private float y;

		// Token: 0x04002DB1 RID: 11697
		private float z;

		// Token: 0x04002DB2 RID: 11698
		private Vector3 bMin;

		// Token: 0x04002DB3 RID: 11699
		private Vector3 bNormalized;

		// Token: 0x04002DB4 RID: 11700
		private Vector3 percentage;

		// Token: 0x04002DB5 RID: 11701
		private float multiplier;

		// Token: 0x04002DB6 RID: 11702
		private Vector3 dir;
	}
}
