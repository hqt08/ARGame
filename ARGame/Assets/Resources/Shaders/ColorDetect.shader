Shader "Custom/ColorDetect" {
Properties {
 _MainTex ("", 2D) = "white" {}
 _ThresholdColor ("Threshold Color", Color) = (0.3,0.3,0.3,1)
 _ShadingColor ("Shading Color", Color) = (0,1,0,1)
}
 
SubShader {
 
ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings
 
 Pass{
  CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag
  #include "UnityCG.cginc" 
  //we include "UnityCG.cginc" to use the appdata_img struct
    
   struct v2f {
    float4 pos : POSITION;
    half2 uv : TEXCOORD0;
   };
   
   fixed4 _ThresholdColor;
   fixed4 _ShadingColor;

   //Our Vertex Shader 
   v2f vert (appdata_img v){
    v2f o;
    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
    o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
    return o; 
   }
    
   sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders
    
   //Our Fragment Shader
   fixed4 frag (v2f i) : COLOR{
   fixed4 orgCol = tex2D(_MainTex, i.uv); //Get the orginal rendered color 
     
   //Change to shading color if color is below the threshold color
   float r_thres = step(orgCol.r, _ThresholdColor.r);
   float g_thres = step(orgCol.g, _ThresholdColor.g);
   float b_thres = step(orgCol.b, _ThresholdColor.b);
   float thres = (r_thres+g_thres+b_thres == 3);
   fixed4 col = lerp(orgCol, _ShadingColor, thres);
     
   return col;
  }
  ENDCG
 }
} 
 FallBack "Diffuse"
}