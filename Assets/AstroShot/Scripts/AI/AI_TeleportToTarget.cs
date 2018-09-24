using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_TeleportToTarget : MonoBehaviour
{
    public Transform Targert;
    public float maxDistance = 5.0f;
    public float cooldown = 10.0f;
    public Vector3 offset = Vector3.zero;

    PlayerMovement player;
    Rigidbody rBody;
    float timer;
    [SerializeField]
    float distance;
	
	void Start ()
    {
        player = Targert.GetComponent<PlayerMovement>();
        rBody = GetComponent<Rigidbody>();
        //StartCoroutine("TeleportRoutine");
	}
	
	
    void TeleportToTarget()
    {
        distance = Vector3.Distance(Targert.position, transform.position);

        if (distance >= maxDistance && player.isGrounded && timer >= cooldown)
        {
            transform.position = Targert.position + offset;
            timer = 0;
        }
        //yield return new WaitForSeconds(cooldown);
    }

	void FixedUpdate ()
    {
        timer += Time.deltaTime;
        TeleportToTarget();
    }


}
