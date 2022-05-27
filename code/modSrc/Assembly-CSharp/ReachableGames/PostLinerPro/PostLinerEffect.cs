using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F0 RID: 1008
	public sealed class PostLinerEffect : PostProcessEffectRenderer<PostLinerPro>
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x0001844C File Offset: 0x0001664C
		public override DepthTextureMode GetCameraFlags()
		{
			return base.GetCameraFlags() | DepthTextureMode.DepthNormals;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x001707FC File Offset: 0x0016E9FC
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

		// Token: 0x04002DFA RID: 11770
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002DFB RID: 11771
		private static int _pixelOffsetId = Shader.PropertyToID("_PixelOffset");

		// Token: 0x04002DFC RID: 11772
		private static int _fillColorId = Shader.PropertyToID("_FillColor");

		// Token: 0x04002DFD RID: 11773
		private static int _fillColorHiddenId = Shader.PropertyToID("_FillColorHidden");

		// Token: 0x04002DFE RID: 11774
		private static int _fillBlendId = Shader.PropertyToID("_FillBlend");

		// Token: 0x04002DFF RID: 11775
		private static int _fillBlendHiddenId = Shader.PropertyToID("_FillBlendHidden");

		// Token: 0x04002E00 RID: 11776
		private static int _fillDepthFadingId = Shader.PropertyToID("_FillDepthFading");

		// Token: 0x04002E01 RID: 11777
		private static int _outlineColorId = Shader.PropertyToID("_OutlineColor");

		// Token: 0x04002E02 RID: 11778
		private static int _outlineColorHiddenId = Shader.PropertyToID("_OutlineColorHidden");

		// Token: 0x04002E03 RID: 11779
		private static int _lineThicknessId = Shader.PropertyToID("_LineThickness");

		// Token: 0x04002E04 RID: 11780
		private static int _errorToleranceId = Shader.PropertyToID("_ErrorTolerance");

		// Token: 0x04002E05 RID: 11781
		private static int _topologySensitivityId = Shader.PropertyToID("_TopologySensitivity");

		// Token: 0x04002E06 RID: 11782
		private static int _topologyBlendId = Shader.PropertyToID("_TopologyBlend");

		// Token: 0x04002E07 RID: 11783
		private static int _topologyBlendHiddenId = Shader.PropertyToID("_TopologyBlendHidden");

		// Token: 0x04002E08 RID: 11784
		private static int _topologyDepthFadingId = Shader.PropertyToID("_TopologyDepthFading");

		// Token: 0x04002E09 RID: 11785
		private static int _hardEdgeBlendId = Shader.PropertyToID("_HardEdgeBlend");

		// Token: 0x04002E0A RID: 11786
		private static int _hardEdgeDepthFadingId = Shader.PropertyToID("_HardEdgeDepthFading");

		// Token: 0x04002E0B RID: 11787
		private static int _fadeDistanceId = Shader.PropertyToID("_FadeDistance");

		// Token: 0x04002E0C RID: 11788
		private static int _finalBlendId = Shader.PropertyToID("_FinalBlend");
	}
}
