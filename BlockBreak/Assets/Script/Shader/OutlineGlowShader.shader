Shader "Custom/OutlineGlowShader"
{
    Properties{
       _MainTex("Texture",2D) = "white" {}
       _OutlineWidth("OutlineWidth",Range(0,10)) = 0
       _OutlineColor("OutlineColor",Color) = (0,0,0,1)
       _AlphaThreshold("AlphaThreshold",Range(0,1)) = 0
       _Light("Light",Range(1,5)) = 1
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent"}



            Pass{
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha
                Cull Off
                CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"


                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };


                sampler2D _MainTex;
                float4 _MainTex_ST; //��ȡ_MainTex�����Tiling��Offset������xyzw
                float4 _MainTex_TexelSize;//��ȡ_MainTex����Ŀ�ߣ�4���������£�Vector4(1 / width, 1 / height, width, height)

                float _OutlineWidth;
                float4 _OutlineColor;
                float _AlphaThreshold;
                float _Light;

                //������ɫ�������������
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }


                //ƬԪ��ɫ��
                half4 frag(v2f v) : SV_Target{
                    fixed4 col = tex2D(_MainTex,v.uv);

                //��ȡ����alpha
                float pointAlpha = col.a;

                //��ȡ��Χ��������4�����UV
                float2 up_uv = v.uv + float2(0,_OutlineWidth * _MainTex_TexelSize.y);
                float2 down_uv = v.uv + float2(0,-_OutlineWidth * _MainTex_TexelSize.y);
                float2 left_uv = v.uv + float2(-_OutlineWidth * _MainTex_TexelSize.x,0);
                float2 right_uv = v.uv + float2(_OutlineWidth * _MainTex_TexelSize.x,0);

                //��ȡ��Χ��������4�����alpha�ܺ�
                float aroundAlpha = tex2D(_MainTex,up_uv).a +
                tex2D(_MainTex,down_uv).a +
                tex2D(_MainTex,left_uv).a +
                tex2D(_MainTex,right_uv).a;

                //��Χ��͸����step
                float arroundStep = step(0.01,aroundAlpha);
                //�����͸����step
                float pointStep = step(_AlphaThreshold,pointAlpha);

                //��������ֵ
                _OutlineColor.rgb = _OutlineColor.rgb * _Light;

                //�Ȱ���Χ�������ĳ������ɫ
                float4 result = lerp(col,_OutlineColor,arroundStep);
                //������ԭɫ��Զ
                result = lerp(result,col,pointStep);
                //����
                return result;

                 }
            ENDCG
            }
        }
           FallBack "Sprites/Default"
}
