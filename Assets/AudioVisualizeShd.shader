Shader "Unlit/AudioVisualizeShd"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i, uniform float audioData) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;

                // float mx = fmod(i.x,50.0);
                // float my = fmod(i.y,50.0);
                // fixed2 pos = fixed2(mx, my) - fixed2(25.0, 25.0);
                // float dist_squared = dot(pos, pos);
 
                // fixed4 col = (dist_squared < 400.0) ? vec4(.90, .90, .90, 1.0): vec4(.20, .20, .40, 1.0);
                // return col;

            }
            ENDCG

            // GLSLPROGRAM
            // main() 
            // {
            //     #ifdef VERTEX
                
            //     #endif
            
            //     #ifdef FRAGMENT 
            //     varying vec3 gl_FragCoord; // vertex shader computes this, fragment shader uses this
            //     vec2 pos = mod(gl_FragCoord.xy, vec2(50.0)) - vec2(25.0);
            //     float dist_squared = dot(pos, pos);

            //     gl_FragColor = (dist_squared < 400.0) ? vec4(.90, .90, .90, 1.0) : vec4(.20, .20, .40, 1.0);
            //     #endif
            // }
            // ENDGLSL
        }
    }
}
