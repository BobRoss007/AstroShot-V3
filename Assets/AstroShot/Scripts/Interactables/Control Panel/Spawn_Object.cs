using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Object : MonoBehaviour {
	
	public Transform spawnPoint;
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}

		public void SpawnObject()
	{
		if(spawnPoint && prefab)
		{
			Instantiate(prefab,spawnPoint.position,spawnPoint.rotation);
		}
		else
		{
			Debug.LogError("no spawnpoint assigned");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
