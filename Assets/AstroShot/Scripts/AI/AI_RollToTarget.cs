using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RollToTarget : MonoBehaviour {

	public Transform target;
	public float ballRadius = 0.5f;
	public float minDistance = 2.0f;
	public float distanceMargin = 0.5f;
	public float distanceDamping = 10.0f;
	public float maxSpeed = 10.0f;
    public float maxTorque = 10.0f;

	Rigidbody rBody;

	void Start ()
	{
		rBody = GetComponent<Rigidbody>();

		rBody.maxAngularVelocity = maxSpeed / ballRadius;
	}
	
	void FixedUpdate ()
	{
		Vector3 flatVel = new Vector3(rBody.velocity.x, 0, rBody.velocity.z);
		float velocityMag = flatVel.magnitude;

		Vector3 targetDir = target.position - (rBody.position + flatVel * Time.fixedDeltaTime * distanceDamping);
		targetDir = new Vector3(targetDir.x, 0, targetDir.z);
		float distance = targetDir.magnitude;

		float distMult = (distance - minDistance) / distanceMargin;
		distMult = Mathf.Clamp(distMult, -1, 1);

		Vector3 torqueVec = Quaternion.AngleAxis(90, Vector3.up) * targetDir.normalized;

		rBody.AddTorque(torqueVec * maxTorque * distMult, ForceMode.Acceleration);

      

	}

  
}
