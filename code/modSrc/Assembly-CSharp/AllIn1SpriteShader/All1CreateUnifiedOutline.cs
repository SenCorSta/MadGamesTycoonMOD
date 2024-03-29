﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	
	[ExecuteInEditMode]
	public class All1CreateUnifiedOutline : MonoBehaviour
	{
		
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

		
		private void MissingMaterial()
		{
		}

		
		private void GetAllChildren(Transform parent, ref List<Transform> transforms)
		{
			foreach (object obj in parent)
			{
				Transform transform = (Transform)obj;
				transforms.Add(transform);
				this.GetAllChildren(transform, ref transforms);
			}
		}

		
		[SerializeField]
		private Material outlineMaterial;

		
		[SerializeField]
		private Transform outlineParentTransform;

		
		[Space]
		[Header("Only needed if Sprite (ignored if UI)")]
		[SerializeField]
		private int duplicateOrderInLayer = -100;

		
		[SerializeField]
		private string duplicateSortingLayer = "Default";

		
		[Space]
		[Header("This operation will delete the component")]
		[SerializeField]
		private bool createUnifiedOutline;
	}
}
