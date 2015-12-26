Shader "Custom/GlowGenerator"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_GloomTexture("GloomTexture", 2D) = "black" {}
		_Color("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_GlowStrength("Glow Strength", Range(0, 10)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Tags{ "Queue" = "Transparent" }
		

		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _GloomTexture;
			fixed4 _Color;
			uniform float4 _MainTex_ST;
			uniform float4 _MainTex_TexelSize;
			float _GlowStrength;
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};


			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.texcoord.y = 1 - o.texcoord.y;
				#endif

				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				
				fixed4 g = tex2D(_GloomTexture, i.texcoord);
				
				return float4(_Color.rgb, g.a * _GlowStrength);
				//return float4(_Color.rgb , g.a * _GlowStrength);
				
				
			}
			ENDCG
		}
	}
}
