// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "UniStorm/Dynamic Light Clouds (No Fog) -1" {
    Properties {
      _LoY ("Opaque Y", Float) = 0
      _HiY ("Transparent Y", Float) = 10
      
      _Color ("Cloud Color", Color) = (0.5,0.5,0.5,0.5)
      
      _MainTex1 ("Cloud Mask (RGB) Alpha (A)", 2D) = "white"
      _MainTex2 ("Noise (RGB) Alpha (A)", 2D) = "white"
      _MainTex3 ("Cloud Texture (RGB) Alpha (A)", 2D) = "white"

      _CloudThickness ("Cloud Thickness", Float) = 1
    }
 
    SubShader {
      Tags {"Queue"="Transparent-250" "IgnoreProjector"="True" "RenderType"="Transparent"}
      Tags { "LightMode" = "ForwardBase" }
      ZWrite Off
      Blend One One
      Cull Front
      Lighting Off
	  
      CGPROGRAM
      #pragma surface surf Lambert alpha finalcolor:mycolor vertex:myvert
      #pragma target 3.0
      #pragma multi_compile_fog
      
      struct Input {
          float2 uv_MainTex1;
          float2 uv_MainTex2;
          float2 uv_MainTex3;
          half alpha;
          UNITY_FOG_COORDS(1)
          half fog;
      };
 
      sampler2D _MainTex1;
      sampler2D _MainTex2;
      sampler2D _MainTex3;
      fixed4 _Color;
      half _LoY;
      half _HiY;
      uniform half _CloudThickness;
      fixed4 _FogColor;
 
      void myvert (inout appdata_full v, out Input data) 
      { 
		  UNITY_INITIALIZE_OUTPUT(Input,data);
		  
          float4 worldV = mul (unity_ObjectToWorld, v.vertex);

          data.alpha = 1 - saturate((worldV.y - _LoY) / (_HiY - _LoY));

          UNITY_TRANSFER_FOG(data,worldV);
      }
 
      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color) 
      {
          fixed4 c1 = tex2D(_MainTex1, IN.uv_MainTex1) * _Color;
    	  fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2) ;
    	  fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3) ;

    	  color.a = IN.alpha * c1.a * c2.a * c3.a * _CloudThickness;
          UNITY_APPLY_FOG(IN.fogCoord, color.rgb);
          color.rgb = c2 * (_Color * 1.5);
      }
 
      void surf (Input IN, inout SurfaceOutput o) 
      {
		
      }
      ENDCG

     
    } 
    Fallback "Diffuse"
  }