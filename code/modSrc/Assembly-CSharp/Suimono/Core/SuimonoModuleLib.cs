using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039E RID: 926
	public class SuimonoModuleLib : MonoBehaviour
	{
		// Token: 0x04002A55 RID: 10837
		public Texture2D texNormalC;

		// Token: 0x04002A56 RID: 10838
		public Texture2D texNormalT;

		// Token: 0x04002A57 RID: 10839
		public Texture2D texNormalR;

		// Token: 0x04002A58 RID: 10840
		public Texture2D texFoam;

		// Token: 0x04002A59 RID: 10841
		public Texture2D texRampWave;

		// Token: 0x04002A5A RID: 10842
		public Texture2D texRampDepth;

		// Token: 0x04002A5B RID: 10843
		public Texture2D texRampBlur;

		// Token: 0x04002A5C RID: 10844
		public Texture2D texRampFoam;

		// Token: 0x04002A5D RID: 10845
		public Texture2D texWave;

		// Token: 0x04002A5E RID: 10846
		public Cubemap texCube1;

		// Token: 0x04002A5F RID: 10847
		public Texture2D texBlank;

		// Token: 0x04002A60 RID: 10848
		public Texture2D texMask;

		// Token: 0x04002A61 RID: 10849
		public Texture2D texDrops;

		// Token: 0x04002A62 RID: 10850
		[Space(10f)]
		public Material materialSurface;

		// Token: 0x04002A63 RID: 10851
		public Material materialSurfaceScale;

		// Token: 0x04002A64 RID: 10852
		public Material materialSurfaceShadow;

		// Token: 0x04002A65 RID: 10853
		[Space(10f)]
		public GameObject surfaceObject;

		// Token: 0x04002A66 RID: 10854
		public GameObject moduleObject;

		// Token: 0x04002A67 RID: 10855
		[Space(10f)]
		public fx_causticModule causticObject;

		// Token: 0x04002A68 RID: 10856
		public Light causticObjectLight;

		// Token: 0x04002A69 RID: 10857
		public cameraTools transToolsObject;

		// Token: 0x04002A6A RID: 10858
		public Camera transCamObject;

		// Token: 0x04002A6B RID: 10859
		public cameraCausticsHandler causticHandlerObjectTrans;

		// Token: 0x04002A6C RID: 10860
		public cameraTools causticToolsObject;

		// Token: 0x04002A6D RID: 10861
		public Camera causticCamObject;

		// Token: 0x04002A6E RID: 10862
		public cameraCausticsHandler causticHandlerObject;

		// Token: 0x04002A6F RID: 10863
		public GameObject wakeObject;

		// Token: 0x04002A70 RID: 10864
		public Camera wakeCamObject;

		// Token: 0x04002A71 RID: 10865
		public GameObject normalsObject;

		// Token: 0x04002A72 RID: 10866
		public Camera normalsCamObject;

		// Token: 0x04002A73 RID: 10867
		public SuimonoModuleFX fxObject;

		// Token: 0x04002A74 RID: 10868
		public fx_soundModule sndparentobj;

		// Token: 0x04002A75 RID: 10869
		public ParticleSystem underwaterDebris;

		// Token: 0x04002A76 RID: 10870
		public Transform underwaterDebrisTransform;

		// Token: 0x04002A77 RID: 10871
		public Renderer underwaterDebrisRendererComponent;

		// Token: 0x04002A78 RID: 10872
		[Space(10f)]
		public Mesh[] meshLevel;

		// Token: 0x04002A79 RID: 10873
		public Shader[] shaderRepository;

		// Token: 0x04002A7A RID: 10874
		public TextAsset[] presetRepository;
	}
}
