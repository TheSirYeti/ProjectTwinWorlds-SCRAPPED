// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GroundVFX"
{
	Properties
	{
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_DisolveSpeed("DisolveSpeed", Vector) = (-0.5,0,0,0)
		_Vector3("Vector 3", Vector) = (0,0,0,0)
		_Amplitude("Amplitude", Float) = 0.12
		_DisolveScale("DisolveScale", Float) = 35
		_RingWidth("RingWidth", Range( 0 , 2)) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,0,0,0)
		_Float2("Float 2", Range( 0 , 2)) = 0
		_Float19("Float 19", Range( 0 , 1)) = 1
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
			float2 uv_texcoord;
		};

		uniform float2 _Vector0;
		uniform float _RingWidth;
		uniform float _Amplitude;
		uniform float4 _Color0;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float2 _Vector3;
		uniform float _Float2;
		uniform float2 _DisolveSpeed;
		uniform float _DisolveScale;
		uniform float _Float19;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float Direccion14 = ( distance( _Vector0 , v.texcoord.xy ) / _RingWidth );
			float temp_output_15_0 = sqrt( ( ( 6.28318548202515 / 1.0 ) * 1.0 ) );
			float mulTime20 = _Time.y * 2.02;
			float temp_output_23_0 = ( ( ( 1.0 - ( Direccion14 * temp_output_15_0 ) ) + mulTime20 ) * 4.1 );
			float temp_output_31_0 = ( sin( temp_output_23_0 ) * 0.79 );
			float Amplitude34 = _Amplitude;
			float4 appendResult48 = (float4(0.0 , 0.0 , saturate( ( (0.03529412 + (temp_output_31_0 - 1.139501E-09) * (0.09685075 - 0.03529412) / (0.1882353 - 1.139501E-09)) * Amplitude34 ) ) , 0.0));
			float4 VertexPosition50 = saturate( appendResult48 );
			v.vertex.xyz += VertexPosition50.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode51 = tex2D( _TextureSample0, uv_TextureSample0 );
			float Direccion14 = ( distance( _Vector0 , i.uv_texcoord ) / _RingWidth );
			float temp_output_15_0 = sqrt( ( ( 6.28318548202515 / 1.0 ) * 1.0 ) );
			float mulTime20 = _Time.y * 2.02;
			float temp_output_23_0 = ( ( ( 1.0 - ( Direccion14 * temp_output_15_0 ) ) + mulTime20 ) * 4.1 );
			float temp_output_31_0 = ( sin( temp_output_23_0 ) * 0.79 );
			float temp_output_39_0 = saturate( temp_output_31_0 );
			float temp_output_81_0 = (1.33 + (pow( ( distance( _Vector3 , i.uv_texcoord ) / _Float2 ) , 3.85 ) - 0.0) * (1.26 - 1.33) / (1.0 - 0.0));
			float2 uv_TexCoord64 = i.uv_texcoord + ( _Time.y * _DisolveSpeed );
			float simplePerlin2D66 = snoise( uv_TexCoord64*_DisolveScale );
			simplePerlin2D66 = simplePerlin2D66*0.5 + 0.5;
			float Xd87 = ( 1.0 - ( ( 1.0 - temp_output_81_0 ) - ( simplePerlin2D66 + temp_output_81_0 ) ) );
			float4 temp_output_92_0 = ( temp_output_39_0 * Xd87 * tex2DNode51 );
			o.Albedo = ( ( _Color0 * tex2DNode51 ) * temp_output_92_0 ).rgb;
			o.Occlusion = _Float19;
			o.Alpha = temp_output_92_0.r;
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
2085;71;1920;970;6086.803;1678.718;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;2;-3253.606,-1239.403;Inherit;False;886.677;758.8679;;7;44;42;11;8;6;4;3;Efecto de radio;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;3;-3195.773,-1189.403;Float;False;Property;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;0,0;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-3206.697,-1046.942;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;5;-4872.694,419.2168;Inherit;False;714.442;304.2192;Wavelenght (w) =  sqrt((2*pi/L)*G);6;15;13;12;10;9;7;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DistanceOpNode;6;-2886.779,-1160.917;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-4838.05,551.7607;Inherit;False;Constant;_Lenght;Lenght;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2893.921,-820.2325;Float;False;Property;_RingWidth;RingWidth;6;0;Create;True;0;0;0;False;0;False;0;0.3141519;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;9;-4800.811,474.2419;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-4673.997,574.4767;Inherit;False;Constant;_G;G;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-2563.411,-994.9283;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;10;-4658.81,475.2419;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;14;-2179.559,-983.9376;Inherit;True;Direccion;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1;-4922.694,199.0273;Inherit;False;1707.246;519.4507;Comment;9;40;23;22;21;20;19;18;17;16;Calculo de la ola;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-4470.369,467.5793;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-4555.845,250.2948;Inherit;True;14;Direccion;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;15;-4293.227,471.0948;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;71;-6424.685,-1768.756;Inherit;False;886.677;758.8679;;8;79;78;77;76;75;74;73;72;Efecto de radio;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-4077.682,301.5833;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-4016.921,580.7742;Inherit;False;Constant;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;2.02;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;77;-6366.852,-1718.756;Float;False;Property;_Vector3;Vector 3;2;0;Create;True;0;0;0;False;0;False;0,0;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;76;-6376.776,-1492.295;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;59;-5833.674,-2769.121;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;20;-3784.681,562.6193;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-6065,-1349.585;Float;False;Property;_Float2;Float 2;11;0;Create;True;0;0;0;False;0;False;0;0.3141519;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;19;-3872.267,304.4721;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;60;-5827.521,-2444.833;Inherit;False;Property;_DisolveSpeed;DisolveSpeed;1;0;Create;True;0;0;0;False;0;False;-0.5,0;50,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DistanceOpNode;78;-6110.592,-1676.105;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-3608.776,603.4782;Inherit;False;Constant;_RingAmount;RingAmount;6;0;Create;True;0;0;0;False;0;False;4.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-3604.823,303.9182;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-5598.722,-1319.069;Inherit;False;Constant;_Float13;Float 13;14;0;Create;True;0;0;0;False;0;False;3.85;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;73;-5734.49,-1524.281;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-5455.794,-2626.187;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;64;-5229.62,-2738.393;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;85;-5192.094,-1096.857;Inherit;False;Constant;_Float15;Float 15;14;0;Create;True;0;0;0;False;0;False;1.26;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-3384.447,446.3211;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;80;-5433.319,-1496.944;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-4975.742,-2587.86;Inherit;False;Property;_DisolveScale;DisolveScale;4;0;Create;True;0;0;0;False;0;False;35;120;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-5184.094,-1293.857;Inherit;False;Constant;_Float17;Float 17;14;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;24;-3004.785,492.7835;Inherit;False;682.1265;352.7526;Previo al seno falta un calculo;3;39;31;26;Sen;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-5200.094,-1198.857;Inherit;False;Constant;_Float18;Float 18;14;0;Create;True;0;0;0;False;0;False;1.33;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-5161.094,-1394.857;Inherit;False;Constant;_Float16;Float 16;14;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;81;-4976.795,-1486.62;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;66;-4809.929,-2812.669;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;25;-1753.07,707.9947;Inherit;False;1654.281;407.6063;Comment;7;50;49;48;47;36;34;32;Movimiento en eje Z;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;27;-1980.37,-66.9123;Inherit;False;750.1154;419.2124;Comment;5;35;33;30;29;28;Remap;1,1,1,1;0;0
Node;AmplifyShaderEditor.SinOpNode;26;-2956.003,540.3476;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1703.07,817.5319;Inherit;False;Property;_Amplitude;Amplitude;3;0;Create;True;0;0;0;False;0;False;0.12;0.33;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1930.37,77.09742;Inherit;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;0;False;0;False;0.1882353;0.059;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1905.777,149.682;Inherit;False;Constant;_Float1;Float 1;17;0;Create;True;0;0;0;False;0;False;0.03529412;0.004;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1887.913,237.3001;Inherit;False;Constant;_Float3;Float 3;16;0;Create;True;0;0;0;False;0;False;0.09685075;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1922.548,-16.91229;Inherit;False;Constant;_Float10;Float 10;15;0;Create;True;0;0;0;False;0;False;1.139501E-09;0.047;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;86;-4659.326,-2244.887;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-4380.6,-2668.447;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-2762.966,574.3554;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.79;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;-1495.715,806.2999;Inherit;False;Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;35;-1524.255,45.83772;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;70;-4130.725,-2431.447;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;90;-3850.162,-2393.27;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1214.011,757.9947;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;47;-1002.465,775.8466;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;87;-3501.828,-2351.833;Inherit;True;Xd;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;48;-787.1956,787.3692;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;53;-1190.22,-1081.567;Inherit;False;Property;_Color0;Color 0;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.6981132,0.5748613,0.2667319,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;91;-935.2298,304.3305;Inherit;True;87;Xd;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;51;-1246.012,-807.4247;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;0;False;0;False;-1;None;548c9dc762ef82148a7857bc3bba8ab9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;39;-2527.195,589.2687;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-832.5461,-1050.445;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2989.851,9.611216;Inherit;False;680.7098;303.7817;Comment;3;43;41;38;Cos;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;49;-551.2577,810.7278;Inherit;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-640.8945,159.3804;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;74;-5727.514,-1248.22;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-329.2272,-78.40109;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;56;-938.124,-666.8049;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;40;-4070.529,479.6693;Inherit;False;W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;58;-1056.505,-451.0889;Inherit;False;Property;_Color1;Color 1;9;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4150943,0.3448967,0.1781773,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;-377.853,270.3801;Inherit;False;Property;_Float19;Float 19;12;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;42;-2556.435,-718.867;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-484.6856,-744.1274;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-5976.363,-1137.888;Float;False;Property;_Float14;Float 14;10;0;Create;True;0;0;0;False;0;False;0;0.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;43;-2939.851,60.39295;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-2234.023,115.1559;Inherit;False;Cos;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;-342.0598,826.0921;Inherit;True;VertexPosition;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;38;-2507.141,59.61121;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-2805.284,-608.5352;Float;False;Property;_TimeScale;TimeScale;5;0;Create;True;0;0;0;False;0;False;0;0.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-2145.145,599.9798;Inherit;False;Seno;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-2748.908,60.26295;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.79;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-730.5795,-522.5679;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;GroundVFX;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;3;0
WireConnection;6;1;4;0
WireConnection;11;0;6;0
WireConnection;11;1;8;0
WireConnection;10;0;9;0
WireConnection;10;1;7;0
WireConnection;14;0;11;0
WireConnection;13;0;10;0
WireConnection;13;1;12;0
WireConnection;15;0;13;0
WireConnection;17;0;16;0
WireConnection;17;1;15;0
WireConnection;20;0;18;0
WireConnection;19;0;17;0
WireConnection;78;0;77;0
WireConnection;78;1;76;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;73;0;78;0
WireConnection;73;1;72;0
WireConnection;61;0;59;0
WireConnection;61;1;60;0
WireConnection;64;1;61;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;80;0;73;0
WireConnection;80;1;79;0
WireConnection;81;0;80;0
WireConnection;81;1;82;0
WireConnection;81;2;83;0
WireConnection;81;3;84;0
WireConnection;81;4;85;0
WireConnection;66;0;64;0
WireConnection;66;1;62;0
WireConnection;26;0;23;0
WireConnection;86;0;81;0
WireConnection;89;0;66;0
WireConnection;89;1;81;0
WireConnection;31;0;26;0
WireConnection;34;0;32;0
WireConnection;35;0;31;0
WireConnection;35;1;29;0
WireConnection;35;2;30;0
WireConnection;35;3;33;0
WireConnection;35;4;28;0
WireConnection;70;0;86;0
WireConnection;70;1;89;0
WireConnection;90;0;70;0
WireConnection;36;0;35;0
WireConnection;36;1;34;0
WireConnection;47;0;36;0
WireConnection;87;0;90;0
WireConnection;48;2;47;0
WireConnection;39;0;31;0
WireConnection;52;0;53;0
WireConnection;52;1;51;0
WireConnection;49;0;48;0
WireConnection;92;0;39;0
WireConnection;92;1;91;0
WireConnection;92;2;51;0
WireConnection;74;0;75;0
WireConnection;93;0;52;0
WireConnection;93;1;92;0
WireConnection;40;0;15;0
WireConnection;42;0;44;0
WireConnection;43;0;23;0
WireConnection;45;0;38;0
WireConnection;50;0;49;0
WireConnection;38;0;41;0
WireConnection;46;0;39;0
WireConnection;41;0;43;0
WireConnection;57;0;56;0
WireConnection;57;1;58;0
WireConnection;0;0;93;0
WireConnection;0;5;94;0
WireConnection;0;9;92;0
WireConnection;0;11;50;0
ASEEND*/
//CHKSM=E93123C3E55F88FB3614D3371322C5D19CFD063E