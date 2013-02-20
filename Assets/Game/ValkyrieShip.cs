using UnityEngine;
using System.Collections;

public class ValkyrieShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// most of these are going to be called in by serverPlayerManager
	// use to handle transformations and track ship statistics
	public void Thrust(NetworkPlayer player) {
		transform.position += Vector3.up * 1;
	}
}
