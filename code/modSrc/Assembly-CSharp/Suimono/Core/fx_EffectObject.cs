using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A6 RID: 934
	[ExecuteInEditMode]
	public class fx_EffectObject : MonoBehaviour
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x0016C0E1 File Offset: 0x0016A2E1
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_fxobj.psd", true);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0016C11C File Offset: 0x0016A31C
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

		// Token: 0x060022BF RID: 8895 RVA: 0x0016C210 File Offset: 0x0016A410
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

		// Token: 0x060022C0 RID: 8896 RVA: 0x0016C438 File Offset: 0x0016A638
		private void EmitSoundFX()
		{
			if (this.actionPass && this.audioObj != null && this.moduleObject != null && this.moduleObject.gameObject.activeInHierarchy && this.rulepass)
			{
				this.moduleObject.AddSoundFX(this.audioObj, this.emitPos, new Vector3(0f, this.fxRand.Next(this.audioPit.x, this.audioPit.y), this.fxRand.Next(this.audioVol.x, this.audioVol.y)));
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0016C4EC File Offset: 0x0016A6EC
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

		// Token: 0x060022C2 RID: 8898 RVA: 0x0016CAA0 File Offset: 0x0016ACA0
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

		// Token: 0x060022C3 RID: 8899 RVA: 0x0016CBBC File Offset: 0x0016ADBC
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

		// Token: 0x060022C4 RID: 8900 RVA: 0x0016CD1A File Offset: 0x0016AF1A
		private void OnDisable()
		{
			base.CancelInvoke("SetUpdate");
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0016CD28 File Offset: 0x0016AF28
		private void OnEnable()
		{
			fx_EffectObject.staggerOffset++;
			this.stagger = ((float)fx_EffectObject.staggerOffset + 0f) * 0.05f;
			fx_EffectObject.staggerOffset %= fx_EffectObject.staggerModulus;
			base.CancelInvoke("SetUpdate");
			base.InvokeRepeating("SetUpdate", 0.1f + this.stagger, 0.1f);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060022C6 RID: 8902 RVA: 0x0016CD90 File Offset: 0x0016AF90
		// (remove) Token: 0x060022C7 RID: 8903 RVA: 0x0016CDC8 File Offset: 0x0016AFC8
		public event fx_EffectObject.TriggerHandler OnTrigger;

		// Token: 0x060022C8 RID: 8904 RVA: 0x0016CE00 File Offset: 0x0016B000
		private void BroadcastEvent()
		{
			if (!this.moduleObject.isActiveAndEnabled || !this.rulepass || this.OnTrigger == null || this.timerEvent < this.eventInterval)
			{
				return;
			}
			this.timerEvent = 0f;
			this.OnTrigger(this.eventAtSurface ? new Vector3(this.emitPos.x, this.transf.position.y + this.currentWaterPos - 0.35f, this.emitPos.z) : this.transf.position, this.transf.rotation);
		}

		// Token: 0x04002C3F RID: 11327
		public SuimonoModuleFX fxObject;

		// Token: 0x04002C40 RID: 11328
		public Sui_FX_Rules[] effectRule;

		// Token: 0x04002C41 RID: 11329
		public float[] effectData;

		// Token: 0x04002C42 RID: 11330
		public Sui_FX_Rules[] resetRule;

		// Token: 0x04002C43 RID: 11331
		public string[] effectSystemName;

		// Token: 0x04002C44 RID: 11332
		public Sui_FX_System[] effectSystem;

		// Token: 0x04002C45 RID: 11333
		public Vector2 effectDelay = new Vector2(1f, 1f);

		// Token: 0x04002C46 RID: 11334
		public Vector2 emitTime = new Vector2(1f, 1f);

		// Token: 0x04002C47 RID: 11335
		public Vector2 emitNum = new Vector2(1f, 1f);

		// Token: 0x04002C48 RID: 11336
		public Vector2 effectSize = new Vector2(1f, 1f);

		// Token: 0x04002C49 RID: 11337
		public float emitSpeed;

		// Token: 0x04002C4A RID: 11338
		public float speedThreshold;

		// Token: 0x04002C4B RID: 11339
		public float directionMultiplier;

		// Token: 0x04002C4C RID: 11340
		public bool emitAtWaterLevel;

		// Token: 0x04002C4D RID: 11341
		public float effectDistance = 100f;

		// Token: 0x04002C4E RID: 11342
		public AudioClip audioObj;

		// Token: 0x04002C4F RID: 11343
		public Vector2 audioVol = new Vector2(0.9f, 1f);

		// Token: 0x04002C50 RID: 11344
		public Vector2 audioPit = new Vector2(0.8f, 1.2f);

		// Token: 0x04002C51 RID: 11345
		public float audioSpeed;

		// Token: 0x04002C52 RID: 11346
		public bool enableEvents;

		// Token: 0x04002C53 RID: 11347
		public Color tintCol = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002C54 RID: 11348
		public bool clampRot;

		// Token: 0x04002C55 RID: 11349
		public int actionIndex = 1;

		// Token: 0x04002C56 RID: 11350
		[NonSerialized]
		public List<string> actionOptions = new List<string>
		{
			"Once",
			"Repeat",
			"Specific"
		};

		// Token: 0x04002C57 RID: 11351
		public int actionNum = 5;

		// Token: 0x04002C58 RID: 11352
		public float actionReset = 15f;

		// Token: 0x04002C59 RID: 11353
		public int typeIndex;

		// Token: 0x04002C5A RID: 11354
		public int[] ruleIndex;

		// Token: 0x04002C5B RID: 11355
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

		// Token: 0x04002C5C RID: 11356
		public int systemIndex;

		// Token: 0x04002C5D RID: 11357
		public List<string> sysNames = new List<string>();

		// Token: 0x04002C5E RID: 11358
		public float currentSpeed;

		// Token: 0x04002C5F RID: 11359
		private int actionCount;

		// Token: 0x04002C60 RID: 11360
		private float actionTimer;

		// Token: 0x04002C61 RID: 11361
		private Vector3 savePos = new Vector3(0f, 0f, 0f);

		// Token: 0x04002C62 RID: 11362
		private SuimonoModule moduleObject;

		// Token: 0x04002C63 RID: 11363
		private float emitTimer;

		// Token: 0x04002C64 RID: 11364
		private bool delayPass = true;

		// Token: 0x04002C65 RID: 11365
		private bool actionPass = true;

		// Token: 0x04002C66 RID: 11366
		private float useSpd;

		// Token: 0x04002C67 RID: 11367
		private float useAudioSpd;

		// Token: 0x04002C68 RID: 11368
		private float isOverWater;

		// Token: 0x04002C69 RID: 11369
		private float currentWaterPos;

		// Token: 0x04002C6A RID: 11370
		private Vector3 emitPos;

		// Token: 0x04002C6B RID: 11371
		private bool rulepass;

		// Token: 0x04002C6C RID: 11372
		private float timerAudio;

		// Token: 0x04002C6D RID: 11373
		private float timerParticle;

		// Token: 0x04002C6E RID: 11374
		private float currentCamDistance;

		// Token: 0x04002C6F RID: 11375
		private Vector3 gizPos;

		// Token: 0x04002C70 RID: 11376
		private int sN;

		// Token: 0x04002C71 RID: 11377
		private int s;

		// Token: 0x04002C72 RID: 11378
		private bool[] ruleCheck;

		// Token: 0x04002C73 RID: 11379
		private int ruleCKNum;

		// Token: 0x04002C74 RID: 11380
		private bool[] resetCheck;

		// Token: 0x04002C75 RID: 11381
		private int rCK;

		// Token: 0x04002C76 RID: 11382
		private int emitN;

		// Token: 0x04002C77 RID: 11383
		private float emitS;

		// Token: 0x04002C78 RID: 11384
		private Vector3 emitV;

		// Token: 0x04002C79 RID: 11385
		private float emitR;

		// Token: 0x04002C7A RID: 11386
		private float emitAR;

		// Token: 0x04002C7B RID: 11387
		private bool rp;

		// Token: 0x04002C7C RID: 11388
		private float ruleData;

		// Token: 0x04002C7D RID: 11389
		private float depth;

		// Token: 0x04002C7E RID: 11390
		private Sui_FX_Rules[] tempRules;

		// Token: 0x04002C7F RID: 11391
		private int[] tempIndex;

		// Token: 0x04002C80 RID: 11392
		private float[] tempData;

		// Token: 0x04002C81 RID: 11393
		private int aR;

		// Token: 0x04002C82 RID: 11394
		private int endLP;

		// Token: 0x04002C83 RID: 11395
		private int setInt;

		// Token: 0x04002C84 RID: 11396
		private float[] heightValues;

		// Token: 0x04002C85 RID: 11397
		private Transform transf;

		// Token: 0x04002C86 RID: 11398
		private int randSeed;

		// Token: 0x04002C87 RID: 11399
		private Suimono.Core.Random fxRand;

		// Token: 0x04002C88 RID: 11400
		private static int staggerOffset = 0;

		// Token: 0x04002C89 RID: 11401
		private static int staggerModulus = 20;

		// Token: 0x04002C8A RID: 11402
		private float stagger;

		// Token: 0x04002C8B RID: 11403
		private float _deltaTime;

		// Token: 0x04002C8D RID: 11405
		public float eventInterval = 1f;

		// Token: 0x04002C8E RID: 11406
		public bool eventAtSurface;

		// Token: 0x04002C8F RID: 11407
		private float timerEvent;

		// Token: 0x020003A7 RID: 935
		// (Invoke) Token: 0x060022CC RID: 8908
		public delegate void TriggerHandler(Vector3 position, Quaternion rotatoin);
	}
}
