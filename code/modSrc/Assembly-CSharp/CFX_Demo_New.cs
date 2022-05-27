using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
public class CFX_Demo_New : MonoBehaviour
{
	// Token: 0x06000029 RID: 41 RVA: 0x000030D8 File Offset: 0x000012D8
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

	// Token: 0x0600002A RID: 42 RVA: 0x0000318C File Offset: 0x0000138C
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

	// Token: 0x0600002B RID: 43 RVA: 0x000032A0 File Offset: 0x000014A0
	public void OnToggleGround()
	{
		Color white = Color.white;
		this.groundRenderer.enabled = !this.groundRenderer.enabled;
		white.a = (this.groundRenderer.enabled ? 1f : 0.33f);
		this.groundBtn.color = white;
		this.groundLabel.color = white;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003304 File Offset: 0x00001504
	public void OnToggleCamera()
	{
		Color white = Color.white;
		CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
		white.a = (CFX_Demo_RotateCamera.rotating ? 1f : 0.33f);
		this.camRotBtn.color = white;
		this.camRotLabel.color = white;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003358 File Offset: 0x00001558
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

	// Token: 0x0600002E RID: 46 RVA: 0x000033C8 File Offset: 0x000015C8
	public void OnPreviousEffect()
	{
		this.prevParticle();
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000033D0 File Offset: 0x000015D0
	public void OnNextEffect()
	{
		this.nextParticle();
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000033D8 File Offset: 0x000015D8
	private void UpdateUI()
	{
		this.EffectLabel.text = this.ParticleExamples[this.exampleIndex].name;
		this.EffectIndexLabel.text = string.Format("{0}/{1}", (this.exampleIndex + 1).ToString("00"), this.ParticleExamples.Length.ToString("00"));
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003444 File Offset: 0x00001644
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

	// Token: 0x06000032 RID: 50 RVA: 0x000034DA File Offset: 0x000016DA
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

	// Token: 0x06000033 RID: 51 RVA: 0x000034E9 File Offset: 0x000016E9
	private void prevParticle()
	{
		this.exampleIndex--;
		if (this.exampleIndex < 0)
		{
			this.exampleIndex = this.ParticleExamples.Length - 1;
		}
		this.UpdateUI();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003518 File Offset: 0x00001718
	private void nextParticle()
	{
		this.exampleIndex++;
		if (this.exampleIndex >= this.ParticleExamples.Length)
		{
			this.exampleIndex = 0;
		}
		this.UpdateUI();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003548 File Offset: 0x00001748
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

	// Token: 0x0400001E RID: 30
	public Renderer groundRenderer;

	// Token: 0x0400001F RID: 31
	public Collider groundCollider;

	// Token: 0x04000020 RID: 32
	[Space]
	[Space]
	public Image slowMoBtn;

	// Token: 0x04000021 RID: 33
	public Text slowMoLabel;

	// Token: 0x04000022 RID: 34
	public Image camRotBtn;

	// Token: 0x04000023 RID: 35
	public Text camRotLabel;

	// Token: 0x04000024 RID: 36
	public Image groundBtn;

	// Token: 0x04000025 RID: 37
	public Text groundLabel;

	// Token: 0x04000026 RID: 38
	[Space]
	public Text EffectLabel;

	// Token: 0x04000027 RID: 39
	public Text EffectIndexLabel;

	// Token: 0x04000028 RID: 40
	private GameObject[] ParticleExamples;

	// Token: 0x04000029 RID: 41
	private int exampleIndex;

	// Token: 0x0400002A RID: 42
	private bool slowMo;

	// Token: 0x0400002B RID: 43
	private Vector3 defaultCamPosition;

	// Token: 0x0400002C RID: 44
	private Quaternion defaultCamRotation;

	// Token: 0x0400002D RID: 45
	private List<GameObject> onScreenParticles = new List<GameObject>();
}
