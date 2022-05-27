using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x02000404 RID: 1028
	[ExecuteInEditMode]
	public class All1CreateUnifiedOutline : MonoBehaviour
	{
		// Token: 0x0600244C RID: 9292 RVA: 0x00174DD4 File Offset: 0x00172FD4
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

		// Token: 0x0600244D RID: 9293 RVA: 0x00174E74 File Offset: 0x00173074
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

		// Token: 0x0600244E RID: 9294 RVA: 0x00002715 File Offset: 0x00000915
		private void MissingMaterial()
		{
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00174FD0 File Offset: 0x001731D0
		private void GetAllChildren(Transform parent, ref List<Transform> transforms)
		{
			foreach (object obj in parent)
			{
				Transform transform = (Transform)obj;
				transforms.Add(transform);
				this.GetAllChildren(transform, ref transforms);
			}
		}

		// Token: 0x04002E66 RID: 11878
		[SerializeField]
		private Material outlineMaterial;

		// Token: 0x04002E67 RID: 11879
		[SerializeField]
		private Transform outlineParentTransform;

		// Token: 0x04002E68 RID: 11880
		[Space]
		[Header("Only needed if Sprite (ignored if UI)")]
		[SerializeField]
		private int duplicateOrderInLayer = -100;

		// Token: 0x04002E69 RID: 11881
		[SerializeField]
		private string duplicateSortingLayer = "Default";

		// Token: 0x04002E6A RID: 11882
		[Space]
		[Header("This operation will delete the component")]
		[SerializeField]
		private bool createUnifiedOutline;
	}
}
