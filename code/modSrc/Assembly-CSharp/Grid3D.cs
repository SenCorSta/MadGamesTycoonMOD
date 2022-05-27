using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Grid3D : MonoBehaviour
{
	
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

	
	private void OnGUI()
	{
		GUILayout.Label(" Use arrow keys to move camera. Hold Shift + arrow up/down to move vertically. Hold Shift + arrow left/right to rotate.", Array.Empty<GUILayoutOption>());
	}

	
	public int numberOfLines = 20;

	
	public float distanceBetweenLines = 2f;

	
	public float moveSpeed = 8f;

	
	public float rotateSpeed = 70f;

	
	public float lineWidth = 2f;
}
