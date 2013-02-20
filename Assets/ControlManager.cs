using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() {
		if(Input.anyKey) {
			sendInputToServer();
		}
	}
	 
	void sendInputToServer() {
//		Debug.Log("Getting ready to send input");
//		float vertical = Input.GetAxis("Vertical");
//		float horizontal = Input.GetAxis("Horizontal");
//		if((vertical!=0)||(horizontal!=0)) {
//			networkView.RPC("handlePlayerInput",RPCMode.Server, Network.player,vertical, horizontal);
//		}
//		if (Input.GetKeyDown("space")) {
//			//Debug.Log("Space down");
//			networkView.RPC("thrust", RPCMode.Server, Network.player);
//		}
	}
	
	public void Thrust() {		
			//Debug.Log("Space down");
			networkView.RPC("thrust", RPCMode.Server, Network.player);		
	}
	
	public void TakeTheHelm() {
		Debug.Log ("Taking helm");
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject go in gos) {
			Destroy(go);
		}
		networkView.RPC("TakeHelm", RPCMode.Server, Network.player);
	}
	
	public void LeaveStation() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject go in gos) {
			Destroy(go);
		}
		networkView.RPC("LeaveHelm", RPCMode.Server, Network.player);
	}
}