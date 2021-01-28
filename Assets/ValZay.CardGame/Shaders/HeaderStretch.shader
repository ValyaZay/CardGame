Shader "CardGame/HeaderStretch"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        //Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;

        struct Input
        {
            half2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
