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
                float4 _MainTex_ST; //获取_MainTex纹理的Tiling和Offset，带入xyzw
                float4 _MainTex_TexelSize;//获取_MainTex纹理的宽高，4个分量如下：Vector4(1 / width, 1 / height, width, height)

                float _OutlineWidth;
                float4 _OutlineColor;
                float _AlphaThreshold;
                float _Light;

                //顶点着色器不做额外操作
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }


                //片元着色器
                half4 frag(v2f v) : SV_Target{
                    fixed4 col = tex2D(_MainTex,v.uv);

                //获取自身alpha
                float pointAlpha = col.a;

                //获取周围上下左右4个点的UV
                float2 up_uv = v.uv + float2(0,_OutlineWidth * _MainTex_TexelSize.y);
                float2 down_uv = v.uv + float2(0,-_OutlineWidth * _MainTex_TexelSize.y);
                float2 left_uv = v.uv + float2(-_OutlineWidth * _MainTex_TexelSize.x,0);
                float2 right_uv = v.uv + float2(_OutlineWidth * _MainTex_TexelSize.x,0);

                //获取周围上下左右4个点的alpha总和
                float aroundAlpha = tex2D(_MainTex,up_uv).a +
                tex2D(_MainTex,down_uv).a +
                tex2D(_MainTex,left_uv).a +
                tex2D(_MainTex,right_uv).a;

                //周围的透明度step
                float arroundStep = step(0.01,aroundAlpha);
                //自身的透明度step
                float pointStep = step(_AlphaThreshold,pointAlpha);

                //乘上亮度值
                _OutlineColor.rgb = _OutlineColor.rgb * _Light;

                //先把周围和自身都改成描边颜色
                float4 result = lerp(col,_OutlineColor,arroundStep);
                //把自身原色还远
                result = lerp(result,col,pointStep);
                //返回
                return result;

                 }
            ENDCG
            }
        }
           FallBack "Sprites/Default"
}
