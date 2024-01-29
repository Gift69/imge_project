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
        GameObject.Find("NetworkLogic").GetComponent<ConnectedPlayers>().playernames.Add(playername);
    }

}