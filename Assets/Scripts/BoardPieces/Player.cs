using Mirror;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : BoardPiece
{
    public Sprite empty;
    public int score = 0;
    public GameObject virtualPlayerPrefab;

    private Ingame_UI ui;

    public const int ACTION_COUNT = 5;

    public Action[] actions = new Action[ACTION_COUNT];
    public List<GameObject>[] virtualActionObjs = new List<GameObject>[ACTION_COUNT];

    public VirtualPlayer vPlayer = null;

    public int stunDuration = 0;

    public VirtualPlayer VPlayer
    {
        get { if (vPlayer == null) cell.placeBoardPiece(vPlayer = Instantiate(virtualPlayerPrefab).GetComponent<VirtualPlayer>()); return vPlayer; }
        set { vPlayer = value; }
    }

    public void selectAction(Action action)
    {
        for (int i = 0; i < ACTION_COUNT; i++)
        {
            if (actions[i] == null)
            {
                actions[i] = action;
                action.selectableButton.SetEnabled(false);

                var button = ui.GetExecutableActionButton(i);
                button.style.backgroundImage = new UnityEngine.UIElements.StyleBackground(action.getIcon());
                button.SetEnabled(true);

                PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setActionForPlayer(PassBetweenScenes.id, i, action.toSync());

                for (int j = i; j < ACTION_COUNT && actions[j] != null; j++)
                {
                    virtualActionObjs[j] = actions[j].executeVirtual(VPlayer);
                }
                return;
            }
        }
    }

    public void removeActionAt(int index)
    {
        actions[index].selectableButton.SetEnabled(true);
        actions[index].unsetValue();

        var button = ui.GetExecutableActionButton(index);
        button.style.backgroundImage = new StyleBackground(empty);
        button.SetEnabled(false);

        PassBetweenScenes.playerInstance.GetComponent<OnPlayerSpawn>().setActionForPlayer(PassBetweenScenes.id, index, new SyncAction(Action.Type.NOTHING, new()));


        actions[index] = null;

        for (int i = 0; i < ACTION_COUNT; i++)
        {
            if (virtualActionObjs[i] != null)
            {
                foreach (GameObject obj in virtualActionObjs[i])
                {
                    Destroy(obj);
                }
                virtualActionObjs[i] = null;
            }
        }
        cell.placeBoardPiece(VPlayer);

        for (int i = 0; i < ACTION_COUNT; i++)
        {
            if (actions[i] == null)
                break;
            virtualActionObjs[i] = actions[i].executeVirtual(VPlayer);
        }

    }

    public void removeAction(Action action)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] == action)
            {
                removeActionAt(i);
                return;
            }
        }
    }
    public void removeAllActions()
    {
        GameObject.Destroy(vPlayer.gameObject);
        vPlayer = null;
        for (int i = 0; i < ACTION_COUNT; i++)
        {
            actions[i] = null;
            if (virtualActionObjs[i] != null)
            {
                foreach (GameObject obj in virtualActionObjs[i])
                {
                    Destroy(obj);
                }
                virtualActionObjs[i] = null;
            }
        }
    }

    public void debugState()
    {
        for (int i = 0; i < ACTION_COUNT; i++)
        {
            Debug.Log(i + ": action=" + (actions[i] == null ? "null" : "(" + actions[i].getValue().x + ", " + actions[i].getValue().y + ")") + " virtual-objs=" + (virtualActionObjs[i] == null ? "null" : "not null"));
        }
    }

    // Start is called before the first frame update
    public new void Awake()
    {
        base.Awake();
        ui = GameObject.FindGameObjectWithTag("IngameUI").GetComponent<Ingame_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            this.cell.getCellRelative(new(1, 0)).placeBoardPiece(this);
        else if (Input.GetKeyDown(KeyCode.A))
            this.cell.getCellRelative(new(-1, 0)).placeBoardPiece(this);
        this.cell = this.cell;
    }
}
