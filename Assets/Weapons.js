#pragma strict

var commandRelay: CommandRelay;

function Start () {
	Debug.Log("Took the helm");
}
	
// Update is called once per frame
function Update () {
	if (Input.GetKeyDown ("f"))
	{
		commandRelay.Fire();
	}
}