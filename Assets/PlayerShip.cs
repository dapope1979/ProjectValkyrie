using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
	bool moving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			Vector3 moveDir = new Vector3(0, 1, 0);
			float speed = 5;
	    	transform.Translate(speed * moveDir * Time.deltaTime);
		}
	}
	
	[RPC]
	public void ToggleMoving() {
		Debug.Log ("toggling");
		moving = !moving;
	}
}
