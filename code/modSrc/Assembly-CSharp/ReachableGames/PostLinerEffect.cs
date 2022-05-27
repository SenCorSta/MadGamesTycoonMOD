using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames
{
	// Token: 0x020003EA RID: 1002
	public sealed class PostLinerEffect : PostProcessEffectRenderer<PostLiner>
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x0001844C File Offset: 0x0001664C
		public override DepthTextureMode GetCameraFlags()
		{
			return base.GetCameraFlags() | DepthTextureMode.DepthNormals;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x001701C0 File Offset: 0x0016E3C0
		public override void Render(PostProcessRenderContext context)
		{
			Texture globalTexture = Shader.GetGlobalTexture(PostLinerEffect._globalTextureId);
			if (globalTexture != null)
			{
				PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/ReachableGames/PostLiner"));
				propertySheet.properties.SetVector(PostLinerEffect._pixelOffsetId, new Vector4(1f / (float)globalTexture.width, 1f / (float)globalTexture.height, 0f, 0f));
				propertySheet.properties.SetColor(PostLinerEffect._fillColorId, base.settings.fillColor);
				propertySheet.properties.SetFloat(PostLinerEffect._fillBlendId, base.settings.fillBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._fillDepthFadingId, base.settings.fillDepthFading);
				propertySheet.properties.SetColor(PostLinerEffect._outlineColorId, base.settings.outlineColor);
				propertySheet.properties.SetFloat(PostLinerEffect._lineThicknessId, base.settings.lineThickness);
				propertySheet.properties.SetFloat(PostLinerEffect._errorToleranceId, base.settings.errorTolerance);
				propertySheet.properties.SetFloat(PostLinerEffect._topologySensitivityId, base.settings.topologySensitivity);
				propertySheet.properties.SetFloat(PostLinerEffect._topologyBlendId, base.settings.topologyBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._topologyDepthFadingId, base.settings.topologyDepthFading);
				propertySheet.properties.SetFloat(PostLinerEffect._hardEdgeBlendId, base.settings.hardEdgeBlend);
				propertySheet.properties.SetFloat(PostLinerEffect._hardEdgeDepthFadingId, base.settings.hardEdgeDepthFading);
				propertySheet.properties.SetFloat(PostLinerEffect._finalBlendId, base.settings.finalBlend);
				context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
				return;
			}
			context.command.CopyTexture(context.source, context.destination);
		}

		// Token: 0x04002DD0 RID: 11728
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002DD1 RID: 11729
		private static int _pixelOffsetId = Shader.PropertyToID("_PixelOffset");

		// Token: 0x04002DD2 RID: 11730
		private static int _fillColorId = Shader.PropertyToID("_FillColor");

		// Token: 0x04002DD3 RID: 11731
		private static int _fillBlendId = Shader.PropertyToID("_FillBlend");

		// Token: 0x04002DD4 RID: 11732
		private static int _fillDepthFadingId = Shader.PropertyToID("_FillDepthFading");

		// Token: 0x04002DD5 RID: 11733
		private static int _outlineColorId = Shader.PropertyToID("_OutlineColor");

		// Token: 0x04002DD6 RID: 11734
		private static int _lineThicknessId = Shader.PropertyToID("_LineThickness");

		// Token: 0x04002DD7 RID: 11735
		private static int _errorToleranceId = Shader.PropertyToID("_ErrorTolerance");

		// Token: 0x04002DD8 RID: 11736
		private static int _topologySensitivityId = Shader.PropertyToID("_TopologySensitivity");

		// Token: 0x04002DD9 RID: 11737
		private static int _topologyBlendId = Shader.PropertyToID("_TopologyBlend");

		// Token: 0x04002DDA RID: 11738
		private static int _topologyDepthFadingId = Shader.PropertyToID("_TopologyDepthFading");

		// Token: 0x04002DDB RID: 11739
		private static int _hardEdgeBlendId = Shader.PropertyToID("_HardEdgeBlend");

		// Token: 0x04002DDC RID: 11740
		private static int _hardEdgeDepthFadingId = Shader.PropertyToID("_HardEdgeDepthFading");

		// Token: 0x04002DDD RID: 11741
		private static int _finalBlendId = Shader.PropertyToID("_FinalBlend");
	}
}
