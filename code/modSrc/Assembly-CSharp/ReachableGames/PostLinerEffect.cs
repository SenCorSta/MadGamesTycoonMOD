using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames
{
	// Token: 0x020003ED RID: 1005
	public sealed class PostLinerEffect : PostProcessEffectRenderer<PostLiner>
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x00172EEB File Offset: 0x001710EB
		public override DepthTextureMode GetCameraFlags()
		{
			return base.GetCameraFlags() | DepthTextureMode.DepthNormals;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00172EF8 File Offset: 0x001710F8
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

		// Token: 0x04002DE6 RID: 11750
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002DE7 RID: 11751
		private static int _pixelOffsetId = Shader.PropertyToID("_PixelOffset");

		// Token: 0x04002DE8 RID: 11752
		private static int _fillColorId = Shader.PropertyToID("_FillColor");

		// Token: 0x04002DE9 RID: 11753
		private static int _fillBlendId = Shader.PropertyToID("_FillBlend");

		// Token: 0x04002DEA RID: 11754
		private static int _fillDepthFadingId = Shader.PropertyToID("_FillDepthFading");

		// Token: 0x04002DEB RID: 11755
		private static int _outlineColorId = Shader.PropertyToID("_OutlineColor");

		// Token: 0x04002DEC RID: 11756
		private static int _lineThicknessId = Shader.PropertyToID("_LineThickness");

		// Token: 0x04002DED RID: 11757
		private static int _errorToleranceId = Shader.PropertyToID("_ErrorTolerance");

		// Token: 0x04002DEE RID: 11758
		private static int _topologySensitivityId = Shader.PropertyToID("_TopologySensitivity");

		// Token: 0x04002DEF RID: 11759
		private static int _topologyBlendId = Shader.PropertyToID("_TopologyBlend");

		// Token: 0x04002DF0 RID: 11760
		private static int _topologyDepthFadingId = Shader.PropertyToID("_TopologyDepthFading");

		// Token: 0x04002DF1 RID: 11761
		private static int _hardEdgeBlendId = Shader.PropertyToID("_HardEdgeBlend");

		// Token: 0x04002DF2 RID: 11762
		private static int _hardEdgeDepthFadingId = Shader.PropertyToID("_HardEdgeDepthFading");

		// Token: 0x04002DF3 RID: 11763
		private static int _finalBlendId = Shader.PropertyToID("_FinalBlend");
	}
}
