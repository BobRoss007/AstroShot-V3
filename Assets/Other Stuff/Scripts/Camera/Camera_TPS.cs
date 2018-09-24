using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_TPS : MonoBehaviour {

	[Header("--- General ---")]
	public Transform target;
	public Canvas ZoomingCanvas;

    [HideInInspector]
	public bool fpsMode = false;

	[Header("--- Offsets ---")]
	public float standHeight = 1.7f;
	public float crouchHeight = 1.2f;
	public Vector2 distance = new Vector2(0.5f, 8);
	public float startDistance = 1.5f;
	public float sideOffset = 0.5f;
	public float ZoomEye = -0.5f;
	public Vector2 ZoomingDistance = new Vector2(1.0f, 60.0f);
	public float DefaultFOV = 60.0f;
	public float zoomingSpeed = 2.0f;
 
	[Header("--- Collision Detection ---")]
	public float rayRadius = 0.15f;
	public float depthOffset = 0.2f;
	public bool doSideCheck = true;
	public Vector2 sideTransitionLimits = new Vector2(0.5f, 1.0f);
	public float sideTransitionHeight = 0.1f;
	public LayerMask layerMask;


	private Camera camera;
	Vector2 aimAngle = Vector2.zero;
	float scrolledDistance = 0;
    bool isAiming = false;

	bool isCrouched = false;

	RaycastHit rHit;
	float finalDistance = 0;
	float finalSideOffset = 0;
	float finalTransitionHeight = 0;

	bool readAimInput = true;
	
	void Start ()
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        readAimInput = true;

		scrolledDistance = Mathf.Clamp(startDistance, distance.x, distance.y);
		camera = GetComponent<Camera>();
		camera.fieldOfView = Mathf.Clamp(DefaultFOV ,ZoomingDistance.x, ZoomingDistance.y);
	}
	
	
	void PollInputs()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

        

        isAiming = Input.GetMouseButton(1);
		
		readAimInput = (Cursor.lockState == CursorLockMode.Locked);

		Vector2 mouseInput = readAimInput ? new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y")) : Vector2.zero;

		aimAngle += mouseInput;
		aimAngle = new Vector2(aimAngle.x % 360.0f, Mathf.Clamp(aimAngle.y, -89, 89));

		if(ZoomingCanvas)
		{
			ZoomingCanvas.enabled = isAiming;
		}

		if (!isAiming)
		{
			camera.fieldOfView = DefaultFOV;
			scrolledDistance -= Input.mouseScrollDelta.y * 0.5f;
			scrolledDistance = Mathf.Clamp(scrolledDistance, distance.x, distance.y);
		}
		else
		{
			camera.fieldOfView -= Input.mouseScrollDelta.y * zoomingSpeed;
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, ZoomingDistance.x, ZoomingDistance.y);
		}
		
		isCrouched = Input.GetKey(KeyCode.LeftShift);

	}

	void CalculateCameraDistance(Vector3 dirFwd, Vector3 dirRight, Vector3 startPos)
	{
        float desiredDistance;// = isAiming ? ZoomEye : scrolledDistance;
        if(isAiming || fpsMode)
        {
            desiredDistance = ZoomEye;
        }
        else
        {
            desiredDistance = scrolledDistance;
        }

		float sideRayDist = sideTransitionLimits.y + rayRadius;

		
		if(doSideCheck)
		{
			finalSideOffset = sideOffset;
			finalTransitionHeight = 0;
			if(Physics.SphereCast(startPos + -dirRight * depthOffset, rayRadius, dirRight, out rHit, sideRayDist, layerMask, QueryTriggerInteraction.Ignore))
			{
				float distanceToHit = Vector3.Distance(startPos, rHit.point);
				finalSideOffset = Mathf.Min(sideRayDist, rHit.distance);

				float sideLerp = Mathf.InverseLerp(sideRayDist, sideTransitionLimits.x, finalSideOffset);
				sideLerp = Mathf.Clamp01(sideLerp);

				finalSideOffset = Mathf.Lerp(sideOffset, -sideOffset, sideLerp);
				finalTransitionHeight = Mathf.Sin(sideLerp * Mathf.PI) * sideTransitionHeight;
			}
		}
		else
		{
			finalSideOffset = 0;
			finalTransitionHeight = 0;
		}
		

		Vector3 newSidePos = startPos + dirRight * finalSideOffset;

		finalDistance = desiredDistance;
		if(Physics.SphereCast(newSidePos + -dirFwd * depthOffset, rayRadius, dirFwd, out rHit, desiredDistance + rayRadius, layerMask, QueryTriggerInteraction.Ignore))
		{
			float distanceToHit = Vector3.Distance(startPos, rHit.point);

			finalDistance = Mathf.Min(desiredDistance, distanceToHit - rayRadius - depthOffset);
			

			if(finalDistance < distance.x)
			{
				finalDistance = -distance.x;
				finalSideOffset = 0;
			}
			else
			{
				finalDistance = Mathf.Max(finalDistance, distance.x);
			}
		}
	}

	void LateUpdate ()
	{
		PollInputs();
		
		Quaternion aimDirXQuat = Quaternion.AngleAxis(aimAngle.x, Vector3.up);
		
		Vector3 aimFwd = aimDirXQuat * Vector3.forward;
		Vector3 aimRight = aimDirXQuat * Vector3.right;

		aimFwd = Quaternion.AngleAxis(aimAngle.y, aimRight) * aimFwd;

		Vector3 headPos = target.position + Vector3.up * (isCrouched ? crouchHeight : standHeight);

		//Also sets finalDistance
		CalculateCameraDistance(-aimFwd, aimRight, headPos);

		transform.rotation = Quaternion.LookRotation(aimFwd, Vector3.up);
		transform.position = headPos + aimRight * finalSideOffset + -aimFwd * finalDistance + Vector3.up * finalTransitionHeight;
	}

	
		
	
}
