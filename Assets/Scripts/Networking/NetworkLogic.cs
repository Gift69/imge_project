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

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public SyncAction[][] playerActions;

    public Dictionary<Action.Type, Func<bool[], int, Player, HexField.Coord, IEnumerator>> actionCallbacks = new Dictionary<Action.Type, Func<bool[], int, Player, HexField.Coord, IEnumerator>>();

    [SyncVar]
    public float timer = 0;

    public SyncList<Player> players = new SyncList<Player>();

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

    private void Start()
    {
        if (isServer)
        {

            playerActions = new SyncAction[2][];

            playerActions[0] = new SyncAction[]{
            new SyncAction(Action.Type.MOVE, new(1, 0)),
            new SyncAction(Action.Type.MOVE, new(0, 1)),
            new SyncAction(Action.Type.MOVE, new(0, 0, -1))
        };

            playerActions[1] = new SyncAction[]{
            new SyncAction(Action.Type.MOVE, new(-1, 0)),
            new SyncAction(Action.Type.MOVE, new(0, -1)),
            new SyncAction(Action.Type.MOVE, new(0, 0, 1))
        };

            actionFinished = new bool[2];

            var playerObj = Instantiate(playerPrefab);
            NetworkServer.Spawn(playerObj);
            var player = playerObj.GetComponent<Player>();
            hexfield.cellAt(2, 0).placeBoardPiece(player);

            players.Add(player);

            playerObj = Instantiate(playerPrefab);
            NetworkServer.Spawn(playerObj);
            player = playerObj.GetComponent<Player>();
            hexfield.cellAt(-2, 0).placeBoardPiece(player);

            players.Add(player);


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
        mode = Mode.ACTION_EXECUTION;
        timer = 5;

        for (int j = 0; j < playerActions[0].Length; j++)
        {
            for (int i = 0; i < playerActions.Length; i++)
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
