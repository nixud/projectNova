Shader "Unlit/PixelationShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PixelSizeX("Pixel SizeX", Range(1,256)) = 64
        _PixelSizeY("Pixel SizeY", Range(1,256)) = 64
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float _PixelSizeX;
    float _PixelSizeY;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col;

	float ratioX = (int)(i.uv.x * _PixelSizeX) / _PixelSizeX;
	float ratioY = (int)(i.uv.y * _PixelSizeY) / _PixelSizeY;
    
	col = tex2D(_MainTex, float2(ratioX+0.5/_PixelSizeX, ratioY+0.5/_PixelSizeY));

	if (col.a < 0.5)
	{
		col.a = 0;
	}
	else col.a = 1;
    
    return col;

	}
		ENDCG
	}
	}
}