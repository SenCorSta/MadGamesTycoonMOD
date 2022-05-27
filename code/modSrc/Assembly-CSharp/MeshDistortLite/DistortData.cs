using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E9 RID: 1001
	[Serializable]
	public class DistortData
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x001723E0 File Offset: 0x001705E0
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

		// Token: 0x060023D5 RID: 9173 RVA: 0x0017247C File Offset: 0x0017067C
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

		// Token: 0x04002DAF RID: 11695
		public bool enabled = true;

		// Token: 0x04002DB0 RID: 11696
		public string name = "Effect";

		// Token: 0x04002DB1 RID: 11697
		public float animationSpeed = 1f;

		// Token: 0x04002DB2 RID: 11698
		public Distort.Type type;

		// Token: 0x04002DB3 RID: 11699
		public float force = 1f;

		// Token: 0x04002DB4 RID: 11700
		public float movementDisplacement;

		// Token: 0x04002DB5 RID: 11701
		public Vector3 tile = Vector3.one;

		// Token: 0x04002DB6 RID: 11702
		public AnimationCurve displacedForceX = new AnimationCurve();

		// Token: 0x04002DB7 RID: 11703
		public AnimationCurve displacedForceY = new AnimationCurve();

		// Token: 0x04002DB8 RID: 11704
		public AnimationCurve displacedForceZ = new AnimationCurve();

		// Token: 0x04002DB9 RID: 11705
		public AnimationCurve displacedForceXY = new AnimationCurve();

		// Token: 0x04002DBA RID: 11706
		public AnimationCurve displacedForceXZ = new AnimationCurve();

		// Token: 0x04002DBB RID: 11707
		public AnimationCurve displacedForceYX = new AnimationCurve();

		// Token: 0x04002DBC RID: 11708
		public AnimationCurve displacedForceYZ = new AnimationCurve();

		// Token: 0x04002DBD RID: 11709
		public AnimationCurve displacedForceZX = new AnimationCurve();

		// Token: 0x04002DBE RID: 11710
		public AnimationCurve displacedForceZY = new AnimationCurve();

		// Token: 0x04002DBF RID: 11711
		public AnimationCurve staticForceX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DC0 RID: 11712
		public AnimationCurve staticForceY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DC1 RID: 11713
		public AnimationCurve staticForceZ = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002DC2 RID: 11714
		public bool isPingPong = true;

		// Token: 0x04002DC3 RID: 11715
		public bool showInEditor = true;

		// Token: 0x04002DC4 RID: 11716
		public bool calculateInWorldSpace;

		// Token: 0x04002DC5 RID: 11717
		private float x;

		// Token: 0x04002DC6 RID: 11718
		private float y;

		// Token: 0x04002DC7 RID: 11719
		private float z;

		// Token: 0x04002DC8 RID: 11720
		private Vector3 bMin;

		// Token: 0x04002DC9 RID: 11721
		private Vector3 bNormalized;

		// Token: 0x04002DCA RID: 11722
		private Vector3 percentage;

		// Token: 0x04002DCB RID: 11723
		private float multiplier;

		// Token: 0x04002DCC RID: 11724
		private Vector3 dir;
	}
}
