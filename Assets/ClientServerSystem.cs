using UnityEngine;
using System.Collections;
using System;

public class ClientServerSystem : MonoBehaviour {
	public int connectionPort = 25001;
	private string connectionIP = "server address";
	private string initialIP = "server address";
	private bool showJoinHost = true;
	private bool showConnect = false;
	private bool showConnected = false;
	NetworkConnectionError error = NetworkConnectionError.NoError;
	
	private bool fieldHasFocus(string fieldName) {
		return GUI.GetNameOfFocusedControl() == fieldName;
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
    	showConnect = false;
		showJoinHost = true;
		showConnected = false;
	}
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		showConnect = false;
		showJoinHost = true;
		showConnected = false;
	}	
	
    void OnGUI()
    {		
		if ((Network.peerType == NetworkPeerType.Client) || (Network.peerType == NetworkPeerType.Server)) {
			showConnect = false;
			showJoinHost = false;
			showConnected = true;
		}
		
		if (showJoinHost) {
			if (GUI.Button(new Rect(10, 10, 40, 20), "Join"))
            {
				showConnect=true;
				showJoinHost=false;
			}
			if (GUI.Button(new Rect(60, 10, 40, 20), "Host"))
            {
				showJoinHost=false;
				Network.InitializeServer(32, connectionPort, false);
			}
		}
		
		if (showConnect) {
			if (fieldHasFocus("ipAddressTextField") && (connectionIP == initialIP)) {
				connectionIP = GUI.TextField (new Rect (10, 10, 110, 20), "");
			}
			else if (showConnect) {
				GUI.SetNextControlName ("ipAddressTextField");
				connectionIP = GUI.TextField (new Rect (10, 10, 110, 20), connectionIP);
				if (GUI.Button(new Rect(130, 10, 40, 20), "Join"))
	            {
					showConnect=false;
					showJoinHost=false;
					showConnected=true;
					Network.Connect(connectionIP, connectionPort);
				}
			}
		}
		
		if (showConnected) {
			if (Network.peerType == NetworkPeerType.Server) { 
				GUI.Label(new Rect(10, 10, 300, 20), "Status: Hosting a game");
			}
			else if (Network.peerType == NetworkPeerType.Client) {
				GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected");
			}
			else {
				GUI.Label(new Rect(10, 10, 300, 20), "Status: Connecting");
			}
		}	
    }
}
