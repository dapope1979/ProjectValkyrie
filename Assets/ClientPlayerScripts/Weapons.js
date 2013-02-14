#pragma strict

var commandRelay: CommandRelay;

function Start () {
	Debug.Log("Took weapons");
}
	
// Update is called once per frame
function Update () {
	if (Input.GetKeyDown ("f"))
	{
		commandRelay.Fire();
	}
}