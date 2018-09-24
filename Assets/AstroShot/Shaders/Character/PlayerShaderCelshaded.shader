// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:14,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|normal-5964-RGB,emission-2244-OUT,difocc-6538-OUT,spcocc-4231-B,custl-2070-OUT;n:type:ShaderForge.SFN_Color,id:6665,x:30464,y:31899,ptovrint:False,ptlb:Primary,ptin:_Primary,varname:_Primary,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.035,c2:0.03529412,c3:0.3098039,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:30464,y:31657,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_Mask,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:77d6adac9647da54a9ae2f8148fcbcf7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32101,y:32937,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e5d493af5e5d0764faec1a160fce8454,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Color,id:5348,x:30464,y:32110,ptovrint:False,ptlb:Secondary,ptin:_Secondary,varname:_Secondary,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8308824,c2:0.8308824,c3:0.8308824,c4:1;n:type:ShaderForge.SFN_Color,id:5547,x:30464,y:32331,ptovrint:False,ptlb:Third,ptin:_Third,varname:_Third,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4705882,c2:0.4137931,c3:0.3529412,c4:1;n:type:ShaderForge.SFN_Vector3,id:6435,x:30819,y:32048,varname:node_6435,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_ChannelBlend,id:2239,x:30819,y:31854,varname:node_2239,prsc:2,chbt:1|M-7736-RGB,R-5348-RGB,G-5547-RGB,B-6435-OUT,BTM-6665-RGB;n:type:ShaderForge.SFN_VertexColor,id:8432,x:30988,y:32048,varname:node_8432,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4544,x:31185,y:31857,varname:node_4544,prsc:2|A-4751-RGB,B-2239-OUT,C-8432-RGB;n:type:ShaderForge.SFN_Tex2d,id:4751,x:30819,y:31644,ptovrint:True,ptlb:Diffuse,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2350,x:31933,y:32220,varname:node_2350,prsc:2|A-8212-RGB,B-7008-B,C-6333-OUT,D-258-OUT;n:type:ShaderForge.SFN_Vector1,id:1557,x:32430,y:32997,varname:node_1557,prsc:0,v1:0;n:type:ShaderForge.SFN_Tex2d,id:8212,x:31701,y:32107,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:_Specular,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2208,x:32661,y:32157,ptovrint:False,ptlb:Sharpness,ptin:_Sharpness,varname:_Sharpness,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7616,x:31780,y:34655,varname:node_7616,prsc:2|A-730-OUT,B-3278-OUT;n:type:ShaderForge.SFN_Lerp,id:8596,x:31516,y:32335,varname:node_8596,prsc:2|A-2239-OUT,B-1588-OUT,T-4149-OUT;n:type:ShaderForge.SFN_Vector1,id:1588,x:31331,y:32352,varname:node_1588,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:4149,x:31331,y:32409,varname:node_4149,prsc:2,v1:0.33;n:type:ShaderForge.SFN_ChannelBlend,id:7549,x:30815,y:32612,varname:node_7549,prsc:2,chbt:1|M-7736-RGB,R-8726-RGB,G-420-RGB,B-7173-OUT,BTM-4540-RGB;n:type:ShaderForge.SFN_Color,id:4540,x:30464,y:32570,ptovrint:False,ptlb:Rim Primary,ptin:_RimPrimary,varname:_RimPrimary,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:8726,x:30464,y:32779,ptovrint:False,ptlb:Rim Secondary,ptin:_RimSecondary,varname:_RimSecondary,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:420,x:30464,y:32997,ptovrint:False,ptlb:Rim Third,ptin:_RimThird,varname:_RimThird,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:7173,x:30815,y:32819,varname:node_7173,prsc:2,v1:0;n:type:ShaderForge.SFN_ChannelBlend,id:7823,x:30815,y:32923,varname:node_7823,prsc:2,chbt:1|M-7736-RGB,R-8726-A,G-420-A,B-7173-OUT,BTM-4540-A;n:type:ShaderForge.SFN_VertexColor,id:4231,x:32160,y:33244,varname:node_4231,prsc:2;n:type:ShaderForge.SFN_VertexColor,id:7008,x:31701,y:32317,varname:node_7008,prsc:2;n:type:ShaderForge.SFN_Set,id:6490,x:31351,y:31857,varname:diff,prsc:2|IN-4544-OUT;n:type:ShaderForge.SFN_Set,id:8397,x:30990,y:32612,varname:rimCol,prsc:2|IN-7549-OUT;n:type:ShaderForge.SFN_Set,id:905,x:30996,y:32923,varname:rimPinch,prsc:2|IN-7823-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:8569,x:30815,y:32165,varname:node_8569,prsc:2,chbt:1|M-7736-RGB,R-5348-A,G-5547-A,B-4970-OUT,BTM-6665-A;n:type:ShaderForge.SFN_Vector1,id:4970,x:30815,y:32319,varname:node_4970,prsc:2,v1:0;n:type:ShaderForge.SFN_Set,id:9899,x:30988,y:32165,varname:rimAmount,prsc:2|IN-8569-OUT;n:type:ShaderForge.SFN_Get,id:4945,x:29963,y:33590,varname:node_4945,prsc:2|IN-905-OUT;n:type:ShaderForge.SFN_Set,id:2999,x:31484,y:33799,varname:rimFade,prsc:0|IN-8514-OUT;n:type:ShaderForge.SFN_Get,id:3278,x:31589,y:34644,varname:node_3278,prsc:2|IN-2999-OUT;n:type:ShaderForge.SFN_Get,id:7976,x:31495,y:32510,varname:node_7976,prsc:0|IN-2999-OUT;n:type:ShaderForge.SFN_Get,id:5996,x:31495,y:32460,varname:node_5996,prsc:0|IN-8397-OUT;n:type:ShaderForge.SFN_Get,id:730,x:31589,y:34586,varname:node_730,prsc:2|IN-8397-OUT;n:type:ShaderForge.SFN_Get,id:3870,x:31134,y:33925,varname:node_3870,prsc:2|IN-9899-OUT;n:type:ShaderForge.SFN_Multiply,id:8514,x:31323,y:33799,varname:node_8514,prsc:2|A-8427-OUT,B-3870-OUT,C-6869-OUT,D-1046-OUT;n:type:ShaderForge.SFN_Lerp,id:6333,x:31701,y:32441,varname:node_6333,prsc:2|A-8596-OUT,B-5996-OUT,T-7976-OUT;n:type:ShaderForge.SFN_Clamp01,id:8427,x:31155,y:33799,varname:node_8427,prsc:2|IN-8485-OUT;n:type:ShaderForge.SFN_Tex2d,id:5955,x:34504,y:32498,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:_Emissive,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:47f310a10d4f44d42bce1e406bd131bd,ntxv:2,isnm:False;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:8485,x:30985,y:33799,varname:node_8485,prsc:2|IN-9889-OUT,IMIN-4505-OUT,IMAX-1566-OUT,OMIN-8265-OUT,OMAX-4270-OUT;n:type:ShaderForge.SFN_Vector1,id:4505,x:30703,y:33762,varname:node_4505,prsc:0,v1:0;n:type:ShaderForge.SFN_Vector1,id:1566,x:30703,y:33820,varname:node_1566,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:9889,x:30383,y:33489,varname:node_9889,prsc:2|A-3320-OUT,B-9834-OUT;n:type:ShaderForge.SFN_Vector1,id:8265,x:30703,y:33878,varname:node_8265,prsc:0,v1:-7;n:type:ShaderForge.SFN_Vector1,id:4270,x:30703,y:33933,varname:node_4270,prsc:0,v1:6;n:type:ShaderForge.SFN_Vector1,id:2067,x:29984,y:33465,varname:node_2067,prsc:0,v1:-0.25;n:type:ShaderForge.SFN_Vector1,id:5436,x:29984,y:33523,varname:node_5436,prsc:0,v1:0.25;n:type:ShaderForge.SFN_Lerp,id:9834,x:30194,y:33489,varname:node_9834,prsc:2|A-2067-OUT,B-5436-OUT,T-4945-OUT;n:type:ShaderForge.SFN_NormalVector,id:6566,x:29255,y:33443,prsc:2,pt:True;n:type:ShaderForge.SFN_ViewVector,id:5979,x:29255,y:33312,varname:node_5979,prsc:2;n:type:ShaderForge.SFN_Dot,id:142,x:29458,y:33312,varname:node_142,prsc:2,dt:1|A-5979-OUT,B-6566-OUT;n:type:ShaderForge.SFN_OneMinus,id:3320,x:30194,y:33356,varname:node_3320,prsc:2|IN-142-OUT;n:type:ShaderForge.SFN_Dot,id:7632,x:30950,y:34106,varname:node_7632,prsc:2,dt:4|A-6369-OUT,B-2772-OUT;n:type:ShaderForge.SFN_Vector3,id:2772,x:30747,y:34176,varname:node_2772,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_NormalVector,id:6369,x:30747,y:34018,prsc:2,pt:True;n:type:ShaderForge.SFN_OneMinus,id:6869,x:31431,y:34106,varname:node_6869,prsc:2|IN-1046-OUT;n:type:ShaderForge.SFN_Clamp01,id:1046,x:31271,y:34106,varname:node_1046,prsc:2|IN-1471-OUT;n:type:ShaderForge.SFN_Set,id:1062,x:31431,y:34056,varname:topDownShade,prsc:2|IN-1046-OUT;n:type:ShaderForge.SFN_Multiply,id:6538,x:32361,y:33244,varname:node_6538,prsc:2|A-3105-OUT,B-4231-B;n:type:ShaderForge.SFN_Get,id:3105,x:32139,y:33187,varname:node_3105,prsc:2|IN-1062-OUT;n:type:ShaderForge.SFN_Multiply,id:5642,x:32849,y:32157,varname:node_5642,prsc:2|A-2208-R,B-4598-OUT;n:type:ShaderForge.SFN_Slider,id:4598,x:32504,y:32335,ptovrint:False,ptlb:Sharpness Adjust,ptin:_SharpnessAdjust,varname:node_4598,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:258,x:31544,y:32579,ptovrint:False,ptlb:Specular Adjust,ptin:_SpecularAdjust,varname:node_258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Set,id:1667,x:32114,y:32220,varname:spec,prsc:2|IN-2350-OUT;n:type:ShaderForge.SFN_Set,id:6246,x:33009,y:32157,varname:gloss,prsc:2|IN-5642-OUT;n:type:ShaderForge.SFN_Set,id:7605,x:31951,y:34604,varname:rimResult,prsc:2|IN-7616-OUT;n:type:ShaderForge.SFN_Set,id:9751,x:32534,y:33244,varname:topdownAO,prsc:2|IN-6538-OUT;n:type:ShaderForge.SFN_Set,id:8149,x:32361,y:33370,varname:vcolAO,prsc:2|IN-4231-B;n:type:ShaderForge.SFN_LightAttenuation,id:2572,x:33068,y:34381,varname:node_2572,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:7734,x:32855,y:34221,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:3675,x:32855,y:34381,varname:node_3675,prsc:2;n:type:ShaderForge.SFN_Dot,id:9232,x:33068,y:34221,varname:node_9232,prsc:2,dt:1|A-7734-OUT,B-3675-OUT;n:type:ShaderForge.SFN_LightColor,id:497,x:33408,y:34533,varname:node_497,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3374,x:34064,y:34420,varname:node_3374,prsc:2|A-8443-OUT,B-4427-OUT,C-8799-OUT,D-2572-OUT;n:type:ShaderForge.SFN_Set,id:6587,x:35911,y:34204,varname:result,prsc:2|IN-159-OUT;n:type:ShaderForge.SFN_Get,id:2070,x:32409,y:32940,varname:node_2070,prsc:2|IN-6587-OUT;n:type:ShaderForge.SFN_Step,id:8163,x:33477,y:34221,varname:node_8163,prsc:2|A-9127-OUT,B-96-OUT;n:type:ShaderForge.SFN_Vector1,id:9127,x:33262,y:34381,varname:node_9127,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:5297,x:34429,y:34420,varname:node_5297,prsc:2|A-3821-OUT,B-3374-OUT;n:type:ShaderForge.SFN_Get,id:3821,x:34238,y:34366,varname:node_3821,prsc:2|IN-6490-OUT;n:type:ShaderForge.SFN_Clamp01,id:9428,x:33408,y:34661,varname:node_9428,prsc:2|IN-497-RGB;n:type:ShaderForge.SFN_Multiply,id:96,x:33262,y:34221,varname:node_96,prsc:2|A-9232-OUT,B-2572-OUT;n:type:ShaderForge.SFN_Get,id:2536,x:34615,y:33195,varname:node_2536,prsc:2|IN-7605-OUT;n:type:ShaderForge.SFN_HalfVector,id:4564,x:32855,y:34063,varname:node_4564,prsc:2;n:type:ShaderForge.SFN_Dot,id:9121,x:33068,y:34063,varname:node_9121,prsc:2,dt:1|A-4564-OUT,B-7734-OUT;n:type:ShaderForge.SFN_Multiply,id:9134,x:34299,y:34052,varname:node_9134,prsc:2|A-7023-OUT,B-8367-OUT,C-7953-OUT,D-497-RGB;n:type:ShaderForge.SFN_Get,id:7023,x:33621,y:33735,varname:node_7023,prsc:2|IN-1667-OUT;n:type:ShaderForge.SFN_Get,id:3539,x:34043,y:34366,varname:node_3539,prsc:2|IN-9751-OUT;n:type:ShaderForge.SFN_Clamp01,id:8367,x:33849,y:33870,varname:node_8367,prsc:2|IN-8418-OUT;n:type:ShaderForge.SFN_Fresnel,id:6253,x:32870,y:34793,varname:node_6253,prsc:2;n:type:ShaderForge.SFN_Lerp,id:4427,x:33674,y:34627,varname:node_4427,prsc:2|A-9428-OUT,B-497-RGB,T-2128-OUT;n:type:ShaderForge.SFN_RemapRange,id:4173,x:33043,y:34793,varname:node_4173,prsc:2,frmn:0.1,frmx:0.8,tomn:0,tomx:1|IN-6253-OUT;n:type:ShaderForge.SFN_Clamp01,id:23,x:33232,y:34793,varname:node_23,prsc:2|IN-4173-OUT;n:type:ShaderForge.SFN_OneMinus,id:2128,x:33408,y:34793,varname:node_2128,prsc:2|IN-23-OUT;n:type:ShaderForge.SFN_Code,id:7556,x:33862,y:33047,varname:node_7556,prsc:2,code:ZgBsAG8AYQB0ADMAIABhAG0AYgBpAGUAbgB0ACAAPQAgAFMAaABhAGQAZQBTAEgAOQAoACAAZgBsAG8AYQB0ADQAKAAgAG4AbwByAG0AYQBsACwAIAAxAC4AMAAgACkAIAApAC4AcgBnAGIAOwAKAAoAcgBlAHQAdQByAG4AIABhAG0AYgBpAGUAbgB0ADsA,output:2,fname:LightProbeGather,width:438,height:134,input:2,input_1_label:normal|A-466-OUT;n:type:ShaderForge.SFN_NormalVector,id:466,x:33642,y:33072,prsc:2,pt:True;n:type:ShaderForge.SFN_Code,id:2403,x:33442,y:32763,varname:node_2403,prsc:2,code:ZgBsAG8AYQB0ADQAIABjAHUAYgBlACAAPQAgAFUATgBJAFQAWQBfAFMAQQBNAFAATABFAF8AVABFAFgAQwBVAEIARQBfAEwATwBEACgAdQBuAGkAdAB5AF8AUwBwAGUAYwBDAHUAYgBlADAALAAgAG4AbwByAG0AYQBsACwAIAAoADEAIAAtACAAZwBsAG8AcwBzACkAIAAqACAANgApADsADQAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAADQAKAAoALwAvAHIAZQB0AHUAcgBuACAAYwB1AGIAZQA7AAoAcgBlAHQAdQByAG4AIABEAGUAYwBvAGQAZQBIAEQAUgAoAGMAdQBiAGUALAAgAHUAbgBpAHQAeQBfAFMAcABlAGMAQwB1AGIAZQAwAF8ASABEAFIAKQA7AA==,output:2,fname:CubeGlossyGather,width:620,height:130,input:2,input:8,input:10,input_1_label:normal,input_2_label:gloss,input_3_label:spec|A-1192-OUT,B-6881-OUT,C-9128-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:1192,x:33240,y:32727,varname:node_1192,prsc:2;n:type:ShaderForge.SFN_Code,id:6409,x:33419,y:35478,varname:node_6409,prsc:2,code:ZgBsAG8AYQB0ADQAIABjAHUAYgBlACAAPQAgAFUATgBJAFQAWQBfAFMAQQBNAFAATABFAF8AVABFAFgAQwBVAEIARQBfAEwATwBEACgAdQBuAGkAdAB5AF8AUwBwAGUAYwBDAHUAYgBlADAALAAgAG4AbwByAG0AYQBsACwAIAA1ACkAOwANACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAANAAoACgByAGUAdAB1AHIAbgAgAGMAdQBiAGUAOwAKAC8ALwByAGUAdAB1AHIAbgAgAEQAZQBjAG8AZABlAEgARABSACgAYwB1AGIAZQAsACAAdQBuAGkAdAB5AF8AUwBwAGUAYwBDAHUAYgBlADAAXwBIAEQAUgApADsA,output:2,fname:CubeAmbientGather,width:620,height:130,input:2,input_1_label:normal|A-9791-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:9791,x:33199,y:35533,varname:node_9791,prsc:2;n:type:ShaderForge.SFN_Get,id:6881,x:33219,y:32860,varname:node_6881,prsc:2|IN-6246-OUT;n:type:ShaderForge.SFN_Get,id:9128,x:33219,y:32913,varname:node_9128,prsc:2|IN-1667-OUT;n:type:ShaderForge.SFN_Clamp01,id:396,x:34975,y:33195,varname:node_396,prsc:2|IN-2301-OUT;n:type:ShaderForge.SFN_RemapRange,id:1471,x:31106,y:34106,varname:node_1471,prsc:2,frmn:-0.1,frmx:0.6,tomn:0,tomx:1|IN-7632-OUT;n:type:ShaderForge.SFN_OneMinus,id:9831,x:34975,y:33039,varname:node_9831,prsc:2|IN-5994-VOUT;n:type:ShaderForge.SFN_Multiply,id:2301,x:34805,y:33195,varname:node_2301,prsc:2|A-2536-OUT,B-9831-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:5994,x:34805,y:33039,varname:node_5994,prsc:2|IN-7556-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:1584,x:33473,y:33870,varname:node_1584,prsc:2|IN-9121-OUT,IMIN-7965-OUT,IMAX-9058-OUT,OMIN-8121-OUT,OMAX-6952-OUT;n:type:ShaderForge.SFN_Vector1,id:6952,x:33262,y:33976,varname:node_6952,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:8121,x:33262,y:33918,varname:node_8121,prsc:2,v1:0;n:type:ShaderForge.SFN_Get,id:6097,x:32649,y:33942,varname:node_6097,prsc:2|IN-6246-OUT;n:type:ShaderForge.SFN_Lerp,id:7965,x:33262,y:33654,varname:node_7965,prsc:2|A-4824-OUT,B-8052-OUT,T-3602-OUT;n:type:ShaderForge.SFN_Vector1,id:4824,x:32962,y:33671,varname:node_4824,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8052,x:32962,y:33729,varname:node_8052,prsc:2,v1:0.95;n:type:ShaderForge.SFN_Vector1,id:1788,x:32962,y:33796,varname:node_1788,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:899,x:34173,y:32695,varname:node_899,prsc:2|A-2012-OUT,B-2403-OUT,T-5245-OUT;n:type:ShaderForge.SFN_Get,id:5245,x:33911,y:32695,varname:node_5245,prsc:2|IN-1667-OUT;n:type:ShaderForge.SFN_RemapRange,id:852,x:33477,y:34343,varname:node_852,prsc:2,frmn:0,frmx:0.025,tomn:0,tomx:1|IN-96-OUT;n:type:ShaderForge.SFN_Clamp01,id:8443,x:33643,y:34343,varname:node_8443,prsc:2|IN-852-OUT;n:type:ShaderForge.SFN_Lerp,id:9058,x:33262,y:33783,varname:node_9058,prsc:2|A-1788-OUT,B-441-OUT,T-3602-OUT;n:type:ShaderForge.SFN_Vector1,id:441,x:32962,y:33858,varname:node_441,prsc:2,v1:0.95;n:type:ShaderForge.SFN_Power,id:3602,x:32855,y:33942,varname:node_3602,prsc:2|VAL-6097-OUT,EXP-8372-OUT;n:type:ShaderForge.SFN_Vector1,id:8372,x:32670,y:33997,varname:node_8372,prsc:2,v1:0.2;n:type:ShaderForge.SFN_RemapRange,id:8418,x:33660,y:33870,varname:node_8418,prsc:2,frmn:0.47,frmx:0.53,tomn:0,tomx:1|IN-1584-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:2821,x:35196,y:33487,ptovrint:False,ptlb:DebugReflect,ptin:_DebugReflect,varname:node_2821,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_ToggleProperty,id:7090,x:35196,y:33566,ptovrint:False,ptlb:DebugAmbient,ptin:_DebugAmbient,varname:_DebugReflect_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_ToggleProperty,id:5040,x:35196,y:33640,ptovrint:False,ptlb:DebugDirect,ptin:_DebugDirect,varname:_DebugAmbient_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Code,id:9210,x:35447,y:33493,varname:node_9210,prsc:2,code:aQBmACgAZABEAGUAYgB1AGcAIAAhAD0AIAAwACkACgB7AAoAaQBmACgAZABSAGUAZgBsACAAIQA9ACAAMAApACAAcgBlAHQAdQByAG4AIAByAGUAZgBsADsACgBlAGwAcwBlACAAaQBmACgAZABBAG0AYgAgACEAPQAgADAAKQAgAHIAZQB0AHUAcgBuACAAYQBtAGIAOwAKAGUAbABzAGUAIABpAGYAKABkAEQAaQByACAAIQA9ACAAMAApACAAcgBlAHQAdQByAG4AIABkAGkAcgA7AAoAZQBsAHMAZQAgAHIAZQB0AHUAcgBuACAAZgBpAG4AYQBsADsACgB9AAoAZQBsAHMAZQAgAHIAZQB0AHUAcgBuACAAZgBpAG4AYQBsADsA,output:2,fname:DebuggerOutput,width:420,height:208,input:2,input:8,input:8,input:8,input:8,input:2,input:2,input:2,input_1_label:final,input_2_label:dRefl,input_3_label:dAmb,input_4_label:dDir,input_5_label:dDebug,input_6_label:refl,input_7_label:amb,input_8_label:dir|A-6256-OUT,B-2821-OUT,C-7090-OUT,D-5040-OUT,E-7996-OUT,F-2403-OUT,G-7556-OUT,H-3374-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:7996,x:35196,y:33713,ptovrint:False,ptlb:Debug,ptin:_Debug,varname:_DebugDirect_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Vector1,id:8799,x:33643,y:34467,varname:node_8799,prsc:2,v1:0.51;n:type:ShaderForge.SFN_SwitchProperty,id:7566,x:34504,y:32694,ptovrint:False,ptlb:UseReflections,ptin:_UseReflections,varname:node_7566,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-2012-OUT,B-8484-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:7953,x:33849,y:34006,varname:node_7953,prsc:2,a:0.05,b:1|IN-1508-OUT;n:type:ShaderForge.SFN_Multiply,id:1508,x:33660,y:34032,varname:node_1508,prsc:2|A-2572-OUT,B-8443-OUT;n:type:ShaderForge.SFN_Add,id:1260,x:34651,y:34241,varname:node_1260,prsc:2|A-9134-OUT,B-5297-OUT;n:type:ShaderForge.SFN_Power,id:4732,x:33849,y:33735,varname:node_4732,prsc:2|VAL-7023-OUT,EXP-4592-OUT;n:type:ShaderForge.SFN_Vector1,id:4592,x:33642,y:33794,varname:node_4592,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Min,id:3535,x:33262,y:34063,varname:node_3535,prsc:2|A-9121-OUT,B-7953-OUT;n:type:ShaderForge.SFN_Add,id:1751,x:34715,y:32602,varname:node_1751,prsc:2|A-5955-RGB,B-7566-OUT;n:type:ShaderForge.SFN_Vector1,id:2012,x:33932,y:32623,varname:node_2012,prsc:2,v1:0;n:type:ShaderForge.SFN_Set,id:9507,x:36002,y:33152,varname:emis,prsc:2|IN-9210-OUT;n:type:ShaderForge.SFN_Get,id:2244,x:32535,y:32821,varname:node_2244,prsc:2|IN-9507-OUT;n:type:ShaderForge.SFN_Max,id:4442,x:35221,y:32886,varname:node_4442,prsc:2|A-1751-OUT,B-396-OUT;n:type:ShaderForge.SFN_Max,id:2673,x:34952,y:34012,varname:node_2673,prsc:2|A-2821-OUT,B-7090-OUT;n:type:ShaderForge.SFN_Max,id:6679,x:35132,y:34080,varname:node_6679,prsc:2|A-2673-OUT,B-5040-OUT;n:type:ShaderForge.SFN_Min,id:594,x:35303,y:34042,varname:node_594,prsc:2|A-7996-OUT,B-6679-OUT;n:type:ShaderForge.SFN_OneMinus,id:719,x:35481,y:34042,varname:node_719,prsc:2|IN-594-OUT;n:type:ShaderForge.SFN_Multiply,id:159,x:35730,y:34204,varname:node_159,prsc:2|A-719-OUT,B-1260-OUT;n:type:ShaderForge.SFN_Multiply,id:655,x:34805,y:32905,varname:node_655,prsc:2|A-1512-OUT,B-7556-OUT;n:type:ShaderForge.SFN_Get,id:1512,x:34599,y:32951,varname:node_1512,prsc:2|IN-6490-OUT;n:type:ShaderForge.SFN_Add,id:6256,x:35410,y:32931,varname:node_6256,prsc:2|A-4442-OUT,B-655-OUT;n:type:ShaderForge.SFN_Multiply,id:8484,x:34335,y:32694,varname:node_8484,prsc:2|A-899-OUT,B-7556-OUT,C-9720-OUT;n:type:ShaderForge.SFN_Vector1,id:9720,x:34173,y:32828,varname:node_9720,prsc:2,v1:2;proporder:7736-4751-8212-7566-258-2208-4598-5964-5955-6665-5348-5547-4540-8726-420-7996-2821-7090-5040;pass:END;sub:END;*/

