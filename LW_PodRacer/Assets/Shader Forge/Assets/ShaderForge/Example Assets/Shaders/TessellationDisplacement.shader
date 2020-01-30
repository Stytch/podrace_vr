// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:1,fgcb:1,fgca:0,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:34362,y:32994,varname:node_1,prsc:2|emission-162-OUT,voffset-7351-OUT,tess-4852-OUT;n:type:ShaderForge.SFN_TexCoord,id:5,x:32078,y:33020,varname:node_5,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6,x:32307,y:33100,varname:node_6,prsc:2|A-5-UVOUT,B-7-OUT;n:type:ShaderForge.SFN_Vector1,id:7,x:32078,y:33232,varname:node_7,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Tex2d,id:12,x:32784,y:33297,ptovrint:False,ptlb:node_2256,ptin:_node_2256,varname:node_803,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6-OUT,MIP-15-OUT;n:type:ShaderForge.SFN_Vector1,id:15,x:32465,y:33423,varname:node_15,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:152,x:32898,y:32986,ptovrint:False,ptlb:Displacement (R),ptin:_DisplacementR,varname:_DisplacementR,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-2996-UVOUT,MIP-15-OUT;n:type:ShaderForge.SFN_Subtract,id:154,x:33121,y:33212,varname:node_154,prsc:2|A-12-A,B-152-R;n:type:ShaderForge.SFN_Clamp01,id:156,x:33370,y:33108,varname:node_156,prsc:2|IN-154-OUT;n:type:ShaderForge.SFN_Panner,id:161,x:32532,y:32984,varname:node_161,prsc:2,spu:0.3,spv:0|UVIN-6-OUT;n:type:ShaderForge.SFN_Lerp,id:162,x:33597,y:32875,varname:node_162,prsc:2|A-163-OUT,B-152-RGB,T-156-OUT;n:type:ShaderForge.SFN_Vector3,id:163,x:33370,y:32790,varname:node_163,prsc:2,v1:1,v2:0.5,v3:0;n:type:ShaderForge.SFN_Multiply,id:170,x:33370,y:32940,varname:node_170,prsc:2|A-8264-OUT,B-152-RGB;n:type:ShaderForge.SFN_Rotator,id:2996,x:32705,y:32885,varname:node_2996,prsc:2|UVIN-161-UVOUT,ANG-6229-OUT;n:type:ShaderForge.SFN_Slider,id:6229,x:32353,y:32868,ptovrint:False,ptlb:node_6229,ptin:_node_6229,varname:node_6229,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:8264,x:33156,y:32741,ptovrint:False,ptlb:node_8264,ptin:_node_8264,varname:node_8264,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:7351,x:33659,y:33598,varname:node_7351,prsc:2|A-152-RGB,B-6149-OUT;n:type:ShaderForge.SFN_Vector1,id:4852,x:34033,y:33204,varname:node_4852,prsc:2,v1:20;n:type:ShaderForge.SFN_ValueProperty,id:6149,x:33121,y:33449,ptovrint:False,ptlb:node_6149,ptin:_node_6149,varname:node_6149,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.1;proporder:152-6229-8264-12-6149;pass:END;sub:END;*/

