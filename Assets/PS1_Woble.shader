Shader "Custom/PS1_Wobble"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Precision ("Wobble Precision", Float) = 40.0 // „ем меньше число, тем сильнее дрожание
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Precision;

            v2f vert (appdata v)
            {
                v2f o;
                // ѕереводим координаты в пространство клипа (экранное)
                float4 snapPos = UnityObjectToClipPos(v.vertex);
                
                // √лавна€ маги€: "снапим" (прит€гиваем) вершины к сетке
                snapPos.xyz = floor(snapPos.xyz * _Precision) / _Precision;
                
                o.vertex = snapPos;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}