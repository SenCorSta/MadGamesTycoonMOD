using System;
using Suimono.Core;
using UnityEngine;


public class guiFPS : MonoBehaviour
{
	
	private void Awake()
	{
		if (GameObject.Find("SUIMONO_Module") != null)
		{
			this.moduleObject = GameObject.Find("SUIMONO_Module").gameObject.GetComponent<SuimonoModule>();
		}
		if (GameObject.Find("SUIMONO_Surface_Ocean") != null)
		{
			this.oceanObject = GameObject.Find("SUIMONO_Surface_Ocean").gameObject.GetComponent<SuimonoObject>();
		}
	}

	
	private void Start()
	{
		this.currentPreset = 0;
	}

	
	private void LateUpdate()
	{
		if (this.moduleObject != null)
		{
			this.VerMsg = "|  Ver. " + this.moduleObject.suimonoVersionNumber;
		}
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames += 1f;
		if ((double)this.timeleft <= 0.0)
		{
			this.GuiMsg = "FPS: " + (this.accum / this.frames).ToString("f0");
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0f;
		}
		this.inputKeyMode = Input.GetKeyUp("1");
		if (this.oceanObject != null)
		{
			if (!this.oceanObject.enableTess)
			{
				this.displayMode = "DX9";
			}
			if (this.oceanObject.enableTess)
			{
				this.displayMode = "DX11";
			}
			if (this.inputKeyMode)
			{
				this.oceanObject.enableTess = !this.oceanObject.enableTess;
			}
		}
		this.inputKeyPreset = Input.GetKeyUp("2");
		if (this.usePreset == 0)
		{
			this.displayPreset = "Blue Ocean with Waves";
		}
		if (this.usePreset == 1)
		{
			this.displayPreset = "Calm Carribean Blue";
		}
		if (this.usePreset == 2)
		{
			this.displayPreset = "Calm Clear";
		}
		if (this.usePreset == 3)
		{
			this.displayPreset = "Deep Dark Ocean";
		}
		if (this.usePreset == 4)
		{
			this.displayPreset = "Hot Spring Murky";
		}
		if (this.usePreset == 5)
		{
			this.displayPreset = "Mirror Reflection";
		}
		if (this.usePreset == 6)
		{
			this.displayPreset = "Mud Thick Brown";
		}
		if (this.usePreset == 7)
		{
			this.displayPreset = "Swimming Pool";
		}
		if (this.inputKeyPreset)
		{
			this.currentPreset++;
			if (this.currentPreset > 7)
			{
				this.currentPreset = 0;
			}
		}
		if (this.usePreset != this.currentPreset)
		{
			this.usePreset = this.currentPreset;
			if (this.usePreset == 0)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Blue Ocean with Waves");
			}
			if (this.usePreset == 1)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Calm Carribean Blue");
			}
			if (this.usePreset == 2)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Calm Clear");
			}
			if (this.usePreset == 3)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Deep Dark Ocean");
			}
			if (this.usePreset == 4)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Hot Spring Murky");
			}
			if (this.usePreset == 5)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Mirror Reflection");
			}
			if (this.usePreset == 6)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Mud Thick Brown");
			}
			if (this.usePreset == 7)
			{
				this.oceanObject.SuimonoSetPreset("Built-In Presets", "Swimming Pool");
			}
		}
	}

	
	private void OnGUI()
	{
		GUI.color = new Color(0f, 0f, 0f, 1f);
		GUI.Label(new Rect(15f, 10f, 600f, 20f), "SUIMONO 2.0 - Interactive Water System for Unity");
		GUI.Label(new Rect(323f, 10f, 200f, 20f), this.VerMsg);
		GUI.color = new Color(1f, 0.45f, 0f, 1f);
		GUI.Label(new Rect(15f, 26f, 100f, 20f), this.GuiMsg);
		GUI.color = new Color(1f, 0.45f, 0f, 1f);
		GUI.Label(new Rect(90f, 26f, 300f, 20f), "Preset: " + this.displayPreset);
		GUI.color = new Color(1f, 0.45f, 0f, 1f);
		GUI.Label(new Rect(290f, 26f, 300f, 20f), "Mode: " + this.displayMode);
		if (this.showLabel != null)
		{
			GUI.color = this.labelColor;
			GUI.Label(new Rect(15f, 53f, (float)this.showLabel.width, (float)this.showLabel.height), this.showLabel);
		}
	}

	
	public Texture2D showLabel;

	
	public Vector2 labelOffset = new Vector2(0.5f, 0.5f);

	
	public Color labelColor = new Color(1f, 1f, 1f, 1f);

	
	private float updateInterval = 0.3f;

	
	private string GuiMsg = "---";

	
	private string VerMsg = "---";

	
	private float accum;

	
	private float frames;

	
	private float timeleft;

	
	private SuimonoModule moduleObject;

	
	private SuimonoObject oceanObject;

	
	private string displayMode = "---";

	
	private bool inputKeyMode;

	
	private string displayPreset = "---";

	
	private bool inputKeyPreset;

	
	private int currentPreset;

	
	private int usePreset;
}
