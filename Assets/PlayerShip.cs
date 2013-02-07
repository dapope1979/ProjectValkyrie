using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0.0f, 1.0f * Time.deltaTime, 0.0f));
	}
}
