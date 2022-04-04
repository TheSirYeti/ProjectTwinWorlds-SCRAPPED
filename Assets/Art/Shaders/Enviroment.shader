// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Enviroment"
{
	Properties
	{
		_Origin("Origin", Float) = 0
		_Spread("Spread", Float) = 0
		_ClampMin("ClampMin", Float) = 0
		_ClampMax("ClampMax", Float) = 0
		_TopColor("TopColor", Color) = (0.945098,0.8313726,0.8313726,0)
		_BottomColor("BottomColor", Color) = (0.6980392,0.9529412,0.7764706,0)
		_Concrete_Pavement_Tiled_Maps("Concrete_Pavement_Tiled_Maps", 2D) = "white" {}
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

		uniform sampler2D _Concrete_Pavement_Tiled_Maps;
		uniform float4 _BottomColor;
		uniform float4 _TopColor;
		uniform float _Origin;
		uniform float _Spread;
		uniform float _ClampMin;
		uniform float _ClampMax;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord13 = i.uv_texcoord * float2( 16,16 );
			o.Normal = tex2D( _Concrete_Pavement_Tiled_Maps, uv_TexCoord13 ).rgb;
			float3 ase_worldPos = i.worldPos;
			float clampResult10 = clamp( ( ( ase_worldPos.y - _Origin ) / _Spread ) , _ClampMin , _ClampMax );
			float4 lerpResult11 = lerp( _BottomColor , _TopColor , clampResult10);
			o.Albedo = lerpResult11.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
1933;-7;1920;944;1858.93;179.0266;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-2142.236,55.36393;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;2;-1951.144,303.5686;Inherit;False;Property;_Origin;Origin;0;0;Create;True;0;0;0;False;0;False;0;-3.46;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-1764.144,49.56863;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1600.144,284.5686;Inherit;False;Property;_Spread;Spread;1;0;Create;True;0;0;0;False;0;False;0;2.99;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;5;-1407.144,46.56863;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1395.144,172.5687;Inherit;False;Property;_ClampMin;ClampMin;2;0;Create;True;0;0;0;False;0;False;0;-0.96;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1390.144,303.5686;Inherit;False;Property;_ClampMax;ClampMax;3;0;Create;True;0;0;0;False;0;False;0;1.71;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-1017.424,-219.8151;Inherit;False;Property;_BottomColor;BottomColor;5;0;Create;True;0;0;0;False;0;False;0.6980392,0.9529412,0.7764706,0;0.6980392,0.9529412,0.7764706,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-1275.81,-204.6344;Inherit;False;Property;_TopColor;TopColor;4;0;Create;True;0;0;0;False;0;False;0.945098,0.8313726,0.8313726,0;0.945098,0.8313726,0.8313726,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;10;-1187.144,66.56863;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-976.101,373.6774;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;16,16;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;11;-682.0231,72.68491;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-669.3691,313.7622;Inherit;True;Property;_Concrete_Pavement_Tiled_Maps;Concrete_Pavement_Tiled_Maps;6;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Enviroment;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;2
WireConnection;3;1;2;0
WireConnection;5;0;3;0
WireConnection;5;1;4;0
WireConnection;10;0;5;0
WireConnection;10;1;6;0
WireConnection;10;2;7;0
WireConnection;11;0;8;0
WireConnection;11;1;9;0
WireConnection;11;2;10;0
WireConnection;12;1;13;0
WireConnection;0;0;11;0
WireConnection;0;1;12;0
ASEEND*/
//CHKSM=665EC067223ED5F74FA80465361D22772942B035