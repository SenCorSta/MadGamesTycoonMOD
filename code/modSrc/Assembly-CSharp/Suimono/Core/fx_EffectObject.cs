using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class fx_EffectObject : MonoBehaviour
	{
		
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_fxobj.psd", true);
		}

		
		private void Start()
		{
			this.transf = base.transform;
			if (GameObject.Find("SUIMONO_Module"))
			{
				this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				if (this.moduleObject != null)
				{
					this.fxObject = this.moduleObject.suimonoModuleLibrary.fxObject;
				}
			}
			if (this.fxObject != null)
			{
				this.sysNames = this.fxObject.sysNames;
			}
			this.randSeed = Environment.TickCount;
			this.fxRand = new Suimono.Core.Random(this.randSeed);
			fx_EffectObject.staggerOffset++;
			this.stagger = ((float)fx_EffectObject.staggerOffset + 0f) * 0.05f;
			fx_EffectObject.staggerOffset %= fx_EffectObject.staggerModulus;
			base.InvokeRepeating("SetUpdate", 0.1f + this.stagger, 0.033333335f);
		}

		
		private void SetUpdate()
		{
			if (this.moduleObject != null)
			{
				this._deltaTime = Time.deltaTime;
				this.actionPass = false;
				if (this.actionIndex == 0 && this.actionCount < 1)
				{
					this.actionPass = true;
				}
				if (this.actionIndex == 2 && this.actionCount < this.actionNum)
				{
					this.actionPass = true;
				}
				if (this.actionIndex == 1)
				{
					this.actionPass = true;
				}
				if (this.actionCount > 0 && (this.actionIndex == 0 || this.actionIndex == 2))
				{
					this.actionTimer += this._deltaTime;
					if (this.actionTimer > this.actionReset && this.actionReset > 0f)
					{
						this.actionCount = 0;
						this.actionTimer = 0f;
					}
				}
				if (this.fxRand == null)
				{
					this.fxRand = new Suimono.Core.Random(this.randSeed);
				}
				if (Application.isPlaying)
				{
					if (this.moduleObject.setTrack != null)
					{
						this.currentCamDistance = Vector3.Distance(this.transf.position, this.moduleObject.setTrack.transform.position);
						if (this.currentCamDistance <= this.effectDistance)
						{
							if (this.savePos != this.transf.position)
							{
								this.currentSpeed = Vector3.Distance(this.savePos, new Vector3(this.transf.position.x, this.transf.position.y, this.transf.position.z)) / this._deltaTime;
							}
							this.savePos = this.transf.position;
							this.timerParticle += this._deltaTime;
							this.timerAudio += this._deltaTime;
							this.EmitFX();
							if (this.timerAudio > this.audioSpeed)
							{
								this.timerAudio = 0f;
								this.EmitSoundFX();
							}
						}
					}
					if (this.enableEvents)
					{
						this.timerEvent += this._deltaTime;
						this.BroadcastEvent();
					}
				}
			}
		}

		
		private void EmitSoundFX()
		{
			if (this.actionPass && this.audioObj != null && this.moduleObject != null && this.moduleObject.gameObject.activeInHierarchy && this.rulepass)
			{
				this.moduleObject.AddSoundFX(this.audioObj, this.emitPos, new Vector3(0f, this.fxRand.Next(this.audioPit.x, this.audioPit.y), this.fxRand.Next(this.audioVol.x, this.audioVol.y)));
			}
		}

		
		private void EmitFX()
		{
			if (this.moduleObject.enableInteraction && Application.isPlaying && this.moduleObject != null && this.moduleObject.gameObject.activeInHierarchy && this.actionPass)
			{
				this.delayPass = false;
				this.emitTimer += this._deltaTime;
				if (this.emitTimer >= this.emitSpeed)
				{
					this.emitTimer = 0f;
					this.delayPass = true;
				}
				this.heightValues = this.moduleObject.SuimonoGetHeightAll(this.transf.position);
				this.currentWaterPos = this.heightValues[3];
				this.isOverWater = this.heightValues[4];
				this.rulepass = false;
				if (this.ruleCheck == null)
				{
					this.ruleCheck = new bool[this.effectRule.Length];
				}
				this.ruleCKNum = 0;
				if (this.resetCheck == null)
				{
					this.resetCheck = new bool[this.resetRule.Length];
				}
				if (Application.isPlaying)
				{
					this.rCK = 0;
					while (this.rCK < this.effectRule.Length)
					{
						bool flag = false;
						this.ruleData = this.speedThreshold;
						this.depth = this.currentWaterPos;
						if (this.rCK < this.effectData.Length)
						{
							this.ruleData = this.effectData[this.rCK];
						}
						if (this.ruleIndex[this.rCK] == 0)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 1 && this.isOverWater == 1f && this.depth > 0f)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 2 && this.isOverWater == 1f && this.depth <= 0f)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 3 && this.isOverWater == 1f && this.depth < 0.15f && this.depth > -0.15f)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 4 && this.isOverWater == 1f && this.currentSpeed > this.ruleData)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 5 && this.isOverWater == 1f && this.currentSpeed < this.ruleData)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 6 && this.isOverWater == 1f && this.depth > this.ruleData)
						{
							flag = true;
						}
						if (this.ruleIndex[this.rCK] == 7 && this.isOverWater == 1f && this.depth < this.ruleData)
						{
							flag = true;
						}
						this.ruleCheck[this.rCK] = flag;
						this.rCK++;
					}
				}
				this.rCK = 0;
				while (this.rCK < this.effectRule.Length)
				{
					if (this.ruleCheck[this.rCK])
					{
						this.ruleCKNum++;
					}
					this.rCK++;
				}
				if (this.ruleCKNum == this.effectRule.Length)
				{
					this.rulepass = true;
				}
				if (this.effectRule.Length == 0)
				{
					this.rulepass = true;
				}
				if (this.delayPass && this.rulepass)
				{
					this.emitN = Mathf.FloorToInt(this.fxRand.Next(this.emitNum.x, this.emitNum.y));
					this.emitS = this.fxRand.Next(this.effectSize.x, this.effectSize.y);
					this.emitV = new Vector3(0f, 0f, 0f);
					this.emitPos = base.transform.position;
					this.emitR = base.transform.eulerAngles.y - 180f;
					if (!this.clampRot)
					{
						this.emitR = this.fxRand.Next(-30f, 10f);
					}
					this.emitAR = this.fxRand.Next(-360f, 360f);
					if (this.emitAtWaterLevel)
					{
						this.emitPos = new Vector3(this.emitPos.x, base.transform.position.y + this.currentWaterPos - 0.35f, this.emitPos.z);
					}
					if (this.directionMultiplier > 0f)
					{
						this.emitV = base.transform.up * (this.directionMultiplier * Mathf.Clamp01(this.currentSpeed / this.speedThreshold));
					}
					if (this.timerParticle > this.emitSpeed)
					{
						this.timerParticle = 0f;
						if (this.systemIndex - 1 >= 0)
						{
							this.emitPos.y = this.emitPos.y + this.emitS * 0.4f;
							this.emitPos.x = this.emitPos.x + this.fxRand.Next(-0.2f, 0.2f);
							this.emitPos.z = this.emitPos.z + this.fxRand.Next(-0.2f, 0.2f);
							this.moduleObject.AddFX(this.systemIndex - 1, this.emitPos, this.emitN, this.fxRand.Next(0.5f, 0.75f) * this.emitS, this.emitR, this.emitAR, this.emitV, this.tintCol);
						}
						this.actionCount++;
					}
				}
			}
		}

		
		public void AddRule()
		{
			this.tempRules = this.effectRule;
			this.tempIndex = this.ruleIndex;
			this.tempData = this.effectData;
			this.effectRule = new Sui_FX_Rules[this.tempRules.Length + 1];
			this.ruleIndex = new int[this.tempRules.Length + 1];
			this.effectData = new float[this.tempRules.Length + 1];
			this.aR = 0;
			while (this.aR < this.tempRules.Length)
			{
				this.effectRule[this.aR] = this.tempRules[this.aR];
				this.ruleIndex[this.aR] = this.tempIndex[this.aR];
				this.effectData[this.aR] = this.tempData[this.aR];
				this.aR++;
			}
			this.effectRule[this.tempRules.Length] = Sui_FX_Rules.none;
			this.ruleIndex[this.tempRules.Length] = 0;
			this.effectData[this.tempRules.Length] = 0f;
		}

		
		public void DeleteRule(int ruleNum)
		{
			this.tempRules = this.effectRule;
			this.tempIndex = this.ruleIndex;
			this.tempData = this.effectData;
			this.endLP = this.tempRules.Length - 1;
			if (this.endLP <= 0)
			{
				this.endLP = 0;
				this.effectRule = new Sui_FX_Rules[0];
				this.ruleIndex = new int[0];
				this.effectData = new float[0];
				return;
			}
			this.effectRule = new Sui_FX_Rules[this.endLP];
			this.ruleIndex = new int[this.endLP];
			this.effectData = new float[this.endLP];
			this.setInt = -1;
			this.aR = 0;
			while (this.aR <= this.endLP)
			{
				if (this.aR != ruleNum)
				{
					this.setInt++;
				}
				else
				{
					this.setInt += 2;
				}
				if (this.setInt <= this.endLP)
				{
					this.effectRule[this.aR] = this.tempRules[this.setInt];
					this.ruleIndex[this.aR] = this.tempIndex[this.setInt];
					this.effectData[this.aR] = this.tempData[this.setInt];
				}
				this.aR++;
			}
		}

		
		private void OnDisable()
		{
			base.CancelInvoke("SetUpdate");
		}

		
		private void OnEnable()
		{
			fx_EffectObject.staggerOffset++;
			this.stagger = ((float)fx_EffectObject.staggerOffset + 0f) * 0.05f;
			fx_EffectObject.staggerOffset %= fx_EffectObject.staggerModulus;
			base.CancelInvoke("SetUpdate");
			base.InvokeRepeating("SetUpdate", 0.1f + this.stagger, 0.1f);
		}

		
		// (add) Token: 0x06002273 RID: 8819 RVA: 0x0016B4F4 File Offset: 0x001696F4
		// (remove) Token: 0x06002274 RID: 8820 RVA: 0x0016B52C File Offset: 0x0016972C
		public event fx_EffectObject.TriggerHandler OnTrigger;

		
		private void BroadcastEvent()
		{
			if (!this.moduleObject.isActiveAndEnabled || !this.rulepass || this.OnTrigger == null || this.timerEvent < this.eventInterval)
			{
				return;
			}
			this.timerEvent = 0f;
			this.OnTrigger(this.eventAtSurface ? new Vector3(this.emitPos.x, this.transf.position.y + this.currentWaterPos - 0.35f, this.emitPos.z) : this.transf.position, this.transf.rotation);
		}

		
		public SuimonoModuleFX fxObject;

		
		public Sui_FX_Rules[] effectRule;

		
		public float[] effectData;

		
		public Sui_FX_Rules[] resetRule;

		
		public string[] effectSystemName;

		
		public Sui_FX_System[] effectSystem;

		
		public Vector2 effectDelay = new Vector2(1f, 1f);

		
		public Vector2 emitTime = new Vector2(1f, 1f);

		
		public Vector2 emitNum = new Vector2(1f, 1f);

		
		public Vector2 effectSize = new Vector2(1f, 1f);

		
		public float emitSpeed;

		
		public float speedThreshold;

		
		public float directionMultiplier;

		
		public bool emitAtWaterLevel;

		
		public float effectDistance = 100f;

		
		public AudioClip audioObj;

		
		public Vector2 audioVol = new Vector2(0.9f, 1f);

		
		public Vector2 audioPit = new Vector2(0.8f, 1.2f);

		
		public float audioSpeed;

		
		public bool enableEvents;

		
		public Color tintCol = new Color(1f, 1f, 1f, 1f);

		
		public bool clampRot;

		
		public int actionIndex = 1;

		
		[NonSerialized]
		public List<string> actionOptions = new List<string>
		{
			"Once",
			"Repeat",
			"Specific"
		};

		
		public int actionNum = 5;

		
		public float actionReset = 15f;

		
		public int typeIndex;

		
		public int[] ruleIndex;

		
		[NonSerialized]
		public List<string> ruleOptions = new List<string>
		{
			"None",
			"Object Is Underwater",
			"Object Is Above Water",
			"Object Is At Surface",
			"Object Speed Is Greater Than",
			"Object Speed Is Less Than",
			"Water Depth Is Greater Than",
			"Water Depth Is Less Than"
		};

		
		public int systemIndex;

		
		public List<string> sysNames = new List<string>();

		
		public float currentSpeed;

		
		private int actionCount;

		
		private float actionTimer;

		
		private Vector3 savePos = new Vector3(0f, 0f, 0f);

		
		private SuimonoModule moduleObject;

		
		private float emitTimer;

		
		private bool delayPass = true;

		
		private bool actionPass = true;

		
		private float useSpd;

		
		private float useAudioSpd;

		
		private float isOverWater;

		
		private float currentWaterPos;

		
		private Vector3 emitPos;

		
		private bool rulepass;

		
		private float timerAudio;

		
		private float timerParticle;

		
		private float currentCamDistance;

		
		private Vector3 gizPos;

		
		private int sN;

		
		private int s;

		
		private bool[] ruleCheck;

		
		private int ruleCKNum;

		
		private bool[] resetCheck;

		
		private int rCK;

		
		private int emitN;

		
		private float emitS;

		
		private Vector3 emitV;

		
		private float emitR;

		
		private float emitAR;

		
		private bool rp;

		
		private float ruleData;

		
		private float depth;

		
		private Sui_FX_Rules[] tempRules;

		
		private int[] tempIndex;

		
		private float[] tempData;

		
		private int aR;

		
		private int endLP;

		
		private int setInt;

		
		private float[] heightValues;

		
		private Transform transf;

		
		private int randSeed;

		
		private Suimono.Core.Random fxRand;

		
		private static int staggerOffset = 0;

		
		private static int staggerModulus = 20;

		
		private float stagger;

		
		private float _deltaTime;

		
		public float eventInterval = 1f;

		
		public bool eventAtSurface;

		
		private float timerEvent;

		
		// (Invoke) Token: 0x06002279 RID: 8825
		public delegate void TriggerHandler(Vector3 position, Quaternion rotatoin);
	}
}
