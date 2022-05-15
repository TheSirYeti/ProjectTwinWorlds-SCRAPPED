// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PortalShader"
{
	Properties
	{
		_PortalTexture("Portal Texture", 2D) = "white" {}
		_MainColor("Main Color", Color) = (1,0,0,0)
		_PortalAmplifiaction("Portal Amplifiaction", Float) = 7.49
		_TimeValue("Time Value", Float) = 0
		_SecondaryColor("Secondary Color", Color) = (0.06167676,0.361398,0.3962264,0)
		_BorderColor("Border Color", Color) = (0.4231132,0.1831613,0.7924528,0)
		_ReColor("Re-Color", Color) = (0,0.78371,1,0)
		_Intensity("Intensity", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _PortalTexture;
		uniform float _PortalAmplifiaction;
		uniform float _TimeValue;
		uniform float _Intensity;
		uniform float4 _MainColor;
		uniform float4 _SecondaryColor;
		uniform float4 _ReColor;
		uniform float4 _BorderColor;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_2 = (5.0).xxxx;
			return temp_cast_2;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 center45_g2 = float2( 0.5,0.5 );
			float2 delta6_g2 = ( v.texcoord.xy - center45_g2 );
			float angle10_g2 = ( length( delta6_g2 ) * _PortalAmplifiaction );
			float x23_g2 = ( ( cos( angle10_g2 ) * delta6_g2.x ) - ( sin( angle10_g2 ) * delta6_g2.y ) );
			float2 break40_g2 = center45_g2;
			float2 break41_g2 = float2( 1,1 );
			float y35_g2 = ( ( sin( angle10_g2 ) * delta6_g2.x ) + ( cos( angle10_g2 ) * delta6_g2.y ) );
			float2 appendResult44_g2 = (float2(( x23_g2 + break40_g2.x + break41_g2.x ) , ( break40_g2.y + break41_g2.y + y35_g2 )));
			float mulTime16 = _Time.y * _TimeValue;
			float cos14 = cos( mulTime16 );
			float sin14 = sin( mulTime16 );
			float2 rotator14 = mul( appendResult44_g2 - float2( 1,1 ) , float2x2( cos14 , -sin14 , sin14 , cos14 )) + float2( 1,1 );
			float4 PortalTexture25 = tex2Dlod( _PortalTexture, float4( rotator14, 0, 0.0) );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			v.vertex.xyz += ( PortalTexture25 * float4( ( ase_vertexNormal * ase_worldPos ) , 0.0 ) * _Intensity ).rgb;
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 center45_g2 = float2( 0.5,0.5 );
			float2 delta6_g2 = ( i.uv_texcoord - center45_g2 );
			float angle10_g2 = ( length( delta6_g2 ) * _PortalAmplifiaction );
			float x23_g2 = ( ( cos( angle10_g2 ) * delta6_g2.x ) - ( sin( angle10_g2 ) * delta6_g2.y ) );
			float2 break40_g2 = center45_g2;
			float2 break41_g2 = float2( 1,1 );
			float y35_g2 = ( ( sin( angle10_g2 ) * delta6_g2.x ) + ( cos( angle10_g2 ) * delta6_g2.y ) );
			float2 appendResult44_g2 = (float2(( x23_g2 + break40_g2.x + break41_g2.x ) , ( break40_g2.y + break41_g2.y + y35_g2 )));
			float mulTime16 = _Time.y * _TimeValue;
			float cos14 = cos( mulTime16 );
			float sin14 = sin( mulTime16 );
			float2 rotator14 = mul( appendResult44_g2 - float2( 1,1 ) , float2x2( cos14 , -sin14 , sin14 , cos14 )) + float2( 1,1 );
			float4 PortalTexture25 = tex2D( _PortalTexture, rotator14 );
			float4 temp_output_19_0 = ( 1.0 - PortalTexture25 );
			o.Emission = ( ( _MainColor * PortalTexture25 ) + ( _SecondaryColor * temp_output_19_0 ) + ( _ReColor * step( temp_output_19_0 , _BorderColor ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
623;73;902;610;503.5986;1102.257;5.046999;True;False
Node;AmplifyShaderEditor.CommentaryNode;18;-1215.572,172.2742;Inherit;False;1555.953;1075.464;UV Modification;9;3;7;6;5;4;15;17;16;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1159.615,222.2742;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;7;-1165.572,499.4113;Inherit;False;Constant;_TwirlCenter;TwirlCenter;0;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;6;-985.2845,577.653;Inherit;False;Property;_PortalAmplifiaction;Portal Amplifiaction;2;0;Create;True;0;0;0;False;0;False;7.49;16.83;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-947.6739,818.1487;Inherit;False;Constant;_TwirlOffset;TwirlOffset;0;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;17;-431.1977,988.7382;Inherit;False;Property;_TimeValue;Time Value;3;0;Create;True;0;0;0;False;0;False;0;1.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;15;-393.3791,687.0918;Inherit;False;Constant;_RotatorAnchor;Rotator Anchor;1;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;16;-153.9619,895.9409;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;4;-713.1874,451.9803;Inherit;False;Twirl;-1;;2;90936742ac32db8449cd21ab6dd337c8;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;14;64.38129,730.0346;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;542.9795,759.7559;Inherit;True;Property;_PortalTexture;Portal Texture;0;0;Create;True;0;0;0;False;0;False;-1;00ae145fe0d40a244a759bd505b67971;db3db23d2436fda43b1138a55cd04670;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;34;790.89,-583.9592;Inherit;False;1417.777;1147.045;Color;12;10;12;27;21;20;26;19;31;29;33;32;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;25;906.8964,708.6064;Inherit;False;PortalTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;851.8409,23.10451;Inherit;False;25;PortalTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;31;1001.544,313.2056;Inherit;False;Property;_BorderColor;Border Color;5;0;Create;True;0;0;0;False;0;False;0.4231132,0.1831613,0.7924528,0;0.4231132,0.1831613,0.7924528,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;19;1085.354,4.173993;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;38;1964.865,1078.139;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;27;855.1295,-307.6783;Inherit;False;25;PortalTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;29;1261.686,309.0854;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;10;840.89,-533.9592;Inherit;False;Property;_MainColor;Main Color;1;0;Create;True;0;0;0;False;0;False;1,0,0,0;0.4532603,0.1460929,0.6320754,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;21;1115.438,-182.757;Inherit;False;Property;_SecondaryColor;Secondary Color;4;0;Create;True;0;0;0;False;0;False;0.06167676,0.361398,0.3962264,0;0.2025144,0.1441794,0.2264151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;39;1973.809,877.5262;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;33;1430.787,116.0367;Inherit;False;Property;_ReColor;Re-Color;6;0;Create;True;0;0;0;False;0;False;0,0.78371,1,0;0.2295979,0.1776878,0.3113208,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;41;2341.687,1108.938;Inherit;False;Property;_Intensity;Intensity;7;0;Create;True;0;0;0;False;0;False;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;1661.489,278.3187;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;1113.992,-425.7173;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;1433.835,-152.8642;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;2185.227,681.8474;Inherit;False;25;PortalTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;2263.879,906.2018;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;40;2572.711,431.5461;Inherit;False;Constant;_Tesselation;Tesselation;8;0;Create;True;0;0;0;False;0;False;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;1972.667,45.22507;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;2593.593,760.2132;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;2855.586,170.1442;Float;False;True;-1;6;ASEMaterialInspector;0;0;Unlit;PortalShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;17;0
WireConnection;4;1;3;0
WireConnection;4;2;7;0
WireConnection;4;3;6;0
WireConnection;4;4;5;0
WireConnection;14;0;4;0
WireConnection;14;1;15;0
WireConnection;14;2;16;0
WireConnection;11;1;14;0
WireConnection;25;0;11;0
WireConnection;19;0;26;0
WireConnection;29;0;19;0
WireConnection;29;1;31;0
WireConnection;32;0;33;0
WireConnection;32;1;29;0
WireConnection;12;0;10;0
WireConnection;12;1;27;0
WireConnection;20;0;21;0
WireConnection;20;1;19;0
WireConnection;42;0;39;0
WireConnection;42;1;38;0
WireConnection;22;0;12;0
WireConnection;22;1;20;0
WireConnection;22;2;32;0
WireConnection;36;0;35;0
WireConnection;36;1;42;0
WireConnection;36;2;41;0
WireConnection;2;2;22;0
WireConnection;2;11;36;0
WireConnection;2;14;40;0
ASEEND*/
//CHKSM=16CC0DD4372E13376E535885EB4D062A73BE2020