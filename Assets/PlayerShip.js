#pragma strict

var moving = false;
var helmOccupied = false;

	// Use this for initialization
function Start () {

}

// Update is called once per frame
function Update () {
	if (moving) {
		var moveDir = Vector3(0, 0, 1);
		var speed = 5;
    	transform.Translate(speed * moveDir * Time.deltaTime);
	}
	
	// proof of concept for sending ship data to clients
	// I am thinking we should serialize to a JSON object and pass as an argument
	// clients can then pull the info relevant to their stations
	// will mostly be used for things like text display and whether or not a station is filled
	if (Network.peerType == NetworkPeerType.Server) {
		var go:GameObject = GameObject.Find("CommandRelay"); 
		var bm:CommandRelay = go.GetComponent(CommandRelay);
		
		
		var state = {
			"moving": moving,
			"helmOccupied": helmOccupied
		};
		
		var jsonString = JSONUtils.ObjectToJSON(state);
		
		go.networkView.RPC("fromShipToRelay", RPCMode.AllBuffered, jsonString);
	}
}

@RPC
function ToggleMoving() {
	moving = !moving;
}

@RPC
function Fire() {
	Debug.Log("Firing");
}

@RPC
function Abandon() {
	Debug.Log("Abandoning");
}