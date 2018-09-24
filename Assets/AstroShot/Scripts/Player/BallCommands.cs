using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallCommands : MonoBehaviour
{
    public Canvas CommandCanvas;
    public bool GotoCommandMode = false;
    public Camera MainCamera;
    public float MaxPos = 5.0f;
    public GameObject Ball;


    Animator anim;
    Camera_TPS CameraScript;
    AI_BallPhysicsController ballScript;


    void Start ()
    {
        CommandCanvas.enabled = false;
        anim = CommandCanvas.GetComponentInChildren<Animator>();
        CameraScript = MainCamera.GetComponent<Camera_TPS>();
        ballScript = Ball.GetComponent<AI_BallPhysicsController>();
	}

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.C))
        {
            CommandCanvas.enabled = true;
            anim.SetBool("Command", true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("Command", false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(Input.GetKey(KeyCode.LeftAlt))
        {
            GotoCommandMode = false;
        }

        if(GotoCommandMode)
        {
            OnGotoCommandMode();
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            CameraScript.fpsMode = false;
        }

    }

    public void EnterGotoCommandMode()
    {
        GotoCommandMode = true;
    }

    void OnGotoCommandMode()
    {
        //going to FPS mode.. Effect take place in Camera_TPS script
        CameraScript.fpsMode = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, MaxPos))
        {
            //debuging
            Debug.Log(hitInfo.point);
            Debug.DrawRay(ray.origin, ray.direction * MaxPos, Color.yellow);


            if (Input.GetMouseButton(0))
            {
                //send position for the ball to go to
                ballScript.GoToTarget(hitInfo.point, false);

            }
        }
    }
}
