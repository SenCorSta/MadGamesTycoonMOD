using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A3 RID: 931
	[ExecuteInEditMode]
	public class fx_EffectObject : MonoBehaviour
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x00017033 File Offset: 0x00015233
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_fxobj.psd", true);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0016A88C File Offset: 0x00168A8C
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

		// Token: 0x0600226C RID: 8812 RVA: 0x0016A980 File Offset: 0x00168B80
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

		// Token: 0x0600226D RID: 8813 RVA: 0x0016ABA8 File Offset: 0x00168DA8
		private void EmitSoundFX()
		{
			if (this.actionPass && this.audioObj != null && this.moduleObject != null && this.moduleObject.gameObject.activeInHierarchy && this.rulepass)
			{
				this.moduleObject.AddSoundFX(this.audioObj, this.emitPos, new Vector3(0f, this.fxRand.Next(this.audioPit.x, this.audioPit.y), this.fxRand.Next(this.audioVol.x, this.audioVol.y)));
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0016AC5C File Offset: 0x00168E5C
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

		// Token: 0x0600226F RID: 8815 RVA: 0x0016B210 File Offset: 0x00169410
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

		// Token: 0x06002270 RID: 8816 RVA: 0x0016B32C File Offset: 0x0016952C
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

		// Token: 0x06002271 RID: 8817 RVA: 0x0001706B File Offset: 0x0001526B
		private void OnDisable()
		{
			base.CancelInvoke("SetUpdate");
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0016B48C File Offset: 0x0016968C
		private void OnEnable()
		{
			fx_EffectObject.staggerOffset++;
			this.stagger = ((float)fx_EffectObject.staggerOffset + 0f) * 0.05f;
			fx_EffectObject.staggerOffset %= fx_EffectObject.staggerModulus;
			base.CancelInvoke("SetUpdate");
			base.InvokeRepeating("SetUpdate", 0.1f + this.stagger, 0.1f);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06002273 RID: 8819 RVA: 0x0016B4F4 File Offset: 0x001696F4
		// (remove) Token: 0x06002274 RID: 8820 RVA: 0x0016B52C File Offset: 0x0016972C
		public event fx_EffectObject.TriggerHandler OnTrigger;

		// Token: 0x06002275 RID: 8821 RVA: 0x0016B564 File Offset: 0x00169764
		private void BroadcastEvent()
		{
			if (!this.moduleObject.isActiveAndEnabled || !this.rulepass || this.OnTrigger == null || this.timerEvent < this.eventInterval)
			{
				return;
			}
			this.timerEvent = 0f;
			this.OnTrigger(this.eventAtSurface ? new Vector3(this.emitPos.x, this.transf.position.y + this.currentWaterPos - 0.35f, this.emitPos.z) : this.transf.position, this.transf.rotation);
		}

		// Token: 0x04002C29 RID: 11305
		public SuimonoModuleFX fxObject;

		// Token: 0x04002C2A RID: 11306
		public Sui_FX_Rules[] effectRule;

		// Token: 0x04002C2B RID: 11307
		public float[] effectData;

		// Token: 0x04002C2C RID: 11308
		public Sui_FX_Rules[] resetRule;

		// Token: 0x04002C2D RID: 11309
		public string[] effectSystemName;

		// Token: 0x04002C2E RID: 11310
		public Sui_FX_System[] effectSystem;

		// Token: 0x04002C2F RID: 11311
		public Vector2 effectDelay = new Vector2(1f, 1f);

		// Token: 0x04002C30 RID: 11312
		public Vector2 emitTime = new Vector2(1f, 1f);

		// Token: 0x04002C31 RID: 11313
		public Vector2 emitNum = new Vector2(1f, 1f);

		// Token: 0x04002C32 RID: 11314
		public Vector2 effectSize = new Vector2(1f, 1f);

		// Token: 0x04002C33 RID: 11315
		public float emitSpeed;

		// Token: 0x04002C34 RID: 11316
		public float speedThreshold;

		// Token: 0x04002C35 RID: 11317
		public float directionMultiplier;

		// Token: 0x04002C36 RID: 11318
		public bool emitAtWaterLevel;

		// Token: 0x04002C37 RID: 11319
		public float effectDistance = 100f;

		// Token: 0x04002C38 RID: 11320
		public AudioClip audioObj;

		// Token: 0x04002C39 RID: 11321
		public Vector2 audioVol = new Vector2(0.9f, 1f);

		// Token: 0x04002C3A RID: 11322
		public Vector2 audioPit = new Vector2(0.8f, 1.2f);

		// Token: 0x04002C3B RID: 11323
		public float audioSpeed;

		// Token: 0x04002C3C RID: 11324
		public bool enableEvents;

		// Token: 0x04002C3D RID: 11325
		public Color tintCol = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002C3E RID: 11326
		public bool clampRot;

		// Token: 0x04002C3F RID: 11327
		public int actionIndex = 1;

		// Token: 0x04002C40 RID: 11328
		[NonSerialized]
		public List<string> actionOptions = new List<string>
		{
			"Once",
			"Repeat",
			"Specific"
		};

		// Token: 0x04002C41 RID: 11329
		public int actionNum = 5;

		// Token: 0x04002C42 RID: 11330
		public float actionReset = 15f;

		// Token: 0x04002C43 RID: 11331
		public int typeIndex;

		// Token: 0x04002C44 RID: 11332
		public int[] ruleIndex;

		// Token: 0x04002C45 RID: 11333
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

		// Token: 0x04002C46 RID: 11334
		public int systemIndex;

		// Token: 0x04002C47 RID: 11335
		public List<string> sysNames = new List<string>();

		// Token: 0x04002C48 RID: 11336
		public float currentSpeed;

		// Token: 0x04002C49 RID: 11337
		private int actionCount;

		// Token: 0x04002C4A RID: 11338
		private float actionTimer;

		// Token: 0x04002C4B RID: 11339
		private Vector3 savePos = new Vector3(0f, 0f, 0f);

		// Token: 0x04002C4C RID: 11340
		private SuimonoModule moduleObject;

		// Token: 0x04002C4D RID: 11341
		private float emitTimer;

		// Token: 0x04002C4E RID: 11342
		private bool delayPass = true;

		// Token: 0x04002C4F RID: 11343
		private bool actionPass = true;

		// Token: 0x04002C50 RID: 11344
		private float useSpd;

		// Token: 0x04002C51 RID: 11345
		private float useAudioSpd;

		// Token: 0x04002C52 RID: 11346
		private float isOverWater;

		// Token: 0x04002C53 RID: 11347
		private float currentWaterPos;

		// Token: 0x04002C54 RID: 11348
		private Vector3 emitPos;

		// Token: 0x04002C55 RID: 11349
		private bool rulepass;

		// Token: 0x04002C56 RID: 11350
		private float timerAudio;

		// Token: 0x04002C57 RID: 11351
		private float timerParticle;

		// Token: 0x04002C58 RID: 11352
		private float currentCamDistance;

		// Token: 0x04002C59 RID: 11353
		private Vector3 gizPos;

		// Token: 0x04002C5A RID: 11354
		private int sN;

		// Token: 0x04002C5B RID: 11355
		private int s;

		// Token: 0x04002C5C RID: 11356
		private bool[] ruleCheck;

		// Token: 0x04002C5D RID: 11357
		private int ruleCKNum;

		// Token: 0x04002C5E RID: 11358
		private bool[] resetCheck;

		// Token: 0x04002C5F RID: 11359
		private int rCK;

		// Token: 0x04002C60 RID: 11360
		private int emitN;

		// Token: 0x04002C61 RID: 11361
		private float emitS;

		// Token: 0x04002C62 RID: 11362
		private Vector3 emitV;

		// Token: 0x04002C63 RID: 11363
		private float emitR;

		// Token: 0x04002C64 RID: 11364
		private float emitAR;

		// Token: 0x04002C65 RID: 11365
		private bool rp;

		// Token: 0x04002C66 RID: 11366
		private float ruleData;

		// Token: 0x04002C67 RID: 11367
		private float depth;

		// Token: 0x04002C68 RID: 11368
		private Sui_FX_Rules[] tempRules;

		// Token: 0x04002C69 RID: 11369
		private int[] tempIndex;

		// Token: 0x04002C6A RID: 11370
		private float[] tempData;

		// Token: 0x04002C6B RID: 11371
		private int aR;

		// Token: 0x04002C6C RID: 11372
		private int endLP;

		// Token: 0x04002C6D RID: 11373
		private int setInt;

		// Token: 0x04002C6E RID: 11374
		private float[] heightValues;

		// Token: 0x04002C6F RID: 11375
		private Transform transf;

		// Token: 0x04002C70 RID: 11376
		private int randSeed;

		// Token: 0x04002C71 RID: 11377
		private Suimono.Core.Random fxRand;

		// Token: 0x04002C72 RID: 11378
		private static int staggerOffset = 0;

		// Token: 0x04002C73 RID: 11379
		private static int staggerModulus = 20;

		// Token: 0x04002C74 RID: 11380
		private float stagger;

		// Token: 0x04002C75 RID: 11381
		private float _deltaTime;

		// Token: 0x04002C77 RID: 11383
		public float eventInterval = 1f;

		// Token: 0x04002C78 RID: 11384
		public bool eventAtSurface;

		// Token: 0x04002C79 RID: 11385
		private float timerEvent;

		// Token: 0x020003A4 RID: 932
		// (Invoke) Token: 0x06002279 RID: 8825
		public delegate void TriggerHandler(Vector3 position, Quaternion rotatoin);
	}
}
