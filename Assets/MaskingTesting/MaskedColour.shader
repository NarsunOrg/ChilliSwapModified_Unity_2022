Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Color1("Color1", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		_Color3("Color3", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	    _MaskTex("Mask", 2D) = "white" {}
		_MaskTex1("Mask1", 2D) = "white" {}
		_MaskTex2("Mask2", 2D) = "white" {}
		_MaskTex3("Mask3", 2D) = "white" {}
		//_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic",Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex, _MaskTex, _MaskTex1, _MaskTex2, _MaskTex3;
	    

        struct Input
        {
			float2 uv_MainTex;
			float2 uv_MaskTex;
			float2 uv_MaskTex1;
			float2 uv_MaskTex2;
			float2 uv_MaskTex3;
			/*float2 uv_Metallic;
			float2 uv_Normal;*/
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			float4 c = tex2D (_MainTex, IN.uv_MainTex);
			float mask = tex2D(_MaskTex, IN.uv_MainTex).r; 
			float mask1 = tex2D(_MaskTex1, IN.uv_MainTex).r;
			float mask2 = tex2D(_MaskTex2, IN.uv_MainTex).r;
			float mask3 = tex2D(_MaskTex3, IN.uv_MainTex).r; //only use 1 channel so no need to use all channels with half3/4
			/*float3 mro = tex2D(_Metallic, IN.uv_Metallic);
			float4 n = tex2D(_Normal, IN.uv_Normal);*/
			c.rgb = c.rgb * (1 - mask) + _Color * mask;
			c.rgb = c.rgb * (1 - mask1) + _Color1 * mask1;
			c.rgb = c.rgb * (1 - mask2) + _Color2 * mask2;
			c.rgb = c.rgb * (1 - mask3) + _Color3 * mask3;
			//(1 - mask) this part inverts the mask and removes it from the texture.
			// "+-Color + mask" adds the mask back into the texture with the Color.

            o.Albedo = c.rgb;
			//o.Normal = UnpackNormal(n);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
