using UnityEngine;
using System.Collections;

public class ServerMain : MonoBehaviour {
	//http://3dgep.com/?p=4609#more-4609

	int listenPort = 25000;
	int maxClients = 5;
	int lastLevelPrefix = 0;
	 
	void startServer() {
		Network.InitializeServer(maxClients, listenPort, false);    
	}

	void stopServer() {
		Network.Disconnect();
	}

	void OnGUI ()
	{
		if (Network.peerType == NetworkPeerType.Disconnected) {
			GUILayout.Label("Network server is not running.");
			if (GUILayout.Button ("Start Server"))
			{               
				startServer();  
			}
		}
		else {
			if (Network.peerType == NetworkPeerType.Connecting)
				GUILayout.Label("Network server is starting up...");
			else { 
				GUILayout.Label("Network server is running.");          
				showServerInformation();    
				showClientInformation();
				if (GUILayout.Button("Start Game")) {
					// Load level with incremented level prefix (for view IDs)
   					networkView.RPC( "LoadLevel", RPCMode.AllBuffered, "testLevel", lastLevelPrefix + 1);
				}
			}
			if (GUILayout.Button ("Stop Server"))
			{             
				stopServer();   
			}
		}

		if (GUILayout.Button("Back")) {
			if (Network.peerType == NetworkPeerType.Server) {               
				stopServer();
			}
			Application.LoadLevel ("Home");
			GameObject[] gos = GameObject.FindGameObjectsWithTag("GameController");
			foreach(GameObject go in gos) {
				Destroy(go);
			}
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject go in players) {
				Destroy(go);
			}
			Destroy(this);
		}
	}
	 
	void showClientInformation() {
		GUILayout.Label("Clients: " + Network.connections.Length + "/" + maxClients);
		foreach (NetworkPlayer p in Network.connections) {
			GUILayout.Label(" Player from ip/port: " + p.ipAddress + "/" + p.port); 
		}
	}
	 
	void showServerInformation() {
		GUILayout.Label("IP: " + Network.player.ipAddress + " Port: " + Network.player.port);  
	}

	//[script RequireComponent(ServerPlayerManager)]
	private ServerPlayerManager spm;
	void Awake() {
	 	DontDestroyOnLoad(this);
		spm = (ServerPlayerManager) gameObject.GetComponent(typeof(ServerPlayerManager));
	}
	 
	void OnPlayerConnected(NetworkPlayer player) { 
		spm.spawnPlayer(player);
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected");
		spm.deletePlayer(player);
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
