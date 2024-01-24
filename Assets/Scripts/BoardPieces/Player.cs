using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : BoardPiece
{
    public int score = 0;
    public GameObject virtualPlayerPrefab;

    public const int ACTION_COUNT = 5;

    public Action[] actions = new Action[ACTION_COUNT];
    public List<GameObject>[] virtualActionObjs = new List<GameObject>[ACTION_COUNT];

    private VirtualPlayer vPlayer = null;

    public VirtualPlayer VPlayer
    {
        get { if (vPlayer == null) cell.placeBoardPiece(vPlayer = Instantiate(virtualPlayerPrefab).GetComponent<VirtualPlayer>()); return vPlayer; }
        set { vPlayer = value; }
    }

    public void selectAction(Action action)
    {
        for(int i = 0; i < ACTION_COUNT; i++)
        {
            if (actions[i] == null)
            {
                actions[i] = action;

                for(int j = i; j < ACTION_COUNT && actions[j] != null; j++)
                {
                    virtualActionObjs[j] = actions[j].executeVirtual(VPlayer);
                }
                debugState();
                return;
            }
        }
    }

    public void removeActionAt(int index)
    {
        actions[index] = null;
        for (int i = 0; i < ACTION_COUNT; i++)
        {
            if (virtualActionObjs[i] != null)
            {
                foreach(GameObject obj in virtualActionObjs[i])
                {
                    Destroy(obj);
                }
                virtualActionObjs[i] = null;
            }
        }
        cell.placeBoardPiece(VPlayer);

        for(int i = 0; i < ACTION_COUNT; i++)
        {
            if (actions[i] == null)
                break;
            virtualActionObjs[i] = actions[i].executeVirtual(VPlayer);
        }
        debugState();
    }

    public void removeAction(Action action)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            if (actions[i] == action)
            {
                removeActionAt(i);
                return;
            }
        }
    }

    public void debugState()
    {
        for(int i = 0; i < ACTION_COUNT; i++)
        {
            Debug.Log(i + ": action=" + (actions[i] == null ? "null" : "(" + actions[i].getValue().x + ", " + actions[i].getValue().y + ")") + " virtual-objs=" + (virtualActionObjs[i] == null ? "null" : "not null"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
