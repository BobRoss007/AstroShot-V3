using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCloning : MonoBehaviour {

	public string cloneTag = "Please Replace Me";
	public GameObject prefabToClone;
	public float spawnDistance = 1f;

	Rigidbody rBody;
	ObjectCloning fellowClone;	
	GameObject lastClone;

	[HideInInspector] public bool hasCloned { private set; get; }

	void Start()
	{
		hasCloned = false;

		rBody = GetComponentInParent<Rigidbody>();
	}

	void OnCollisionEnter(Collision col)
	{
		fellowClone = col.collider.GetComponent<ObjectCloning>();
		
		bool canClone = false;
		if(fellowClone)
		{
			canClone = !fellowClone.hasCloned;
		}

		if(col.collider.tag == cloneTag && !hasCloned && canClone)
		{
			Vector3 randomDirection = new Vector3(Random.Range(-spawnDistance, spawnDistance), Random.Range(-spawnDistance, spawnDistance), Random.Range(-spawnDistance, spawnDistance)).normalized;
			lastClone = Instantiate(prefabToClone, transform.position + randomDirection * spawnDistance, transform.rotation);

			lastClone.GetComponentInParent<Rigidbody>().AddForce(randomDirection, ForceMode.Impulse);
			rBody.AddForce(-randomDirection, ForceMode.Impulse);

			hasCloned = true;
		}

		
	}
}
