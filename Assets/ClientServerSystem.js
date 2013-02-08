#pragma strict

var connectionPort = 25001;
var connectionIP = "server address";
var initialIP = "server address";
var showJoinHost = true;
var showConnect = false;
var showConnected = false;
var error = NetworkConnectionError.NoError;

function fieldHasFocus(fieldName) {
	return GUI.GetNameOfFocusedControl() == fieldName;
}

function OnFailedToConnect(error:NetworkConnectionError) {
	showConnect = false;
	showJoinHost = true;
	showConnected = false;
}
function OnDisconnectedFromServer(info:NetworkDisconnection) {
	showConnect = false;
	showJoinHost = true;
	showConnected = false;
}	

function OnGUI()
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
