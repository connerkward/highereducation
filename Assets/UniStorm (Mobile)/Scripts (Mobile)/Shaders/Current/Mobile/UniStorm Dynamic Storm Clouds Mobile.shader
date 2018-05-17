// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "UniStorm Mobile/Dynamic Storm Clouds (Unity 5)" {
    Properties {
      _LoY ("Opaque Y", Float) = 0
      _HiY ("Transparent Y", Float) = 10
      
      _Color ("Cloud Color", Color) = (0.5,0.5,0.5,0.5)
      
      _MainTex1 ("Cloud Mask (RGB) Alpha (A)", 2D) = "white"
      _MainTex2 ("Noise (RGB) Alpha (A)", 2D) = "white"
      _MainTex3 ("Cloud Texture (RGB) Alpha (A)", 2D) = "white"
    }
 
    SubShader {
      Tags {"Queue"="Transparent-100" "IgnoreProjector"="True" "RenderType"="Transparent"}
 	
      ZWrite Off
      Blend SrcAlpha One
      Cull Off
      AlphaTest Greater 0.01
      Lighting Off
      
      Fog  { Density .0001 } 
	  Fog  { Range 0 , 20000 }
 
      CGPROGRAM
      #pragma surface surf Lambert alpha finalcolor:mycolor vertex:myvert
      #pragma target 3.0
      
      struct Input {
          float2 uv_MainTex1;
          float2 uv_MainTex2;
          float2 uv_MainTex3;
          half alpha;
      };
 
      sampler2D _MainTex1;
      sampler2D _MainTex2;
      sampler2D _MainTex3;
      fixed4 _Color;
      half _LoY;
      half _HiY;
 
      void myvert (inout appdata_full v, out Input data) { 
      	  
		  UNITY_INITIALIZE_OUTPUT(Input,data);
		  
          float4 worldV = mul (unity_ObjectToWorld, v.vertex);

          data.alpha = 1 - saturate((worldV.y - _LoY) / (_HiY - _LoY));
      }
 
      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color) {
          fixed4 c1 = tex2D(_MainTex1, IN.uv_MainTex1) * _Color;
    	  fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
    	  fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3);
          
          color.a = IN.alpha * c3.a *c2.a * c1.a;
      }
 
      void surf (Input IN, inout SurfaceOutput o) {
   		  fixed4 c1 = tex2D(_MainTex1, IN.uv_MainTex1) * _Color;
    	  fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
    	  fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3);
    	  
    	  o.Albedo = IN.alpha * c3.a *c2.a * c1.a * _Color;
      }
      ENDCG

     
    } 
    Fallback "Diffuse"
  }