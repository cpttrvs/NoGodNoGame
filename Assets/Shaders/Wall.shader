// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Wall"
{
	Properties
	{
		_Stone("Stone", 2D) = "white" {}
		_Plaster("Plaster", 2D) = "white" {}
		_Stone_Height("Stone_Height", 2D) = "white" {}
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

		uniform sampler2D _Stone;
		uniform float4 _Stone_ST;
		uniform sampler2D _Plaster;
		uniform float4 _Plaster_ST;
		uniform sampler2D _Stone_Height;
		uniform float4 _Stone_Height_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Stone = i.uv_texcoord * _Stone_ST.xy + _Stone_ST.zw;
			float2 uv_Plaster = i.uv_texcoord * _Plaster_ST.xy + _Plaster_ST.zw;
			float4 tex2DNode2 = tex2D( _Plaster, uv_Plaster );
			float2 uv_Stone_Height = i.uv_texcoord * _Stone_Height_ST.xy + _Stone_Height_ST.zw;
			float clampResult71 = clamp( ( i.vertexColor.r * 5.0 ) , 0.0 , 1.0 );
			float4 lerpResult4 = lerp( tex2D( _Stone, uv_Stone ) , tex2DNode2 , ( ( 1.0 - tex2D( _Stone_Height, uv_Stone_Height ).r ) * clampResult71 ));
			float saferPower74 = max( i.vertexColor.r , 0.0001 );
			float4 lerpResult59 = lerp( lerpResult4 , tex2DNode2 , pow( saferPower74 , 3.0 ));
			o.Albedo = lerpResult59.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
2416;163;1192;941;3364.066;954.6946;1.684919;True;True
Node;AmplifyShaderEditor.VertexColorNode;3;-1667.233,52.90725;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;67;-1592.83,-101.0865;Inherit;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1737.516,-324.5673;Inherit;True;Property;_Stone_Height;Stone_Height;2;0;Create;True;0;0;False;0;-1;20977112acb4953448c475886d7b869b;20977112acb4953448c475886d7b869b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-1389.83,-104.0865;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;71;-1212.83,-136.0865;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;36;-1428.304,-300.1654;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1362.646,-840.0926;Inherit;True;Property;_Stone;Stone;0;0;Create;True;0;0;False;0;-1;56fb266f2603a52479ea2c537224d23c;56fb266f2603a52479ea2c537224d23c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1514.014,-513.2684;Inherit;True;Property;_Plaster;Plaster;1;0;Create;True;0;0;False;0;-1;0cef78a918829474d841ed0376703dfb;0cef78a918829474d841ed0376703dfb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-974.2721,-163.5736;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;-654.0026,-334.9525;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;74;-1216.866,35.12643;Inherit;False;True;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;59;-397.5494,-140.7079;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-14,-140;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Wall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;68;0;3;1
WireConnection;68;1;67;0
WireConnection;71;0;68;0
WireConnection;36;0;5;1
WireConnection;25;0;36;0
WireConnection;25;1;71;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;25;0
WireConnection;74;0;3;1
WireConnection;59;0;4;0
WireConnection;59;1;2;0
WireConnection;59;2;74;0
WireConnection;0;0;59;0
ASEEND*/
//CHKSM=4C7FA4E185C7AECC7AA6291B9A8C5F06D6626BE4