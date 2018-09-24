// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:14,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-6376-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31727,y:32315,ptovrint:False,ptlb:Inside Color,ptin:_InsideColor,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1219723,c2:0.3529412,c3:0.3529412,c4:1;n:type:ShaderForge.SFN_Tex2d,id:3618,x:31727,y:32661,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_3618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:2440,x:31727,y:32490,ptovrint:False,ptlb:Outside Color,ptin:_OutsideColor,varname:_InsideColor_copy,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3686275,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:4253,x:31915,y:32500,varname:node_4253,prsc:2|A-7241-RGB,B-2440-RGB,T-3618-R;n:type:ShaderForge.SFN_TexCoord,id:2233,x:30279,y:33007,varname:node_2233,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Noise,id:8797,x:31018,y:33007,varname:node_8797,prsc:2|XY-6471-OUT;n:type:ShaderForge.SFN_Posterize,id:2332,x:30682,y:33007,varname:node_2332,prsc:2|IN-653-OUT,STPS-9747-OUT;n:type:ShaderForge.SFN_Vector1,id:9747,x:30492,y:33130,varname:node_9747,prsc:2,v1:32;n:type:ShaderForge.SFN_Add,id:653,x:30492,y:33007,varname:node_653,prsc:2|A-2233-V,B-4868-TSL;n:type:ShaderForge.SFN_Time,id:4868,x:30279,y:33154,varname:node_4868,prsc:2;n:type:ShaderForge.SFN_Set,id:3399,x:31486,y:33041,varname:noise,prsc:2|IN-6189-OUT;n:type:ShaderForge.SFN_TexCoord,id:6849,x:29232,y:32673,varname:node_6849,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9089,x:29411,y:32673,varname:node_9089,prsc:2|A-6849-UVOUT,B-9324-OUT;n:type:ShaderForge.SFN_Vector1,id:9324,x:29232,y:32821,varname:node_9324,prsc:2,v1:32;n:type:ShaderForge.SFN_Frac,id:1592,x:29583,y:32673,varname:node_1592,prsc:2|IN-9089-OUT;n:type:ShaderForge.SFN_Distance,id:3550,x:29937,y:32683,varname:node_3550,prsc:2|A-1236-G,B-7069-OUT;n:type:ShaderForge.SFN_Vector1,id:7069,x:29767,y:32814,varname:node_7069,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Distance,id:9186,x:29937,y:32549,varname:node_9186,prsc:2|A-1236-R,B-7069-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1236,x:29767,y:32673,varname:node_1236,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1592-OUT;n:type:ShaderForge.SFN_RemapRange,id:8148,x:30111,y:32549,varname:node_8148,prsc:2,frmn:0,frmx:0.5,tomn:0,tomx:1|IN-9186-OUT;n:type:ShaderForge.SFN_RemapRange,id:6643,x:30111,y:32718,varname:node_6643,prsc:2,frmn:0,frmx:0.5,tomn:0,tomx:1|IN-3550-OUT;n:type:ShaderForge.SFN_Clamp01,id:4465,x:30290,y:32549,varname:node_4465,prsc:2|IN-8148-OUT;n:type:ShaderForge.SFN_Clamp01,id:3629,x:30290,y:32718,varname:node_3629,prsc:2|IN-6643-OUT;n:type:ShaderForge.SFN_Step,id:9664,x:30463,y:32549,varname:node_9664,prsc:2|A-5235-OUT,B-4465-OUT;n:type:ShaderForge.SFN_Step,id:4657,x:30463,y:32718,varname:node_4657,prsc:2|A-5235-OUT,B-3629-OUT;n:type:ShaderForge.SFN_Vector1,id:5235,x:30279,y:32846,varname:node_5235,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Vector1,id:9709,x:30639,y:32816,varname:node_9709,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Blend,id:6189,x:31320,y:33041,varname:node_6189,prsc:2,blmd:10,clmp:True|SRC-9674-OUT,DST-8797-OUT;n:type:ShaderForge.SFN_Max,id:9674,x:30639,y:32688,varname:node_9674,prsc:2|A-9664-OUT,B-4657-OUT;n:type:ShaderForge.SFN_Get,id:1684,x:31902,y:32648,varname:node_1684,prsc:2|IN-3399-OUT;n:type:ShaderForge.SFN_Posterize,id:9784,x:30682,y:33158,varname:node_9784,prsc:2|IN-2233-U,STPS-9747-OUT;n:type:ShaderForge.SFN_Append,id:6471,x:30851,y:33007,varname:node_6471,prsc:2|A-9784-OUT,B-2332-OUT;n:type:ShaderForge.SFN_Multiply,id:6376,x:32085,y:32500,varname:node_6376,prsc:2|A-4253-OUT,B-1963-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:4263,x:31923,y:32704,varname:node_4263,prsc:2,a:0,b:0.1|IN-3618-R;n:type:ShaderForge.SFN_Max,id:1963,x:32085,y:32648,varname:node_1963,prsc:2|A-1684-OUT,B-4263-OUT,C-5776-OUT;n:type:ShaderForge.SFN_TexCoord,id:8576,x:31172,y:33489,varname:node_8576,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1055,x:31354,y:33489,varname:node_1055,prsc:2|A-8576-U,B-4861-OUT;n:type:ShaderForge.SFN_Pi,id:4861,x:31205,y:33642,varname:node_4861,prsc:2;n:type:ShaderForge.SFN_Sin,id:6083,x:31523,y:33489,varname:node_6083,prsc:2|IN-1055-OUT;n:type:ShaderForge.SFN_RemapRange,id:4354,x:31696,y:33489,varname:node_4354,prsc:2,frmn:0.45,frmx:0.6,tomn:0,tomx:1|IN-6083-OUT;n:type:ShaderForge.SFN_Clamp01,id:4208,x:31868,y:33489,varname:node_4208,prsc:2|IN-4354-OUT;n:type:ShaderForge.SFN_Set,id:543,x:32041,y:33489,varname:sideholes,prsc:2|IN-4208-OUT;n:type:ShaderForge.SFN_Get,id:6780,x:31745,y:32864,varname:node_6780,prsc:2|IN-543-OUT;n:type:ShaderForge.SFN_OneMinus,id:5776,x:31923,y:32864,varname:node_5776,prsc:2|IN-6780-OUT;proporder:3618-7241-2440;pass:END;sub:END;*/

