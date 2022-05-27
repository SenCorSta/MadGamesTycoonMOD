using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	
	[ExecuteInEditMode]
	public class PostLinerRenderer : MonoBehaviour
	{
		
		private void Awake()
		{
			PostLinerRenderer.Instance = null;
		}

		
		private void Start()
		{
			PostLinerRenderer.Instance = this;
		}

		
		private void OnDestroy()
		{
			PostLinerRenderer.Instance = null;
		}

		
		public void ClearAllOutlines()
		{
			this._recursiveList.Clear();
			this._outlineObjects.Clear();
			this._objectLayers.Clear();
		}

		
		public void AddToOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Add(o);
			});
		}

		
		public void RemoveFromOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Remove(o);
			});
		}

		
		private void DoRecursive(Transform root, PostLinerRenderer.DoAction action)
		{
			this._recursiveList.Clear();
			this._recursiveList.Enqueue(root);
			while (this._recursiveList.Count > 0)
			{
				Transform transform = this._recursiveList.Dequeue();
				foreach (object obj in transform)
				{
					Transform item = (Transform)obj;
					this._recursiveList.Enqueue(item);
				}
				action(transform);
			}
		}

		
		public void OnPreRender()
		{
			this.UpdateRenderTexture(Camera.current);
		}

		
		private void UpdateRenderTexture(Camera c)
		{
			if (c.depthTextureMode == DepthTextureMode.None)
			{
				c.depthTextureMode = DepthTextureMode.Depth;
			}
			this._recursiveList.Clear();
			this._objectLayers.Clear();
			using (HashSet<Transform>.Enumerator enumerator = this._outlineObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Transform transform = enumerator.Current;
					if (transform == null)
					{
						this._recursiveList.Enqueue(transform);
					}
					else
					{
						this._objectLayers.Add(transform.gameObject.layer);
						transform.gameObject.layer = this._outlineLayer;
					}
				}
				goto IL_A9;
			}
			IL_92:
			this._outlineObjects.Remove(this._recursiveList.Dequeue());
			IL_A9:
			if (this._recursiveList.Count <= 0)
			{
				if (this._hiddenCamera == null)
				{
					GameObject gameObject = new GameObject("OutlineCamera");
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					gameObject.transform.SetParent(base.transform);
					this._hiddenCamera = gameObject.AddComponent<Camera>();
				}
				this._hiddenCamera.CopyFrom(c);
				if (this._renderTexture == null || this._hiddenCamera.pixelWidth != this._renderTexture.width || this._hiddenCamera.pixelHeight != this._renderTexture.height)
				{
					if (this._renderTexture != null)
					{
						this._renderTexture.Release();
					}
					this._renderTexture = new RenderTexture(this._hiddenCamera.pixelWidth, this._hiddenCamera.pixelHeight, 16, RenderTextureFormat.Depth, RenderTextureReadWrite.Default);
					this._renderTexture.hideFlags = HideFlags.HideAndDontSave;
				}
				this._hiddenCamera.enabled = false;
				this._hiddenCamera.depthTextureMode = DepthTextureMode.Depth;
				this._hiddenCamera.clearFlags = CameraClearFlags.Depth;
				this._hiddenCamera.targetTexture = this._renderTexture;
				this._hiddenCamera.forceIntoRenderTexture = true;
				this._hiddenCamera.rect = new Rect(0f, 0f, 1f, 1f);
				this._hiddenCamera.cullingMask = 1 << this._outlineLayer;
				this._hiddenCamera.Render();
				Shader.SetGlobalTexture(PostLinerRenderer._globalTextureId, this._renderTexture);
				int num = 0;
				foreach (Transform transform2 in this._outlineObjects)
				{
					transform2.gameObject.layer = this._objectLayers[num++];
				}
				return;
			}
			goto IL_92;
		}

		
		[HideInInspector]
		public int _outlineLayer;

		
		private Camera _hiddenCamera;

		
		private RenderTexture _renderTexture;

		
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		
		private HashSet<Transform> _outlineObjects = new HashSet<Transform>();

		
		private List<int> _objectLayers = new List<int>();

		
		public static PostLinerRenderer Instance = null;

		
		private Queue<Transform> _recursiveList = new Queue<Transform>();

		
		// (Invoke) Token: 0x060023BF RID: 9151
		private delegate void DoAction(Transform g);
	}
}
