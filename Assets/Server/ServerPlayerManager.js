#pragma strict
 
var players = new Hashtable();
 
function spawnPlayer(player : NetworkPlayer) {
    var ply : PlayerInfo = GameObject.FindObjectOfType(PlayerInfo);
    var go : GameObject = Network.Instantiate(ply.playerPrefab, Vector3.up*3, Quaternion.identity, 0);
    players[player] = go;
}

function deletePlayer(player : NetworkPlayer) {
	Debug.Log("Deleting player");
    var go : GameObject = players[player];
    Network.RemoveRPCs(go.networkView.viewID); 
    Network.Destroy(go); 
    players.Remove(player); 
}

@RPC
function handlePlayerInput(player: NetworkPlayer, vertical: float, horizontal: float) {
    Debug.Log("Handling input on the server");
    var go : GameObject = players[player];
    go.transform.position = go.transform.position + Vector3.right*horizontal;
    go.transform.position = go.transform.position + Vector3.forward*vertical;
}