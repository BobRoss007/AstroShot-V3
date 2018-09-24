using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Low_Gravity_Field : MonoBehaviour {

	public enum TriggerShape { Box, Sphere };

	[Header("--- General Info ---")]
	public TriggerShape triggerShape;
	public Vector3 offset = Vector3.zero;
	public Vector3 size = Vector3.one;
	public float gravityAmount = 1.0f;

	[Header("--- Extras ---")]
	public float quantumFluctuations = 0.0f;
	public float centripetalForce = 0.0f;

	[Header("--- Technical ---")]
	public LayerMask layerMask;
	public int maximumObjects = 1000;

	Collider[] overlappedCols = new Collider[10];
	int overlaps;
	// Use this for initialization
	void Start ()
	{
		overlappedCols = new Collider[maximumObjects];	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate () 
	{
		if(triggerShape == TriggerShape.Box)
		{
			overlaps = Physics.OverlapBoxNonAlloc(transform.TransformPoint(offset), size * 0.5f, overlappedCols, transform.rotation, layerMask);
		}
		else if(triggerShape == TriggerShape.Sphere)
		{
			overlaps = Physics.OverlapSphereNonAlloc(transform.TransformPoint(offset), size.x, overlappedCols, layerMask);
		}

		
		for(int i = 0; i < overlaps; i++)
		{
			Rigidbody rBody = overlappedCols[i].GetComponentInParent<Rigidbody>();
			if(rBody)
			{
				Vector3 quantumFluc = new Vector3(Random.Range(-quantumFluctuations, quantumFluctuations) * 0.5f, Random.Range(-quantumFluctuations, quantumFluctuations) * 0.5f, Random.Range(-quantumFluctuations, quantumFluctuations) * 0.5f);
				rBody.AddForce(-Physics.gravity * gravityAmount + quantumFluc, ForceMode.Acceleration);
				rBody.AddTorque(quantumFluc, ForceMode.Acceleration);

				Vector3 centriForce = (transform.TransformPoint(offset) - rBody.position);
				rBody.AddForce(centriForce.normalized * (1f / Mathf.Max(1, centriForce.magnitude - 1)) * centripetalForce, ForceMode.Acceleration);
			}
		}
	}	

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		if(triggerShape == TriggerShape.Box)
		{
			Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(offset), transform.rotation, size);
    		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}
		else if(triggerShape == TriggerShape.Sphere)
		{
			Gizmos.DrawWireSphere(transform.TransformPoint(offset), size.x);
		}
		
	}
}
