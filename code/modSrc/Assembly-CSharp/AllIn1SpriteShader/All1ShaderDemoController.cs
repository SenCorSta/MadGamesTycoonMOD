using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003F9 RID: 1017
	public class All1ShaderDemoController : MonoBehaviour
	{
		// Token: 0x0600241D RID: 9245 RVA: 0x00174090 File Offset: 0x00172290
		private void Start()
		{
			this.currExpositor = 0;
			this.SetExpositorText();
			for (int i = 0; i < this.expositors.Length; i++)
			{
				this.expositors[i].transform.position = new Vector3(0f, this.expositorDistance * (float)i, 0f);
			}
			this.backgroundMat = this.background.GetComponent<Image>().material;
			this.targetColors = new Color[4];
			this.targetColors[0] = this.backgroundMat.GetColor("_GradTopLeftCol");
			this.targetColors[1] = this.backgroundMat.GetColor("_GradTopRightCol");
			this.targetColors[2] = this.backgroundMat.GetColor("_GradBotLeftCol");
			this.targetColors[3] = this.backgroundMat.GetColor("_GradBotRightCol");
			this.currentColors = (this.targetColors.Clone() as Color[]);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00174190 File Offset: 0x00172390
		private void Update()
		{
			this.GetInput();
			this.currentColors[0] = Color.Lerp(this.currentColors[0], this.targetColors[this.currExpositor % this.targetColors.Length], this.colorLerpSpeed * Time.deltaTime);
			this.currentColors[1] = Color.Lerp(this.currentColors[1], this.targetColors[(1 + this.currExpositor) % this.targetColors.Length], this.colorLerpSpeed * Time.deltaTime);
			this.currentColors[2] = Color.Lerp(this.currentColors[2], this.targetColors[(2 + this.currExpositor) % this.targetColors.Length], this.colorLerpSpeed * Time.deltaTime);
			this.currentColors[3] = Color.Lerp(this.currentColors[3], this.targetColors[(3 + this.currExpositor) % this.targetColors.Length], this.colorLerpSpeed * Time.deltaTime);
			this.backgroundMat.SetColor("_GradTopLeftCol", this.currentColors[0]);
			this.backgroundMat.SetColor("_GradTopRightCol", this.currentColors[1]);
			this.backgroundMat.SetColor("_GradBotLeftCol", this.currentColors[2]);
			this.backgroundMat.SetColor("_GradBotRightCol", this.currentColors[3]);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00174328 File Offset: 0x00172528
		private void GetInput()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			{
				this.expositors[this.currExpositor].ChangeTarget(-1);
				return;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			{
				this.expositors[this.currExpositor].ChangeTarget(1);
				return;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				this.ChangeExpositor(-1);
				return;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				this.ChangeExpositor(1);
			}
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x001743C0 File Offset: 0x001725C0
		private void ChangeExpositor(int offset)
		{
			this.currExpositor += offset;
			if (this.currExpositor > this.expositors.Length - 1)
			{
				this.currExpositor = 0;
			}
			else if (this.currExpositor < 0)
			{
				this.currExpositor = this.expositors.Length - 1;
			}
			this.SetExpositorText();
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00174415 File Offset: 0x00172615
		private void SetExpositorText()
		{
			this.expositorsTitle.text = this.expositors[this.currExpositor].name;
			this.expositorsTitleOutline.text = this.expositors[this.currExpositor].name;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00174451 File Offset: 0x00172651
		public int GetCurrExpositor()
		{
			return this.currExpositor;
		}

		// Token: 0x04002E34 RID: 11828
		[SerializeField]
		private DemoCircleExpositor[] expositors;

		// Token: 0x04002E35 RID: 11829
		[SerializeField]
		private Text expositorsTitle;

		// Token: 0x04002E36 RID: 11830
		[SerializeField]
		private Text expositorsTitleOutline;

		// Token: 0x04002E37 RID: 11831
		public float expositorDistance;

		// Token: 0x04002E38 RID: 11832
		private int currExpositor;

		// Token: 0x04002E39 RID: 11833
		[SerializeField]
		private GameObject background;

		// Token: 0x04002E3A RID: 11834
		private Material backgroundMat;

		// Token: 0x04002E3B RID: 11835
		[SerializeField]
		private float colorLerpSpeed;

		// Token: 0x04002E3C RID: 11836
		private Color[] targetColors;

		// Token: 0x04002E3D RID: 11837
		private Color[] currentColors;
	}
}
