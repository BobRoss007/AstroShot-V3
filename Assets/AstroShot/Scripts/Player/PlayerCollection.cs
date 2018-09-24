using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour {
	int coins = 0;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Coin") {
			coins += 1;
			Destroy (other.gameObject);
			Debug.Log (coins);
		}
	}
}
