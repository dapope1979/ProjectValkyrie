using UnityEngine;
using System.Collections;

public class ValkyrieShip : MonoBehaviour {
	
	
	public NetworkPlayer helmPlayer;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (helmPlayer.ToString());
	}

	// most of these are going to be called in by serverPlayerManager
	// use to handle transformations and track ship statistics
	public void Thrust(NetworkPlayer player) {
		transform.position += Vector3.up * 1;
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		stream.Serialize(ref helmPlayer);
	    
	}
}
