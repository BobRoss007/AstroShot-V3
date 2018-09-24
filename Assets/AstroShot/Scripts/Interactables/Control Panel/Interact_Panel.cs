using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //Must add this to get access to the UnityEvent stuff

public class Interact_Panel : MonoBehaviour {
  
	public LayerMask layerMask;
	public GameObject interactText;
	public Vector3 size = Vector3.one;
	public UnityEvent onInteract;
	public UnityEvent onActivate;
	public UnityEvent onDeactivate;
	
	Collider[] overlappedCols = new Collider[10];
	int overlaps;
	bool canInteract;
	bool previousInteract;

	void Start ()
	{

	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.E) && canInteract)
		{
			onInteract.Invoke();
		}
		interactText.SetActive(canInteract);
	}

	// void OnTriggerStay(Collider col)
	// {
	// 	if(col.CompareTag("Player"))
	// 	{
	// 		if(Input.GetKeyDown(KeyCode.E))
	// 		{
	// 			SpawnObject();
	// 		}
	// 	}
	// }

	void FixedUpdate () 
	{
		canInteract = false;
		overlaps = Physics.OverlapBoxNonAlloc(transform.position, size * 0.5f, overlappedCols, transform.rotation, layerMask);
		for(int i = 0; i < overlaps; i++)
		{
			if (overlappedCols[i].CompareTag("Player"))
			{
				canInteract = true; 
			}
		}
		if (previousInteract != canInteract)
		{
			if (canInteract)
			{
				if (onActivate != null) 
				onActivate.Invoke();
			}
			else
			{
				if (onDeactivate != null)
				onDeactivate.Invoke();
			}
		}
		previousInteract = canInteract;
	}	
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, size);
    	Gizmos.DrawWireCube(Vector3.zero, Vector3.one); 
	}
}