Shader "RaptorCat/Character/ShieldShader" {
    Properties {
        _Emissive ("Emissive", 2D) = "white" {}
        [HDR]_InsideColor ("Inside Color", Color) = (0.1219723,0.3529412,0.3529412,1)
        [HDR]_OutsideColor ("Outside Color", Color) = (0.3686275,1,1,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _InsideColor;
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform float4 _OutsideColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Emissive_var = tex2D(_Emissive,TRANSFORM_TEX(i.uv0, _Emissive));
                float node_5235 = 0.8;
                float2 node_1236 = frac((i.uv1*32.0)).rg;
                float node_7069 = 0.5;
                float node_9747 = 32.0;
                float4 node_4868 = _Time;
                float2 node_6471 = float2(floor(i.uv1.r * node_9747) / (node_9747 - 1),floor((i.uv1.g+node_4868.r) * node_9747) / (node_9747 - 1));
                float2 node_8797_skew = node_6471 + 0.2127+node_6471.x*0.3713*node_6471.y;
                float2 node_8797_rnd = 4.789*sin(489.123*(node_8797_skew));
                float node_8797 = frac(node_8797_rnd.x*node_8797_rnd.y*(1+node_8797_skew.x));
                float noise = saturate(( node_8797 > 0.5 ? (1.0-(1.0-2.0*(node_8797-0.5))*(1.0-max(step(node_5235,saturate((distance(node_1236.r,node_7069)*2.0+0.0))),step(node_5235,saturate((distance(node_1236.g,node_7069)*2.0+0.0)))))) : (2.0*node_8797*max(step(node_5235,saturate((distance(node_1236.r,node_7069)*2.0+0.0))),step(node_5235,saturate((distance(node_1236.g,node_7069)*2.0+0.0))))) ));
                float sideholes = saturate((sin((i.uv1.r*3.141592654))*6.666665+-2.999999));
                float3 emissive = (lerp(_InsideColor.rgb,_OutsideColor.rgb,_Emissive_var.r)*max(max(noise,lerp(0,0.1,_Emissive_var.r)),(1.0 - sideholes)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
