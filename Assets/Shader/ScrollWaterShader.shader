Shader "Custom/3DWaterRippleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionSpeed ("Distortion Speed", Range(0, 10)) = 1
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.1
        _Tiling ("Tiling", Range(1, 50)) = 1.0
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _DistortionSpeed;
            float _DistortionStrength;
            float _Tiling;
 
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Tiling; // Применяем tiling к текстурным координатам
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv += float2(sin(_Time.y * _DistortionSpeed + uv.y * 10.0) * _DistortionStrength, cos(_Time.y * _DistortionSpeed + uv.x * 10.0) * _DistortionStrength);
                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
