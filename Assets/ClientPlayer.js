#pragma strict

function Start () {
	transform.Find("Helm").gameObject.SetActive(false);	
}

function Update () {

}

function OnGUI()
{		
	if (Network.peerType == NetworkPeerType.Client)  {
		if (GUI.Button(new Rect(10, 30, 100, 20), "Take the Helm")) {
			transform.Find("Helm").gameObject.SetActive(true);	
		}
	}
}