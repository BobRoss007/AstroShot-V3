using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Rotator : MonoBehaviour {

	public Transform meshTransform;
	public Vector3 axis = Vector3.up;
	public float Rpm = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(meshTransform)
		{
			meshTransform.rotation *= Quaternion.AngleAxis(Rpm * Time.deltaTime, axis);
		}
	}
}
