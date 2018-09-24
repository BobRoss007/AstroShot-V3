Shader "RaptorCat/Character/PlayerShaderCelshaded V2"
{
    Properties
    {
        _Mask ("Mask", 2D) = "white" {}
        _MainTex ("Diffuse", 2D) = "white" {}
        _Specular ("Specular", 2D) = "white" {}
        [MaterialToggle] _UseReflections ("UseReflections", Float ) = 0
        _SpecularAdjust ("Specular Adjust", Range(0, 1)) = 1
        _Sharpness ("Sharpness", 2D) = "white" {}
        _SharpnessAdjust ("Sharpness Adjust", Range(0, 1)) = 1
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Emissive ("Emissive", 2D) = "black" {}
        _Primary ("Primary", Color) = (0.035,0.03529412,0.3098039,1)
        _Secondary ("Secondary", Color) = (0.8308824,0.8308824,0.8308824,1)
        _Third ("Third", Color) = (0.4705882,0.4137931,0.3529412,1)
        _RimPrimary ("Rim Primary", Color) = (1,1,1,1)
        _RimSecondary ("Rim Secondary", Color) = (1,1,1,1)
        _RimThird ("Rim Third", Color) = (1,1,1,1)
        [MaterialToggle] _Debug ("Debug", Float ) = 0
        [MaterialToggle] _DebugReflect ("DebugReflect", Float ) = 0
        [MaterialToggle] _DebugAmbient ("DebugAmbient", Float ) = 0
        [MaterialToggle] _DebugDirect ("DebugDirect", Float ) = 0
        _AmbientBlur ("Ambient Blur", Float) = 0.72
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="ForwardBase" }
            
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Primary;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float4 _Secondary;
            uniform float4 _Third;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Sharpness; uniform float4 _Sharpness_ST;
            uniform float4 _RimPrimary;
            uniform float4 _RimSecondary;
            uniform float4 _RimThird;
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform float _SharpnessAdjust;
            uniform float _SpecularAdjust;
            uniform float _AmbientBlur;


            float3 LightProbeGather( float3 normal )
            {
                float3 ambient = ShadeSH9( float4( normal, 1.0 ) ).rgb;
            
                return ambient;
            }
            
            float3 CubeGlossyGather( float3 normal , fixed gloss , fixed3 spec )
            {
                float4 cube = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, normal, (1 - gloss) * 6);
              
            
                //return cube;
                return DecodeHDR(cube, unity_SpecCube0_HDR) * spec;
            }
            
            uniform fixed _DebugReflect;
            uniform fixed _DebugAmbient;
            uniform fixed _DebugDirect;
            // float3 DebuggerOutput( float3 final , fixed dRefl , fixed dAmb , fixed dDir , fixed dDebug , float3 refl , float3 amb , float3 dir ){
            // if(dDebug != 0)
            // {
            // if(dRefl != 0) return refl;
            // else if(dAmb != 0) return amb;
            // else if(dDir != 0) return dir;
            // else return final;
            // }
            // else return final;
            // }
            
            uniform fixed _Debug;
            uniform fixed _UseReflections;
            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };

            struct VertexOutput
            {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

            float4 frag(VertexOutput i) : COLOR
            {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                fixed3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
                ////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                ////// Emissive:
                
                // float node_2012 = 0.0;
                
                
                
                
                // float node_1588 = 1.0;
                // float node_7173 = 0.0;
                
                
                
                // fixed node_4505 = 0.0;
                // fixed node_8265 = (-7.0);
                
                // //sky gradient falloff
                // float node_1046 = saturate((0.5*dot(normalDirection,float3(0,1,0))+0.5*1.428571+0.1428571));
                // fixed rimFade = (saturate((node_8265 + ( (((1.0 - max(0,dot(viewDirection,normalDirection)))+lerp((-0.25),0.25,rimPinch)) - node_4505) * (6.0 - node_8265) ) / (1.0 - node_4505)))*rimAmount*(1.0 - node_1046)*node_1046);
                // float3 spec = (specular.rgb*i.vertexColor.b*lerp(lerp(diffuseColor,float3(node_1588,node_1588,node_1588),0.33),rimCol,rimFade)*_SpecularAdjust);
                // float3 node_2403 = CubeGlossyGather( viewReflectDirection , gloss , spec );
                // float3 node_7556 = LightProbeGather( normalDirection );
                // float3 rimResult = (rimCol*rimFade);
                // float4 node_5994_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                // float4 node_5994_p = lerp(float4(float4(node_7556,0.0).zy, node_5994_k.wz), float4(float4(node_7556,0.0).yz, node_5994_k.xy), step(float4(node_7556,0.0).z, float4(node_7556,0.0).y));
                // float4 node_5994_q = lerp(float4(node_5994_p.xyw, float4(node_7556,0.0).x), float4(float4(node_7556,0.0).x, node_5994_p.yzx), step(node_5994_p.x, float4(node_7556,0.0).x));
                // float node_5994_d = node_5994_q.x - min(node_5994_q.w, node_5994_q.y);
                // float node_5994_e = 1.0e-10;
                // float3 node_5994 = float3(abs(node_5994_q.z + (node_5994_q.w - node_5994_q.y) / (6.0 * node_5994_d + node_5994_e)), node_5994_d / (node_5994_q.x + node_5994_e), node_5994_q.x);;
                
                // float3 diff = (diffuse.rgb*diffuseColor*i.vertexColor.rgb);
                // float node_96 = (max(0,dot(normalDirection,lightDirection))*attenuation);
                // float node_8443 = saturate((node_96*40.0+0.0));
                // float3 node_3374 = (node_8443*lerp(saturate(_LightColor0.rgb),_LightColor0.rgb,(1.0 - saturate(((1.0-max(0,dot(normalDirection, viewDirection)))*1.428571+-0.1428571))))*0.51*attenuation);
                //float3 emis = DebuggerOutput( (max((emissive.rgb+lerp( node_2012, (lerp(float3(node_2012,node_2012,node_2012),node_2403,spec)*node_7556*2.0), _UseReflections )),saturate((rimResult*(1.0 - node_5994.b))))+(diff*node_7556)) , _DebugReflect , _DebugAmbient , _DebugDirect , _Debug , node_2403 , node_7556 , node_3374 );
                
                ////// PREP VALUES //////
                float4 mask = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 diffuse = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 specular = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float gloss = tex2D(_Sharpness,TRANSFORM_TEX(i.uv0, _Sharpness)) * _SharpnessAdjust;
                float4 emissive = tex2D(_Emissive,TRANSFORM_TEX(i.uv0, _Emissive));

                float3 rimCol = lerp (lerp (lerp (_RimPrimary.rgb, _RimSecondary.rgb, mask.r), _RimThird.rgb, mask.g), 0, mask.b);
                float rimPinch = lerp (lerp (lerp (_RimPrimary.a, _RimSecondary.a, mask.r), _RimThird.a, mask.g), 0, mask.b);
                float rimAmount = lerp (lerp (lerp (_Primary.a, _Secondary.a, mask.r), _Third.a, mask.g), 0, mask.b);

                ////// DIFFUSE LIGHTING //////
                float3 diffuseColor = lerp (lerp (lerp ( _Primary.rgb, _Secondary.rgb, mask.r), _Third.rgb, mask.g), float3(1,1,1), mask.b) * diffuse;
                
                float lightFirst = saturate(dot(lightDirection, normalDirection));
                float lightPosterized = ceil(lightFirst * 8) * 0.6;
                float shadows =  saturate((attenuation - 0.5) * 8);//ceil(attenuation * 6) * 0.5;
                float directLight = saturate(lightPosterized * shadows);
                directLight = (saturate(lightPosterized) * attenuation);
                float3 directFinal = directLight * _LightColor0.rgb;
                float lightColorIntensity = max(_LightColor0.r, max(_LightColor0.g, _LightColor0.b));
                float directIntensity = directLight * lightColorIntensity;

                ////// ADDITIONAL LIGHTING //////
                float fresnelBase = max(0,dot(viewDirection,normalDirection));

                ////// SPECULAR REFLECTIONS //////
                float specFresnel = pow(1 - fresnelBase, 2);
                float3 specMod = float3(min(diffuseColor.r, specular.r), min(diffuseColor.g, specular.g), min(diffuseColor.b, specular.b));
                specMod = lerp(specMod, specular.rgb, 0.5);
                float3 adjustedSpec = lerp(specMod, specular.rgb, max(specFresnel, mask.b) );
                
                adjustedSpec = lerp(adjustedSpec, float3(1,1,1), specFresnel * 0.5);

                float adjustedGloss = lerp(gloss, 1, 1 - fresnelBase) * i.vertexColor.b;
                
                float specSpread = 2;//lerp(0.1, 1, adjustedSpec);
                float specDot = saturate(dot(halfDirection, normalDirection));
                specDot = floor(ceil(specDot * 10 * specSpread) * (1 / (specSpread * 10)));
                float3 finalSpecDot = specDot * adjustedSpec * _LightColor0.rgb * directLight;

                ////// AMBIENT LIGHT //////
                float3 ambient = CubeGlossyGather(normalDirection, 1 - _AmbientBlur, 1);
                float3 ambientResult = diffuseColor * ambient;
                float3 reflectionResult = CubeGlossyGather(viewReflectDirection, adjustedGloss, adjustedSpec);

                float ambientIntensity = max(ambient.r, max(ambient.g, ambient.b));

                ////// RIM LIGHTING //////
                float rimSkyFalloff = saturate((-normalDirection.g + 2) * 0.5);
                
                float rimGradient = 1 - saturate(fresnelBase - 0.5 * lerp(0.5, 1, rimPinch) );
                rimGradient = pow(rimGradient, exp2((1 + (rimPinch)) * 3) );

                float rimInfluence = saturate(rimGradient * rimAmount * rimSkyFalloff - saturate(lightFirst * attenuation * lightColorIntensity * 2 + ambientIntensity));
                float3 rimFinal = saturate(rimInfluence * rimCol);

                ////// FINAL STEPS //////
                float3 diffuseFinal = lerp(ambientResult.rgb, directFinal.rgb * diffuseColor, directLight);
                float3 finalRGB = max(diffuseFinal.rgb + finalSpecDot.rgb, reflectionResult.rgb);
                //finalRGB = lerp(finalRGB, finalSpecDot + 1, specDot * 0.25);
                finalRGB = (finalRGB + rimFinal) * i.vertexColor.b + emissive;
                // float3 emissive = rimResult*(1.0 - node_5994.b);
                // float3 node_7023 = spec;
                // float node_9121 = max(0,dot(halfDirection,normalDirection));
                // float node_3602 = pow(gloss,0.2);
                // float node_7965 = lerp(0.0,0.95,node_3602);
                // float node_8121 = 0.0;
                // float node_7953 = lerp(0.05,1,(attenuation*node_8443));
                // float3 result = ((1.0 - min(_Debug,max(max(_DebugReflect,_DebugAmbient),_DebugDirect)))*((node_7023*saturate(((node_8121 + ( (node_9121 - node_7965) * (1.0 - node_8121) ) / (lerp(1.0,0.95,node_3602) - node_7965))*16.66667+-7.833337))*node_7953*_LightColor0.rgb)+(diff*node_3374)));
                // float3 finalColor = emissive + result;
                // fixed4 finalRGBA = fixed4(finalColor,1);
                // UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                //return finalRGBA;
                finalRGB = lerp(finalRGB, reflectionResult, _DebugReflect);
                finalRGB = lerp(finalRGB, ambient, _DebugAmbient);
                finalRGB = lerp(finalRGB, directFinal, _DebugDirect);

                return float4(finalRGB,1);

            }
            ENDCG
        }

        Pass
        {
            Name "FORWARD_DELTA"
            Tags { "LightMode"="ForwardAdd" }
            Blend One One
            
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Primary;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float4 _Secondary;
            uniform float4 _Third;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Sharpness; uniform float4 _Sharpness_ST;
            uniform float4 _RimPrimary;
            uniform float4 _RimSecondary;
            uniform float4 _RimThird;
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform float _SharpnessAdjust;
            uniform float _SpecularAdjust;
            uniform float _AmbientBlur;


            float3 LightProbeGather( float3 normal )
            {
                float3 ambient = ShadeSH9( float4( normal, 1.0 ) ).rgb;
            
                return ambient;
            }
            
            float3 CubeGlossyGather( float3 normal , fixed gloss , fixed3 spec )
            {
                float4 cube = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, normal, (1 - gloss) * 6);
              
            
                //return cube;
                return DecodeHDR(cube, unity_SpecCube0_HDR) * spec;
            }
            
            uniform fixed _DebugReflect;
            uniform fixed _DebugAmbient;
            uniform fixed _DebugDirect;
            
            uniform fixed _Debug;
            uniform fixed _UseReflections;
            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };

            struct VertexOutput
            {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

            float4 frag(VertexOutput i) : COLOR
            {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                fixed3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
                ////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                
                ////// PREP VALUES //////
                float4 mask = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 diffuse = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 specular = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float gloss = tex2D(_Sharpness,TRANSFORM_TEX(i.uv0, _Sharpness)) * _SharpnessAdjust;
                float4 emissive = tex2D(_Emissive,TRANSFORM_TEX(i.uv0, _Emissive));

                float3 rimCol = lerp (lerp (lerp (_RimPrimary.rgb, _RimSecondary.rgb, mask.r), _RimThird.rgb, mask.g), 0, mask.b);
                float rimPinch = lerp (lerp (lerp (_RimPrimary.a, _RimSecondary.a, mask.r), _RimThird.a, mask.g), 0, mask.b);
                float rimAmount = lerp (lerp (lerp (_Primary.a, _Secondary.a, mask.r), _Third.a, mask.g), 0, mask.b);

                ////// DIFFUSE LIGHTING //////
                float3 diffuseColor = lerp (lerp (lerp ( _Primary.rgb, _Secondary.rgb, mask.r), _Third.rgb, mask.g), float3(1,1,1), mask.b) * diffuse;
                
                float lightFirst = saturate(dot(lightDirection, normalDirection));
                float lightPosterized = ceil(lightFirst * 8) * 0.6;
                float shadows =  saturate((attenuation - 0.5) * 8);//ceil(attenuation * 6) * 0.5;
                float directLight = saturate(lightPosterized * shadows);
                directLight = (saturate(lightPosterized) * attenuation);
                float3 directFinal = directLight * _LightColor0.rgb;
                float lightColorIntensity = max(_LightColor0.r, max(_LightColor0.g, _LightColor0.b));
                float directIntensity = directLight * lightColorIntensity;

                ////// ADDITIONAL LIGHTING //////
                float fresnelBase = max(0,dot(viewDirection,normalDirection));

                ////// SPECULAR REFLECTIONS //////
                float specFresnel = pow(1 - fresnelBase, 2);
                float3 specMod = float3(min(diffuseColor.r, specular.r), min(diffuseColor.g, specular.g), min(diffuseColor.b, specular.b));
                specMod = lerp(specMod, specular.rgb, 0.5);
                float3 adjustedSpec = lerp(specMod, specular.rgb, max(specFresnel, mask.b) );
                
                adjustedSpec = lerp(adjustedSpec, float3(1,1,1), specFresnel * 0.5);

                float adjustedGloss = lerp(gloss, 1, 1 - fresnelBase) * i.vertexColor.b;
                
                float specSpread = 2;//lerp(0.1, 1, adjustedSpec);
                float specDot = saturate(dot(halfDirection, normalDirection));
                specDot = floor(ceil(specDot * 10 * specSpread) * (1 / (specSpread * 10)));
                float3 finalSpecDot = specDot * adjustedSpec * _LightColor0.rgb * directLight;

                ////// AMBIENT LIGHT //////
                float3 ambient = CubeGlossyGather(normalDirection, 1 - _AmbientBlur, 1);
                float3 ambientResult = diffuseColor * ambient;
                float3 reflectionResult = CubeGlossyGather(viewReflectDirection, adjustedGloss, adjustedSpec);

                float ambientIntensity = max(ambient.r, max(ambient.g, ambient.b));

                ////// RIM LIGHTING //////
                float rimSkyFalloff = saturate((-normalDirection.g + 2) * 0.5);
                
                float rimGradient = 1 - saturate(fresnelBase - 0.5 * lerp(0.5, 1, rimPinch) );
                rimGradient = pow(rimGradient, exp2((1 + (rimPinch)) * 3) );

                float rimInfluence = saturate(rimGradient * rimAmount * rimSkyFalloff - saturate(lightFirst * attenuation * lightColorIntensity * 2 + ambientIntensity));
                float3 rimFinal = saturate(rimInfluence * rimCol);

                ////// FINAL STEPS //////
                float3 diffuseFinal = lerp(ambientResult.rgb, directFinal.rgb * diffuseColor, directLight);
                //float3 finalRGB = max(diffuseFinal.rgb + finalSpecDot.rgb, reflectionResult.rgb);
                float3 finalRGB = directFinal.rgb * diffuseColor + finalSpecDot.rgb;
                

                //finalRGB = (finalRGB + rimFinal) * i.vertexColor.b + emissive;

                finalRGB = lerp(finalRGB, reflectionResult, _DebugReflect);
                finalRGB = lerp(finalRGB, ambient, _DebugAmbient);
                finalRGB = lerp(finalRGB, directFinal, _DebugDirect);

                return float4(finalRGB,1);

            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
