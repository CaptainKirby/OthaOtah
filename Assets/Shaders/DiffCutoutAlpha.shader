// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32530,y:32757|diff-1809-OUT,diffpow-1597-OUT,alpha-2-G,clip-1470-OUT;n:type:ShaderForge.SFN_VertexColor,id:2,x:32839,y:32951,cmnt:green is opaque;n:type:ShaderForge.SFN_Tex2d,id:4,x:33130,y:32894,ptlb:Diffuse RGBA(alpha cutout),ptin:_DiffuseRGBAalphacutout,tex:ce779c7b8cc9b944e939cc614a9a15d4,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1470,x:32780,y:33143|A-4-A,B-1803-A;n:type:ShaderForge.SFN_Slider,id:1575,x:32781,y:32611,ptlb:Diffuse Power,ptin:_DiffusePower,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Power,id:1597,x:32917,y:32677|VAL-1575-OUT,EXP-1599-OUT;n:type:ShaderForge.SFN_Vector1,id:1599,x:33334,y:32872,v1:10;n:type:ShaderForge.SFN_TexCoord,id:1758,x:33781,y:32638,uv:0;n:type:ShaderForge.SFN_Time,id:1775,x:33818,y:32841;n:type:ShaderForge.SFN_ValueProperty,id:1776,x:33959,y:33066,ptlb:Min Speed,ptin:_MinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Slider,id:1777,x:33674,y:33251,ptlb:Rotation Speed,ptin:_RotationSpeed,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:1784,x:33877,y:33412|A-1776-OUT,B-1786-OUT,T-1777-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1786,x:33848,y:33164,ptlb:Max Speed,ptin:_MaxSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Color,id:1803,x:33114,y:33159,ptlb:Main Color,ptin:_MainColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1809,x:32794,y:32820|A-4-RGB,B-1803-RGB;proporder:4-1575-1776-1777-1786-1803;pass:END;sub:END;*/

Shader "Shader Forge/DiffCutoutAlpha" {
    Properties {
        _DiffuseRGBAalphacutout ("Diffuse RGBA(alpha cutout)", 2D) = "black" {}
        _DiffusePower ("Diffuse Power", Range(0, 1)) = 0
        _MinSpeed ("Min Speed", Float ) = 0
        _RotationSpeed ("Rotation Speed", Range(0, 1)) = 0
        _MaxSpeed ("Max Speed", Float ) = 0
        _MainColor ("Main Color", Color) = (1,1,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float _DiffusePower;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float2 node_1895 = i.uv0;
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_1895.rg, _DiffuseRGBAalphacutout));
                clip((node_4.a*_MainColor.a) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = pow(max( 0.0, NdotL), pow(_DiffusePower,10.0)) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (node_4.rgb*_MainColor.rgb);
/// Final Color:
                return fixed4(finalColor,i.vertexColor.g);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float _DiffusePower;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float2 node_1896 = i.uv0;
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_1896.rg, _DiffuseRGBAalphacutout));
                clip((node_4.a*_MainColor.a) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = pow(max( 0.0, NdotL), pow(_DiffusePower,10.0)) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (node_4.rgb*_MainColor.rgb);
/// Final Color:
                return fixed4(finalColor * i.vertexColor.g,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_1897 = i.uv0;
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_1897.rg, _DiffuseRGBAalphacutout));
                clip((node_4.a*_MainColor.a) - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_1898 = i.uv0;
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_1898.rg, _DiffuseRGBAalphacutout));
                clip((node_4.a*_MainColor.a) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
