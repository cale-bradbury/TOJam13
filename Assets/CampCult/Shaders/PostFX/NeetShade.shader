
Shader "ShaderMan/NeetShade"
	{

	Properties{
	_MainTex ("MainTex", 2D) = "white" {}
	count("count", Float) = 24.
	center("center", Vector) = (.5,.5, .5, .5)
	mul("mul", Float) = .99
	falloffStart("falloffStart", Float) = .02
	falloffEnd("falloffStart", Float) = .3
	alpha("alpha", Float) = 1.
	phase ("phase", Float) = 0.
	}

	SubShader
	{
	Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

	Pass
	{
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha

	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

	struct VertexInput {
    fixed4 vertex : POSITION;
	fixed2 uv:TEXCOORD0;
    fixed4 tangent : TANGENT;
    fixed3 normal : NORMAL;
	//VertexInput
	};


	struct VertexOutput {
	fixed4 pos : SV_POSITION;
	fixed2 uv:TEXCOORD0;
	//VertexOutput
	};

	//Variables
sampler2D _MainTex;

	fixed count = 24.;			//how many repititions to have
	fixed4 center = fixed4(.5,.5, .5, .5);//center of the effect
	fixed mul = .99;		//how far it shifts the image, probibly keep between .9 and 1.0
	fixed falloffStart = .02;	//where the falloff of the effect starts as fracion of screen size
	fixed falloffEnd = .3;		//where the falloff of the effect ends as fracion of screen size
	fixed alpha = 1.;		//makes the coloration less visible, really high numbers are very glitch looking, but looks best at 1. imo
	fixed phase = 0.;		//rotate the color
	//REPLACE iGLobalTime on Line 24






	VertexOutput vert (VertexInput v)
	{
	VertexOutput o;
	o.pos = UnityObjectToClipPos (v.vertex);
	o.uv = v.uv;
	//VertexFactory
	return o;
	}
	fixed4 frag(VertexOutput i) : SV_Target
	{

	fixed2 uv = i.uv / 1;


	fixed4 c = tex2D(_MainTex, uv);
	uv -= center.xy;

	fixed4 z = fixed4(0.,0.,0.,0.);
	fixed s = 1. / count;
	[unroll(100)]
for (fixed i = s; i < 1. + s; i += s) {
		uv *= mul;
		fixed4 x = tex2D(_MainTex, uv + center.xy);
		z += lerp(x, cos(x*center.z + i * center.w - _Time.y * phase)*.5 + .5, alpha);

	}
		z /= count;

		float d = length(uv);
		d = smoothstep(falloffStart, falloffEnd, d);

		c = lerp(c, z, min(alpha, d));
		c.a = 1.;
		return c;
	}
	ENDCG
	}
  }
}

