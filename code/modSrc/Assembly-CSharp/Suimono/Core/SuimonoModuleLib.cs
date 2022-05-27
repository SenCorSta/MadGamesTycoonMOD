using System;
using UnityEngine;

namespace Suimono.Core
{
	// Token: 0x0200039B RID: 923
	public class SuimonoModuleLib : MonoBehaviour
	{
		// Token: 0x04002A3F RID: 10815
		public Texture2D texNormalC;

		// Token: 0x04002A40 RID: 10816
		public Texture2D texNormalT;

		// Token: 0x04002A41 RID: 10817
		public Texture2D texNormalR;

		// Token: 0x04002A42 RID: 10818
		public Texture2D texFoam;

		// Token: 0x04002A43 RID: 10819
		public Texture2D texRampWave;

		// Token: 0x04002A44 RID: 10820
		public Texture2D texRampDepth;

		// Token: 0x04002A45 RID: 10821
		public Texture2D texRampBlur;

		// Token: 0x04002A46 RID: 10822
		public Texture2D texRampFoam;

		// Token: 0x04002A47 RID: 10823
		public Texture2D texWave;

		// Token: 0x04002A48 RID: 10824
		public Cubemap texCube1;

		// Token: 0x04002A49 RID: 10825
		public Texture2D texBlank;

		// Token: 0x04002A4A RID: 10826
		public Texture2D texMask;

		// Token: 0x04002A4B RID: 10827
		public Texture2D texDrops;

		// Token: 0x04002A4C RID: 10828
		[Space(10f)]
		public Material materialSurface;

		// Token: 0x04002A4D RID: 10829
		public Material materialSurfaceScale;

		// Token: 0x04002A4E RID: 10830
		public Material materialSurfaceShadow;

		// Token: 0x04002A4F RID: 10831
		[Space(10f)]
		public GameObject surfaceObject;

		// Token: 0x04002A50 RID: 10832
		public GameObject moduleObject;

		// Token: 0x04002A51 RID: 10833
		[Space(10f)]
		public fx_causticModule causticObject;

		// Token: 0x04002A52 RID: 10834
		public Light causticObjectLight;

		// Token: 0x04002A53 RID: 10835
		public cameraTools transToolsObject;

		// Token: 0x04002A54 RID: 10836
		public Camera transCamObject;

		// Token: 0x04002A55 RID: 10837
		public cameraCausticsHandler causticHandlerObjectTrans;

		// Token: 0x04002A56 RID: 10838
		public cameraTools causticToolsObject;

		// Token: 0x04002A57 RID: 10839
		public Camera causticCamObject;

		// Token: 0x04002A58 RID: 10840
		public cameraCausticsHandler causticHandlerObject;

		// Token: 0x04002A59 RID: 10841
		public GameObject wakeObject;

		// Token: 0x04002A5A RID: 10842
		public Camera wakeCamObject;

		// Token: 0x04002A5B RID: 10843
		public GameObject normalsObject;

		// Token: 0x04002A5C RID: 10844
		public Camera normalsCamObject;

		// Token: 0x04002A5D RID: 10845
		public SuimonoModuleFX fxObject;

		// Token: 0x04002A5E RID: 10846
		public fx_soundModule sndparentobj;

		// Token: 0x04002A5F RID: 10847
		public ParticleSystem underwaterDebris;

		// Token: 0x04002A60 RID: 10848
		public Transform underwaterDebrisTransform;

		// Token: 0x04002A61 RID: 10849
		public Renderer underwaterDebrisRendererComponent;

		// Token: 0x04002A62 RID: 10850
		[Space(10f)]
		public Mesh[] meshLevel;

		// Token: 0x04002A63 RID: 10851
		public Shader[] shaderRepository;

		// Token: 0x04002A64 RID: 10852
		public TextAsset[] presetRepository;
	}
}
