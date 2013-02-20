using UnityEngine;
using System.Collections;

public class ServerPlayerManager : MonoBehaviour {
	
	public static NetworkPlayer emptyPlayer;
	
	Hashtable players = new Hashtable();
	ValkyrieShip ship;

	void Awake () {
	 	DontDestroyOnLoad(this);
	}
	 
	public void spawnPlayer(NetworkPlayer player) {
		PlayerInfo ply = (PlayerInfo) GameObject.FindObjectOfType(typeof(PlayerInfo));
		GameObject go = (GameObject) Network.Instantiate(ply.playerPrefab, Vector3.up*3, Quaternion.identity, 0);
		players[player] = go;
	}

	public void deletePlayer(NetworkPlayer player) {
		Debug.Log("Deleting player");
		GameObject go = (GameObject) players[player];
		Network.RemoveRPCs(go.networkView.viewID); 
		Network.Destroy(go);
		Network.DestroyPlayerObjects(player);
		players.Remove(player);
		
		// I think I would prefer to move this into the ship
		// ship.RemovePlayer
		if (ship.helmPlayer == player) {
			ship.helmPlayer=ServerPlayerManager.emptyPlayer;	
		}
	}

	void OnLevelWasLoaded(int level) {
		ship = (ValkyrieShip) GameObject.FindObjectOfType(typeof(ValkyrieShip));
	}

	[RPC]
	void handlePlayerInput(NetworkPlayer player, float vertical, float horizontal) {
		Debug.Log("Handling input on the server");
		GameObject go = (GameObject) players[player];
		go.transform.position = go.transform.position + Vector3.right*horizontal;
		go.transform.position = go.transform.position + Vector3.forward*vertical;
	}

	// Map in input from the player and relay to the ship object
	// use interaction with the ship object to occupy stations and check if occupied
	// work out how to load stations player side and then how to layout this portion
	[RPC]
	void thrust(NetworkPlayer player) {
		//Debug.Log("Thrust ship");
		ship.Thrust(player);
	}
	
	[RPC]
	void TakeHelm(NetworkPlayer player) {
		Debug.Log ("Client claimed helm");
		ship.helmPlayer = player;
	}

	
}
