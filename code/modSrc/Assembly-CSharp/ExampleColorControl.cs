using System;
using UnityEngine;
using UnityEngine.UI;


public class ExampleColorControl : MonoBehaviour
{
	
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

	
	private void Update()
	{
		this.SetColor(new Color(this.r, this.g, this.b, this.a));
	}

	
	private float GetSinValue(float speed)
	{
		return 0.5f + 0.5f * Mathf.Sin(Time.time * speed);
	}

	
	public void SetHue(float value)
	{
		this.r = value;
	}

	
	public void SetSaturation(float value)
	{
		this.g = value;
	}

	
	public void SetValue(float value)
	{
		this.b = value;
	}

	
	public void SetAlpha(float value)
	{
		this.a = value;
	}

	
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

	
	private SpriteRenderer[] sprites;

	
	private Image[] images;

	
	public float animSpeedR = 3f;

	
	public float animSpeedG = 4f;

	
	public float animSpeedB = 5f;

	
	public float animSpeedA = 6f;

	
	public Sprite[] textures;

	
	public Sprite[] texturesRGB;

	
	public Sprite[] texturesALPHA;

	
	public Image texPreviewRGB;

	
	public Image texPreviewALPHA;

	
	public Image colorPreview;

	
	private int texIndex;

	
	private float r;

	
	private float g;

	
	private float b;

	
	private float a;
}
