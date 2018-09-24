using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Control : MonoBehaviour {

	[Header("--- Rotation Info ---")]
	public Vector3 rotationAxis = new Vector3(0, 1, 0);
	public float rotationRpm = 10.0f;
	public float rotationAcceleration = 10.0f;
	public bool stopWhenDeactivated = true;

	Rigidbody rBody;
	bool doRotation = false;
	

	// Use this for initialization
	void Start ()
	{
		rBody = GetComponent<Rigidbody>();

		if(!rBody)
		{
			Debug.LogError("There is no RigidBody on the same game object as Platform_Control");
		}
		else
		{
			UpdateRigidbodyLimits();
		}
	}
	
	void UpdateRigidbodyLimits()
	{
		rBody.maxAngularVelocity = rotationRpm;
	}

	public void SpinPlatform()
	{
		
	}

	public void SpinPlatformToggle()
	{
		doRotation = !doRotation;
		UpdateRigidbodyLimits();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if(doRotation)
		{
			DoPhysicsRotation(false);
		}
		else if(stopWhenDeactivated)
		{
			DoPhysicsRotation(true);
		}
	}

	void DoPhysicsRotation(bool stopRotation)
	{
		float rpmSign = Mathf.Sign(rotationRpm);
		Vector3 correctRotationAxis = rotationAxis * rpmSign;

		Vector3 localRpm = rBody.transform.InverseTransformVector(rBody.angularVelocity);
		Vector3 localAxisRpm = Vector3.Scale(localRpm, correctRotationAxis);

		float direction = Mathf.Sign(Vector3.Dot(correctRotationAxis, localAxisRpm));
		float currentRpm = localAxisRpm.magnitude;
		float rpmAmount = Mathf.Clamp(currentRpm / Mathf.Abs(rotationRpm), 0, 2);

		rpmAmount = stopRotation ? (direction * -Mathf.Clamp01(rpmAmount)) : (Mathf.Clamp01(1 - rpmAmount) * rpmSign);

		Vector3 axis = transform.TransformDirection(rotationAxis);

		rBody.AddTorque(axis * rpmAmount * rotationAcceleration, ForceMode.Acceleration);

		Debug.DrawRay(rBody.position, axis * rpmAmount, Color.green, 0, false);
	}

}
