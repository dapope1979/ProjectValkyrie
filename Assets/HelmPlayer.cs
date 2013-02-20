using UnityEngine;
using System.Collections;

public class HelmPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		
		ValkyrieShip ship = (ValkyrieShip) GameObject.FindObjectOfType(typeof(ValkyrieShip));
		if ((ship != null) && (ship.helmPlayer == Network.player) ) {
			if (GUILayout.Button("Thrust")) {
				Debug.Log("button tapped");
				ControlManager controlMan = (ControlManager) GameObject.FindObjectOfType(typeof(ControlManager));
				controlMan.Thrust();
			}
		}
	}
	
	[RPC]
	void Thrust(NetworkPlayer player) {
	}
}
