Shader "Unlit/SimpleShader"
{
    Properties
    {
        _Color ("Color",Color) = (1,1,1,1)
        _Gloss ("Gloss",float) = 1
        //_MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert //just name
            #pragma fragment frag //just name

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            
            //mesh data
            struct VertextInput 
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv0 : TEXCOORD0;
            };
            
            //what vertex give to fragment shader
            struct VertextOutput 
            {
                float4 clipSpacePos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD2;
            };

            float4 _Color;
            float _Gloss;
            //vertex shader
            VertextOutput vert (VertextInput v)
            {
                VertextOutput o;
                o.uv0 = v.uv0;
                o.normal = v.normal;
                o.worldPos = mul( unity_ObjectToWorld ,v.vertex);
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag (VertextOutput o) : SV_Target
            {
            float3 normal = normalize( o.normal);
                //DirectDiffuseLight
                float4 lightDir = _WorldSpaceLightPos0;
                float3 lightColor = _LightColor0.rgb;
                float lightFalloff = max(0,dot(lightDir,normal));
                float3 directDiffuseLight = lightColor * lightFalloff;
                
                //Ambient light
                float3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.rgb;
                
                //Direct specular light
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragToCam = camPos - o.worldPos;
                float3 viewDir = normalize( fragToCam );
                float3 viewReflect = reflect(-viewDir, normal);
                float specularFallOff = max(0, dot(viewReflect,lightDir));
                //modify Gloss;
                specularFallOff = pow(specularFallOff,_Gloss);
                
                //Composite
                float3 finalSurfaceColor = _Color * (ambientLight+directDiffuseLight) + specularFallOff;
                
                //blin
                
                
                return float4(finalSurfaceColor,0);
            }
            ENDCG
        }
    }
}
