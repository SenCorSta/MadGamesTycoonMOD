using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003F6 RID: 1014
	public class All1ShaderDemoController : MonoBehaviour
	{
		// Token: 0x060023CA RID: 9162 RVA: 0x001711AC File Offset: 0x0016F3AC
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

		// Token: 0x060023CB RID: 9163 RVA: 0x001712AC File Offset: 0x0016F4AC
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

		// Token: 0x060023CC RID: 9164 RVA: 0x00171444 File Offset: 0x0016F644
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

		// Token: 0x060023CD RID: 9165 RVA: 0x001714DC File Offset: 0x0016F6DC
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

		// Token: 0x060023CE RID: 9166 RVA: 0x00018602 File Offset: 0x00016802
		private void SetExpositorText()
		{
			this.expositorsTitle.text = this.expositors[this.currExpositor].name;
			this.expositorsTitleOutline.text = this.expositors[this.currExpositor].name;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0001863E File Offset: 0x0001683E
		public int GetCurrExpositor()
		{
			return this.currExpositor;
		}

		// Token: 0x04002E1E RID: 11806
		[SerializeField]
		private DemoCircleExpositor[] expositors;

		// Token: 0x04002E1F RID: 11807
		[SerializeField]
		private Text expositorsTitle;

		// Token: 0x04002E20 RID: 11808
		[SerializeField]
		private Text expositorsTitleOutline;

		// Token: 0x04002E21 RID: 11809
		public float expositorDistance;

		// Token: 0x04002E22 RID: 11810
		private int currExpositor;

		// Token: 0x04002E23 RID: 11811
		[SerializeField]
		private GameObject background;

		// Token: 0x04002E24 RID: 11812
		private Material backgroundMat;

		// Token: 0x04002E25 RID: 11813
		[SerializeField]
		private float colorLerpSpeed;

		// Token: 0x04002E26 RID: 11814
		private Color[] targetColors;

		// Token: 0x04002E27 RID: 11815
		private Color[] currentColors;
	}
}
