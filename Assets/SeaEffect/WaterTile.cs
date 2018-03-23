using UnityEngine;

/// <summary>
/// 
/// </summary>
[ExecuteInEditMode]
public class WaterTile : MonoBehaviour 
{
	public PlanarReflection reflection;
	public WaterBase waterBase;

    public void Start () 
	{
		AcquireComponents();
	}
	
	///获取组件
	private void AcquireComponents() 
	{
		if (!reflection) {
			if (transform.parent)
				reflection = (PlanarReflection)transform.parent.GetComponent<PlanarReflection>();//平面反射
			else
				reflection = (PlanarReflection)transform.GetComponent<PlanarReflection>();	
		}
		
		if (!waterBase) {
			if (transform.parent)
				waterBase = (WaterBase)transform.parent.GetComponent<WaterBase>();
			else
				waterBase = (WaterBase)transform.GetComponent<WaterBase>();	
		}
	}
	
#if UNITY_EDITOR
	public void Update () 
	{
		AcquireComponents();
	}
#endif
	
	public void OnWillRenderObject() 
	{
		if (reflection)
			reflection.WaterTileBeingRendered(transform, Camera.current);
		if (waterBase)
			waterBase.WaterTileBeingRendered(transform, Camera.current);		
	}
}
