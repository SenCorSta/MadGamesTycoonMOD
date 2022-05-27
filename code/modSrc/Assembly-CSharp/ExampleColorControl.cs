using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000025 RID: 37
public class ExampleColorControl : MonoBehaviour
{
	// Token: 0x060000A1 RID: 161 RVA: 0x0001BA98 File Offset: 0x00019C98
	private void Start()
	{
		this.sprites = base.GetComponentsInChildren<SpriteRenderer>();
		this.images = base.GetComponentsInChildren<Image>();
		this.r = 0.5f;
		this.g = 0.5f;
		this.b = 0.5f;
		this.a = 1f;
		this.texIndex = 0;
		this.SetTexture();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00002750 File Offset: 0x00000950
	private void Update()
	{
		this.SetColor(new Color(this.r, this.g, this.b, this.a));
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00002775 File Offset: 0x00000975
	private float GetSinValue(float speed)
	{
		return 0.5f + 0.5f * Mathf.Sin(Time.time * speed);
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000278F File Offset: 0x0000098F
	public void SetHue(float value)
	{
		this.r = value;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00002798 File Offset: 0x00000998
	public void SetSaturation(float value)
	{
		this.g = value;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000027A1 File Offset: 0x000009A1
	public void SetValue(float value)
	{
		this.b = value;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000027AA File Offset: 0x000009AA
	public void SetAlpha(float value)
	{
		this.a = value;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0001BAF8 File Offset: 0x00019CF8
	public void ChangeTexture(int change)
	{
		this.texIndex += change;
		if (this.texIndex >= this.textures.Length)
		{
			this.texIndex = 0;
		}
		else if (this.texIndex < 0)
		{
			this.texIndex = this.textures.Length - 1;
		}
		this.SetTexture();
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0001BB4C File Offset: 0x00019D4C
	private void SetColor(Color color)
	{
		for (int i = 0; i < this.sprites.Length; i++)
		{
			this.sprites[i].color = color;
		}
		for (int j = 0; j < this.images.Length; j++)
		{
			this.images[j].color = color;
		}
		this.colorPreview.color = color;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0001BBA8 File Offset: 0x00019DA8
	private void SetTexture()
	{
		Sprite sprite = this.textures[this.texIndex];
		for (int i = 0; i < this.sprites.Length; i++)
		{
			this.sprites[i].sprite = sprite;
		}
		for (int j = 0; j < this.images.Length; j++)
		{
			this.images[j].sprite = sprite;
		}
		this.texPreviewRGB.sprite = this.texturesRGB[this.texIndex];
		this.texPreviewALPHA.sprite = this.texturesALPHA[this.texIndex];
	}

	// Token: 0x040000C3 RID: 195
	private SpriteRenderer[] sprites;

	// Token: 0x040000C4 RID: 196
	private Image[] images;

	// Token: 0x040000C5 RID: 197
	public float animSpeedR = 3f;

	// Token: 0x040000C6 RID: 198
	public float animSpeedG = 4f;

	// Token: 0x040000C7 RID: 199
	public float animSpeedB = 5f;

	// Token: 0x040000C8 RID: 200
	public float animSpeedA = 6f;

	// Token: 0x040000C9 RID: 201
	public Sprite[] textures;

	// Token: 0x040000CA RID: 202
	public Sprite[] texturesRGB;

	// Token: 0x040000CB RID: 203
	public Sprite[] texturesALPHA;

	// Token: 0x040000CC RID: 204
	public Image texPreviewRGB;

	// Token: 0x040000CD RID: 205
	public Image texPreviewALPHA;

	// Token: 0x040000CE RID: 206
	public Image colorPreview;

	// Token: 0x040000CF RID: 207
	private int texIndex;

	// Token: 0x040000D0 RID: 208
	private float r;

	// Token: 0x040000D1 RID: 209
	private float g;

	// Token: 0x040000D2 RID: 210
	private float b;

	// Token: 0x040000D3 RID: 211
	private float a;
}
