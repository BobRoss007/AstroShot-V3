using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour {

    public PlayerMovement movementScript;

    public Transform root;
    public Transform torso;
    public Transform foot;

    public float forwardGAngle = 25f;
    public float sidewaysGAngle = 10f;
    public float GForceSmoothing = 5.0f;

    public ParticleSystem runParticles;

    Vector3 torsoStartPosition = Vector3.zero;
    Vector3 footStartPosition = Vector3.zero;

    Vector3 prevVelocity = Vector3.zero;
    Vector3 prevGforce = Vector3.zero;

    Vector3 smoothedAngles = Vector3.zero;

    void Start() {
        torsoStartPosition = torso.localPosition;
        footStartPosition = foot.localPosition;
    }


    void LateUpdate() {
        if(movementScript) {
            DoVisuals();
        }

    }

    void DoVisuals() {
        Vector3 curLocVel = transform.InverseTransformVector(movementScript.rBody.velocity);
        Vector3 prevLocVel = transform.InverseTransformVector(prevVelocity);
        prevVelocity = movementScript.rBody.velocity;

        float gForceX = (prevLocVel.x - curLocVel.x) / Time.deltaTime / 10;
        float gForceZ = (prevLocVel.z - curLocVel.z) / Time.deltaTime / 10;

        float xAngle = gForceX * sidewaysGAngle;
        float zAngle = gForceZ * -forwardGAngle;

        if(movementScript.isCrouching) {
            xAngle *= 2;
            zAngle *= 0.5f;
        }

        if(movementScript.isRolling) {
            float rollPercent = Mathf.Clamp01(movementScript.rollTimer / movementScript.rollDuration);

            float smoothLerp = Mathf.Sin(rollPercent * Mathf.PI);
            torso.localPosition = Vector3.Lerp(torsoStartPosition, Vector3.zero, smoothLerp);

            if(movementScript.isGrounded) {
                foot.localPosition = Vector3.Lerp(root.InverseTransformPoint(movementScript.rHit.point), footStartPosition, smoothLerp);
            }
            else {
                Vector3 footCrouchPosition = new Vector3(0, -movementScript.legCrouchHeight, 0);
                foot.localPosition = Vector3.Lerp(foot.localPosition, movementScript.isCrouching ? footCrouchPosition : footStartPosition, smoothLerp);
            }

            float rollRotation = Mathf.Lerp(0, 360, 1 - rollPercent);
            Vector3 localRotation = root.localEulerAngles;
            localRotation.x = rollRotation;

            root.Rotate(360 * (1f / movementScript.rollDuration) * Time.deltaTime, 0, 0);
        }
        else {
            torso.localPosition = torsoStartPosition;

            if(movementScript.rHit.distance < 0) {
                foot.localPosition = new Vector3(0, -movementScript.legStandHeight + movementScript.legOffset - movementScript.legRadius, 0);
            }
            else {
                foot.position = movementScript.rHit.point;
            }
            foot.localPosition = new Vector3(0, foot.localPosition.y, 0);

            smoothedAngles = Vector3.Lerp(smoothedAngles, new Vector3(zAngle, 0, xAngle), GForceSmoothing * Time.deltaTime);
            root.localEulerAngles = smoothedAngles;
        }

        var emitter = runParticles.emission;
        emitter.enabled = movementScript.isGrounded;
    }
}
