// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:1,fgcb:1,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3644,x:33586,y:32712,varname:node_3644,prsc:2|diff-9291-OUT,alpha-9877-R;n:type:ShaderForge.SFN_Append,id:4457,x:31643,y:32900,varname:node_4457,prsc:2|A-2697-OUT,B-1587-OUT;n:type:ShaderForge.SFN_Time,id:2892,x:31643,y:33082,varname:node_2892,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2697,x:31438,y:32934,ptovrint:False,ptlb:Xoffset,ptin:_Xoffset,varname:node_2697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.4;n:type:ShaderForge.SFN_Multiply,id:6124,x:31861,y:32908,varname:node_6124,prsc:2|A-4457-OUT,B-2892-T;n:type:ShaderForge.SFN_TexCoord,id:9376,x:31867,y:32726,varname:node_9376,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3780,x:32097,y:32812,varname:node_3780,prsc:2|A-9376-UVOUT,B-6124-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9570,x:32104,y:33011,ptovrint:False,ptlb:node_9570,ptin:_node_9570,varname:node_9570,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:565d1215aaca4fd4ca8e4d203c2b6452,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9877,x:32332,y:32901,varname:node_9877,prsc:2,tex:565d1215aaca4fd4ca8e4d203c2b6452,ntxv:0,isnm:False|UVIN-136-UVOUT,TEX-9570-TEX;n:type:ShaderForge.SFN_ValueProperty,id:8566,x:31885,y:32392,ptovrint:False,ptlb:OuterFresnel,ptin:_OuterFresnel,varname:node_8566,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_ValueProperty,id:1587,x:31421,y:33089,ptovrint:False,ptlb:Yoffset,ptin:_Yoffset,varname:node_1587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_ValueProperty,id:6041,x:31907,y:32528,ptovrint:False,ptlb:InnerFresnel,ptin:_InnerFresnel,varname:node_6041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Fresnel,id:6912,x:32135,y:32321,varname:node_6912,prsc:2|EXP-8566-OUT;n:type:ShaderForge.SFN_Fresnel,id:8232,x:32117,y:32546,varname:node_8232,prsc:2|EXP-6041-OUT;n:type:ShaderForge.SFN_OneMinus,id:1476,x:32371,y:32321,varname:node_1476,prsc:2|IN-6912-OUT;n:type:ShaderForge.SFN_Slider,id:1461,x:32297,y:33139,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_1461,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_VertexColor,id:8849,x:32854,y:33126,varname:node_8849,prsc:2;n:type:ShaderForge.SFN_Color,id:8358,x:32936,y:33359,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:0;n:type:ShaderForge.SFN_Vector1,id:7245,x:32961,y:33554,varname:node_7245,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:9291,x:33228,y:32873,varname:node_9291,prsc:2|A-7616-OUT,B-8849-RGB,C-8358-RGB,D-7245-OUT,E-8849-A;n:type:ShaderForge.SFN_Multiply,id:8770,x:32647,y:32397,varname:node_8770,prsc:2|A-1476-OUT,B-9564-OUT;n:type:ShaderForge.SFN_Multiply,id:1666,x:32927,y:32401,varname:node_1666,prsc:2|A-8770-OUT,B-5172-OUT;n:type:ShaderForge.SFN_Vector1,id:5172,x:32667,y:32579,varname:node_5172,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:7616,x:32808,y:32865,varname:node_7616,prsc:2|A-9877-A,B-1461-OUT,C-9877-RGB,D-1666-OUT;n:type:ShaderForge.SFN_Rotator,id:136,x:32322,y:32615,varname:node_136,prsc:2|UVIN-3780-OUT,ANG-8315-OUT;n:type:ShaderForge.SFN_Vector1,id:8315,x:32117,y:32706,varname:node_8315,prsc:2,v1:-180;n:type:ShaderForge.SFN_OneMinus,id:9564,x:32464,y:32528,varname:node_9564,prsc:2|IN-8232-OUT;proporder:2697-9570-8566-1587-6041-1461-8358;pass:END;sub:END;*/

Shader "Custom/default" {
    Properties {
        _Xoffset ("Xoffset", Float ) = 0.4
        _node_9570 ("node_9570", 2D) = "white" {}
        _OuterFresnel ("OuterFresnel", Float ) = 0.8
        _Yoffset ("Yoffset", Float ) = -0.5
        _InnerFresnel ("InnerFresnel", Float ) = 2
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Color ("Color", Color) = (0.5,0.5,0.5,0)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _node_9570; uniform float4 _node_9570_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Xoffset)
                UNITY_DEFINE_INSTANCED_PROP( float, _OuterFresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Yoffset)
                UNITY_DEFINE_INSTANCED_PROP( float, _InnerFresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_136_ang = (-180.0);
                float node_136_spd = 1.0;
                float node_136_cos = cos(node_136_spd*node_136_ang);
                float node_136_sin = sin(node_136_spd*node_136_ang);
                float2 node_136_piv = float2(0.5,0.5);
                float _Xoffset_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Xoffset );
                float _Yoffset_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Yoffset );
                float4 node_2892 = _Time;
                float2 node_136 = (mul((i.uv0+(float2(_Xoffset_var,_Yoffset_var)*node_2892.g))-node_136_piv,float2x2( node_136_cos, -node_136_sin, node_136_sin, node_136_cos))+node_136_piv);
                float4 node_9877 = tex2D(_node_9570,TRANSFORM_TEX(node_136, _node_9570));
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float _OuterFresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterFresnel );
                float _InnerFresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _InnerFresnel );
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 diffuseColor = ((node_9877.a*_Opacity_var*node_9877.rgb*(((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_OuterFresnel_var))*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_InnerFresnel_var)))*2.0))*i.vertexColor.rgb*_Color_var.rgb*2.0*i.vertexColor.a);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,node_9877.r);
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _node_9570; uniform float4 _node_9570_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Xoffset)
                UNITY_DEFINE_INSTANCED_PROP( float, _OuterFresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Yoffset)
                UNITY_DEFINE_INSTANCED_PROP( float, _InnerFresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_136_ang = (-180.0);
                float node_136_spd = 1.0;
                float node_136_cos = cos(node_136_spd*node_136_ang);
                float node_136_sin = sin(node_136_spd*node_136_ang);
                float2 node_136_piv = float2(0.5,0.5);
                float _Xoffset_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Xoffset );
                float _Yoffset_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Yoffset );
                float4 node_2892 = _Time;
                float2 node_136 = (mul((i.uv0+(float2(_Xoffset_var,_Yoffset_var)*node_2892.g))-node_136_piv,float2x2( node_136_cos, -node_136_sin, node_136_sin, node_136_cos))+node_136_piv);
                float4 node_9877 = tex2D(_node_9570,TRANSFORM_TEX(node_136, _node_9570));
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float _OuterFresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterFresnel );
                float _InnerFresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _InnerFresnel );
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 diffuseColor = ((node_9877.a*_Opacity_var*node_9877.rgb*(((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_OuterFresnel_var))*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_InnerFresnel_var)))*2.0))*i.vertexColor.rgb*_Color_var.rgb*2.0*i.vertexColor.a);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * node_9877.r,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
