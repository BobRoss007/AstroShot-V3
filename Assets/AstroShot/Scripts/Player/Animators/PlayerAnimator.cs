using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimator : NetworkBehaviour {

	[System.Serializable]
	public enum blinkAxis { X, Y, Z };

	[System.Serializable]
	public class BlinkInfo
	{
		public Transform bone;
		public bool xAxis;
		public bool yAxis;
		public bool zAxis;
		public Vector3 closedRotation;
	}

    //public PlayerActor playerActor;
	public PlayerMovement movementScript;
	public Animator animController;

	[Header("Placeholders")]
	public Transform ballTransform;
	public Transform ballAttachpoint;

	[Header("Blending Parameters")]
	public float blendRate = 5f;
	[Space]
	public string runBlend = "RunBlend";
	public string groundedBlend = "GroundedBlend";
	public string crouchBlend = "CrouchBlend";
	public string rollSpeed = "RollSpeed";
	public float runPoint = 1f;
	public float idlePoint = 0f;
	public string isRolling = "isRolling";
	public string isBallMode = "isBallMode";

	float runBlendValue = 0f;
	float groundedBlendValue = 1f;

	[Header("Blinking")]
	public BlinkInfo[] bonesToBlink;
	public Vector2 randomBlinkInterval = new Vector2(0.5f, 10);
	public float blinkSpeed = 15;

	float blinkTimer = 0;
	float blinkLerp = 0;
	bool isBlinking = false;

	void Start () {
		
	}
	
	
	void Update ()
	{
        //if(!hasAuthority) return;

		float maxSpeedProportional = Mathf.Lerp(1, movementScript.maxCrouchSpeed / movementScript.maxRunSpeed, movementScript.crouchAmount);
		maxSpeedProportional = 1;
		runBlendValue = movementScript.rawSpeed / Mathf.Lerp(movementScript.maxRunSpeed, movementScript.maxCrouchSpeed, movementScript.crouchAmount);
		runBlendValue = Mathf.Lerp(idlePoint, runPoint, Mathf.Clamp(runBlendValue, 0, maxSpeedProportional));

		groundedBlendValue = Mathf.Lerp(groundedBlendValue, movementScript.isGrounded ? 1f : 0f, Time.deltaTime * blendRate);

		animController.SetFloat(runBlend, runBlendValue);
		animController.SetFloat(groundedBlend, groundedBlendValue);
		animController.SetFloat(crouchBlend, movementScript.crouchAmount);
		animController.SetBool(isRolling, movementScript.isRolling);
        animController.SetBool(isBallMode, false);

        //if(movementScript.isRolling)
            animController.SetFloat(rollSpeed, 1f / movementScript.rollDuration);
        //else if(playerActor.PlayerBallModeComponent.enabled) {
        //    float rotationDirection = Vector3.Dot(transform.forward, playerActor.PlayerBallModeComponent.VisualVelocity);

        //    rotationDirection = Mathf.Sign(rotationDirection);
        //    //Debug.Log(rotationDirection);

        //    animController.SetFloat(rollSpeed, (Mathf.Abs(playerActor.PlayerBallModeComponent.VisualVelocity.z) * rotationDirection) *.1f);
        //}
    }

    private void LateUpdate()
	{
		if(ballTransform && ballAttachpoint)
		{
			ballTransform.position = ballAttachpoint.position;
			ballTransform.rotation = ballAttachpoint.rotation;

			ballTransform.gameObject.SetActive(movementScript.isRolling);
		}

		if (blinkTimer <= 0)
		{
			blinkTimer = Random.Range(randomBlinkInterval.x, randomBlinkInterval.y);
			isBlinking = true;
		}
		else
		{
			blinkTimer -= Time.deltaTime;
		}

		if (isBlinking)
		{
			blinkLerp = Mathf.Clamp01(blinkLerp + Time.deltaTime * blinkSpeed);
			isBlinking = !(blinkLerp == 1);
		}
		else
		{
			blinkLerp = Mathf.Clamp01(blinkLerp - Time.deltaTime * blinkSpeed);
		}

		for (int i = 0; i < bonesToBlink.Length; i++)
		{
			Quaternion rot = bonesToBlink[i].bone.localRotation;
			if (bonesToBlink[i].xAxis)
			{
				rot = Quaternion.Euler(bonesToBlink[i].closedRotation.x, rot.eulerAngles.y, rot.eulerAngles.z);
			}

			if(bonesToBlink[i].yAxis)
			{
				rot = Quaternion.Euler(rot.eulerAngles.x, bonesToBlink[i].closedRotation.y, rot.eulerAngles.z);
			}

			if (bonesToBlink[i].zAxis)
			{
				rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, bonesToBlink[i].closedRotation.z);
			}

			
			bonesToBlink[i].bone.localRotation = Quaternion.Slerp(bonesToBlink[i].bone.localRotation, rot, blinkLerp);
		}
	}
}
