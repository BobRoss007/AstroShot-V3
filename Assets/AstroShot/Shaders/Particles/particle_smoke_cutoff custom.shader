// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|normal-9170-OUT,emission-6512-OUT,custl-5421-OUT,clip-5202-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:30545,y:32259,varname:node_6343,prsc:2|A-6665-RGB,B-5106-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:30349,y:32149,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:29191,y:33255,ptovrint:True,ptlb:Heightmask,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:93f414f8fb1e47249b53af670ddb4966,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:31868,y:32818,ptovrint:True,ptlb:Normal,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a2420ccf61d97ab44b7c33d830f85595,ntxv:3,isnm:True;n:type:ShaderForge.SFN_VertexColor,id:5106,x:30349,y:32319,varname:node_5106,prsc:2;n:type:ShaderForge.SFN_Lerp,id:9170,x:32069,y:32818,varname:node_9170,prsc:2|A-7193-OUT,B-5964-RGB,T-3366-OUT;n:type:ShaderForge.SFN_Vector3,id:7193,x:31868,y:32704,varname:node_7193,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Slider,id:5144,x:31538,y:33047,ptovrint:False,ptlb:Normal Strength,ptin:_NormalStrength,varname:_NormalStrength,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:3761,x:32229,y:33561,varname:node_3761,prsc:2|EXP-3538-OUT;n:type:ShaderForge.SFN_Clamp01,id:4187,x:32395,y:33561,varname:node_4187,prsc:2|IN-4530-OUT;n:type:ShaderForge.SFN_Vector1,id:3538,x:32057,y:33609,varname:node_3538,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:9507,x:29409,y:33189,varname:node_9507,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:2791,x:29603,y:33255,varname:node_2791,prsc:2|A-9507-OUT,B-7736-R,T-6792-OUT;n:type:ShaderForge.SFN_Multiply,id:6763,x:30732,y:33232,varname:node_6763,prsc:2|A-1125-OUT,B-2969-OUT,C-5817-OUT;n:type:ShaderForge.SFN_Slider,id:6792,x:29191,y:33483,ptovrint:False,ptlb:Height Strength,ptin:_HeightStrength,varname:_HeightStrength,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:6916,x:29847,y:32814,ptovrint:False,ptlb:test,ptin:_test,varname:_test,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Relay,id:1125,x:30574,y:33003,varname:node_1125,prsc:2|IN-9583-OUT;n:type:ShaderForge.SFN_Set,id:3749,x:32880,y:33561,varname:fresnel,prsc:2|IN-4187-OUT;n:type:ShaderForge.SFN_Get,id:2969,x:30539,y:33268,varname:node_2969,prsc:2|IN-3749-OUT;n:type:ShaderForge.SFN_Vector1,id:4823,x:30560,y:33404,varname:node_4823,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:6530,x:30560,y:33479,ptovrint:False,ptlb:Distance Blend,ptin:_DistanceBlend,varname:_DistanceBlend,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Depth,id:9344,x:30732,y:33554,varname:node_9344,prsc:2;n:type:ShaderForge.SFN_Divide,id:4548,x:30732,y:33404,varname:node_4548,prsc:2|A-4823-OUT,B-6530-OUT;n:type:ShaderForge.SFN_Multiply,id:9179,x:30895,y:33404,varname:node_9179,prsc:2|A-4548-OUT,B-9344-OUT;n:type:ShaderForge.SFN_Clamp01,id:5817,x:31055,y:33404,varname:node_5817,prsc:2|IN-9179-OUT;n:type:ShaderForge.SFN_Set,id:9911,x:31066,y:33074,varname:clipInput,prsc:2|IN-6872-OUT;n:type:ShaderForge.SFN_Get,id:5202,x:32008,y:33183,varname:node_5202,prsc:2|IN-9911-OUT;n:type:ShaderForge.SFN_RemapRange,id:1254,x:29759,y:33255,varname:node_1254,prsc:2,frmn:0,frmx:1,tomn:0.01,tomx:1|IN-2791-OUT;n:type:ShaderForge.SFN_OneMinus,id:4890,x:32561,y:33561,varname:node_4890,prsc:2|IN-4187-OUT;n:type:ShaderForge.SFN_Clamp01,id:4439,x:29928,y:33255,varname:node_4439,prsc:2|IN-1254-OUT;n:type:ShaderForge.SFN_Multiply,id:9583,x:30519,y:32679,varname:node_9583,prsc:2|A-5106-A,B-6916-OUT;n:type:ShaderForge.SFN_Step,id:6872,x:30914,y:33074,varname:node_6872,prsc:2|A-4439-OUT,B-6763-OUT;n:type:ShaderForge.SFN_Multiply,id:3366,x:31868,y:32992,varname:node_3366,prsc:2|A-9583-OUT,B-5144-OUT;n:type:ShaderForge.SFN_Dot,id:1223,x:31871,y:33746,varname:node_1223,prsc:2,dt:1|A-1963-OUT,B-2523-OUT;n:type:ShaderForge.SFN_NormalVector,id:2523,x:31696,y:33869,prsc:2,pt:True;n:type:ShaderForge.SFN_Power,id:4530,x:32057,y:33746,varname:node_4530,prsc:2|VAL-1223-OUT,EXP-3538-OUT;n:type:ShaderForge.SFN_ViewVector,id:1963,x:31696,y:33746,varname:node_1963,prsc:2;n:type:ShaderForge.SFN_Set,id:4863,x:30719,y:32259,varname:diff,prsc:2|IN-6343-OUT;n:type:ShaderForge.SFN_LightColor,id:8637,x:31522,y:32008,varname:node_8637,prsc:2;n:type:ShaderForge.SFN_LightVector,id:840,x:31344,y:31604,varname:node_840,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:2388,x:31522,y:31886,varname:node_2388,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5073,x:31344,y:31741,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:556,x:31522,y:31604,varname:node_556,prsc:2,dt:1|A-840-OUT,B-5073-OUT;n:type:ShaderForge.SFN_Dot,id:2279,x:31522,y:31741,varname:node_2279,prsc:2,dt:4|A-840-OUT,B-5073-OUT;n:type:ShaderForge.SFN_Multiply,id:771,x:32289,y:31604,varname:node_771,prsc:2|A-6787-OUT,B-6836-OUT,C-2388-OUT,D-8637-RGB;n:type:ShaderForge.SFN_Set,id:7980,x:32464,y:31604,varname:result,prsc:2|IN-771-OUT;n:type:ShaderForge.SFN_Get,id:6787,x:31501,y:31548,varname:node_6787,prsc:2|IN-4863-OUT;n:type:ShaderForge.SFN_Get,id:5421,x:32532,y:32948,varname:node_5421,prsc:2|IN-7980-OUT;n:type:ShaderForge.SFN_RemapRange,id:6786,x:31695,y:31604,varname:node_6786,prsc:2,frmn:0.45,frmx:0.55,tomn:0,tomx:1|IN-556-OUT;n:type:ShaderForge.SFN_Clamp01,id:3293,x:31865,y:31604,varname:node_3293,prsc:2|IN-6786-OUT;n:type:ShaderForge.SFN_Min,id:6836,x:32064,y:31604,varname:node_6836,prsc:2|A-3293-OUT,B-2279-OUT;n:type:ShaderForge.SFN_Code,id:6512,x:32182,y:32396,varname:node_6512,prsc:2,code:cgBlAHQAdQByAG4AIABTAGgAYQBkAGUAUwBIAFAAZQByAFAAaQB4AGUAbAAoAG4AbwByAG0AYQBsACwAIAAwACwAIAB3AG8AcgBsAGQAUABvAHMAKQA7AA==,output:2,fname:Function_node_6512,width:384,height:164,input:2,input:2,input_1_label:normal,input_2_label:worldPos|A-6067-OUT,B-8465-XYZ;n:type:ShaderForge.SFN_NormalVector,id:6067,x:31948,y:32350,prsc:2,pt:True;n:type:ShaderForge.SFN_FragmentPosition,id:8465,x:31948,y:32502,varname:node_8465,prsc:2;proporder:7736-6665-6792-5964-5144-6916-6530;pass:END;sub:END;*/

