using System;
using UnityEngine;


public class sui_demo_trigger : MonoBehaviour
{
	
	private void Start()
	{
		this.CM = GameObject.Find("_CONTROLLER").GetComponent<sui_demo_ControllerMaster>();
	}

	
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

	
	public bool requireLineOfSight = true;

	
	public sui_demo_trigger.Sui_Demo_TriggerType triggerType;

	
	public Texture2D showDot;

	
	public Texture2D showIcon;

	
	public Texture2D backgroundImage;

	
	public string label = "";

	
	public Color labelColor = new Color(0f, 0f, 0f, 1f);

	
	public Vector2 dotOffset = new Vector2(0.5f, 0.5f);

	
	public Vector2 labelOffset = new Vector2(0.5f, 0.5f);

	
	public string actionKey = "f";

	
	public bool requireReset = true;

	
	public Transform trackObject;

	
	public float fadeSpeed;

	
	public float checkDistance = 200f;

	
	private sui_demo_ControllerMaster CM;

	
	private bool isInRange;

	
	private bool onAction;

	
	private string useLabel = "";

	
	private GUISkin style;

	
	private float fadeTimer;

	
	private bool isInSight;

	
	private bool enableAction;

	
	private Vector3 savedPos = new Vector3(0f, 0f, 0f);

	
	public enum Sui_Demo_TriggerType
	{
		
		switchtovehicle,
		
		watersurface
	}
}
