#pragma strict

var ship:PlayerShip;

function Start () {
	Debug.Log("Took the helm");
}
	
// Update is called once per frame
function Update () {
	if (Input.GetKeyDown ("f"))
	{
		if (Network.peerType == NetworkPeerType.Client) {
			ship.networkView.RPC("Fire", RPCMode.Server);
		}
		else {
	    	ship.Fire();
    	}
	}
}