using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionUpdater : MonoBehaviour {

	//public ReflectionProbe playerProbe;
	//public Transform playerTarget;
	//[Space]
	public ReflectionProbe waterProbe;
	public Transform waterPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//playerProbe.transform.position = playerTarget.position;

		Vector3 waterProbePosition = transform.position;
		waterProbePosition.y = -waterProbePosition.y - waterPlane.position.y;
		waterProbe.transform.position = waterProbePosition;

		//playerProbe.RenderProbe();
		//waterProbe.RenderProbe();
	}
}
