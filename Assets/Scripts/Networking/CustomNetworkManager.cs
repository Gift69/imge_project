using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustumNetworkManager : NetworkManager
{
    public GameObject networkLogic;
    public GameObject networkLogicIngame;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
        //PassBetweenScenes.playerInstance = player;
        //player.GetComponent<PlayerData>().playername = PassBetweenScenes.playername;
    }

    public override void OnClientDisconnect()
    {
        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().RemovePlayer(PassBetweenScenes.playername);
        base.OnClientDisconnect();
    }

    public override void Awake()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
        base.Awake();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Stage")
        {
            var x = GameObject.Instantiate(networkLogicIngame);
            NetworkServer.Spawn(x);
        }
    }


}
