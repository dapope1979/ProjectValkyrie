using UnityEngine;
using System.Collections;

public class ValkyrieShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[RPC]
	void Thrust(NetworkPlayer player) {
		Debug.Log("Thrust ship");
	}
}
