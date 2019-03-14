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
			GUILayout.BeginArea(new Rect(400, 10, 300, 300));
			if (GUILayout.Button("Leave Station")) {
				Debug.Log("button tapped");
				ControlManager controlMan = (ControlManager) GameObject.FindObjectOfType(typeof(ControlManager));
				controlMan.LeaveStation();
			}
			if (GUILayout.Button("Thrust")) {
				ControlManager controlMan = (ControlManager) GameObject.FindObjectOfType(typeof(ControlManager));
				controlMan.Thrust();
			}
			GUILayout.EndArea();
		}
	}
	
	[RPC]
	void Thrust(NetworkPlayer player) {
	}
}
