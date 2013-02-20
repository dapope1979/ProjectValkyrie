using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	void Awake () {
	 	DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI ()
	{   		
		
		ValkyrieShip ship = (ValkyrieShip) GameObject.FindObjectOfType(typeof(ValkyrieShip));
		if ((Network.peerType == NetworkPeerType.Client) &&
				(ship != null) && 
				(ship.helmPlayer == ServerPlayerManager.emptyPlayer) ) {

			GUILayout.BeginArea(new Rect(400, 10, 300, 300));
			if (GUILayout.Button("Take Helm")) {
				ControlManager controlMan = (ControlManager) GameObject.FindObjectOfType(typeof(ControlManager));
				controlMan.TakeTheHelm();				
			}
			GUILayout.EndArea();
		}
	    
	}
	
	
}
