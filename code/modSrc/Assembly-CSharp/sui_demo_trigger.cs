using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class sui_demo_trigger : MonoBehaviour
{
	// Token: 0x060000DA RID: 218 RVA: 0x00002956 File Offset: 0x00000B56
	private void Start()
	{
		this.CM = GameObject.Find("_CONTROLLER").GetComponent<sui_demo_ControllerMaster>();
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00020D80 File Offset: 0x0001EF80
	private void FixedUpdate()
	{
		this.useLabel = this.label;
		if (Camera.main != null && this.savedPos != Camera.main.transform.position)
		{
			this.savedPos = Camera.main.transform.position;
			this.isInSight = this.CheckLineOfSight();
		}
		this.isInRange = false;
		if (Vector3.Distance(base.transform.position, this.trackObject.transform.position) <= this.checkDistance * 0.75f)
		{
			this.isInRange = true;
		}
		this.enableAction = false;
		if (this.isInRange && !this.requireLineOfSight)
		{
			this.enableAction = true;
		}
		else if (this.isInRange && this.requireLineOfSight && this.isInSight)
		{
			this.enableAction = true;
		}
		this.onAction = false;
		if (Input.GetKeyUp(this.actionKey) && this.enableAction)
		{
			this.onAction = true;
		}
		if (this.onAction)
		{
			this.useLabel = "";
			if (this.triggerType == sui_demo_trigger.Sui_Demo_TriggerType.switchtovehicle && this.CM != null)
			{
				if (this.CM.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.character)
				{
					this.CM.currentControllerType = sui_demo_ControllerMaster.Sui_Demo_ControllerType.boat;
				}
				else if (this.CM.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.boat)
				{
					this.CM.currentControllerType = sui_demo_ControllerMaster.Sui_Demo_ControllerType.character;
				}
			}
		}
		if (this.enableAction)
		{
			this.fadeTimer = Mathf.Lerp(this.fadeTimer, 0.8f, Time.deltaTime * this.fadeSpeed * 1f);
		}
		else
		{
			this.fadeTimer = Mathf.Lerp(this.fadeTimer, 0f, Time.deltaTime * this.fadeSpeed * 1f);
		}
		if (this.isInRange)
		{
			if (base.GetComponent<Renderer>())
			{
				base.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0f, 1f, 0f, 0.1f));
				return;
			}
		}
		else if (base.GetComponent<Renderer>())
		{
			base.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.5f, 0f, 0f, 0.1f));
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00020FBC File Offset: 0x0001F1BC
	public bool CheckLineOfSight()
	{
		bool result = false;
		if (this.requireLineOfSight && Camera.main != null)
		{
			float num = 0f;
			Ray ray = new Ray
			{
				origin = Camera.main.transform.position,
				direction = Camera.main.transform.forward
			};
			foreach (RaycastHit raycastHit in Physics.RaycastAll(ray, 1000f))
			{
				Collider collider = raycastHit.collider;
				if (collider && collider == this.trackObject.GetComponent<Collider>())
				{
					num = raycastHit.distance;
				}
			}
			foreach (RaycastHit raycastHit2 in Physics.RaycastAll(ray, this.checkDistance + num))
			{
				Collider collider2 = raycastHit2.collider;
				if (collider2 && collider2 == base.GetComponent<Collider>())
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x000210C8 File Offset: 0x0001F2C8
	private void OnGUI()
	{
		if (this.fadeTimer > 0f && this.useLabel != "")
		{
			int num = this.useLabel.Length * 6 + 5;
			GUI.color = new Color(0f, 0f, 0f, this.fadeTimer);
			GUI.Label(new Rect((float)Screen.width * this.labelOffset.x - (float)num * 0.5f + 8f, (float)Screen.height * this.labelOffset.y + 21f, (float)num, 20f), this.useLabel);
			GUI.color = new Color(this.labelColor.r, this.labelColor.g, this.labelColor.b, this.fadeTimer);
			GUI.Label(new Rect((float)Screen.width * this.labelOffset.x - (float)num * 0.5f + 7f, (float)Screen.height * this.labelOffset.y + 20f, (float)num, 20f), this.useLabel);
			if (this.showIcon != null)
			{
				GUI.color = new Color(this.labelColor.r, this.labelColor.g, this.labelColor.b, this.fadeTimer);
				GUI.Label(new Rect((float)Screen.width * this.labelOffset.x - (float)num * 0.8f + 7f, (float)Screen.height * this.labelOffset.y + 16f, (float)this.showIcon.width, (float)this.showIcon.height), this.showIcon);
				GUI.color = new Color(0f, 0f, 0f, this.fadeTimer);
				GUI.Label(new Rect((float)Screen.width * this.labelOffset.x - (float)num * 0.8f + 16f, (float)Screen.height * this.labelOffset.y + 20f, 20f, 30f), this.actionKey.ToUpper());
			}
		}
	}

	// Token: 0x0400023D RID: 573
	public bool requireLineOfSight = true;

	// Token: 0x0400023E RID: 574
	public sui_demo_trigger.Sui_Demo_TriggerType triggerType;

	// Token: 0x0400023F RID: 575
	public Texture2D showDot;

	// Token: 0x04000240 RID: 576
	public Texture2D showIcon;

	// Token: 0x04000241 RID: 577
	public Texture2D backgroundImage;

	// Token: 0x04000242 RID: 578
	public string label = "";

	// Token: 0x04000243 RID: 579
	public Color labelColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04000244 RID: 580
	public Vector2 dotOffset = new Vector2(0.5f, 0.5f);

	// Token: 0x04000245 RID: 581
	public Vector2 labelOffset = new Vector2(0.5f, 0.5f);

	// Token: 0x04000246 RID: 582
	public string actionKey = "f";

	// Token: 0x04000247 RID: 583
	public bool requireReset = true;

	// Token: 0x04000248 RID: 584
	public Transform trackObject;

	// Token: 0x04000249 RID: 585
	public float fadeSpeed;

	// Token: 0x0400024A RID: 586
	public float checkDistance = 200f;

	// Token: 0x0400024B RID: 587
	private sui_demo_ControllerMaster CM;

	// Token: 0x0400024C RID: 588
	private bool isInRange;

	// Token: 0x0400024D RID: 589
	private bool onAction;

	// Token: 0x0400024E RID: 590
	private string useLabel = "";

	// Token: 0x0400024F RID: 591
	private GUISkin style;

	// Token: 0x04000250 RID: 592
	private float fadeTimer;

	// Token: 0x04000251 RID: 593
	private bool isInSight;

	// Token: 0x04000252 RID: 594
	private bool enableAction;

	// Token: 0x04000253 RID: 595
	private Vector3 savedPos = new Vector3(0f, 0f, 0f);

	// Token: 0x0200003D RID: 61
	public enum Sui_Demo_TriggerType
	{
		// Token: 0x04000255 RID: 597
		switchtovehicle,
		// Token: 0x04000256 RID: 598
		watersurface
	}
}
