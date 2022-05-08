// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewFountain"
{
	Properties
	{
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 2
		_Amplitude("Amplitude", Float) = 0.12
		_Color0("Color 0", Color) = (0.2666667,1,0,0)
		_Color1("Color 1", Color) = (0.2666667,1,0,0)
		_VoronoiScale("VoronoiScale", Float) = 1
		_VoronoiAngle("VoronoiAngle", Float) = 0
		_RingWidth("RingWidth", Range( 0 , 2)) = 0
		_Smoothness("Smoothness", Range( -10 , 2)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float2 _Vector0;
		uniform float _RingWidth;
		uniform float _Amplitude;
		uniform float _VoronoiScale;
		uniform float _VoronoiAngle;
		uniform float4 _Color1;
		uniform float4 _Color0;
		uniform float _Smoothness;
		uniform float _EdgeLength;


		float2 voronoihash680( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi680( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
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
			 		float2 o = voronoihash680( n + g );
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


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float Direccion111 = ( distance( _Vector0 , v.texcoord.xy ) / _RingWidth );
			float temp_output_83_0 = sqrt( ( ( 6.28318548202515 / 1.0 ) * 1.0 ) );
			float mulTime137 = _Time.y * 2.02;
			float temp_output_175_0 = ( ( ( 1.0 - ( Direccion111 * temp_output_83_0 ) ) + mulTime137 ) * 4.1 );
			float temp_output_32_0 = ( sin( temp_output_175_0 ) * 0.79 );
			float temp_output_652_0 = (0.03529412 + (temp_output_32_0 - 1.139501E-09) * (0.09685075 - 0.03529412) / (0.1882353 - 1.139501E-09));
			float Amplitude139 = _Amplitude;
			float4 appendResult153 = (float4(0.0 , 0.0 , saturate( ( temp_output_652_0 * Amplitude139 ) ) , 0.0));
			float4 VertexPosition636 = saturate( appendResult153 );
			v.vertex.xyz += VertexPosition636.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float time680 = _VoronoiAngle;
			float mulTime687 = _Time.y * 0.3;
			float2 panner695 = ( mulTime687 * float2( 0,0.1 ) + i.uv_texcoord);
			float2 coords680 = panner695 * _VoronoiScale;
			float2 id680 = 0;
			float2 uv680 = 0;
			float voroi680 = voronoi680( coords680, time680, id680, uv680, 0 );
			float Direccion111 = ( distance( _Vector0 , i.uv_texcoord ) / _RingWidth );
			float temp_output_83_0 = sqrt( ( ( 6.28318548202515 / 1.0 ) * 1.0 ) );
			float mulTime137 = _Time.y * 2.02;
			float temp_output_175_0 = ( ( ( 1.0 - ( Direccion111 * temp_output_83_0 ) ) + mulTime137 ) * 4.1 );
			float temp_output_32_0 = ( sin( temp_output_175_0 ) * 0.79 );
			float temp_output_652_0 = (0.03529412 + (temp_output_32_0 - 1.139501E-09) * (0.09685075 - 0.03529412) / (0.1882353 - 1.139501E-09));
			o.Albedo = ( ( ( pow( voroi680 , 0.53 ) * 0.2 ) + _Color1 ) + ( _Color0 * temp_output_652_0 ) ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
597;73;949;657;127.0036;1888.099;5.370164;False;False
Node;AmplifyShaderEditor.CommentaryNode;671;-3060.694,358.7949;Inherit;False;1707.246;519.4507;Comment;10;78;132;131;136;137;178;176;135;175;140;Calculo de la ola;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;18;-1391.606,-1079.636;Inherit;False;886.677;758.8679;;7;26;24;23;22;21;20;25;Efecto de radio;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;26;-1333.773,-1029.636;Float;False;Property;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;0,0;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-1344.697,-887.175;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;78;-3010.694,578.9843;Inherit;False;714.442;304.2192;Wavelenght (w) =  sqrt((2*pi/L)*G);6;86;83;82;79;92;93;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DistanceOpNode;20;-1024.779,-1001.15;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-2976.05,711.5282;Inherit;False;Constant;_Lenght;Lenght;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1031.921,-660.4652;Float;False;Property;_RingWidth;RingWidth;13;0;Create;True;0;0;0;False;0;False;0;0.3141519;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;79;-2938.811,634.0094;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;82;-2796.81,635.0094;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;24;-701.4111,-835.161;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-2811.997,734.2442;Inherit;False;Constant;_G;G;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-2608.369,627.3468;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;111;-317.5587,-824.1702;Inherit;False;Direccion;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;83;-2431.226,630.8623;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;132;-2695.113,408.7949;Inherit;False;111;Direccion;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;136;-2154.921,740.5417;Inherit;False;Constant;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;2.02;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;131;-2215.682,461.3508;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;137;-1937.681,602.3868;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;178;-2010.267,464.2397;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;135;-1742.823,463.6857;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;176;-1746.776,763.2457;Inherit;False;Constant;_RingAmount;RingAmount;6;0;Create;True;0;0;0;False;0;False;4.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;175;-1522.447,606.0887;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;17;-1142.785,652.551;Inherit;False;682.1265;352.7526;Previo al seno falta un calculo;3;32;28;27;Sen;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;675;108.9308,867.7621;Inherit;False;1654.281;407.6063;Comment;7;138;139;152;657;153;502;636;Movimiento en eje Z;1,1,1,1;0;0
Node;AmplifyShaderEditor.SinOpNode;27;-1094.003,700.1151;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;674;-118.3693,92.85529;Inherit;False;750.1154;419.2124;Comment;5;655;656;654;653;652;Remap;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;692;1031.142,-405.1878;Inherit;False;Constant;_TimeSclae;TimeSclae;12;0;Create;True;0;0;0;False;0;False;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;683;1118.722,-735.7297;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;656;-43.77636,309.4496;Inherit;False;Constant;_Float5;Float 5;17;0;Create;True;0;0;0;False;0;False;0.03529412;0.004;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;696;1201.657,-589.1049;Inherit;False;Constant;_Vector2;Vector 2;12;0;Create;True;0;0;0;False;0;False;0,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;138;158.9308,977.2993;Inherit;False;Property;_Amplitude;Amplitude;7;0;Create;True;0;0;0;False;0;False;0.12;0.33;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;687;1240.971,-436.0524;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;654;-68.36932,236.865;Inherit;False;Constant;_Float6;Float 6;14;0;Create;True;0;0;0;False;0;False;0.1882353;0.059;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;655;-25.91206,397.0677;Inherit;False;Constant;_Float3;Float 3;16;0;Create;True;0;0;0;False;0;False;0.09685075;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;653;-60.54761,142.8553;Inherit;False;Constant;_Float10;Float 10;15;0;Create;True;0;0;0;False;0;False;1.139501E-09;0.047;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-900.9656,734.1229;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.79;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;681;1532.898,-440.3522;Inherit;False;Property;_VoronoiScale;VoronoiScale;11;0;Create;True;0;0;0;False;0;False;1;11.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;682;1510.283,-524.319;Inherit;False;Property;_VoronoiAngle;VoronoiAngle;12;0;Create;True;0;0;0;False;0;False;0;1.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;695;1499.087,-674.9368;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;139;366.2855,966.0673;Inherit;False;Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;652;337.7461,205.6053;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;694;1998.862,-246.2064;Inherit;False;Constant;_Float1;Float 1;12;0;Create;True;0;0;0;False;0;False;0.53;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;680;1881.657,-612.4905;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;152;647.9891,917.7621;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;657;859.536,935.614;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;693;2253.119,-405.3991;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;691;2452.776,-61.59253;Inherit;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;690;2564.756,-189.5698;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;372;1750.564,64.61603;Inherit;False;Property;_Color0;Color 0;9;0;Create;True;0;0;0;False;0;False;0.2666667,1,0,0;0.1947671,0.1435119,0.7075472,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;677;2425.75,45.81517;Inherit;False;Property;_Color1;Color 1;10;0;Create;True;0;0;0;False;0;False;0.2666667,1,0,0;0.5424528,0.8233747,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;153;1074.805,947.1366;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;502;1310.743,970.4952;Inherit;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;678;2026.365,192.8082;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;670;-1127.851,169.3788;Inherit;False;680.7098;303.7817;Comment;3;190;369;370;Cos;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;689;2721.875,-6.482605;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;30;2650.49,413.6074;Float;False;Property;_Smoothness;Smoothness;14;0;Create;True;0;0;0;False;0;False;0;-0.84;-10;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;370;-645.1411,219.3788;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;-665.1949,749.0363;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;140;-2208.528,639.4368;Inherit;False;W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;679;2835.068,165.5722;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;669;-283.1444,759.7474;Inherit;False;Seno;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;369;-886.9081,220.0305;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.79;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;188;-372.0222,274.9235;Inherit;False;Cos;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;23;-694.4351,-559.0996;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;190;-1077.851,220.1605;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-943.2842,-448.7678;Float;False;Property;_TimeScale;TimeScale;8;0;Create;True;0;0;0;False;0;False;0;0.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;636;1519.941,985.8595;Inherit;True;VertexPosition;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3065.86,267.5947;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;NewFountain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;2;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;2;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;26;0
WireConnection;20;1;25;0
WireConnection;82;0;79;0
WireConnection;82;1;92;0
WireConnection;24;0;20;0
WireConnection;24;1;22;0
WireConnection;86;0;82;0
WireConnection;86;1;93;0
WireConnection;111;0;24;0
WireConnection;83;0;86;0
WireConnection;131;0;132;0
WireConnection;131;1;83;0
WireConnection;137;0;136;0
WireConnection;178;0;131;0
WireConnection;135;0;178;0
WireConnection;135;1;137;0
WireConnection;175;0;135;0
WireConnection;175;1;176;0
WireConnection;27;0;175;0
WireConnection;687;0;692;0
WireConnection;32;0;27;0
WireConnection;695;0;683;0
WireConnection;695;2;696;0
WireConnection;695;1;687;0
WireConnection;139;0;138;0
WireConnection;652;0;32;0
WireConnection;652;1;653;0
WireConnection;652;2;654;0
WireConnection;652;3;656;0
WireConnection;652;4;655;0
WireConnection;680;0;695;0
WireConnection;680;1;682;0
WireConnection;680;2;681;0
WireConnection;152;0;652;0
WireConnection;152;1;139;0
WireConnection;657;0;152;0
WireConnection;693;0;680;0
WireConnection;693;1;694;0
WireConnection;690;0;693;0
WireConnection;690;1;691;0
WireConnection;153;2;657;0
WireConnection;502;0;153;0
WireConnection;678;0;372;0
WireConnection;678;1;652;0
WireConnection;689;0;690;0
WireConnection;689;1;677;0
WireConnection;370;0;369;0
WireConnection;28;0;32;0
WireConnection;140;0;83;0
WireConnection;679;0;689;0
WireConnection;679;1;678;0
WireConnection;669;0;28;0
WireConnection;369;0;190;0
WireConnection;188;0;370;0
WireConnection;23;0;21;0
WireConnection;190;0;175;0
WireConnection;636;0;502;0
WireConnection;0;0;679;0
WireConnection;0;4;30;0
WireConnection;0;11;636;0
ASEEND*/
//CHKSM=C4C8821C965AD1D77F06EC1537161BA505B5C9E5