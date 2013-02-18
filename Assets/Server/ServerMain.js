#pragma strict
//http://3dgep.com/?p=4609#more-4609

var listenPort = 25000;
var maxClients = 5;
 
function startServer() {
    Network.InitializeServer(maxClients, listenPort, false);    
}

function stopServer() {
    Network.Disconnect();
}

function OnGUI ()
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
 
function showClientInformation() {
    GUILayout.Label("Clients: " + Network.connections.Length + "/" + maxClients);
        for(var p: NetworkPlayer in Network.connections) {
        GUILayout.Label(" Player from ip/port: " + p.ipAddress + "/" + p.port); 
    }
}
 
function showServerInformation() {
    GUILayout.Label("IP: " + Network.player.ipAddress + " Port: " + Network.player.port);  
}

@script RequireComponent(ServerPlayerManager)
private var spm : ServerPlayerManager;
function Awake() {
    spm = gameObject.GetComponent(ServerPlayerManager);
}
 
function OnPlayerConnected(player: NetworkPlayer) { 
    spm.spawnPlayer(player);
}

function OnPlayerDisconnected(player : NetworkPlayer) {
    Debug.Log("Player disconnected");
    spm.deletePlayer(player);
}