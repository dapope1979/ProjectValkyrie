#pragma strict
var ship:PlayerShip;

function Start () {
	transform.Find("Helm").gameObject.SetActive(false);	
}

function Update () {

}

function OnGUI()
{		
	if ((Network.peerType == NetworkPeerType.Client) || (Network.peerType == NetworkPeerType.Server))  {
		if (GUI.Button(new Rect(10, 30, 100, 20), "Take the Helm")) {
			var helmObject : GameObject = transform.Find("Helm").gameObject;
			var helmScript : Helm = helmObject.GetComponent(Helm);
			helmObject.SetActive(true);
			helmScript.ship = ship;
		}
		if (GUI.Button(new Rect(10, 60, 100, 20), "Take Weapons")) {
			var weaponsObject : GameObject = transform.Find("Weapons").gameObject;
			var weaponsScript : Weapons = weaponsObject.GetComponent(Weapons);
			weaponsObject.SetActive(true);
			weaponsScript.ship = ship;
		}
	}
}