Shader "RaptorCat/Particles/particle_smoke_cutoff (Custom)" {
    Properties {
        _MainTex ("Heightmask", 2D) = "black" {}
        _Color ("Color", Color) = (1,1,1,1)
        _HeightStrength ("Height Strength", Range(0, 1)) = 1
        _BumpMap ("Normal", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Range(0, 1)) = 1
        _test ("test", Range(0, 1)) = 1
        _DistanceBlend ("Distance Blend", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
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
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _NormalStrength;
            uniform float _HeightStrength;
            uniform float _test;
            uniform float _DistanceBlend;
            float3 Function_node_6512( float3 normal , float3 worldPos ){
            return ShadeSHPerPixel(normal, 0, worldPos);
            }
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
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
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float node_9583 = (i.vertexColor.a*_test);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMap_var.rgb,(node_9583*_NormalStrength));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_3538 = 0.5;
                float node_4187 = saturate(pow(max(0,dot(viewDirection,normalDirection)),node_3538));
                float fresnel = node_4187;
                float clipInput = step(saturate((lerp(0.0,_MainTex_var.r,_HeightStrength)*0.99+0.01)),(node_9583*fresnel*saturate(((1.0/_DistanceBlend)*partZ))));
                clip(clipInput - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float3 emissive = Function_node_6512( normalDirection , i.posWorld.rgb );
                float3 diff = (_Color.rgb*i.vertexColor.rgb);
                float3 result = (diff*min(saturate((max(0,dot(lightDirection,normalDirection))*9.999998+-4.499999)),0.5*dot(lightDirection,normalDirection)+0.5)*attenuation*_LightColor0.rgb);
                float3 finalColor = emissive + result;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
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
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _NormalStrength;
            uniform float _HeightStrength;
            uniform float _test;
            uniform float _DistanceBlend;
            float3 Function_node_6512( float3 normal , float3 worldPos ){
            return ShadeSHPerPixel(normal, 0, worldPos);
            }
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
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
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float node_9583 = (i.vertexColor.a*_test);
                float3 normalLocal = lerp(float3(0,0,1),_BumpMap_var.rgb,(node_9583*_NormalStrength));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_3538 = 0.5;
                float node_4187 = saturate(pow(max(0,dot(viewDirection,normalDirection)),node_3538));
                float fresnel = node_4187;
                float clipInput = step(saturate((lerp(0.0,_MainTex_var.r,_HeightStrength)*0.99+0.01)),(node_9583*fresnel*saturate(((1.0/_DistanceBlend)*partZ))));
                clip(clipInput - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 diff = (_Color.rgb*i.vertexColor.rgb);
                float3 result = (diff*min(saturate((max(0,dot(lightDirection,normalDirection))*9.999998+-4.499999)),0.5*dot(lightDirection,normalDirection)+0.5)*attenuation*_LightColor0.rgb);
                float3 finalColor = result;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _HeightStrength;
            uniform float _test;
            uniform float _DistanceBlend;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_9583 = (i.vertexColor.a*_test);
                float node_3538 = 0.5;
                float node_4187 = saturate(pow(max(0,dot(viewDirection,normalDirection)),node_3538));
                float fresnel = node_4187;
                float clipInput = step(saturate((lerp(0.0,_MainTex_var.r,_HeightStrength)*0.99+0.01)),(node_9583*fresnel*saturate(((1.0/_DistanceBlend)*partZ))));
                clip(clipInput - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
