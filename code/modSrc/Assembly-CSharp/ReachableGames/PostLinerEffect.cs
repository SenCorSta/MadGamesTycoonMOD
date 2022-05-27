using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ReachableGames
{
	
	public sealed class PostLinerEffect : PostProcessEffectRenderer<PostLiner>
	{
		
		public override DepthTextureMode GetCameraFlags()
		{
			return base.GetCameraFlags() | DepthTextureMode.DepthNormals;
		}

		
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

		
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		
		private static int _pixelOffsetId = Shader.PropertyToID("_PixelOffset");

		
		private static int _fillColorId = Shader.PropertyToID("_FillColor");

		
		private static int _fillBlendId = Shader.PropertyToID("_FillBlend");

		
		private static int _fillDepthFadingId = Shader.PropertyToID("_FillDepthFading");

		
		private static int _outlineColorId = Shader.PropertyToID("_OutlineColor");

		
		private static int _lineThicknessId = Shader.PropertyToID("_LineThickness");

		
		private static int _errorToleranceId = Shader.PropertyToID("_ErrorTolerance");

		
		private static int _topologySensitivityId = Shader.PropertyToID("_TopologySensitivity");

		
		private static int _topologyBlendId = Shader.PropertyToID("_TopologyBlend");

		
		private static int _topologyDepthFadingId = Shader.PropertyToID("_TopologyDepthFading");

		
		private static int _hardEdgeBlendId = Shader.PropertyToID("_HardEdgeBlend");

		
		private static int _hardEdgeDepthFadingId = Shader.PropertyToID("_HardEdgeDepthFading");

		
		private static int _finalBlendId = Shader.PropertyToID("_FinalBlend");
	}
}
