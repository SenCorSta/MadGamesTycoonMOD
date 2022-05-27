using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000378 RID: 888
public class Simple3D2 : MonoBehaviour
{
	// Token: 0x0600204F RID: 8271 RVA: 0x0014F7F8 File Offset: 0x0014D9F8
	private void Start()
	{
		List<Vector3> points = VectorLine.BytesToVector3List(this.vectorCube.bytes);
		VectorLine line = new VectorLine(base.gameObject.name, points, 2f);
		VectorManager.ObjectSetup(base.gameObject, line, Visibility.Dynamic, Brightness.None);
	}

	// Token: 0x040028A5 RID: 10405
	public TextAsset vectorCube;
}
