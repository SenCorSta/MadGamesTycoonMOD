using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x020002C9 RID: 713
public class PostProcessing : MonoBehaviour
{
	// Token: 0x060019AF RID: 6575 RVA: 0x0010D1E4 File Offset: 0x0010B3E4
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

	// Token: 0x060019B0 RID: 6576 RVA: 0x0001156C File Offset: 0x0000F76C
	public void BlendIn()
	{
		this.blend = 1f;
		this.chromatic.intensity.value = 1f;
		this.vignette.intensity.value = 1f;
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x0010D2F8 File Offset: 0x0010B4F8
	private void Update()
	{
		this.blend -= Time.deltaTime * 1f;
		this.chromatic.intensity.value = this.blend;
		this.vignette.intensity.value = this.blend;
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x000115A3 File Offset: 0x0000F7A3
	private void OnDestroy()
	{
		RuntimeUtilities.DestroyVolume(this.volumeChromatic, true, true);
		RuntimeUtilities.DestroyVolume(this.volumeVignette, true, true);
	}

	// Token: 0x040020D1 RID: 8401
	private float time = 20f;

	// Token: 0x040020D2 RID: 8402
	private float targetChromaticIntensityUpper = 1.5f;

	// Token: 0x040020D3 RID: 8403
	private float targetChromaticIntensityLower;

	// Token: 0x040020D4 RID: 8404
	private float currentChromaticIntensity;

	// Token: 0x040020D5 RID: 8405
	private ChromaticAberration chromatic;

	// Token: 0x040020D6 RID: 8406
	private PostProcessVolume volumeChromatic;

	// Token: 0x040020D7 RID: 8407
	private float targetVignetteIntensityUpper = 0.43f;

	// Token: 0x040020D8 RID: 8408
	private float targetVignetteIntensityLower = 0.2f;

	// Token: 0x040020D9 RID: 8409
	private float targetVignetteSmoothness = 0.3f;

	// Token: 0x040020DA RID: 8410
	private float targetVignetteRoundness = 1f;

	// Token: 0x040020DB RID: 8411
	private bool targetVignetteRounded;

	// Token: 0x040020DC RID: 8412
	private float currentVignetteIntensity;

	// Token: 0x040020DD RID: 8413
	private Vignette vignette;

	// Token: 0x040020DE RID: 8414
	private PostProcessVolume volumeVignette;

	// Token: 0x040020DF RID: 8415
	private float blend = 1f;
}