Shader "RaptorCat/Character/PlayerShaderCelshaded" {
    Properties {
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
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
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
            float3 LightProbeGather( float3 normal ){
            float3 ambient = ShadeSH9( float4( normal, 1.0 ) ).rgb;
            
            return ambient;
            }
            
            float3 CubeGlossyGather( float3 normal , fixed gloss , fixed3 spec ){
            float4 cube = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, normal, (1 - gloss) * 6);              
            
            //return cube;
            return DecodeHDR(cube, unity_SpecCube0_HDR);
            }
            
            uniform fixed _DebugReflect;
            uniform fixed _DebugAmbient;
            uniform fixed _DebugDirect;
            float3 DebuggerOutput( float3 final , fixed dRefl , fixed dAmb , fixed dDir , fixed dDebug , float3 refl , float3 amb , float3 dir ){
            if(dDebug != 0)
            {
            if(dRefl != 0) return refl;
            else if(dAmb != 0) return amb;
            else if(dDir != 0) return dir;
            else return final;
            }
            else return final;
            }
            
            uniform fixed _Debug;
            uniform fixed _UseReflections;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
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
                float4 _Emissive_var = tex2D(_Emissive,TRANSFORM_TEX(i.uv0, _Emissive));
                float node_2012 = 0.0;
                fixed4 _Sharpness_var = tex2D(_Sharpness,TRANSFORM_TEX(i.uv0, _Sharpness));
                float gloss = (_Sharpness_var.r*_SharpnessAdjust);
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 node_2239 = (lerp( lerp( lerp( _Primary.rgb, _Secondary.rgb, _Mask_var.rgb.r ), _Third.rgb, _Mask_var.rgb.g ), float3(1,1,1), _Mask_var.rgb.b ));
                float node_1588 = 1.0;
                float node_7173 = 0.0;
                float3 rimCol = (lerp( lerp( lerp( _RimPrimary.rgb, _RimSecondary.rgb, _Mask_var.rgb.r ), _RimThird.rgb, _Mask_var.rgb.g ), node_7173, _Mask_var.rgb.b ));
                float rimPinch = (lerp( lerp( lerp( _RimPrimary.a, _RimSecondary.a, _Mask_var.rgb.r ), _RimThird.a, _Mask_var.rgb.g ), node_7173, _Mask_var.rgb.b ));
                fixed node_4505 = 0.0;
                fixed node_8265 = (-7.0);
                float rimAmount = (lerp( lerp( lerp( _Primary.a, _Secondary.a, _Mask_var.rgb.r ), _Third.a, _Mask_var.rgb.g ), 0.0, _Mask_var.rgb.b ));
                float node_1046 = saturate((0.5*dot(normalDirection,float3(0,1,0))+0.5*1.428571+0.1428571));
                fixed rimFade = (saturate((node_8265 + ( (((1.0 - max(0,dot(viewDirection,normalDirection)))+lerp((-0.25),0.25,rimPinch)) - node_4505) * (6.0 - node_8265) ) / (1.0 - node_4505)))*rimAmount*(1.0 - node_1046)*node_1046);
                float3 spec = (_Specular_var.rgb*i.vertexColor.b*lerp(lerp(node_2239,float3(node_1588,node_1588,node_1588),0.33),rimCol,rimFade)*_SpecularAdjust);
                float3 node_2403 = CubeGlossyGather( viewReflectDirection , gloss , spec );
                float3 node_7556 = LightProbeGather( normalDirection );
                float3 rimResult = (rimCol*rimFade);
                float4 node_5994_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_5994_p = lerp(float4(float4(node_7556,0.0).zy, node_5994_k.wz), float4(float4(node_7556,0.0).yz, node_5994_k.xy), step(float4(node_7556,0.0).z, float4(node_7556,0.0).y));
                float4 node_5994_q = lerp(float4(node_5994_p.xyw, float4(node_7556,0.0).x), float4(float4(node_7556,0.0).x, node_5994_p.yzx), step(node_5994_p.x, float4(node_7556,0.0).x));
                float node_5994_d = node_5994_q.x - min(node_5994_q.w, node_5994_q.y);
                float node_5994_e = 1.0e-10;
                float3 node_5994 = float3(abs(node_5994_q.z + (node_5994_q.w - node_5994_q.y) / (6.0 * node_5994_d + node_5994_e)), node_5994_d / (node_5994_q.x + node_5994_e), node_5994_q.x);;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diff = (_MainTex_var.rgb*node_2239*i.vertexColor.rgb);
                float node_96 = (max(0,dot(normalDirection,lightDirection))*attenuation);
                float node_8443 = saturate((node_96*40.0+0.0));
                float3 node_3374 = (node_8443*lerp(saturate(_LightColor0.rgb),_LightColor0.rgb,(1.0 - saturate(((1.0-max(0,dot(normalDirection, viewDirection)))*1.428571+-0.1428571))))*0.51*attenuation);
                float3 emis = DebuggerOutput( (max((_Emissive_var.rgb+lerp( node_2012, (lerp(float3(node_2012,node_2012,node_2012),node_2403,spec)*node_7556*2.0), _UseReflections )),saturate((rimResult*(1.0 - node_5994.b))))+(diff*node_7556)) , _DebugReflect , _DebugAmbient , _DebugDirect , _Debug , node_2403 , node_7556 , node_3374 );
                float3 emissive = emis;
                float3 node_7023 = spec;
                float node_9121 = max(0,dot(halfDirection,normalDirection));
                float node_3602 = pow(gloss,0.2);
                float node_7965 = lerp(0.0,0.95,node_3602);
                float node_8121 = 0.0;
                float node_7953 = lerp(0.05,1,(attenuation*node_8443));
                float3 result = ((1.0 - min(_Debug,max(max(_DebugReflect,_DebugAmbient),_DebugDirect)))*((node_7023*saturate(((node_8121 + ( (node_9121 - node_7965) * (1.0 - node_8121) ) / (lerp(1.0,0.95,node_3602) - node_7965))*16.66667+-7.833337))*node_7953*_LightColor0.rgb)+(diff*node_3374)));
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
            
            ColorMask RGB
            
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
            float3 LightProbeGather( float3 normal ){
            float3 ambient = ShadeSH9( float4( normal, 1.0 ) ).rgb;
            
            return ambient;
            }
            
            float3 CubeGlossyGather( float3 normal , fixed gloss , fixed3 spec ){
            float4 cube = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, normal, (1 - gloss) * 6);              
            
            //return cube;
            return DecodeHDR(cube, unity_SpecCube0_HDR);
            }
            
            uniform fixed _DebugReflect;
            uniform fixed _DebugAmbient;
            uniform fixed _DebugDirect;
            float3 DebuggerOutput( float3 final , fixed dRefl , fixed dAmb , fixed dDir , fixed dDebug , float3 refl , float3 amb , float3 dir ){
            if(dDebug != 0)
            {
            if(dRefl != 0) return refl;
            else if(dAmb != 0) return amb;
            else if(dDir != 0) return dir;
            else return final;
            }
            else return final;
            }
            
            uniform fixed _Debug;
            uniform fixed _UseReflections;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
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
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 node_2239 = (lerp( lerp( lerp( _Primary.rgb, _Secondary.rgb, _Mask_var.rgb.r ), _Third.rgb, _Mask_var.rgb.g ), float3(1,1,1), _Mask_var.rgb.b ));
                float node_1588 = 1.0;
                float node_7173 = 0.0;
                float3 rimCol = (lerp( lerp( lerp( _RimPrimary.rgb, _RimSecondary.rgb, _Mask_var.rgb.r ), _RimThird.rgb, _Mask_var.rgb.g ), node_7173, _Mask_var.rgb.b ));
                float rimPinch = (lerp( lerp( lerp( _RimPrimary.a, _RimSecondary.a, _Mask_var.rgb.r ), _RimThird.a, _Mask_var.rgb.g ), node_7173, _Mask_var.rgb.b ));
                fixed node_4505 = 0.0;
                fixed node_8265 = (-7.0);
                float rimAmount = (lerp( lerp( lerp( _Primary.a, _Secondary.a, _Mask_var.rgb.r ), _Third.a, _Mask_var.rgb.g ), 0.0, _Mask_var.rgb.b ));
                float node_1046 = saturate((0.5*dot(normalDirection,float3(0,1,0))+0.5*1.428571+0.1428571));
                fixed rimFade = (saturate((node_8265 + ( (((1.0 - max(0,dot(viewDirection,normalDirection)))+lerp((-0.25),0.25,rimPinch)) - node_4505) * (6.0 - node_8265) ) / (1.0 - node_4505)))*rimAmount*(1.0 - node_1046)*node_1046);
                float3 spec = (_Specular_var.rgb*i.vertexColor.b*lerp(lerp(node_2239,float3(node_1588,node_1588,node_1588),0.33),rimCol,rimFade)*_SpecularAdjust);
                float3 node_7023 = spec;
                float node_9121 = max(0,dot(halfDirection,normalDirection));
                fixed4 _Sharpness_var = tex2D(_Sharpness,TRANSFORM_TEX(i.uv0, _Sharpness));
                float gloss = (_Sharpness_var.r*_SharpnessAdjust);
                float node_3602 = pow(gloss,0.2);
                float node_7965 = lerp(0.0,0.95,node_3602);
                float node_8121 = 0.0;
                float node_96 = (max(0,dot(normalDirection,lightDirection))*attenuation);
                float node_8443 = saturate((node_96*40.0+0.0));
                float node_7953 = lerp(0.05,1,(attenuation*node_8443));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diff = (_MainTex_var.rgb*node_2239*i.vertexColor.rgb);
                float3 node_3374 = (node_8443*lerp(saturate(_LightColor0.rgb),_LightColor0.rgb,(1.0 - saturate(((1.0-max(0,dot(normalDirection, viewDirection)))*1.428571+-0.1428571))))*0.51*attenuation);
                float3 result = ((1.0 - min(_Debug,max(max(_DebugReflect,_DebugAmbient),_DebugDirect)))*((node_7023*saturate(((node_8121 + ( (node_9121 - node_7965) * (1.0 - node_8121) ) / (lerp(1.0,0.95,node_3602) - node_7965))*16.66667+-7.833337))*node_7953*_LightColor0.rgb)+(diff*node_3374)));
                float3 finalColor = result;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
