// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( DEgrade1PPSRenderer ), PostProcessEvent.AfterStack, "DEgrade1", true )]
public sealed class DEgrade1PPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Origin" )]
	public FloatParameter _Origin1 = new FloatParameter { value = 0.42f };
	[Tooltip( "Spread" )]
	public FloatParameter _Spread1 = new FloatParameter { value = 0.8f };
	[Tooltip( "TopColor" )]
	public ColorParameter _TopColor1 = new ColorParameter { value = new Color(0.945098f,0.8313726f,0.8313726f,0f) };
	[Tooltip( "BottomColor" )]
	public ColorParameter _BottomColor1 = new ColorParameter { value = new Color(0.6980392f,0.9529412f,0.7764706f,0f) };
	[Tooltip( "Gradient Whit Texture" )]
	public FloatParameter _GradientWhitTexture1 = new FloatParameter { value = 0.1227949f };
}

public sealed class DEgrade1PPSRenderer : PostProcessEffectRenderer<DEgrade1PPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "DEgrade 1" ) );
		sheet.properties.SetFloat( "_Origin1", settings._Origin1 );
		sheet.properties.SetFloat( "_Spread1", settings._Spread1 );
		sheet.properties.SetColor( "_TopColor1", settings._TopColor1 );
		sheet.properties.SetColor( "_BottomColor1", settings._BottomColor1 );
		sheet.properties.SetFloat( "_GradientWhitTexture1", settings._GradientWhitTexture1 );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
