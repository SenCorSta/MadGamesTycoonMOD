using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F4 RID: 1012
	public class FlyCamera : MonoBehaviour
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x000185BA File Offset: 0x000167BA
		private void Start()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00171018 File Offset: 0x0016F218
		private void Update()
		{
			if (Cursor.visible)
			{
				if (Input.GetKeyDown(KeyCode.BackQuote))
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
					return;
				}
			}
			else
			{
				Vector3 zero = Vector3.zero;
				if (Input.GetKey(KeyCode.W))
				{
					zero.z += 1f;
				}
				if (Input.GetKey(KeyCode.S))
				{
					zero.z += -1f;
				}
				if (Input.GetKey(KeyCode.A))
				{
					zero.x += -1f;
				}
				if (Input.GetKey(KeyCode.D))
				{
					zero.x += 1f;
				}
				base.transform.localPosition += base.transform.TransformVector(zero * this.Speed * Time.deltaTime);
				float num = Input.GetAxis("Mouse Y") * this.MouseSensitivity * (this.InvertMouse ? -1f : 1f);
				float num2 = Input.GetAxis("Mouse X") * this.MouseSensitivity;
				Vector3 euler = new Vector3(Mathf.LerpAngle(base.transform.localEulerAngles.x, base.transform.localEulerAngles.x + num, 1f), Mathf.LerpAngle(base.transform.localEulerAngles.y, base.transform.localEulerAngles.y + num2, 1f), 0f);
				base.transform.localRotation = Quaternion.Euler(euler);
				if (Input.GetKeyDown(KeyCode.BackQuote))
				{
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
				}
			}
		}

		// Token: 0x04002E19 RID: 11801
		public float Speed = 1f;

		// Token: 0x04002E1A RID: 11802
		public float MouseSensitivity = 1f;

		// Token: 0x04002E1B RID: 11803
		public bool InvertMouse;
	}
}
