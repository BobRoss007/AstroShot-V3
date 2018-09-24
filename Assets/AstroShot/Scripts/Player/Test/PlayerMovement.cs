using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GibTreaty {
    [AddComponentMenu("GibTreaty/Player Movement")]
    public class PlayerMovement : NetworkBehaviour {

        [Header("Component Links")]
        [SerializeField]
        Transform _headCollider;

        [SerializeField]
        Vector3 standingHeadOffset = new Vector3(0, 1.4f, 0);

        [SerializeField]
        Vector3 crouchingHeadOffset = new Vector3(0, 1.2f, 0.2f);

        [SerializeField]
        Vector3 rollingHeadOffset = new Vector3(0, 0.95f, 0);

        [Space]
        [SerializeField]
        GeneralSettings _general = new GeneralSettings();

        [SerializeField]
        RunSettings _running = new RunSettings();

        [SerializeField]
        JumpSettings _jumping = new JumpSettings();

        [SerializeField]
        RollSettings _rolling = new RollSettings();

        [SerializeField]
        LegSpringSettings _legSpring = new LegSpringSettings();

        [Header("Debugging")]
        [SerializeField]
        bool _debugInputDirection = false;

        [SerializeField]
        bool _debugSpherecast = false;

        [SerializeField]
        bool _debugLegTargets = false;

        [SerializeField]
        bool _debugStopPosition = false;

        Transform cameraTransform;
        Vector3 desiredFacing = Vector3.forward;

        float crouchTapTimer = 0;
        float jumpTimer = 0;
        Vector3 stopPos = Vector3.zero;
        Vector3 stopPosFront = Vector3.zero;
        float prevStopDist = 0;
        Vector3 prevObjectLocalStopPos = Vector3.zero;
        Vector3 stopForce = Vector3.zero;
        Vector3 playerObjectVelocity = Vector3.zero;
        Transform objectTransform;
        Quaternion lastObjectRotation = Quaternion.identity;
        float objectRotationThisFrame = 0;
        float velocitySqr = 0;

        float fallVelocity = 0;
        Vector3 horizontalPlaneVelocity = Vector3.zero;
        Vector3 worldInputDirection = Vector3.zero;
        Vector2 inputs = Vector2.zero;
        float inputMagnitude = 0;
        bool isTryingToMove = false;
        bool hasJumped = false;
        bool wantToJump = false;
        bool wantToRoll = false;
        bool wantToCrouch = false;
        bool hasValidHit = false;

        float inputAccelMult = 0;
        float prevLegDist = 0;

        #region Properties
        public float CrouchAmount { get; set; }

        public Vector3 CrouchingHeadOffset {
            get {
                return crouchingHeadOffset;
            }

            set {
                crouchingHeadOffset = value;
            }
        }

        public bool DebugInputDirection {
            get {
                return _debugInputDirection;
            }

            set {
                _debugInputDirection = value;
            }
        }

        public bool DebugLegTargets {
            get {
                return _debugLegTargets;
            }

            set {
                _debugLegTargets = value;
            }
        }

        public bool DebugSpherecast {
            get {
                return _debugSpherecast;
            }

            set {
                _debugSpherecast = value;
            }
        }

        public bool DebugStopPosition {
            get {
                return _debugStopPosition;
            }

            set {
                _debugStopPosition = value;
            }
        }

        public GeneralSettings General {
            get { return _general; }
        }

        public Transform HeadCollider {
            get {
                return _headCollider;
            }

            set {
                _headCollider = value;
            }
        }

        public RaycastHit Hit { get; set; }

        public bool IsCrouching { get; set; }

        public bool IsGrounded { get; set; }

        public bool IsRolling { get; set; }

        public bool IsRunning { get; set; }

        public JumpSettings Jumping {
            get { return _jumping; }
        }

        public Rigidbody LastRigidbody { get; set; }

        public float RawSpeed { get; set; }

        public Rigidbody RigidbodyComponent { get; private set; }

        public RollSettings Rolling {
            get { return _rolling; }
        }

        public Vector3 RollingHeadOffset {
            get {
                return rollingHeadOffset;
            }

            set {
                rollingHeadOffset = value;
            }
        }

        public float RollTimer { get; set; }

        public RunSettings Running {
            get { return _running; }
        }

        public float RunSpeedPercent { get; set; }

        public Vector3 StandingHeadOffset {
            get {
                return standingHeadOffset;
            }

            set {
                standingHeadOffset = value;
            }
        }
        #endregion

        public void Initialize() {
            RigidbodyComponent = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
        }

        void Awake() {
            Initialize();
        }

        void Update() {
            if(!hasAuthority) return;

            UpdateInput();
        }

        void FixedUpdate() {
            Hit = GetGroundHit();

            if(hasValidHit)
                RecalculatePhysicsInfo();

            IsCrouching = wantToCrouch;

            UpdateHeadColliderPosition();
            UpdateWorldInput();

            fallVelocity = Mathf.Max(0, -RigidbodyComponent.velocity.y);

            #region Drag - Unused
            float forceMultiplier = fallVelocity / General.MaxFallSpeed;
            #endregion

            RollCheck();

            if(!IsRolling)
                UpdateRunning();
            else
                UpdateRolling();

            UpdateJumping();
            UpdateLegSpring();
            UpdateFacingDirection();

            RigidbodyComponent.angularVelocity = Vector3.zero;
        }

        void OnGUI() {
            GUILayout.Label("Input Direction Dot " + inputAccelMult);
            GUILayout.Label("Input Magnitude " + inputMagnitude);
            GUILayout.Label("Player Velocity " + RawSpeed);
            GUILayout.Label("Crouch Amount " + CrouchAmount);
            GUILayout.Label("");
            GUILayout.Label("rHit distance " + Hit.distance);
            GUILayout.Label("isRolling " + IsRolling.ToString());
        }

        void OnDrawGizmos() {
            if(DebugInputDirection) {
                Debug.DrawRay(transform.position, worldInputDirection, Color.white, 0, false);
                Debug.DrawRay(transform.position, worldInputDirection + Vector3.up * 0.05f, Color.black, 0, false);
                Debug.DrawRay(transform.position, playerObjectVelocity, Color.yellow, 0, false);
            }

            if(DebugSpherecast) {
                Gizmos.color = new Color(0, 1, 0, 0.25f);
                Gizmos.DrawSphere(transform.position + transform.up * (_legSpring.LegOffset), _legSpring.LegRadius);
                Gizmos.DrawSphere(transform.position + transform.up * (_legSpring.LegOffset - _legSpring.GroundedDistance - _legSpring.LegOffset), _legSpring.LegRadius);

                Gizmos.color = new Color(0, 1, 1, 0.3f);
                Gizmos.DrawSphere(transform.position + transform.up * _legSpring.LegOffset + -transform.up * Hit.distance, _legSpring.LegRadius);

                Debug.DrawRay(transform.position + transform.up * (_legSpring.LegOffset), -Vector3.up * (_legSpring.GroundedDistance + _legSpring.LegOffset), Color.red, 0, false);
            }

            if(DebugLegTargets) {
                //Gizmos.color = Color.white;
                //Gizmos.DrawSphere(transform.position + transform.up * -legStandHeight, legRadius * 0.5f);
                Debug.DrawLine(transform.position + transform.up * -_legSpring.LegStandHeight + transform.right * 0.1f, transform.position + transform.up * -_legSpring.LegStandHeight + transform.right * -0.1f, Color.white, 0, false);
                Debug.DrawLine(transform.position + transform.up * -_legSpring.LegCrouchHeight + transform.right * 0.1f, transform.position + transform.up * -_legSpring.LegCrouchHeight + transform.right * -0.1f, Color.white, 0, false);
            }

            if(DebugStopPosition) {
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


        }

        public void ConfirmGrounded() {
            IsGrounded = true;
        }

        RaycastHit GetGroundHit() {
            float groundDist = _legSpring.GroundedDistance + _legSpring.LegOffset;

            RaycastHit rHit = new RaycastHit();

            RaycastHit[] sHits = Physics.SphereCastAll(transform.position + transform.up * (_legSpring.LegOffset), _legSpring.LegRadius, -Vector3.up, groundDist, _legSpring.LayersToHit);
            float hitDist = -1;
            for(int i = 0; i < sHits.Length; i++) {
                //Debug.Log("SphereCast Hit - " + sHits[i].collider.name);
                if(hitDist == -1) {
                    if(sHits[i].distance < groundDist) {
                        hitDist = sHits[i].distance;
                        rHit = sHits[i];

                        hasValidHit = true;
                    }
                }
                else if(sHits[i].distance < hitDist) {
                    hitDist = sHits[i].distance;
                    rHit = sHits[i];

                    hasValidHit = true;
                }
            }

            if(hitDist == -1) {
                IsGrounded = false;
                hasValidHit = false;

                rHit.distance = groundDist + _legSpring.LegRadius;
                return rHit;
            }
            //Debug.Log("Used Hit - " + rHit.collider.name);

            //Debug.DrawRay(transform.position + transform.up * (legOffset + legRadius), -Vector3.up * groundedDistance, new Color(0, 1, 0, 0.25f), 0, false);
            //Debug.DrawRay(rHit.point, rHit.normal, new Color(1, 0, 1, 0.5f), 0, false);

            float groundAngle = Vector3.Angle(rHit.normal, Vector3.up);
            if(groundAngle < 70 && rHit.distance > 0) {
                IsGrounded = true;
            }
            else {
                IsGrounded = false;
            }

            if(IsGrounded && !wantToJump) {
                hasJumped = false;
            }

            return rHit;
        }

        void RecalculatePhysicsInfo() {
            LastRigidbody = Hit.rigidbody;


            if(LastRigidbody != null) {
                playerObjectVelocity = RigidbodyComponent.velocity - LastRigidbody.GetPointVelocity(Hit.point);
            }
            else {
                playerObjectVelocity = RigidbodyComponent.velocity;
            }

            objectRotationThisFrame = 0;
            if(Hit.transform == objectTransform) {
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

                objectTransform = Hit.transform;
            }
            //Debug.Log(objectRotationThisFrame);

            stopPos.y = 0;
            Vector3 playerPos = transform.position;
            playerPos.y = 0;
            float stopPosDist = Vector3.Distance(playerPos, stopPos);
            Vector3 stopDir = (playerPos - stopPos);
            stopDir = new Vector3(stopDir.x, 0, stopDir.z).normalized;
            if(stopPosDist > Running.StopSpringDistance) {
                stopPos = playerPos + -stopDir * Running.StopSpringDistance;
                stopPosFront = stopPos + stopDir * Running.StopSpringDistance;
            }

            float predictedStopDist = Vector3.Distance(playerPos + playerObjectVelocity * Time.fixedDeltaTime, stopPos);
            float forceMag = Mathf.Clamp01(predictedStopDist / (Running.StopSpringDistance)) * Running.Deceleration;

            Vector3 stopDirFront = (playerPos - stopPosFront).normalized;
            float predictedStopDistFront = Vector3.Distance(playerPos + playerObjectVelocity * Time.fixedDeltaTime, stopPosFront);
            float forceMagFront = Mathf.Clamp01(predictedStopDistFront / Running.StopSpringDistance) * Running.Deceleration;

            stopForce = -stopDir * forceMag + -stopDirFront * forceMagFront;

            prevStopDist = predictedStopDist;
            prevObjectLocalStopPos = Hit.transform.InverseTransformPoint(stopPos);
        }

        void RollCheck() {
            if(velocitySqr >= Rolling.RollVelocityThreshold && wantToRoll && !IsRolling) {
                IsRolling = true;
                RollTimer = Rolling.RollDuration;
            }
        }

        void UpdateLegSpring() {
            float legHeight = IsCrouching ? _legSpring.LegCrouchHeight : _legSpring.LegStandHeight;
            float springForce = 0;

            if(IsRolling) {
                springForce = -Mathf.Clamp01(Hit.distance / _legSpring.GroundedDistance + _legSpring.LegOffset + _legSpring.LegRadius);
            }
            else if(Hit.distance < _legSpring.LegOffset + legHeight - _legSpring.LegRadius) {
                float lerp = 1 - Mathf.Clamp01(Hit.distance / (_legSpring.LegOffset + legHeight - _legSpring.LegRadius));
                springForce = Mathf.Lerp(1, _legSpring.LegStiffness, lerp);
            }
            else if(jumpTimer <= 0) {
                float lerp = 1 - Mathf.Clamp01((Hit.distance - _legSpring.LegOffset) / (_legSpring.GroundedDistance + _legSpring.LegRadius - _legSpring.LegOffset + legHeight));
                float force = Mathf.Lerp(1, -1, lerp);
                springForce = force;
            }

            float curLegDist = Mathf.Clamp01(Hit.distance / (legHeight + _legSpring.LegOffset + _legSpring.GroundedDistance));
            float springMult = 1 - curLegDist;
            float legVel = (prevLegDist - curLegDist) / Time.fixedDeltaTime;

            prevLegDist = curLegDist;

            float dampAdjust = 1 - Mathf.Clamp01((Hit.distance / (_legSpring.LegOffset + legHeight - _legSpring.LegRadius)) - 1);
            float legDamp = Mathf.Min(legVel * _legSpring.LegDamping, Hit.distance > (_legSpring.LegOffset + legHeight - _legSpring.LegRadius) ? 0 : Mathf.Infinity);

            legDamp = legVel * _legSpring.LegDamping * dampAdjust;

            float legForce = Mathf.Clamp(springForce + legDamp, jumpTimer > 0 ? 0 : _legSpring.LegMinForce, IsRolling ? 0 : _legSpring.LegMaxForce);//Mathf.Clamp(legStiffness * (springMult + legDamp), legMinForce, legMaxForce);

            if(hasValidHit)
                RigidbodyComponent.AddForce(Vector3.up * -Physics.gravity.y * legForce, ForceMode.Acceleration);

            float hitHeight = (Hit.distance + _legSpring.LegRadius) - _legSpring.LegOffset;//transform.InverseTransformPoint(rHit.point).y;

            CrouchAmount = Mathf.InverseLerp(_legSpring.LegStandHeight, _legSpring.LegCrouchHeight, hitHeight);
            //Debug.DrawRay(transform.position + -transform.forward * 0.1f, Vector3.up * legDamp, Color.green, 0, false);            
        }

        void UpdateInput() {
            inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputMagnitude = Mathf.Clamp01(inputs.magnitude);
            isTryingToMove = Mathf.Abs(inputs.x) + Mathf.Abs(inputs.y) != 0;

            wantToJump = Input.GetButton("Jump");
            wantToCrouch = Input.GetButton("Crouch");

            if(Input.GetButtonDown("Crouch")) {
                crouchTapTimer = Rolling.CrouchTapTiming;
            }

            if(crouchTapTimer > 0) {
                if(!wantToCrouch) {
                    wantToRoll = true;
                }
                crouchTapTimer -= Time.deltaTime;
            }
            else {
                wantToRoll = false;
            }
        }

        void UpdateFacingDirection() {
            if(velocitySqr > 1.0f) {
                Vector3 velocityDirection = playerObjectVelocity;
                desiredFacing = new Vector3(velocityDirection.x, 0, velocityDirection.z).normalized;
            }
            else if(isTryingToMove)
                desiredFacing = worldInputDirection;
            else
                desiredFacing = Quaternion.AngleAxis(objectRotationThisFrame, Vector3.up) * transform.forward;


            if(desiredFacing != Vector3.zero)
                transform.forward = Vector3.RotateTowards(transform.forward, desiredFacing, 0.45f * Time.deltaTime * 60, 1f);
        }

        void UpdateHeadColliderPosition() {
            if(HeadCollider)
                if(IsRolling)
                    HeadCollider.localPosition = RollingHeadOffset;
                else if(IsGrounded)
                    HeadCollider.localPosition = Vector3.Lerp(StandingHeadOffset, CrouchingHeadOffset, CrouchAmount);
                else
                    HeadCollider.localPosition = StandingHeadOffset;
        }

        void UpdateJumping() {
            if(wantToJump && !IsRolling && jumpTimer <= 0) {
                if(!hasJumped && IsGrounded) {
                    float jumpForce = Mathf.Sqrt(Jumping.JumpHeight * Mathf.Abs(Physics.gravity.y) * 2);
                    if(jumpForce > playerObjectVelocity.y) {
                        RigidbodyComponent.velocity = new Vector3(RigidbodyComponent.velocity.x, 0, RigidbodyComponent.velocity.z);
                        RigidbodyComponent.AddForce(Vector3.up * jumpForce + worldInputDirection * inputMagnitude * Jumping.JumpForwardImpulse, ForceMode.VelocityChange);
                    }

                    IsGrounded = false;
                    hasJumped = true;

                    jumpTimer = Jumping.JumpResetTime;
                }
            }
            else {
                jumpTimer -= Time.deltaTime;
            }
        }

        void UpdateRolling() {
            if(IsGrounded) {
                float accelMult = 1 - Mathf.Clamp01(velocitySqr / (Rolling.RollSpeed * Rolling.RollSpeed));
                RigidbodyComponent.AddForce(worldInputDirection * accelMult * Rolling.RollAcceleration, ForceMode.Acceleration);
            }

            RollTimer -= Time.deltaTime;
            IsRolling = RollTimer > 0;
        }

        void UpdateRunning() {
            //Calculate directional stiffness force
            //Vector3 velDir = transform.InverseTransformVector(rBody.velocity);
            //velDir = new Vector3(velDir.x, 0, 0);
            //Vector3 dirForce = -velDir * DirectionStiffness;

            //Get player velocity, calculate rate of acceleration
            float moveSpeed = IsCrouching ? Running.MaxCrouchSpeed : Running.MaxRunSpeed;

            Vector3 playerVelMs = playerObjectVelocity;
            playerVelMs.y = 0;
            float playerVelMag = playerVelMs.x * playerVelMs.x + playerVelMs.z * playerVelMs.z;
            RunSpeedPercent = Mathf.Pow(Mathf.Clamp01(playerVelMag / (moveSpeed * moveSpeed)), Running.AccelerationCurve);
            float velAccelMult = 1 - RunSpeedPercent;

            RawSpeed = Mathf.Sqrt(playerVelMag);
            IsRunning = isTryingToMove && RunSpeedPercent > 0.01f;

            inputAccelMult = Mathf.Clamp01(1 - (Vector3.Dot(horizontalPlaneVelocity.normalized, worldInputDirection) + 1) * 0.5f);

            //Calculate direction vectors
            Vector3 fwd = cameraTransform.rotation * Vector3.forward;
            fwd.y = 0;
            fwd = fwd.normalized;
            fwd = (Vector3.ProjectOnPlane(fwd, Hit.normal) + fwd).normalized;

            Vector3 right = cameraTransform.rotation * Vector3.right;
            right.y = 0;
            right = right.normalized;
            right = (Vector3.ProjectOnPlane(right, Hit.normal) + right).normalized;

            //Final steps & force application
            float accelMult = Mathf.Max(inputAccelMult, velAccelMult);
            float finalAccel = (IsGrounded ? (IsCrouching ? Running.CrouchAcceleration : Running.StandAcceleration) : Running.AirAcceleration) * accelMult;
            Vector3 finalForce = Vector3.zero;
            if(isTryingToMove) {
                finalForce = (inputs.x * finalAccel * right) + (inputs.y * finalAccel * fwd);
            }
            else if(IsGrounded && jumpTimer < Jumping.JumpResetTime * 0.5f) {
                finalForce = stopForce;
            }
            RigidbodyComponent.AddForce(finalForce /*+ dirForce*/, ForceMode.Acceleration);
        }

        void UpdateWorldInput() {
            horizontalPlaneVelocity = new Vector3(playerObjectVelocity.x, 0, playerObjectVelocity.z);
            velocitySqr = horizontalPlaneVelocity.sqrMagnitude;

            Vector3 currentWorldInputDirection = cameraTransform.TransformDirection(new Vector3(inputs.x, 0, inputs.y));

            currentWorldInputDirection = new Vector3(currentWorldInputDirection.x, 0, currentWorldInputDirection.z).normalized;

            if(IsRolling)
                worldInputDirection = Vector3.RotateTowards(worldInputDirection, currentWorldInputDirection, Rolling.RollTurnSpeed, .1f).normalized;
            else
                worldInputDirection = currentWorldInputDirection;
        }

        [System.Serializable]
        public class GeneralSettings {
            [SerializeField]
            float _maxFallSpeed = 30f;

            #region Properties
            public float MaxFallSpeed {
                get { return _maxFallSpeed; }
                set { _maxFallSpeed = value; }
            }
            #endregion
        }

        [System.Serializable]
        public class JumpSettings {
            [SerializeField]
            float _jumpHeight = 4f;

            [SerializeField]
            float _jumpForwardImpulse = 3f;

            [SerializeField]
            float _jumpResetTime = 0.5f;

            #region Properties
            public float JumpForwardImpulse {
                get { return _jumpForwardImpulse; }
                set { _jumpForwardImpulse = value; }
            }

            public float JumpHeight {
                get { return _jumpHeight; }
                set { _jumpHeight = value; }
            }

            public float JumpResetTime {
                get { return _jumpResetTime; }
                set { _jumpResetTime = value; }
            }
            #endregion
        }

        [System.Serializable]
        public class LegSpringSettings {
            [SerializeField]
            LayerMask _layersToHit;

            [SerializeField]
            float _groundedDistance = 0.3f;

            [SerializeField]
            float _legStandHeight = 0f;

            [SerializeField]
            float _legCrouchHeight = -0.35f;

            [SerializeField]
            float _legRadius = 0.32f;

            [SerializeField]
            float _legOffset = 0.95f;

            [Space]
            [SerializeField]
            float _legStiffness = 4f;

            [SerializeField]
            float _legDamping = 2.0f;

            [SerializeField]
            float _legMaxForce = 5f;

            [SerializeField]
            float _legMinForce = -2.5f;

            #region Properties
            public float GroundedDistance {
                get { return _groundedDistance; }
                set { _groundedDistance = value; }
            }

            public float LegCrouchHeight {
                get { return _legCrouchHeight; }
                set { _legCrouchHeight = value; }
            }

            public float LegDamping {
                get { return _legDamping; }
                set { _legDamping = value; }
            }

            public float LegMaxForce {
                get { return _legMaxForce; }
                set { _legMaxForce = value; }
            }

            public float LegOffset {
                get { return _legOffset; }
                set { _legOffset = value; }
            }

            public float LegMinForce {
                get { return _legMinForce; }
                set { _legMinForce = value; }
            }

            public float LegRadius {
                get { return _legRadius; }
                set { _legRadius = value; }
            }

            public float LegStandHeight {
                get { return _legStandHeight; }
                set { _legStandHeight = value; }
            }

            public float LegStiffness {
                get { return _legStiffness; }
                set { _legStiffness = value; }
            }

            public LayerMask LayersToHit {
                get { return _layersToHit; }
                set { _layersToHit = value; }
            }
            #endregion
        }

        [System.Serializable]
        public class RollSettings {
            [SerializeField]
            float _rollAcceleration = 60f;

            [SerializeField]
            float _rollSpeed = 14f;

            [SerializeField]
            float _rollTurnSpeed = 0.15f;

            [SerializeField]
            float _rollDuration = 0.5f;

            [Space]
            [SerializeField]
            float _crouchTapTiming = 0.3f;

            [SerializeField]
            float _rollVelocityThreshold = 2.5f;

            #region Properties
            public float CrouchTapTiming {
                get { return _crouchTapTiming; }
                set { _crouchTapTiming = value; }
            }

            public float RollAcceleration {
                get { return _rollAcceleration; }
                set { _rollAcceleration = value; }
            }

            public float RollDuration {
                get { return _rollDuration; }
                set { _rollDuration = value; }
            }

            public float RollSpeed {
                get { return _rollSpeed; }
                set { _rollSpeed = value; }
            }

            public float RollTurnSpeed {
                get { return _rollTurnSpeed; }
                set { _rollTurnSpeed = value; }
            }

            public float RollVelocityThreshold {
                get { return _rollVelocityThreshold; }
                set { _rollVelocityThreshold = value; }
            }
            #endregion
        }

        [System.Serializable]
        public class RunSettings {
            [SerializeField]
            float _standAcceleration = 15f;

            [SerializeField]
            float _crouchAcceleration = 20f;

            [SerializeField]
            float _airAcceleration = 5;

            [SerializeField]
            float _accelerationCurve = 2.0f;

            [SerializeField]
            float _maxRunSpeed = 6f;

            [SerializeField]
            float _maxCrouchSpeed = 3f;

            [Space]
            [SerializeField]
            float _deceleration = 15f;

            [SerializeField]
            float _stopDamping = 20f;

            [SerializeField]
            float _stopSpringDistance = 0.05f;

            #region Properties
            public float AccelerationCurve {
                get { return _accelerationCurve; }
                set { _accelerationCurve = value; }
            }

            public float AirAcceleration {
                get { return _airAcceleration; }
                set { _airAcceleration = value; }
            }

            public float CrouchAcceleration {
                get { return _crouchAcceleration; }
                set { _crouchAcceleration = value; }
            }

            public float Deceleration {
                get { return _deceleration; }
                set { _deceleration = value; }
            }

            public float MaxCrouchSpeed {
                get { return _maxCrouchSpeed; }
                set { _maxCrouchSpeed = value; }
            }

            public float MaxRunSpeed {
                get { return _maxRunSpeed; }
                set { _maxRunSpeed = value; }
            }

            public float StandAcceleration {
                get { return _standAcceleration; }
                set { _standAcceleration = value; }
            }

            public float StopDamping {
                get { return _stopDamping; }
                set { _stopDamping = value; }
            }

            public float StopSpringDistance {
                get { return _stopSpringDistance; }
                set { _stopSpringDistance = value; }
            }
            #endregion
        }
    }
}