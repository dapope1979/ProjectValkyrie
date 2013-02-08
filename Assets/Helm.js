#pragma strict

var ship:PlayerShip;

function Start () {

}
	
// Update is called once per frame
function Update () {
	if (Network.isClient && Input.anyKeyDown)
	{
		Debug.Log ("sending");
	    ship.networkView.RPC("ToggleMoving", RPCMode.Others);
	}
}