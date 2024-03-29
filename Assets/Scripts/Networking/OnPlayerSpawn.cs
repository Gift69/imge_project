using UnityEngine;
using Mirror;
using static NetworkLogic;

public class OnPlayerSpawn : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        PassBetweenScenes.playerInstance = this.gameObject;
    }
    [Command]
    public void requestAuthority(GameObject gameObject, GameObject player)
    {
        var x = gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(player.GetComponent<NetworkIdentity>().connectionToClient);
        Debug.Log(x);
    }
    [Command]
    public void removeAuthority(GameObject gameObject)
    {
        gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority();
    }

    [Command]
    public void AddPlayer(string playername)
    {
        GameObject.Find("ConnectedPlayers").GetComponent<ConnectedPlayers>().playernames.Add(playername);
    }

    [Command]
    public void RemovePlayer(string playername)
    {
        GameObject.Find("ConnectedPlayers").GetComponent<ConnectedPlayers>().playernames.Remove(playername);
    }

    [Command]
    public void StopServer()
    {
        GameObject.Find("ConnectedPlayers").GetComponent<ConnectedPlayers>().playernames.RemoveAll(x => true);
        GameObject.Find("NetworkManager").GetComponent<CustumNetworkManager>().StopHost();
    }

    [Command]
    public void AddActions(PlayerActions actions)
    {
        GameObject.FindGameObjectWithTag("NetworkLogic").GetComponent<NetworkLogic>().otherplayerActions.Add(actions);
    }

    [Command]
    public void setActionForPlayer(int playerIndex, int position, SyncAction action)
    {
        var netlogic = GameObject.FindGameObjectWithTag("NetworkLogic").GetComponent<NetworkLogic>();
        if (netlogic.mode == Mode.ACTION_ORDERING)
            netlogic.playerActions[playerIndex][position] = action;
    }


    [Command]
    public void setPickedCharacter(int id, int characterId)
    {
        var netlogic = GameObject.FindGameObjectWithTag("NetworkLogic").GetComponent<NetworkLogic>();
        netlogic.pickedCharacter[id] = characterId;
    }

}