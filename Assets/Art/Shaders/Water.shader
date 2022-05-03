// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_TimeVoronoi("TimeVoronoi", Float) = 0
		_ScaleVoronoi("ScaleVoronoi", Float) = 5
		_SecondColor("SecondColor", Color) = (0,0.8865156,1,0)
		_Power("Power", Float) = 2
		_Vectormulty("Vectormulty", Range( 0 , 10)) = 0
		_Divide("Divide", Range( 0 , 10)) = 0
		_FirstColor("FirstColor", Color) = (0,0.3510606,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Vectormulty;
		uniform float _Divide;
		uniform float4 _FirstColor;
		uniform float4 _SecondColor;
		uniform float _ScaleVoronoi;
		uniform float _TimeVoronoi;
		uniform float _Power;


		float2 voronoihash13( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi13( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash13( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float temp_output_7_0 = ( cos( _Time.y ) / _Divide );
			float2 appendResult10 = (float2(temp_output_7_0 , temp_output_7_0));
			float2 uv_TexCoord16 = v.texcoord.xy * float2( 0.1,0.1 ) + appendResult10;
			v.vertex.xyz += ( ( ase_vertexNormal * float3(0,1,0) * _Vectormulty ) * uv_TexCoord16.y );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float time13 = ( _Time.y * _TimeVoronoi );
			float2 coords13 = i.uv_texcoord * _ScaleVoronoi;
			float2 id13 = 0;
			float2 uv13 = 0;
			float voroi13 = voronoi13( coords13, time13, id13, uv13, 0 );
			float4 lerpResult21 = lerp( _FirstColor , _SecondColor , pow( voroi13 , _Power ));
			o.Albedo = lerpResult21.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
493;73;1053;613;3239.324;1395.861;1.77395;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;1;-2301.188,774.5026;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;2;-2073.188,756.5026;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2079.188,974.5026;Inherit;False;Property;_Divide;Divide;5;0;Create;True;0;0;0;False;0;False;0;7.74;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3836.031,-300.2816;Inherit;False;Property;_TimeVoronoi;TimeVoronoi;0;0;Create;True;0;0;0;False;0;False;0;1.79;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-3870.299,-465.6873;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-3323.705,-607.5073;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-3431.749,-845.4828;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;7;-1795.188,765.5026;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-3255.573,-457.9965;Inherit;False;Property;_ScaleVoronoi;ScaleVoronoi;1;0;Create;True;0;0;0;False;0;False;5;6.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;12;-2024.592,250.124;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;10;-1647.188,744.5024;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;11;-2060.913,18.64276;Inherit;True;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;13;-2978.224,-719.3849;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;14;-2792.843,-485.0873;Inherit;False;Property;_Power;Power;3;0;Create;True;0;0;0;False;0;False;2;0.81;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1939.592,629.1239;Inherit;False;Property;_Vectormulty;Vectormulty;4;0;Create;True;0;0;0;False;0;False;0;0.13;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1540.203,289.0363;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1449.315,577.2886;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.1,0.1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;17;-2596.843,-796.0874;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2.27;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-2199.841,-699.4141;Inherit;False;Property;_SecondColor;SecondColor;2;0;Create;True;0;0;0;False;0;False;0,0.8865156,1,0;0,0.8862748,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;-2193.513,-971.9684;Inherit;False;Property;_FirstColor;FirstColor;6;0;Create;True;0;0;0;False;0;False;0,0.3510606,1,0;0,0.3510606,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1061.315,476.6678;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;21;-1750.752,-875.4557;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;9;0;5;0
WireConnection;9;1;4;0
WireConnection;7;0;2;0
WireConnection;7;1;3;0
WireConnection;10;0;7;0
WireConnection;10;1;7;0
WireConnection;13;0;6;0
WireConnection;13;1;9;0
WireConnection;13;2;8;0
WireConnection;18;0;11;0
WireConnection;18;1;12;0
WireConnection;18;2;15;0
WireConnection;16;1;10;0
WireConnection;17;0;13;0
WireConnection;17;1;14;0
WireConnection;22;0;18;0
WireConnection;22;1;16;2
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;21;2;17;0
WireConnection;0;0;21;0
WireConnection;0;11;22;0
ASEEND*/
//CHKSM=94133024C68FF754C84C7098A1E5C9515CE1C2B7