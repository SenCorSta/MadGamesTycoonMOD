using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003F8 RID: 1016
	public class AllIn1ScrollProperty : MonoBehaviour
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x00171664 File Offset: 0x0016F864
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

		// Token: 0x060023D6 RID: 9174 RVA: 0x00171748 File Offset: 0x0016F948
		private void Update()
		{
			this.currValue += this.scrollSpeed * Time.deltaTime;
			if (this.applyModulo)
			{
				this.currValue %= this.modulo;
			}
			this.mat.SetFloat(this.propertyShaderID, this.currValue);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00018646 File Offset: 0x00016846
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04002E2D RID: 11821
		[SerializeField]
		private string numericPropertyName = "_RotateUvAmount";

		// Token: 0x04002E2E RID: 11822
		[SerializeField]
		private float scrollSpeed;

		// Token: 0x04002E2F RID: 11823
		[Space]
		[SerializeField]
		private bool applyModulo;

		// Token: 0x04002E30 RID: 11824
		[SerializeField]
		private float modulo = 1f;

		// Token: 0x04002E31 RID: 11825
		[Space]
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		// Token: 0x04002E32 RID: 11826
		private int propertyShaderID;

		// Token: 0x04002E33 RID: 11827
		private float currValue;
	}
}
