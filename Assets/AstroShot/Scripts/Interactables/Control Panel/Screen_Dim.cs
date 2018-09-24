using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Dim : MonoBehaviour {

	public bool isActive;
	public string propertyName;
	public Color lightOn;
	public Color lightOff;
	public float blendUpTime = 0.25f;
	public float blendDownTime = 0.1f;
	public int materialIndex;
	public MeshRenderer rend;

	MaterialPropertyBlock matProperty;
	float blendLerp = 0;
	Color finalColor = Color.black;

	void Start () 
	{
		matProperty = new MaterialPropertyBlock();
	}
	
	void Update () 
	{
		if (isActive)
		{
			blendLerp = Mathf.Lerp(Mathf.Max(0.5f, blendLerp), 1, 1f / blendUpTime * Time.deltaTime);
		}
		else
		{
			blendLerp = Mathf.Lerp(blendLerp, 0, 1f / blendDownTime * Time.deltaTime);
		}

		finalColor = Color.Lerp(lightOff, lightOn, blendLerp);
		matProperty.SetColor(propertyName, finalColor);

		rend.SetPropertyBlock(matProperty, materialIndex);

	}
	public void ScreenActivate ()
	{
		isActive = true;
	}
	public void ScreenDeactivate ()
	{
		isActive = false;
	}
}
