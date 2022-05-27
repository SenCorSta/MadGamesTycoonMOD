using System;
using System.Collections;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036F RID: 879
public class DrawBox : MonoBehaviour
{
	// Token: 0x06002039 RID: 8249 RVA: 0x0014DC46 File Offset: 0x0014BE46
	private IEnumerator Start()
	{
		base.GetComponent<Renderer>().enabled = false;
		this.rigidbodies = (UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[]);
		VectorLine.canvas.planeDistance = 0.5f;
		yield return null;
		VectorLine.SetCanvasCamera(this.vectorCam);
		yield break;
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x0014DC58 File Offset: 0x0014BE58
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

	// Token: 0x0600203B RID: 8251 RVA: 0x0014DD80 File Offset: 0x0014BF80
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

	// Token: 0x04002890 RID: 10384
	public float moveSpeed = 1f;

	// Token: 0x04002891 RID: 10385
	public float explodePower = 20f;

	// Token: 0x04002892 RID: 10386
	public Camera vectorCam;

	// Token: 0x04002893 RID: 10387
	private bool mouseDown;

	// Token: 0x04002894 RID: 10388
	private Rigidbody[] rigidbodies;

	// Token: 0x04002895 RID: 10389
	private bool canClick = true;

	// Token: 0x04002896 RID: 10390
	private bool boxDrawn;
}
