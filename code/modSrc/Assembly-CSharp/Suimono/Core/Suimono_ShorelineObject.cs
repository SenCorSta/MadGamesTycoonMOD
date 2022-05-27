using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039E RID: 926
	[ExecuteInEditMode]
	public class Suimono_ShorelineObject : MonoBehaviour
	{
		// Token: 0x0600224A RID: 8778 RVA: 0x00167F3C File Offset: 0x0016613C
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_shore.psd", true);
			this.gizPos.y = this.gizPos.y - 0.03f;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00167F94 File Offset: 0x00166194
		private void Start()
		{
			if (Application.isPlaying)
			{
				this.debug = false;
			}
			if (GameObject.Find("SUIMONO_Module") != null)
			{
				this.moduleObject = (SuimonoModule)UnityEngine.Object.FindObjectOfType(typeof(SuimonoModule));
				this.suimonoVersionNumber = this.moduleObject.suimonoVersionNumber;
			}
			this.CamInfo = base.transform.Find("cam_LocalShore").gameObject.GetComponent<Camera>();
			this.CamInfo.depthTextureMode = DepthTextureMode.DepthNormals;
			this.CamTools = base.transform.Find("cam_LocalShore").gameObject.GetComponent<cameraTools>();
			this.CamDepth = base.transform.Find("cam_LocalShore").gameObject.GetComponent<SuimonoCamera_depth>();
			this.renderObject = base.gameObject.GetComponent<Renderer>();
			this.meshObject = base.gameObject.GetComponent<MeshFilter>();
			if (base.transform.parent && base.transform.parent.gameObject.GetComponent<SuimonoObject>() != null)
			{
				this.attachToSurface = base.transform.parent;
			}
			if (this.attachToSurface != null)
			{
				this.attachToSurface.Find("Suimono_Object").gameObject.GetComponent<Renderer>().enabled = true;
			}
			this.matObject = new Material(Shader.Find("Suimono2/Suimono2_FX_ShorelineObject"));
			this.renderObject.material = this.matObject;
			this.hasRendered = false;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00168118 File Offset: 0x00166318
		private void LateUpdate()
		{
			if (this.moduleObject != null)
			{
				this.suimonoVersionNumber = this.moduleObject.suimonoVersionNumber;
				base.gameObject.layer = this.moduleObject.layerDepthNum;
				this.CamInfo.gameObject.layer = this.moduleObject.layerDepthNum;
				this.CamInfo.farClipPlane = this.maxDepth;
				base.gameObject.tag = "Untagged";
				this.CamInfo.gameObject.tag = "Untagged";
				this.suiLayerMasks = new List<string>();
				this.i = 0;
				while (this.i < 32)
				{
					this.layerName = LayerMask.LayerToName(this.i);
					this.suiLayerMasks.Add(this.layerName);
					this.i++;
				}
				if (!Application.isPlaying && this.attachToSurface != null)
				{
					if (this.debug)
					{
						this.attachToSurface.Find("Suimono_Object").gameObject.GetComponent<Renderer>().enabled = false;
					}
					else
					{
						this.attachToSurface.Find("Suimono_Object").gameObject.GetComponent<Renderer>().enabled = true;
					}
				}
				if (this.shorelineModeIndex == 0)
				{
					if (this.CamInfo != null)
					{
						this.CamInfo.enabled = true;
						this.CamInfo.cullingMask = this.depthLayer;
					}
				}
				else if (this.CamInfo != null)
				{
					this.CamInfo.enabled = false;
				}
				if (this.debug)
				{
					if (this.renderObject != null)
					{
						this.renderObject.hideFlags = HideFlags.None;
					}
					if (this.meshObject != null)
					{
						this.meshObject.hideFlags = HideFlags.None;
					}
					if (this.matObject != null)
					{
						this.matObject.hideFlags = HideFlags.None;
					}
					if (this.shorelineModeIndex == 0)
					{
						if (this.CamInfo != null)
						{
							this.CamInfo.gameObject.hideFlags = HideFlags.None;
						}
						if (this.CamTools != null)
						{
							this.CamTools.executeInEditMode = true;
							this.CamTools.CameraUpdate();
						}
					}
					if (this.renderObject != null)
					{
						this.renderObject.enabled = true;
					}
				}
				else
				{
					if (this.renderObject != null)
					{
						this.renderObject.hideFlags = HideFlags.HideInInspector;
					}
					if (this.meshObject != null)
					{
						this.meshObject.hideFlags = HideFlags.HideInInspector;
					}
					if (this.matObject != null)
					{
						this.matObject.hideFlags = HideFlags.HideInInspector;
					}
					if (this.shorelineModeIndex == 0)
					{
						if (this.CamInfo != null)
						{
							this.CamInfo.gameObject.hideFlags = HideFlags.HideInHierarchy;
						}
						if (this.CamTools != null)
						{
							this.CamTools.executeInEditMode = false;
						}
					}
					if (!Application.isPlaying && this.renderObject != null)
					{
						this.renderObject.enabled = false;
					}
					else
					{
						this.renderObject.enabled = true;
					}
				}
				if (this.saveMode != this.shorelineModeIndex)
				{
					this.saveMode = this.shorelineModeIndex;
					this.hasRendered = false;
				}
				this.renderPass = true;
				if (this.shorelineModeIndex == 0)
				{
					if (this.shorelineRunIndex == 0 && this.hasRendered && Application.isPlaying)
					{
						this.renderPass = false;
					}
					if (this.shorelineRunIndex == 1)
					{
						this.renderPass = true;
					}
				}
				if (this.shorelineModeIndex == 1 && this.hasRendered && Application.isPlaying)
				{
					this.renderPass = false;
				}
				if (!this.renderPass)
				{
					if (this.CamInfo != null)
					{
						this.CamInfo.enabled = false;
					}
					if (this.CamTools != null)
					{
						this.CamTools.enabled = false;
						return;
					}
				}
				else
				{
					if (this.CamInfo != null)
					{
						this.CamInfo.enabled = true;
					}
					if (this.CamTools != null)
					{
						this.CamTools.enabled = true;
					}
					if (this.CamDepth != null)
					{
						this.CamDepth.enabled = true;
					}
					if (this.shorelineModeIndex == 0)
					{
						this.CamDepth._sceneDepth = this.sceneDepth;
						this.CamDepth._shoreDepth = this.shoreDepth;
					}
					if (this.attachToSurface != null)
					{
						base.transform.localScale = new Vector3(base.transform.localScale.x, 1f, base.transform.localScale.z);
						if (this.attachToSurface != null && this.autoPosition)
						{
							base.transform.position = new Vector3(base.transform.position.x, this.attachToSurface.position.y, base.transform.position.z);
						}
						if (this.shorelineModeIndex == 0)
						{
							this.maxScale = Mathf.Max(base.transform.localScale.x, base.transform.localScale.z);
							this.CamInfo.orthographicSize = this.maxScale * 20f;
							if (base.transform.localScale.x < base.transform.localScale.z)
							{
								this.camCoords = new Vector4(base.transform.localScale.x / base.transform.localScale.z, 1f, 0.5f - base.transform.localScale.x / base.transform.localScale.z * 0.5f, 0f);
							}
							else if (base.transform.localScale.x > base.transform.localScale.z)
							{
								this.camCoords = new Vector4(1f, base.transform.localScale.z / base.transform.localScale.x, 0f, 0.5f - base.transform.localScale.z / base.transform.localScale.x * 0.5f);
							}
							this.CamTools.surfaceRenderer.sharedMaterial.SetColor("_Mult", this.camCoords);
							if (this.CamTools != null)
							{
								if (this.currPos != base.transform.position)
								{
									this.currPos = base.transform.position;
									this.CamTools.CameraUpdate();
								}
								if (this.currScale != base.transform.localScale)
								{
									this.currScale = base.transform.localScale;
									this.CamTools.CameraUpdate();
								}
								if (this.currRot != base.transform.rotation)
								{
									this.currRot = base.transform.rotation;
									this.CamTools.CameraUpdate();
								}
								if (this.curr_sceneDepth != this.sceneDepth)
								{
									this.curr_sceneDepth = this.sceneDepth;
									this.CamTools.CameraUpdate();
								}
								if (this.curr_shoreDepth != this.shoreDepth)
								{
									this.curr_shoreDepth = this.shoreDepth;
									this.CamTools.CameraUpdate();
								}
								if (Application.isPlaying)
								{
									this.CamTools.CameraUpdate();
								}
							}
						}
						if (this.shorelineModeIndex == 1 && this.customDepthTex != null && this.renderObject != null)
						{
							this.renderObject.sharedMaterial.SetColor("_Mult", new Vector4(1f, 1f, 0f, 0f));
							this.renderObject.sharedMaterial.SetTexture("_MainTex", this.customDepthTex);
						}
						if (Application.isPlaying && Time.time > 1f)
						{
							this.hasRendered = true;
						}
					}
				}
			}
		}

		// Token: 0x04002B88 RID: 11144
		public int lodIndex;

		// Token: 0x04002B89 RID: 11145
		public int shorelineModeIndex;

		// Token: 0x04002B8A RID: 11146
		public List<string> shorelineModeOptions = new List<string>
		{
			"Auto-Generate",
			"Custom Texture"
		};

		// Token: 0x04002B8B RID: 11147
		public int shorelineRunIndex;

		// Token: 0x04002B8C RID: 11148
		public List<string> shorelineRunOptions = new List<string>
		{
			"At Start",
			"Continuous"
		};

		// Token: 0x04002B8D RID: 11149
		public Transform attachToSurface;

		// Token: 0x04002B8E RID: 11150
		public bool autoPosition = true;

		// Token: 0x04002B8F RID: 11151
		public float maxDepth = 25f;

		// Token: 0x04002B90 RID: 11152
		public float sceneDepth = 14.5f;

		// Token: 0x04002B91 RID: 11153
		public float shoreDepth = 27.7f;

		// Token: 0x04002B92 RID: 11154
		public bool debug;

		// Token: 0x04002B93 RID: 11155
		public string suimonoVersionNumber;

		// Token: 0x04002B94 RID: 11156
		public int depthLayer = 2;

		// Token: 0x04002B95 RID: 11157
		public List<string> suiLayerMasks = new List<string>();

		// Token: 0x04002B96 RID: 11158
		public Texture2D customDepthTex;

		// Token: 0x04002B97 RID: 11159
		public int useResolution = 512;

		// Token: 0x04002B98 RID: 11160
		private Material useMat;

		// Token: 0x04002B99 RID: 11161
		private Texture reflTex;

		// Token: 0x04002B9A RID: 11162
		private Texture envTex;

		// Token: 0x04002B9B RID: 11163
		private Matrix4x4 MV;

		// Token: 0x04002B9C RID: 11164
		private Camera CamInfo;

		// Token: 0x04002B9D RID: 11165
		private cameraTools CamTools;

		// Token: 0x04002B9E RID: 11166
		private SuimonoCamera_depth CamDepth;

		// Token: 0x04002B9F RID: 11167
		private float curr_sceneDepth;

		// Token: 0x04002BA0 RID: 11168
		private float curr_shoreDepth;

		// Token: 0x04002BA1 RID: 11169
		private float curr_foamDepth;

		// Token: 0x04002BA2 RID: 11170
		private float curr_edgeDepth;

		// Token: 0x04002BA3 RID: 11171
		private Vector3 currPos;

		// Token: 0x04002BA4 RID: 11172
		private Vector3 currScale;

		// Token: 0x04002BA5 RID: 11173
		private Quaternion currRot;

		// Token: 0x04002BA6 RID: 11174
		private Vector4 camCoords = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04002BA7 RID: 11175
		private Material localMaterial;

		// Token: 0x04002BA8 RID: 11176
		private Renderer renderObject;

		// Token: 0x04002BA9 RID: 11177
		private MeshFilter meshObject;

		// Token: 0x04002BAA RID: 11178
		private Material matObject;

		// Token: 0x04002BAB RID: 11179
		public SuimonoModule moduleObject;

		// Token: 0x04002BAC RID: 11180
		private float maxScale;

		// Token: 0x04002BAD RID: 11181
		private int i;

		// Token: 0x04002BAE RID: 11182
		private string layerName;

		// Token: 0x04002BAF RID: 11183
		private bool hasRendered;

		// Token: 0x04002BB0 RID: 11184
		private bool renderPass = true;

		// Token: 0x04002BB1 RID: 11185
		private int saveMode = -1;

		// Token: 0x04002BB2 RID: 11186
		private Vector3 gizPos;
	}
}
