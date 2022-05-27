using System;
using UnityEngine;
using Vectrosity;

// Token: 0x02000371 RID: 881
public class VectorObject : MonoBehaviour
{
	// Token: 0x06002043 RID: 8259 RVA: 0x0014DF00 File Offset: 0x0014C100
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("Shape", XrayLineData.use.shapePoints[(int)this.shape], XrayLineData.use.lineTexture, XrayLineData.use.lineWidth);
		vectorLine.color = Color.green;
		VectorManager.ObjectSetup(base.gameObject, vectorLine, Visibility.Always, Brightness.None);
	}

	// Token: 0x0400289A RID: 10394
	public VectorObject.Shape shape;

	// Token: 0x02000372 RID: 882
	public enum Shape
	{
		// Token: 0x0400289C RID: 10396
		Cube,
		// Token: 0x0400289D RID: 10397
		Sphere
	}
}
