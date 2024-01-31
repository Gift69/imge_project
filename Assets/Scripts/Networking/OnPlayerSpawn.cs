using UnityEngine;
using Mirror;

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


}