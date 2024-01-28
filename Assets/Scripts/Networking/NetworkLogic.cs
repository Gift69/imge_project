using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkLogic : NetworkBehaviour
{
    public SyncList<String> playernames = new SyncList<String>();

    void Update()
    {
        ///Debug.Log(players.Count);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
