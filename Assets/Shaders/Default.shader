// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Default" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		_Fog("Fog dist Start/End",Vector) = (0,1,0,0)
		_FogY("Fog Y Start/End/NoiseAmp",Vector) = (0,1,0,0)
		_FogColor("Fog Color", Color) = (0,0,0,0)
		_FogNoise("Fog Noise", 2D) = "black" {}
		//LightingSteps("Lighting Steps", Float) = 3
		//LightingSmoothness("Lighting Smoothness", Float) = .1
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Cell vertex:vert//fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#include "noise.cginc"

		static float LightingSteps = 3;
		static float LightingSmoothness = .1;

		sampler2D _MainTex;
		sampler2D _FogNoise;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float dist;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _FogColor;
		float4 _FogY;
		float4 _Fog;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END


		half4 LightingCell(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
			half3 h = normalize(lightDir + viewDir);

			half diff = max(0, dot(s.Normal, lightDir));

			float nh = max(0, dot(s.Normal, h));
			float spec = pow(nh, 48.0);

			diff *= LightingSteps;

			diff = floor(diff)+smoothstep(0., LightingSmoothness, frac(diff));

			diff /= LightingSteps;
			half4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff)*atten;// +_LightColor0.rgb * spec) * atten;
			c.a = s.Alpha;
			return c;
		}

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			o.dist = length(WorldSpaceViewDir(v.vertex));
		}


		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			float f_off = 0.;//snoise(IN.worldPos.xyz*2.)*_FogY.z;

			float f = smoothstep(_FogY.y, _FogY.x, IN.worldPos.y + f_off);
			f = max(f, smoothstep(_Fog.x, _Fog.y, IN.dist));

			c.rgb = lerp(c.rgb, _FogColor.rgb, f);

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
