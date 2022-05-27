using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F2 RID: 1010
	[PostProcess(typeof(PostLinerEffect), PostProcessEvent.BeforeTransparent, "ReachableGames/Post Liner Pro", true)]
	[Serializable]
	public sealed class PostLinerPro : PostProcessEffectSettings
	{
		// Token: 0x04002DFF RID: 11775
		[Tooltip("RGB controls the color of visible pixels.")]
		public ColorParameter fillColor = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002E00 RID: 11776
		[Tooltip("RGB controls the color of obscured pixels.")]
		public ColorParameter fillColorHidden = new ColorParameter
		{
			value = Color.black
		};

		// Token: 0x04002E01 RID: 11777
		[Range(0f, 1f)]
		[Tooltip("At 0, only edges are drawn, at 1 the whole object is brightly tinted.")]
		public FloatParameter fillBlend = new FloatParameter
		{
			value = 0.177f
		};

		// Token: 0x04002E02 RID: 11778
		[Range(0f, 1f)]
		[Tooltip("At 0, obscured pixels are not modified, at 1 all obscured pixels are fillColorHidden.")]
		public FloatParameter fillBlendHidden = new FloatParameter
		{
			value = 0.166f
		};

		// Token: 0x04002E03 RID: 11779
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter fillDepthFading = new FloatParameter
		{
			value = 0.025f
		};

		// Token: 0x04002E04 RID: 11780
		[Space]
		[Tooltip("RGB controls the color of the outline.")]
		public ColorParameter outlineColor = new ColorParameter
		{
			value = Color.yellow
		};

		// Token: 0x04002E05 RID: 11781
		[Tooltip("RGB controls the color of the hidden outline.")]
		public ColorParameter outlineColorHidden = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002E06 RID: 11782
		[Range(0f, 10f)]
		[Tooltip("Larger values makes for thicker outlines.")]
		public FloatParameter lineThickness = new FloatParameter
		{
			value = 0.58f
		};

		// Token: 0x04002E07 RID: 11783
		[Range(0f, 1f)]
		[Tooltip("With interpenetrating objects, this controls how much of the wrong object will have outlines too.  Depth map precision issue.")]
		public FloatParameter errorTolerance = new FloatParameter
		{
			value = 0.231f
		};

		// Token: 0x04002E08 RID: 11784
		[Space]
		[Range(1E-05f, 1f)]
		[Tooltip("Sensitivity to changes in depth.")]
		public FloatParameter topologySensitivity = new FloatParameter
		{
			value = 0.0001f
		};

		// Token: 0x04002E09 RID: 11785
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines.")]
		public FloatParameter topologyBlend = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04002E0A RID: 11786
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines that are obscured.")]
		public FloatParameter topologyBlendHidden = new FloatParameter
		{
			value = 0.68f
		};

		// Token: 0x04002E0B RID: 11787
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter topologyDepthFading = new FloatParameter
		{
			value = 0.005f
		};

		// Token: 0x04002E0C RID: 11788
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Control the amount of hard edge lines interior to the object.")]
		public FloatParameter hardEdgeBlend = new FloatParameter
		{
			value = 0.227f
		};

		// Token: 0x04002E0D RID: 11789
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter hardEdgeDepthFading = new FloatParameter
		{
			value = 0.005f
		};

		// Token: 0x04002E0E RID: 11790
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Master knob for distance over which fading out of effects happens.  Value is a relative range between near and far plane.")]
		public FloatParameter fadeDistance = new FloatParameter
		{
			value = 0.01f
		};

		// Token: 0x04002E0F RID: 11791
		[Range(0f, 1f)]
		[Tooltip("Master knob for the maximum blend amount.")]
		public FloatParameter finalBlend = new FloatParameter
		{
			value = 1f
		};
	}
}
