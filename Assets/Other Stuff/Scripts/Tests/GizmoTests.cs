using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTests : MonoBehaviour {

	public Vector3 size = Vector3.one;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, size);
		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
