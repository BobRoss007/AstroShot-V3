Shader "RaptorCat/Simple/Simple_VertexMasked" {
	Properties {
		_Color ("Zero Color", Color) = (1,0,0,1)
		_ColorR ("Red Color", Color) = (0,1,0,1)
		_ColorG ("Green Color", Color) = (0,0,1,1)
		_Specular ("Zero Specular", Color) = (0.5,0,0,0.2)
		_SpecularR ("Red Specular", Color) = (0,0.5,0,0.1)
		_SpecularG ("Green Speclar", Color) = (0,0,0.5,0.1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


		struct Input
		{
			float4 color : COLOR;
		};

		
		half4 _Color;
		half4 _ColorR;
		half4 _ColorG;
		half4 _Specular;
		half4 _SpecularR;
		half4 _SpecularG;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input v, inout SurfaceOutputStandardSpecular o) {
			// Albedo comes from a texture tinted by color
			
			half2 blends = half2(v.color.r * v.color.r, v.color.g * v.color.g);

			float4 diffuse = lerp(lerp(_Color, _ColorR, blends.r), _ColorG, blends.g);
			float4 specular = lerp(lerp(_Specular, _SpecularR, blends.r), _SpecularG, blends.g);

			o.Albedo = diffuse.rgb;
			
			o.Specular = specular.rgb * v.color.b;
			o.Smoothness = specular.a;

			o.Occlusion = v.color.b;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
