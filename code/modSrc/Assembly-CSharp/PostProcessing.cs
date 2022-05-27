using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x020002CC RID: 716
public class PostProcessing : MonoBehaviour
{
	// Token: 0x060019F9 RID: 6649 RVA: 0x00108DB0 File Offset: 0x00106FB0
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

	// Token: 0x060019FA RID: 6650 RVA: 0x00108EC1 File Offset: 0x001070C1
	public void BlendIn()
	{
		this.blend = 1f;
		this.chromatic.intensity.value = 1f;
		this.vignette.intensity.value = 1f;
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x00108EF8 File Offset: 0x001070F8
	private void Update()
	{
		this.blend -= Time.deltaTime * 1f;
		this.chromatic.intensity.value = this.blend;
		this.vignette.intensity.value = this.blend;
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x00108F49 File Offset: 0x00107149
	private void OnDestroy()
	{
		RuntimeUtilities.DestroyVolume(this.volumeChromatic, true, true);
		RuntimeUtilities.DestroyVolume(this.volumeVignette, true, true);
	}

	// Token: 0x040020EB RID: 8427
	private float time = 20f;

	// Token: 0x040020EC RID: 8428
	private float targetChromaticIntensityUpper = 1.5f;

	// Token: 0x040020ED RID: 8429
	private float targetChromaticIntensityLower;

	// Token: 0x040020EE RID: 8430
	private float currentChromaticIntensity;

	// Token: 0x040020EF RID: 8431
	private ChromaticAberration chromatic;

	// Token: 0x040020F0 RID: 8432
	private PostProcessVolume volumeChromatic;

	// Token: 0x040020F1 RID: 8433
	private float targetVignetteIntensityUpper = 0.43f;

	// Token: 0x040020F2 RID: 8434
	private float targetVignetteIntensityLower = 0.2f;

	// Token: 0x040020F3 RID: 8435
	private float targetVignetteSmoothness = 0.3f;

	// Token: 0x040020F4 RID: 8436
	private float targetVignetteRoundness = 1f;

	// Token: 0x040020F5 RID: 8437
	private bool targetVignetteRounded;

	// Token: 0x040020F6 RID: 8438
	private float currentVignetteIntensity;

	// Token: 0x040020F7 RID: 8439
	private Vignette vignette;

	// Token: 0x040020F8 RID: 8440
	private PostProcessVolume volumeVignette;

	// Token: 0x040020F9 RID: 8441
	private float blend = 1f;
}
