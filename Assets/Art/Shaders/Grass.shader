// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Grass"
{
	Properties
	{
		_GrassTexture1("Grass Texture", 2D) = "white" {}
		_MyPos1("MyPos", Vector) = (0,0,0,0)
		_Radius1("Radius", Float) = 0
		_SubstractRadius1("SubstractRadius", Float) = 0
		_SubstracPJ1("SubstracPJ", Float) = 0
		_Mascara1("Mascara", Float) = 1.08
		_Seepdgras1("Seepdgras", Float) = 2
		_Vectoraltura1("Vector altura", Vector) = (0.5,0,0.5,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _Mascara1;
		uniform float _Seepdgras1;
		uniform float3 _Vectoraltura1;
		uniform float3 _MyPos1;
		uniform float _SubstracPJ1;
		uniform float _Radius1;
		uniform float _SubstractRadius1;
		uniform sampler2D _GrassTexture1;
		uniform float4 _GrassTexture1_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float mulTime8 = _Time.y * _Seepdgras1;
			float3 temp_cast_0 = (_SubstracPJ1).xxx;
			float temp_output_19_0 = ( saturate( ( distance( ase_worldPos , _MyPos1 ) / _Radius1 ) ) - _SubstractRadius1 );
			float3 appendResult23 = (float3(temp_output_19_0 , 0.0 , temp_output_19_0));
			float3 lerpResult27 = lerp( ( ( ase_worldPos.y * _Mascara1 ) * ( sin( mulTime8 ) + v.texcoord.xy.x ) * _Vectoraltura1 ) , ( ( 1.0 - _MyPos1 ) - temp_cast_0 ) , appendResult23);
			v.vertex.xyz += lerpResult27;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_GrassTexture1 = i.uv_texcoord * _GrassTexture1_ST.xy + _GrassTexture1_ST.zw;
			float4 tex2DNode28 = tex2D( _GrassTexture1, uv_GrassTexture1 );
			o.Albedo = tex2DNode28.rgb;
			o.Alpha = tex2DNode28.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
123;62;1920;886;4064.651;1480.434;3.121061;True;False
Node;AmplifyShaderEditor.Vector3Node;2;-2256.924,-681.6682;Inherit;False;Property;_MyPos1;MyPos;1;0;Create;True;0;0;0;False;0;False;0,0,0;0.64,-0.2658792,0.09;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-2404.921,-243.5234;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;6;-2038.847,-492.1187;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1829.959,519.8955;Inherit;False;Property;_Seepdgras1;Seepdgras;6;0;Create;True;0;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1858.295,-180.5945;Inherit;False;Property;_Radius1;Radius;2;0;Create;True;0;0;0;False;0;False;0;3.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;7;-1866.22,692.1045;Inherit;False;461;200;Utilizando este nodo podemos generar una máscara;1;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-1661.116,560.2775;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;9;-1999.27,1051.712;Inherit;False;752.2188;245.6584;Podríamos utilizarlo para usar una misma dirección para todos los pastos. Es decir, viento;1;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;10;-1699.482,-450.3333;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1714.276,317.1475;Inherit;False;Property;_Mascara1;Mascara;5;0;Create;True;0;0;0;False;0;False;1.08;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;14;-1717.09,1118.37;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1732.896,751.0355;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-1269.311,-304.8676;Inherit;False;Property;_SubstractRadius1;SubstractRadius;3;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;11;-1469.098,557.3375;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;13;-1264.431,-464.6874;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1280.167,22.82642;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1657.894,-711.8283;Inherit;False;Property;_SubstracPJ1;SubstracPJ;4;0;Create;True;0;0;0;False;0;False;0;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1081.492,-482.1462;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;18;-1204.679,236.0305;Inherit;False;Property;_Vectoraltura1;Vector altura;7;0;Create;True;0;0;0;False;0;False;0.5,0,0.5;1,0,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;20;-2096.699,-972.0435;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1274.333,460.7695;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-976.3914,-647.326;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-865.1022,77.36047;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;25;-1450.104,-872.1078;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;26;-1645.277,-1456.689;Inherit;False;371;280;Albedo y (A) Opacity;1;28;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;28;-1595.277,-1406.689;Inherit;True;Property;_GrassTexture1;Grass Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;-846.3594,-822.7238;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-85.98614,-803.3561;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Grass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;3;0
WireConnection;6;1;2;0
WireConnection;8;0;4;0
WireConnection;10;0;6;0
WireConnection;10;1;5;0
WireConnection;11;0;8;0
WireConnection;13;0;10;0
WireConnection;22;0;14;2
WireConnection;22;1;16;0
WireConnection;19;0;13;0
WireConnection;19;1;12;0
WireConnection;20;0;2;0
WireConnection;17;0;11;0
WireConnection;17;1;15;1
WireConnection;23;0;19;0
WireConnection;23;2;19;0
WireConnection;24;0;22;0
WireConnection;24;1;17;0
WireConnection;24;2;18;0
WireConnection;25;0;20;0
WireConnection;25;1;21;0
WireConnection;27;0;24;0
WireConnection;27;1;25;0
WireConnection;27;2;23;0
WireConnection;0;0;28;0
WireConnection;0;9;28;4
WireConnection;0;11;27;0
ASEEND*/
//CHKSM=E3102BAA135004333F246B4756E8EAEEED7C65DC