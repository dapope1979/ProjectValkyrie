#pragma strict

var ship:PlayerShip;

function Start () {

}

function Update () {
	//Debug.Log(ship.moving);
}

function ToggleMoving() {
	if (Network.peerType == NetworkPeerType.Client) {
		ship.networkView.RPC("ToggleMoving", RPCMode.Server);
	}
	else {
    	ship.ToggleMoving();
	}
}

function Fire() {
	if (Network.peerType == NetworkPeerType.Client) {
		ship.networkView.RPC("Fire", RPCMode.Server);
	}
	else {
    	ship.Fire();
	}
}

// not implemented in PlayerShip yet
function Abandon() {
	if (Network.peerType == NetworkPeerType.Client) {
		ship.networkView.RPC("Abandon", RPCMode.Server);
	}
	else {
    	ship.Abandon();
	}
}

// read the JSON and store in variables
@RPC
function fromShipToRelay(jsonString:String) {
	var state = JSONUtils.ParseJSON( jsonString );	
	// append gameTime to force Unity's console to treat like a new log statement
	Debug.Log("Getting ship info " +state["moving"] +" " +state["helmOccupied"] +" " +Time.timeSinceLevelLoad);
}