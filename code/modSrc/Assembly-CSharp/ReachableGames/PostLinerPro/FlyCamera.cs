using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F7 RID: 1015
	public class FlyCamera : MonoBehaviour
	{
		// Token: 0x06002418 RID: 9240 RVA: 0x00173EB3 File Offset: 0x001720B3
		private void Start()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00173EC4 File Offset: 0x001720C4
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

		// Token: 0x04002E2F RID: 11823
		public float Speed = 1f;

		// Token: 0x04002E30 RID: 11824
		public float MouseSensitivity = 1f;

		// Token: 0x04002E31 RID: 11825
		public bool InvertMouse;
	}
}
