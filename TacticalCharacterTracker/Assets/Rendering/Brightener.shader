Shader "Unlit/Brightener"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(1,2)) = 1
        _Red ("Red", Range(0,2)) = 1
        _Green ("Green", Range(0,2)) = 1
        _Blue ("Blue", Range(0,2)) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
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

            sampler2D _MainTex;
            float _Brightness;
            float _Red;
            float _Green;
            float _Blue;
            
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.r *= _Red;
                col.g *= _Green;
                col.b *= _Blue;
                col.rgb *= _Brightness;
                return fixed4(col.rgb, col.a);
            }
            ENDCG
        }
    }
}
