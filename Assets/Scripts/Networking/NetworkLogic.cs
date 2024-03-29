using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkLogic : NetworkBehaviour
{
    public SyncList<int> pickedCharacter = new SyncList<int>();
    public SyncList<PlayerActions> otherplayerActions = new SyncList<PlayerActions>();
    public GameObject magePrefab;
    public GameObject knightPrefab;

    public GameObject minerPrefab;

    public GameObject gentlemanPrefab;

    public GameObject vPlayerPrefab;

    public Player[] players;

    public SyncList<GameObject> playerObjects = new SyncList<GameObject>();

    public SyncAction[][] playerActions;

    public delegate void Finish();

    public delegate IEnumerator ActionCoroutine(Finish finish, Player player, HexField.Coord value);

    public Dictionary<Action.Type, ActionCoroutine> actionCallbacks = new Dictionary<Action.Type, ActionCoroutine>();

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

    public HexField hexfield;

    private int playerCount;

    public int refCount = 0;

    private readonly Dictionary<int, HexField.Coord[]> spawnCoords = new Dictionary<int, HexField.Coord[]> {
        {2, new HexField.Coord[] { new(-4, 0), new(4, 0) } },
        {3, new HexField.Coord[] { new(-4, 0), new(0, -4), new(0, 0, 4) } },
        {4, new HexField.Coord[] { new(-1, 3, 0), new(1, 0, 3), new(1, -3, 0), new(-1, 0, -3) } }
    };

    private bool started = false;

    void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < 4; i++)
                pickedCharacter.Add(0);
        }
    }

    public void StartReal()
    {
        started = true;
        hexfield = GameObject.FindGameObjectWithTag("HexField").GetComponent<HexField>();
        if (isServer)
        {
            Debug.Log("SceneManager.GetActiveScene().name");
            playerCount = PassBetweenScenes.playercount;

            playerActions = new SyncAction[playerCount][];

            for (int i = 0; i < playerCount; i++)
            {
                playerActions[i] = new SyncAction[Player.ACTION_COUNT];
                for (int j = 0; j < Player.ACTION_COUNT; j++)
                    playerActions[i][j] = new SyncAction(Action.Type.NOTHING, new());
            }

            players = new Player[playerCount];

            GameObject playerObj;
            for (int i = 0; i < playerCount; i++)
            {
                switch (pickedCharacter[i])
                {
                    case 0:
                        playerObj = Instantiate(magePrefab);
                        break;
                    case 1:
                        playerObj = Instantiate(gentlemanPrefab);
                        break;
                    case 2:
                        playerObj = Instantiate(knightPrefab);
                        break;
                    default:
                        playerObj = Instantiate(minerPrefab);
                        break;
                }
                playerObj.GetComponent<Spielfigur>().materialIndex = i;
                playerObj.GetComponent<Spielfigur>().OnChangeMaterial(i, i);
                NetworkServer.Spawn(playerObj);
                Debug.Log(playerObj);
                playerObjects.Add(playerObj);
                players[i] = playerObj.GetComponent<Player>();
                hexfield.cellAt(spawnCoords[playerCount][i]).placeBoardPiece(players[i]);
            }

            actionCallbacks.Add(Action.Type.MOVE, moveAction);
            actionCallbacks.Add(Action.Type.NOTHING, nothingAction);
            actionCallbacks.Add(Action.Type.BOMB, bombAction);
        }
    }

    void Update()
    {
        if (started && isServer)
        {
            for (int i = 0; i < playerObjects.Count; i++)
            {
                playerObjects[i].GetComponent<Spielfigur>().materialIndex = i;
                playerObjects[i].GetComponent<Spielfigur>().OnChangeMaterial(i, i);
            }
            if (mode == Mode.START)
            {
                StartCoroutine(coroutine());

            }
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
        }
    }

    private class WaitForFrames : CustomYieldInstruction
    {
        private int _targetFrameCount;

        public WaitForFrames(int numberOfFrames)
        {
            _targetFrameCount = Time.frameCount + numberOfFrames;
        }

        public override bool keepWaiting
        {
            get
            {
                return Time.frameCount < _targetFrameCount;
            }
        }
    }

    [Server]
    public IEnumerator coroutine()
    {
        timer = 20;
        mode = Mode.ACTION_SELECTION;
        yield return new WaitWhile(() => timer > 0);
        timer = 30;
        mode = Mode.ACTION_ORDERING;
        yield return new WaitWhile(() => timer > 0);
        // Padding time
        yield return new WaitForSeconds(0.5f);
        mode = Mode.ACTION_EXECUTION;

        for (int j = 0; j < playerActions[0].Length; j++)
        {
            refCount = 0;
            for (int i = 0; i < playerCount; i++)
            {
                incRef();
                StartCoroutine(actionCallbacks[playerActions[i][j].type](this.decRef, players[i], playerActions[i][j].value));
                playerActions[i][j] = new SyncAction(Action.Type.NOTHING, new());
            }
            Debug.Log("It " + j);
            yield return new WaitUntil(() => refCount == 0);
        }
        otherplayerActions.Clear();

        mode = Mode.START;
    }

    public void decRef()
    {
        refCount--;
    }

    public void incRef()
    {
        refCount++;
    }

    public static IEnumerator moveAction(Finish finish, Player player, HexField.Coord value)
    {
        float x = 0;
        Vector3 start = player.cell.transform.position + Vector3.up * 0.4f;
        Vector3 end = player.cell.getCellRelative(value).transform.position + Vector3.up * 0.4f;
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
        finish();
    }

    public static IEnumerator nothingAction(Finish finish, Player player, HexField.Coord value)
    {
        yield return new WaitForFrames(30);
        finish();
    }

    public static IEnumerator bombAction(Finish finish, Player player, HexField.Coord value)
    {
        GameObject bomb = GameObject.FindWithTag("bomb");
        GameObject bomb1 = Instantiate(bomb, player.transform.position, Quaternion.identity);
        BoardPiece bomb_ = bomb1.GetComponent<BoardPiece>();
        player.cell.getCellRelative(value).placeBoardPiece(bomb_);
        finish();
        yield return null;
    }
}
