using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x02000403 RID: 1027
	[ExecuteInEditMode]
	public class SetAtlasUvs : MonoBehaviour
	{
		// Token: 0x06002400 RID: 9216 RVA: 0x000187FD File Offset: 0x000169FD
		private void Start()
		{
			this.Setup();
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000187FD File Offset: 0x000169FD
		private void Reset()
		{
			this.Setup();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00018805 File Offset: 0x00016A05
		private void Setup()
		{
			if (this.GetRendererReferencesIfNeeded())
			{
				this.GetAndSetUVs();
			}
			if (!this.updateEveryFrame && Application.isPlaying && this != null)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00018834 File Offset: 0x00016A34
		private void OnWillRenderObject()
		{
			if (this.updateEveryFrame)
			{
				this.GetAndSetUVs();
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00172048 File Offset: 0x00170248
		public void GetAndSetUVs()
		{
			if (!this.GetRendererReferencesIfNeeded())
			{
				return;
			}
			if (!this.isUI)
			{
				Rect textureRect = this.spriteRender.sprite.textureRect;
				textureRect.x /= (float)this.spriteRender.sprite.texture.width;
				textureRect.width /= (float)this.spriteRender.sprite.texture.width;
				textureRect.y /= (float)this.spriteRender.sprite.texture.height;
				textureRect.height /= (float)this.spriteRender.sprite.texture.height;
				this.render.sharedMaterial.SetFloat("_MinXUV", textureRect.xMin);
				this.render.sharedMaterial.SetFloat("_MaxXUV", textureRect.xMax);
				this.render.sharedMaterial.SetFloat("_MinYUV", textureRect.yMin);
				this.render.sharedMaterial.SetFloat("_MaxYUV", textureRect.yMax);
				return;
			}
			Rect textureRect2 = this.uiImage.sprite.textureRect;
			textureRect2.x /= (float)this.uiImage.sprite.texture.width;
			textureRect2.width /= (float)this.uiImage.sprite.texture.width;
			textureRect2.y /= (float)this.uiImage.sprite.texture.height;
			textureRect2.height /= (float)this.uiImage.sprite.texture.height;
			this.uiImage.material.SetFloat("_MinXUV", textureRect2.xMin);
			this.uiImage.material.SetFloat("_MaxXUV", textureRect2.xMax);
			this.uiImage.material.SetFloat("_MinYUV", textureRect2.yMin);
			this.uiImage.material.SetFloat("_MaxYUV", textureRect2.yMax);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0017228C File Offset: 0x0017048C
		public void ResetAtlasUvs()
		{
			if (!this.GetRendererReferencesIfNeeded())
			{
				return;
			}
			if (!this.isUI)
			{
				this.render.sharedMaterial.SetFloat("_MinXUV", 0f);
				this.render.sharedMaterial.SetFloat("_MaxXUV", 1f);
				this.render.sharedMaterial.SetFloat("_MinYUV", 0f);
				this.render.sharedMaterial.SetFloat("_MaxYUV", 1f);
				return;
			}
			this.uiImage.material.SetFloat("_MinXUV", 0f);
			this.uiImage.material.SetFloat("_MaxXUV", 1f);
			this.uiImage.material.SetFloat("_MinYUV", 0f);
			this.uiImage.material.SetFloat("_MaxYUV", 1f);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00018844 File Offset: 0x00016A44
		public void UpdateEveryFrame(bool everyFrame)
		{
			this.updateEveryFrame = everyFrame;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0017237C File Offset: 0x0017057C
		private bool GetRendererReferencesIfNeeded()
		{
			if (this.spriteRender == null)
			{
				this.spriteRender = base.GetComponent<SpriteRenderer>();
			}
			if (this.spriteRender != null)
			{
				if (this.spriteRender.sprite == null)
				{
					UnityEngine.Object.DestroyImmediate(this);
					return false;
				}
				if (this.render == null)
				{
					this.render = base.GetComponent<Renderer>();
				}
				this.isUI = false;
			}
			else
			{
				if (this.uiImage == null)
				{
					this.uiImage = base.GetComponent<Image>();
					if (!(this.uiImage != null))
					{
						UnityEngine.Object.DestroyImmediate(this);
						return false;
					}
				}
				if (this.render == null)
				{
					this.render = base.GetComponent<Renderer>();
				}
				this.isUI = true;
			}
			if (this.spriteRender == null && this.uiImage == null)
			{
				UnityEngine.Object.DestroyImmediate(this);
				return false;
			}
			return true;
		}

		// Token: 0x04002E55 RID: 11861
		[SerializeField]
		private bool updateEveryFrame;

		// Token: 0x04002E56 RID: 11862
		private Renderer render;

		// Token: 0x04002E57 RID: 11863
		private SpriteRenderer spriteRender;

		// Token: 0x04002E58 RID: 11864
		private Image uiImage;

		// Token: 0x04002E59 RID: 11865
		private bool isUI;
	}
}
