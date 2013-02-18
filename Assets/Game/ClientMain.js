#pragma strict

var remoteIP = "127.0.0.1";
var remotePort = 25000;
 
function connectToServer() {
    Network.Connect(remoteIP, remotePort);  
}
 
function disconnectFromServer() {
    Network.Disconnect();
}

function OnGUI ()
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
        
    }
}

function OnDisconnectedFromServer (info : NetworkDisconnection) {
    var gos : GameObject[] = GameObject.FindGameObjectsWithTag("Player");
    for(var go : GameObject in gos) {
        Destroy(go);
    }
}

function Update() {
    if(Input.anyKey) {
        sendInputToServer();
    }
}
 
function sendInputToServer() {
    Debug.Log("Getting ready to send input");
    var vertical: float = Input.GetAxis("Vertical");
    var horizontal: float = Input.GetAxis("Horizontal");
    if((vertical!=0)||(horizontal!=0)) {
        networkView.RPC("handlePlayerInput",RPCMode.Server,Network.player,vertical, horizontal);
    }
}

@RPC
function handlePlayerInput(player: NetworkPlayer, vertical: float, horizontal: float) {
}