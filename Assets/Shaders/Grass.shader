// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Grass"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.9
		_Grasstuft1("Grasstuft1", 2D) = "white" {}
		_WindMovement("Wind Movement", Vector) = (0,0,0,0)
		_WindStrength("Wind Strength", Float) = 1
		_WindDensity("Wind Density", Float) = 1
		_ColorBase("Color Base", Color) = (0.7830189,0.7583481,0.1662069,0)
		_ColorTip("Color Tip", Color) = (0.5866716,0.735849,0.239498,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float2 _WindMovement;
		uniform float _WindDensity;
		uniform float _WindStrength;
		uniform float4 _ColorTip;
		uniform float4 _ColorBase;
		uniform sampler2D _Grasstuft1;
		uniform float4 _Grasstuft1_ST;
		uniform float _Cutoff = 0.9;


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
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 uv_TexCoord3 = v.texcoord.xy * ase_vertex3Pos.xy + ( _Time.y * _WindMovement );
			float simplePerlin2D11 = snoise( uv_TexCoord3*_WindDensity );
			simplePerlin2D11 = simplePerlin2D11*0.5 + 0.5;
			float4 appendResult18 = (float4(( ( ( simplePerlin2D11 - 0.5 ) * _WindStrength ) + ase_vertex3Pos.x ) , ase_vertex3Pos.y , ase_vertex3Pos.z , 0.0));
			float4 lerpResult23 = lerp( float4( ase_vertex3Pos , 0.0 ) , appendResult18 , v.texcoord.xy.y);
			v.vertex.xyz += lerpResult23.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult26 = lerp( _ColorTip , _ColorBase , ( 1.0 - i.uv_texcoord.y ));
			o.Albedo = lerpResult26.rgb;
			o.Alpha = 1;
			float2 uv_Grasstuft1 = i.uv_texcoord * _Grasstuft1_ST.xy + _Grasstuft1_ST.zw;
			clip( tex2D( _Grasstuft1, uv_Grasstuft1 ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
0;12;1920;1007;1343.145;398.7971;1.319248;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1885.816,188.8316;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-1881.816,366.8316;Inherit;False;Property;_WindMovement;Wind Movement;2;0;Create;True;0;0;False;0;0,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1592.816,247.8316;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-1822.816,-33.16842;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1485.816,94.83157;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1590.816,440.8316;Inherit;False;Property;_WindDensity;Wind Density;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;11;-1216.816,239.8316;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;-937.8162,263.8316;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1118.212,586.1223;Inherit;False;Property;_WindStrength;Wind Strength;3;0;Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1179.478,846.9898;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;16;-825.9518,650.0652;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-625.9519,297.0652;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;24;-789.7411,858.6118;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-516.9519,425.0652;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-242.9519,585.0652;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PosVertexDataNode;25;-262.0975,397.6721;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;27;-544.358,-353.4502;Inherit;False;Property;_ColorBase;Color Base;5;0;Create;True;0;0;False;0;0.7830189,0.7583481,0.1662069,0;0.4494598,0.4528302,0.2798149,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;29;-487.7047,-173.5954;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;28;-532.7889,-558.585;Inherit;False;Property;_ColorTip;Color Tip;6;0;Create;True;0;0;False;0;0.5866716,0.735849,0.239498,0;0.7830189,0.7647827,0.3361072,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-398.3052,181.4984;Inherit;True;Property;_Grasstuft1;Grasstuft1;1;0;Create;True;0;0;False;0;-1;7615aec8f84d5d742823021ab358255e;7615aec8f84d5d742823021ab358255e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;26;-226.1394,-377.6537;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;23;6.693598,558.9684;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;404.2803,145.7272;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard; ;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.9;True;True;0;True;TransparentCutout;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;0
WireConnection;7;1;5;0
WireConnection;3;0;2;0
WireConnection;3;1;7;0
WireConnection;11;0;3;0
WireConnection;11;1;10;0
WireConnection;13;0;11;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;24;0;22;0
WireConnection;17;0;15;0
WireConnection;17;1;16;1
WireConnection;18;0;17;0
WireConnection;18;1;16;2
WireConnection;18;2;16;3
WireConnection;29;0;24;1
WireConnection;26;0;28;0
WireConnection;26;1;27;0
WireConnection;26;2;29;0
WireConnection;23;0;25;0
WireConnection;23;1;18;0
WireConnection;23;2;24;1
WireConnection;0;0;26;0
WireConnection;0;10;1;1
WireConnection;0;11;23;0
ASEEND*/
//CHKSM=2671E686DE3F7C41182AB25CD8B0AC45A2D9B9CC