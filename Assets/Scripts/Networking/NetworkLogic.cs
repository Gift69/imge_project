using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkLogic : NetworkBehaviour
{
    public SyncList<String> playernames = new SyncList<String>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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

    private void Start()
    {
        actionFinished = new bool[2];
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
        timer = 3;
        mode = Mode.ACTION_SELECTION;
        yield return new WaitWhile(() => timer > 0);
        timer = 3;
        mode = Mode.ACTION_ORDERING;
        yield return new WaitWhile(() => timer > 0);
        mode = Mode.ACTION_EXECUTION;
        timer = 100;

        for (int i = 0; i < actionFinished.Length; i++)
        {
            actionFinished[i] = false;
        }
        StartCoroutine(move(actionFinished, 0));
        StartCoroutine(shoot(actionFinished, 1));

        yield return new WaitUntil(() => { foreach (bool a in actionFinished) if (!a) return false; return true; });
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
}
