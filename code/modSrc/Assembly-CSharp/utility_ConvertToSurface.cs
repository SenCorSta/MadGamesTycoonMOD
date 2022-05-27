using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x0200002F RID: 47
[ExecuteInEditMode]
public class utility_ConvertToSurface : MonoBehaviour
{
	// Token: 0x060000AF RID: 175 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00002098 File Offset: 0x00000298
	private void LateUpdate()
	{
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0001BCCC File Offset: 0x00019ECC
	private bool CheckAllResources()
	{
		bool result = true;
		if (base.gameObject.GetComponent<Renderer>() == null)
		{
			result = false;
			Debug.Log("GameObject requires a <Renderer> Component!");
		}
		if (base.gameObject.GetComponent<MeshFilter>() == null)
		{
			result = false;
			Debug.Log("GameObject requires a <MeshFilter> Component!");
		}
		else if (base.gameObject.GetComponent<MeshFilter>().sharedMesh == null)
		{
			result = false;
			Debug.Log("MeshFilter requires a Mesh!");
		}
		return result;
	}

	// Token: 0x0400010A RID: 266
	public bool convertToSuimono;

	// Token: 0x0400010B RID: 267
	private SuimonoModule moduleObject;

	// Token: 0x0400010C RID: 268
	private SuimonoObject surfaceComponent;

	// Token: 0x0400010D RID: 269
	private GameObject mainObj;

	// Token: 0x0400010E RID: 270
	private Transform surfaceObj;

	// Token: 0x0400010F RID: 271
	private Transform scaleObj;

	// Token: 0x04000110 RID: 272
	private Renderer objRenderer;

	// Token: 0x04000111 RID: 273
	private MeshFilter objMeshFilter;

	// Token: 0x04000112 RID: 274
	private Mesh objMesh;
}
