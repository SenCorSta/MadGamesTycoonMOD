using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	
	public class All1ShaderDemoController : MonoBehaviour
	{
		
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

		
		private void SetExpositorText()
		{
			this.expositorsTitle.text = this.expositors[this.currExpositor].name;
			this.expositorsTitleOutline.text = this.expositors[this.currExpositor].name;
		}

		
		public int GetCurrExpositor()
		{
			return this.currExpositor;
		}

		
		[SerializeField]
		private DemoCircleExpositor[] expositors;

		
		[SerializeField]
		private Text expositorsTitle;

		
		[SerializeField]
		private Text expositorsTitleOutline;

		
		public float expositorDistance;

		
		private int currExpositor;

		
		[SerializeField]
		private GameObject background;

		
		private Material backgroundMat;

		
		[SerializeField]
		private float colorLerpSpeed;

		
		private Color[] targetColors;

		
		private Color[] currentColors;
	}
}
