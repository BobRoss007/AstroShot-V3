using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public Transform target;
	public float distance = 10.0f;
	public float height = 1.5f;
	public float pitch = -5.0f;

	Vector3 prevPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 dir = (prevPos - target.position).normalized;
		//dir = new Vector3(dir.x, 0, dir.z).normalized;
		
		float dist = Vector3.Distance(prevPos, target.position);
		if(dist < distance)
		{
			Vector3 newDir = new Vector3(dir.x, Mathf.Abs(dir.y), dir.z);
			dir = Vector3.Slerp(dir, newDir, Time.deltaTime * 8);
		}
		float newDistance = distance;
		// if(dir.y < 0)
		// {
		// 	newDistance = distance * 3;
		// }

		prevPos = target.position + dir * newDistance;

		Vector3 rightDir = Vector3.ProjectOnPlane(dir, Vector3.up);
		rightDir = Quaternion.AngleAxis(90, Vector3.up) * dir;
		dir = Quaternion.AngleAxis(pitch, rightDir) * dir;

		transform.position = target.position + dir * distance + Vector3.up * height;
		transform.forward = -dir;
	}
}
