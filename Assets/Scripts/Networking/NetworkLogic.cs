using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkLogic : NetworkBehaviour
{
    [SyncVar]
    public List<GameObject> players = new List<GameObject>();

    void Update()
    {
        Debug.Log(players.Count);
    }

    
}
