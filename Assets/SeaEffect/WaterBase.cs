////////////////////////////////////////////////////////////////////
// WaterBase.cs
// 
// By YF Z 
// Modifed By Luisa Z 
// Aug 2017
// From Taiyouxi (http://www.taiyouxi.cn)
// shader的基本设置，水花的边缘LOD等
// 
////////////////////////////////////////////////////////////////////

using UnityEngine;

public enum WaterQuality {
		High = 2,
		Medium = 1,
		Low = 0,
}

[ExecuteInEditMode]
public class WaterBase : MonoBehaviour 
{
	public Material sharedMaterial;
	public WaterQuality waterQuality = WaterQuality.High;
	public bool edgeBlend = true;

    public bool bTest;

	public void UpdateShader() 
	{		
		if(waterQuality > WaterQuality.Medium)
			sharedMaterial.shader.maximumLOD = 401; //最大lod值，Subshader如果lod低于这个值就渲染
        else if(waterQuality> WaterQuality.Medium)
			sharedMaterial.shader.maximumLOD = 301;
		else 
			sharedMaterial.shader.maximumLOD = 201;


        Shader.EnableKeyword("WATER_EDGEBLEND_ON");  //预编译边缘圆角开
        Shader.DisableKeyword("WATER_EDGEBLEND_OFF");

    }
	
    //水的边缘渲染
	public void WaterTileBeingRendered (Transform tr, Camera currentCam) 
	{
	}

    public void OnEnable() 
	{
        if (sharedMaterial)
        {
            UpdateShader();
        }
    }
}