using UnityEngine;
using System.Collections;

public class Helm : MonoBehaviour {
	public PlayerShip ship;
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isClient && Input.anyKeyDown)
		{
			Debug.Log ("sending");
		    ship.networkView.RPC("ToggleMoving", RPCMode.Others);
		}
	}
}