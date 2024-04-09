Shader "Unlit/2D_Screen2222"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
                ColorBitValue ("Color Levels", Range(0,1024)) = 1024
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
                float3 worldPos : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float ColorIntensity;
            float4 UserGazePoint;
            float FoveaRegionSize;
            float BrightnessIntensity;
            int ColorBitValue;
            float2 GridSize;
            float4 AverageColor;
            float Resolution_X;
            float Resolution_Y;
            float GroupSize;
            float _brightnessIntensity;
            float _colorIntensity;
            float distancebet;
            fixed4 color;
            float2 gridPosition;
            float2 gridStep;
            float2 topLeft;
            float2 topRight;
            float2 bottomLeft;
            float2 bottomRight;
            float4 colorTopLeft;
            float4 colorTopRight;
            float4 colorBottomLeft;
            float4 colorBottomRight;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float3 HueShift(float3 color, float _colorIntensity)
            {
                float3x3 RGB_YIQ =
					float3x3(0.299, 0.587, 0.114,
							  0.5959, -0.275, -0.3213,
							  0.2115, -0.5227, 0.3112);
				
                float3x3 YIQ_RGB =
					float3x3(1, 0.956, 0.619,
							  1, -0.272, -0.647,
							  1, -1.106, 1.702);

                float3 YIQ = mul(RGB_YIQ, color);
                float hue = atan2(YIQ.z, YIQ.y);
                float chroma = length(float2(YIQ.y, YIQ.z));

                float Y = YIQ.x;
                float I = chroma * cos(hue);
                float Q = chroma * sin(hue);

                float3 shiftYIQ = float3(Y, I, Q);
                float3 newRGB = mul(YIQ_RGB, shiftYIQ);
                return newRGB;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                Resolution_X = 7680.0;
                Resolution_Y = 4320.0;

                _brightnessIntensity = 1.0;
                _colorIntensity = 1.0;

                distancebet = distance(i.worldPos, UserGazePoint.xyz);
                if (distancebet > FoveaRegionSize)
                {
                    _brightnessIntensity -= (distancebet - FoveaRegionSize) * BrightnessIntensity;
                    _brightnessIntensity = max(_brightnessIntensity, 0);
                    _colorIntensity -= (distancebet - FoveaRegionSize) * ColorIntensity;
                    _colorIntensity = max(_colorIntensity,0);
                }
                color = tex2D(_MainTex, i.uv) * _brightnessIntensity;


                if(distancebet > FoveaRegionSize)
                {


                    GridSize = float2(Resolution_X, Resolution_Y);
                    GridSize *= GroupSize;

                    gridPosition = floor(i.uv * GridSize) / GridSize;
                    gridStep = 1.0/GridSize;
                                                    
                    topLeft = gridPosition;
                    topRight = gridPosition + float2(gridStep.x, 0);
                    bottomLeft = gridPosition + float2(0, gridStep.y);
                    bottomRight = gridPosition + gridStep;
    
                    colorTopLeft = tex2D(_MainTex, topLeft);
                    colorTopRight = tex2D(_MainTex, topRight);
                    colorBottomLeft = tex2D(_MainTex, bottomLeft);
                    colorBottomRight = tex2D(_MainTex, bottomRight);
                    
                    AverageColor = (colorTopLeft + colorTopRight + colorBottomLeft + colorBottomRight) / 4.0 * _brightnessIntensity;
                    color = AverageColor;
                    color.rgb = floor(color.rgb * ColorBitValue) / ColorBitValue;
                }
                //color.rgb = HueShift(color.rgb, _colorIntensity);


                return fixed4(color.rgb, 1.0);

            }
            ENDCG
        }
    }
}
