using System;
using System.Collections;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036C RID: 876
public class DrawBox : MonoBehaviour
{
	// Token: 0x06001FE6 RID: 8166 RVA: 0x0001525D File Offset: 0x0001345D
	private IEnumerator Start()
	{
		base.GetComponent<Renderer>().enabled = false;
		this.rigidbodies = (UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[]);
		VectorLine.canvas.planeDistance = 0.5f;
		yield return null;
		VectorLine.SetCanvasCamera(this.vectorCam);
		yield break;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x0014E2BC File Offset: 0x0014C4BC
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 1f;
		Vector3 vector = Camera.main.ScreenToWorldPoint(mousePosition);
		if (Input.GetMouseButtonDown(0) && this.canClick)
		{
			base.GetComponent<Renderer>().enabled = true;
			base.transform.position = vector;
			this.mouseDown = true;
		}
		if (this.mouseDown)
		{
			base.transform.localScale = new Vector3(vector.x - base.transform.position.x, vector.y - base.transform.position.y, 1f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.mouseDown = false;
			this.boxDrawn = true;
		}
		base.transform.Translate(-Vector3.up * Time.deltaTime * this.moveSpeed * Input.GetAxis("Vertical"));
		base.transform.Translate(Vector3.right * Time.deltaTime * this.moveSpeed * Input.GetAxis("Horizontal"));
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x0014E3E4 File Offset: 0x0014C5E4
	private void OnGUI()
	{
		GUI.Box(new Rect(20f, 20f, 320f, 38f), "Draw a box by clicking and dragging with the mouse\nMove the drawn box with the arrow keys");
		Rect position = new Rect(20f, 62f, 60f, 30f);
		this.canClick = !position.Contains(Event.current.mousePosition);
		if (this.boxDrawn && GUI.Button(position, "Boom!"))
		{
			Rigidbody[] array = this.rigidbodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddExplosionForce(this.explodePower, new Vector3(0f, -6.5f, -1.5f), 20f, 0f, ForceMode.VelocityChange);
			}
		}
	}

	// Token: 0x0400287A RID: 10362
	public float moveSpeed = 1f;

	// Token: 0x0400287B RID: 10363
	public float explodePower = 20f;

	// Token: 0x0400287C RID: 10364
	public Camera vectorCam;

	// Token: 0x0400287D RID: 10365
	private bool mouseDown;

	// Token: 0x0400287E RID: 10366
	private Rigidbody[] rigidbodies;

	// Token: 0x0400287F RID: 10367
	private bool canClick = true;

	// Token: 0x04002880 RID: 10368
	private bool boxDrawn;
}
