using System;
using System.Collections.Generic;
using UnityEngine;


public class GrassController : MonoBehaviour
{
	
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

	
	public List<GameObject> grassPrefabs = new List<GameObject>();

	
	public int grassNumber = 64;

	
	public float grassAreaWidth = 5f;

	
	public float grassAreaHeight = 5f;

	
	public string interactionTag = "Player";

	
	private Vector4[] grassInteractionPositions = new Vector4[4];

	
	private Transform ground;

	
	private List<GameObject> grass = new List<GameObject>();
}
