using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000350 RID: 848
public class Grid3D : MonoBehaviour
{
	// Token: 0x06001F75 RID: 8053 RVA: 0x0014BF78 File Offset: 0x0014A178
	private void Start()
	{
		this.numberOfLines = Mathf.Clamp(this.numberOfLines, 2, 8190);
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.numberOfLines; i++)
		{
			list.Add(new Vector3((float)i * this.distanceBetweenLines, 0f, 0f));
			list.Add(new Vector3((float)i * this.distanceBetweenLines, 0f, (float)(this.numberOfLines - 1) * this.distanceBetweenLines));
		}
		for (int j = 0; j < this.numberOfLines; j++)
		{
			list.Add(new Vector3(0f, 0f, (float)j * this.distanceBetweenLines));
			list.Add(new Vector3((float)(this.numberOfLines - 1) * this.distanceBetweenLines, 0f, (float)j * this.distanceBetweenLines));
		}
		new VectorLine("Grid", list, this.lineWidth).Draw3DAuto();
		Vector3 position = base.transform.position;
		position.x = (float)(this.numberOfLines - 1) * this.distanceBetweenLines / 2f;
		base.transform.position = position;
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x0014C0A0 File Offset: 0x0014A2A0
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			base.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * this.rotateSpeed);
			base.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * this.moveSpeed);
			return;
		}
		base.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * this.moveSpeed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * this.moveSpeed));
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x00014D60 File Offset: 0x00012F60
	private void OnGUI()
	{
		GUILayout.Label(" Use arrow keys to move camera. Hold Shift + arrow up/down to move vertically. Hold Shift + arrow left/right to rotate.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x040027EA RID: 10218
	public int numberOfLines = 20;

	// Token: 0x040027EB RID: 10219
	public float distanceBetweenLines = 2f;

	// Token: 0x040027EC RID: 10220
	public float moveSpeed = 8f;

	// Token: 0x040027ED RID: 10221
	public float rotateSpeed = 70f;

	// Token: 0x040027EE RID: 10222
	public float lineWidth = 2f;
}
