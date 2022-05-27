using System;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036E RID: 878
public class VectorObject : MonoBehaviour
{
	// Token: 0x06001FF0 RID: 8176 RVA: 0x0014E528 File Offset: 0x0014C728
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("Shape", XrayLineData.use.shapePoints[(int)this.shape], XrayLineData.use.lineTexture, XrayLineData.use.lineWidth);
		vectorLine.color = Color.green;
		VectorManager.ObjectSetup(base.gameObject, vectorLine, Visibility.Always, Brightness.None);
	}

	// Token: 0x04002884 RID: 10372
	public VectorObject.Shape shape;

	// Token: 0x0200036F RID: 879
	public enum Shape
	{
		// Token: 0x04002886 RID: 10374
		Cube,
		// Token: 0x04002887 RID: 10375
		Sphere
	}
}
