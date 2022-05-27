using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003EF RID: 1007
	[PostProcess(typeof(PostLinerEffect), PostProcessEvent.BeforeTransparent, "ReachableGames/Post Liner Pro", true)]
	[Serializable]
	public sealed class PostLinerPro : PostProcessEffectSettings
	{
		// Token: 0x04002DE9 RID: 11753
		[Tooltip("RGB controls the color of visible pixels.")]
		public ColorParameter fillColor = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002DEA RID: 11754
		[Tooltip("RGB controls the color of obscured pixels.")]
		public ColorParameter fillColorHidden = new ColorParameter
		{
			value = Color.black
		};

		// Token: 0x04002DEB RID: 11755
		[Range(0f, 1f)]
		[Tooltip("At 0, only edges are drawn, at 1 the whole object is brightly tinted.")]
		public FloatParameter fillBlend = new FloatParameter
		{
			value = 0.177f
		};

		// Token: 0x04002DEC RID: 11756
		[Range(0f, 1f)]
		[Tooltip("At 0, obscured pixels are not modified, at 1 all obscured pixels are fillColorHidden.")]
		public FloatParameter fillBlendHidden = new FloatParameter
		{
			value = 0.166f
		};

		// Token: 0x04002DED RID: 11757
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter fillDepthFading = new FloatParameter
		{
			value = 0.025f
		};

		// Token: 0x04002DEE RID: 11758
		[Space]
		[Tooltip("RGB controls the color of the outline.")]
		public ColorParameter outlineColor = new ColorParameter
		{
			value = Color.yellow
		};

		// Token: 0x04002DEF RID: 11759
		[Tooltip("RGB controls the color of the hidden outline.")]
		public ColorParameter outlineColorHidden = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002DF0 RID: 11760
		[Range(0f, 10f)]
		[Tooltip("Larger values makes for thicker outlines.")]
		public FloatParameter lineThickness = new FloatParameter
		{
			value = 0.58f
		};

		// Token: 0x04002DF1 RID: 11761
		[Range(0f, 1f)]
		[Tooltip("With interpenetrating objects, this controls how much of the wrong object will have outlines too.  Depth map precision issue.")]
		public FloatParameter errorTolerance = new FloatParameter
		{
			value = 0.231f
		};

		// Token: 0x04002DF2 RID: 11762
		[Space]
		[Range(1E-05f, 1f)]
		[Tooltip("Sensitivity to changes in depth.")]
		public FloatParameter topologySensitivity = new FloatParameter
		{
			value = 0.0001f
		};

		// Token: 0x04002DF3 RID: 11763
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines.")]
		public FloatParameter topologyBlend = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04002DF4 RID: 11764
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines that are obscured.")]
		public FloatParameter topologyBlendHidden = new FloatParameter
		{
			value = 0.68f
		};

		// Token: 0x04002DF5 RID: 11765
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter topologyDepthFading = new FloatParameter
		{
			value = 0.005f
		};

		// Token: 0x04002DF6 RID: 11766
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Control the amount of hard edge lines interior to the object.")]
		public FloatParameter hardEdgeBlend = new FloatParameter
		{
			value = 0.227f
		};

		// Token: 0x04002DF7 RID: 11767
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter hardEdgeDepthFading = new FloatParameter
		{
			value = 0.005f
		};

		// Token: 0x04002DF8 RID: 11768
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Master knob for distance over which fading out of effects happens.  Value is a relative range between near and far plane.")]
		public FloatParameter fadeDistance = new FloatParameter
		{
			value = 0.01f
		};

		// Token: 0x04002DF9 RID: 11769
		[Range(0f, 1f)]
		[Tooltip("Master knob for the maximum blend amount.")]
		public FloatParameter finalBlend = new FloatParameter
		{
			value = 1f
		};
	}
}
