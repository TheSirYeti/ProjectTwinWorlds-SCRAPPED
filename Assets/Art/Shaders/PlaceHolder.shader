// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PlaceHolder"
{
	Properties
	{
		_Origin1("Origin", Float) = 0
		_Spread1("Spread", Float) = 0
		_ClampMin1("ClampMin", Float) = 0
		_ClampMax1("ClampMax", Float) = 0
		_TopColor1("TopColor", Color) = (0.945098,0.8313726,0.8313726,0)
		_BottomColor1("BottomColor", Color) = (0.6980392,0.9529412,0.7764706,0)
		_Normal1("Normal", 2D) = "white" {}
		_GradientWhitTexture1("Gradient Whit Texture", Range( 0 , 1)) = 0
		_NormalLerp1("NormalLerp", Range( 0 , 1)) = 1
		_NormalNone1("NormalNone", 2D) = "white" {}
		_Metallic1("Metallic", Range( 0 , 1)) = 0
		_Smoothness1("Smoothness", Range( 0 , 1)) = 0
		_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _NormalNone1;
		uniform float4 _NormalNone1_ST;
		uniform sampler2D _Normal1;
		uniform float _NormalLerp1;
		uniform float4 _Color0;
		uniform float4 _BottomColor1;
		uniform float4 _TopColor1;
		uniform float _Origin1;
		uniform float _Spread1;
		uniform float _ClampMin1;
		uniform float _ClampMax1;
		uniform float _GradientWhitTexture1;
		uniform float _Metallic1;
		uniform float _Smoothness1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalNone1 = i.uv_texcoord * _NormalNone1_ST.xy + _NormalNone1_ST.zw;
			float2 uv_TexCoord8 = i.uv_texcoord * float2( 5,5 );
			float4 lerpResult21 = lerp( tex2D( _NormalNone1, uv_NormalNone1 ) , tex2D( _Normal1, uv_TexCoord8 ) , _NormalLerp1);
			o.Normal = lerpResult21.rgb;
			float3 ase_worldPos = i.worldPos;
			float clampResult9 = clamp( ( ( ase_worldPos.y - _Origin1 ) / _Spread1 ) , _ClampMin1 , _ClampMax1 );
			float4 lerpResult13 = lerp( _BottomColor1 , _TopColor1 , clampResult9);
			float4 lerpResult20 = lerp( _Color0 , lerpResult13 , _GradientWhitTexture1);
			o.Albedo = lerpResult20.rgb;
			o.Metallic = _Metallic1;
			o.Smoothness = _Smoothness1;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
1913;205;1920;814;1898.629;476.3307;1.457805;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-2375.161,51.70213;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;2;-2184.068,299.9068;Inherit;False;Property;_Origin1;Origin;0;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-1997.068,45.90683;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1833.068,280.9068;Inherit;False;Property;_Spread1;Spread;1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;5;-1640.068,42.90683;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1628.068,168.9069;Inherit;False;Property;_ClampMin1;ClampMin;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1623.068,299.9068;Inherit;False;Property;_ClampMax1;ClampMax;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;9;-1420.068,62.90683;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1370.025,366.0156;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-1230.348,-345.4769;Inherit;False;Property;_BottomColor1;BottomColor;5;0;Create;True;0;0;0;False;0;False;0.6980392,0.9529412,0.7764706,0;0.6980392,0.9529412,0.7764706,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1508.734,-208.2962;Inherit;False;Property;_TopColor1;TopColor;4;0;Create;True;0;0;0;False;0;False;0.945098,0.8313726,0.8313726,0;0.945098,0.8313726,0.8313726,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-779.1685,143.7059;Inherit;True;Property;_NormalNone1;NormalNone;9;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-724.8295,36.45771;Inherit;False;Property;_GradientWhitTexture1;Gradient Whit Texture;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;22;-878.8098,-522.6852;Inherit;False;Property;_Color0;Color 0;12;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.6886792,0.6886792,0.6886792,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;13;-1025.948,-34.97689;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;14;-1041.293,247.1004;Inherit;True;Property;_Normal1;Normal;6;0;Create;True;0;0;0;False;0;False;-1;None;b3ebe75a494ce02419be1926e012bbe7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-772.8295,468.4577;Inherit;False;Property;_NormalLerp1;NormalLerp;8;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-389.4404,117.1076;Inherit;False;Property;_Smoothness1;Smoothness;11;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-401.7405,49.58761;Inherit;False;Property;_Metallic1;Metallic;10;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;20;-576.8295,-208.5423;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;21;-479.8294,222.4577;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;PlaceHolder;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;2
WireConnection;3;1;2;0
WireConnection;5;0;3;0
WireConnection;5;1;4;0
WireConnection;9;0;5;0
WireConnection;9;1;6;0
WireConnection;9;2;7;0
WireConnection;13;0;11;0
WireConnection;13;1;10;0
WireConnection;13;2;9;0
WireConnection;14;1;8;0
WireConnection;20;0;22;0
WireConnection;20;1;13;0
WireConnection;20;2;17;0
WireConnection;21;0;15;0
WireConnection;21;1;14;0
WireConnection;21;2;16;0
WireConnection;0;0;20;0
WireConnection;0;1;21;0
WireConnection;0;3;18;0
WireConnection;0;4;19;0
ASEEND*/
//CHKSM=0D527103BDE78DFB12AD1F629B752A96E855D165