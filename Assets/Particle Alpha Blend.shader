Shader "Particles/Alpha Blended CUSTOM" {
Properties {
	_Color ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_EmissionColor("Emission Color", Color) = (0.5,0.5,0.5,0.5)
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "MyTag"="RenderBlack" }
	
	
	Blend SrcAlpha OneMinusSrcAlpha
	
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off

		SubShader {

			Pass {
		
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				#pragma multi_compile_fog
			
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed4 _Color;
				fixed4 _EmissionColor;
				float4 _MainTex_ST;
				sampler2D_float _CameraDepthTexture;
				float _InvFade;


				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
				};
			
			
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					#ifdef SOFTPARTICLES_ON
					o.projPos = ComputeScreenPos (o.vertex);
					COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

			
			
				fixed4 frag (v2f i) : SV_Target
				{
					#ifdef SOFTPARTICLES_ON
					float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
					float partZ = i.projPos.z;
					float fade = saturate (_InvFade * (sceneZ-partZ));
					i.color.a *= fade;
					#endif
				
					fixed4 col = 2.0f * i.color * _Color * tex2D(_MainTex, i.texcoord);
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}


		
	} 
}
