using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x020003A1 RID: 929
	[ExecuteInEditMode]
	public class Suimono_ShorelineObject : MonoBehaviour
	{
		// Token: 0x0600229D RID: 8861 RVA: 0x001696E4 File Offset: 0x001678E4
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_shore.psd", true);
			this.gizPos.y = this.gizPos.y - 0.03f;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0016973C File Offset: 0x0016793C
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

		// Token: 0x0600229F RID: 8863 RVA: 0x001698C0 File Offset: 0x00167AC0
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

		// Token: 0x04002B9E RID: 11166
		public int lodIndex;

		// Token: 0x04002B9F RID: 11167
		public int shorelineModeIndex;

		// Token: 0x04002BA0 RID: 11168
		public List<string> shorelineModeOptions = new List<string>
		{
			"Auto-Generate",
			"Custom Texture"
		};

		// Token: 0x04002BA1 RID: 11169
		public int shorelineRunIndex;

		// Token: 0x04002BA2 RID: 11170
		public List<string> shorelineRunOptions = new List<string>
		{
			"At Start",
			"Continuous"
		};

		// Token: 0x04002BA3 RID: 11171
		public Transform attachToSurface;

		// Token: 0x04002BA4 RID: 11172
		public bool autoPosition = true;

		// Token: 0x04002BA5 RID: 11173
		public float maxDepth = 25f;

		// Token: 0x04002BA6 RID: 11174
		public float sceneDepth = 14.5f;

		// Token: 0x04002BA7 RID: 11175
		public float shoreDepth = 27.7f;

		// Token: 0x04002BA8 RID: 11176
		public bool debug;

		// Token: 0x04002BA9 RID: 11177
		public string suimonoVersionNumber;

		// Token: 0x04002BAA RID: 11178
		public int depthLayer = 2;

		// Token: 0x04002BAB RID: 11179
		public List<string> suiLayerMasks = new List<string>();

		// Token: 0x04002BAC RID: 11180
		public Texture2D customDepthTex;

		// Token: 0x04002BAD RID: 11181
		public int useResolution = 512;

		// Token: 0x04002BAE RID: 11182
		private Material useMat;

		// Token: 0x04002BAF RID: 11183
		private Texture reflTex;

		// Token: 0x04002BB0 RID: 11184
		private Texture envTex;

		// Token: 0x04002BB1 RID: 11185
		private Matrix4x4 MV;

		// Token: 0x04002BB2 RID: 11186
		private Camera CamInfo;

		// Token: 0x04002BB3 RID: 11187
		private cameraTools CamTools;

		// Token: 0x04002BB4 RID: 11188
		private SuimonoCamera_depth CamDepth;

		// Token: 0x04002BB5 RID: 11189
		private float curr_sceneDepth;

		// Token: 0x04002BB6 RID: 11190
		private float curr_shoreDepth;

		// Token: 0x04002BB7 RID: 11191
		private float curr_foamDepth;

		// Token: 0x04002BB8 RID: 11192
		private float curr_edgeDepth;

		// Token: 0x04002BB9 RID: 11193
		private Vector3 currPos;

		// Token: 0x04002BBA RID: 11194
		private Vector3 currScale;

		// Token: 0x04002BBB RID: 11195
		private Quaternion currRot;

		// Token: 0x04002BBC RID: 11196
		private Vector4 camCoords = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04002BBD RID: 11197
		private Material localMaterial;

		// Token: 0x04002BBE RID: 11198
		private Renderer renderObject;

		// Token: 0x04002BBF RID: 11199
		private MeshFilter meshObject;

		// Token: 0x04002BC0 RID: 11200
		private Material matObject;

		// Token: 0x04002BC1 RID: 11201
		public SuimonoModule moduleObject;

		// Token: 0x04002BC2 RID: 11202
		private float maxScale;

		// Token: 0x04002BC3 RID: 11203
		private int i;

		// Token: 0x04002BC4 RID: 11204
		private string layerName;

		// Token: 0x04002BC5 RID: 11205
		private bool hasRendered;

		// Token: 0x04002BC6 RID: 11206
		private bool renderPass = true;

		// Token: 0x04002BC7 RID: 11207
		private int saveMode = -1;

		// Token: 0x04002BC8 RID: 11208
		private Vector3 gizPos;
	}
}
