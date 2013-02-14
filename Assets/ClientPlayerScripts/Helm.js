#pragma strict

var commandRelay:CommandRelay;

function Start () {
}
	
// Update is called once per frame
function Update () {
	if (Input.GetKeyDown ("space"))
	{
		commandRelay.ToggleMoving();		
	}
}

