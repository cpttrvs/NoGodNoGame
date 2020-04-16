// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Roof"
{
	Properties
	{
		_ThatchLong("Thatch Long", 2D) = "white" {}
		_ThatchShort("Thatch Short", 2D) = "white" {}
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
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _ThatchLong;
		uniform float4 _ThatchLong_ST;
		uniform sampler2D _ThatchShort;
		uniform float4 _ThatchShort_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ThatchLong = i.uv_texcoord * _ThatchLong_ST.xy + _ThatchLong_ST.zw;
			float2 uv_ThatchShort = i.uv_texcoord * _ThatchShort_ST.xy + _ThatchShort_ST.zw;
			float4 lerpResult4 = lerp( tex2D( _ThatchLong, uv_ThatchLong ) , tex2D( _ThatchShort, uv_ThatchShort ) , i.vertexColor.r);
			o.Albedo = lerpResult4.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
0;41;1920;978;2307.214;1009.95;1.39728;True;True
Node;AmplifyShaderEditor.VertexColorNode;3;-1667.233,52.90725;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1508.425,-499.2956;Inherit;True;Property;_ThatchShort;Thatch Short;1;0;Create;True;0;0;False;0;-1;0cef78a918829474d841ed0376703dfb;fa50143cb74a84047a6a3dce7473c04e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1362.646,-840.0926;Inherit;True;Property;_ThatchLong;Thatch Long;0;0;Create;True;0;0;False;0;-1;56fb266f2603a52479ea2c537224d23c;a7289ec842744da4a84ad3e529fea26e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;-654.0026,-334.9525;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-14,-140;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Roof;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;3;1
WireConnection;0;0;4;0
ASEEND*/
//CHKSM=355ABB1E5A011ADB64DEC3CA704012D4B682BEAB