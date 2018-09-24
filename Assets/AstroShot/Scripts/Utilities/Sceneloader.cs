using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sceneloader : MonoBehaviour {

	
	public void LoadCave () 
	{
		SceneManager.LoadScene("Style Test Inside", LoadSceneMode.Single);
	}
	
	
	void Update () {
		
	}
}
