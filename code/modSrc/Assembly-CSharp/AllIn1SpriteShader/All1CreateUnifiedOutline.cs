using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x02000401 RID: 1025
	[ExecuteInEditMode]
	public class All1CreateUnifiedOutline : MonoBehaviour
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x00171D10 File Offset: 0x0016FF10
		private void Update()
		{
			if (this.createUnifiedOutline)
			{
				if (this.outlineMaterial == null)
				{
					this.createUnifiedOutline = false;
					this.MissingMaterial();
					return;
				}
				List<Transform> list = new List<Transform>();
				this.GetAllChildren(base.transform, ref list);
				foreach (Transform transform in list)
				{
					this.CreateOutlineSpriteDuplicate(transform.gameObject);
				}
				this.CreateOutlineSpriteDuplicate(base.gameObject);
				UnityEngine.Object.DestroyImmediate(this);
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00171DB0 File Offset: 0x0016FFB0
		private void CreateOutlineSpriteDuplicate(GameObject target)
		{
			bool flag = false;
			SpriteRenderer component = target.GetComponent<SpriteRenderer>();
			Image component2 = target.GetComponent<Image>();
			if (component != null)
			{
				flag = false;
			}
			else if (component2 != null)
			{
				flag = true;
			}
			else if (component == null && component2 == null && !base.transform.Equals(this.outlineParentTransform))
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = target.name + "Outline";
			gameObject.transform.position = target.transform.position;
			gameObject.transform.rotation = target.transform.rotation;
			gameObject.transform.localScale = target.transform.lossyScale;
			if (this.outlineParentTransform == null)
			{
				gameObject.transform.parent = target.transform;
			}
			else
			{
				gameObject.transform.parent = this.outlineParentTransform;
			}
			if (!flag)
			{
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				spriteRenderer.sprite = component.sprite;
				spriteRenderer.sortingOrder = this.duplicateOrderInLayer;
				spriteRenderer.sortingLayerName = this.duplicateSortingLayer;
				spriteRenderer.material = this.outlineMaterial;
				spriteRenderer.flipX = component.flipX;
				spriteRenderer.flipY = component.flipY;
				return;
			}
			Image image = gameObject.AddComponent<Image>();
			image.sprite = component2.sprite;
			image.material = this.outlineMaterial;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00002098 File Offset: 0x00000298
		private void MissingMaterial()
		{
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00171F0C File Offset: 0x0017010C
		private void GetAllChildren(Transform parent, ref List<Transform> transforms)
		{
			foreach (object obj in parent)
			{
				Transform transform = (Transform)obj;
				transforms.Add(transform);
				this.GetAllChildren(transform, ref transforms);
			}
		}

		// Token: 0x04002E50 RID: 11856
		[SerializeField]
		private Material outlineMaterial;

		// Token: 0x04002E51 RID: 11857
		[SerializeField]
		private Transform outlineParentTransform;

		// Token: 0x04002E52 RID: 11858
		[Space]
		[Header("Only needed if Sprite (ignored if UI)")]
		[SerializeField]
		private int duplicateOrderInLayer = -100;

		// Token: 0x04002E53 RID: 11859
		[SerializeField]
		private string duplicateSortingLayer = "Default";

		// Token: 0x04002E54 RID: 11860
		[Space]
		[Header("This operation will delete the component")]
		[SerializeField]
		private bool createUnifiedOutline;
	}
}
