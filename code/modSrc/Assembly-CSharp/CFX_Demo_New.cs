using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CFX_Demo_New : MonoBehaviour
{
	
	private void Awake()
	{
		List<GameObject> list = new List<GameObject>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			list.Add(gameObject);
		}
		list.Sort((GameObject o1, GameObject o2) => o1.name.CompareTo(o2.name));
		this.ParticleExamples = list.ToArray();
		this.defaultCamPosition = Camera.main.transform.position;
		this.defaultCamRotation = Camera.main.transform.rotation;
		base.StartCoroutine("CheckForDeletedParticles");
		this.UpdateUI();
	}

	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.prevParticle();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.nextParticle();
		}
		else if (Input.GetKeyDown(KeyCode.Delete))
		{
			this.destroyParticles();
		}
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit raycastHit = default(RaycastHit);
			if (this.groundCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 9999f))
			{
				GameObject gameObject = this.spawnParticle();
				gameObject.transform.position = raycastHit.point + gameObject.transform.position;
			}
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			Camera.main.transform.Translate(Vector3.forward * ((axis < 0f) ? -1f : 1f), Space.Self);
		}
		if (Input.GetMouseButtonDown(2))
		{
			Camera.main.transform.position = this.defaultCamPosition;
			Camera.main.transform.rotation = this.defaultCamRotation;
		}
	}

	
	public void OnToggleGround()
	{
		Color white = Color.white;
		this.groundRenderer.enabled = !this.groundRenderer.enabled;
		white.a = (this.groundRenderer.enabled ? 1f : 0.33f);
		this.groundBtn.color = white;
		this.groundLabel.color = white;
	}

	
	public void OnToggleCamera()
	{
		Color white = Color.white;
		CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
		white.a = (CFX_Demo_RotateCamera.rotating ? 1f : 0.33f);
		this.camRotBtn.color = white;
		this.camRotLabel.color = white;
	}

	
	public void OnToggleSlowMo()
	{
		Color white = Color.white;
		this.slowMo = !this.slowMo;
		if (this.slowMo)
		{
			Time.timeScale = 0.33f;
			white.a = 1f;
		}
		else
		{
			Time.timeScale = 1f;
			white.a = 0.33f;
		}
		this.slowMoBtn.color = white;
		this.slowMoLabel.color = white;
	}

	
	public void OnPreviousEffect()
	{
		this.prevParticle();
	}

	
	public void OnNextEffect()
	{
		this.nextParticle();
	}

	
	private void UpdateUI()
	{
		this.EffectLabel.text = this.ParticleExamples[this.exampleIndex].name;
		this.EffectIndexLabel.text = string.Format("{0}/{1}", (this.exampleIndex + 1).ToString("00"), this.ParticleExamples.Length.ToString("00"));
	}

	
	private GameObject spawnParticle()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParticleExamples[this.exampleIndex]);
		gameObject.transform.position = new Vector3(0f, gameObject.transform.position.y, 0f);
		gameObject.SetActive(true);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		if (component != null && component.main.loop)
		{
			component.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			component.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
		this.onScreenParticles.Add(gameObject);
		return gameObject;
	}

	
	private IEnumerator CheckForDeletedParticles()
	{
		for (;;)
		{
			yield return new WaitForSeconds(5f);
			for (int i = this.onScreenParticles.Count - 1; i >= 0; i--)
			{
				if (this.onScreenParticles[i] == null)
				{
					this.onScreenParticles.RemoveAt(i);
				}
			}
		}
		yield break;
	}

	
	private void prevParticle()
	{
		this.exampleIndex--;
		if (this.exampleIndex < 0)
		{
			this.exampleIndex = this.ParticleExamples.Length - 1;
		}
		this.UpdateUI();
	}

	
	private void nextParticle()
	{
		this.exampleIndex++;
		if (this.exampleIndex >= this.ParticleExamples.Length)
		{
			this.exampleIndex = 0;
		}
		this.UpdateUI();
	}

	
	private void destroyParticles()
	{
		for (int i = this.onScreenParticles.Count - 1; i >= 0; i--)
		{
			if (this.onScreenParticles[i] != null)
			{
				UnityEngine.Object.Destroy(this.onScreenParticles[i]);
			}
			this.onScreenParticles.RemoveAt(i);
		}
	}

	
	public Renderer groundRenderer;

	
	public Collider groundCollider;

	
	[Space]
	[Space]
	public Image slowMoBtn;

	
	public Text slowMoLabel;

	
	public Image camRotBtn;

	
	public Text camRotLabel;

	
	public Image groundBtn;

	
	public Text groundLabel;

	
	[Space]
	public Text EffectLabel;

	
	public Text EffectIndexLabel;

	
	private GameObject[] ParticleExamples;

	
	private int exampleIndex;

	
	private bool slowMo;

	
	private Vector3 defaultCamPosition;

	
	private Quaternion defaultCamRotation;

	
	private List<GameObject> onScreenParticles = new List<GameObject>();
}
