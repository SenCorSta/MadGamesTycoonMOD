using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000375 RID: 885
public class Simple3D2 : MonoBehaviour
{
	// Token: 0x06001FFC RID: 8188 RVA: 0x0014FDEC File Offset: 0x0014DFEC
	private void Start()
	{
		List<Vector3> points = VectorLine.BytesToVector3List(this.vectorCube.bytes);
		VectorLine line = new VectorLine(base.gameObject.name, points, 2f);
		VectorManager.ObjectSetup(base.gameObject, line, Visibility.Dynamic, Brightness.None);
	}

	// Token: 0x0400288F RID: 10383
	public TextAsset vectorCube;
}
