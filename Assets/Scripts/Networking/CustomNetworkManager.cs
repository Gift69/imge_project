using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustumNetworkManager : NetworkManager
{   
    public GameObject networkLogic;

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

        //networkLogic.GetComponent<NetworkLogic>().players.Add(player);
        //player.GetComponent<PlayerData>().playername = PassBetweenScenes.playername;
    }
}
