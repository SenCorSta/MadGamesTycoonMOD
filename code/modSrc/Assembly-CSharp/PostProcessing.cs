using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessing : MonoBehaviour
{
	
	private void Start()
	{
		this.chromatic = ScriptableObject.CreateInstance<ChromaticAberration>();
		this.chromatic.enabled.Override(true);
		this.chromatic.intensity.Override(this.targetChromaticIntensityUpper);
		this.volumeChromatic = PostProcessManager.instance.QuickVolume(base.gameObject.layer, 100f, new PostProcessEffectSettings[]
		{
			this.chromatic
		});
		this.vignette = ScriptableObject.CreateInstance<Vignette>();
		this.vignette.enabled.Override(true);
		this.vignette.intensity.Override(this.targetVignetteIntensityUpper);
		this.vignette.smoothness.Override(this.targetVignetteSmoothness);
		this.vignette.roundness.Override(this.targetVignetteRoundness);
		this.vignette.rounded.Override(this.targetVignetteRounded);
		this.volumeVignette = PostProcessManager.instance.QuickVolume(base.gameObject.layer, 90f, new PostProcessEffectSettings[]
		{
			this.vignette
		});
	}

	
	public void BlendIn()
	{
		this.blend = 1f;
		this.chromatic.intensity.value = 1f;
		this.vignette.intensity.value = 1f;
	}

	
	private void Update()
	{
		this.blend -= Time.deltaTime * 1f;
		this.chromatic.intensity.value = this.blend;
		this.vignette.intensity.value = this.blend;
	}

	
	private void OnDestroy()
	{
		RuntimeUtilities.DestroyVolume(this.volumeChromatic, true, true);
		RuntimeUtilities.DestroyVolume(this.volumeVignette, true, true);
	}

	
	private float time = 20f;

	
	private float targetChromaticIntensityUpper = 1.5f;

	
	private float targetChromaticIntensityLower;

	
	private float currentChromaticIntensity;

	
	private ChromaticAberration chromatic;

	
	private PostProcessVolume volumeChromatic;

	
	private float targetVignetteIntensityUpper = 0.43f;

	
	private float targetVignetteIntensityLower = 0.2f;

	
	private float targetVignetteSmoothness = 0.3f;

	
	private float targetVignetteRoundness = 1f;

	
	private bool targetVignetteRounded;

	
	private float currentVignetteIntensity;

	
	private Vignette vignette;

	
	private PostProcessVolume volumeVignette;

	
	private float blend = 1f;
}
