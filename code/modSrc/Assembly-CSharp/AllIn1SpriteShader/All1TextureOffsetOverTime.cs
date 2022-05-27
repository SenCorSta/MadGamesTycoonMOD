using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003F7 RID: 1015
	public class All1TextureOffsetOverTime : MonoBehaviour
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x00171534 File Offset: 0x0016F734
		private void Start()
		{
			if (this.mat == null)
			{
				SpriteRenderer component = base.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					this.mat = component.material;
				}
				else
				{
					Image component2 = base.GetComponent<Image>();
					if (component2 != null)
					{
						this.mat = component2.material;
					}
				}
			}
			if (this.mat == null)
			{
				this.DestroyComponentAndLogError(base.gameObject.name + " has no valid Material, deleting All1TextureOffsetOverTIme component");
				return;
			}
			if (this.mat.HasProperty(this.texturePropertyName))
			{
				this.textureShaderId = Shader.PropertyToID(this.texturePropertyName);
				return;
			}
			this.DestroyComponentAndLogError(base.gameObject.name + "'s Material doesn't have a " + this.texturePropertyName + " property");
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00171600 File Offset: 0x0016F800
		private void Update()
		{
			this.currOffset.x = this.currOffset.x + this.offsetSpeed.x * Time.deltaTime;
			this.currOffset.y = this.currOffset.y + this.offsetSpeed.y * Time.deltaTime;
			this.mat.SetTextureOffset(this.textureShaderId, this.currOffset);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00018646 File Offset: 0x00016846
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04002E28 RID: 11816
		[SerializeField]
		private string texturePropertyName = "_MainTex";

		// Token: 0x04002E29 RID: 11817
		[SerializeField]
		private Vector2 offsetSpeed;

		// Token: 0x04002E2A RID: 11818
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		// Token: 0x04002E2B RID: 11819
		private int textureShaderId;

		// Token: 0x04002E2C RID: 11820
		private Vector2 currOffset = Vector2.zero;
	}
}
