using UnityEngine;
using System.Collections;


public class NetworkManager : Photon.MonoBehaviour {
	
	private string version = "v001";
	
	public string playerPrefabName = "Chopper";
	private SpawnPoint[] spawnPoints;

	public void Start () {
		spawnPoints = GetComponents<SpawnPoint>();

		Connect ();
	}

	private void Connect() {
		Debug.Log ("Connect");
		PhotonNetwork.ConnectUsingSettings (version);
	}
	
	public void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	
	public void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	
	public void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");
		SpawnPlayer ();
	}
	
	public void SpawnPlayer() {
		Debug.Log ("SpawnPlayer");
				
	}

	public SpawnPoint GetSpawnPoint() {
		SpawnPoint spawnPoint;
		do {
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		} while(!spawnPoint.IsClear());
		return spawnPoint;
	}
}
