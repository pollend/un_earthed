using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject spawn;

	string typeName = "Unearth";
	string gameName = "Unearth 1";
	string currentServer = "";
	HostData[] hostList;

//	bool inLobby = true;

	void Start()
	{
		SpawnPlayer(0);
//		StartServer();
//		RefreshHostList();
	}
	
//	void OnGUI()
//	{
//		if (!Network.isClient && !Network.isServer && inLobby)
//		{
//			if (GUI.Button(new Rect(10, 10, 100, 50), "Start Server"))
//				StartServer();
//			
//			if (GUI.Button(new Rect(10, 70, 100, 50), "Join Game")) {
//				RefreshHostList();
//			}
//			
//			if (hostList != null)
//			{
//				if (hostList.Length > 0) {
//					inLobby = false;
//					JoinServer(hostList[0]);
//				}
//			}
//		}
//	}

	void StartServer()
	{
		Network.InitializeServer(4, 24000, Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Started");
		SpawnPlayer(0);
	}

	void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	void JoinServer(HostData hostData)
	{
		currentServer = hostData.gameName;
		Network.Connect(hostData);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Joined " + currentServer);
		SpawnPlayer(1);
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log("Player connected");
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Debug.Log("Player Left");
		Network.DestroyPlayerObjects(player);
	}

	void SpawnPlayer(int offset)
	{
//		GameObject player = Network.Instantiate(playerPrefab, new Vector3(0f, 0f, -0.1f), Quaternion.identity, 0) as GameObject;
		GameObject player = Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity) as GameObject;
		Camera.main.transform.parent = player.transform;
	}
}
