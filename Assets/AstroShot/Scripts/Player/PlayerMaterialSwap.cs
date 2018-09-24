using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSwap : MonoBehaviour {

	public Material[] materials;
	public SkinnedMeshRenderer renderer;

	int matIndex = 0;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			matIndex++;
		}

		if(matIndex >= materials.Length)
		{
			matIndex = 0;
		}

		renderer.material = materials[matIndex];
	}
}
