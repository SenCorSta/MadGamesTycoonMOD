﻿using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	[ExecuteInEditMode]
	public class SetGlobalTime : MonoBehaviour
	{
		
		private void Start()
		{
			this.globalTime = Shader.PropertyToID("globalUnscaledTime");
		}

		
		private void Update()
		{
			Shader.SetGlobalFloat(this.globalTime, Time.time / 20f);
		}

		
		private int globalTime;
	}
}
