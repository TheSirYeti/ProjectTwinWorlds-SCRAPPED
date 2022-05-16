// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DEgrade 1"
{
	Properties
	{
		_Origin1("Origin", Float) = 0.42
		_Spread1("Spread", Float) = 0.8
		_TopColor1("TopColor", Color) = (0.945098,0.8313726,0.8313726,0)
		_BottomColor1("BottomColor", Color) = (0.6980392,0.9529412,0.7764706,0)
		_GradientWhitTexture1("Gradient Whit Texture", Range( 0 , 1)) = 0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Cull Off
		ZWrite Off
		ZTest Always
		
		Pass
		{
			CGPROGRAM

			

			#pragma vertex Vert
			#pragma fragment Frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			
		
			struct ASEAttributesDefault
			{
				float3 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
			};

			struct ASEVaryingsDefault
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoordStereo : TEXCOORD1;
			#if STEREO_INSTANCING_ENABLED
				uint stereoTargetEyeIndex : SV_RenderTargetArrayIndex;
			#endif
				float4 ase_texcoord2 : TEXCOORD2;
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float4 _BottomColor1;
			uniform float4 _TopColor1;
			uniform float _Origin1;
			uniform float _Spread1;
			uniform float _GradientWhitTexture1;


			
			float2 TransformTriangleVertexToUV (float2 vertex)
			{
				float2 uv = (vertex + 1.0) * 0.5;
				return uv;
			}

			ASEVaryingsDefault Vert( ASEAttributesDefault v  )
			{
				ASEVaryingsDefault o;
				o.vertex = float4(v.vertex.xy, 0.0, 1.0);
				o.texcoord = TransformTriangleVertexToUV (v.vertex.xy);
#if UNITY_UV_STARTS_AT_TOP
				o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
				o.texcoordStereo = TransformStereoScreenSpaceTex (o.texcoord, 1.0);

				v.texcoord = o.texcoordStereo;
				float4 ase_ppsScreenPosVertexNorm = float4(o.texcoordStereo,0,1);

				o.ase_texcoord2 = float4(v.vertex,1);

				return o;
			}

			float4 Frag (ASEVaryingsDefault i  ) : SV_Target
			{
				float4 ase_ppsScreenPosFragNorm = float4(i.texcoordStereo,0,1);

				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float temp_output_7_0 = ( ( i.ase_texcoord2.xyz.y - _Origin1 ) / _Spread1 );
				float4 lerpResult9 = lerp( _BottomColor1 , _TopColor1 , temp_output_7_0);
				float4 lerpResult15 = lerp( tex2D( _MainTex, uv_MainTex ) , lerpResult9 , _GradientWhitTexture1);
				

				float4 color = lerpResult15;
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18912
2693;81;482;640;686.5757;360.3385;1;False;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-1751.152,349.2831;Inherit;False;Property;_Origin1;Origin;0;0;Create;True;0;0;0;False;0;False;0.42;-0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-1797.905,1.411758;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-1541.007,70.22887;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1395.479,366.1699;Inherit;False;Property;_Spread1;Spread;1;0;Create;True;0;0;0;False;0;False;0.8;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-1121.713,-317.2188;Inherit;False;Property;_BottomColor1;BottomColor;5;0;Create;True;0;0;0;False;0;False;0.6980392,0.9529412,0.7764706,0;0,0.04493475,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-1138.457,-107.4762;Inherit;False;Property;_TopColor1;TopColor;4;0;Create;True;0;0;0;False;0;False;0.945098,0.8313726,0.8313726,0;0.08741736,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;7;-1162.007,133.2287;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;8;-621.5601,-503.4672;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;9;-547.8851,55.34505;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-503.6641,-82.43365;Inherit;False;Property;_GradientWhitTexture1;Gradient Whit Texture;6;0;Create;True;0;0;0;False;0;False;0.01;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-502.3911,-353.5808;Inherit;True;Property;_TextureSample1;Texture Sample 0;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-1145.007,390.2289;Inherit;False;Property;_ClampMax1;ClampMax;3;0;Create;True;0;0;0;False;0;False;0;1.71;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1150.007,259.2289;Inherit;False;Property;_ClampMin1;ClampMin;2;0;Create;True;0;0;0;False;0;False;0;-0.96;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;14;-907.4161,223.0632;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;15;-98.76678,-118.2203;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;309.605,-78.73593;Float;False;True;-1;2;ASEMaterialInspector;0;2;DEgrade 1;32139be9c1eb75640a847f011acf3bcf;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;False;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;3;0;2;2
WireConnection;3;1;1;0
WireConnection;7;0;3;0
WireConnection;7;1;4;0
WireConnection;9;0;5;0
WireConnection;9;1;6;0
WireConnection;9;2;7;0
WireConnection;11;0;8;0
WireConnection;14;0;7;0
WireConnection;14;1;13;0
WireConnection;14;2;12;0
WireConnection;15;0;11;0
WireConnection;15;1;9;0
WireConnection;15;2;10;0
WireConnection;0;0;15;0
ASEEND*/
//CHKSM=331856004130B7203BF300B520080F3CD015AA7B