Shader "Custom/ProgressBar"
{
    Properties
    {
        _Value ("Progress Bar Value", Range(0,1)) = 1
        _BackgroundColor ("Background Color", Color) = (1,1,1,1)
        _FillColor ("Fill Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Value;
            float4 _BackgroundColor;
            float4 _FillColor;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.uv.x < _Value ? _FillColor : _BackgroundColor;
                return col;
            }
            ENDCG
        }
    }
}
