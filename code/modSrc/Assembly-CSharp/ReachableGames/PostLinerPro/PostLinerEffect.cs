using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F3 RID: 1011
	public sealed class PostLinerEffect : PostProcessEffectRenderer<PostLinerPro>
	{
		// Token: 0x06002400 RID: 9216 RVA: 0x00172EEB File Offset: 0x001710EB
		public override DepthTextureMode GetCameraFlags()
		{
			return base.GetCameraFlags() | DepthTextureMode.DepthNormals;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x001735A8 File Offset: 0x001717A8
		public override void Render(PostProcessRenderContext context)
		{
			Texture globalTexture = Shader.GetGlobalTexture(PostLinerEffect._globalTextureId);
			if (globalTexture != null)
			{
				PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/ReachableGames/PostLinerPro"));
				propertySheet.properties.SetVector(PostLinerEffect._pixelOffsetId, new Vector4(1f / (float)globalTexture.width, 1f / (float)globalTexture.height, 0f, 0f));
				propertySheet.properties.SetColor(PostLinerEffect._fillColorId, base.settings.fillColor);
				propertySheet.properties.SetColor(PostLinerEffect._fillColorHiddenId, base.settings.fillColorHidden);
				propertySheet.properties.SetFloat(PostLinerEffect._fillBlendId, base.settings.fillBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._fillBlendHiddenId, base.settings.fillBlendHidden);
				propertySheet.properties.SetFloat(PostLinerEffect._fillDepthFadingId, base.settings.fillDepthFading);
				propertySheet.properties.SetColor(PostLinerEffect._outlineColorId, base.settings.outlineColor);
				propertySheet.properties.SetColor(PostLinerEffect._outlineColorHiddenId, base.settings.outlineColorHidden);
				propertySheet.properties.SetFloat(PostLinerEffect._lineThicknessId, base.settings.lineThickness);
				propertySheet.properties.SetFloat(PostLinerEffect._errorToleranceId, base.settings.errorTolerance);
				propertySheet.properties.SetFloat(PostLinerEffect._topologySensitivityId, base.settings.topologySensitivity);
				propertySheet.properties.SetFloat(PostLinerEffect._topologyBlendId, base.settings.topologyBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._topologyBlendHiddenId, base.settings.topologyBlendHidden);
				propertySheet.properties.SetFloat(PostLinerEffect._topologyDepthFadingId, base.settings.topologyDepthFading);
				propertySheet.properties.SetFloat(PostLinerEffect._hardEdgeBlendId, base.settings.hardEdgeBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._hardEdgeDepthFadingId, base.settings.hardEdgeDepthFading);
				propertySheet.properties.SetFloat(PostLinerEffect._fadeDistanceId, base.settings.fadeDistance);
				propertySheet.properties.SetFloat(PostLinerEffect._finalBlendId, base.settings.finalBlend);
				context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
				return;
			}
			context.command.CopyTexture(context.source, context.destination);
		}

		// Token: 0x04002E10 RID: 11792
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002E11 RID: 11793
		private static int _pixelOffsetId = Shader.PropertyToID("_PixelOffset");

		// Token: 0x04002E12 RID: 11794
		private static int _fillColorId = Shader.PropertyToID("_FillColor");

		// Token: 0x04002E13 RID: 11795
		private static int _fillColorHiddenId = Shader.PropertyToID("_FillColorHidden");

		// Token: 0x04002E14 RID: 11796
		private static int _fillBlendId = Shader.PropertyToID("_FillBlend");

		// Token: 0x04002E15 RID: 11797
		private static int _fillBlendHiddenId = Shader.PropertyToID("_FillBlendHidden");

		// Token: 0x04002E16 RID: 11798
		private static int _fillDepthFadingId = Shader.PropertyToID("_FillDepthFading");

		// Token: 0x04002E17 RID: 11799
		private static int _outlineColorId = Shader.PropertyToID("_OutlineColor");

		// Token: 0x04002E18 RID: 11800
		private static int _outlineColorHiddenId = Shader.PropertyToID("_OutlineColorHidden");

		// Token: 0x04002E19 RID: 11801
		private static int _lineThicknessId = Shader.PropertyToID("_LineThickness");

		// Token: 0x04002E1A RID: 11802
		private static int _errorToleranceId = Shader.PropertyToID("_ErrorTolerance");

		// Token: 0x04002E1B RID: 11803
		private static int _topologySensitivityId = Shader.PropertyToID("_TopologySensitivity");

		// Token: 0x04002E1C RID: 11804
		private static int _topologyBlendId = Shader.PropertyToID("_TopologyBlend");

		// Token: 0x04002E1D RID: 11805
		private static int _topologyBlendHiddenId = Shader.PropertyToID("_TopologyBlendHidden");

		// Token: 0x04002E1E RID: 11806
		private static int _topologyDepthFadingId = Shader.PropertyToID("_TopologyDepthFading");

		// Token: 0x04002E1F RID: 11807
		private static int _hardEdgeBlendId = Shader.PropertyToID("_HardEdgeBlend");

		// Token: 0x04002E20 RID: 11808
		private static int _hardEdgeDepthFadingId = Shader.PropertyToID("_HardEdgeDepthFading");

		// Token: 0x04002E21 RID: 11809
		private static int _fadeDistanceId = Shader.PropertyToID("_FadeDistance");

		// Token: 0x04002E22 RID: 11810
		private static int _finalBlendId = Shader.PropertyToID("_FinalBlend");
	}
}
