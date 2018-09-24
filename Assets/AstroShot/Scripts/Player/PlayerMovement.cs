using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	[Header("Component Links")]
	public string playerTag = "Player";
    public Rigidbody rBody;
    public Transform headCollider;
    public Vector3 standingHeadOffset = new Vector3(0, 1.4f, 0);
    public Vector3 crouchingHeadOffset = new Vector3(0, 1.2f, 0.2f);
    public Vector3 rollingHeadOffset = new Vector3(0, 0.95f, 0);
    public Collider lowerCollider;
    public Collider upperCollider;

    [Header("Debugging")]
    public bool debugInfoLabels = false;
    public bool debugInputDirection = false;
    public bool debugForceVectors = false;
    public bool debugSpherecast = false;
    public bool debugLegTargets = false;
    public bool debugStopPosition = false;

    [Header("General")]
    public float maxFallSpeed = 30f;

    [Header("Running")]
    public float standAccel = 15f;
    public float crouchAccel = 20f;
    public float airAccel = 5;
    public float accelCurve = 2.0f;
    public float maxRunSpeed = 6f;
    public float maxCrouchSpeed = 3f;
    public float runResponsiveness = 1.25f;
    [Space]
    public float deceleration = 15f;
    public float stopDamping = 20f;
    public float stopSpringDistance = 0.05f;
    [Space]
    public float minSlopeAngle = 40.0f;
    public float maxSlopeAngle = 50.0f;
    //[Space]
    //public float DirectionStiffness = 0.5f;

    [Header("Jumping")]
    public float jumpHeight = 3f;
    public float jumpForwardImpulse = 0.5f;
    public float jumpCooldownTime = 0.5f;
	public float jumpAscendStopTime = 0.25f;
	public float jumpDescendGravityMult = 2f;

    [Header("Rolling")]
    public float rollAccel = 60f;
    public float rollSpeed = 14f;
    public float rollTurnSpeed = 0.15f;
    public float rollDuration = 0.5f;
    [Space]
    public float crouchTapTiming = 0.3f;
    public float rollVelocityThreshold = 2.5f;
	public float rollStandThreshold = 0.25f;

    [Header("Leg Spring")]
    public LayerMask layersToHit;
    public float groundedDistance = 0.3f;
    public float legStandHeight = 0f;
    public float legCrouchHeight = -0.35f;
    public float legRadius = 0.32f;
    public float legOffset = 0.95f;
    [Space]
    public float legStiffness = 4f;
    public float legDamping = 2.0f;
    public float legMaxForce = 5f;
    public float legMinForce = -2.5f;

    [Header("Visuals")]
    public float topSpeedBlendRate = 8.0f;

    public Transform cameraTransform;
    Vector3 desiredFacing = Vector3.forward;

    Vector3 stopPos = Vector3.zero;
    Vector3 stopPosFront = Vector3.zero;
    float prevStopDist = 0;
    Vector3 prevObjectLocalStopPos = Vector3.zero;
    Vector3 stopForce = Vector3.zero;
    Vector3 playerObjectVelocity = Vector3.zero;
    Transform objectTransform;
    Quaternion lastObjectRotation = Quaternion.identity;
    float objectRotationThisFrame = 0;
    float velSqr = 0;
	Vector3 prevObjectLocalHitPosition = Vector3.zero;
	Vector3 prevHitPoint = Vector3.zero;
	Vector3 prevObjectTransVelocity = Vector3.zero;

    float crouchTapTimer = 0;
    float jumpCooldownTimer = 0;
	float jumpAscendStopTimer = 0;
    Vector3 worldInputDir = Vector3.zero;
    public Vector2 inputs = Vector2.zero;
    float inputMagnitude = 0;
    bool isTryingToMove = false;
    bool hasJumped = false;
    bool wantToJump = false;
    bool canDrop = false;
    bool hasDropped = false;
	bool canRoll = false;
    bool wantToRoll = false;
    bool wantToCrouch = false;
    bool wantToPermRoll = false;
    bool hasValidHit = false;

    float inputDecelMult = 0;
    float prevLegDist = 0;

    [Space]
    public float debugJumpTime = 0;
    public float debugJumpHeight = 0;
    float debugJumpPrevPos = 0;

    //public hidden values    
    [HideInInspector]
    public float runSpeedPercent = 0f;
    [HideInInspector]
    public float rawSpeed = 0f;
    [HideInInspector]
    public float crouchAmount = 0;
    [HideInInspector]
    public float rollTimer = 0;
    [HideInInspector]
    public RaycastHit rHit;
    [HideInInspector]
    public Rigidbody lastRBody;

    [Header("SyncVars")]
    [SyncVar]
    public bool isRunning = false;
    //[HideInInspector, SyncVar]
    [SyncVar]
    public bool isGrounded = true;
    //[HideInInspector, SyncVar]
    [SyncVar]
    public bool isRolling = false;
    //[HideInInspector, SyncVar]
    [SyncVar]
    public bool isCrouching = false;

    private void OnGUI() {
        if(debugInfoLabels) {
            GUILayout.Label("Input Direction Dot " + inputDecelMult);
            GUILayout.Label("Input Magnitude " + inputMagnitude);
            GUILayout.Label("Player Velocity " + rawSpeed);
            GUILayout.Label("Crouch Amount " + crouchAmount);
            GUILayout.Label("");
            GUILayout.Label("rHit distance " + rHit.distance);
            GUILayout.Label("isRolling " + isRolling.ToString());
        }
    }

    private void OnDrawGizmos() {
        if(debugInputDirection) {
            Debug.DrawRay(transform.position, worldInputDir, Color.white, 0, false);
            Debug.DrawRay(transform.position, worldInputDir + Vector3.up * 0.05f, Color.black, 0, false);
            Debug.DrawRay(transform.position, playerObjectVelocity, Color.yellow, 0, false);
        }

        if(debugSpherecast) {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawSphere(transform.position + transform.up * (legOffset), legRadius);
            Gizmos.DrawSphere(transform.position + transform.up * (legOffset - groundedDistance - legOffset), legRadius);

            Gizmos.color = new Color(0, 1, 1, 0.3f);
            Gizmos.DrawSphere(transform.position + transform.up * legOffset + -transform.up * rHit.distance, legRadius);

            Debug.DrawRay(transform.position + transform.up * (legOffset), -Vector3.up * (groundedDistance + legOffset), Color.red, 0, false);
        }

        if(debugLegTargets) {
            //Gizmos.color = Color.white;
            //Gizmos.DrawSphere(transform.position + transform.up * -legStandHeight, legRadius * 0.5f);
            Debug.DrawLine(transform.position + transform.up * -legStandHeight + transform.right * 0.1f, transform.position + transform.up * -legStandHeight + transform.right * -0.1f, Color.white, 0, false);
            Debug.DrawLine(transform.position + transform.up * -legCrouchHeight + transform.right * 0.1f, transform.position + transform.up * -legCrouchHeight + transform.right * -0.1f, Color.white, 0, false);
        }

        if(debugStopPosition) {
            Vector3 playerHeight = new Vector3(0, transform.position.y, 0);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(stopPos + playerHeight, 0.1f);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(stopPos + playerHeight, 0.09f);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(stopPosFront + playerHeight, 0.1f);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(stopPosFront + playerHeight, 0.09f);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, 0.04f);
        }

		//Gizmos.color = Color.white;
		//Gizmos.DrawWireSphere(transform.position + Vector3.up * legOffset + -Vector3.up * (legOffset + legStandHeight - legRadius), legRadius);
		//Gizmos.DrawWireSphere(transform.position + Vector3.up * legOffset + -Vector3.up * (legOffset + legCrouchHeight - legRadius), legRadius);
		//Gizmos.DrawWireSphere(transform.position + Vector3.up * legOffset + -Vector3.up * (legOffset + legCrouchHeight - legRadius), legRadius * 0.5f);
		
		//Gizmos.color = Color.green;
		//Gizmos.DrawWireSphere(transform.position + Vector3.up * legOffset + -Vector3.up * rHit.distance, legRadius);
	}

    private RaycastHit GetGroundHit()
    {
        float groundDist = groundedDistance + legOffset;

        RaycastHit rHit = new RaycastHit();

        RaycastHit[] sHits = Physics.SphereCastAll(rBody.position + transform.up * (legOffset), legRadius, -Vector3.up, groundDist, layersToHit, QueryTriggerInteraction.Ignore);
        float hitDist = -1;
        for(int i = 0; i < sHits.Length; i++)
		{
            if(sHits[i].collider.transform.root == transform)
            {
                continue;
            }

			// if(sHits[i].collider.tag == playerTag)
			// {
			// 	continue;
			// }
            
            //Debug.Log("SphereCast Hit - " + sHits[i].collider.name);
            if(hitDist == -1) {
                if(sHits[i].distance <= groundDist)
				{
                    hitDist = sHits[i].distance;
                    rHit = sHits[i];

                    hasValidHit = true;
                }
            }
            else if(sHits[i].distance <= hitDist)
			{
                hitDist = sHits[i].distance;
                rHit = sHits[i];

                hasValidHit = true;
            }
        }

        if(hitDist == -1) {
            isGrounded = false;
            hasValidHit = false;

            rHit.distance = groundDist + legRadius;
            rHit.normal = Vector3.up;
            return rHit;
        }
        //Debug.Log("Used Hit - " + rHit.collider.name);

        //Debug.DrawRay(transform.position + transform.up * (legOffset + legRadius), -Vector3.up * groundedDistance, new Color(0, 1, 0, 0.25f), 0, false);
        //Debug.DrawRay(rHit.point, rHit.normal, new Color(1, 0, 1, 0.5f), 0, false);

        float groundAngle = Vector3.Angle(rHit.normal, Vector3.up);
        if(groundAngle < 90 && rHit.distance > 0) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if(isGrounded && !wantToJump) {
            hasJumped = false;
            canDrop = true;
            hasDropped = false;
        }

        return rHit;
    }

    private void RecalculatePhysicsInfo() {
        if(!hasValidHit) {
            playerObjectVelocity = rBody.velocity;
            objectRotationThisFrame = 0;

            CalculateStopForce();
            return;
        }

        lastRBody = rHit.rigidbody;


        if(lastRBody != null) {
            playerObjectVelocity = rBody.velocity - lastRBody.GetPointVelocity(rHit.point);

			Debug.DrawRay(rHit.point, lastRBody.GetPointVelocity(rHit.point), new Color(0.5f, 1, 0.5f, 0.1f));
        }
        else if(rHit.transform == objectTransform) {
			Vector3 transformBasedVelocity = (objectTransform.TransformPoint(prevObjectLocalHitPosition) - prevHitPoint) / Time.fixedDeltaTime;
			prevObjectLocalHitPosition = objectTransform.InverseTransformPoint(rHit.point);
			prevHitPoint = rHit.point;

			Debug.DrawRay(transform.position, transformBasedVelocity, new Color(1, 1, 1, 0.1f));
			Debug.DrawRay(objectTransform.TransformPoint(prevObjectLocalHitPosition), Vector3.up * 0.5f, new Color(1, 0.5f, 0.5f, 0.1f));
			//Debug.DrawLine(objectTransform.TransformPoint(prevObjectLocalStopPos), transform.position, new Color(1, 0.5f, 0.5f, 0.1f));

			playerObjectVelocity = rBody.velocity - ((transformBasedVelocity + prevObjectTransVelocity) * 0.5f);
			prevObjectTransVelocity = transformBasedVelocity;
        }
		else
		{
			prevObjectLocalHitPosition = rHit.transform.InverseTransformPoint(rHit.point);
			prevHitPoint = rHit.point;
			prevObjectTransVelocity = Vector3.zero;

			playerObjectVelocity = rBody.velocity;
		}

        objectRotationThisFrame = 0;
        if(rHit.transform == objectTransform) {
            Vector3 prevRotation = lastObjectRotation * Vector3.forward;
            prevRotation.y = 0;
            prevRotation.Normalize();
            Vector3 curRotation = objectTransform.rotation * Vector3.forward;
            curRotation.y = 0;
            curRotation.Normalize();

            if(prevRotation != curRotation) {
                objectRotationThisFrame = Vector3.Angle(curRotation, prevRotation) * Mathf.Sign(Vector3.Cross(prevRotation, curRotation).y);
            }

            lastObjectRotation = objectTransform.rotation;

            stopPos = objectTransform.TransformPoint(prevObjectLocalStopPos);
        }
        else {
            lastObjectRotation = Quaternion.identity;

            objectTransform = rHit.transform;
        }
        //Debug.Log(objectRotationThisFrame);

        CalculateStopForce();
    }

    void CalculateStopForce() {
        stopPos.y = 0;
        Vector3 playerPos = rBody.position;
        playerPos.y = 0;
        float stopPosDist = Vector3.Distance(playerPos, stopPos);
        Vector3 stopDir = (playerPos - stopPos);
        stopDir = new Vector3(stopDir.x, 0, stopDir.z).normalized;
        if(stopPosDist > stopSpringDistance) {
            stopPos = playerPos + -stopDir * stopSpringDistance;
            stopPosFront = stopPos + stopDir * stopSpringDistance;
        }

        float predictedStopDist = Vector3.Distance(playerPos + playerObjectVelocity * Time.fixedDeltaTime, stopPos);
        float forceMag = Mathf.Clamp01(predictedStopDist / (stopSpringDistance)) * deceleration;

        Vector3 stopDirFront = (playerPos - stopPosFront).normalized;
        float predictedStopDistFront = Vector3.Distance(playerPos + playerObjectVelocity * Time.fixedDeltaTime, stopPosFront);
        float forceMagFront = Mathf.Clamp01(predictedStopDistFront / (stopSpringDistance)) * deceleration;

        Vector3 flatVel = new Vector3(playerObjectVelocity.x, 0, playerObjectVelocity.z);
        flatVel = Vector3.ClampMagnitude(flatVel, stopSpringDistance * deceleration);

        stopForce = -stopDir * forceMag + -stopDirFront * forceMagFront + flatVel * Time.fixedDeltaTime;

        prevStopDist = predictedStopDist;

		stopPos.y = rHit.point.y;
        prevObjectLocalStopPos = rHit.transform == null ? Vector3.zero : rHit.transform.InverseTransformPoint(stopPos);
    }

    void Start() {
        cameraTransform = Camera.main.transform;

        inputs = Vector2.zero;
        inputMagnitude = 0;
        worldInputDir = Vector3.forward;
        playerObjectVelocity = new Vector3(0.00001f, 0.00001f, 0.00001f);
    }

    void Update() {
        //if(!hasAuthority) return;

        inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputMagnitude = Mathf.Clamp01(inputs.magnitude);
        isTryingToMove = Mathf.Abs(inputs.x) + Mathf.Abs(inputs.y) != 0;

        wantToJump = Input.GetButton("Jump");
        wantToCrouch = Input.GetButton("Crouch");


        if(crouchTapTimer <= 0) {
            if(Input.GetButtonDown("Crouch") && isTryingToMove) {
                crouchTapTimer = crouchTapTiming;
            }
            wantToRoll = false;
        }
        else {
            //if(isRolling && Input.GetButtonDown("Crouch")) {
            //    wantToPermRoll = true;
            //}

            if(Input.GetButtonUp("Crouch") && canRoll) {
                wantToRoll = true;
            }

            crouchTapTimer -= Time.deltaTime;
        }
        wantToPermRoll = wantToPermRoll && wantToCrouch && isRolling;
    }

    void FixedUpdate() {
        rHit = GetGroundHit();

        //if(!hasAuthority) return;

        RecalculatePhysicsInfo();

        isCrouching = wantToCrouch;

        if(headCollider) {
            if(isRolling) {
                headCollider.localPosition = rollingHeadOffset;
            }
            else if(isGrounded) {
                headCollider.localPosition = Vector3.Lerp(standingHeadOffset, crouchingHeadOffset, crouchAmount);
            }
            else {
                headCollider.localPosition = standingHeadOffset;
            }
        }

        #region Useful Values
        //Some useful values
        Vector3 velocityOnYplane = new Vector3(playerObjectVelocity.x, 0, playerObjectVelocity.z);

        velSqr = velocityOnYplane.sqrMagnitude;
        float fallVel = Mathf.Max(0, -rBody.velocity.y);

        Vector3 CurWorldInputDir = cameraTransform.TransformDirection(new Vector3(inputs.x, 0, inputs.y));
        CurWorldInputDir = new Vector3(CurWorldInputDir.x, 0, CurWorldInputDir.z).normalized;
        if(isRolling) {
            worldInputDir = Vector3.RotateTowards(worldInputDir, CurWorldInputDir, Mathf.Deg2Rad * rollTurnSpeed * Time.deltaTime, .1f).normalized;
        }
        else {
            worldInputDir = CurWorldInputDir;
        }

		Vector3 projectedWorldInputDir = (Vector3.ProjectOnPlane(worldInputDir, rHit.normal));
		float surfaceAngleMult = projectedWorldInputDir.magnitude;//1 - Mathf.Max(0, (Mathf.Abs(projectedWorldInputDir.y) - 0.5f) * 2);
		Vector3.Normalize(projectedWorldInputDir);

		inputDecelMult = Mathf.Clamp01(1 - (Vector3.Dot(velocityOnYplane.normalized, worldInputDir) + 1) * 0.5f);
        inputDecelMult = Mathf.Pow(inputDecelMult, 0.5f);

        float hitHeight = (rHit.distance + legRadius) - legOffset; //DO NOT base on hit point Y pos
        crouchAmount = Mathf.InverseLerp(legStandHeight, legCrouchHeight, hitHeight);

        float slopeDotAngle = (1 - Mathf.Clamp01(Vector3.Dot(Vector3.up, rHit.normal))) * 90;
        float dirSlopeAngleMult = Mathf.Clamp01(-projectedWorldInputDir.y);
        //slopeDotAngle *= dirSlopeAngleMult;
        float slopeAngleMult = Mathf.Clamp01((slopeDotAngle - minSlopeAngle) / (maxSlopeAngle - minSlopeAngle));
        //Vector3 forceDir = slopeDotAngle >= maxSlopeAngle ? rHit.normal : Vector3.up;

        Vector3 flatProjectedNormal = Vector3.ProjectOnPlane(rHit.normal + worldInputDir.normalized * 0.5f, Vector3.up);
        flatProjectedNormal = Vector3.ProjectOnPlane(flatProjectedNormal, rHit.normal).normalized;

        projectedWorldInputDir = Vector3.Slerp(projectedWorldInputDir, flatProjectedNormal, slopeAngleMult);
        inputMagnitude = Mathf.Max(inputMagnitude, slopeAngleMult);
        surfaceAngleMult = inputMagnitude;

        worldInputDir = new Vector3(projectedWorldInputDir.x, 0, projectedWorldInputDir.z).normalized;

        bool forceMove = slopeAngleMult > 0.01f;
        //isTryingToMove =  || isTryingToMove;

        #endregion

        #region Movement

        #region Drag
        float forceMult = fallVel / maxFallSpeed;
		#endregion

		#region Roll Check
		canRoll = velSqr >= (rollVelocityThreshold * rollVelocityThreshold) && rollTimer <= 0 && crouchAmount < (1 - rollStandThreshold) && !isRolling;

		if (wantToPermRoll) {
            isRolling = true;
            rollTimer = 0.01f;
        }
        else if(canRoll && wantToRoll && !isRolling) {
            isRolling = true;
            rollTimer = rollDuration;

			canRoll = false;

			if(isTryingToMove)
			{
				playerObjectVelocity = new Vector3(worldInputDir.x * playerObjectVelocity.magnitude, playerObjectVelocity.y, worldInputDir.z * playerObjectVelocity.magnitude);
			}

        }
        #endregion

        #region Running
        if(!isRolling && isGrounded)
        {
            //Calculate directional stiffness force
            //Vector3 velDir = transform.InverseTransformVector(rBody.velocity);
            //velDir = new Vector3(velDir.x, 0, 0);
            //Vector3 dirForce = -velDir * DirectionStiffness;

            //Get player velocity, calculate rate of acceleration
            float targetMoveSpeed = Mathf.Lerp(maxRunSpeed, maxCrouchSpeed, crouchAmount) * inputMagnitude * surfaceAngleMult;

            Vector3 surfaceVector = Vector3.ProjectOnPlane(playerObjectVelocity.normalized, rHit.normal);
            float surfaceMult = 1;
            if(surfaceVector.y >= 0) {
                surfaceVector.y = 0;
                surfaceMult = surfaceVector.magnitude;
            }

            Vector3 playerVelMs = playerObjectVelocity;
            float playerVelMag = playerVelMs.x * playerVelMs.x + playerVelMs.z * playerVelMs.z;
            runSpeedPercent = Mathf.Pow(Mathf.Max(0, playerVelMag / (targetMoveSpeed * targetMoveSpeed * surfaceMult)), accelCurve);

            float speedControlMult = 0;
            if(runSpeedPercent > 1) {
                speedControlMult = Mathf.Clamp01(runSpeedPercent - 1);
                runSpeedPercent = Mathf.Clamp01(runSpeedPercent);
            }
            float velAccelMult = 1 - runSpeedPercent;

            rawSpeed = isTryingToMove ? Mathf.Sqrt(playerVelMag) : Mathf.Lerp(rawSpeed, 0, Time.fixedDeltaTime * topSpeedBlendRate);
            isRunning = (isTryingToMove || forceMove) && runSpeedPercent > 0.01f;


            //Final steps & force application
            float accelMult = velAccelMult * surfaceMult;

			float finalAccel = (isCrouching ? crouchAccel : standAccel) * accelMult;
            Vector3 finalForce = Vector3.zero;
            if(isTryingToMove || forceMove)
            {
                finalForce = projectedWorldInputDir * finalAccel + -velocityOnYplane.normalized * speedControlMult * deceleration;
            }
            else if(isGrounded && jumpCooldownTimer < jumpCooldownTime * 0.5f)
            {
                finalForce = stopForce;
            }

            Vector3 turnDir = transform.InverseTransformDirection(projectedWorldInputDir).normalized;
            Vector3 turnForce = (turnDir.x * runResponsiveness * transform.right + -transform.forward * Mathf.Max(Mathf.Abs(turnDir.x), -Mathf.Min(0, turnDir.z))) * 0.5f * Mathf.Lerp(standAccel, crouchAccel, crouchAmount);

			finalForce += turnForce;

            Vector3 projectedFinalForce = Vector3.ProjectOnPlane(finalForce, rHit.normal);
            finalForce = Vector3.Slerp(finalForce, projectedFinalForce, slopeAngleMult);

            rBody.AddForce(finalForce, ForceMode.Acceleration);

            if(debugForceVectors) {
                Debug.DrawRay(transform.position, finalForce * 0.25f, Color.green, 0, false);
            }
        }
        #endregion

        #region Airborne
        if(!isRolling) {
            float targetMoveSpeed = Mathf.Lerp(maxRunSpeed, maxCrouchSpeed, crouchAmount);
            float accelMult = 1 - Mathf.Clamp01((velSqr / (targetMoveSpeed * targetMoveSpeed)) * inputMagnitude * surfaceAngleMult);

            Vector3 turnDir = transform.InverseTransformDirection(worldInputDir).normalized;
            Vector3 turnForce = (turnDir.x * airAccel * transform.right + -transform.forward * Mathf.Abs(turnDir.x) * airAccel) * 0.5f;

            rBody.AddForce(worldInputDir * airAccel * accelMult + turnForce, ForceMode.Acceleration);
        }
        #endregion

        #region Rolling
        else {
            if(isGrounded) {
				float timerMult = rollTimer / rollDuration;
				float speedTarget = Mathf.Lerp(maxRunSpeed, rollSpeed, timerMult);

				float accelMult = 1 - Mathf.Clamp(velSqr / (speedTarget * speedTarget * surfaceAngleMult), 0, 2);

                Vector3 turnDir = transform.InverseTransformDirection(projectedWorldInputDir).normalized;
                Vector3 turnForce = (turnDir.x * rollAccel * transform.right + -transform.forward * Mathf.Abs(turnDir.x) * rollAccel) * 0.5f;

                rBody.AddForce(projectedWorldInputDir * accelMult * rollAccel + turnForce, ForceMode.Acceleration);
            }

            if(!wantToPermRoll) {
                rollTimer -= Time.deltaTime;
                isRolling = rollTimer > 0;
            }

			if(velSqr < (rollVelocityThreshold * rollVelocityThreshold))
			{
				isRolling = false;
				rollTimer = -1;
			}
        }
        #endregion

        #endregion

        #region Jumping
        //Jumping
        if(wantToJump && !isRolling && jumpCooldownTimer <= 0)
        {
            if(!hasJumped && isGrounded)
            {
                float jumpForce = Mathf.Sqrt(jumpHeight * Mathf.Abs(Physics.gravity.y) * 2);
                float dragCompensation = jumpForce * rBody.drag * (1 + jumpForce / 10);
                if(jumpForce > playerObjectVelocity.y)
                {
                    Vector3 jumpDir = (Vector3.up + rHit.normal * slopeAngleMult).normalized;

                    rBody.velocity = new Vector3(rBody.velocity.x, 0, rBody.velocity.z);
                    rBody.AddForce(jumpDir * (jumpForce + dragCompensation) + worldInputDir * inputMagnitude * jumpForwardImpulse, ForceMode.VelocityChange);
                }

                isGrounded = false;
                hasJumped = true;
                hasDropped = false;
                canDrop = true;

                jumpCooldownTimer = jumpCooldownTime;

                debugJumpTime = 0;
                debugJumpHeight = 0;
                debugJumpPrevPos = transform.position.y;
            }
        }
        else
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if(hasDropped)
        {
            wantToJump = false;
        }

        if(canDrop && !wantToJump && !isGrounded)
        {
			if(rBody.velocity.y > 0 && hasDropped)
			{
				//Debug.DrawRay(transform.position + transform.forward * -1, Vector3.up * Mathf.Clamp01(jumpAscendStopTimer / jumpAscendStopTime), Color.blue);
				float velY = Mathf.Lerp(0, rBody.velocity.y, Mathf.Clamp01(jumpAscendStopTimer / jumpAscendStopTime));
				rBody.velocity = new Vector3(rBody.velocity.x, velY, rBody.velocity.z);

				jumpAscendStopTimer -= Time.deltaTime;
			}

			rBody.AddForce(Physics.gravity * Mathf.Max(0, jumpDescendGravityMult - 1), ForceMode.Acceleration);
			//rBody.AddForce(-Vector3.up * Mathf.Max(jumpHeight - debugJumpHeight, 0), ForceMode.VelocityChange);
			if(!hasDropped)
			{
				hasDropped = true;
				jumpAscendStopTimer = jumpAscendStopTime;
			}
            
        }

        if(transform.position.y > debugJumpPrevPos && !isGrounded)
        {
            debugJumpHeight += transform.position.y - debugJumpPrevPos;
            debugJumpPrevPos = transform.position.y;

            debugJumpTime += Time.deltaTime;
        }
        #endregion

        #region Leg Spring
        //Leg spring
        float legHeight = isCrouching ? legCrouchHeight : legStandHeight;
        //if (hasValidHit)
        {
			bool needsPulling = !((rHit.distance < legOffset + legHeight - legRadius) || (jumpCooldownTimer > 0));

            float springForce = 0;
			float springLerp = 0;
			//if(isRolling) {
			//    springForce = -Mathf.Clamp01(rHit.distance / groundedDistance + legOffset + legRadius);
			//}
			if (!isRolling && rHit.distance < legOffset + legHeight - legRadius) {
                float lerp = 1 - Mathf.Clamp01(rHit.distance / (legOffset + legHeight - legRadius));
                springForce = Mathf.Lerp(1, legStiffness, lerp);
            }
            else if(jumpCooldownTimer <= 0) {
				float distInvert = (legOffset + groundedDistance) - (rHit.distance + legRadius); 
				//float lerp = 1 - Mathf.Clamp01((rHit.distance - legOffset) / (groundedDistance + legRadius - legOffset + legHeight));
				float lerp = 1 - Mathf.Clamp01((rHit.distance - legOffset - legRadius) / (groundedDistance));
				//springLerp = Mathf.Clamp01(Mathf.InverseLerp(-legHeight + groundedDistance, groundedDistance, distInvert));
				springLerp = 1 - Mathf.Clamp01((distInvert) / (groundedDistance - legHeight));
				float force = Mathf.Lerp(1, legMinForce, springLerp);//Mathf.Lerp(1, -1, Mathf.Max(0, springLerp - 0.1f) * 1.111f);
				springForce = force;				
            }

			//Debug.DrawRay(transform.position + transform.forward * 0.25f, Vector3.up * ((legOffset + groundedDistance) - (rHit.distance + legRadius)), Color.green);
			//Debug.DrawRay(transform.position + transform.forward * 0.35f, Vector3.up * -legHeight, Color.cyan);
			//Debug.DrawRay(transform.position + transform.forward * 0.45f, Vector3.up * groundedDistance, Color.blue);
			//Debug.DrawRay(transform.position + transform.forward * 0.55f, Vector3.up * springLerp, Color.magenta);

			float curLegDist = Mathf.Clamp01(rHit.distance / (legHeight + legOffset + groundedDistance));
            float springMult = 1 - curLegDist;
            float legVel = (prevLegDist - curLegDist) / Time.fixedDeltaTime;
            prevLegDist = curLegDist;

            float dampAdjust = 1 - Mathf.Clamp01((rHit.distance / (legOffset + legHeight - legRadius)) - 1);
            float legDamp = Mathf.Min(legVel * legDamping, rHit.distance > (legOffset + legHeight - legRadius) ? 0 : Mathf.Infinity);
            legDamp = legVel * legDamping * dampAdjust;

            float legForce = Mathf.Clamp(springForce + (isGrounded ? legDamp : 0), (needsPulling) ? legMinForce : 0, isRolling ? 0 : legMaxForce);//Mathf.Clamp(legStiffness * (springMult + legDamp), legMinForce, legMaxForce);

            if(hasValidHit)
            {
                rBody.AddForce(Vector3.up * -Physics.gravity.y * legForce, ForceMode.Acceleration);

				if(rHit.rigidbody != null)
				{
					rHit.rigidbody.AddForceAtPosition(-Vector3.up * -Physics.gravity.y * Mathf.Clamp01(legForce) * Mathf.Min(1, rBody.mass / rHit.rigidbody.mass), rHit.point, ForceMode.Acceleration);
				}
            }

            if(debugForceVectors)
            {
                Debug.DrawRay(transform.position, Vector3.up * legForce * 0.1f, Color.blue, 0, false);
            }

        }
        #endregion

        #region Facing Direction
        //Player facing direction
        //if(velSqr > 1.0f)
        if(velSqr > 1.0f && isTryingToMove)
        {
            Vector3 velocityDirection = playerObjectVelocity;
            desiredFacing = new Vector3(velocityDirection.x, 0, velocityDirection.z).normalized;
        }
        else if(isTryingToMove)
        {
            desiredFacing = worldInputDir;
        }
        else
        {
            desiredFacing = Quaternion.AngleAxis(objectRotationThisFrame, Vector3.up) * transform.forward;
        }

        if(desiredFacing != Vector3.zero)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, desiredFacing, 0.45f * Time.deltaTime * 60, 1f);
        }
        #endregion

        rBody.angularVelocity = Vector3.zero;
    }

    public void ConfirmGrounded() {
        isGrounded = true;
    }

    public void EnablePlayerMovement(bool enable) {
        lowerCollider.gameObject.SetActive(enable);
        upperCollider.gameObject.SetActive(enable);
        enabled = enable;
    }
}
