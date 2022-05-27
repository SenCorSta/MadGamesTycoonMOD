using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suimono.Core
{
	
	[ExecuteInEditMode]
	public class Suimono_ShorelineObject : MonoBehaviour
	{
		
		private void OnDrawGizmos()
		{
			this.gizPos = base.transform.position;
			this.gizPos.y = this.gizPos.y + 0.03f;
			Gizmos.DrawIcon(this.gizPos, "gui_icon_shore.psd", true);
			this.gizPos.y = this.gizPos.y - 0.03f;
		}

		
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

		
		public int lodIndex;

		
		public int shorelineModeIndex;

		
		public List<string> shorelineModeOptions = new List<string>
		{
			"Auto-Generate",
			"Custom Texture"
		};

		
		public int shorelineRunIndex;

		
		public List<string> shorelineRunOptions = new List<string>
		{
			"At Start",
			"Continuous"
		};

		
		public Transform attachToSurface;

		
		public bool autoPosition = true;

		
		public float maxDepth = 25f;

		
		public float sceneDepth = 14.5f;

		
		public float shoreDepth = 27.7f;

		
		public bool debug;

		
		public string suimonoVersionNumber;

		
		public int depthLayer = 2;

		
		public List<string> suiLayerMasks = new List<string>();

		
		public Texture2D customDepthTex;

		
		public int useResolution = 512;

		
		private Material useMat;

		
		private Texture reflTex;

		
		private Texture envTex;

		
		private Matrix4x4 MV;

		
		private Camera CamInfo;

		
		private cameraTools CamTools;

		
		private SuimonoCamera_depth CamDepth;

		
		private float curr_sceneDepth;

		
		private float curr_shoreDepth;

		
		private float curr_foamDepth;

		
		private float curr_edgeDepth;

		
		private Vector3 currPos;

		
		private Vector3 currScale;

		
		private Quaternion currRot;

		
		private Vector4 camCoords = new Vector4(1f, 1f, 0f, 0f);

		
		private Material localMaterial;

		
		private Renderer renderObject;

		
		private MeshFilter meshObject;

		
		private Material matObject;

		
		public SuimonoModule moduleObject;

		
		private float maxScale;

		
		private int i;

		
		private string layerName;

		
		private bool hasRendered;

		
		private bool renderPass = true;

		
		private int saveMode = -1;

		
		private Vector3 gizPos;
	}
}
