using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager Instance {
		get;
		private set;
	}

	PlayerManager() {
		Instance = this;
	}

	public List<Player> players = new List<Player>();
	Dictionary<string, Player> playersByName = new Dictionary<string, Player>();

	public Transform spawnPosition;
	public Player playerPrefab;   // set in inspector

	public void OnJoinedRoom()
	{
		Debug.Log("Joined: " + PhotonNetwork.playerName);

		Vector3 spawnPos = Vector3.zero;
		if (spawnPosition != null)
		{
			spawnPos = spawnPosition.position;
		}

//		Vector3 random = Random.insideUnitSphere;
//		random.y = 0;
//		random = random.normalized;


		var go = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity, 0);
		NotifyPlayerCreated (go.GetComponent<Player>());
	}

	void Start() {
	}

	void Update() {
		
	}

	void NotifyPlayerCreated(Player player) {
	}

	public void SpawnPlayer(Player player) {
	}

	public void NotifyPlayerDeath(Player player) {
	}

	public void NotifyPlayerDisconnect(Player player) {
		
	}
}