Shader "Shader Forge/Examples/TessellationDisplacement" {
    Properties {
        _DisplacementR ("Displacement (R)", 2D) = "black" {}
        _node_6229 ("node_6229", Range(0, 1)) = 1
        _node_8264 ("node_8264", Float ) = 10
        _node_2256 ("node_2256", 2D) = "white" {}
        _node_6149 ("node_6149", Float ) = -0.1
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
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _node_2256; uniform float4 _node_2256_ST;
            uniform sampler2D _DisplacementR; uniform float4 _DisplacementR_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6229)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6149)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _node_6229_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6229 );
                float node_2996_ang = _node_6229_var;
                float node_2996_spd = 1.0;
                float node_2996_cos = cos(node_2996_spd*node_2996_ang);
                float node_2996_sin = sin(node_2996_spd*node_2996_ang);
                float2 node_2996_piv = float2(0.5,0.5);
                float4 node_511 = _Time;
                float2 node_6 = (o.uv0*1.5);
                float2 node_2996 = (mul((node_6+node_511.g*float2(0.3,0))-node_2996_piv,float2x2( node_2996_cos, -node_2996_sin, node_2996_sin, node_2996_cos))+node_2996_piv);
                float node_15 = 2.0;
                float4 _DisplacementR_var = tex2Dlod(_DisplacementR,float4(TRANSFORM_TEX(node_2996, _DisplacementR),0.0,node_15));
                float _node_6149_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6149 );
                v.vertex.xyz += (_DisplacementR_var.rgb*_node_6149_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return 20.0;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float _node_6229_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6229 );
                float node_2996_ang = _node_6229_var;
                float node_2996_spd = 1.0;
                float node_2996_cos = cos(node_2996_spd*node_2996_ang);
                float node_2996_sin = sin(node_2996_spd*node_2996_ang);
                float2 node_2996_piv = float2(0.5,0.5);
                float4 node_511 = _Time;
                float2 node_6 = (i.uv0*1.5);
                float2 node_2996 = (mul((node_6+node_511.g*float2(0.3,0))-node_2996_piv,float2x2( node_2996_cos, -node_2996_sin, node_2996_sin, node_2996_cos))+node_2996_piv);
                float node_15 = 2.0;
                float4 _DisplacementR_var = tex2Dlod(_DisplacementR,float4(TRANSFORM_TEX(node_2996, _DisplacementR),0.0,node_15));
                float4 _node_2256_var = tex2Dlod(_node_2256,float4(TRANSFORM_TEX(node_6, _node_2256),0.0,node_15));
                float3 emissive = lerp(float3(1,0.5,0),_DisplacementR_var.rgb,saturate((_node_2256_var.a-_DisplacementR_var.r)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(1,1,1,0));
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _DisplacementR; uniform float4 _DisplacementR_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6229)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6149)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _node_6229_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6229 );
                float node_2996_ang = _node_6229_var;
                float node_2996_spd = 1.0;
                float node_2996_cos = cos(node_2996_spd*node_2996_ang);
                float node_2996_sin = sin(node_2996_spd*node_2996_ang);
                float2 node_2996_piv = float2(0.5,0.5);
                float4 node_8397 = _Time;
                float2 node_6 = (o.uv0*1.5);
                float2 node_2996 = (mul((node_6+node_8397.g*float2(0.3,0))-node_2996_piv,float2x2( node_2996_cos, -node_2996_sin, node_2996_sin, node_2996_cos))+node_2996_piv);
                float node_15 = 2.0;
                float4 _DisplacementR_var = tex2Dlod(_DisplacementR,float4(TRANSFORM_TEX(node_2996, _DisplacementR),0.0,node_15));
                float _node_6149_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6149 );
                v.vertex.xyz += (_DisplacementR_var.rgb*_node_6149_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return 20.0;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _node_2256; uniform float4 _node_2256_ST;
            uniform sampler2D _DisplacementR; uniform float4 _DisplacementR_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6229)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6149)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _node_6229_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6229 );
                float node_2996_ang = _node_6229_var;
                float node_2996_spd = 1.0;
                float node_2996_cos = cos(node_2996_spd*node_2996_ang);
                float node_2996_sin = sin(node_2996_spd*node_2996_ang);
                float2 node_2996_piv = float2(0.5,0.5);
                float4 node_9895 = _Time;
                float2 node_6 = (o.uv0*1.5);
                float2 node_2996 = (mul((node_6+node_9895.g*float2(0.3,0))-node_2996_piv,float2x2( node_2996_cos, -node_2996_sin, node_2996_sin, node_2996_cos))+node_2996_piv);
                float node_15 = 2.0;
                float4 _DisplacementR_var = tex2Dlod(_DisplacementR,float4(TRANSFORM_TEX(node_2996, _DisplacementR),0.0,node_15));
                float _node_6149_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6149 );
                v.vertex.xyz += (_DisplacementR_var.rgb*_node_6149_var);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return 20.0;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float _node_6229_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6229 );
                float node_2996_ang = _node_6229_var;
                float node_2996_spd = 1.0;
                float node_2996_cos = cos(node_2996_spd*node_2996_ang);
                float node_2996_sin = sin(node_2996_spd*node_2996_ang);
                float2 node_2996_piv = float2(0.5,0.5);
                float4 node_9895 = _Time;
                float2 node_6 = (i.uv0*1.5);
                float2 node_2996 = (mul((node_6+node_9895.g*float2(0.3,0))-node_2996_piv,float2x2( node_2996_cos, -node_2996_sin, node_2996_sin, node_2996_cos))+node_2996_piv);
                float node_15 = 2.0;
                float4 _DisplacementR_var = tex2Dlod(_DisplacementR,float4(TRANSFORM_TEX(node_2996, _DisplacementR),0.0,node_15));
                float4 _node_2256_var = tex2Dlod(_node_2256,float4(TRANSFORM_TEX(node_6, _node_2256),0.0,node_15));
                o.Emission = lerp(float3(1,0.5,0),_DisplacementR_var.rgb,saturate((_node_2256_var.a-_DisplacementR_var.r)));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
