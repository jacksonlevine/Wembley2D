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
        Tags { "RenderType" = "Opaque" }
        LOD 200
        ZWrite Off
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
            struct appdata_t {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 tuv : TEXCOORD0;
            };

            struct v2f {
                float2 tuv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
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
            v2f vert(appdata_t i, uint instanceID: SV_InstanceID) {
                v2f o;
                //UNITY_SETUP_INSTANCE_ID(i);
                float index = max(0.0, _Index);
                //#if defined(UNITY_INSTANCING_ENABLED) || defined(UNITY_PROCEDURAL_INSTANCING_ENABLED) || defined(UNITY_STEREO_INSTANCING_ENABLED)
                //index += unity_InstanceID;
                //#endif
                float4 pos = i.vertex;
                o.vertex = pos;
                //o.ref = _Properties[instanceID].ref;
                o.tuv = i.tuv;
                o.color = _Properties[instanceID].color;
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target {
                float4 col = tex2D(_MainTex, i.tuv)*i.color;
                // sample texture and return it
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //if(i.color[3] != 0.0f && i.color[3] != 0.2f) {
                    return col;
                    //} else {
                    //return i.color;
                    //}
                
            }

            ENDCG
        }
    }
}