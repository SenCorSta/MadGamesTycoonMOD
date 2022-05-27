using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003AA RID: 938
	public class fx_buoyancy : MonoBehaviour
	{
		// Token: 0x060022D1 RID: 8913 RVA: 0x0016D058 File Offset: 0x0016B258
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_buoy.psd", true);
			this.gizPos.y = this.gizPos.y - 0.03f;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0016D0B0 File Offset: 0x0016B2B0
		private void Start()
		{
			if (GameObject.Find("SUIMONO_Module") != null)
			{
				this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
			}
			this.randSeed = Environment.TickCount;
			this.buyRand = new Suimono.Core.Random(this.randSeed);
			if (this.applyToParent)
			{
				fx_buoyancy[] componentsInChildren = base.transform.parent.gameObject.GetComponentsInChildren<fx_buoyancy>();
				if (componentsInChildren != null)
				{
					this.splitFac = 1f / (float)componentsInChildren.Length;
				}
			}
			if (this.applyToParent)
			{
				this.physTarget = base.transform.parent.transform;
				if (this.physTarget != null && this.rigidbodyComponent == null)
				{
					this.rigidbodyComponent = this.physTarget.GetComponent<Rigidbody>();
					return;
				}
			}
			else
			{
				this.physTarget = base.transform;
				if (this.physTarget != null && this.rigidbodyComponent == null)
				{
					this.rigidbodyComponent = base.GetComponent<Rigidbody>();
				}
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0016D1B8 File Offset: 0x0016B3B8
		private void FixedUpdate()
		{
			this.SetUpdate();
			if (!this.isUnderwater)
			{
				this.maxVerticalSpeed = 0.25f;
			}
			else if (this.isUnderwater)
			{
				this.maxVerticalSpeed = Mathf.Clamp(this.surfaceLevel - (base.transform.position.y + this.buoyancyOffset - 0.5f), 0f, 5f);
				if (this.maxVerticalSpeed > 4f)
				{
					this.maxVerticalSpeed = 4f;
				}
			}
			this.buoyancy = 1f + this.maxVerticalSpeed * this.buoyancyStrength;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0016D254 File Offset: 0x0016B454
		private void SetUpdate()
		{
			if (this.moduleObject != null)
			{
				if (this.buyRand == null)
				{
					this.buyRand = new Suimono.Core.Random(this.randSeed);
				}
				this.performHeight = true;
				if (this.physTarget != null && this.moduleObject.setCamera != null)
				{
					if (this.activationRange > 0f)
					{
						this.currRange = Vector3.Distance(this.moduleObject.setCamera.transform.position, this.physTarget.transform.position);
						if (this.currRange >= this.activationRange)
						{
							this.performHeight = false;
						}
					}
					if (this.activationRange <= 0f)
					{
						this.performHeight = true;
					}
					if (!this.isEnabled)
					{
						this.performHeight = false;
					}
				}
				if (this.performHeight)
				{
					this.heightValues = this.moduleObject.SuimonoGetHeightAll(base.transform.position);
					this.isOver = this.heightValues[4];
					this.waveHt = this.heightValues[8];
					this.surfaceLevel = this.heightValues[0];
					this.forceAngles = this.moduleObject.SuimonoConvertAngleToVector(this.heightValues[6]);
					this.forceSpeed = this.heightValues[7] * 0.1f;
				}
				this.forceHeightFactor = Mathf.Clamp01(this.forceHeightFactor);
				this.isUnderwater = false;
				this.underwaterLevel = 0f;
				this.testObjectHeight = base.transform.position.y + this.buoyancyOffset - 0.5f;
				this.waveHeight = this.surfaceLevel;
				if (this.testObjectHeight < this.waveHeight)
				{
					this.isUnderwater = true;
				}
				this.underwaterLevel = this.waveHeight - this.testObjectHeight;
				if (!this.keepAtSurface && this.rigidbodyComponent)
				{
					this.rigidbodyComponent.isKinematic = this.saveRigidbodyState;
				}
				if (!this.keepAtSurface && this.engageBuoyancy && this.isOver == 1f && this.rigidbodyComponent && !this.rigidbodyComponent.isKinematic)
				{
					if (this.rigidbodyComponent.isKinematic)
					{
						this.rigidbodyComponent.isKinematic = this.saveRigidbodyState;
					}
					this.buoyancyFactor = 10f;
					if (this.isUnderwater)
					{
						if (base.transform.position.y + this.buoyancyOffset - 0.5f < this.waveHeight - this.surfaceRange)
						{
							this.forceMod = this.buoyancyFactor * (this.buoyancy * this.rigidbodyComponent.mass) * this.underwaterLevel * this.splitFac * (this.isUnderwater ? 1f : 0f);
							if (this.rigidbodyComponent.velocity.y < this.maxVerticalSpeed)
							{
								this.rigidbodyComponent.AddForceAtPosition(new Vector3(0f, 1f, 0f) * this.forceMod, base.transform.position);
							}
							this.modTime = 0f;
						}
						else
						{
							this.modTime = (base.transform.position.y + this.buoyancyOffset - 0.5f) / (this.waveHeight + this.buyRand.Next(0f, 0.25f) * (this.isUnderwater ? 1f : 0f));
							if (this.rigidbodyComponent.velocity.y > 0f)
							{
								this.rigidbodyComponent.velocity = new Vector3(this.rigidbodyComponent.velocity.x, Mathf.SmoothStep(this.rigidbodyComponent.velocity.y, 0f, this.modTime), this.rigidbodyComponent.velocity.z);
							}
						}
						if (this.inheritForce && base.transform.position.y + this.buoyancyOffset - 0.5f <= this.waveHeight)
						{
							this.waveFac = Mathf.Lerp(0f, this.forceHeightFactor, this.waveHt);
							if (this.forceHeightFactor == 0f)
							{
								this.waveFac = 1f;
							}
							this.rigidbodyComponent.AddForceAtPosition(new Vector3(this.forceAngles.x, 0f, this.forceAngles.y) * (this.buoyancyFactor * 2f) * this.forceSpeed * this.waveFac * this.splitFac * this.forceAmount, base.transform.position);
						}
					}
				}
				if (this.keepAtSurface && this.isOver == 1f)
				{
					this.saveKeepAtSurface = this.keepAtSurface;
					if (this.surfaceLevel - this.physTarget.position.y - this.buoyancyOffset >= -0.25f)
					{
						if (this.rigidbodyComponent != null && !this.rigidbodyComponent.isKinematic)
						{
							this.saveRigidbodyState = false;
							this.rigidbodyComponent.isKinematic = true;
						}
						this.physPosition = this.physTarget.position;
						this.physPosition.y = Mathf.Lerp(this.startYPosition, this.targetYPosition, this.lerpSurfacePosTime);
						this.physTarget.position = this.physPosition;
					}
					else
					{
						this.rigidbodyComponent.isKinematic = this.saveRigidbodyState;
					}
					this.lerpSurfacePosTime += Time.deltaTime * 4f;
					if (this.lerpSurfacePosTime > 1f || this.keepAtSurface != this.saveKeepAtSurface)
					{
						this.lerpSurfacePosTime = 0f;
						this.startYPosition = this.physTarget.position.y;
						this.targetYPosition = this.surfaceLevel - this.buoyancyOffset;
					}
				}
			}
		}

		// Token: 0x04002C94 RID: 11412
		public bool applyToParent;

		// Token: 0x04002C95 RID: 11413
		public bool engageBuoyancy;

		// Token: 0x04002C96 RID: 11414
		public float activationRange = 5000f;

		// Token: 0x04002C97 RID: 11415
		public bool inheritForce;

		// Token: 0x04002C98 RID: 11416
		public bool keepAtSurface;

		// Token: 0x04002C99 RID: 11417
		public float buoyancyOffset;

		// Token: 0x04002C9A RID: 11418
		public float buoyancyStrength = 1f;

		// Token: 0x04002C9B RID: 11419
		public float forceAmount = 1f;

		// Token: 0x04002C9C RID: 11420
		public float forceHeightFactor;

		// Token: 0x04002C9D RID: 11421
		private float maxVerticalSpeed = 5f;

		// Token: 0x04002C9E RID: 11422
		private float surfaceRange = 0.2f;

		// Token: 0x04002C9F RID: 11423
		private float buoyancy;

		// Token: 0x04002CA0 RID: 11424
		private float surfaceLevel;

		// Token: 0x04002CA1 RID: 11425
		private float underwaterLevel;

		// Token: 0x04002CA2 RID: 11426
		private bool isUnderwater;

		// Token: 0x04002CA3 RID: 11427
		private Transform physTarget;

		// Token: 0x04002CA4 RID: 11428
		private SuimonoModule moduleObject;

		// Token: 0x04002CA5 RID: 11429
		private float waveHeight;

		// Token: 0x04002CA6 RID: 11430
		private float modTime;

		// Token: 0x04002CA7 RID: 11431
		private float splitFac = 1f;

		// Token: 0x04002CA8 RID: 11432
		private Rigidbody rigidbodyComponent;

		// Token: 0x04002CA9 RID: 11433
		private float isOver;

		// Token: 0x04002CAA RID: 11434
		private Vector2 forceAngles = new Vector2(0f, 0f);

		// Token: 0x04002CAB RID: 11435
		private float forceSpeed;

		// Token: 0x04002CAC RID: 11436
		private float waveHt;

		// Token: 0x04002CAD RID: 11437
		private int randSeed;

		// Token: 0x04002CAE RID: 11438
		private Suimono.Core.Random buyRand;

		// Token: 0x04002CAF RID: 11439
		private Vector3 gizPos;

		// Token: 0x04002CB0 RID: 11440
		private float testObjectHeight;

		// Token: 0x04002CB1 RID: 11441
		private float buoyancyFactor;

		// Token: 0x04002CB2 RID: 11442
		private float forceMod;

		// Token: 0x04002CB3 RID: 11443
		private float waveFac;

		// Token: 0x04002CB4 RID: 11444
		private float[] heightValues;

		// Token: 0x04002CB5 RID: 11445
		private bool isEnabled = true;

		// Token: 0x04002CB6 RID: 11446
		private bool performHeight;

		// Token: 0x04002CB7 RID: 11447
		private float currRange = -1f;

		// Token: 0x04002CB8 RID: 11448
		private Vector3 physPosition;

		// Token: 0x04002CB9 RID: 11449
		private bool saveRigidbodyState;

		// Token: 0x04002CBA RID: 11450
		private float lerpSurfacePosTime;

		// Token: 0x04002CBB RID: 11451
		private float targetYPosition;

		// Token: 0x04002CBC RID: 11452
		private float startYPosition;

		// Token: 0x04002CBD RID: 11453
		private bool saveKeepAtSurface;
	}
}
