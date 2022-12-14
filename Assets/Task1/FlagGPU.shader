Shader "Custom/FlagGPU"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Amplitude ("Amplitude", Float) = 1
        _Speed ("Speed", Float) = 1

        _FlagRoot ("FlagRoot", Vector) = (0,0,0,0)
        _FlagSize ("FlagSize", Vector) = (1,1,0,0)

        _ScrollSpeedX ("ScrollSpeedX", float) = 0.0
    }
    SubShader
    {

        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 200
        Cull Back

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Amplitude;
        float _Speed;
        fixed3 _FlagRoot;
        fixed2 _FlagSize;
        float _ScrollSpeedX;

        // To add instancing support for this shader you need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full vertexData)
        {
            float3 p = vertexData.vertex.xyz;

            float f = p.x - _Speed * _Time.y;
            p.z = _Amplitude * (p.x - _FlagRoot.x) / _FlagSize.x * sin(f);

            float3 tangent = normalize(float3(1, _Amplitude * cos(f), 0));
            float3 normal = float3(-tangent.y, tangent.x, 0);

            vertexData.vertex.xyz = p;
            vertexData.normal = normal;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, (IN.uv_MainTex + float2(_Time.y * _ScrollSpeedX % 1, 0))) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG

        // 2nd pass to fix flipped normals
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 200
        Cull Front

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Amplitude;
        float _Speed;
        fixed3 _FlagRoot;
        fixed2 _FlagSize;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full vertexData, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float3 p = vertexData.vertex.xyz;

            float f = p.x - _Speed * _Time.y;
            p.z = _Amplitude * (p.x - _FlagRoot.x) / _FlagSize.x * sin(f);

            float3 tangent = normalize(float3(1, _Amplitude * cos(f), 0));
            float3 normal = float3(-tangent.y, tangent.x, 0);

            vertexData.vertex.xyz = p;
            vertexData.normal = -normal;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG


    }
    FallBack "Diffuse"
}