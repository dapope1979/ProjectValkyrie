#pragma strict



var ship:PlayerShip;
var shipState:Hashtable;

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

function IsHelmOccupied() {
	return shipState["helmOccupied"];
}

function TakeTheHelm() {
	if (Network.peerType == NetworkPeerType.Client) {
		ship.networkView.RPC("SetHelmOccupied", RPCMode.Server, true);
	}
	else {
    	ship.SetHelmOccupied(true);
	}
}

// not implemented in PlayerShip yet
function AbandonHelm() {
	if (Network.peerType == NetworkPeerType.Client) {
		ship.networkView.RPC("SetHelmOccupied", RPCMode.Server, false);
	}
	else {
    	ship.SetHelmOccupied(false);
	}
}

// read the JSON and store in variables
@RPC
function fromShipToRelay(jsonString:String) {
	shipState = JSONUtils.ParseJSON( jsonString );	
	// append gameTime to force Unity's console to treat like a new log statement
	//Debug.Log("Getting ship info " +shipState["moving"] +" " +shipState["helmOccupied"] +" " +Time.timeSinceLevelLoad);
}