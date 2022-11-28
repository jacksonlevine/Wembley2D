Shader "Custom/InstancedIndirectColor" {
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _Tiles ("Tiles", Vector) = (16,16,0.0625,0.0625)
        _Index ("Tile Start Index", Float) = 0
    }
    SubShader {
        Tags { "RenderType" = "TransparentCutout" "LightMode"="Always"}
        LOD 200
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma target 4.5
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"
            struct appdata_t {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 tuv : TEXCOORD0;
                float3 ambient : TEXCOORD1;
                float3 diffuse : TEXCOORD2;
                float3 normal  : NORMAL;
            };

            struct v2f {
                float2 tuv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex   : SV_POSITION;
                //fixed4 color    : COLOR;
                float3 ambient : TEXCOORD1;
                float3 diffuse : TEXCOORD2;
                float4 color   : TEXCOORD3;
                SHADOW_COORDS(4)
                //float ref      : TANGENT0;
            };

            struct MeshProperties {
                float4x4 mat;
                //float ref : TANGENT0;
                float4 color;
                float3 tuv : TEXCOORD0;
            };
            StructuredBuffer<MeshProperties> _Properties;
            //test
            float4 _MainTex_ST;
            float4 _Tiles;
            float _Index;
            v2f vert(appdata_full i, uint instanceID: SV_InstanceID) {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(i);
                float index = max(0.0, _Index);
                #if defined(UNITY_INSTANCING_ENABLED) || defined(UNITY_PROCEDURAL_INSTANCING_ENABLED) || defined(UNITY_STEREO_INSTANCING_ENABLED)
                index += unity_InstanceID;
                #endif

                float3 worldNormal = i.normal;
                float3 ndotl = saturate(dot(worldNormal, _WorldSpaceLightPos0.xyz));
                float4 pos = mul(_Properties[instanceID].mat, i.vertex);
                o.vertex = UnityObjectToClipPos(pos);
                float3 ambient = ShadeSH9(float4(worldNormal, 0.0f));
                float3 diffuse = (ndotl * _LightColor0.rgb);
                fixed4 color = i.color;
                o.ambient = ambient;
                o.diffuse = diffuse;
                o.color = color;
                TRANSFER_SHADOW(o);
                //o.ref = _Properties[instanceID].ref;

                float2 uvOffset = float2(
                    floor(fmod(index, _Tiles.x)),
                    floor(fmod(index *_Tiles.z, _Tiles.y))
                    );
 
                o.tuv = i.texcoord.xy + uvOffset.xy * _Tiles.zw;
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target {
                fixed shadow = SHADOW_ATTENUATION(i);
                float3 lighting = i.diffuse * shadow + i.ambient;
                float4 col = (float4)(lighting[0], lighting[1], lighting[2], 1)*tex2D(_MainTex, i.tuv)*i.color;
                // sample texture and return it
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //if(i.color[3] != 0.0f && i.color[3] != 0.2f) {
                if(col[3] < 0.3f) {
                    discard;
                    }
                    return col;
                    //} else {
                    //return i.color;
                    //}
                
            }

            ENDCG
        }
    }
}