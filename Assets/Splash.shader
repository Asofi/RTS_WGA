Shader "Custom/Splash" {
	Properties {
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Center("Center", Vector) = (0, 0, 0, 0) 
		_Radius("Radius", Float) = 0.5
		_Color("Color", Color) = (1, 0, 0, 1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Lighting Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 2.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		fixed4 _Color;
		float3	_Center;
		float _Radius;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float d = distance(_Center, IN.worldPos);
			if (d < _Radius)
				o.Albedo = _Color;
			else
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				
		}
		ENDCG
	}
	FallBack "Diffuse"
}
