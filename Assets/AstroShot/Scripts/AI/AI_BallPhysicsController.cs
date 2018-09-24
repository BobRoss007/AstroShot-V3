using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AI_BallPhysicsController : MonoBehaviour
{

	public Transform target;
	public float ballRadius = 0.5f;
	public float minDistance = 3.0f;
	public float distanceMargin = 0.5f;
	public float distanceDamping = 30.0f;
	public float maxSpeed = 10.0f;
    public float maxTorque = 100.0f;
	[Space]
	[Range(0,1)]
	public float spinStop = 0.5f;

	[Header("--- Test Functionality ---")]

	[SerializeField]
	bool useMinDist = true;
	[SerializeField]
	bool followTransform = true;
	Vector3 targetPos = Vector3.zero;

	Rigidbody rBody;

	void Start ()
	{
		rBody = GetComponent<Rigidbody>();

		rBody.maxAngularVelocity = maxSpeed / ballRadius;
	}
	
	///<param name="pos">Where to go?</param>
	///<param name="useMinDistance">Keep a safe distance?</param>
	public void GoToTarget(Vector3 pos, bool useMinDistance)
	{
		useMinDist = useMinDistance;
		followTransform = false;
		targetPos = pos;
	}

	///<param name="useMinDistance">Keep a safe distance?</param>
	public void StayInPlace(bool useMinDistance = false)
	{
		useMinDist = useMinDistance;
		followTransform = false;
		targetPos = transform.position + Vector3.Scale(rBody.velocity, rBody.velocity) / maxSpeed * ballRadius * Mathf.PI * 2;
	}

	///<param name="useMinDistance">Keep a safe distance?</param>
	public void FollowTransform(bool useMinDistance)
	{
		useMinDist = useMinDistance;
		followTransform = true;
	}

	void FixedUpdate ()
	{
		float minDist = useMinDist ? minDistance : 0;

		if(followTransform)
		{
			if(target)
			{
				targetPos = target.position;
			}
			else
			{
				Debug.LogError("No target transform set for ball. Stops in place in protest.");
				StayInPlace();
			}
			
		}		

		Vector3 flatVel = new Vector3(rBody.velocity.x, 0, rBody.velocity.z);
		float velocityMag = flatVel.magnitude;

		Vector3 targetDir = targetPos - (rBody.position + flatVel * Time.fixedDeltaTime * distanceDamping);
		targetDir = new Vector3(targetDir.x, 0, targetDir.z);
		float distance = targetDir.magnitude;

		float distMult = (distance - minDist) / distanceMargin;
		distMult = Mathf.Clamp(distMult, -1, 1);

		Vector3 torqueVec = Quaternion.AngleAxis(90, Vector3.up) * targetDir.normalized;
		Vector3 antiSpin = new Vector3(0, -rBody.angularVelocity.y * spinStop, 0);

		rBody.AddTorque(torqueVec * maxTorque * distMult + antiSpin, ForceMode.Acceleration);

	}

	//#if UNITY_EDITOR
	//[CustomEditor(typeof(AI_BallPhysicsController))]
	//public class AI_BallPhysicsControllerUI : Editor
	//{
	//	public override void OnInspectorGUI()
	//	{
	//		AI_BallPhysicsController ball = (AI_BallPhysicsController)target;
	//		DrawDefaultInspector();
	//		//EditorUtility.SetDirty(ball);

	//		if(GUILayout.Button("Go to target"))
	//		{
	//			ball.GoToTarget(Vector3.zero, false);
	//		}

	//		if(GUILayout.Button("Stay in place"))
	//		{
	//			ball.StayInPlace(false);
	//		}

	//		if(GUILayout.Button("Follow transform"))
	//		{
	//			ball.FollowTransform(true);
	//		}
	//	}
	//}
	//#endif
  
}
