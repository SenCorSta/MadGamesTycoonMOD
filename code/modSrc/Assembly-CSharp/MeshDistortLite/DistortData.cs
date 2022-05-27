using System;
using UnityEngine;

namespace MeshDistortLite
{
	
	[Serializable]
	public class DistortData
	{
		
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

		
		public bool enabled = true;

		
		public string name = "Effect";

		
		public float animationSpeed = 1f;

		
		public Distort.Type type;

		
		public float force = 1f;

		
		public float movementDisplacement;

		
		public Vector3 tile = Vector3.one;

		
		public AnimationCurve displacedForceX = new AnimationCurve();

		
		public AnimationCurve displacedForceY = new AnimationCurve();

		
		public AnimationCurve displacedForceZ = new AnimationCurve();

		
		public AnimationCurve displacedForceXY = new AnimationCurve();

		
		public AnimationCurve displacedForceXZ = new AnimationCurve();

		
		public AnimationCurve displacedForceYX = new AnimationCurve();

		
		public AnimationCurve displacedForceYZ = new AnimationCurve();

		
		public AnimationCurve displacedForceZX = new AnimationCurve();

		
		public AnimationCurve displacedForceZY = new AnimationCurve();

		
		public AnimationCurve staticForceX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		
		public AnimationCurve staticForceY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		
		public AnimationCurve staticForceZ = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		
		public bool isPingPong = true;

		
		public bool showInEditor = true;

		
		public bool calculateInWorldSpace;

		
		private float x;

		
		private float y;

		
		private float z;

		
		private Vector3 bMin;

		
		private Vector3 bNormalized;

		
		private Vector3 percentage;

		
		private float multiplier;

		
		private Vector3 dir;
	}
}
