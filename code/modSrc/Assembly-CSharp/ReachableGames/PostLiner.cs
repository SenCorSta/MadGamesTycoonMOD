using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames
{
	// Token: 0x020003E9 RID: 1001
	[PostProcess(typeof(PostLinerEffect), PostProcessEvent.BeforeTransparent, "ReachableGames/Post Liner", false)]
	[Serializable]
	public sealed class PostLiner : PostProcessEffectSettings
	{
		// Token: 0x04002DC4 RID: 11716
		[Tooltip("RGB controls the color of the fill.")]
		public ColorParameter fillColor = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x04002DC5 RID: 11717
		[Range(0f, 1f)]
		[Tooltip("At 0, only edges are drawn, at 1 the whole object is brightly tinted.")]
		public FloatParameter fillBlend = new FloatParameter
		{
			value = 0.122f
		};

		// Token: 0x04002DC6 RID: 11718
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter fillDepthFading = new FloatParameter
		{
			value = 0.083f
		};

		// Token: 0x04002DC7 RID: 11719
		[Space]
		[Tooltip("RGB controls the color of the outline.")]
		public ColorParameter outlineColor = new ColorParameter
		{
			value = Color.yellow
		};

		// Token: 0x04002DC8 RID: 11720
		[Range(0f, 10f)]
		[Tooltip("Larger values makes for thicker outlines.")]
		public FloatParameter lineThickness = new FloatParameter
		{
			value = 0.78f
		};

		// Token: 0x04002DC9 RID: 11721
		[Range(0f, 5f)]
		[Tooltip("With interpenetrating objects, this controls how much of the wrong object will have outlines too.  Depth map precision issue.")]
		public FloatParameter errorTolerance = new FloatParameter
		{
			value = 0.03f
		};

		// Token: 0x04002DCA RID: 11722
		[Space]
		[Range(1E-05f, 0.001f)]
		[Tooltip("Sensitivity to changes in depth.")]
		public FloatParameter topologySensitivity = new FloatParameter
		{
			value = 0.00027f
		};

		// Token: 0x04002DCB RID: 11723
		[Range(0f, 1f)]
		[Tooltip("Blend control for depth-based outlines.")]
		public FloatParameter topologyBlend = new FloatParameter
		{
			value = 0.68f
		};

		// Token: 0x04002DCC RID: 11724
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter topologyDepthFading = new FloatParameter
		{
			value = 0.02f
		};

		// Token: 0x04002DCD RID: 11725
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Control the amount of hard edge lines interior to the object.")]
		public FloatParameter hardEdgeBlend = new FloatParameter
		{
			value = 0.652f
		};

		// Token: 0x04002DCE RID: 11726
		[Range(0f, 1f)]
		[Tooltip("Controls distance where this effect fades away completely.  Value is a range between near and far plane.")]
		public FloatParameter hardEdgeDepthFading = new FloatParameter
		{
			value = 0.02f
		};

		// Token: 0x04002DCF RID: 11727
		[Space]
		[Range(0f, 1f)]
		[Tooltip("Master knob for the maximum blend amount.")]
		public FloatParameter finalBlend = new FloatParameter
		{
			value = 1f
		};
	}
}
