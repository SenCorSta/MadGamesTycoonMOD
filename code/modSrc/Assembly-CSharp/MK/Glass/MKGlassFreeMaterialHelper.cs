using System;
using UnityEngine;

namespace MK.Glass
{
	// Token: 0x020003AE RID: 942
	public static class MKGlassFreeMaterialHelper
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x00017134 File Offset: 0x00015334
		public static void SetMainTint(Material material, float tint)
		{
			material.SetFloat("_MainTint", tint);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00017142 File Offset: 0x00015342
		public static float GetMainTint(Material material)
		{
			return material.GetFloat("_MainTint");
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0001714F File Offset: 0x0001534F
		public static void SetMainTexture(Material material, Texture tex)
		{
			material.SetTexture("_MainTex", tex);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0001715D File Offset: 0x0001535D
		public static Texture GetMainTexture(Material material)
		{
			return material.GetTexture("_MainTex");
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0001716A File Offset: 0x0001536A
		public static void SetMainColor(Material material, Color color)
		{
			material.SetColor("_Color", color);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00017178 File Offset: 0x00015378
		public static Color GetMainColor(Material material)
		{
			return material.GetColor("_Color");
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00017185 File Offset: 0x00015385
		public static void SetNormalmap(Material material, Texture tex)
		{
			material.SetTexture("_BumpMap", tex);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00017193 File Offset: 0x00015393
		public static Texture GetBumpMap(Material material)
		{
			return material.GetTexture("_BumpMap");
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000171A0 File Offset: 0x000153A0
		public static void SetDistortion(Material material, float distortion)
		{
			material.SetFloat("_Distortion", distortion);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000171AE File Offset: 0x000153AE
		public static float GetDistortion(Material material)
		{
			return material.GetFloat("_Distortion");
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000171BB File Offset: 0x000153BB
		public static void SetRimColor(Material material, Color color)
		{
			material.SetColor("_RimColor", color);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000171C9 File Offset: 0x000153C9
		public static Color GetRimColor(Material material)
		{
			return material.GetColor("_RimColor");
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000171D6 File Offset: 0x000153D6
		public static void SetRimSize(Material material, float size)
		{
			material.SetFloat("_RimSize", size);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000171E4 File Offset: 0x000153E4
		public static float GetRimSize(Material material)
		{
			return material.GetFloat("_RimSize");
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000171F1 File Offset: 0x000153F1
		public static void SetRimIntensity(Material material, float intensity)
		{
			material.SetFloat("_RimIntensity", intensity);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000171FF File Offset: 0x000153FF
		public static float GetRimIntensity(Material material)
		{
			return material.GetFloat("_RimIntensity");
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0001720C File Offset: 0x0001540C
		public static void SetSpecularShininess(Material material, float shininess)
		{
			material.SetFloat("_Shininess", shininess);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0001721A File Offset: 0x0001541A
		public static float GetSpecularShininess(Material material)
		{
			return material.GetFloat("_Shininess");
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00017227 File Offset: 0x00015427
		public static void SetSpecularColor(Material material, Color color)
		{
			material.SetColor("_SpecColor", color);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x00017235 File Offset: 0x00015435
		public static Color GetSpecularColor(Material material)
		{
			return material.GetColor("_SpecColor");
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00017242 File Offset: 0x00015442
		public static void SetSpecularIntensity(Material material, float intensity)
		{
			material.SetFloat("_SpecularIntensity", intensity);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00017250 File Offset: 0x00015450
		public static float GetSpecularIntensity(Material material)
		{
			return material.GetFloat("_SpecularIntensity");
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0001725D File Offset: 0x0001545D
		public static void SetEmissionColor(Material material, Color color)
		{
			material.SetColor("_EmissionColor", color);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0001726B File Offset: 0x0001546B
		public static Color GetEmissionColor(Material material)
		{
			return material.GetColor("_EmissionColor");
		}

		// Token: 0x020003AF RID: 943
		public static class PropertyNames
		{
			// Token: 0x04002CEA RID: 11498
			public const string SHOW_MAIN_BEHAVIOR = "_MKEditorShowMainBehavior";

			// Token: 0x04002CEB RID: 11499
			public const string SHOW_LIGHT_BEHAVIOR = "_MKEditorShowLightBehavior";

			// Token: 0x04002CEC RID: 11500
			public const string SHOW_RENDER_BEHAVIOR = "_MKEditorShowRenderBehavior";

			// Token: 0x04002CED RID: 11501
			public const string SHOW_SPECULAR_BEHAVIOR = "_MKEditorShowSpecularBehavior";

			// Token: 0x04002CEE RID: 11502
			public const string SHOW_RIM_BEHAVIOR = "_MKEditorShowRimBehavior";

			// Token: 0x04002CEF RID: 11503
			public const string MAIN_TEXTURE = "_MainTex";

			// Token: 0x04002CF0 RID: 11504
			public const string MAIN_COLOR = "_Color";

			// Token: 0x04002CF1 RID: 11505
			public const string MAIN_TINT = "_MainTint";

			// Token: 0x04002CF2 RID: 11506
			public const string BUMP_MAP = "_BumpMap";

			// Token: 0x04002CF3 RID: 11507
			public const string DISTORTION = "_Distortion";

			// Token: 0x04002CF4 RID: 11508
			public const string RIM_COLOR = "_RimColor";

			// Token: 0x04002CF5 RID: 11509
			public const string RIM_SIZE = "_RimSize";

			// Token: 0x04002CF6 RID: 11510
			public const string RIM_INTENSITY = "_RimIntensity";

			// Token: 0x04002CF7 RID: 11511
			public const string SPECULAR_SHININESS = "_Shininess";

			// Token: 0x04002CF8 RID: 11512
			public const string SPEC_COLOR = "_SpecColor";

			// Token: 0x04002CF9 RID: 11513
			public const string SPECULAR_INTENSITY = "_SpecularIntensity";

			// Token: 0x04002CFA RID: 11514
			public const string EMISSION_COLOR = "_EmissionColor";

			// Token: 0x04002CFB RID: 11515
			public const string EMISSION = "_Emission";
		}
	}
}
