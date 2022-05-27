using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class GrassController : MonoBehaviour
{
	// Token: 0x06000096 RID: 150 RVA: 0x0000510C File Offset: 0x0000330C
	private void Awake()
	{
		this.ground = base.transform;
		float num = this.grassAreaWidth / 2f;
		float num2 = this.grassAreaHeight / 2f;
		for (int i = 0; i < this.grassNumber; i++)
		{
			Vector3 position = base.transform.position + new Vector3(UnityEngine.Random.Range(-num, num), 0f, UnityEngine.Random.Range(-num2, num2));
			GameObject item = UnityEngine.Object.Instantiate<GameObject>(this.grassPrefabs[UnityEngine.Random.Range(0, this.grassPrefabs.Count)], position, Quaternion.Euler(0f, (float)UnityEngine.Random.Range(0, 360), 0f), this.ground.transform);
			this.grass.Add(item);
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000051D8 File Offset: 0x000033D8
	private void Update()
	{
		int num = 0;
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(this.interactionTag))
		{
			this.grassInteractionPositions[num++] = gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
		}
		Shader.SetGlobalFloat("_PositionArray", (float)num);
		Shader.SetGlobalVectorArray("_Positions", this.grassInteractionPositions);
	}

	// Token: 0x040000B8 RID: 184
	public List<GameObject> grassPrefabs = new List<GameObject>();

	// Token: 0x040000B9 RID: 185
	public int grassNumber = 64;

	// Token: 0x040000BA RID: 186
	public float grassAreaWidth = 5f;

	// Token: 0x040000BB RID: 187
	public float grassAreaHeight = 5f;

	// Token: 0x040000BC RID: 188
	public string interactionTag = "Player";

	// Token: 0x040000BD RID: 189
	private Vector4[] grassInteractionPositions = new Vector4[4];

	// Token: 0x040000BE RID: 190
	private Transform ground;

	// Token: 0x040000BF RID: 191
	private List<GameObject> grass = new List<GameObject>();
}
