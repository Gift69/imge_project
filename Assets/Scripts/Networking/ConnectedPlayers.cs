using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectedPlayers : NetworkBehaviour
{
    public SyncList<string> playernames = new SyncList<string>();

    [SyncVar]
    public bool started = false;
}