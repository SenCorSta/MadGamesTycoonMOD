using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FB RID: 1019
	public class AllIn1ScrollProperty : MonoBehaviour
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x001745B8 File Offset: 0x001727B8
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
			if (this.mat.HasProperty(this.numericPropertyName))
			{
				this.propertyShaderID = Shader.PropertyToID(this.numericPropertyName);
			}
			else
			{
				this.DestroyComponentAndLogError(base.gameObject.name + "'s Material doesn't have a " + this.numericPropertyName + " property");
			}
			this.currValue = this.mat.GetFloat(this.propertyShaderID);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0017469C File Offset: 0x0017289C
		private void Update()
		{
			this.currValue += this.scrollSpeed * Time.deltaTime;
			if (this.applyModulo)
			{
				this.currValue %= this.modulo;
			}
			this.mat.SetFloat(this.propertyShaderID, this.currValue);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x0017458C File Offset: 0x0017278C
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04002E43 RID: 11843
		[SerializeField]
		private string numericPropertyName = "_RotateUvAmount";

		// Token: 0x04002E44 RID: 11844
		[SerializeField]
		private float scrollSpeed;

		// Token: 0x04002E45 RID: 11845
		[Space]
		[SerializeField]
		private bool applyModulo;

		// Token: 0x04002E46 RID: 11846
		[SerializeField]
		private float modulo = 1f;

		// Token: 0x04002E47 RID: 11847
		[Space]
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		// Token: 0x04002E48 RID: 11848
		private int propertyShaderID;

		// Token: 0x04002E49 RID: 11849
		private float currValue;
	}
}
