using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkLogic : NetworkBehaviour
{
    public GameObject playerPrefab;
    public GameObject vPlayerPrefab;

    public Player[] players;
    public SyncAction[][] playerActions;

    public Dictionary<Action.Type, Func<bool[], int, Player, HexField.Coord, IEnumerator>> actionCallbacks = new Dictionary<Action.Type, Func<bool[], int, Player, HexField.Coord, IEnumerator>>();

    [SyncVar]
    public float timer = 0;


    public enum Mode
    {
        ACTION_SELECTION,
        ACTION_ORDERING,
        ACTION_EXECUTION,
        WAIT,
        START
    }

    [SyncVar]
    public Mode mode = Mode.START;

    private bool[] actionFinished;
    public HexField hexfield;

    private int playerCount;

    private readonly Dictionary<int, HexField.Coord[]> spawnCoords = new Dictionary<int, HexField.Coord[]> {
        {2, new HexField.Coord[] { new(-4, 0), new(4, 0) } },
        {3, new HexField.Coord[] { new(-4, 0), new(0, -4), new(0, 0, 4) } },
        {4, new HexField.Coord[] { new(-1, 3, 0), new(1, 0, 3), new(1, -3, 0), new(-1, 0, -3) } }
    };

    private void Start()
    {
        if (isServer)
        {
            playerCount = 4; // GameObject.Find("ConnectedPlayers").GetComponent<ConnectedPlayers>().playernames.Count;

            playerActions = new SyncAction[playerCount][];

            for (int i = 0; i < playerCount; i++)
                playerActions[i] = new SyncAction[]{
                    new SyncAction(Action.Type.MOVE, new(1, 0)),
                    new SyncAction(Action.Type.MOVE, new(0, 1)),
                    new SyncAction(Action.Type.MOVE, new(0, 0, -1))
                };

            actionFinished = new bool[playerCount];

            players = new Player[playerCount];

            GameObject playerObj;
            for (int i = 0; i < playerCount; i++)
            {
                playerObj = Instantiate(playerPrefab);
                Debug.Log(playerObj);
                NetworkServer.Spawn(playerObj);
                players[i] = playerObj.GetComponent<Player>();
                hexfield.cellAt(spawnCoords[playerCount][i]).placeBoardPiece(players[i]);
            }


            hexfield.currentPlayer = players[0];

            actionCallbacks.Add(Action.Type.MOVE, moveAction);
        }
    }

    void Update()
    {
        if (isServer)
        {
            if (mode == Mode.START)
            {
                StartCoroutine(coroutine());

            }
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
        }
    }

    [Server]
    public IEnumerator coroutine()
    {
        timer = 2;
        mode = Mode.ACTION_SELECTION;
        yield return new WaitWhile(() => timer > 0);
        timer = 2;
        mode = Mode.ACTION_ORDERING;
        yield return new WaitWhile(() => timer > 0);
        // Padding time
        yield return new WaitForSeconds(0.5f);
        mode = Mode.ACTION_EXECUTION;
        timer = 5;

        for (int j = 0; j < playerActions[0].Length; j++)
        {
            for (int i = 0; i < playerCount; i++)
            {
                actionFinished[i] = false;
                StartCoroutine(actionCallbacks[playerActions[i][j].type](actionFinished, i, players[i], playerActions[i][j].value));
            }
            Debug.Log("It " + j);
            yield return new WaitUntil(() => { foreach (bool a in actionFinished) if (!a) return false; return true; });
        }

        mode = Mode.START;
        timer = 0;
    }

    public IEnumerator move(bool[] a, int index)
    {
        yield return new WaitForSeconds(5);
        a[index] = true;
    }

    public IEnumerator shoot(bool[] a, int index)
    {
        yield return new WaitForSeconds(3);
        a[index] = true;
    }

    public static IEnumerator moveAction(bool[] actionActive, int index, Player player, HexField.Coord value)
    {
        float x = 0;
        Vector3 start = player.cell.transform.position;
        Vector3 end = player.cell.getCellRelative(value).transform.position;
        while (x < 0.5)
        {
            player.transform.position = start * (1 - x) + end * x;
            x += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
        player.coord += value;
        while (x < 1.0)
        {
            player.transform.position = start * (1 - x) + end * x;
            x += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
        player.cell = player.cell;
        actionActive[index] = true;
    }
}
