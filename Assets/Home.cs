using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(20,20,80,20), "Server")) {
			Application.LoadLevel ("Server");
		}
		if (GUI.Button(new Rect(120,20,80,20), "Client")) {
			Application.LoadLevel ("testLevel");
		}
	}	
}
