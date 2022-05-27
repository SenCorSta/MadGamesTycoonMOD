using System;
using UnityEngine;

namespace MK.Glass
{
	// Token: 0x020003B1 RID: 945
	public static class MKGlassFreeMaterialHelper
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x0016EEAD File Offset: 0x0016D0AD
		public static void SetMainTint(Material material, float tint)
		{
			material.SetFloat("_MainTint", tint);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0016EEBB File Offset: 0x0016D0BB
		public static float GetMainTint(Material material)
		{
			return material.GetFloat("_MainTint");
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0016EEC8 File Offset: 0x0016D0C8
		public static void SetMainTexture(Material material, Texture tex)
		{
			material.SetTexture("_MainTex", tex);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0016EED6 File Offset: 0x0016D0D6
		public static Texture GetMainTexture(Material material)
		{
			return material.GetTexture("_MainTex");
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0016EEE3 File Offset: 0x0016D0E3
		public static void SetMainColor(Material material, Color color)
		{
			material.SetColor("_Color", color);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0016EEF1 File Offset: 0x0016D0F1
		public static Color GetMainColor(Material material)
		{
			return material.GetColor("_Color");
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0016EEFE File Offset: 0x0016D0FE
		public static void SetNormalmap(Material material, Texture tex)
		{
			material.SetTexture("_BumpMap", tex);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0016EF0C File Offset: 0x0016D10C
		public static Texture GetBumpMap(Material material)
		{
			return material.GetTexture("_BumpMap");
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0016EF19 File Offset: 0x0016D119
		public static void SetDistortion(Material material, float distortion)
		{
			material.SetFloat("_Distortion", distortion);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0016EF27 File Offset: 0x0016D127
		public static float GetDistortion(Material material)
		{
			return material.GetFloat("_Distortion");
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0016EF34 File Offset: 0x0016D134
		public static void SetRimColor(Material material, Color color)
		{
			material.SetColor("_RimColor", color);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0016EF42 File Offset: 0x0016D142
		public static Color GetRimColor(Material material)
		{
			return material.GetColor("_RimColor");
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0016EF4F File Offset: 0x0016D14F
		public static void SetRimSize(Material material, float size)
		{
			material.SetFloat("_RimSize", size);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0016EF5D File Offset: 0x0016D15D
		public static float GetRimSize(Material material)
		{
			return material.GetFloat("_RimSize");
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0016EF6A File Offset: 0x0016D16A
		public static void SetRimIntensity(Material material, float intensity)
		{
			material.SetFloat("_RimIntensity", intensity);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x0016EF78 File Offset: 0x0016D178
		public static float GetRimIntensity(Material material)
		{
			return material.GetFloat("_RimIntensity");
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0016EF85 File Offset: 0x0016D185
		public static void SetSpecularShininess(Material material, float shininess)
		{
			material.SetFloat("_Shininess", shininess);
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0016EF93 File Offset: 0x0016D193
		public static float GetSpecularShininess(Material material)
		{
			return material.GetFloat("_Shininess");
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0016EFA0 File Offset: 0x0016D1A0
		public static void SetSpecularColor(Material material, Color color)
		{
			material.SetColor("_SpecColor", color);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0016EFAE File Offset: 0x0016D1AE
		public static Color GetSpecularColor(Material material)
		{
			return material.GetColor("_SpecColor");
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0016EFBB File Offset: 0x0016D1BB
		public static void SetSpecularIntensity(Material material, float intensity)
		{
			material.SetFloat("_SpecularIntensity", intensity);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0016EFC9 File Offset: 0x0016D1C9
		public static float GetSpecularIntensity(Material material)
		{
			return material.GetFloat("_SpecularIntensity");
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0016EFD6 File Offset: 0x0016D1D6
		public static void SetEmissionColor(Material material, Color color)
		{
			material.SetColor("_EmissionColor", color);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0016EFE4 File Offset: 0x0016D1E4
		public static Color GetEmissionColor(Material material)
		{
			return material.GetColor("_EmissionColor");
		}

		// Token: 0x020003B2 RID: 946
		public static class PropertyNames
		{
			// Token: 0x04002D00 RID: 11520
			public const string SHOW_MAIN_BEHAVIOR = "_MKEditorShowMainBehavior";

			// Token: 0x04002D01 RID: 11521
			public const string SHOW_LIGHT_BEHAVIOR = "_MKEditorShowLightBehavior";

			// Token: 0x04002D02 RID: 11522
			public const string SHOW_RENDER_BEHAVIOR = "_MKEditorShowRenderBehavior";

			// Token: 0x04002D03 RID: 11523
			public const string SHOW_SPECULAR_BEHAVIOR = "_MKEditorShowSpecularBehavior";

			// Token: 0x04002D04 RID: 11524
			public const string SHOW_RIM_BEHAVIOR = "_MKEditorShowRimBehavior";

			// Token: 0x04002D05 RID: 11525
			public const string MAIN_TEXTURE = "_MainTex";

			// Token: 0x04002D06 RID: 11526
			public const string MAIN_COLOR = "_Color";

			// Token: 0x04002D07 RID: 11527
			public const string MAIN_TINT = "_MainTint";

			// Token: 0x04002D08 RID: 11528
			public const string BUMP_MAP = "_BumpMap";

			// Token: 0x04002D09 RID: 11529
			public const string DISTORTION = "_Distortion";

			// Token: 0x04002D0A RID: 11530
			public const string RIM_COLOR = "_RimColor";

			// Token: 0x04002D0B RID: 11531
			public const string RIM_SIZE = "_RimSize";

			// Token: 0x04002D0C RID: 11532
			public const string RIM_INTENSITY = "_RimIntensity";

			// Token: 0x04002D0D RID: 11533
			public const string SPECULAR_SHININESS = "_Shininess";

			// Token: 0x04002D0E RID: 11534
			public const string SPEC_COLOR = "_SpecColor";

			// Token: 0x04002D0F RID: 11535
			public const string SPECULAR_INTENSITY = "_SpecularIntensity";

			// Token: 0x04002D10 RID: 11536
			public const string EMISSION_COLOR = "_EmissionColor";

			// Token: 0x04002D11 RID: 11537
			public const string EMISSION = "_Emission";
		}
	}
}
