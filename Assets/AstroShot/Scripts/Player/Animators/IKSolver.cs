using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSolver : MonoBehaviour {

	public enum BoneForward { X, Y, Z };

	public bool debugFirstPass = false;
	public bool debugSecondPass = false;
	public bool scaleBones = false;

	public bool doSecondPass = true;

	public Transform upper;
	public Transform lower;
	public Transform end;
	public Transform target;
	public Transform pole;

	public bool autoGetLengths = true;
	public float upperLength = 1;
	public float lowerLength = 1;
	public BoneForward boneForward;

	public float distanceRatio;

	Vector3 upperDir = Vector3.forward;
	Vector3 lowerDir = Vector3.forward;
	Vector3 poleDir = Vector3.one;

	Vector3 targetDir = Vector3.forward;
	Vector3 upperTarget = Vector3.zero;
	Vector3 lowerTarget = Vector3.zero;
	float upperXangle = 0;

	bool ikToggle = true;

	void Start()
	{
		if(autoGetLengths)
		{
			upperLength = Vector3.Distance(upper.position, lower.position);
			lowerLength = Vector3.Distance(lower.position, target.position);
		}
	}

	void LateUpdate ()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			ikToggle = !ikToggle;
		}

		if(!ikToggle) return;

		targetDir = (target.position - upper.position).normalized;

		poleDir = (pole.position - upper.position).normalized;
		poleDir = Vector3.ProjectOnPlane(poleDir, (target.position - upper.position).normalized);
		//Debug.DrawRay(upper.position, (pole.position - upper.position).normalized, Color.blue, 0, false);
		//Debug.DrawRay(upper.position, poleDir, Color.magenta, 0, false);

		float restDist = IKFirstPass();

		if(doSecondPass)
		{
			IKSecondPass(restDist);
		}

		Quaternion upperQuat = Quaternion.LookRotation(upperDir, poleDir);
		Quaternion lowerQuat = Quaternion.LookRotation(lowerDir, poleDir);

		Quaternion axisQuat = Quaternion.identity;
		if(boneForward == BoneForward.Z)
		{
			axisQuat = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		}
		else if(boneForward == BoneForward.Y)
		{
			axisQuat = Quaternion.LookRotation(Vector3.up, Vector3.forward);
		}
		else if(boneForward == BoneForward.X)
		{
			axisQuat = Quaternion.LookRotation(Vector3.right, Vector3.up);
		}

		upper.rotation = upperQuat * axisQuat;
		lower.rotation = lowerQuat * axisQuat;
		end.rotation = target.rotation;

		lower.position = upper.position + upperTarget;

		Quaternion poleRot = Quaternion.FromToRotation(upper.up, (pole.position - upper.position));
		//Debug.DrawRay(midPoint, poleRot * Vector3.forward, Color.white, 0, false);

		//upper.rotation *= poleRot;

		if(scaleBones)
		{
			upper.localScale = new Vector3(1, 1, upperLength);
			lower.localScale = new Vector3(1, 1, lowerLength);
		}

	}

	float IKFirstPass()
	{
		float totalDist = upperLength + lowerLength;
		float dist = Mathf.Clamp(Vector3.Distance(upper.position, target.position), 0, totalDist);

		distanceRatio = (upperLength / totalDist);
		float missingSide = Mathf.Sqrt(Mathf.Pow(upperLength, 2) - Mathf.Pow(dist * distanceRatio, 2));

		Vector3 upperDist = Vector3.forward * dist * distanceRatio;

		//poleDir = Vector3.ProjectOnPlane((pole.position - (upper.position + upperDist).normalized).normalized, Vector3.forward).normalized;
		//Debug.DrawRay(upper.position, poleDir, Color.magenta, 0, false);
		Vector3 upperHeight = -Vector3.up * missingSide;

		upperTarget = upper.position + upperDist + upperHeight;
		lowerTarget = upper.position + Vector3.forward * dist;

		upperDir = (upperTarget - upper.position).normalized;
		lowerDir = (lowerTarget - upperTarget).normalized;


		Vector3 lowerStartPos = upper.position + upperDir * upperLength;
		Vector3 lowerEndPos = lowerStartPos + lowerDir * lowerLength;

		float restDist = Vector3.Distance(upper.position, lowerEndPos);

		if (debugFirstPass)
		{
			Debug.DrawRay(upper.position, Vector3.forward * dist, Color.green, 0, false);
			Debug.DrawRay(upper.position + Vector3.forward * dist * distanceRatio, upperHeight, Color.green, 0, false);
			Debug.DrawRay(lowerEndPos, Vector3.up * 0.1f, Color.magenta, 0, false);

			Debug.DrawRay(upper.position, upperDir * upperLength, Color.red, 0, false);
			Debug.DrawRay(lower.position, lowerDir * lowerLength, Color.red, 0, false);

			Debug.DrawLine(upper.position, lowerEndPos, Color.white, 0, false);
		}

		return restDist;
	}

	void IKSecondPass(float restDist)
	{
		float totalDist = (upperLength + lowerLength);
		float dist = Mathf.Clamp(Vector3.Distance(upper.position, target.position), 0, totalDist);
		dist *= dist / restDist;// Mathf.Pow(dist / restDist, 2);
		dist = Mathf.Clamp(dist, Mathf.Epsilon, totalDist - Mathf.Epsilon);		

		distanceRatio = (upperLength / totalDist);
		float missingSide = Mathf.Sqrt(Mathf.Pow(upperLength, 2) - Mathf.Pow(dist * distanceRatio, 2));

		upperXangle = Mathf.Atan2(dist, missingSide) * Mathf.Rad2Deg;

		Vector3 poleStart = upper.position + (target.position - upper.position).normalized * dist * distanceRatio;
		poleDir = Vector3.ProjectOnPlane((pole.position - poleStart).normalized, targetDir).normalized;

		Vector3 upperDist = targetDir * dist * distanceRatio;
		Vector3 upperHeight = poleDir * missingSide;

		upperTarget = upper.position + upperDist + upperHeight;
		lowerTarget = upper.position + targetDir * dist;

		upperDir = (upperTarget - upper.position).normalized;
		lowerDir = (lowerTarget - upperTarget).normalized;

		Vector3 lowerStartPos = upper.position + upperDir * upperLength;
		Vector3 lowerEndPos = lowerStartPos + lowerDir * lowerLength;

		Vector3 correctionAxis = Quaternion.AngleAxis(90, targetDir) * poleDir;
		float angleCorrection = Vector3.Angle(targetDir, lowerEndPos - upper.position) * (distanceRatio > 0.5f ? -1 : 1);
		Quaternion rotCorrection = Quaternion.AngleAxis(angleCorrection, correctionAxis);

		upperDir = rotCorrection * upperDir;
		lowerDir = rotCorrection * lowerDir;

		upperTarget = upperDir * upperLength;

		if (debugSecondPass)
		{
			Debug.DrawRay(upper.position, targetDir * dist, new Color(0, 1, 1, 0.25f), 0, false);
			Debug.DrawRay(upper.position + upperDist, upperHeight, new Color(0, 1, 1, 0.25f), 0, false);
			//Debug.DrawRay(lowerEndPos, Vector3.up * 0.1f, Color.magenta, 0, false);
			Debug.DrawRay(poleStart, correctionAxis, Color.magenta, 0, false);

			Debug.DrawRay(upper.position, upperDir * upperLength, Color.red * Color.yellow, 0, false);
			Debug.DrawRay(lower.position, lowerDir * lowerLength, Color.red * Color.yellow, 0, false);

			Debug.DrawRay(poleStart, poleDir, Color.magenta, 0, false);

			Debug.DrawRay(pole.position + -Vector3.up * 0.1f, Vector3.up * 0.2f, Color.magenta, 0, false);
			Debug.DrawRay(pole.position + -Vector3.forward * 0.1f, Vector3.forward * 0.2f, Color.magenta, 0, false);
			Debug.DrawRay(pole.position + -Vector3.right * 0.1f, Vector3.right * 0.2f, Color.magenta, 0, false);
		}
	}
}
