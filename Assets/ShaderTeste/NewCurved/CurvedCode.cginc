// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


#include "UnityCG.cginc"

struct appdata
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float4 color : COLOR;
	float3 normal : NORMAL;
};

struct v2f
{
	float2 uv : TEXCOORD0;
	UNITY_FOG_COORDS(1)
	float4 color : TEXCOORD2;
	float4 vertex : SV_POSITION;
	half3 worldNormal: NORMAL;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float _CurveStrength;
float _Brightness;
float _Strength;
float4 _Color;
float _Detail;

float Toon(float3 normal, float3 lightDir) {
	float NdotL = max(0.0,dot(normalize(normal), normalize(lightDir)));

	return floor(NdotL / _Detail);
}

v2f vert(appdata v)
{
	v2f o;

	float _Horizon = 100.0f;
	float _FadeDist = 50.0f;

	o.vertex = UnityObjectToClipPos(v.vertex);


	float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);

	o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;

	o.uv = TRANSFORM_TEX(v.uv, _MainTex);

	o.color = v.color;

	UNITY_TRANSFER_FOG(o, o.vertex);
	o.worldNormal = UnityObjectToWorldNormal(v.normal);
	return o;
}

fixed4 frag(v2f i) : SV_Target
{
	// sample the texture
	fixed4 col = tex2D(_MainTex, i.uv);
	col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;
// apply fog
UNITY_APPLY_FOG(i.fogCoord, col);
return col;
}