using UnityEngine;
using System.Collections;

public class ServerMain : MonoBehaviour {
	//http://3dgep.com/?p=4609#more-4609

	int listenPort = 25000;
	int maxClients = 5;
	 
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
		spm = (ServerPlayerManager) gameObject.GetComponent(typeof(ServerPlayerManager));
	}
	 
	void OnPlayerConnected(NetworkPlayer player) { 
		spm.spawnPlayer(player);
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected");
		spm.deletePlayer(player);
	}
}
