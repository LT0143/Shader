Shader "Custom/My First Lighting Shader" {

	Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Albedo", 2D) = "white" {}

    	//当设置这个标志时，默认的材质检查器将不会显示一个纹理的UV/偏移/偏移控制
		[NoScaleOffset] _NormalMap ("Normals", 2D) = "bump" {}
		_BumpScale ("Bump Scale", Float) = 1

		[NoScaleOffset] _MetallicMap ("Metallic", 2D) = "white" {}
		[Gamma] _Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.1

		[NoScaleOffset] _OcclusionMap ("Occlusion", 2D) = "white" {}
		_OcclusionStrength("Occlusion Strength", Range(0, 1)) = 1

		[NoScaleOffset] _EmissionMap ("Emission", 2D) = "black" {}
		_Emission ("Emission", Color) = (0, 0, 0)

		[NoScaleOffset] _DetailMask ("Detail Mask", 2D) = "white" {}
		_DetailTex ("Detail Albedo", 2D) = "gray" {}
		[NoScaleOffset] _DetailNormalMap ("Detail Normals", 2D) = "bump" {}
		_DetailBumpScale ("Detail Bump Scale", Float) = 1
	}

	CGINCLUDE

	#define BINORMAL_PER_FRAGMENT

	ENDCG

	SubShader {

		Pass {
			Tags {
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM

			#pragma target 3.0
			//定义一个着色器 .区别在于着色器特征的排列只在需要时编译，如果没有材质使用某个关键字，那么关于此关键字的着色器变体将不会被编译。
			//Unity也会检查哪些关键字用在构建项目阶段，只会包含必需的着色器变体。
			#pragma shader_feature _METALLIC_MAP 
			#pragma shader_feature _SMOOTHNESS_ALBEDO _SMOOTHNESS_METALLIC
			#pragma shader_feature _OCCLUSION_MAP  
			#pragma shader_feature _EMISSION_MAP
			#pragma shader_feature _DETAIL_MASK
			// unity 多重程序变体，着色器变体。
			// 当使用很多关键字时，编译所有的排列会花费很多时间，所有这些变体都包含在构建项目中，这可能是不必要的
			#pragma multi_compile _SHADOWS_SCREEN  
			#pragma multi_compile _VERTEXLIGHT_ON  

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#define FORWARD_BASE_PASS

			#include "My Lighting.cginc"

			ENDCG
		}

		Pass {
			Tags {
				"LightMode" = "ForwardAdd"  //第二个通道用来渲染额外的光源
			}

			Blend One One
			ZWrite Off

			CGPROGRAM

			#pragma target 3.0

			#pragma shader_feature _METALLIC_MAP
			#pragma shader_feature _ _SMOOTHNESS_ALBEDO _SMOOTHNESS_METALLIC
			#pragma shader_feature _DETAIL_MASK

			#pragma multi_compile_fwdadd_fullshadows
			
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "My Lighting.cginc"

			ENDCG
		}

		Pass {
			Tags {
				"LightMode" = "ShadowCaster"
			}

			CGPROGRAM

			#pragma target 3.0

			#pragma multi_compile_shadowcaster

			#pragma vertex MyShadowVertexProgram
			#pragma fragment MyShadowFragmentProgram

			#include "My Shadows.cginc"

			ENDCG
		}
	}
	//自定义GUI
	CustomEditor "MyLightingShaderGUI"
}