using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {
	
	private string version = "v001";
	
	public string playerPrefabName = "Chopper";
	private SpawnPoint[] spawnPoints;

	public void Start () {
		spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

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
		SpawnMyPlayer ();
	}
	
	public void SpawnMyPlayer() {
		Debug.Log ("SpawnMyPlayer");

		SpawnPoint spawnPoint = GetSpawnPoint ();
		GameObject myPlayerObject = PhotonNetwork.Instantiate (playerPrefabName, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);

		ChopperInput chopperInput = myPlayerObject.GetComponent<ChopperInput> ();
		chopperInput.enabled = true;
	}

	public SpawnPoint GetSpawnPoint() {
		SpawnPoint spawnPoint;
		do {
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		} while(!spawnPoint.IsClear());
		return spawnPoint;
	}
}








