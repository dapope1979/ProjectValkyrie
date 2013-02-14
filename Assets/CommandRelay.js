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

// proof of concept for receiving ship info from server
@RPC
function fromShipToRelay() {
	Debug.Log("Getting ship info");
}