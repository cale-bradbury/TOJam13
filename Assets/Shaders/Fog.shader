// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Fog"
{
	Properties
	{
		_MaskTex("Mask", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,1)
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
#pragma multi_compile_particles
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos: TEXCOORD1;
				fixed4 color : COLOR;
#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
#endif
			};

			float4 _Color;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _InvFade;
			sampler2D_float _CameraDepthTexture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldPos.xz = o.worldPos.xz*_NoiseTex_ST.xy + _NoiseTex_ST.zw;
				o.uv = v.uv;
				o.color = v.color;
#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
#endif
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				// sample the texture
				fixed4 col = tex2D(_MaskTex, i.uv);
				float4 noise = tex2D(_NoiseTex, i.worldPos.xz);
				float2 tempPos = i.worldPos.xz;
				for (float j = 0.; j < 3.; j++) {
					tempPos *= .6;
					tempPos += .3;
					if (j%2. == 0.) {
						tempPos.y -= _Time.x;
						noise += tex2D(_NoiseTex, tempPos);
					}
					else {
						tempPos.y += _Time.x;
						noise *= tex2D(_NoiseTex, tempPos);
					}
				}
				col.a *= noise.r;

#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
				float partZ = i.projPos.z;
				float fade = saturate(_InvFade * (sceneZ - partZ));
				col.a *= fade;
#endif
				_Color *= i.color;
				col.rgb = _Color.rgb;
				col.a *= _Color.a;
				return col;
			}
			ENDCG
		}
	}
}
