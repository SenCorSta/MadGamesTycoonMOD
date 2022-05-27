using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames
{
	// Token: 0x020003EC RID: 1004
	[PostProcess(typeof(PostLinerEffect), PostProcessEvent.BeforeTransparent, "ReachableGames/Post Liner", false)]
	[Serializable]
	public sealed class PostLiner : PostProcessEffectSettings
	{
		// Token: 0x04002DDA RID: 11738
		[Tooltip("RGB controls the color of the fill.")]
		public ColorParameter fillColor = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002DDB RID: 11739
		[Range(0f, 1f)]
		[Tooltip("At 0, only edges are drawn, at 1 the whole object is brightly tinted.")]
		public FloatParameter fillBlend = new FloatParameter
		{
			value = 0.122f
		};

		// Token: 0x04002DDC RID: 11740
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter fillDepthFading = new FloatParameter
		{
			value = 0.083f
		};

		// Token: 0x04002DDD RID: 11741
		[Space]
		[Tooltip("RGB controls the color of the outline.")]
		public ColorParameter outlineColor = new ColorParameter
		{
			value = Color.yellow
		};

		// Token: 0x04002DDE RID: 11742
		[Range(0f, 10f)]
		[Tooltip("Larger values makes for thicker outlines.")]
		public FloatParameter lineThickness = new FloatParameter
		{
			value = 0.78f
		};

		// Token: 0x04002DDF RID: 11743
		[Range(0f, 5f)]
		[Tooltip("With interpenetrating objects, this controls how much of the wrong object will have outlines too.  Depth map precision issue.")]
		public FloatParameter errorTolerance = new FloatParameter
		{
			value = 0.03f
		};

		// Token: 0x04002DE0 RID: 11744
		[Space]
		[Range(1E-05f, 0.001f)]
		[Tooltip("Sensitivity to changes in depth.")]
		public FloatParameter topologySensitivity = new FloatParameter
		{
			value = 0.00027f
		};

		// Token: 0x04002DE1 RID: 11745
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines.")]
		public FloatParameter topologyBlend = new FloatParameter
		{
			value = 0.68f
		};

		// Token: 0x04002DE2 RID: 11746
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter topologyDepthFading = new FloatParameter
		{
			value = 0.02f
		};

		// Token: 0x04002DE3 RID: 11747
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Control the amount of hard edge lines interior to the object.")]
		public FloatParameter hardEdgeBlend = new FloatParameter
		{
			value = 0.652f
		};

		// Token: 0x04002DE4 RID: 11748
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter hardEdgeDepthFading = new FloatParameter
		{
			value = 0.02f
		};

		// Token: 0x04002DE5 RID: 11749
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Master knob for the maximum blend amount.")]
		public FloatParameter finalBlend = new FloatParameter
		{
			value = 1f
		};
	}
}
