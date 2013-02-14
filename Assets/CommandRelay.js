#pragma strict

var ship:PlayerShip;

function Start () {

}

function Update () {

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