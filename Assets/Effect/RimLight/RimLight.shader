Shader "Transparent/RimLight"
{
	Properties
	{

		_MainTex("Texture",2D) = "white"{}
		_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim Power", Range(0.1,8.0)) = 3.0
		_Alpha("Alpha",Range(0,1)) = 1.0
	}
		SubShader
		{

			Tags{ "Queue" = "Transparent"}
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma surface surf Lambert 
			struct Input {
				float2 uv_MainTex;
				//float uv_BumpMap;
				float3 viewDir;
			};
			sampler2D _MainTex;
			float4 _RimColor;
			float _RimPower;
			float _Alpha;
			void surf(Input IN, inout SurfaceOutput o) {
				 half4 c = tex2D(_MainTex, IN.uv_MainTex);
				 o.Albedo = c.rgb;
				 //o.Alpha = _Alpha;
				// o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				//saturate 限制值于[0,1]之间
					o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			}
			

		ENDCG
		}

			Fallback "Diffuse"
}