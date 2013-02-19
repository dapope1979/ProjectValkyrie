using UnityEngine;
using System.Collections;

public class ClientMain : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	int lastLevelPrefix = 1;

	void Awake () {
	 	DontDestroyOnLoad(this);
	}
	 
	void connectToServer() {
		Network.Connect(remoteIP, remotePort);  
	}
	 
	void disconnectFromServer() {
		Network.Disconnect();
	}

	void OnGUI ()
	{   
		if (Network.peerType == NetworkPeerType.Disconnected)
		{   
			GUILayout.Label("Not connected to server.");
			GUI.SetNextControlName ("ipAddressTextField");
			remoteIP = GUILayout.TextField (remoteIP);

			if (GUILayout.Button ("Connect to server"))
				connectToServer();                
		}
		else
		{
			if(Network.peerType == NetworkPeerType.Connecting)
				GUILayout.Label("Connecting to server...");
			else {
				GUILayout.Label("Connected to server.");
				GUILayout.Label("IP/port: " + Network.player.ipAddress + "/" + Network.player.port);
			}
			if (GUILayout.Button ("Disconnect"))
				disconnectFromServer();
		}

		if (GUILayout.Button("Back")) {
			if(Network.peerType == NetworkPeerType.Client) {
				disconnectFromServer();
			}
			Application.LoadLevel ("Home");
			Destroy(this);
			GameObject[] gos = GameObject.FindGameObjectsWithTag("GameController");
			foreach(GameObject go in gos) {
				Destroy(go);
			}
		}
	}

	void OnDisconnectedFromServer (NetworkDisconnection info) {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject go in gos) {
			Destroy(go);
		}
	}

	void Update() {
		if(Input.anyKey) {
			sendInputToServer();
		}
	}
	 
	void sendInputToServer() {
		Debug.Log("Getting ready to send input");
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		if((vertical!=0)||(horizontal!=0)) {
			networkView.RPC("handlePlayerInput",RPCMode.Server,Network.player,vertical, horizontal);
		}
	}

	[RPC]
	void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal) {
	}

	[RPC]
	void LoadLevel(string level, int levelPrefix) {
		 Debug.Log("Loading level " + level + " with prefix " + levelPrefix);
		 lastLevelPrefix = levelPrefix;
		 // There is no reason to send any more data over the network on the default 
		 // channel,
		 // because we are about to load the level, because all those objects will get deleted 
		 // anyway
		 Network.SetSendingEnabled(0, false); 
		 // We need to stop receiving because first the level must be loaded.
		 // Once the level is loaded, RPC's and other state update attached to objects in the 
		 // level are allowed to fire
		 Network.isMessageQueueRunning = false;
		   
		 // All network views loaded from a level will get a prefix into their NetworkViewID.
		 // This will prevent old updates from clients leaking into a newly created scene.
		 Network.SetLevelPrefix(levelPrefix);
		 Application.LoadLevel(level);
		 // Allow receiving data again
		 Network.isMessageQueueRunning = true;
		 // Now the level has been loaded and we can start sending out data
		 Network.SetSendingEnabled(0, true);
		
	}
}
