#pragma strict

var ship:PlayerShip;

function Start () {
	Debug.Log("Took the helm");
}
	
// Update is called once per frame
function Update () {
	if (Input.anyKeyDown)
	{
		Debug.Log(ship);
	    ship.ToggleMoving();
	}
}

