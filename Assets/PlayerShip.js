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
}

@RPC
function ToggleMoving() {
	moving = !moving;
}

@RPC
function Fire() {
	Debug.Log("Firing");
}