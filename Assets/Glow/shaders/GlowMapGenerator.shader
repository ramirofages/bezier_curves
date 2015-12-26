Shader "hidden/GlowmapGenerator"
{

	SubShader
	{
		Tags{ "MyTag" = "RenderBlack" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{

			Fog{ Mode Off }

			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"
				fixed4 _Color;
				sampler2D _MainTex;

				float4 frag(v2f_img i) : COLOR
				{
					float4 c = tex2D(_MainTex, i.uv);
					
					return float4(0, 0, 0, c.a);
					
				}
			ENDCG
		}
	}

	
}
