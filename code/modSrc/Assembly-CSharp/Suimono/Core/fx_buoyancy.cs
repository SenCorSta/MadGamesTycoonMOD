using System;
using UnityEngine;

namespace Suimono.Core
{
	
	public class fx_buoyancy : MonoBehaviour
	{
		
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_buoy.psd", true);
			this.gizPos.y = this.gizPos.y - 0.03f;
		}

		
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

		
		public bool applyToParent;

		
		public bool engageBuoyancy;

		
		public float activationRange = 5000f;

		
		public bool inheritForce;

		
		public bool keepAtSurface;

		
		public float buoyancyOffset;

		
		public float buoyancyStrength = 1f;

		
		public float forceAmount = 1f;

		
		public float forceHeightFactor;

		
		private float maxVerticalSpeed = 5f;

		
		private float surfaceRange = 0.2f;

		
		private float buoyancy;

		
		private float surfaceLevel;

		
		private float underwaterLevel;

		
		private bool isUnderwater;

		
		private Transform physTarget;

		
		private SuimonoModule moduleObject;

		
		private float waveHeight;

		
		private float modTime;

		
		private float splitFac = 1f;

		
		private Rigidbody rigidbodyComponent;

		
		private float isOver;

		
		private Vector2 forceAngles = new Vector2(0f, 0f);

		
		private float forceSpeed;

		
		private float waveHt;

		
		private int randSeed;

		
		private Suimono.Core.Random buyRand;

		
		private Vector3 gizPos;

		
		private float testObjectHeight;

		
		private float buoyancyFactor;

		
		private float forceMod;

		
		private float waveFac;

		
		private float[] heightValues;

		
		private bool isEnabled = true;

		
		private bool performHeight;

		
		private float currRange = -1f;

		
		private Vector3 physPosition;

		
		private bool saveRigidbodyState;

		
		private float lerpSurfacePosTime;

		
		private float targetYPosition;

		
		private float startYPosition;

		
		private bool saveKeepAtSurface;
	}
}
