////////////////////////////////////////////////////////////////////
// WaterBase.cs
// 
// By YF Z 
// Modifed By Luisa Z 
// Aug 2017
// From Taiyouxi (http://www.taiyouxi.cn)
// shader�Ļ������ã�ˮ���ı�ԵLOD��
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
			sharedMaterial.shader.maximumLOD = 401; //���lodֵ��Subshader���lod�������ֵ����Ⱦ
        else if(waterQuality> WaterQuality.Medium)
			sharedMaterial.shader.maximumLOD = 301;
		else 
			sharedMaterial.shader.maximumLOD = 201;


        Shader.EnableKeyword("WATER_EDGEBLEND_ON");  //Ԥ�����ԵԲ�ǿ�
        Shader.DisableKeyword("WATER_EDGEBLEND_OFF");

    }
	
    //ˮ�ı�Ե��Ⱦ
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